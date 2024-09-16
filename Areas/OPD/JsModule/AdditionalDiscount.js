var total = 0;
var amount = 0;

$(document).ready(function () {
    $('#ddlPayMode').prop({ 'selectedIndex': 0, 'disabled': true }).change();
    $('select').select2();
    $('#tblPayment tbody').on('change', 'input:checkbox', function () {
        amount = 0;
        total = 0;
        $('#tblPayment tbody').find('input:checkbox:checked').each(function () {
            $(this).closest('tr').addClass('item');
            amount += parseFloat($(this).closest('tr').find('td:eq(4)').text());
        });
        $('#tblPayment tbody').find('input:checkbox:not(:checked)').each(function () {
            $(this).closest('tr').find('td').remove();
            calc_total();
        });
        $('#txtTotal').val(amount)

    });
});
function Action() {
    $('input:radio').on('change', function () {
        var isCheck = $(this).is(':checked');
        var val = $(this).val();
        if (val == 'Cancelled') {
            $('#tblPayment tbody').find('input:checkbox').each(function () {
                $(this).prop({ 'checked': true, 'disabled': true }).change();
            })
            $('#ddlPayMode').prop({ 'selectedIndex': 0, 'disabled': true }).change();
        }
        if (val == 'Refunded') {
            $('#tblPayment tbody').find('input:checkbox:checked').each(function () {
                $(this).prop({ 'checked': false, 'disabled': false }).change();
            })
            $('#ddlPayMode').prop({ 'selectedIndex': 0, 'disabled': false }).change();
        }
    });

}
function GetAdditionalDiscount() {
    $('#tblDetails tbody').empty();
    $('#tblPayment tbody').empty();
    var url = config.baseUrl + "/api/Service/Opd_ServiceQueries";
    var objBO = {};
    objBO.prm_1 = $('#txtVisitNo').val();
    objBO.Logic = 'GetServicesRefund';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            console.log(data);
            if (data.ResultSet.Table.length > 0) {
                $('#txtVisitNo').attr('disabled', true);
                total = 0;
                amount = 0;
                $('#txtTotal').val(0);
                $('#txtVisitNo').val('');
                $('#txtVisitNo,textarea').val('');
                $('#txtVisitNo').val(objBO.prm_1);
                var tbody = "";
                $.each(data.ResultSet.Table, function (key, val) {
                    if (val.trnStatus == 'Refunded') {
                        tbody += "<tr style='background:#ffb8b8'>";
                    }
                    else {
                        tbody += "<tr>";
                    }
                    tbody += "<td>" + val.UHID + "</td>";
                    tbody += "<td>" + val.patient_name + "</td>";
                    tbody += "<td>" + val.DoctorName + "</td>";
                    tbody += "<td>" + val.gender + "</td>";
                    tbody += "<td>" + val.mobile_no + "</td>";
                    tbody += "<td>" + val.visitNo + "</td>";
                    tbody += "<td>" + val.VisitDate + "</td>";
                    tbody += "<td>" + val.PanelName + "</td>";
                    tbody += "<td>" + val.GrossAmount + "</td>";
                    tbody += "<td>" + val.discount + "</td>";
                    tbody += "<td>" + val.NetAmount + "</td>";
                    tbody += "<td>" + val.trnStatus + "</td>";
                    tbody += "</tr>";

                    //if (val.IsCredit == 0) {
                    //	$('#btnCancel').hide('slide', { diretion: 'right' }, 500, function () {
                    //		$('#btnRefund').show('slide', { direction: 'right' }, 500);
                    //	});
                    //	$('span.selection').find('span[aria-labelledby=select2-ddlPayMode-container]').show('slide', { diretion: 'left' }, 500);
                    //}
                    //else {
                    //	$('#btnCancel').show('slide', { diretion: 'right' }, 500, function () {
                    //		$('#btnRefund').hide('slide', { direction: 'right' }, 500);
                    //	});
                    //	$('span.selection').find('span[aria-labelledby=select2-ddlPayMode-container]').hide('slide', { diretion: 'left' }, 500);
                    //}
                    //if (val.trnStatus == 'Refunded') {
                    //	$('input[type=button]:not(#btnSearch)').prop('disabled', true).css('display', 'block');
                    //}
                    //else {
                    //	$('input[type=button]').prop('disabled', false);
                    //}
                });
                $('#tblDetails tbody').append(tbody);

                var html = "";
                $.each(data.ResultSet.Table1, function (key, val) {
                    if (val.trnStatus == 'Refunded') {
                        html += "<tr style='background:#ffb8b8'>";
                    }
                    else {
                        html += "<tr>";
                    }
                    html += "<td style='display:none'>" + val.ItemId + "</td>";
                    html += "<td>" + val.ItemName + "</td>";
                    html += "<td class='text-right'>" + val.GrossAmount + "</td>";
                    html += "<td class='text-right'>" + val.discount + "</td>";
                    html += "<td class='text-right'>" + val.NetAmount + "</td>";
                    if (val.trnStatus == "Refunded" || val.trnStatus == "Cancelled") {
                        html += "<td>-</td>";
                    }
                    else {
                        html += "<td></td>";
                    }
                    html += "</tr>";
                });
                $('#tblPayment tbody').append(html);
                calc_total();
            }
            else {
                //alert("Data Not Found..");
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Opd_AdditionalDiscount() {
    if (Validation()) {
        var url = config.baseUrl + "/api/Service/Opd_AdditionDiscount";
        var objBO = {};
        var item = [];
        objBO.hosp_id = Active.unitId;
        objBO.VisitNo = $('#txtVisitNo').val();
        objBO.CancelReason = $('#txtRemark').val();
        $('#tblPayment tbody tr.item').each(function () {
            var itemid = $(this).find('td:eq(0)').text();
            item.push(itemid);
        });
        objBO.ItemIdList = item.join(',');
        objBO.amount = $('#txtTotal').val();
        objBO.PayMode = 'CASH';
        objBO.CancellationType = $('input[name=type]:checked').val();
        objBO.IPAddress = '-';
        objBO.login_id = Active.userId;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            async: false,
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $('#txtVisitNo,textarea').val('');
                    GetAdditionalDiscount();
                    // $('#tblDetails tbody').empty();
                    //  $('#tblPayment tbody').empty();
                    ///  $('#txtTotal tbody').val(0);
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function Receipt(tnxid) {
    var url = config.documentServerUrl + "/Print/ServiceReceipt?TnxId=" + tnxid;
    window.open(url, '_blank');
}
function Validation() {
    var Total = parseFloat($('#txtTotal').val());
    var Remark = $('#txtRemark').val();
    var Panel = $('#tblDetails tbody tr').find('td:eq(7)').text();
    if (Remark == '') {
        $('#txtRemark').focus();
        alert('Please Provide Cancellation Remark..');
        return false;
    }
    return true;
}
function AdditionalDiscountList() {
    var url = config.baseUrl + "/api/Service/Opd_ServiceQueries";
    var objBO = {};
    objBO.prm_1 = $('#txtVisitNo').val();
    objBO.CouponCode = $('#txtAdditionalDiscount').val();
    objBO.Logic = 'AdditionalDiscItem';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            console.log(data);
            if (data.ResultSet.Table.length > 0) {
                $('#txtVisitNo').attr('disabled', true);
                var tbody = "";
                $.each(data.ResultSet.Table, function (key, val) {
                    tbody += "<tr>";
                    tbody += "<td style='display:none'>" + val.ItemId + "</td>";
                    tbody += "<td>" + val.ItemName + "</td>";
                    tbody += "<td class='text-right'>" + + Math.abs(val.GrossAmount) + "</td>";
                    tbody += "<td class='text-right'>" + val.discount + "</td>";
                    tbody += "<td class='text-right'>" + Math.abs(val.NetAmount) + "</td>";
                    tbody += "<td><input type='checkbox' checked  style='height: 15px;width: 15px;text-align:center;' /></td>";
                    tbody += "</tr>";
                });
                $('#tblPayment tbody').append(tbody);

                calc_total();
                checkValue();
                $('#btnAddList').prop('disabled', true);
            }
            else {
                alert("Data Not Found..");
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function calc_total() {
    let totalamount = 0;
    $("#tblPayment tbody tr").each(function () {
        let amount = parseFloat($(this).find('td:eq(4)').text());
        if (!isNaN(amount)) {
            totalamount += amount;
        }
    });
    $("#txttotalamout").text(totalamount.toFixed(0));
}
function checkValue() {
    amount = 0;
    var selectedItemIds = [];
    var itmid = "";
    $('#tblPayment tbody').find('input:checkbox:checked').each(function () {
        $(this).closest('tr').addClass('item');
        itmid = $(this).closest('tr').find('td:eq(0)').text()
        amount += parseFloat($(this).closest('tr').find('td:eq(4)').text());
        selectedItemIds.push(itmid);
    });
    $('#txtTotal').val(amount)
    var matchFound = false;
    $('#tblPayment tbody tr').each(function () {
        debugger
        var checkbox = $(this).find('input:checkbox');
        if (!checkbox.is(':checked')) {
            var listitmid = $(this).find('td:eq(0)').text();
            if (selectedItemIds.includes(listitmid)) {
                matchFound = true;
                return false;
            }
        }
    });
    if (matchFound) {
        $('#btnRefund').prop('disabled', true);
    }
    else {
        $('#btnRefund').prop('disabled', false);
    }
} 
