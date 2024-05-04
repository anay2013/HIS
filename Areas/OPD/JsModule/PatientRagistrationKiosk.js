var _OTP = null;
$(document).ready(function () {
    GetState();
})
function GetState() {
    $('#ddlState').empty().append($('<option value="Select">Select</option>'));
    var url = config.baseUrl + "/api/Appointment/PatientMasterKioskQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.mobile_no = '-';
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.Logic = 'GetState';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlState').append($('<option></option>').val(val.state_code).html(val.state_name));
                    });
                }
            }
        },
        complete: function (data) {
            $('#ddlState').val(32);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetCityByState() {
    $('#ddlCity').empty().append($('<option value="Select">Select</option>'));
    var url = config.baseUrl + "/api/Appointment/PatientMasterKioskQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.mobile_no = '-';
    objBO.prm_1 = $('#ddlState option:selected').val();
    objBO.prm_2 = '-';
    objBO.Logic = 'GetCityByState';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlCity').append($('<option></option>').val(val.dist_code).html(val.distt_name));
                    });
                }
            }
        },
        complete: function (data) {
            if ($('#ddlState option:selected').val() == 32)
                $('#ddlCity').val(45);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function PatientInfo() {
    $('#tblPatientInfo tbody').empty();
    var url = config.baseUrl + "/api/Appointment/PatientMasterKioskQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.mobile_no = $('#txtMobileNo').val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.Logic = 'PatientInfoByMobileNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.gender + "</td>";
                        tbody += "<td>" + val.dob + "</td>";
                        tbody += "<td>" + val.address + "</td>";
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
function Insert() {
    if (validate()) {
        var url = config.baseUrl + "/api/Appointment/PatientMaster_InsertKiosk";
        var objBO = {};
        objBO.hosp_id = '-';
        objBO.UHID = '-';
        objBO.Title = '-';
        objBO.FirstName = $('#txtFirstName').val();
        objBO.LastName = $('#txtLastName').val();
        objBO.patient_name = $('#txtFirstName').val();
        objBO.gender = $('#ddlGender option:selected').text();
        objBO.dob = $('#txtDOB').val();
        objBO.mobile_no = $('#txtMobileNo').val();
        objBO.email_id = $('#txtEmail').val();
        objBO.state = $('#ddlState option:selected').text();
        objBO.district = $('#ddlCity option:selected').text();
        objBO.address = $('#txtAddress').val();
        objBO.prm_1 = '-';
        objBO.prm_2 = '-';
        objBO.Logic = 'Insert';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            async: false,
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                    Clear();
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function validate() {
    var mobile_no = $('#txtMobileNo').val();
    var FirstName = $('#txtFirstName').val();
    var gender = $('#ddlGender option:selected').text();
    var dob = $('#txtDOB').val();
    var state = $('#ddlState option:selected').text();
    var district = $('#ddlCity option:selected').text();
    var address = $('#txtAddress').val();

    if (mobile_no == '') {
        alert('Please Provide Mobile No.')
        return false
    }
    if (FirstName == '') {
        alert('Please Provide First Name.')
        return false
    } if (gender == 'Select') {
        alert('Please Select Gender.')
        return false
    }
    if (dob == '') {
        alert('Please Select DOB.')
        return false

    }
    if (state == 'Select') {
        alert('Please Select State.')
        return false
    }
    if (district == 'Select') {
        alert('Please Select District.')
        return false
    }
    if (address == '') {
        alert('Please Provide Address.')
        return false
    }
    return true
}
function Clear() {
    $('input').val('');
    $('#txtMobileNo').prop('disabled', false);
    $('.info').addClass('lock');
}
function SendOTP() {
    var url = config.baseUrl + "/api/Utility/SentOtp";
    var objBO = {};
    objBO.Mobile = $('#txtMobileNo').val();
    objBO.TemplateId = 'TemplateId';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.length == 4) {
                _OTP = data;
                alert('OTP Sent Successfully.')
                $('#txtMobileNo').prop('disabled', true);
                $('#txtOTP').focus();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AuthenticateOTP() {
    var val = $('#txtOTP').val();
    if (val == '') {
        alert('Please Provide OTP');
        $('#txtOTP').focus();
        return;
    }
    if (val != _OTP) {
        alert('Incorrect OTP.');
        $('#txtOTP').val('');

        return;
    }
    if (val == _OTP) {
        _OTP = null;
        $('#txtOTP').val('');
        $('.info').removeClass('lock');
        PatientInfo();
    }
}