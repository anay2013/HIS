var _panelId = '';
$(document).ready(function () {

});

function GetAdjustedInfo() {
    $('#tblFundInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/CanelFundMGM_Queries";
    var objBO = {};
    objBO.hospiId = '-';
    objBO.PanelId = '-';
    objBO.UHID = '-';
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.prm_1 = $('#txtIPDNO').val();
    objBO.prm_2 = '-';
    objBO.prm_3 = '-';
    objBO.loginId = Active.userId;
    objBO.logic = 'GetIpdAdjustment';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            if (Object.keys(data.ResultSet.Table).length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    var PatientInfo = data.ResultSet.Table[0].PatientInfo;
                    $('#PatientInfo').html(PatientInfo);
                    if (temp != val.PanelName) {
                        tbody += "<tr style='background:#d9d9d9'>";
                        tbody += "<td colspan='10' style='font-size:12px;'>" + val.PanelName + "</td>";
                        tbody += "</tr>";
                        temp = val.PanelName;
                    }
                    else {

                    }
                    tbody += "<tr>";
                    tbody += "<td>" + val.UHID + "</td>";
                    tbody += "<td>" + val.trn_date + "</td>";
                    tbody += "<td>" + val.Amount + "</td>";
                    tbody += "<td>" + val.RefNo + "</td>";
                    tbody += "<td>" + val.RefDetail + "</td>";
                    tbody += "<td>" + val.UtrDate + "</td>";
                    tbody += "<td>" + val.IsBillClosed + "</td>";
                    if (val.IsBillClosed == "Y") {

                        tbody += "<td><button class='btn btn-danger btn-xs' disabled><i class='fa fa-close'>&nbsp;</i>Cancel</button></td>";
                    }
                    if (val.IsBillClosed == "N") {
                        tbody += "<td><button id='btnCancel'onclick=CancelAdjustedItem(" + val.AutoId + ")  class='btn btn-danger btn-xs'><i class='fa fa-close'>&nbsp;</i>Cancel</button></td>";
                    }
                    tbody += "</tr>";
                });
                $('#tblFundInfo tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function CancelAdjustedItem(autoId) {
    $("#btnCancel").prop("disabled", true);
    if (confirm('Are you sure to Cancel')) {
        var url = config.baseUrl + "/api/Corporate/InsertFundManagement";
        var objBO = {};
        objBO.AutoId = autoId;
        objBO.PanelId = _panelId;
        objBO.trn_date = '1999-01-01';
        objBO.Amount = '-';
        objBO.RefDetail = '-';
        objBO.RefNo = '-';
        objBO.UHID = '-';
        objBO.UTRNo = '-';
        objBO.UTRDate = '1999-01-01';
        objBO.TnxType = '-';
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'CancelAdjustedAmount';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#btnCancel").prop("disabled", true);
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}





