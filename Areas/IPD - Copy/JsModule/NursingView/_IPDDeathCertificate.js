
$(document).ready(function () {
    GetDoctorList();    
    CurrentDateTime1();
});

function CurrentDateTime1() {
    var today = '';
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var hour = date.getHours();
    var minute = date.getMinutes();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    today = year + "-" + month + "-" + day + 'T' + hour + ':' + minute;
   
    $('#txtEstimatedTime').val(today);
    var today1 = sessionStorage.getItem('ServerTodayDate').split('T')[0];
    $('#txtEstimatedTime').attr("max", today1 + 'T00:00:00');


    $('#txtPronouncedDeadt').val(today);
    $('#txtPronouncedDeadt').attr("max", today1 + 'T00:00:00');

}
function Loaddatafill()
{
    debugger
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = _IPDNo;
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1990-01-01';
    objBO.to = '1990-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetDeathCertificateInfoByIPDNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.AutoId != "") {
                            $("#txtautoid").text(data.ResultSet.Table[0].AutoId);
                            $("#txtNature").val(data.ResultSet.Table[0].NatureOfDeath);
                            $("#txtImmediate").val(data.ResultSet.Table[0].ImmediateCauseOfDeath);
                            $("#txtPronouncedDeadt").val(data.ResultSet.Table[0].PronouncedDeathAt);
                            $("#txtEstimatedTime").val(data.ResultSet.Table[0].EstimatedDeathTime);
                            $('#ddldeathDoctorby').val(data.ResultSet.Table[0].DeathCertificateBy).change();
                            $('#txtBodyHandOvered').val(data.ResultSet.Table[0].BodyHandOverTo);
                            $('#txtSufferingFrom').val(data.ResultSet.Table[0].SufferingFrom);
                            $('#txtremark').val(data.ResultSet.Table[0].Remark);
                            $("#btnupdate").show();
                            $("#btnsave").hide();

                        }
                        else
                        {
                            $("#btnsave").show();
                            $("#btnupdate").hide();
                        }
                        
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}  
function Insertdata() {
    var isConfirmed = confirm('Are you sure you want to Save the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/IPDNursing/IPD_InsertDeathCertificate";
        var objBO = {};
        objBO.AutoId = '0';
        objBO.IPDNo = _IPDNo;
        objBO.NatureOfDeath = $("#txtNature").val();
        objBO.ImmediateCauseOfDeath = $("#txtImmediate").val();
        objBO.PronouncedDeathAt = $("#txtPronouncedDeadt").val();
        objBO.EstimatedDeathTime = $("#txtEstimatedTime").val();
        objBO.DeathCertificateBy = $('#ddldeathDoctorby option:selected').val();
        objBO.BodyHandOverTo = $("#txtBodyHandOvered").val();
        objBO.SufferingFrom = $("#txtSufferingFrom").val();
        objBO.Remark = $("#txtremark").val();
        objBO.login_id = Active.userId;
        objBO.Logic = "Insert";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#txtNature").text('');
                    $("#txtImmediate").text('');
                    $("#txtPronouncedDeadt").text('');
                    $("#txtEstimatedTime").text('');
                    $('#ddldeathDoctorby').val('Select').change();
                    $("#txtBodyHandOvered").text('');
                    $("#txtSufferingFrom").text('');
                    $("#txtremark").text('');

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
function GetDoctorList() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';    
    objBO.from = '1990-01-01';
    objBO.to = '1990-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'DoctorList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddldeathDoctorby').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddldeathDoctorby').append($('<option></option>').val(val.DoctorId).html(val.DoctorName));
                    });
                    Loaddatafill()

                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Updatedata() {
    debugger
    var isConfirmed = confirm('Are you sure you want to Update the data?');
    if (isConfirmed) {

        var id = $("#txtautoid").text();
        var url = config.baseUrl + "/api/IPDNursing/IPD_InsertDeathCertificate";
        var objBO = {};
        objBO.AutoId = id;
        objBO.IPDNo = _IPDNo;
        objBO.NatureOfDeath = $("#txtNature").val();
        objBO.ImmediateCauseOfDeath = $("#txtImmediate").val();
        objBO.PronouncedDeathAt = $("#txtPronouncedDeadt").val();
        objBO.EstimatedDeathTime = $("#txtEstimatedTime").val();
        objBO.DeathCertificateBy = $('#ddldeathDoctorby option:selected').val();
        objBO.BodyHandOverTo = $("#txtBodyHandOvered").val();
        objBO.SufferingFrom = $("#txtSufferingFrom").val();
        objBO.Remark = $("#txtremark").val();
        objBO.login_id = Active.userId;
        objBO.Logic = "Update";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#txtNature").val('');
                    $("#txtImmediate").val('');
                    $("#txtPronouncedDeadt").val('');
                    $("#txtEstimatedTime").val('');
                    $('#ddldeathDoctorby').val('Select').change();
                    $("#txtBodyHandOvered").val('');
                    $("#txtSufferingFrom").val('');
                    $("#txtremark").val('');
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
    else
    {
        alert('Data Update canceled.');
    }
}
function Printdataa() {
    var url = "../Print/DeathNotification?IPDNo=" + _IPDNo
    window.open(url, '_blank');
    
}