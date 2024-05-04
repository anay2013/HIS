$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    GetRoomChargesInfo();
    LockPrvDate()
});
function LockPrvDate() {
    $("#txtFrom,#txtTo").each(function () {
        $(this).attr("min", _AdmitDateServer.split('T')[0]);
        $(this).attr("max", sessionStorage.getItem('ServerTodayDate').split('T')[0]);
    });
}

function GetRoomChargesInfo() {
    $('#ddlCategory').empty().append($('<option></option>').val('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetRoomChargesInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                        $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlCategory').append($('<option></option>').val(val.RoomBillingType).html(val.RoomBillingType));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateCatBetweenDate() {
    if (confirm('Are you sure to update?')) {
        var url = config.baseUrl + "/api/IPDBilling/IPD_BillingCategoryUpdate";
        var objBO = {};
        objBO.AutoId =0;
        objBO.IPDNo = _IPDNo;
        objBO.RoomBillingCategory = $('#ddlCategory option:selected').text();
        objBO.from = $('#txtFrom').val(),
        objBO.to = $('#txtTo').val(),
        objBO.Prm1 = "";
        objBO.login_id = Active.userId;
        objBO.Logic = "UpdateBillingCatBetweenDate";

        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}