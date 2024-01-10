$(document).ready(function () {
    FillCurrentDate("txtfromdate");
    FillCurrentDate("txttodate");

});
function GetDataAllBind() {
    $('#tblSaleVoucher tbody').empty();
    var url = config.baseUrl + "/api/Account/Audit_SalesAndVouchers";
    var objBO = {};
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.loginid = Active.loginid;
    objBO.Logic = 'Audit:InDetail';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.clr == "Red") {
                            tbody += "<tr style='background:#ffb8b8'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        //tbody += "<tr>";
                        tbody += "<td>" + val.BDate.split('T')[0] + "</td>";
                        tbody += "<td>" + val.TotalSales + "</td>";
                        tbody += "<td>" + val.IP_CashSales + "</td>";
                        tbody += "<td>" + val.IP_CreditSales + "</td>";
                        tbody += "<td>" + val.IP_Receivable + "</td>";
                        tbody += "<td>" + val.IP_Copay + "</td>";
                        tbody += "<td>" + val.OP_CashSales + "</td>";
                        tbody += "<td>" + val.OP_CreditSales + "</td>";
                        tbody += "<td>" + val.CashSales + "</td>";
                        tbody += "<td>" + val.NeftAmount + "</td>";
                        tbody += "<td>" + val.SwipeCardAmt + "</td>";
                        tbody += "<td>" + val.VchAmount + "</td>";
                        tbody += "<td>" + val.AuditTotalSales1 + "</td>";
                        tbody += "<td>" + val.AuditTotalSales2 + "</td>";
                        //tbody += "<td>" + val.clr + "</td>";

                        tbody += "</tr>";
                    });
                    $('#tblSaleVoucher tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function DownloadExcelAuditSale() {
    var logic = 'Audit:InDetail';
    $('#tblSaleVoucher tbody').empty();
    var url = config.baseUrl + "/api/Account/Audit_SalesAndVouchers";
    var objBO = {};
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.loginid = 'CHL-02809';
    objBO.Logic = logic;
    objBO.OutPutType = "Excel";
    Global_DownloadExcel(url, objBO, logic + ".xlsx");
}