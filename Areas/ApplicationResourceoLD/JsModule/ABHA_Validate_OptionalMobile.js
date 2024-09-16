var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('Authentication')
})
function GetOTP(elem) {
    if ($('#txtMobileNo').val().length !=10) {
        alert('Please Provide 10 digit Mobile No.');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_ValidateOptionalMobile_GetOTP";
    var objBO = {};
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
        alert('Please Provide Correct OTP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_ValidateOptionalMobile_VerifyOTP";
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
                _requestId = data.requestId;
                $('#responseMsg').html('<b>Response : </b>: ' + data.message).show();
                Clear();
            }
            else {
                alert('OTP Not Matched.')
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
function select(elem) {
    if ($('#txtHealthIdNumber').val()=='') {
        alert('Please Provide Health Id Number');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_ValidateOptionalMobile_select";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.healthIdNumber = $('#txtHealthIdNumber').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                _requestId = data.requestId;
                $('#responseMsg').html('<b>Response : </b>: ' + data.message).show();               
            }
            else {
                alert('OTP Not Matched.')
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
function Clear() {
    $('input:text').val('');
}