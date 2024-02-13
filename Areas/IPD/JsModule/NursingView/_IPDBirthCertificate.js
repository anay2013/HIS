
$(document).ready(function () {
   
    CurrentDateTime1();
    FillCurrentDate('txtdateIssue');
    Loaddata();
   
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
   
    $('#txtdatetime').val(today);
    var today1  = sessionStorage.getItem('ServerTodayDate').split('T')[0];
    $('#txtdatetime').attr("max", today1  + 'T00:00:00');

}

function Savedata() {
    if ($('#ddlDeliveryType').val() == '0') {
        alert('Please Select Delivery Type');
        $('#DeliveryType').focus();
        return
    }
    var isConfirmed = confirm('Are you sure you want to Save the data?');
    if (isConfirmed) {
        debugger
        var url = config.baseUrl + "/api/IPDNursing/IPD_InsertBirthCertificate";
        var objBO = {};
        objBO.AutoId = '0';
        objBO.Mother_UHID = _UHID;
        objBO.IPDNo = _IPDNo;
        objBO.IssueDate = $("#txtdateIssue").val();
        objBO.DeliveryType = $('#ddlDeliveryType option:selected').val();
        objBO.DeliveryTime = $('#txtdatetime').val();
        objBO.BabyName = $("#txtbabyname").val();
        objBO.Gender = $('input[name="Gender"]:checked').val();
        objBO.Height = $("#txtheight").val();
        objBO.Weight = $("#txtWeight").val();
        objBO.GuardianName = $("#txtguardian").val();
        objBO.Address = $("#textaddress").val();
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
                    $("#txtdateIssue").val('');
                    $("#ddlDeliveryType").val('0');
                    $("#txtdatetime").val('');
                    $("#txtbabyname").val('');
                    $('input[name="Gender"]').prop('checked', false);
                    $("#txtheight").val('');
                    $("#txtWeight").val('');
                    $("#txtguardian").val('');
                    $("#textaddress").val('');

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

function Loaddata() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.AutoId = '0';
    objBO.Mother_UHID = _UHID;
    objBO.IPDNo = _IPDNo;
    objBO.IssueDate = $('#txtdateIssue').val();
    objBO.DeliveryType = '-';
    objBO.DeliveryTime = $('#txtdatetime').val();
    objBO.BabyName = '-';
    objBO.Gender = '-';
    objBO.Height = '-';
    objBO.Weight = '-';
    objBO.GuardianName = '-';
    objBO.Address = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetDBirthCertificateInfoByIPDNo";
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
                            var issuedate = data.ResultSet.Table[0].IssueDate.split('/')[2] + '-' + data.ResultSet.Table[0].IssueDate.split('/')[1] + '-' + data.ResultSet.Table[0].IssueDate.split('/')[0];
                            $("#txtdateIssue").val(issuedate);
                            $("#ddlDeliveryType").val(data.ResultSet.Table[0].DeliveryType);
                            $("#txtdatetime").val(data.ResultSet.Table[0].DeliveryTime);
                            $("#txtbabyname").val(data.ResultSet.Table[0].BabyName);
                            $('input[name="Gender"][value="' + data.ResultSet.Table[0].Gender + '"]').prop('checked', true);
                            $('#txtheight').val(data.ResultSet.Table[0].Height);
                            $('#txtWeight').val(data.ResultSet.Table[0].Weight);
                            $('#txtguardian').val(data.ResultSet.Table[0].GuardianName);
                            $('#textaddress').val(data.ResultSet.Table[0].Address);
                            $("#btnupdate").show();
                            $("#btnsave").hide();

                        }
                        else {
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
function Updatedata() {
    var isConfirmed = confirm('Are you sure you want to Update the data?');
    if (isConfirmed) {
        debugger
        var id = $("#txtautoid").text();
        var url = config.baseUrl + "/api/IPDNursing/IPD_InsertBirthCertificate";
        var objBO = {};
        objBO.AutoId = id;
        objBO.Mother_UHID = _UHID;
        objBO.IPDNo = _IPDNo;
        objBO.IssueDate = $("#txtdateIssue").val();
        objBO.DeliveryType = $('#ddlDeliveryType option:selected').val();
        objBO.DeliveryTime = $('#txtdatetime').val();
        objBO.BabyName = $("#txtbabyname").val();
        objBO.Gender = $('input[name="Gender"]:checked').val();
        objBO.Height = $("#txtheight").val();
        objBO.Weight = $("#txtWeight").val();
        objBO.GuardianName = $("#txtguardian").val();
        objBO.Address = $("#textaddress").val();
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
                    $("#txtdateIssue").val('');
                    $("#ddlDeliveryType").val('0');
                    $("#txtdatetime").val('');
                    $("#txtbabyname").val('');
                    $('input[name="Gender"]').prop('checked', false);
                    $("#txtheight").val('');
                    $("#txtWeight").val('');
                    $("#txtguardian").val('');
                    $("#textaddress").val('');

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

function PrintBirthCertificate() {
    var url = "../Print/BirthCertificate?IPDNo=" + _IPDNo
    window.open(url, '_blank');
    
}