var _requestId = "";
var _luHealthIdNumber = "";
$(document).ready(function () {
    ClientAuthorization()
    $('.menubar ul li:eq(1)').click();
    AbhaLink()
})
function AbhaLink() {
    //$('.linkABHA').css('visibility', (sessionStorage.hasOwnProperty('ABHA_UHID_Info')) ? 'visible' : 'hidden');
    //var ABHA_UHID_Info = JSON.parse(sessionStorage.getItem('ABHA_UHID_Info'));
    //if (ABHA_UHID_Info.IsABHALink) {
    //    $('.linkABHA p:eq(0) span').text(ABHA_UHID_Info.uhid);
    //    $('.linkABHA p:eq(1) span').text(ABHA_UHID_Info.name);
    //    $('.linkABHA p:eq(2) span').text(ABHA_UHID_Info.abhaAddress);
    //    $('#btnLinkAbha').prop('disabled', true);
    //} else {
    //    $('#btnLinkAbha').prop('disabled', false);
    //}
    $('.linkABHA').css('visibility', (sessionStorage.hasOwnProperty('ABHA_UHID_Info')) ? 'visible' : 'hidden');
    var ABHA_UHID_Info = JSON.parse(sessionStorage.getItem('ABHA_UHID_Info'));
    $('.linkABHA p:eq(0) span').text(ABHA_UHID_Info.uhid);
    $('.linkABHA p:eq(1) span').text(ABHA_UHID_Info.name);
    $('.linkABHA p:eq(2) span').text(ABHA_UHID_Info.abhaAddress);   
    (ABHA_UHID_Info.IsABHALink) ? $('#btnLinkAbha').show() : $('#btnLinkAbha').hide();
    $('#btnLinkAbha').prop('disabled', ABHA_UHID_Info.IsABHALink);
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
function activeList(indx) {
    $('.menubar ul li').removeClass('activeList');
    $('.menubar ul li').eq(indx).addClass('activeList');
}
function loadBody(elem) {
    var pagelist = ['DASH_AbhaCard', 'DASH_EditProfile', 'DASH_SetPassword', 'DASH_ReKYCVerification', 'DASH_DeactivateDelete'];
    var page = pagelist[$(elem).index() - 1];
    var indx = $(elem).index();
    $.ajax({
        type: 'POST',
        url: 'AdviceBody',
        data: { page: page },
        success: function (data) {
            $('.dashboard_body').empty();
            $('.dashboard_body').html(data);
            activeList(indx)
        }
    });
}
function logout_abha() {
    if (confirm('are you sure to logout?')) {
        sessionStorage.removeItem('abha_login_info');
        sessionStorage.removeItem('ABHA_UHID_Info');
        window.location.href = config.rootUrl + "/Admin/ManageABHA1?mid=SM446";
    }
}
function LinkABHA() {
    if (confirm('Are you sure to Link Abha to UHID.!')) {
        var url = config.baseUrl + "/api/Appointment/Opd_AppointmentUpdate";
        var objBO = {};
        //Value
        var date = new Date();
        objBO.UHID = JSON.parse(sessionStorage.getItem('ABHA_UHID_Info')).uhid;
        objBO.prm_1 = JSON.parse(sessionStorage.getItem('ABHA_UHID_Info')).abhaAddress;
        objBO.AppDate = '1900/01/01';
        objBO.AppInTime = '01:00';
        objBO.AppOutTime = '01:00';
        objBO.login_id = Active.userId;
        objBO.Logic = 'LinkABHA';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);                  
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}