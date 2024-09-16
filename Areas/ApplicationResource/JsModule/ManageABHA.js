
$(document).ready(function () {
    loadBody('ABHA_Registration_Aadhar');
    $('ul.menubar li a').on('click', function () {
        activeLink(this)
    });
    CheckExpiry();
    AbhaLink();
})
function AbhaLink() {
    $('.linkABHA').css('visibility', (sessionStorage.hasOwnProperty('ABHA_UHID_Info')) ? 'visible' : 'hidden');
    var ABHA_UHID_Info = JSON.parse(sessionStorage.getItem('ABHA_UHID_Info'));
    $('.linkABHA p:eq(0) span').text(ABHA_UHID_Info.uhid);
    $('.linkABHA p:eq(1) span').text(ABHA_UHID_Info.name);
    $('.linkABHA p:eq(2) span').text(ABHA_UHID_Info.abhaAddress);
    (ABHA_UHID_Info.IsABHALink) ? $('.linkABHA p:eq(2)').show() : $('.linkABHA p:eq(2)').hide();
    (ABHA_UHID_Info.IsABHALink) ? $('#btnLinkAbha').show() : $('#btnLinkAbha').hide();
    $('#btnLinkAbha').prop('disabled', ABHA_UHID_Info.IsABHALink);
}
function CheckExpiry() {
    $('.authTimer').eq(1).hide();
    $('.authTimer').eq(0).show();
    var url = config.baseUrl + "/api/M1/ABHA_CreateOrGetToken";
    var objBO = {};
    objBO.expires_in = '-';
    objBO.access_token = '-';
    objBO.IsExired = '-';
    objBO.Logic = 'CheckExpiry';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data.IsExired == 'Y') {
                $('.authTimer').eq(1).hide();
                $('.authTimer').eq(0).show();
                $('.authButton').prop('disabled', false);
            } else {
                $('.authTimer').eq(0).hide();
                $('.authTimer').eq(1).show();
                $('.authButton').prop('disabled', true);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function loadBody(page) {
    $.ajax({
        type: 'POST',
        url: 'AdviceBody',
        data: { page: page },
        success: function (data) {
            $('.rightPanel').empty();
            $('.rightPanel').html(data);
        }
    });    
}
function activeLink(elem) {    
    $('ul.menubar li a').removeClass('activeLink');
    $(elem).addClass('activeLink');
    $('html,body').scrollTop(20);
}