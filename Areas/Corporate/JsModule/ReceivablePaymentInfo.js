$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    CloseSidebar();
    $('#tblReceivablePaymentInfo tbody').on('click', '#paymentdetails', function () {
        ReceiptNo = $(this).closest('tr').find('td:eq(1)').text();
        Popdetails(ReceiptNo)
    });

});


function Popdetails(ReceiptNo) {
    $('#paymentdetailspop').modal('show');
    ReceivablePaymentDetails(ReceiptNo);
}
function ReceivablePaymentInfo(logic) {
    debugger
    $("#tblReceivablePaymentInfo tbody").empty();
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var count = 0;
                if (Object.keys(data.ResultSet.Table).length) {

                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";

                        tbody += "<td style='width:5%;'>" + count + "</td>";
                        tbody += "<td style='width:10%;'><a href='#' style='color: blue;' id='paymentdetails'>" + val.MasterReceiptNo + "</a></td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.PayMode + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.RefNo + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.BankName + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Amount + "</td>";
                        tbody += "<td style='width:7%;text-align:left'>" + val.chequeDate + "</td>";
                        tbody += "<td style='width:15%;text-align:left'>" + val.emp_name + "</td>";
                        tbody += "<td style='width:15%;text-align:left'>" + val.Remark + "</td>";
                        tbody += "<td style='width:15%;text-align:left'>" + val.EntryDate + "</td>";

                        if (val.FilePath.length > 10) {
                            tbody += "<td style='width:5%;text-align:center'><a href='" + val.FilePath + "' target='_blank'><i class='fa fa-folder-open' style='color:orange;font-size:15px'></i></a></td>";
                        }
                        else {
                            tbody += "<td style='width:10%;text-align:left'></td>";
                        }
                        tbody += "</tr>";
                    });
                    $('#tblReceivablePaymentInfo tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReceivablePaymentDetails(ReceiptNo) {
    debugger
    $("#tblPaymentDetails tbody").empty();
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = ReceiptNo;
    objBO.Prm2 = '-';
    objBO.Logic = 'PaymentDetailByReceiptNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.IPOPType + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.IPDNo + "</td>";
                        tbody += "<td style='text-align:left;width:25%'>" + val.patient_name + "</td>";
                        tbody += "<td style='text-align:left;width:15%'>" + val.Tnxdate + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.Receivable + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.TDS + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.IPD_Payment + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.IPD_BadDet + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPaymentDetails tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function PaymentInfoPrint(ReceiptNo) {
    var url = "/Corporate/Print/PrintInvoicePaymentInfo?ReceiptNo=" + ReceiptNo;
    window.open(url, '_blank');
}




