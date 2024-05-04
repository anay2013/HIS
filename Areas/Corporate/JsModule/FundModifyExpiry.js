var _panelId = '';
$(document).ready(function () {
});
function GetFundInfo() {
    $('#tblFundInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/FundMGM_Queries";
    var objBO = {};
    objBO.Prm1 = $('#txtUHID').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'GetFundInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.AutoId + "</td>";
                        tbody += "<td>" + val.trn_date + "</td>";
                        tbody += "<td>" + val.Amount + "</td>";
                        tbody += "<td>" + val.RefDetail + "</td>";
                        tbody += "<td>" + val.UtrNo + "</td>";
                        tbody += "<td>" + val.UtrDate + "</td>";
                        if (val.expdate != null)
                            tbody += "<td><input type='date' class='form-control' min='" + val.UtrDate.split('T')[0] + "' value='" + val.expdate.split('T')[0] + "'/></td>";
                        else
                            tbody += "<td><input type='date' class='form-control' min='" + val.UtrDate.split('T')[0] + "'/></td>";
                        tbody += "<td><button onclick=UpdateExp(this) class='btn btn-warning btn-xs'><i class='fa fa-check-circle'>&nbsp;</i>Update</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblFundInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateExp(elem) {
    if (confirm('Are you sure to Update?')) {
        var url = config.baseUrl + "/api/Corporate/InsertFundManagement";
        var objBO = {};
        objBO.AutoId = $(elem).closest('tr').find('td:first').text();
        objBO.PanelId = '-';
        objBO.trn_date = '2023/05/23';
        objBO.Amount = '-';
        objBO.RefDetail = '-';
        objBO.RefNo = '-';
        objBO.UHID = '-';
        objBO.UTRNo = '-';
        objBO.UTRDate = $(elem).closest('tr').find('input[type=date]').val();
        objBO.TnxType = '-';
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'UpdateExpiry';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
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






