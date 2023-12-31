﻿
$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Medicine Billing Info').show();
    FillCurrentDate('txtFilterFrom')
    FillCurrentDate('txtFilterTo')
    CloseSidebar();
    $('#tblIPDInfo thead').on('change', 'input[type=checkbox]', function () {
        var isChk = $(this).is(':checked');
        if (isChk)
            $(this).parents('table').find('tbody').find('input[type=checkbox]').prop('checked', true);
        else
            $(this).parents('table').find('tbody').find('input[type=checkbox]').prop('checked', false);
    });
    $('#txtSearchIPDNO').val($('#tblAdviceHeader tr:eq(0) td:eq(9)').text());
});
function IPDSalebyIPDNo() {
    $('#tblIPDInfo tbody').empty();
    $('#div#skill .circle').show();
    if ($('#txtSearchIPDNO').val() == '') {
        alert('Please Provide IPD No.');
        return
    }
    var url = config.baseUrl + "/api/Pharmacy/Hospital_Queries";
    var objBO = {};
    objBO.unit_id = '-';
    objBO.uhid = '-';
    objBO.prm_1 = $('#txtSearchIPDNO').val();
    objBO.prm_2 = $('#ddlFilterBy option:selected').text();
    objBO.prm_3 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "SaleByIpdNoNewHIS";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.Msg.includes('Success')) {

                var tbody = "";
                var count = 0;
                var countName = "";
                var TAmount = 0;
                var TTAmount = 0;
                var tCashAmount = 0;
                var temp = "";
                var temp1 = "";
                var indent_date = data.ResultSet.Table[0].indent_date;
                var totalLength = data.ResultSet.Table.length;
                if (Object.keys(data.ResultSet).length > 0) {
                    if (Object.keys(data.ResultSet.Table).length > 0) {
                        $.each(data.ResultSet.Table, function (key, val) {
                            count++;
                            if (indent_date != val.indent_date) {
                                tbody += "<tr style='background:#afdfc9'>";
                                tbody += "<td colspan='9' class='text-right'><b>Total</b></td>";
                                tbody += "<td class='text-right'><b>" + TTAmount.toFixed(2) + "</b></td>";
                                tbody += "<td colspan='5'></td>";
                                tbody += "</tr>";
                                TTAmount = 0;
                                indent_date = val.indent_date
                            }
                            TTAmount += val.amount;
                            if (temp != val.IPDNo) {
                                tbody += "<tr style='background:#edd591'>";
                                tbody += "<td colspan='15'><b>IPD No. :</b> " + val.IPDNo + ', <b>Patient Name : </b>' + val.pt_name + "</td>";
                                tbody += "</tr>";
                                temp = val.IPDNo;
                            }
                            if (temp1 != val.indent_date) {
                                tbody += "<tr style='background:#ddd'>";
                                tbody += "<td colspan='15'>" + val.indent_date + "</td>";
                                tbody += "</tr>";
                                temp1 = val.indent_date;
                            }

                            TAmount += val.amount;

                            if (val.his_push_flag == 'Y') {
                                tbody += "<tr style='background:#00a66961'>";
                            }
                            else {
                                tbody += "<tr>";
                            }

                            tbody += "<td>" + count + "</td>";
                            tbody += "<td>" + val.indent_no + "</td>";
                            tbody += "<td>" + val.ItemId + "</td>";
                            tbody += "<td>" + val.ItemName + "</td>";
                            tbody += "<td class='text-right'>" + val.ItemCount + "</td>";
                            tbody += "<td><a class='billrecpt' href=" + config.documentServerUrl + "/IPD/Print/SalesBill?InvoiceNo=" + val.sale_inv_no + "' target='_blank'>" + val.sale_inv_no + "</a></td>";
                            tbody += "<td class='text-right'>" + val.saleCount + "</td>";
                            tbody += "<td class='text-right'>" + val.Total + "</td>";
                            tbody += "<td class='text-right'>" + val.discount + "</td>";
                            tbody += "<td class='text-right'>" + val.amount + "</td>";
                            tbody += "<td>" + val.pay_mode + "</td>";
                            tbody += "<td>" + val.card_no + "</td>";
                            if (val.his_push_flag == "N")
                                tbody += "<td><input type='checkbox'/></td>";
                            else
                                tbody += "<td></td>";
                            tbody += "</tr>";
                            if (count == totalLength) {
                                tbody += "<tr style='background:#afdfc9'>";
                                tbody += "<td colspan='9' class='text-right'><b>Total</b></td>";
                                tbody += "<td class='text-right'><b>" + TTAmount.toFixed(2) + "</b></td>";
                                tbody += "<td colspan='5'></td>";
                                tbody += "</tr>";
                                TTAmount = 0;
                            }
                        });

                    }

                }
                $('#tblIPDInfo tbody').append(tbody);
            }
            else {
                alert(data.Msg);
            }
        },
        complete: function () {
            $('#div#skill .circle').hide();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DownloadExcel() {
    var url = config.baseUrl + "/api/Pharmacy/Pharmacy_Queries";
    var objBO = {};
    objBO.IPDNo = $('#txtSearchIPDNO').val();
    objBO.from = $('#txtFilterFrom').val();
    objBO.to = $('#txtFilterTo').val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.Logic = "GetBillInfoForBetweenDate";
    objBO.ReportType = 'Excel';
    Global_DownloadExcel(url, objBO, "BillInfoBetweenDate.xlsx");
}
function ItemInsert() {
    var url = config.baseUrl + "/api/IPDNursingService/Pharmacy_InsertBillInfo";
    var objBooking = {};
    var objRateList = [];
    $('#tblIPDInfo tbody tr').each(function () {
        if ($(this).find('td:eq(12)').find('input[type=checkbox]').is(':checked')) {
            objRateList.push({
                'AutoId': 0,
                'TnxId': $(this).find('td:eq(5)').find('a').text(),
                'RateListId': '-',
                'CatId': "PharmacyItems",
                'ItemId': $(this).find('td:eq(2)').text(),
                'RateListName': $(this).find('td:eq(1)').text(),
                'ItemSection': '-',
                'IsPackage': 0,
                'IsRateEditable': 'N',
                'IsPatientPayable': 'N',
                'IsDiscountable': 'N',
                'mrp_rate': $(this).find('td:eq(7)').text(),
                'panel_rate': $(this).find('td:eq(7)').text(),
                'panel_discount': $(this).find('td:eq(8)').text(),
                'qty': 1,
                'adl_disc_perc': 0,
                'adl_disc_amount': 0,
                'net_amount': $(this).find('td:eq(9)').text(),
                'IsUrgent': 'N',
                'Remark': '-',
                'TaxRate': 0,
                'TaxAmt': 0
            });
        }
    });
    objBooking.hosp_id = Active.HospId;
    objBooking.DoctorId = _doctorId;
    objBooking.IPDNo = _IPDNo;
    objBooking.ipAddress = '-';
    objBooking.login_id = Active.userId;
    objBooking.BillingRole = 'Billing';
    objBooking.Logic = "Med-Item";
    var MasterObject = {};
    MasterObject.objBooking = objBooking;
    MasterObject.objRateList = objRateList;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(MasterObject),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        traditional: true,
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                $('#tblIPDInfo tbody').find('input[type=checkbox]').prop('checked', false);
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