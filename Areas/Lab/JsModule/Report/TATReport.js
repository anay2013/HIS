var ipoptype = "";
var subcatid = "";

$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    LoadTestCategory();
});

function LoadTestCategory() {
    $("#ddlTest").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
    $("#ddlDepartment").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
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
                        $("#ddlDepartment").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
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
    var depId = $("#ddlDepartment").val();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = '-';
    objBO.SubCat = depId;
    objBO.ReportStatus = '-';
    objBO.TestCode = '-';
    objBO.Prm1 = '-';
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
    objBO.IpOpType = '-';
    objBO.SubCat = $('#ddlDepartment option:selected').val();
    objBO.ReportStatus = '-';
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.Prm1 = '-';
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
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width:10%'>" + val.IPOPType + "</td>";
                        tbody += "<td hidden>" + val.SubCatID + "</td>";
                        tbody += "<td style='width:36%'>" + val.testCategory + "</td>";
                        tbody += "<td style='text-align:center;width:12%'>" + val.NoTest + "</td>";
                        tbody += "<td style='text-align:center;width:15%'>" + val.OnTime + " &nbsp;&nbsp;[" + val.OnTimePerc + "%]" + "</td>";
                        tbody += "<td style='text-align:center;width:15%;'>" + val.Delayed + " &nbsp;&nbsp;[" + val.DelayedPerc + "%]" + "</td>";
                        tbody += "<td style='width:7%;text-align:center'><input type='text' style='border-radius:4px;width:60px;background-color:#d3d0cf;border:none; height:20px;color:red;text-align:center;' value='" + val.Delayed_MaxGap + "'></td>";
                        tbody += "<td style='width:7%;text-align:center'><button class='btn-success' onclick='SelectRowValue(this)' id='btnAdd' style='border:none;height:20px;margin-bottom:3px;text-align:center;'>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SelectRowValue(elem) {
    ipoptype = $(elem).closest('tr').find('td:eq(0)').text();
    subcatid = $(elem).closest('tr').find('td:eq(1)').text();
    $('#TATReportDetails').modal('show');
    GetDataTATReportDetails()
}
function GetDataTATReportDetails() {
    debugger
    $('#tblTATDetailsDetails tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_TATQueries";
    var objBO = {};
    objBO.LabCode = '-';
    objBO.IpOpType = ipoptype;
    objBO.SubCat = subcatid;
    objBO.ReportStatus = '-';
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.Prm1 = '-';
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

                        tbody += "<td style='width:7%'>" + val.IPOPType + "</td>";
                        tbody += "<td style='width:9%'>" + val.ipop_no + "</td>";
                        //tbody += "<td style='width:9%'>" + val.VisitNo + "</td>";
                        tbody += "<td style='width:10%'>" + val.testCategory + "</td>";
                        tbody += "<td style='width:10%'>" + val.TestName + "</td>";
                        tbody += "<td style='width:12%'>" + val.RegDate + "</td>";
                        tbody += "<td style='width:12%'>" + val.SampleOrInDateTime + "</td>";
                        tbody += "<td style='width:12%'>" + val.ReportDateTime + "</td>";
                        tbody += "<td style='width:12%'>" + val.ApprovedDate + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.TatHrs + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.DelayHrs + "</td>";
                        tbody += "<td style='width:5%;text-align:center'>" + val.OnTime + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTATDetailsDetails tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}