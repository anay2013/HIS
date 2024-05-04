var _BillNo = "";
$(document).ready(function () {
    ReceiptInfoByIPDNo();
    $('table thead').on('change', 'input:checkbox', function () {
        var IsCheck = $(this).is(':checked');
        if (IsCheck)
            $(this).parents('table').find('tbody').find('input:checkbox').prop('checked', true);
        else
            $(this).parents('table').find('tbody').find('input:checkbox').prop('checked', false);
    });
});

function ReceiptInfoByIPDNo() {
    $('#tblBillInfo tbody').empty();
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
    objBO.Logic = 'ReceitInfoByIpdNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ReceiptNo + "</td>";
                        tbody += "<td>" + val.receiptDate + "</td>";
                        tbody += "<td class='text-right'>" + val.Amount + "</td>";
                        tbody += "<td>" + val.PayMode + "</td>";
                        tbody += "<td>" + val.ReceiptType + "</td>";
                        tbody += "<td>" + val.EntryBy + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblBillInfo tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('.BillStatus span').text(val.BillStatus);
                        $('.BillStatus i').addClass((val.BillStatus == 'Mismached') ? 'fa-ban' : 'fa-check-circle');
                        $('.BillStatus').addClass(val.BillStatus);
                        (val.BillStatus == 'Mismached') ? $('#btnCloseBill').addClass('disabled') : $('#btnCloseBill').removeClass('disabled');
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BillPrint_CategoryWiseOnlyBill() {
    var ExcludeDiscount = $('input[name=ExcludeDiscount]').is(':checked') ? 'Y' : 'N';
    var strReceiptList = [];
    $('#tblBillInfo tbody').find('input:checkbox:checked').each(function () {
        strReceiptList.push($(this).closest('tr').find('td:eq(1)').text());
    });
    var url = "../Print/IPDBillSummary?_ReceiptList=" + window.btoa(strReceiptList.join(',')) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=CategorywiseOnly&ExcludeAdlItemDiscount=" + ExcludeDiscount;
    window.open(url, '_blank');
}
function BillPrint_ItemWise() {
    var strReceiptList = [];
    var ExcludeDiscount = $('input[name=ExcludeDiscount]').is(':checked') ? 'Y' : 'N';
    $('#tblBillInfo tbody').find('input:checkbox:checked').each(function () {
        strReceiptList.push($(this).closest('tr').find('td:eq(1)').text());
    });
    var url = "../Print/IPDBillSummary?_ReceiptList=" + window.btoa(strReceiptList.join(',')) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=ItemWise&ExcludeAdlItemDiscount=" + ExcludeDiscount;
    window.open(url, '_blank');

    //var url = "../Print/IPDBillSummary?_ReceiptList=" + window.btoa(_BillNo) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=ItemWise";
    //window.open(url, '_blank');
}
function BillPrint_DateWise() {
    var ExcludeDiscount = $('input[name=ExcludeDiscount]').is(':checked') ? 'Y' : 'N';
    var strReceiptList = [];
    $('#tblBillInfo tbody').find('input:checkbox:checked').each(function () {
        strReceiptList.push($(this).closest('tr').find('td:eq(1)').text());
    });
    var url = "../Print/IPDBillSummary?_ReceiptList=" + window.btoa(strReceiptList.join(',')) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=DateWise&ExcludeAdlItemDiscount=" + ExcludeDiscount;
    window.open(url, '_blank');
}
function BillPrint_IncludingPackagedItem() {
    var ExcludeDiscount = $('input[name=ExcludeDiscount]').is(':checked') ? 'Y' : 'N';
    var strReceiptList = [];
    $('#tblBillInfo tbody').find('input:checkbox:checked').each(function () {
        strReceiptList.push($(this).closest('tr').find('td:eq(1)').text());
    });
    var url = "../Print/IPDBillSummary?_ReceiptList=" + window.btoa(strReceiptList.join(',')) + "&_IPDNo=" + window.btoa(_IPDNo) + "&_BillPrintType=IncludingPackagedItem&ExcludeAdlItemDiscount=" + ExcludeDiscount;
    window.open(url, '_blank');
}
function Receipt_IPDDischargeReport() {
    var url = "../Print/IPDDischargeReport?_IPDNo=" + _IPDNo;
    window.open(url, '_blank');
}
function PrintAllLabReport() {
    var url = config.rootUrl + "/Lab/print/PrintAllLabReportIPD?IPDNo=" + _IPDNo+"&IsHeader=N";
    window.open(url, '_blank');
}