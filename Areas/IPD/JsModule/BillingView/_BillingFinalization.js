var _BillNo = "";
$(document).ready(function () {
    $('#UnMappedSection').hide();
    SummarisedBilling();
    $('#dash-dynamic-section').find('label.title').text('Billing Finalization').show();
    $('select').select2();
});
function SummarisedBilling() {
    $('#UnMappedSection').hide();
    $('#tblPaymentInfo tbody').empty();
    $('#tblGenerateBillInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = $('#tblAdviceHeader tbody tr:last td:eq(5)').text();
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'SummarisedBillWithAdvances';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtGrossAmt').val(val.GrossAmount.toFixed(2));
                        $('#txtPDiscount').val(val.panel_discount.toFixed(2));
                        $('#txtAdlDiscount').val(val.adl_discount.toFixed(2));
                        $('#txtDiscount').val(val.Discount.toFixed(2));
                        $('#txtTax').val(val.Tax.toFixed(2));
                        $('#txtNetPayable').val(val.NetPayable.toFixed(2));
                        $('#txtAdvanceAmt').val(val.AdvanceAmount.toFixed(2));
                        $('#txtBalanceAmt').val(val.BalanceAmount.toFixed(2));
                        $('#txtApprovalAmt').val(val.ApprovalAmount.toFixed(2));
                    });
                }
            } if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table4).length) {
                    $.each(data.ResultSet.Table4, function (key, val) {
                        if (parseFloat(val.UnMappedAmount) > 0) {
                            $('#UnMappedSection').show();
                            $('#txtUnMappedAmt').val(val.UnMappedAmount.toFixed(2));
                        }
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbody1 = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        count++;
                        _BillNo = val.BillNo;
                        if (val.IsBillClosed == 'Y')
                            $('#btnSaveApproval,#btnCloseBill').prop('disabled', true);
                        else
                            $('#btnSaveApproval,#btnCloseBill').prop('disabled', false);

                        $('#txtPanelApprovalAmount').val(val.PanelApprovedAmount);
                        $('#txtCoPayment').val(val.CoPayAmount);
                        if (val.IsBillClosed == 'Y')
                            tbody1 += "<tr class='bg-success'>";
                        else
                            tbody1 += "<tr>";

                        tbody1 += "<td>" + val.BillingType + "</td>";
                        tbody1 += "<td>" + val.BillNo + "</td>";
                        tbody1 += "<td class='text-right'>" + val.TotalAmount + "</td>";
                        tbody1 += "<td class='text-right'>" + val.TotalDiscount + "</td>";
                        tbody1 += "<td class='text-right'>" + val.NetAmount + "</td>";
                        tbody1 += "<td class='text-right'>" + val.roundoff + "</td>";
                        tbody1 += "<td class='text-right'>" + val.Tax + "</td>";
                        tbody1 += "<td class='text-right'>" + val.NetPayable + "</td>";
                        tbody1 += "<td class='text-right'>" + val.Received + "</td>";
                        tbody1 += "<td class='text-right'>" + val.PanelApprovedAmount + "</td>";
                        tbody1 += "<td class='text-right'>" + val.BalanceAmount + "</td>";

                        $('#txtRemark').val(val.Remark);
                    });
                    $('#tblGenerateBillInfo tbody').append(tbody1);
                }
            }

            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#txtPanelApprovalAmount').val(val.ApprovalAmount);
                        $('#txtCoPayment').val(val.CoPayAmount);
                        $('#txtTPADiscount').val(val.Discount);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table3).length) {
                    $.each(data.ResultSet.Table3, function (key, val) {
                        $('.BillStatus span').text(val.BillStatus);
                        $('.BillStatus i').addClass((val.BillStatus == 'Mismached') ? 'fa-ban' : 'fa-check-circle');
                        $('.BillStatus').addClass(val.BillStatus);
                        (val.BillStatus == 'Mismached') ? $('#btnCloseBill').addClass('disabled') : $('#btnCloseBill').removeClass('disabled');
                        (val.BillStatus == 'Mismached') ? $('#btnGenerateBill').html('<i class="fa fa-sign-in">&nbsp;</i>Update Bill') : $('#btnGenerateBill').html('<i class="fa fa-sign-in">&nbsp;</i>Generate Bill');
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GenerateBill() {
    if (confirm('Are you sure to Submit?')) {
        if (parseFloat($('#txtAdlDiscount').val()) > 0 && $('#txtRemark').val() == '') {
            alert('Please Provide Remark')
            $('#txtRemark').focus()
            return
        }
        var url = config.baseUrl + "/api/IPDNursingService/IPD_GenerateBill";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.BillingType = $('input[name=billing-type]:checked').val();
        objBO.Remark = $('#txtRemark').val();
        objBO.login_id = Active.userId;
        objBO.Logic = "Generate-Bill";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    SummarisedBilling();
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function MarkDischarge() {
    if (confirm('Are you sure to Submit?')) {
        var url = config.baseUrl + "/api/IPDNursingService/IPD_GenerateBill";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.BillingType = "-";
        objBO.Remark = "";
        objBO.login_id = Active.userId;
        objBO.Logic = "MarkDischarged";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                alert(data);
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function SaveCloseApproval(logic) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDNursingService/IPD_GenerateBill";
        var objBO = {};
        objBO.BillNo = _BillNo;
        objBO.IPDNo = _IPDNo;
        objBO.BillingType = '-';
        objBO.ApprAmount = $('#txtPanelApprovalAmount').val();
        objBO.CoPayAmount = $('#txtCoPayment').val();
        objBO.DiscountAmount = 0;
        objBO.Remark = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = logic;
        var NetPayable = $('#tblGenerateBillInfo tbody').find('tr:eq(0)').find('td:last').text();
        //if (parseFloat(objBO.ApprAmount) + parseFloat(objBO.CoPayAmount) > parseFloat(NetPayable)) {
        //    alert('Not Allowed.');
        //    return
        //}
        $('#btnCloseBill').addClass('button-loading');
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    SummarisedBilling();
                    $('#btnCloseBill').removeClass('button-loading');
                }
                else {
                    alert(data);
                    $('#btnCloseBill').removeClass('button-loading');
                }
            },
            error: function (response) {
                alert('Server Error...!');
                $('#btnCloseBill').removeClass('button-loading');
            }
        });
    }
}
function ApplyPanelDiscount(elem) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDNursingService/IPD_GenerateBill";
        var objBO = {};
        objBO.BillNo = _BillNo;
        objBO.IPDNo = _IPDNo;
        objBO.BillingType = '-';
        objBO.ApprAmount = $('#txtPanelApprovalAmount').val();
        objBO.CoPayAmount = $('#txtCoPayment').val();
        objBO.DiscountAmount = 0;
        objBO.Remark = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'ApplyPanelDiscount';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    SummarisedBilling();
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function MapIPDAmount() {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDNursingService/IPD_GenerateBill";
        var objBO = {};
        objBO.BillNo = $('#tblAdviceHeader tbody tr:last td:eq(5)').text();
        objBO.IPDNo = _IPDNo;
        objBO.BillingType = '-';
        objBO.ApprAmount = 0;
        objBO.CoPayAmount = 0;
        objBO.DiscountAmount = 0;
        objBO.Remark = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'MapIPDAmount';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    $('#modalAdvancePayment').modal('hide');
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}