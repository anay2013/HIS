$(document).ready(function () {
    CloseSidebar();
    FloorAndPanelList();
    GetDoctorList();
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
});
function FloorAndPanelList() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'FloorAndPanelList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlFloor').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlFloor').append($('<option></option>').val(val.FloorName).html(val.FloorName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlPanel').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlWard').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlWard').append($('<option></option>').val(val.RoomTypeId).html(val.RoomTypeName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctorList() {
    $('#ddlDoctor').empty().select2();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = Active.doctorId;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetDoctorForConsult';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName)).change();
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AdmittedPatientAnalysis() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = $('#ddlIpdStatus option:selected').val();
    objBO.Prm2 = $('#ddlDoctor option:selected').val();
    objBO.Prm3 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'IpdPatientList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td class='hide'>" + val.IsDischSummaryLocked + "</td>";
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
function ExcelPatientAnalysis() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = $('#ddlIpdStatus option:selected').val();
    objBO.Prm2 = $('#ddlDoctor option:selected').val();
    objBO.Prm3 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'IpdPatientList';
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "PatientAnalysis" + ".xlsx");
}