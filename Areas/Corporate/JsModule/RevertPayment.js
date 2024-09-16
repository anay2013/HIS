$(document).ready(function () {

});
function GetDeleteProessList() {
    $('#tblReport tbody').empty();
    $('#tblReportInfo tbody').empty();
    $('#tblReportInfo2 tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.DoctorId = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $("#txtReceiptNo").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPaymentInfoForRejection';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = '';
            var tbody1 = '';
            var tbody2 = '';
            if (data.ResultSet && data.ResultSet.Table) {
                $.each(data.ResultSet.Table, function (key, val) {
                    tbody += "<tr>";
                    tbody += "<td>" + val.MasterReceiptNo + "</td>";
                    tbody += "<td>" + val.PayMode + "</td>";
                    tbody += "<td style='text-align:center'>" + val.Amount + "</td>";
                    tbody += "<td>" + val.chequeDate + "</td>";
                    tbody += "<td>" + val.RefNo + "</td>";
                    tbody += "<td>" + val.Remark + "</td>";
                    tbody += "<td>" + val.cr_date + "</td>";
                    tbody += "</tr>";
                });
                $('#tblReport tbody').append(tbody);
            }
            if (data.ResultSet && data.ResultSet.Table1) {
                $.each(data.ResultSet.Table1, function (key, val) {
                    tbody1 += "<tr>";
                    tbody1 += "<td>" + val.IPDNo + "</td>";
                    tbody1 += "<td>" + val.TnxType + "</td>";
                    tbody1 += "<td style='text-align:center'>" + val.Amount + "</td>";
                    tbody1 += "<td>" + val.Narration + "</td>";
                    tbody1 += "<td>" + val.ConsiderFor + "</td>";
                    tbody1 += "</tr>";

                    				
                });
                $('#tblReportInfo tbody').append(tbody1);
            }
            if (data.ResultSet && data.ResultSet.Table2) {
                $.each(data.ResultSet.Table2, function (key, val) {
                    tbody2 += "<tr>";
                    tbody2 += "<td>" + val.Voucher_no + "</td>";
                    tbody2 += "<td>" + val.vch_date + "</td>";
                    tbody2 += "<td>" + val.ledgerName + "</td>";
                    tbody2 += "<td style='text-align:center'>" + val.amount + "</td>";
                    tbody2 += "<td>" + val.narration + "</td>";
                    tbody2 += "</tr>";
                  
                });
                $('#tblReportInfo2 tbody').append(tbody2);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function RewardProcess() {
    if (confirm('Are you sure to RewardProcess?')) {
        var url = config.baseUrl + "/api/Corporate/AC_GenerateReceivableReceiving_Reject";
        var objBO = {};
        objBO.hosp_id = Active.HospId; 
        objBO.masterReceiptNo = $("#txtReceiptNo").val();
        objBO.login_id = Active.userId;
        objBO.logic = "ReturnPayment";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                $('#btnDeleteProcess').prop('disabled', true);
                alert(data);

            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}