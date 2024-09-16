var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('ConsentCollection')  
    $('#AbhaSuggesionList').on('click', 'li', function () {
        var id = $(this).text();
        $('#txtHealthIdNumber').val(id);
    })
    ClientAuthorization();
    $('ul.steps li').eq(0).addClass('activeTab');
})
function ClientAuthorization() {
    var url = config.baseUrl + "/api/M1/ClientAuthorization";
    var objBO = {};
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetOTP(elem) {
    if ($('#txtAadhar').val() == '') {
        alert('Please Provide Aadhar');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP1_ABHANoCreate_GetOTP";
    var objBO = {};
    objBO.aadhaar = $('#txtAadhar').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data != null) {
                _requestId = data.requestId;
                $('#responseMsg').html('<b>Response : </b>' + data.message).show();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
        }
    });
}
function VerifyOTP(elem) {
    if ($('#txtOTP').val() == '') {
        alert('Please Provide OTP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP2_ABHANoCreate_VerifyOTP";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.otp = $('#txtOTP').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                $('#responseMsg').html('<b>Response : </b>' + data.message).show();
                NextStep('CheckExistance');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
        }
    });
}
function CheckExistance(elem) {
    if ($('#txtMobileNo').val() == '') {
        alert('Please Provide Mobile No');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP3_ABHANoCreate_CheckExistance";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.mobile = $('#txtMobileNo').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data != null) {
                var msg = (data.message == 'True') ? 'Already Exist' : 'OTP Send To Mobile No.';
                if (data.message != 'True') {
                    $('#txtCheckOTP').removeAttr('disabled');
                    $('#txtCheckOTP').focus();
                } else {
                    $('#txtCheckOTP').attr('disabled', true);
                }
                $('#responseMsg').html('<b>Response : </b>' + msg).show();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
        }
    });
}
function CreateIfNotExist_VerifyOTP(elem) {
    if ($('#txtCheckOTP').val() == '') {
        alert('Please Provide OTP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP4_ABHANoCreate_CreateIfNotExist";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.otp = $('#txtCheckOTP').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                _healthIdNumber = data.healthIdNumber;
                $('#responseMsg').html('<b>Response : </b>' + data.message).show();
                NextStep('CreateAbha');
                GetSuggestion();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
        }
    });
}
function createAbhaAadhaarDetail(elem) {
    if ($('#txtHealthIdNumber').val() == '') {
        alert('Please Provide Health Id Number');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/createAbhaAadhaarDetail";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.abhaAddress = $('#txtHealthIdNumber').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {              
                getProfile(objBO.abhaAddress )
                $('#responseMsg').html('<b>Response : </b>Successfully Created ABHA Id : ' + $('#txtHealthIdNumber').val()).show();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
            $('#txtHealthIdNumber').val('')
            $('#AbhaSuggesionList').empty();
        }
    });
}
function GetSuggestion() {
    var url = config.baseUrl + "/api/M1/createSuggestion";
    var objBO = {};
    objBO.requestId = _requestId;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    $('#AbhaSuggesionList').append("<li>" + data[i] + "</li>");
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
        }
    });
}
function getProfile(abhaAddress) {
    $('.ProfileLoading').show();
    var url = config.baseUrl + "/api/M1/ABHA_GetProfileByAbhaId";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.abhaAddress = abhaAddress;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                var firstName = data.firstName;
                var middleName = data.middleName;
                var lastName = data.lastName;
                var name = data.name;
                var yearOfBirth = data.yearOfBirth;
                var dayOfBirth = data.dayOfBirth;
                var monthOfBirth = data.monthOfBirth;
                var dob = dayOfBirth + '-' + monthOfBirth + '-' + yearOfBirth;
                var gender = (data.gender == 'M') ? 'Male' : 'Female';
                var mobile = data.mobile;
                var healthIdNumber = data.healthIdNumber;
                var healthId = data.healthId;
                var email = data.email;
                var profilePhoto = 'data:image/png;base64,' + data.profilePhoto;
                var pincode = data.pincode;
                var address = data.address;
                $('#prof_photo').attr('src', profilePhoto);
                $('#prof_name').text(name);
                $('#prof_abhanumber').text(healthIdNumber);
                $('#prof_abhaaddress').text(healthId);
                $('#prof_gender').text(gender);
                $('#prof_dob').text(dob);
                $('#prof_mobile').text(mobile);
            } else {
                alert('Not Found.')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $('.ProfileLoading').hide();
        }
    });
}