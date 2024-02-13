$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    GetPanel()
});
function GetPanel() {
    $('#ddlPanel').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
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
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function OverALLReceivable() {
    $("#tblReceivableInfo tbody").empty();
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'Over-ALLReceivable';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var OpeningBalance = 0;
                    var IPD_Rec = 0;
                    var OPD_Rec = 0;
                    var TReceivable = 0;
                    var IPD_Payment = 0;
                    var OPD_Payment = 0;
                    var IPD_BadDet = 0;
                    var OPD_BadDet  = 0;
                    var TPayment = 0;
                    var ClosingBalance = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        OpeningBalance += val.OpeningBalance;
                        IPD_Rec += val.IPD_Rec;
                        OPD_Rec += val.OPD_Rec;
                        TReceivable += val.TReceivable;
                        IPD_BadDet += val.IPD_BadDet;
                        OPD_BadDet += val.OPD_BadDet;
                        OPD_Payment += val.OPD_Payment;
                        TPayment += val.TPayment;
                        ClosingBalance += val.ClosingBalance;

                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.PanelId + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td class='text-right'>" + val.OpeningBalance + "</td>";
                        tbody += "<td class='text-right'>" + val.IPD_Rec + "</td>";
                        tbody += "<td class='text-right'>" + val.OPD_Rec + "</td>";
                        tbody += "<td class='text-right'>" + val.TReceivable + "</td>";
                        tbody += "<td class='text-right'>" + val.IPD_Payment + "</td>";
                        tbody += "<td class='text-right'>" + val.IPD_BadDet + "</td>";
                        tbody += "<td class='text-right'>" + val.OPD_Payment + "</td>";
                        tbody += "<td class='text-right'>" + val.OPD_BadDet + "</td>";
                        tbody += "<td class='text-right'>" + val.TPayment + "</td>";
                        tbody += "<td class='text-right'>" + val.ClosingBalance + "</td>";
                        tbody += "</tr>";
                    });
                    tbody += "<tr style='background:#ddd;font-size:14px;'>";
                    tbody += "<td><b class='text-right'>Total</b></td>";
                    tbody += "<td class='text-right'><b>" + OpeningBalance.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + IPD_Rec.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + OPD_Rec.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + TReceivable.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + IPD_Payment.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + IPD_BadDet.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + OPD_Payment.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + OPD_BadDet.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + TPayment.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + ClosingBalance.toFixed(2) + "</b></td>";
                    tbody += "</tr>";
                    $("#tblReceivableInfo tbody").append(tbody);
                }
                else {
                    alert("Data Not Found");
                };
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DownloadExcel(elem) {
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.ReportType = 'Excel';
    objBO.Logic = $(elem).data('logic');
    Global_DownloadExcel(url, objBO, $(elem).data('logic') + ".xlsx", elem);
}
function Global_DownloadExcel(Url, objBO, fileName, elem) {
    $(elem).addClass('loading');
    var ajax = new XMLHttpRequest();
    ajax.open("Post", Url, true);
    ajax.responseType = "blob";
    ajax.setRequestHeader("Content-type", "application/json")
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            saveAs(blob, fileName); //refernce by ~/JsModule/FileSaver.min.js
            $(elem).removeClass('loading');
        }
    };
    ajax.send(JSON.stringify(objBO));
}