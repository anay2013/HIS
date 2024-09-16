var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('Authentication')
    $('#AbhaSuggesionList').on('click', 'li', function () {
        var id = $(this).text();
        $('#txtHealthIdNumber').val(id);
    });
    LoadDOB()
    ClientAuthorization()
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
function LoadDOB() {
    for (var i = 1; i <= 31; i++) {
        var day = (i < 10) ? ('0' + i) : i;
        $('#ddlDay').append("<option>" + day + "</option>");
    }
    for (var j = 1; j <= 12; j++) {
        var month = (j < 10) ? '0' + j : j
        $('#ddlMonth').append("<option>" + month + "</option>");
    }
    var year = new Date().getFullYear();
    for (var k = 1940; k <= year; k++) {
        $('#ddlYear').append("<option>" + k + "</option>");
    }
}
function GetOTP(elem) {
    if ($('#txtMobileNo').val().length != 10) {
        alert('Please Provide 10 digit Mobile No');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP1_ABHACreateByMobile_GetOTP";
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
    $('#AbhaAddressList').empty();
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP2_ABHACreateByMobile_VerifyOTP";
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
                $('#responseMsg').empty().hide();
                var response = data.phrAddress;
                for (var i = 0; i < response.length; i++) {
                    $('#AbhaAddressList').append("<li onclick=getProfile('" + response[i] + "')>" + response[i] + "</li>");
                }
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
function SubmitDetail(elem) {
    if (!validation()) {
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP3_ABHACreateByMobile_Detail";
    var objBO = {};
    objBO.requestId = _requestId;
    objBO.firstName = $('#txtFirstName').val();
    objBO.middleName = $('#txtMiddleName').val();
    objBO.lastName = $('#txtLastName').val();
    objBO.gender = $('#ddlGender option:selected').val();
    objBO.mobile = $('#txtUserMobileNo').val();
    objBO.email = $('#txtEmail').val();
    objBO.dayOfBirth = $('#ddlDay option:selected').text();
    objBO.monthOfBirth = $('#ddlMonth option:selected').text();
    objBO.yearOfBirth = $('#ddlYear option:selected').text();
    objBO.pincode = $('#txtPinCode').val();
    objBO.address = $('#txtAddress').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data != null) {
                $('#responseMsg').html('<b>Response : </b>: ' + data.message).show();
                Clear();
                $('#txtHealthIdNumber').focus();
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
function CreateABHA(elem) {
    if (_requestId == null) {
        alert('Invalid Request Id')
        return;
    }
    if ($('#txtHealthIdNumber').val() == '') {
        alert('Please Provide Health Id Number');
        return
    }

    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/STEP4_ABHACreateByMobile_HealthId";
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
            console.log(data)
            if (data != null) {
                $('#responseMsg').html('<b>Response : </b>Successfully Created ABHA Id : ' + $('#txtHealthIdNumber').val()).show();
                getProfile($('#txtHealthIdNumber').val())
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $(elem).removeClass('button-loading');
            $('#txtHealthIdNumber').val('')
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
function Search_Patient(abhaAddress) {
    $('.ProfileLoading').show();
    var url = config.baseUrl + "/api/M1/Search_Patient";
    var objBO = {};
    objBO.abhaAddress = abhaAddress;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data != null) {
                var healthIdNumber = data.healthIdNumber;
                getProfile(healthIdNumber);
            } else {
                alert('Health Id Number Not Found.')
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
function Clear() {
    $('.detailForm input:text').val('');
    $('#ddlGender').prop('selectedIndex', '0');
}
function validation() {
    var requestId = _requestId;
    var firstName = $('#txtFirstName').val();
    var middleName = $('#txtMiddleName').val();
    var lastName = $('#txtLastName').val();
    var gender = $('#ddlGender option:selected').val();
    var mobile = $('#txtUserMobileNo').val();
    var email = $('#txtEmail').val();
    var dayOfBirth = $('#ddlDay option:selected').text();
    var monthOfBirth = $('#ddlMonth option:selected').text();
    var yearOfBirth = $('#ddlYear option:selected').text();
    var pincode = $('#txtPinCode').val();
    var address = $('#txtAddress').val();

    if (_requestId == null) {
        alert('Invalid Request Id')
        return false;
    }
    if (firstName == '') {
        alert('Please Provide First Name.')
        return false;
    }
    if (mobile == '') {
        alert('Please Provide Mobile No.')
        return false;
    }
    else if (mobile.length != 10) {
        alert('Please Provide 10 digit Mobile No.')
        return false;
    }
    if (pincode == '') {
        alert('Please Provide Pin Code.')
        return false;
    }
    else if (pincode.length != 6) {
        alert('Please Provide 6 digit Pin Code.')
        return false;
    }
    return true;
}