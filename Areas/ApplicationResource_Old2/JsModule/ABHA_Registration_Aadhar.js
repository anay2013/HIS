var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
var _captchResult = 0;
var timerOn = false;
$(document).ready(function () {
    $('.btnResend').addClass('block');
    NextStep('ConsentCollection')
    $('#AbhaSuggesionList').on('click', 'li', function () {
        var id = $(this).text();
        $('#txtHealthIdNumber').val(id);
    })   
    ClientAuthorization();
    $('ul.steps li').eq(0).addClass('activeTab');
    $('.abdmat').text('@abdm')
    $('#suggestList span').on('click', function () {
        $('#txtAAC_AbhaAddress').val($(this).text())
    })
    captcha();
})


function timer(remaining) {
    var m = Math.floor(remaining / 60);
    var s = remaining % 60;

    m = m < 10 ? '0' + m : m;
    s = s < 10 ? '0' + s : s;
    $('.timer').html(m + ':' + s);
    remaining -= 1;

    if (remaining >= 0 && timerOn) {
       var timeout= setTimeout(function () {
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
function captcha() {
    var a = Math.floor(Math.random() * 6);
    var b = Math.floor(Math.random() * 9) + 1;
    if (a > 3) {
        _captchResult = a + b;
        $('.captchaText').html(a + '+' + b + '=?')
    }
    else {
        _captchResult = a * b;
        $('.captchaText').html(a + 'x' + b + '=?')
    }
}
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
//Consent Collection --CC
function toggleShow() {
    if ($('.toggleShow').hasClass('fa-eye')) {
        $('.aadhartext input').prop('type', 'text');
        $('.toggleShow').switchClass('fa-eye', 'fa-eye-slash');
    }
    else {
        $('.aadhartext input').prop('type', 'password');
        $('.toggleShow').switchClass('fa-eye-slash', 'fa-eye');
    }
}
function CheckAadharDigit(elem) {
    if ($(elem).val().length == 4) {
        if ($(elem).attr('id') != 'txtAadhar3')
            $(elem).next('input').focus();

        $(elem).removeClass('validateAadhar');
    }
    else {
        $(elem).addClass('validateAadhar');
    }
}
function CC_GetOTP(elem) {
    var aadharNo = $('.aadhartext input').eq(0).val() + $('.aadhartext input').eq(1).val() + $('.aadhartext input').eq(2).val();
    if (aadharNo.length != 12) {
        alert('Please Provide Valid Aadhar');
        return
    }
    if (!$('input[name=acceptAadharTermsCond]').is(':checked')) {
        alert('Please Accept terms and condition.');
        return
    }
    if ($('#txtCaptchaText').val() == '') {
        alert('Please Provide Valid Captcha');
        return
    } else if (_captchResult != $('#txtCaptchaText').val()) {
        alert('Captcha is Incorrect.');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP1_ABHANoCreate_GetOTP";
    var objBO = {};
    objBO.aadhaar = aadharNo;
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
                NextStep('AadharAuthentication');
                $('#AadharAuthentication .msg').text(data.message);
                $('.otptext input:first').focus();
                 $('.btnResend').addClass('block');
                timerOn = true;
                timer(90);

            }
            else {
                alert('Incorrect Aadhar No or\n No Data Found.')
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
//Aadhar Authentication --ADA
function CheckOTPDigit(elem) {
    if ($(elem).val().length == 1) {
        if ($(elem).attr('id') != 'txtOTP6')
            $(elem).next('input').focus();
    }
}
function ADA_VerifyOTP(elem) {
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
    if ($('#txtADA_MobileNo').val() == '') {
        alert('Please Provide Mobile No');
        return
    } else if ($('#txtADA_MobileNo').val().length != 10) {
        alert('Please Provide 10 digit Mobile No');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP2_ABHANoCreate_VerifyOTP";
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
            if (data != null) {
                if (data.message == 'OTP Verified') {
                    NextStep('ComminicationDetails');
                    $('#txtCD_MobileNo').val($('#txtADA_MobileNo').val());
                    if (data.message == 'OTP Verified')
                        $('#ComminicationDetails .msg').eq(0).html('<i class="fa fa-check-square">&nbsp;</i>Aadhar authentication has been successfully completed.');
                    else
                        $('#ComminicationDetails .msg').eq(0).text(data.message);

                    $('.otptext input:first').focus();
                }
                else {
                    window.location.href = "ABHA_Dashboard?abhaAddress=" + data.message;
                }
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
//Comminication Details --CD
function CD_CheckDigit(elem) {
    if ($(elem).val().length == 1) {
        if ($(elem).attr('id') != 'txtCD_OTP6')
            $(elem).next('input').focus();
    }
}
function AadharAuth() {
    if (_step == 0)
        alert(1)
}
function VerifyCDMobile(elem) {
    if ($(elem).text() == 'Verify') {
        $('.mobileInputAuth input').prop('disabled', true);
        $(elem).text('Edit')
        CD_VerifyMobile(elem);
    } else if ($(elem).text() == 'Edit') {
        $('.cdVerifyMobileOTP').hide();
        $(elem).text('Verify')
        $('.mobileInputAuth input').prop('disabled', false);
    }
}
function CD_VerifyMobile(elem) {
    timerOn = false;
    if ($('#txtCD_MobileNo').val() == '') {
        alert('Please Provide Mobile No');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP3_ABHANoCreate_CheckExistance";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.mobile = $('#txtCD_MobileNo').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data != null) {
                var msg = (data.message == 'True') ? 'The mobile number provided by you is already linked to ABHA numbers. Please provide a different mobile number.' : 'We just sent an OTP on your mobile number ******' + objBO.mobile.substring(6, 10);
                if (data.message != 'True') {
                    $('.otptext_cd').find('#txtCD_OTP1').focus();
                    $('.cdVerifyMobileOTP').show();
                    $('#ComminicationDetails .msg').eq(1).text(msg);
                    timerOn = true;
                    timer(90);
                } else {
                    _healthIdNumber = data.healthIdNumber;
                    $('.AE_msg span').text(data.healthIdNumber.split('-').pop());
                    $('.cdVerifyMobileOTP').hide();
                    $('#modalABHAExist').modal('show')
                }
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
function ExistingABHAUserLogin() {
    sessionStorage.setItem('abhaAddress', _healthIdNumber);
    window.location.href = config.rootUrl + "/Admin/ABHA_Dashboard?mid=SM446";
}
function CD_VerifyOTP(elem) {
    if (!timerOn) {
        alert('OTP Expired ! Pls send again')
        return
    }
    var otp = $('.otptext_cd input').map(function () {
        return $(this).val()
    }).get()
    if (otp.join('').length != 6) {
        alert('Please Provide Valid OTP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP4_ABHANoCreate_CreateIfNotExist";
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
                _healthIdNumber = data.healthIdNumber;
                NextStep('ComminicationDetails_EmailVerify');
                GetSuggestion();
                $('.btnResend').addClass('block');               
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
                for (var i = 0; i < 4; i++) {
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
//ABHA Address Creation                                                 
function createAbhaAadhaarDetail(elem) {
    if ($('#txtAAC_AbhaAddress').val() == '') {
        alert('Please Provide Abha Address');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/createAbhaAadhaarDetail";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.abhaAddress = $('#txtAAC_AbhaAddress').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                getProfile(objBO.abhaAddress)
                $('#responseMsg').html('<b>Response : </b>Successfully Created ABHA Id : ' + $('#txtHealthIdNumber').val()).show();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
            $('#txtAAC_AbhaAddress').val('')
            $('#AbhaSuggesionList').empty();
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