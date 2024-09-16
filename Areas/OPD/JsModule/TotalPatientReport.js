var _AppId = '';
var _doctorId = '';
$(document).ready(function () {
    CloseSidebar();
    FillCurrentDate('txtFromAppDate')
    FillCurrentDate('txtToAppDate')
    $('select').select2();
    GetDoctorList();

});
function GetDoctorList() {
    $('#ddlDoctor').empty().select2();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.DoctorId = Active.doctorId;
    objBO.DeptId = '-';
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
        complete: function () {
            $('#ddlDoctor').prop('selectedIndex', '0').trigger('change.select2');
            ViewConsultation('TotalPatientReport');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ViewConsultation(logic) {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentSearch";
    var objBO = {};
    objBO.SearchValue = '-';
    objBO.prm_1 = '-';
    objBO.DoctorId = $('#ddlDoctor option:selected').val();
    objBO.from = $('#txtFromAppDate').val();
    objBO.to = $('#txtToAppDate').val();
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = "";
            var total = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        total++;
                        if (val.apStatus == 'Done')
                            tbody += "<tr style='background:#c8ffc8'>";
                        else if (val.apStatus == 'Pending')
                            tbody += "<tr style='background:#f4f9a2'>";
                        else
                            tbody += "<tr>";
                        tbody += "<td>" + val.token_no + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.app_no + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.AppDate + "</td>";
                        tbody += "<td>" + val.apStatus + "</td>";
                        tbody += "<td style='display:none'>" + val.DoctorId + "</td>"; 
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);


                }
            }
            if (Object.keys(data.ResultSet).length > 1) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('.opdCount Total').html('Total : ' + val.Total);
                        $('.opdCount Consultant').html('Consulted : ' + val.Consultant);
                        $('.opdCount Pending').html('Pending : ' + val.Pending);
                    });

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function (response) {

        }
    });
}
