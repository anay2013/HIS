var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
var timerOn = false;
$(document).ready(function () {
    $('.btnResend').addClass('block');
    NextStep('Mobile')
    //demo();
})

function demo() {
    NextStep('abhaInfo')
    var healthIds = [
        {
            "healthId": "nitinkumar_351993@sbx",
            "healthIdNumber": "91-3772-0586-4665",
            "name": "Umesh Chandra",
            "gender": null,
            "dateOfBirth": null
        },
        {
            "healthId": "rishabh20061996@sbx",
            "healthIdNumber": "91-3887-4360-8415",
            "name": "Rishabh Kumar",
            "gender": null,
            "dateOfBirth": null
        },
        {
            "healthId": "ajeet.20021983@sbx",
            "healthIdNumber": "91-5346-3454-4785",
            "name": "Ajeet Kumar Maurya",
            "gender": null,
            "dateOfBirth": null
        },
        {
            "healthId": "nitinkumar_351993@sbx",
            "healthIdNumber": "91-3772-0586-4665",
            "name": "Umesh Chandra",
            "gender": null,
            "dateOfBirth": null
        },
        {
            "healthId": "rishabh20061996@sbx",
            "healthIdNumber": "91-3887-4360-8415",
            "name": "Rishabh Kumar",
            "gender": null,
            "dateOfBirth": null
        },
        {
            "healthId": "ajeet.20021983@sbx",
            "healthIdNumber": "91-5346-3454-4785",
            "name": "Ajeet Kumar Maurya",
            "gender": null,
            "dateOfBirth": null
        }
    ]
    var res = healthIds;
    var html = "";
    for (var i = 0; i < res.length; i++) {
        html += "<div class='col-md-3 abhainfo'>";
        html += "<img src='https://abha.abdm.gov.in/abha/v3/4e37ef4b02bb115400ffd6020ddac10d.svg'/>";
        html += "<b>" + res[i].name + "</b><br />";
        html += "<span class='text-success'><i class='fa fa-check-circle-o'>&nbsp;</i>Verified</span><br />";
        html += "ABHA Number<br />";
        html += "<b>" + res[i].healthIdNumber + "</b><br />";
        html += "ABHA Address<br />";
        html += "<b>" + res[i].healthId + "</b>";
        html += "<button onclick=LoginABHA('" + res[i].healthId + "') class='btn btn-warning btn-block'>View Profile</button>";
        html += "</div>";
    }
    $('.abhaList').append(html);
}
function timer(remaining) {
    var m = Math.floor(remaining / 60);
    var s = remaining % 60;

    m = m < 10 ? '0' + m : m;
    s = s < 10 ? '0' + s : s;
    $('.timer').html(m + ':' + s);
    remaining -= 1;

    if (remaining >= 0 && timerOn) {
        var timeout = setTimeout(function () {
            timer(remaining);
        }, 1000);
        return;
    }

    if (!timerOn) {
        timerOn = false
        clearTimeout(timeout)
        // Do validate stuff here
        return;
    }

    // Do timeout stuff here
    alert('Timeout for otp');
    timerOn = false
    $('.btnResend').removeClass('block');
    $('.timer').html('00:00');
    clearTimeout(timeout)
}
function GetOTP(elem) {
    if ($('#txtValidate_MobileNo').val() == '') {
        alert('Please Provide Mobile No');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ValidateByMobile";
    var objBO = {};
    objBO.mobile = $('#txtValidate_MobileNo').val();
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
                //$('#responseMsg').html('<b>Response : </b>' + data.message).show();
                NextStep('AuthMobileOTP');
                $('#AuthMobileOTP .msg').text(data.message);
                $('.otptext input:first').focus();
                $('.btnResend').addClass('block');
                timerOn = true;
                timer(90);
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
function CheckOTPDigit(elem) {
    if ($(elem).val().length == 1) {
        if ($(elem).attr('id') != 'txtOTP6')
            $(elem).next('input').focus();
    }
}
function VerifyOTP(elem) {
    $('.abhaList').empty();
    if (!timerOn) {
        alert('OTP Expired ! Pls send again')
        return
    }
    var otp = $('.otptext input').map(function () {
        return $(this).val()
    }).get()
    if (otp.join('').length != 6) {
        alert('Please Provide Valid OTP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ValidateByMobileOTP";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.otp = otp.join('');
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data != null) {
                var html = "";
                _requestId = data.requestId;               
                var res = data.healthIds;
                for (var i = 0; i < res.length; i++) {
                    html += "<div class='col-md-3 abhainfo'>";
                    html += "<img src='https://abha.abdm.gov.in/abha/v3/4e37ef4b02bb115400ffd6020ddac10d.svg'/>";
                    html += "<b>" + res[i].name + "</b><br />";
                    html += "<span class='text-success'><i class='fa fa-check-circle-o'>&nbsp;</i>Verified</span><br />";
                    html += "ABHA Number<br />";
                    html += "<b>" + res[i].healthIdNumber + "</b><br />";
                    html += "ABHA Address<br />";
                    html += "<b>" + res[i].healthId + "</b>";
                    html += "<button onclick=LoginABHA('" + res[i].healthId+"') class='btn btn-warning btn-block'>View Profile</button>";
                    html += "</div>";
                }
                $('.abhaList').append(html);
                NextStep('abhaInfo');
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
function LoginABHA(_healthIdNumber) {
    sessionStorage.setItem('abhaAddress', _healthIdNumber);
    window.location.href = config.rootUrl + "/Admin/ABHA_Dashboard?mid=SM446";
}
function Clear() {
    $('input:text').val('');
}