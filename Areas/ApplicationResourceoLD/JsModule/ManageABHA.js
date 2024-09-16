
$(document).ready(function () {
    loadBody('ABHA_Registration_Mobile');
    $('ul.menubar li a').on('click', function () {
        activeLink(this)
    });
    CheckExpiry();
})
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
    $('html,body').scrollTop(20);
}
function activeLink(elem) {
    $('ul.menubar li a').removeClass('activeLink');
    $(elem).addClass('activeLink');
    $('html,body').scrollTop(20);
}