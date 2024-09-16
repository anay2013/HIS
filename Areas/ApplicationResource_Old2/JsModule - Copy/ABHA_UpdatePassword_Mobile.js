var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('Authentication')
})
function GetOTP(elem) {
    if ($('#txtAbhaAddress').val() == '') {
        alert('Please Provide Abha Address');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_UpdatePasswordMobile_GetOTP";
    var objBO = {};
    objBO.abhaAddress = $('#txtAbhaAddress').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
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
    if ($('#txtOTP').val().length != 6) {
        alert('Please Provide 6 digit OTP');
        return
    }
    if ($('#txtNewPassword').val() == '') {
        alert('Please Provide New Password');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_UpdatePasswordMobile_VerifyOTP";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.otp = $('#txtOTP').val();
    objBO.newPassword = $('#txtNewPassword').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
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
