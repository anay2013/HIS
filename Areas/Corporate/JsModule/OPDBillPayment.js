var totalReceivable = 0;
var totalTDSReceivable = 0;
var totalSelectedCount = 0;
$(document).ready(function () {
    $('select').select2();
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    FillCurrentDate('txtPayDate')
    GetPanel();
});
function GetPanel() {
    $('#ddlPanel').empty().append($('<option></option>').val('Select').html('Select'));
    var url = config.baseUrl + "/api/Corporate/IPD_ReceivableQueries";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.IPDNo = '-';
    objBO.BillNo = '-';
    objBO.ReceiptNo = '-';
    objBO.Prm1 = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PanelInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BalanceInfo(logic) {
    if (logic == 'OPD_BalanceInfo:BetweenDate') {
        if ($('#ddlPanel option:selected').text() == 'Select') {
            alert('Please Select Panel')
            return
        }
    }   
    var url = config.baseUrl + "/api/Corporate/IPD_ReceivableQueries";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.BillNo = '-';
    objBO.ReceiptNo = '-';
    objBO.Prm1 = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    if (logic == 'OPD_BalanceInfo:BetweenDate') {
                        $('#tblSelectBillingInfo tbody').empty();
                        $('#tblBillingInfo tbody').empty();
                        $('#txtTotalReceivable').val(0);
                        $.each(data.ResultSet.Table, function (key, val) {
                            if (val.IsSettled == 'Y') {
                                tbody += '<tr style="background:#b7e4b7">';
                                tbody += '<td>Settled</td>';
                            }
                            else {
                                tbody += '<tr>';
                                tbody += '<td><input onchange=selectBill(this) type="checkbox"/></td>';
                            }
                            tbody += '<td>' + val.OPD_ClaimNo + '</td>';
                            tbody += '<td>' + val.BillNo + '</td>';
                            tbody += '<td>' + val.IPDNo + '</td>';
                            tbody += '<td>' + val.patient_name + '</td>';
                            tbody += '<td>' + val.InsuranceName + '</td>';
                            tbody += '<td>' + val.BillDate + '</td>';
                            tbody += '<td>' + val.TotalAmount + '</td>';
                            tbody += '<td>' + val.TotalDiscount + '</td>';
                            tbody += '<td>' + val.NetPayable + '</td>';
                            tbody += '<td class="hide">' + val.InsuranceProvId + '</td>';
                            tbody += '</tr>';
                        });
                        $("#tblBillingInfo tbody").append(tbody);
                    }
                    if (logic == 'OPD_BalanceInfo:ByIPDNo') {
                        var tbody1 = "";
                        $.each(data.ResultSet.Table, function (key, val) {
                            tbody1 += '<tr>';
                            tbody1 += '<td><button onclick=removeSelectedBill(this) class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button></td>';
                            tbody1 += '<td>' + val.OPD_ClaimNo + '</td>';
                            tbody1 += '<td>' + val.BillNo + '</td>';
                            tbody1 += '<td>' + val.IPDNo + '</td>';
                            tbody1 += '<td>' + val.patient_name + '</td>';
                            tbody1 += '<td>' + val.InsuranceName + '</td>';
                            tbody1 += '<td>' + val.BillDate + '</td>';
                            tbody1 += '<td>' + val.TotalAmount + '</td>';
                            tbody1 += '<td>' + val.TotalDiscount + '</td>';
                            tbody1 += '<td>' + val.NetPayable + '</td>';
                            tbody1 += '<td class="hide">' + val.InsuranceProvId + '</td>';
                            tbody1 += '</tr>';
                        });
                        $("#tblSelectBillingInfo tbody").append(tbody1);
                        totalReceivable = 0;
                        $("#tblSelectBillingInfo tbody tr").each(function () {
                            totalReceivable += parseFloat($(this).find('td').eq(9).text());
                        });
                        $('#txtTotalReceivable').val(totalReceivable);
                    }
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function selectBill(elem) {
    var ClaimNo = $(elem).closest('tr').find('td:eq(1)').text();
    var BillNo = $(elem).closest('tr').find('td:eq(2)').text();
    var IPDNo = $(elem).closest('tr').find('td:eq(3)').text();
    var patient_name = $(elem).closest('tr').find('td:eq(4)').text();
    var InsuranceName = $(elem).closest('tr').find('td:eq(5)').text();
    var BillDate = $(elem).closest('tr').find('td:eq(6)').text();
    var TotalAmount = $(elem).closest('tr').find('td:eq(7)').text();
    var Discount = $(elem).closest('tr').find('td:eq(8)').text();
    var NetPayable = $(elem).closest('tr').find('td:eq(9)').text();

    var InsuranceId = $(elem).closest('tr').find('td:eq(10)').text();
    var isCheck = $(elem).is(':checked');
    if (isCheck) {
        var tbody = "";
        tbody += '<tr>';
        tbody += '<td><button onclick=removeSelectedBill(this) class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button></td>';
        tbody += '<td>' + ClaimNo + '</td>';
        tbody += '<td>' + BillNo + '</td>';
        tbody += '<td>' + IPDNo + '</td>';
        tbody += '<td>' + patient_name + '</td>';
        tbody += '<td>' + InsuranceName + '</td>';
        tbody += '<td>' + BillDate + '</td>';
        tbody += '<td>' + TotalAmount + '</td>';
        tbody += '<td>' + Discount + '</td>';
        tbody += '<td>' + NetPayable + '</td>';
        tbody += '<td class="hide">' + InsuranceId + '</td>';
        tbody += '</tr>';
    }
    else {
        $("#tblSelectBillingInfo tbody tr").each(function () {
            if ($(this).find('td:eq(2)').text() == BillNo)
                $(this).remove();
        });
    }
    $("#tblSelectBillingInfo tbody").append(tbody);
    totalReceivable = 0;
    $("#tblSelectBillingInfo tbody tr").each(function () {
        totalReceivable += parseFloat($(this).find('td').eq(9).text());
    });
    $('#txtTotalReceivable').val(totalReceivable);
}
function removeSelectedBill(elem) {
    if (confirm('Are you sure to Remove?')) {
        $(elem).closest('tr').remove();
        var BillNo = $(elem).closest('tr').find('td:eq(1)').text();
        $("#tblBillingInfo tbody tr").each(function () {
            if ($(this).find('td:eq(1)').text() == BillNo)
                $(this).find('td:eq(0)').find('input:checkbox').prop('checked', false);
        });
        totalReceivable = 0;
        $("#tblSelectBillingInfo tbody tr").each(function () {
            totalReceivable += parseFloat($(this).find('td').eq(9).text());
        });
        $('#txtTotalReceivable').val(totalReceivable);
    }
}
function IPD_BillPayment() {
    if (confirm('Are you sure to Submit')) {
        if ($('#tblSelectBillingInfo tbody tr').length < 1) {
            alert('Please Select Bill');
            return
        }
        if ($('#ddlPayMode option:selected').text() == 'Select') {
            alert('Please Select Pay Mode')
            return
        }
        if ($('#ddlBankName option:selected').text() == 'Select') {
            alert('Please Select Bank Name')
            return
        }
        if ($('#txtReferenceNo').val() == '') {
            alert('Please Provide Reference No')
            return
        }
        var url = config.baseUrl + "/api/Corporate/IPD_BillPayment";
        var objBO = [];
        $("#tblSelectBillingInfo tbody tr").each(function () {
            objBO.push({
                'HospId': Active.HospId,
                'TPA_PanelId': $('#ddlPanel option:selected').val(),
                'PayMode': $('#ddlPayMode option:selected').text(),
                'BankName': $('#ddlBankName option:selected').text(),
                'Remark': $('#txtRemark').val(),
                'RefNo': $('#txtReferenceNo').val(),
                'Pay_ChequeDate': $('#txtPayDate').val(),
                'TotalBillAmount': $('#txtTotalReceivable').val(),
                'TDSAmt':0,

                'InsuranceProvId': $(this).find('td:eq(9)').text(),
                'PanelId': $(this).find('td:eq(9)').text(),
                'BillNo': $(this).find('td:eq(2)').text(),
                'IPDNo': $(this).find('td:eq(3)').text(),
                'Recievable': $(this).find('td:eq(8)').text(),
                'PaidAmount': 0,
                'TDSAmount': 0,
                'BadDebt': '-',
                'BadDebtType': '-',

                'login_id': Active.userId,
                'Logic': 'PayBill'
            });
        });        
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                    Clear();
                } else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function checkTotalPayment() {
    if (eval($('#txtPaymentAmount').val()) != eval($('#txtTotalReceivable').val())) {
        // alert('Incorrect Bill Payment Amount');
        $('#txtPaymentAmount').css('border-color', 'red').focus();
        $('#btnPayment').css('pointer-events', 'none');
    }
    else {
        $('#txtPaymentAmount').removeAttr('style');
        $('#btnPayment').css('pointer-events', 'all');
    }
}
function Clear() {
    $('#tblBillingInfo tbody').empty();
    $('#tblSelectBillingInfo tbody').empty();
    $('select').prop('selectedIndex', '0').change();
    $('#txtReferenceNo').val('');
    $('#txtPaymentAmount').val(0);
    $('#txtTotalReceivable').val(0);    
    $('#btnPayment').css('pointer-events', 'none');
}