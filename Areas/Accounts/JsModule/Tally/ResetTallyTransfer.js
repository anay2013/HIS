$(document).ready(function () {
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    $('#tblTallyVoucher tbody').on('click', 'button[title=sync]', function () {
        if (confirm('are you sure?')) {
            var VoucherNo = $(this).closest('tr').find('td:eq(4)').text();
            UpdateVoucherForSync(VoucherNo, $(this).closest('tr'));
        }
    });
    $('#tblTallyVoucher tbody').on('mouseenter', 'button[title=status]', function () {
        if ($(this).is('.btn-danger')) {
            var x = $(this).position();
            var l = x.left;
            var t = x.top;
            $('.msg').text($(this).data('msg'));
            $('.msg').css({ top: t + "px" });
            $('.msg').hide();
            $('.msg').toggle('show');
        }
        else {
            $('.msg').hide();
        }

    });
});
function VoucherDetail() {
    var url = config.baseUrl + "/api/Account/Tally_ViewLedgerInfo";
    var objBO = {};
    objBO.UnitId = Active.unitId;
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.ledgerId = $('#ddlLedgerName').val();
    objBO.Logic = "Tally:VouchersList";
    objBO.VoucherNo = "-";
    objBO.LoginId = Active.userId;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#tblTallyVoucher tbody').empty();
            $('#txtRemain').text(0);
            var htmldata = "";
            var count = 0;
            $.each(data.ResultSet.Table, function (key, val) {
                count++;
                htmldata += '<tr>';

                htmldata += '<td>' + count + '</td>';
                htmldata += '<td><input type="button" id="' + val.voucher_no + '" value="Detail" class="btn btn-info btn-block" onclick="showVoucherPopUp(this.id);" /></td>';
                htmldata += '<td>' + val.vch_date + '</td>';
                htmldata += '<td>' + val.vch_type + '</td>';
                htmldata += '<td>' + val.voucher_no + '</td>';
                htmldata += '<td>' + val.Credit_Ledger + '</td>';
                htmldata += '<td>' + val.amount + '</td>';
                htmldata += '<td>';
                var checked = (val.isTallySync == "Y") ? 'checked' : '-';
                //if (val.isTallySync == "Y")
                //htmldata += '<td><button title="sync" class="btn-flat btn-success">Reset</button></td>';
                htmldata += "<label class='switch'>" +
                    "<input type='checkbox' onchange=UpdateVoucherForSync(this) class='IsActive'  " + checked + ">" +
                    "<span class='slider round'></span>" +
                    "</label>";
                htmldata += '</td>';
                htmldata += '<td>';
                htmldata += '<button title="status" class="btn-flat btn-default">-</button>';
                htmldata += '</td>';
                htmldata += '</tr>';
            });
            $('#txtTotal').text(count);
            $('#tblTallyVoucher tbody').append(htmldata);
        },
        error: function (data) {
            alert('Error..!');
        },
    });
}
function UpdateVoucherForSync(elem) {
    var url = config.baseUrl + "/api/Account/Tally_ViewLedgerInfo";
    var objBO = {};
    objBO.VoucherNo = $(elem).closest('tr').find('td:eq(4)').text();
    objBO.LoginId = Active.userId;
    objBO.Logic = 'Tally:UpdateVoucherForSync';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            VoucherDetail()
        },
        error: function (data) {
            alert('Error..!');
        },
    });
}
function showVoucherPopUp(voucherNo) {

    if (voucherNo.length > 2) {
        $('#elVoucherNo').html(voucherNo);
        VoucherPopUp(voucherNo);
    }
    $('#myModal').modal('show');
}
function VoucherPopUp(voucherNo) {
    var url = config.baseUrl + "/api/Account/AC_VoucherDetail";
    var objBO = {};
    objBO.UnitId = Active.unitId;
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.ledgerId = $('#ddlLedgerName').val();
    objBO.Logic = "VoucherFullDetail";
    objBO.VoucherNo = voucherNo;
    objBO.LoginId = Active.userId;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $("#elVoucherDetail").html(data);
        }
    });
}