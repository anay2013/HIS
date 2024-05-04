var masterkey = "";
var BookedQty = "";
var txtqty = "";
var itemcode = "";
$(document).ready(function () {
    $('#tblMedicineReturn tbody').on('click', '.selectrow', function () {
        var itemid = $(this).closest('tr').find('td:eq(0)').text();
        GetIPDNoWiseDetails(itemid);
    });
    $('#tblMedicineReturnDetalis tbody').on('click', '#btnAdd', function () {
        itemcode = $(this).closest('tr').find('td:eq(0)').text();
        masterkey = $(this).closest('tr').find('td:eq(2)').text();
        BookedQty = parseFloat($(this).closest('tr').find('td:eq(6)').text());
        txtqty = parseFloat($(this).closest('tr').find('td:eq(7) input#txtqty').val());
        SubmitQtystatic(masterkey, BookedQty, txtqty, itemcode);
    });
    $('#tblStoreData tbody').on('click', '.deleteRow', function () {
        $(this).closest('tr').remove();
        var totalQtyy = 0;
        $("#tblStoreData tbody tr").each(function () {
            var qtyStringg = parseFloat($(this).find("td:eq(2)").text()) || 0;
            totalQtyy += qtyStringg;
        });
        $("#txttotalamount").text(totalQtyy) || 0;
    });
});
function GetIPDNoWiseData() {
    $('#tblMedicineReturn tbody').empty();
    var url = config.baseUrl + "/api/Pharmacy/Hospital_Queries";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.uhid = '-';
    objBO.prm_1 = $('#txtIPDNo').val();
    objBO.prm_2 = $('#ddlPayMode').val();;
    objBO.prm_3 = '-';
    objBO.login_id = '-';
    objBO.Logic = "IPD:ProductsUsed";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width:20%'>" + val.item_id + "</td>";
                        tbody += "<td style='width:50%'>" + val.item_name + "</td>";
                        tbody += "<td style='width:20%;text-align:center'>" + val.SoldQty + "</td>";
                        tbody += "<td style='width:5%'><button class='btn-warning selectrow' style='border:none;height:20px;margin-bottom:3px;border-radius:3px'><i class='fa fa-arrow-right'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblMedicineReturn tbody').append(tbody);
                    IPDNOWiseDetails();

                }
                else {
                    IPDNOWiseDetails();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetIPDNoWiseDetails(itemid) {
    $('#tblMedicineReturnDetalis tbody').empty();
    var url = config.baseUrl + "/api/Pharmacy/Hospital_Queries";
    var objBO = {};
    objBO.unit_id = 'MS-H0085';
    objBO.uhid = '-';
    objBO.prm_1 = $('#txtIPDNo').val();
    objBO.prm_2 = itemid;
    objBO.prm_3 = $('#ddlPayMode').val();
    objBO.login_id = '-';
    objBO.Logic = "IPD:ProductsIssuedInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width:20%'hidden>" + val.item_id + "</td>";
                        tbody += "<td style='width:20%' hidden>" + val.item_name + "</td>";
                        tbody += "<td style='width:20%'>" + val.master_key_id + "</td>";
                        tbody += "<td style='width:20%'>" + val.batch_no + "</td>";
                        tbody += "<td style='width:20%'>" + val.exp_date + "</td>";
                        tbody += "<td style='width:15%'>" + val.pay_mode + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.SoldQty + "</td>";
                        tbody += "<td style='text-align:center;width:5%'><input type='text' style='height:20px; width:70px; margin-bottom:2px; background:#d9d4d4;text-align:center;;' id='txtqty'></td>";
                        tbody += "<td style='width:10%'><button class='btn-success' id='btnAdd' style='border:none;height:20px;margin-bottom:3px;'>ADD</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblMedicineReturnDetalis tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SubmitQtystatic(masterkey, BookedQty, txtqty, itemcode) {
    debugger
    var totalQty1 = 0;
    var totalQty = 0;
    if (txtqty <= 0 || txtqty === "-") {
        alert("Enter a valid Qty.");
        return;
    }
    $("#txtmasterkey").val(masterkey);
    $("#tblStoreData tbody tr").each(function () {
        if ($(this).find('td:eq(1)').text() == masterkey) {
            totalQty += parseInt($(this).find('td:eq(2)').text())
        }
    });
    if ((totalQty + txtqty) > BookedQty) {
        alert("Enter Qty should be less than or Equal to the Qty .");
        return;
    }
    else {
        var tbody = "";
        tbody += "<tr>";
        tbody += "<td style='width:20%' hidden>" + itemcode + "</td>";
        tbody += "<td style='width:20%'>" + masterkey + "</td>";
        tbody += "<td style='width:10%;text-align:center'>" + txtqty + "</td>";
        tbody += '<td style="width:10%; text-align:center"><button class="btn btn-danger deleteRow" style="border:none; height:18px; margin-bottom:3px;"><i class="fa fa-close" style="font-size:17px"></i></button></td>';
        tbody += "</tr>";
        $("#tblStoreData tbody").append(tbody);
        $("#tblStoreData tbody tr").each(function () {
            var qtyString = parseFloat($(this).find("td:eq(2)").text()) || 0;
            totalQty1 += qtyString;
        });
        $("#txttotalamount").text(totalQty1) || 0;
        return;
    }
}
function FinalSubmitData() {
    if (confirm('Are you sure to Submit Data?')) {
        var url = config.baseUrl + "/api/Pharmacy/InsertUpdateMedicineReturn";
        var objBO = [];
        $('#tblStoreData tbody tr').each(function () {
            objBO.push({
                'unit_id': 'MS-H0085',
                'SearchBy': $('#txtIPDNo').val(),
                'logic': '-',
                'old_SaleInvno': '-',
                'item_id': $(this).find('td:eq(0)').text(),
                'master_key_id': $(this).find('td:eq(1)').text(),
                'RetQty': $(this).find('td:eq(2)').text(),
                'payMode': $('#ddlPayMode').val(),
                'computer_id': '-',
                'counter_id': '-',
                'login_id': Active.userId
            });
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                    $('#tblStoreData tbody').empty();
                    $('#tblMedicineReturn tbody').empty();
                    $('#tblMedicineReturnDetalis tbody').empty();
                    $("#txttotalamount").text('0');
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
    else {
        alert(" Submit Cancel ");
    }
}
function IPDNOWiseDetails() {
    debugger
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1990-01-01';
    objBO.to = '1990-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PatientDetail';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $("#txtIpdno").text(data.ResultSet.Table[0].IPDNo);
                        $("#txtname").text(data.ResultSet.Table[0].patient_name);
                        $("#txtuhid").text(data.ResultSet.Table[0].UHID);
                        $("#txtpanel").text(data.ResultSet.Table[0].PanelName);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}  