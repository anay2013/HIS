var _requestId = "";
var _luHealthIdNumber = "";
var abhaAddress = "";
$(document).ready(function () {
    if (sessionStorage.hasOwnProperty('abhaAddress') || sessionStorage.hasOwnProperty('ABHA_UHID_Info')) {
        abhaAddress = (sessionStorage.hasOwnProperty('abhaAddress')) ? sessionStorage.getItem('abhaAddress') : JSON.parse(sessionStorage.getItem('ABHA_UHID_Info')).abhaAddress;
        getProfile(abhaAddress)
        GetQRCode(abhaAddress)
    }
    else {
        window.open(config.rootUrl + "/Admin/ManageABHA?mid=SM446", '_blank');
    }
})
function printableConfig() {
    $("#printable").print({

        // Use Global styles
        globalStyles: false,

        // Add link with attrbute media=print
        mediaPrint: false,

        //Custom stylesheet
        stylesheet: "http://fonts.googleapis.com/css?family=Inconsolata",

        //Print in a hidden iframe
        iframe: false,

        // Don't print this
        noPrintSelector: ".avoid-this",

        // Add this on top
        append: "Free jQuery Plugins<br/>",

        // Add this at bottom
        prepend: "<br/>jQueryScript.net",

        // Manually add form values
        manuallyCopyFormValues: true,

        // resolves after print and restructure the code for better maintainability
        deferred: $.Deferred(),

        // timeout
        timeout: 250,

        // Custom title
        title: null,

        // Custom document type
        doctype: '<!doctype html>'

    });
}
function getProfile(abhaAddress) {
    $('.ProfileLoading').show();
    var url = config.baseUrl + "/api/M1/ABHA_GetProfileByAbhaId";
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
            if (data != null && (!data.hasOwnProperty('error'))) {
                var firstName = data.firstName;
                var middleName = data.middleName;
                var lastName = data.lastName;
                var name = data.firstName + ' ' + data.middleName + ' ' + data.lastName;
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
                $('.card_name span').text(name);
                $('#prof_name').text(name);
                $('#prof_abhanumber').text(healthIdNumber);
                $('#prof_abhaaddress').text(healthId);
                $('#prof_gender').text(gender);
                $('#prof_dob').text(dob);
                $('#prof_mobile').text(mobile);
                var objInfo = {};
                objInfo.healthIdNumber = healthIdNumber;
                objInfo.photo = profilePhoto;
                objInfo.name = name;
                sessionStorage.setItem('abha_login_info', JSON.stringify(data))
            } else {
                alert(JSON.parse(data.message).message);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
        complete: function () {
            $('.ProfileLoading').hide();
            login_user()
        }
    });
}
function GetQRCode(abhaAddress) {
    $('.QRCode').attr('src', 'https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/QR_code_for_mobile_English_Wikipedia.svg/800px-QR_code_for_mobile_English_Wikipedia.svg.png');
    return
    var url = config.baseUrl + "/api/M1/ABHA_GetQRCode";
    var objBO = {};
    objBO.abhaAddress = abhaAddress;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(btoa(data))
            if (data != null) {
                var qr = 'data:image/png;base64,' + btoa(data);
                $('.QRCode').attr('src', qr);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
    });
}

function GetABHACard(logic) {
    var url = config.baseUrl + "/api/M1/ABHA_GetABHACard";
    var objBO = {};
    objBO.token = '';
    objBO.abhaAddress = abhaAddress;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                _requestId = data.requestId;
                if (logic == 'download_ABHA_Card') {
                    var a = document.createElement("a");
                    a.href = 'data:image/png;base64,' + data.message;
                    a.download = "ABHA Card.png";
                    a.click();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
    });
}
function login_user() {
    abhaAddress = (sessionStorage.hasOwnProperty('abhaAddress')) ? sessionStorage.getItem('abhaAddress') : JSON.parse(sessionStorage.getItem('ABHA_UHID_Info')).abhaAddress;
    var data = JSON.parse(sessionStorage.getItem('abha_login_info'));
    var profilePhoto = 'data:image/png;base64,' + data.profilePhoto;
    $('.lu_photo').attr('src', profilePhoto);
    $('.lu_username').html(data.firstName + ' ' + data.middleName + ' ' + data.lastName);
}
