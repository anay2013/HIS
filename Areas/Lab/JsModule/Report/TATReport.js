var ipoptype = "";
var subcatid = "";
var GroupTypeName = "";
$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    CategoryList();

});
function CategoryList() {
    $("#ddlCategory").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = '-';
    objBO.SubCat = '-';
    objBO.ReportStatus = '-';
    objBO.TestCode = '-';
    objBO.Prm1 = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = "CategoryList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlCategory").append($("<option></option>").val(value.CatID).html(value.CatName));
                    });
                }
                LoadSubCategory();
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LoadSubCategory() {
    $("#ddlTest").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
    $("#ddlSubCatList").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = '-';
    objBO.SubCat = '-';
    objBO.ReportStatus = '-';
    objBO.TestCode = '-';
    objBO.Prm1 = $("#ddlCategory option:selected").val();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = "LoadTestCategory";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlSubCatList").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
                    });
                }
                LoadTestData();
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LoadTestData() {
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = '-';
    objBO.SubCat = $("#ddlSubCatList option:selected").val();
    objBO.ReportStatus = '-';
    objBO.TestCode = '-';
    objBO.Prm1 = $("#ddlSubCatList option:selected").val();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = "LoadTest";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $("#ddlTest").empty();
                    $("#ddlTest").append($("<option></option>").val("ALL").html("ALL"));
                    if (Object.keys(data.ResultSet.Table).length > 0) {
                        $.each(data.ResultSet.Table, function (key, value) {
                            $("#ddlTest").append($("<option></option>").val(value.testcode).html(value.TestName));
                        });
                    }
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataTATReport() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.SubCat = $('#ddlSubCatList option:selected').val();
    objBO.ReportStatus = $('#ddlReportType option:selected').val();
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.Prm1 = $('#ddlCategory option:selected').val();
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.Logic = "TAT:Summary";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var temp = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                 
                        if (temp != val.ReportType) {
                            tbody += "<tr class='pr' style='background:#60d9606b;'>";
                            tbody += "<td style='font-size:13px;'><b>" + val.ReportType + "</b> <Span style='font-size:13px;float:right;margin-right:10px;'>Total:-</Span></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'><label>0</label></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'><label>0</label></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'><label>0</label></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'></td>";
                            tbody += "</tr>";
                            temp = val.ReportType
                        }
                        else {
                           
                        }
                        tbody += "<tr class='pt'>";
                        tbody += "<td hidden>" + val.SubCatID + "</td>";
                        tbody += "<td style='width:40%'>" + val.testCategory + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.NoTest + "</td>";
                        tbody += "<td style='text-align:center;width:20%;'><button class='btn-warning' onclick='SelectRowValue1(this)' id='btnAddDelayed' style='border:none;height:20px;width:50%;margin-bottom:3px;text-align:center;'>" + val.DelayByReg + " &nbsp;&nbsp;[" + val.DelayedPercByReg + "%]" + "</button></td>";
                        tbody += "<td style='text-align:center;width:20%;'><button class='btn-danger' onclick='SelectRowValue1(this)' id='btnAddDelayed' style='border:none;height:20px;width:50%;margin-bottom:3px;text-align:center;'>" + val.Delayed + " &nbsp;&nbsp;[" + val.DelayedPerc + "%]" + "</button></td>";
                        tbody += "<td style='width:10%;text-align:center'><button class='btn-success' onclick='SelectRowValue(this)' id='btnAdd' style='border:none;width:50%;height:20px;margin-bottom:3px;text-align:center;'>View</button></td>";
                        tbody += "<td hidden>" + val.ReportType + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);


                    TotalCal();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SelectRowValue(elem) {
    ipoptype = $('#ddlIpOpType option:selected').text();
    subcatid = $(elem).closest('tr').find('td:eq(0)').text();
    GroupTypeName = $(elem).closest('tr').find('td:eq(6)').text();
    $('#TATReportDetails').modal('show');
    GetDataTATReportDetails()
}

function GetDataTATReportDetails() {
    $('#tblTATDetails tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    if (GroupTypeName =="Local Lab Test") {
        objBO.Prm1 = '1';
    }
    if (GroupTypeName == "Out Source Lab") {
        objBO.Prm1 = '0';
    }
    objBO.LabCode = '-';
    objBO.IpOpType = ipoptype;
    objBO.SubCat = subcatid;
    objBO.ReportStatus = $('#ddlReportType option:selected').val();
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.Logic = "TAT:Report";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.DelayHrs != '0') {
                            tbody += "<tr style='background:#ffc0cb;'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td>" + val.IpOpType + "</td>";
                        tbody += "<td style='width:9%'>" + val.ipop_no + "</td>";
                        tbody += "<td style='width:10%'>" + val.testCategory + "</td>";
                        tbody += "<td style='width:10%'>" + val.TestName + "</td>";
                        tbody += "<td style='width:12%'>" + val.RegDate + "</td>"; 
                        tbody += "<td style='width:5%;text-align:center'>" + val.RS2Time + "</td>";
                        tbody += "<td style='width:12%'>" + val.SampleOrInDateTime + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.SA2Time + "</td>";
                        tbody += "<td style='width:10%'>" + val.ApprovedDate + "</td>";
                        tbody += "<td style='width:12%'>" + val.ReportDateTime + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.TatHrs + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.DelayHrs + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.OnTime + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTATDetails tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SelectRowValue1(elem) {
    ipoptype = $('#ddlIpOpType option:selected').text();
    subcatid = $(elem).closest('tr').find('td:eq(0)').text();
    GroupTypeName = $(elem).closest('tr').find('td:eq(6)').text();
    $('#TATReportDelayedDetails').modal('show');
    GetDataTATReportDelayedDetails()
}
function GetDataTATReportDelayedDetails() {
    $('#tblTATDelayedDetails tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    if (GroupTypeName == "Local Lab Test") {
        objBO.Prm1 = '1';
    }
    if (GroupTypeName == "Out Source Lab") {
        objBO.Prm1 = '0';
    }
    objBO.LabCode = '-';
    objBO.IpOpType = ipoptype;
    objBO.SubCat = subcatid;
    objBO.ReportStatus = $('#ddlReportType option:selected').val();
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.Logic = "Delayed:Report";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.DelayHrs != '0') {
                            tbody += "<tr style='background:#ffc0cb;'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td>" + val.IpOpType + "</td>";
                        tbody += "<td style='width:9%'>" + val.ipop_no + "</td>";
                        tbody += "<td style='width:10%'>" + val.testCategory + "</td>";
                        tbody += "<td style='width:10%'>" + val.TestName + "</td>";
                        tbody += "<td style='width:12%'>" + val.RegDate + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.RS2Time + "</td>";
                        tbody += "<td style='width:12%'>" + val.SampleOrInDateTime + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.SA2Time + "</td>";
                        tbody += "<td style='width:10%'>" + val.ApprovedDate + "</td>";
                        tbody += "<td style='width:12%'>" + val.ReportDateTime + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.TatHrs + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.DelayHrs + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.OnTime + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTATDelayedDetails tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ExeclTATReport()
{
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.SubCat = $('#ddlSubCatList option:selected').val();
    objBO.ReportStatus = $('#ddlReportType option:selected').val();
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.Prm1 = $('#ddlCategory option:selected').val();
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.OutPutType ="Excel";
    objBO.Logic = "TAT:ReportAllPatient";
    Global_DownloadExcel(url, objBO, "TATReport" + ".xlsx");
}
function TotalCal() {
    var tcount = 0;
    var count = 0;
    var testamount = 0;
    var totalOnTime = 0;
    var totalOnTimePerc = 0;
    var totalDelayed = 0;
    var totalDelayedPerc = 0;

    $('#tblReport tbody tr').each(function () {
        if ($(this).attr('class') == 'pt') {
            tcount++;
            testamount += parseInt($(this).find('td:eq(2)').text());
            totalOnTime += parseInt($(this).find('td:eq(3)').text());
            totalDelayed += parseInt($(this).find('td:eq(4)').text());
            totalOnTimePerc = ((totalOnTime / testamount) * 100).toFixed(2);
            totalDelayedPerc = ((totalDelayed / testamount) * 100).toFixed(2);
            if (count == $('#tblReport tbody tr.pr').length)
            $('#tblReport tbody tr.pr:last').find('td:eq(1)').find('label').text(testamount);
            $('#tblReport tbody tr.pr:last').find('td:eq(2)').find('label').text(totalOnTime + " [" + totalOnTimePerc + "%]");
            $('#tblReport tbody tr.pr:last').find('td:eq(3)').find('label').text(totalDelayed + " [" + totalDelayedPerc + "%]");
        }

        if ($(this).attr('class') == 'pr') {
            count++;
            if (count > 1 && count <= $('#tblReport tbody tr.pr').length) {
                $('#tblReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(1)').find('label').text(testamount);
                $('#tblReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(2)').find('label').text(totalOnTime + " [" + totalOnTimePerc +"%]");
                $('#tblReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(3)').find('label').text(totalDelayed + " [" + totalDelayedPerc +"%]");
                testamount = 0; totalOnTime = 0; totalOnTimePerc = 0; totalDelayed = 0; totalDelayedPerc = 0;
            }
            else {
                $('#tblReport tbody tr.pr:last').find('td:eq(1)').find('label').text(testamount);
                $('#tblReport tbody tr.pr:last').find('td:eq(2)').find('label').text(totalOnTime + " [" + totalOnTimePerc + "%]");
                $('#tblReport tbody tr.pr:last').find('td:eq(3)').find('label').text(totalDelayed + " [" + totalDelayedPerc + "%]");
            }
        }
    });


}