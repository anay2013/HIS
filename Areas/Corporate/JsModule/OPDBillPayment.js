var totalReceivable = 0;
var totalTDSReceivable = 0;
var totalSelectedCount = 0;
$(document).ready(function () {
    $('select').select2();
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    FillCurrentDate('txtPayDate')
    GetPanel('PanelInfo2')
});
function GetPanel(logicvalue) {
    debugger
    $('#ddlPanel').empty().append($('<option></option>').val('Select').html('Select'));
    var url = config.baseUrl + "/api/Corporate/opd_ReceivableQueries";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.IPDNo = '-';
    objBO.BillNo = '-';
    objBO.ReceiptNo = '-';
    objBO.Prm1 = $("#ddlReport option:selected").val()
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.login_id = Active.userId;
    objBO.Logic = logicvalue;
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
function BalanceInfo() {
    $('#tblSelectBillingInfo tbody').empty();
    $('#tblBillingInfo tbody').empty();
    if ($('#ddlPanel option:selected').text() == 'Select') {
        alert('Please Select Panel')
        return
    }
    if ($('#ddlReport option:selected').val() == 'Select') {
        alert('Please Select Report')
        return
    }
    var url = config.baseUrl + "/api/Corporate/OPD_ReceivableQueries";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.BillNo = '-';
    objBO.ReceiptNo = '-';
    objBO.Prm1 = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'OPD_Balance:' + $('#ddlReport option:selected').val();
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
                    var reporttype = 'OPD_Balance:' + $('#ddlReport option:selected').val();
                    if (objBO.Logic == reporttype) {
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
                            tbody += '<td hidden>' + val.PanelId + '</td>';
                            tbody += '<td>' + val.PanelName + '</td>';
                            tbody += '<td>' + val.patient_name + '</td>';
                            tbody += '<td>' + val.BillDate + '</td>';
                            tbody += '<td>' + val.NetPayable + '</td>';
                            tbody += '</tr>';
                        });
                        $("#tblBillingInfo tbody").append(tbody);
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
    var PanelId = $(elem).closest('tr').find('td:eq(3)').text();
    var PanelName = $(elem).closest('tr').find('td:eq(4)').text();
    var patient_name = $(elem).closest('tr').find('td:eq(5)').text();
    var BillDate = $(elem).closest('tr').find('td:eq(6)').text();
    var NetPayable = $(elem).closest('tr').find('td:eq(7)').text();
    var isCheck = $(elem).is(':checked');
    if (isCheck) {
        var tbody = "";
        tbody += '<tr>';
        tbody += '<td><button onclick=removeSelectedBill(this) class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button></td>';
        tbody += '<td>' + ClaimNo + '</td>';
        tbody += '<td>' + BillNo + '</td>';
        tbody += '<td hidden>' + PanelId + '</td>';
        tbody += '<td>' + PanelName + '</td>';
        tbody += '<td>' + patient_name + '</td>';
        tbody += '<td>' + BillDate + '</td>';
        tbody += '<td>' + NetPayable + '</td>';
        tbody += '<td><input type="text" onkeyup=calculate() value="' + NetPayable + '"/></td>';
        tbody += '<td>0</td>';
        tbody += '<td><input type="text"/></td>';
        //tbody += '<td class="hide">' + InsuranceId + '</td>';
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
        totalReceivable += parseFloat($(this).find('td').eq(7).text());
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
            totalReceivable += parseFloat($(this).find('td').eq(7).text());
        });
        $('#txtTotalReceivable').val(totalReceivable);
    }
}
function calculate() {
    var receivable = 0;
    var paid = 0;
    var totalReceivable = 0;
    var TotalBadDebt = 0;
    $("#tblSelectBillingInfo tbody tr").each(function () {
        receivable = parseFloat($(this).find('td').eq(7).text());
        paid = parseFloat($(this).find('td:eq(8) input:text').val());
        if (receivable < paid) {
            $(this).find('td:eq(8) input:text').val(receivable)
            $(this).find('td:eq(9)').text(TotalBadDebt)
        }
        else {
            $(this).find('td:eq(9)').text(receivable - paid);

        }
        totalReceivable += parseFloat($(this).find('td:eq(8) input:text').val());
        TotalBadDebt += parseFloat($(this).find('td').eq(9).text());
    });
    $('#txtTotalReceivable').val(totalReceivable);
    $('#txtTotalBadDebt').val(TotalBadDebt);
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
        var isDebtRemark = 0;
        $("#tblSelectBillingInfo tbody tr").each(function () {
            if (parseFloat($(this).find('td').eq(8).text()) > 0 && $(this).find('td:eq(9) input:text').val() == '') {
                isDebtRemark++;
                $(this).find('td:eq(9) input:text').css(' border-color', 'red');
            }
        });

        if (isDebtRemark > 0) {
            alert('Please Provide Bad Debt Type, when Bad Debt Amount > 0')
            return
        }

        var url = config.baseUrl + "/api/Corporate/UploadpOPD_BillPayment";
        var objBO = [];
        $("#tblSelectBillingInfo tbody tr").each(function () {
            debugger
            objBO.push({
                'HospId': Active.HospId,
                'TPA_PanelId': $('#ddlPanel option:selected').val(),
                'PayMode': $('#ddlPayMode option:selected').text(),
                'BankName': $('#ddlBankName option:selected').text(),
                'Remark': $('#txtRemark').val(),
                'RefNo': $('#txtReferenceNo').val(),
                'Pay_ChequeDate': $('#txtPayDate').val(),
                'TotalBillAmount': $('#txtTotalReceivable').val(),
                'InsuranceProvId': $(this).find('td:eq(3)').text(),
                'PanelId': $(this).find('td:eq(3)').text(),
                'BillNo': $(this).find('td:eq(2)').text(),
                'IPDNo': $(this).find('td:eq(2)').text(),
                'Recievable': $(this).find('td:eq(7)').text(),
                'PaidAmount': $(this).find('td:eq(8) input:text').val(),
                'TDSAmount': 0,
                'BadDebt': $(this).find('td:eq(9)').text(),
                'BadDebtType': $(this).find('td:eq(10) input:text').val(),
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
                        Clear();
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
    $('#btnPayment').css('pointer-events', 'none');
}
function BalanceInfoBillWise() {
    $('#tblSelectBillingInfo tbody').empty();
    $('#tblBillingInfo tbody').empty();
    if ($('#ddlPanel option:selected').text() == 'Select') {
        alert('Please Select Panel')
        return
    }
    var url = config.baseUrl + "/api/Corporate/OPD_ReceivableQueries";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.BillNo = '-';
    objBO.ReceiptNo = '-';
    objBO.Prm1 = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'OPD_Balance:BillWise';
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
                    var reporttype = 'OPD_Balance:BillWise';
                    if (objBO.Logic == reporttype) {
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
                            tbody += '<td hidden>' + val.PanelId + '</td>';
                            tbody += '<td>' + val.PanelName + '</td>';
                            tbody += '<td>' + val.patient_name + '</td>';
                            tbody += '<td>' + val.BillDate + '</td>';
                            tbody += '<td>' + val.NetPayable + '</td>';
                            tbody += '</tr>';
                        });
                        $("#tblBillingInfo tbody").append(tbody);
                    }

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
