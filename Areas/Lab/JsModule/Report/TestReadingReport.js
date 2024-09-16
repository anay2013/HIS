
$(document).ready(function () {
    CloseSidebar();
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');
    FillCurrentDate('txtfromdate2');
    FillCurrentDate('txttodate2');
    GetCategory();
    GetObservation();
});
function GetCategory() {
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = "-";
    objBO.IpOpType = "-";
    objBO.SubCat = "-";
    objBO.ReportStatus = "-";
    objBO.VisitNo = "-";
    objBO.BarccodeNo = "-";
    objBO.TestCategory = "-";
    objBO.Prm1 = "-";
    objBO.Prm2 = "-";
    objBO.AutoTestId = "-";
    objBO.TestCode = "-";
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txttodate").val();
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
                    $("#ddlCategory").append($("<option></option>").val("ALL").html("ALL")).select2();
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlCategory").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
                    });
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
function GetObservation() {
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = "-";
    objBO.IpOpType = "-";
    objBO.SubCat = "-";
    objBO.ReportStatus = "-";
    objBO.VisitNo = "-";
    objBO.BarccodeNo = "-";
    objBO.TestCategory = "-";
    objBO.Prm1 = "-";
    objBO.Prm2 = "-";
    objBO.AutoTestId = "-";
    objBO.TestCode = "-";
    objBO.from = $("#txtfromdate2").val();
    objBO.to = $("#txttodate2").val();
    objBO.Logic = "GetObservation";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $("#ddlObservation").append($("<option></option>").val("ALL").html("ALL")).select2();
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlObservation").append($("<option></option>").val(value.ObservationId).html(value.ObservationName));
                    });
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
function GetAllTestCount(logic) {
    $("#tblReportTestCount tbody").empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = "-";
    objBO.IpOpType = "-";
    objBO.SubCat = $("#ddlCategory option:selected").val();
    objBO.ReportStatus = "-";
    objBO.VisitNo = "-";
    objBO.BarccodeNo = "-";
    objBO.TestCategory = "-";
    objBO.Prm1 = "-";
    objBO.Prm2 = "-";
    objBO.AutoTestId = "-";
    objBO.TestCode = "-";
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txttodate").val();
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = ''; var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) { 
                        count++;
                        tbody += "<tr>";                  
                        tbody += "<td style='width:5%;'>" + count + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td style='width:20%;text-align:center'><input value='" + val.totalCount + "' style='width:30%;height:20px; text-align:center; border-radius:50px;border: none;background: orange;'/></td>";
                        tbody += "</tr>";
                    });
                    $('#tblReportTestCount tbody').append(tbody);
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
//function GetAllObservation(logic) {
//    $("#tblReport tbody").empty();
//    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
//    var objBO = {};
//    objBO.LabCode = "-";
//    objBO.IpOpType = "-";
//    objBO.SubCat = '-';
//    objBO.ReportStatus = "-";
//    objBO.VisitNo = "-";
//    objBO.BarccodeNo = "-";
//    objBO.TestCategory = "-";
//    objBO.Prm1 = $("#ddlObservation option:selected").val();
//    objBO.Prm2 = "-";
//    objBO.AutoTestId = "-";
//    objBO.TestCode = "-";
//    objBO.from = $("#txtfromdate2").val();
//    objBO.to = $("#txttodate2").val();
//    objBO.Logic = logic;
//    $.ajax({
//        method: "POST",
//        url: url,
//        data: JSON.stringify(objBO),
//        dataType: "json",
//        contentType: "application/json;charset=utf-8",
//        success: function (data) {
//            var tbody = ''; var temp = "";
//            if (Object.keys(data.ResultSet).length > 0) {
//                if (Object.keys(data.ResultSet.Table).length > 0) {
//                    $.each(data.ResultSet.Table, function (key, val) {
//                        if (temp != val.patient_name) {
//                            tbody += "<tr style='background:#0d8bc747;'>";
//                            tbody += "<td colspan='8' style='font-size:13px;'><b>" + val.patient_name + "</b></td>";
//                            tbody += "</tr>";
//                            temp = val.patient_name
//                        }
//                        else {

//                        }
//                       // tbody += "<tr>";
//                        tbody += "<td>" + val.ItemName + "</td>";
//                        tbody += "<td style='width:10%;text-align:center'>" + val.mac_reading+ "</td>";
//                        tbody += "<td style='width:10%;text-align:center'>" + val.min_value + "</td>";
//                        tbody += "<td style='width:10%;text-align:center'>" + val.max_value+ "</td>";
//                        tbody += "<td style='width:10%;text-align:center'>" + val.nr_range + "</td>";
//                        tbody += "<td style='width:10%;text-align:center'>" + val.result_unit+ "</td>";
//                        tbody += "<td style='width:10%;text-align:center'>" + val.method_name + "</td>";
//                        tbody += "</tr>";
//                    });
//                    $('#tblReport tbody').append(tbody);
//                }
//            }
//            else {
//                alert('No Data Found')
//            }
//        },
//        error: function (response) {
//            alert('Server Error...!');
//        }
//    });
//}
function GetExecls(logic) {
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    if (logic == "GetTestCountByCategory") {
        objBO.from = $("#txtfromdate").val();
        objBO.to = $("#txttodate").val();
    }
    else if (logic =="GetReadingByObs") {
        objBO.from = $("#txtfromdate2").val();
        objBO.to = $("#txttodate2").val();
    }
    objBO.LabCode = "-";
    objBO.IpOpType = "-";
    objBO.SubCat = $("#ddlCategory option:selected").val();
    objBO.ReportStatus = "-";
    objBO.VisitNo = "-";
    objBO.BarccodeNo = "-";
    objBO.TestCategory = "-";
    objBO.Prm1 = $("#ddlObservation option:selected").val();
    objBO.Prm2 = "-";
    objBO.AutoTestId = "-";
    objBO.TestCode = "-";
    objBO.Logic = logic;
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "LabReports" + ".xlsx");
}