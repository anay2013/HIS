var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('Authentication')
})
function GetOTP(elem) {
    if ($('#txtAadhar').val() == '') {
        alert('Please Provide Aadhar');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP1_ABHADeleteAadhar_GetOTP";
    var objBO = {};
    objBO.token = _requestId;
    objBO.reason = $('#txtReason').val();
    objBO.aadhaar = $('#txtAadharNo').val();
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
    if ($('#txtOTP').val() == '') {
        alert('Please Provide OTP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP2_ABHADeleteAadhar_VerifyOTP";
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
