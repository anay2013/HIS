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
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlTPAList').append($('<option></option>').val(val.PanelId).html(val.PanelName));
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
    objBO.BillNo = $('#ddlTPAList option:selected').val();
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
                var temp = "";
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.PanelName) {
                            tbody += '<tr style="background:lightgreen">';
                            tbody += '<td colspan="13">' + val.PanelName + '</td>';
                            tbody += '</tr>';
                            temp = val.PanelName
                        }

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
    var duplicateInsuranceId = [];
    $("#tblSelectBillingInfo tbody tr").each(function () { duplicateInsuranceId.push($(this).find('td:last').text()) });
    var InsuranceId = $(elem).closest('tr').find('td:eq(12)').text();
    if (duplicateInsuranceId.length > 0 && $.inArray(InsuranceId, duplicateInsuranceId) == -1) {
        alert('Same Insurance Company Allowed!')
        $(elem).prop('checked', false)
        return
    }
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
    $("#tblSelectBillingInfo tbody tr").each(function () {
        totalReceivable += parseFloat($(this).find('td').eq(8).text());
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
    }
}
function calculate() {
    var receivable = 0;
    var paid = 0;

    var totalReceivable = 0;
    var TotalBadDebt = 0;


    $("#tblSelectBillingInfo tbody tr").each(function () {
        receivable = parseFloat($(this).find('td').eq(8).text());
        paid = parseFloat($(this).find('td:eq(9) input:text').val());
        if (receivable < paid) {
            $(this).find('td:eq(9) input:text').val(receivable)
        }
        else {
            $(this).find('td:eq(10)').text(receivable - paid);
        }
        totalReceivable += parseFloat($(this).find('td:eq(9) input:text').val());
        TotalBadDebt += parseFloat($(this).find('td').eq(10).text());
    });
    $('#txtTotalReceivable').val(totalReceivable);
    $('#txtTotalBadDebt').val(TotalBadDebt);
}
function IPD_BillPayment(elem) {
    if (confirm('Are you sure to Submit')) {
        if ($('input[type=file]').val() == '') {
            alert('Please Choose File')
            return
        }
        $(elem).addClass('button-loading');
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
        var isDebtRemark = 0;
        $("#tblSelectBillingInfo tbody tr").each(function () {
            if (parseFloat($(this).find('td').eq(10).text()) > 0 && $(this).find('td:eq(11) input:text').val() == '') {
                isDebtRemark++;
                $(this).find('td:eq(11) input:text').css(' border-color', 'red');
            }
        });

        if (isDebtRemark > 0) {
            alert('Please Provide Bad Debt Type, when Bad Debt Amount > 0')
            return
        }
        var url = config.baseUrl + "/api/Corporate/UploadIPD_BillPayment";
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

                'hasfile': ($('#imgFile').attr('src').length > 10) ? 'Y' : 'N',
                'fileType': ($('#uploadFile').val().split('.').pop() == 'pdf') ? 'application/pdf' : 'Image',
                'fileExtention': $('#uploadFile').val().split('.').pop(),
                'Base64String': $('#imgFile').attr('src'),

                'Logic': 'PayBill'
            });
        });
        var UploadDocumentInfo = new XMLHttpRequest();
        var data = new FormData();
        data.append('obj', JSON.stringify(objBO));
        data.append('ImageByte', objBO[0].Base64String);
        UploadDocumentInfo.onreadystatechange = function () {
            if (UploadDocumentInfo.status) {
                if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                    var json = JSON.parse(UploadDocumentInfo.responseText);
                    if (json.includes('Success')) {
                        var date = new Date();
                        alert('Successfully Uploaded..!');
                        //var FilePath = json.split('|')[1] + "?v=" + date.getMilliseconds();
                        //window.open(FilePath, '_blank');                       
                        $('#txtReferenceNo').val('');
                        $('#txtTotalReceivable').val('');
                        $('#txtTotalBadDebt').val('');
                        $('#txtRemark').val('');
                        $('#tblSelectBillingInfo tbody').empty();
                        $('#tblBillingInfo tbody input:checkbox').prop('checked', false);
                        $('.colnospace select').prop('selectedIndex', '0').trigger('select2.change()');
                        $(elem).removeClass('button-loading');
                    }
                    else {
                        alert(json);
                        $(elem).removeClass('button-loading');
                    }
                }
            }
        }
        UploadDocumentInfo.open('POST', url, true);
        UploadDocumentInfo.send(data);
    }
}
function readURL(elem) {
    if (elem.files && elem.files[0]) {
        var ext = $(elem).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['jpg', 'png', 'pdf']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgFile').removeAttr('src', '');
            $('#imgFile').attr('src', e.target.result);
        }
        reader.readAsDataURL(elem.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $(elem).get(0).files;
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