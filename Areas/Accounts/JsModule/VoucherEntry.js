$(document).ready(function () {
    FillCurrentDate("txtDate");
    var d = GetPreviousDate();
    document.getElementById("txtDate").min = d + 'T10:38'

});
var PageNameLogic = "";
$(function () {
    $('#txtPayTo').on('keyup', function () {
        var value = $(this).val();
        var patt = new RegExp(value, "i");

        $('#FillDataRight').find('tr').each(function () {
            if (!($(this).find('td').text().search(patt) >= 0)) {
                $(this).not('.myHead').hide();
            }
            if (($(this).find('td').text().search(patt) >= 0)) {
                $(this).show();
            }
        });
        $('#hfPayTo').val('');
    });
    $('#txtPayBy').on('keyup', function () {
        var value = $(this).val();
        var patt = new RegExp(value, "i");

        $('#FillDataRight').find('tr').each(function () {
            if (!($(this).find('td').text().search(patt) >= 0)) {
                $(this).not('.myHead').hide();
            }
            if (($(this).find('td').text().search(patt) >= 0)) {
                $(this).show();
            }
        });

        $('#hfPayBy').val('');
    });
    PageNameLogic = $('#txtVchType').val()
    BindOnloadData();
});
function BindOnloadData() {
    var url = config.baseUrl + "/api/Account/AC_GetVoucherInfo";
    var objBO = {};
    objBO.UnitId = Active.unitId;
    objBO.VoucherNo = "-";
    objBO.VchType = PageNameLogic;
    objBO.ledgerfor = "";
    objBO.From = $('#txtDate').val();
    objBO.LoginId = Active.userId;
    objBO.Logic = "ByDateAndVoucherType";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var temp = "";
            var str;
            if (data != '') {
                $("#FillData").empty().append('<thead style="background: #00a65a;color: #fff;"><tr><th width="10%">Date</th><th width="5%">Type</th><th width="35%">Ledger Name</th><th width="15%">Debit Amount</th><th width="15%">Credit Amount</th><th width="30%">Narration</th></tr></thead><tbody></tbody>');
                $.each(data.ResultSet.Table, function (key, val) {
                    if (temp != val.Voucher_No) {
                        str += "<tr>";
                        str += "<td colspan='6' style='text-align:left;background-color:#c4ddeb'>" + val.Voucher_No + "</td>";
                        str += "</tr>";
                        temp = val.Voucher_No
                    }
                    str += "<tr>";
                    str += "<td>" + val.vch_date + "</td>";
                    str += "<td>" + val.vch_type + "</td>";
                    str += "<td>" + val.ledgerName + "</td>";
                    str += "<td class='text-right'>" + val.DeditAmt + "</td>";
                    str += "<td class='text-right'>" + val.CreditAmt + "</td>";
                    str += "<td>" + val.narration + "</td>";
                    str += "</tr>";
                });
                $("#FillData").append(str)
            }
            else {
                $("#FillData").empty();
            }
        }
    });
}
function Bind_PayTo_PayBy(SearchType) {
    var rightTitle = "";
    if (SearchType == "Debit") {
        rightTitle = "Pay To";
    }
    else if (SearchType == "Credit") {
        rightTitle = "Pay By";
    }
    var url = config.baseUrl + "/api/Account/AC_GetVoucherInfo";
    var objBO = {};
    objBO.UnitId = Active.unitId;
    objBO.VoucherNo = "-";
    objBO.VchType = PageNameLogic;
    objBO.ledgerfor = SearchType;
    objBO.From = "1900-01-01";
    objBO.LoginId = Active.userId;
    objBO.Logic = "PickLedgerForVoucherEntry";

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    if (Object.keys(data.ResultSet.Table).length) {
                        $("#FillDataRight").empty()
                        $("#FillDataRight").append('<input type="text" style="width:100%" id="myInput" onkeyup="myFunction()" placeholder="Search for names..">')
                        $("#FillDataRight").append('<thead style="background: #00a65a;color: #fff;"><tr class="myHead"><th>' + rightTitle + '</th></tr></thead><tbody></tbody>');
                        $.each(data.ResultSet.Table, function (key, val) {
                            $("#FillDataRight").append('<tr>' +
                                '<td>' +
                                '<a href="javascript:void(0)" onclick="Set_Details(\'' + SearchType + '\',\'' + val.ld_code + '\',\'' + val.ld_name + '\');">' + val.ld_name + '</a>' +
                                '</td>' +
                                '</tr>');
                        });


                    }
                    $('#tblSaleVoucher tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Set_Details(SearchType, Code, Name) {
    $("#FillDataRight").empty();
    if (SearchType == "Debit") {
        $("#txtPayTo").val('').val(Name);
        $('#hfPayTo').val(Code);
    }
    else if (SearchType == "Credit") {
        $("#txtPayBy").val('').val(Name);
        $('#hfPayBy').val(Code);
    }
}
function BindCredit(e) {
    $('#txtCrAmount').val($(e).val());
}
function SubmitPayDetails() {
    var objPay = {};
    var url = config.baseUrl + "/api/Account/SubmitPayment";
    objPay.UnitId = Active.unitId;
    objPay.VchType = PageNameLogic;
    objPay.VchDate = $('#txtDate').val();
    objPay.PayTo_Code = $('#hfPayTo').val();
    objPay.PayBy_Code = $('#hfPayBy').val();

    objPay.DrCrAmount = $('#txtDrAmount').val();
    objPay.Narration = $('#txtNarration').val();
    objPay.LoginId = Active.userId;
    objPay.Logic = "ReturnCurrentDayRecord";
    objPay.GenFrom = "Web-UI";
    objPay.Division = 'Hospital';
    objPay.VoucherNo = '-';

    if (objPay.PayTo_Code == "") {
        alert("Please fill pay To");
        $('#txtPayTo').focus();
        return false;
    }
    else if (objPay.DrCrAmount == "") {
        alert("Please fill Debit Amount");
        $('#txtDrAmount').focus();
        return false;
    }
    else if (objPay.PayBy_Code == "") {
        alert("Please fill pay By");
        $('#txtPayBy').focus();
        return false;
    }
    else if (objPay.Narration == "") {
        alert("Please fill Narration");
        $('#txtNarration').focus();
        return false;
    }
    $.ajax({
                method: "POST",
                url: url,
                data: JSON.stringify(objPay),
                contentType: "application/json;charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    alert(data);
                    BindOnloadData();
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });
   
}
function ClearControl() {
    $("#FillDataRight").empty();
    $("#txtPayTo").val('');
    $('#hfPayTo').val('');
    $("#txtPayBy").val('');
    $('#hfPayBy').val('');
    $("#txtDrAmount").val('0');
    $("#txtCrAmount").val('0');
    $("#txtNarration").val('');
}