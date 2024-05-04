var selectedRow;

$(document).ready(function () {
    GetDataCancelSale();

    $("#txtpName").on('keyup', function () {
        var val = $(this).val().toLowerCase();
        $("#tblCancelSale tbody tr").each(function () {
            var text = $(this).text().toLowerCase();
            $(this).toggle(text.indexOf(val) > -1);
        });
    });

    $("#txtpno").on('keyup', function () {
        var val = $(this).val().toLowerCase();
        $("#tblCancelSale tbody tr").each(function () {
            var text = $(this).text().toLowerCase();
            $(this).toggle(text.indexOf(val) > -1);
        });
    });

});
function GetDataCancelSale() {
    $('#tblCancelSale tbody').empty();
    var url = config.PharmacyWebAPI_baseUrl + "/api/hospital/ipopqueries";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.card_no = '-';
    objBO.uhid = '-';
    objBO.IPOPNo = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.prm_1 = '-';
    objBO.Logic = 'IPD:EstimateNos';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            $.each(data.result.Table, function (key, val) {
                tbody += "<tr>";
                tbody += "<td>" + val.sale_inv_no + "</td>";
                tbody += "<td>" + val.ipdNo + "</td>";
                tbody += "<td>" + val.pt_name + "</td>";
                tbody += "<td class='text-right'>" + val.Nos + "</td>";
                tbody += "<td><button class='btn btn-warning btn-xs' onclick=selectRow(this);SetSelectedRow(this)><i class='fa fa-sign-in'></i></button></td>";
                tbody += "</tr>";
            });
            $('#tblCancelSale tbody').append(tbody);
            Reset();
            $("#txtpName").trigger('keyup');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SetSelectedRow(elem) {
    selectedRow = elem
    CancelSaleInfo();
}
function Reset() {
    $('#txtSaleInvNo').text('XXXXXXXXXXXX');
    $('#txtPatientName').text('XXXXXXXXXXXX');
    $('#txtIPOP').text('XXXXXXXXXXXX');
    $('#tblCancelSaleInfo tbody').empty();
}
function CancelSaleInfo() {
    debugger
    $('#tblCancelSaleInfo tbody').empty();
    var url = config.PharmacyWebAPI_baseUrl + "/api/sales/GetSalesInvoice_Info";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.Sales_Inv_No = $(selectedRow).closest('tr').find('td:eq(0)').text();
    objBO.order_no = '-';
    objBO.logic = 'SalesInvoice';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            $('#txtPatientName').text($(selectedRow).closest('tr').find('td:eq(2)').text());
            $('#txtIPOP').text($(selectedRow).closest('tr').find('td:eq(1)').text());
            $('#txtSaleInvNo').text($(selectedRow).closest('tr').find('td:eq(0)').text());
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
            $('#tblCancelSaleInfo tbody').append(tbody);
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
        objBO.unit_id = 'MS-H0085';
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
                GetDataCancelSale();
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}



