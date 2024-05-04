var _inv_no = '';
var selectedRow;
var _CreditOrCash = "";
$(document).ready(function () {
    FillSwipeMachines();
    $('#tblPaymentDetails tbody').on('keyup', 'input[type=text]', function () {
        var netAmount = parseFloat($('#txtPayable').text());
        var cash = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val());
        var cheque = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(1)').find('td:eq(1)').find('input[type=text]').val());
        var cc = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(2)').find('td:eq(1)').find('input[type=text]').val());
        var ntfs = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(3)').find('td:eq(1)').find('input[type=text]').val());
        var total = cash + cheque + cc + ntfs;

        if (total > netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be more than Payable Amount...!').css({ 'color': 'red', 'font-size': '11px' });
        }
        else if (total < netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be less than Payable Amount...!').css({ 'color': '#bb8202', 'font-size': '11px' });
        }
        else {
            $('#tblPaymentDetails tbody').find('tr:eq(0),tr:eq(1),tr:eq(2),tr:eq(3)').find('td:eq(1)').find('input[type=text]').removeAttr('style');
            $('#txtError').text('').removeAttr('style');
        }
    });
    $('input[type=checkbox]').on('change', function () {
        isCheck = $(this).is(':checked');
        var val = $(this).val();
        var len = $('input[name=PaymentMode]:checked').length;
        var pay = $('#txtPayable').val();

        var tbody = "";
        if (isCheck) {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == val.toLowerCase()) {
                    $(this).show();
                    $(this).addClass('pay');
                }
            });
            $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
            $('#tblPaymentDetails tbody').find('tr').filter('.pay').first().find('td:eq(1)').find('input[type=text]').val(pay);
        }
        else {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == val.toLowerCase()) {
                    $(this).hide();
                    $(this).removeClass('pay');
                }
            });
            $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
            $('#tblPaymentDetails tbody').find('tr').filter('.pay').first().find('td:eq(1)').find('input[type=text]').val(pay);
        }
    });
});

function IndentBills() {
    $('#tblIndentBills tbody').empty();
    var url = config.PharmacyWebAPI_baseUrl + "/api/hospital/ipopqueries";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.card_no = '-';
    objBO.uhid = '-';
    objBO.IPOPNo = $('#txtIPOPNo').val();;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.prm_1 = '-';
    objBO.Logic = 'Indent-Bills';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            $.each(data.result.Table, function (key, val) {
                if (val.ProcType == 'Completed')
                    tbody += "<tr style='background:#ffb5b5'>";
                else
                    tbody += "<tr>";

                tbody += "<td>" + val.sale_inv_no + "</td>";
                tbody += "<td>" + val.order_no + "</td>";
                tbody += "<td style='display:none'>" + val.info + "</td>";
                tbody += "<td class='text-right'>" + val.net + "</td>";
                tbody += "<td class='text-right'>" + val.HoldStatus + "</td>";
                tbody += "<td><button class='btn btn-warning btn-xs' onclick=selectRow(this);SetSelectedRow(this)><i class='fa fa-sign-in'></i></button></td>";
                tbody += "</tr>";
            });
            $('#tblIndentBills tbody').append(tbody);
            Clear();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SetSelectedRow(elem) {
    selectedRow = elem
    SalesInvoiceInfo();
    FillSwipeMachines();
}
function SalesInvoiceInfo() {
    $('#tblItemInfo tbody').empty();
    var url = config.PharmacyWebAPI_baseUrl + "/api/sales/GetSalesInvoice_Info";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.Sales_Inv_No = $(selectedRow).closest('tr').find('td:eq(0)').text();
    objBO.order_no = $(selectedRow).closest('tr').find('td:eq(1)').text()
    objBO.logic = 'SalesInvoice';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var info = $(selectedRow).closest('tr').find('td:eq(2)').text().split('|');
            $('#txtUnitName').text(info[0]);
            $('#txtSaleInvNo').text(objBO.Sales_Inv_No);
            $('#txtOrderNo').text(objBO.order_no);
            $('#txtPatientName').text(info[1]);
            $('#txtAge').text(0);
            $('#txtIPOP').text(info[2]);
            $('#txtUHID').text(info[3]);
            $('#txtRefName').text(info[4]);
            $('#txtHealthCardNo').text(info[5]);
            $('#txtPanelName').text(info[6]);

            if (info[7].includes('Credit') || info[7].includes('CR')) {
                _CreditOrCash = "Credit";
                $('input[name=PaymentMode]').prop('checked', false).change();
                $('input[name=PaymentMode]').prop('disabled', true);
                $('input[name=Credit]').prop('checked', true).change();
            } else {
                $('input[name=PaymentMode]').prop('checked', true).change();
                $('input[name=PaymentMode]').prop('disabled', false);
                $('input[name=Credit]').prop('checked', false).change();
                _CreditOrCash = "Cash";
            }

            $.each(data.result.Table, function (key, val) {
                $('#txtTotal').text(val.total);
                $('#txtDiscount').text(val.discount);
                $('#txtRoundOff').text(val.roundoff);
                $('#txtPayable').text(val.net);

                $('input[name=PaymentMode]:not(.cash)').prop('checked', false).change();
                $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
                $('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val(val.net);
            });
            $.each(data.result.Table1, function (key, val) {
                tbody += "<tr>";
                tbody += "<td>" + val.item_name + "</td>";
                tbody += "<td>" + val.batch_no + "</td>";
                tbody += "<td>" + val.exp_date.split('T')[0].substring(0, 7) + "</td>";
                tbody += "<td class='text-right'>" + val.pack_type + "</td>";
                tbody += "<td class='text-right'>" + val.pack_qty + "</td>";
                tbody += "<td class='text-right'>" + val.mrp + "</td>";
                tbody += "<td class='text-right'>" + val.sv + "</td>";
                tbody += "<td class='text-right'>" + val.sale_qty + "</td>";
                tbody += "<td class='text-right'>" + val.usr.toFixed(2) + "</td>";
                tbody += "</tr>";
            });

            $.each(data.result.Table2, function (key, val) {
                if (val.ipd_rec_time != null) {
                    $("#btnPostBill").removeAttr('disabled');
                    $("#btnReceive").prop("disabled", true);
                }
                else {
                    $("#btnPostBill").prop("disabled", true);
                    $("#btnReceive").removeAttr('disabled');
                }
            });

            $('#tblItemInfo tbody').append(tbody);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FillSwipeMachines() {
    $('.MachineName').empty().append($('<option></option>').val('Select').html('Select'));
    var url = config.PharmacyWebAPI_baseUrl + "/api/hospital/ipopqueries";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.card_no = '-';
    objBO.uhid = '-';
    objBO.IPOPNo = $('#txtIPOPNo').val();;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.prm_1 = '-';
    objBO.Logic = 'SwipeMachines';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            console.log(data.result.Table);
            $.each(data.result.Table, function (key, val) {
                $('.MachineName').append($('<option></option>').val(val.account_id).html(val.bank_name));
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function MarkIndentReceived() {
    var url = config.PharmacyWebAPI_baseUrl + "/api/sales/GetSalesInvoice_Info";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.Sales_Inv_No = $('#txtSaleInvNo').text();
    objBO.order_no = $('#txtOrderNo').text();
    objBO.logic = 'MarkIndentReceived';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            SalesInvoiceInfo();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function HoldBill() {
    var url = config.PharmacyWebAPI_baseUrl + "/api/sales/GetSalesInvoice_Info";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.Sales_Inv_No = $('#txtSaleInvNo').text();
    objBO.order_no = $('#txtOrderNo').text();
    objBO.logic = 'MarkInvoiceHold';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            IndentBills();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Auto_SalesCancelAndDebitNote() {
    if (confirm('are you sure?')) {
        var url = config.baseUrl + "/api/IPDBilling/Auto_SalesCancelAndDebitNote";
        var objBO = {};
        objBO.unit_id ='MS-H0085';
        objBO.estimateNo = $('#txtSaleInvNo').text();
        objBO.computer_id = '-';
        objBO.counter_id = '-';
        objBO.login_id = Active.userId;
        objBO.logic = '-';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                IndentBills();
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function ViewBill() {
    if (_inv_no == '') {
        alert('Invoice No not Found.');
        return
    }
    var link = "../Print/SalesBill?InvoiceNo=" + _inv_no;
    window.open(link, '_blank');
}
function PostBill() {
    var unitName = $('#txtUnitName').text();
    if (unitName == '') {
        alert('Unit Name Not Found.');
        return
    }
    if ($('input[name=payMode]:eq(2)').is(':checked')) {
        var refNo = $('#txtRefNo').val();
        if (refNo == '') {
            alert('Please Provide Payment Ref. No.');
            $('#txtRefNo').css('border-color', 'red').focus();
            return
        }
        else {
            $('#txtRefNo').removeAttr('style');
        }
    }
    var url = config.baseUrl + "/api/IPDBilling/Retail_SalesBillPosting";
    var objMasterBO = {};
    var objBO = {};
    var BillPaymentInfo = [];
    var netAmount = parseFloat($('#txtPayable').text());
    var cash = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val());
    var cheque = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(1)').find('td:eq(1)').find('input[type=text]').val());
    var cc = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(2)').find('td:eq(1)').find('input[type=text]').val());
    var ntfs = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(3)').find('td:eq(1)').find('input[type=text]').val());
    var total = cash + cheque + cc + ntfs;
    if (total > netAmount) {
        alert('Paid Amount can not be more than Net Amount...!');
        return
    }

    else if (total < netAmount) {
        alert('Paid Amount can not be less than Net Amount...!').css({ 'color': '#bb8202', 'font-size': '11px' });
        return
    }
    var IsPay = $('#tblPaymentDetails tbody tr.pay').length;

    var AccNo = "";
    if (_CreditOrCash == "Credit") {
        AccNo = "16070050";
        BillPaymentInfo.push({
            'unit_id': 'MS-H0085',
            'InvNo': $('#txtSaleInvNo').text(), //Sale Inv No
            'PayMode': "Credit", //Pay Mode
            'AccountNo': AccNo, //Machine Id
            'Amount': parseFloat($('#txtPayable').text()), //Pay Amount
            'referenceNo': "-", //Ref No
            'PayDetail': "-" //Machine Name  
        });
    }
    if (IsPay > 0) {
        $('#tblPaymentDetails tbody tr.pay').each(function () {
            if ($(this).find('td:eq(5)').find('select option:selected').val() == "Select") {
                alert("Select Bank Name in Swipe Card");
                return;
            }

            if ($(this).find('td:eq(0)').text() == "Cash")
                AccNo = "16040001";
            else if ($(this).find('td:eq(0)').text() == "Swipe Card")
                AccNo = $(this).find('td:eq(5)').find('select option:selected').val();

            if (eval($(this).find('td:eq(1)').find('input[type=text]').val()) > 0) {
                BillPaymentInfo.push({
                    'unit_id': 'MS-H0085',
                    'InvNo': $('#txtSaleInvNo').text(), //Sale Inv No
                    'PayMode': $(this).find('td:eq(0)').text(), //Pay Mode
                    'AccountNo': AccNo, //Machine Id
                    'Amount': $(this).find('td:eq(1)').find('input[type=text]').val(), //Pay Amount
                    'referenceNo': $(this).find('td:eq(4)').find('input[type=text]').val(), //Ref No
                    'PayDetail': $(this).find('td:eq(5)').find('select option:selected').text() //Machine Name  
                });
            }

        });
    }

    objBO.unit_id = $('#txtUnitName').text();
    objBO.sale_type = 'Indent';
    objBO.order_no = $('#txtOrderNo').text();
    objBO.estimateNo = $('#txtSaleInvNo').text();
    objBO.pt_name = $('#txtPatientName').text();
    objBO.mobile_no = '-';
    objBO.gstn_no = '-';
    objBO.isHomeDelivery = 'N';
    objBO.presbyCode = '-';
    objBO.prescribedBy = $('#txtRefName').text();
    objBO.memberID = $('#txtHealthCardNo').text();
    objBO.uhid = $('#txtUHID').text();
    objBO.ipopNo = $('#txtIPOP').text();
    objBO.computer_id = '-';
    objBO.counter_id = '-';
    objBO.couponCode = '-';
    objBO.login_id = Active.userId;
    objBO.saleInvNo = $('#txtSaleInvNo').text();
    objMasterBO.SalesBillPosting = objBO;
    objMasterBO.BillPaymentInfo = BillPaymentInfo;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objMasterBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var prm = data.split(':')[1];
            _inv_no = prm;
            alert(data);
            IndentBills();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Clear() {
    $('#txtTotal').text(0.00);
    $('#txtDiscount').text(0.00);
    $('#txtRoundOff').text(0.00);
    $('#txtPayable').text(0.00);

    $('#txtUnitName').text('');
    $('#txtSaleInvNo').text('');
    $('#txtOrderNo').text('');
    $('#txtPatientName').text('XXXXXXXXXXXX');
    $('#txtPanelName').text('XXXXXXXXXXXX');
    $('#txtIPOP').text('XXXXXXXXXXXX');
    $('#txtUHID').text('XXXXXXXXXXXX');
    $('#txtRefName').text('XXXXXXXXXXXX');
    $('#txtHealthCardNo').text('');
    $('#txtRefNo').val('');

    $('#tblItemInfo tbody').empty();
}