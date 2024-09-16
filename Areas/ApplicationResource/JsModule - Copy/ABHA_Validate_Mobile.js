var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('Authentication') 
})
function GetOTP(elem) {
    if ($('#txtABHAAddress').val() == '') {
        alert('Please Provide ABHA Address');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_ValidateMobile_GetOTP";
    var objBO = {};
    objBO.abhaAddress = $('#txtABHAAddress').val();
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
    var url = config.baseUrl + "/api/M1/ABHA_ValidateMobile_VerifyOTP";
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
                alert('Something is wrong.')
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