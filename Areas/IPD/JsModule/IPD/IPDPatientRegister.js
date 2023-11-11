$(document).ready(function () {
    FloorAndPanelList();
    $('select').select2();
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    $('#txtSearchFrom,#txtSearchTo').prop('disabled', true);
    $('#ddlActive').on('change', function () {
        var val = $(this).find('option:selected').text();
        if (val == 'Active')
            $('#txtSearchFrom,#txtSearchTo').prop('disabled', true);
        else
            $('#txtSearchFrom,#txtSearchTo').prop('disabled', false);
    });
    $('#txtSearchFrom,#txtSearchTo').prop('disabled', true);
    $('#btnPatient').on('click', function () {
        var logic = "PatientRegister:InPatient";
        if ($('#ddlActive option:selected').text() == 'Active')
            logic = "PatientRegister:InPatient";
        else
            logic = "PatientRegister:AllPatient";

        PatientRegister(logic);
    });
});

function patientDetails(ipdNo) {
    $('#tblPatientInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = ipdNo;
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PatientRegister:ByIPDNo';
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
                        tbody += "<td>" + val.AdmissionType + "</td>";
                        tbody += "<td>" + val.RoomType + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td>" + val.MLCType + "</td>";
                        tbody += "<td>" + val.RefName + "</td>";
                        tbody += "<td>" + val.PatientType + "</td>";
                        tbody += "<td>" + val.PolicyNo + "</td>";
                        tbody += "<td>" + val.SourceName + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.DischargeBy + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientInfo tbody').append(tbody);
                    $('#modalPatientInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
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
                    $('#ddlFloor').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlFloor').append($('<option></option>').val(val.FloorName).html(val.FloorName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlPanel').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientRegister(logic) {
    $('#tblPatientRegister tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = $('#ddlActive option:selected').val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
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
                        if (val.IpdStatus == 'Out')
                            tbody += "<tr style='background:#ffb9b9'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td><button onclick=patientDetails('" + val.IPDNo + "') class='btn btn-warning btn-xs'><i class='fa fa-user'></i></button></td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.RoomName + "</td>";
                        //tbody += "<td>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td>" + val.RoomTypeRequest + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.Attendant + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td><a class='btn btn-warning btn-xs' href='../Print/AdmissionAndDischargeReport?_IPDNo=" + val.IPDNo + "' target='_blank'>Print</a></td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientRegister tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
