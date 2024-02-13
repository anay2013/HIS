var _DispatchNo = "";
$(document).ready(function () {
    GetDispatchLabInfo();
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
   
});



function GetDispatchLabInfo() {
    var url = config.baseUrl + "/api/Lab/LabOutSourceQueries";
    var objBO = {};
    objBO.subcatId = $('#ddlCategory option:selected').val();
    objBO.prm1 = "Y";
    objBO.logic = "GetTestBySubCategory";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    //$("#ddlCategory").append($("<option></option>").val("0").html("Select"));
                    $.each(data.ResultSet.Table1, function (key, value) {
                        $("#ddlCategory").append($("<option></option>").val(value.LabCode).html(value.LabName)).select2();
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
function GetDispatchInfoByDate() {
    var url = config.baseUrl + "/api/Lab/SampleLabReceivingQueries";
    var objBO = {}
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.DispatchLabCode = $('#ddlCategory option:selected').val();
    objBO.Logic = "GetDispatchInfoToReceiveByDate";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblDispatch tbody').empty();
            var tbody = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.LabCode + "</td>";                        
                        tbody += "<td>" + val.DispatchNo + "</td>";
                        tbody += "<td>" + val.dispatch_date + "</td>";
                        tbody += "<td>" + val.Status + "</td>";
                        tbody += "<td class='text-right'>" + val.Nos + "</td>";
                        tbody += "<td><button class='btn-warning' onclick=GetPatientDispatch('" + val.DispatchNo + "')>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDispatch tbody').append(tbody);
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
function GetDispatchInfoToReceive() {
    var url = config.baseUrl + "/api/Lab/SampleLabReceivingQueries";
    var objBO = {}
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#ddlCategory option:selected').val();
    objBO.Logic = "GetDispatchInfoToReceive";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblDispatch tbody').empty();
            var tbody = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;                 
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.LabCode + "</td>";                        
                        tbody += "<td>" + val.DispatchNo + "</td>";
                        tbody += "<td>" + val.dispatch_date + "</td>";
                        tbody += "<td>" + val.Status + "</td>";
                        tbody += "<td class='text-right'>" + val.Nos + "</td>";
                        tbody += "<td><button class='btn-warning' onclick=GetPatientDispatch('" + val.DispatchNo + "')>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDispatch tbody').append(tbody);
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
function GetPatientDispatch(DispatchNo) {  
    _DispatchNo = DispatchNo;
    var url = config.baseUrl + "/api/Lab/SampleLabReceivingQueries";
    var objBO = {}
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.DispatchNo = _DispatchNo;
    objBO.Logic = "GetPatientInDispatch";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblPatientDetail tbody').empty();
            var tbody = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (val.PC==0)
                            tbody += "<tr style='background-color:lightGreen'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td style='width:5%;'>" + count + "</td>";
                        tbody += "<td style='width:20%;'>" + val.VisitNo + "</td>";
                        tbody += "<td style='width:10%;'>" + val.barcodeNo + "</td>";
                        tbody += "<td style='width:20%;'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:35%;'>" + val.TestDeetail + "</td>";
                        tbody += "<td style='width:5%;'>" + val.TC + "</td>";
                        tbody += "<td style='width:5%;'>" + val.PC + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientDetail tbody').append(tbody);
                  
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
function PushHospitalDataToLIS() {

    var VisitNos = "";
    $('#tblPatientDetail tbody tr').each(function () {
        if ($(this).find('td:eq(6)').text()!="0")
        VisitNos = VisitNos + $(this).find('td:eq(1)').text()+"|";
    });
    if (VisitNos.length < 5) {
        alert("No Pendency Found all Sent");
        return;
    }


    var url = config.baseUrl + "/api/Lab/PushHospitalDataToLIS";
    var objBO = {}

    objBO.DispatchNo = _DispatchNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.VisitNo = VisitNos;
    objBO.Logic = "GetOutSourceRecord";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data);
            GetPatientDispatch(_DispatchNo);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
