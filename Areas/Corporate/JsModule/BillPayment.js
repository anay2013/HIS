var totalReceivable = 0;
var totalTDSReceivable = 0;
var totalSelectedCount = 0;
$(document).ready(function () {
    $('select').select2();
    FillCurrentDate('txtPayDate')
    GetPanel();
});
function GetPanel() {
    $('#ddlPanel').empty().append($('<option></option>').val('Select').html('Select'));
    $('#ddlBankName').empty().append($('<option></option>').val('Select').html('Select')).select2();
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
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlBankName').append($('<option></option>').val(val.AccountNo).html(val.BankName));
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
    var Prm1 = '';
    if (logic == 'BalanceInfo:InsuranceId') {
        if ($('#ddlPanel option:selected').text() == 'Select') {
            alert('Please Select Panel')
            return
        }
        Prm1 = $('#ddlPanel option:selected').val();
    }


    else if (logic == 'BalanceInfo:ByIPDNo')
        Prm1 = $('#txtIPDNo').val();

    $('#ddlPanel option:selected').val()



    $('#tblSelectBillingInfo tbody').empty();
    $('#tblBillingInfo tbody').empty();
    $('#txtTotalReceivable').val(0);
    $('#txtTotalTDSReceivable').val(0);
    var url = config.baseUrl + "/api/Corporate/IPD_ReceivableQueries";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.BillNo = '-';
    objBO.ReceiptNo = '-';
    objBO.Prm1 = Prm1;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IsSettled == 'Y') {
                            tbody += '<tr style="background:#b7e4b7">';
                            tbody += '<td>Settled</td>';
                        }
                        else {
                            tbody += '<tr>';
                            tbody += '<td><input onchange=selectBill(this) type="checkbox"/></td>';
                        }
          
                        tbody += '<td>' + val.BillNo + '</td>';
                        tbody += '<td>' + val.ClaimNo + '</td>';
                        tbody += '<td>' + val.IPDNo + '</td>';
                        tbody += '<td>' + val.patient_name + '</td>';
                        tbody += '<td>' + val.InsuranceName + '</td>';
                        tbody += '<td>' + val.admitDate + '</td>';
                        tbody += '<td>' + val.dischargeDate + '</td>';
                        tbody += '<td>' + val.NetPayable + '</td>';
                        tbody += '<td>' + val.Received + '</td>';
                        tbody += '<td>' + val.Receivable + '</td>';
                        tbody += '<td>' + val.TDS_Receivable + '</td>';
                        tbody += '<td class="hide">' + val.InsuranceProvId + '</td>';
                        tbody += '</tr>';
                    });
                    $("#tblBillingInfo tbody").append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function selectBill(elem) {
    var BillNo = $(elem).closest('tr').find('td:eq(1)').text();
    var claimNo = $(elem).closest('tr').find('td:eq(2)').text();
    var IPDNo = $(elem).closest('tr').find('td:eq(3)').text();
    var patient_name = $(elem).closest('tr').find('td:eq(4)').text();
    var InsuranceName = $(elem).closest('tr').find('td:eq(5)').text();
    var admitDate = $(elem).closest('tr').find('td:eq(6)').text();
    var dischargeDate = $(elem).closest('tr').find('td:eq(7)').text();
    var Receivable = $(elem).closest('tr').find('td:eq(10)').text();

    var InsuranceId = $(elem).closest('tr').find('td:eq(12)').text();
    var isCheck = $(elem).is(':checked');
    if (isCheck) {
        var tbody = "";
        tbody += '<tr>';
        tbody += '<td><button onclick=removeSelectedBill(this) class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button></td>';
        tbody += '<td>' + BillNo + '</td>';
        tbody += '<td>' + claimNo + '</td>';
        tbody += '<td>' + IPDNo + '</td>';
        tbody += '<td>' + patient_name + '</td>';
        tbody += '<td>' + InsuranceName + '</td>';
        tbody += '<td>' + admitDate + '</td>';
        tbody += '<td>' + dischargeDate + '</td>';
        tbody += '<td>' + Receivable + '</td>';
        tbody += '<td><input type="text" onkeyup=calculate() value="' + Receivable + '"/></td>';
        tbody += '<td>0</td>';
        tbody += '<td><input type="text"/></td>';
        tbody += '<td class="hide">' + InsuranceId + '</td>';
        tbody += '</tr>';
    }
    else {
        $("#tblSelectBillingInfo tbody tr").each(function () {
            if ($(this).find('td:eq(1)').text() == BillNo)
                $(this).remove();
        });
    }
    $("#tblSelectBillingInfo tbody").append(tbody);
    totalReceivable = 0;
    totalTDSReceivable = 0;
    $("#tblSelectBillingInfo tbody tr").each(function () {
        totalReceivable += parseFloat($(this).find('td').eq(8).text());
        totalTDSReceivable += parseFloat($(this).find('td').eq(9).text());
    });
    $('#txtTotalReceivable').val(totalReceivable);
    $('#txtTotalTDSReceivable').val(totalTDSReceivable);
}
function removeSelectedBill(elem) {
    if (confirm('Are you sure to Remove?')) {
        $(elem).closest('tr').remove();
        var BillNo = $(elem).closest('tr').find('td:eq(1)').text();
        $("#tblBillingInfo tbody tr").each(function () {
            if ($(this).find('td:eq(1)').text() == BillNo)
                $(this).find('td:eq(0)').find('input:checkbox').prop('checked', false);
        });
    }
}
function calculate() {
    var receivable = 0;
    var paid = 0;
    $("#tblSelectBillingInfo tbody tr").each(function () {
        receivable = parseFloat($(this).find('td').eq(7).text());
        paid = parseFloat($(this).find('td:eq(8) input:text').val());
        if (receivable < paid) {
            $(this).find('td:eq(9)').text(0);
            $(this).find('td:eq(8) input:text').val(receivable)
        }
        else {
            $(this).find('td:eq(9)').text(receivable - paid);
        }

    });
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
                'TDSAmt': $('#txtTotalTDSReceivable').val(),

                'InsuranceProvId': $(this).find('td:eq(12)').text(),
                'PanelId': $(this).find('td:eq(12)').text(),
                'BillNo': $(this).find('td:eq(1)').text(),
                'IPDNo': $(this).find('td:eq(3)').text(),
                'Recievable': $(this).find('td:eq(8)').text(),
                'PaidAmount': $(this).find('td:eq(9) input:text').val(),
                'TDSAmount': 0,
                'BadDebt': $(this).find('td:eq(10)').text(),
                'BadDebtType': $(this).find('td:eq(11) input:text').val(),

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
    $('#txtTotalTDSAmount').val(0);
    $('#txtTotalAdjAmount').val(0);
    $('#btnPayment').css('pointer-events', 'none');
}