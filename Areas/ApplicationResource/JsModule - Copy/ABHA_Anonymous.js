var baseUrl = "http://localhost:54699/";
var _requestId = null;
var _healthIdNumber = null;
$(document).ready(function () {
    NextStep('Authentication')
})
function Search_Patient(elem) {
    if ($('#txtAbhaAddress').val() == '') {
        alert('Please Provide Abha Address');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/Search_Patient";
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
function GetQRCode(elem) {
    if ($('#txtAbhaAddress1').val() == '') {
        alert('Please Provide Abha Address');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_GetQRCode";
    var objBO = {};
    objBO.abhaAddress = $('#txtAbhaAddress1').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                _requestId = data.requestId;
                $('#responseMsg2').html('<b>Response : </b>' + data.message).show();
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
function GetHIPQRCode(elem) {
    if ($('#txtHIP').val() == '') {
        alert('Please Provide HIP');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_GetHIPQRCode";
    var objBO = {};
    objBO.hip = $('#txtHIP').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                _requestId = data.requestId;
                $('#responseMsg4').html('<b>Response : </b>' + data.message).show();
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
function GetABHACard(elem) {
    if ($('#txtAbhaAddress2').val() == '') {
        alert('Please Provide Abha Address');
        return
    }
    $(elem).addClass('button-loading');
    var url = config.baseUrl + "/api/M1/ABHA_GetABHACard";
    var objBO = {};
    objBO.token ='';
    objBO.abhaAddress = $('#txtAbhaAddress2').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                _requestId = data.requestId;
                $('#responseMsg3').html('<b>Response : </b>' + data.message).show();
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
