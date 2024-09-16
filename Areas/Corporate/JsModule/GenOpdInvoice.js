$(document).ready(function () {

    FillCurrentMonth('txtMonth');
    GetPreviousMonth();
});

function FillCurrentMonth(elementid) {
    var date = new Date();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    if (month < 10) month = "0" + month;
    var today = year + "-" + month;
    $("#" + elementid).attr("value", today);
}
function GetPreviousMonth() {
    var date = new Date();
    var month = date.getMonth();
    var year = date.getFullYear();
    month -= 0;
    if (month < 0) {
        month = 11;
        year -= 1;
    }
    if (month < 10) month = "0" + month;
    var previousMonth = year + "-" + month;
    $("#txtMonth").val(previousMonth);
}
function GetList() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Service/OPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-'
    objBO.UHID = '-'
    objBO.OpdNo = '-'
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '-';
    objBO.from = $('#txtMonth').val();
    objBO.to = '1999-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-'
    objBO.login_id = Active.userId;
    objBO.Logic = 'PanelSummaryForBill';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = ""; var totalamount = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        totalamount += val.BillAmount;
                        if (val.BillNo != null)
                            tbody += "<tr style='background:#cfe7cf'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td hidden>" + val.PanelId + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td style='text-align:center;width:15%;'>" + val.BillAmount.toFixed(0) + "</td>";
                        if (val.BillNo == null) {
                            tbody += "<td style='width:15%;text-align:center'><button class='btn-success' id='btnGen' onclick=GeneratedInvoiceOfMonth(this)>Gen Invoice</button></td>";
                            tbody += "<td style='width:15%;text-align:center'><button class='btnclass' id='btnbillno' onclick=BillInformationList('" + val.BillNo + "') disabled>" + val.BillNo + "</button></td>";
                        }
                        else {
                            tbody += "<td style='width:15%;text-align:center'><button class='btn-success' id='btnGen' onclick=GeneratedInvoiceOfMonth(this) disabled>Gen Invoice</button></td>";
                            tbody += "<td style='width:15%;text-align:center'><button class='btnclass2' id='btnbillno' id='btnGen' onclick=BillInformationList('" + val.BillNo + "')>" + val.BillNo + "</button></td>";
                        }
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                    $('#txttotal').text(totalamount.toFixed(0));

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GeneratedInvoiceOfMonth(elem) {
    var PanelId = $(elem).closest('tr').find('td:eq(0)').text();
    if (confirm('Are you sure to BillGeneration?')) {
        var url = config.baseUrl + "/api/Corporate/OPD_BillGeneration";
        var objBO = {};
        objBO.PanelId = PanelId;
        objBO.MonthName = $('#txtMonth').val();
        objBO.Prm1 = "-";
        objBO.Prm2 = "-";
        objBO.LoginId = Active.userId;
        objBO.Logic = "GenerateBill";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    GetList();

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
function BillInformationList(billno) {
    var url = "OpdBillDetails?billno=" + billno;
    window.open(url, '_blank');
}