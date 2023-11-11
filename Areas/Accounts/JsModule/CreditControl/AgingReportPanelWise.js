
$(document).ready(function () {
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
function AgingReportPanelWise() {
    $("#tblAgingInfo tbody").empty();
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = '-';
    objBO.to = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'AgingReportPanelWise';
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
                    var Aging = 0;
                    var CurDay = 0;
                    var Days1To1N = 0;
                    var Days1NTo2N = 0;
                    var Days2NTo3N = 0;
                    var DaysAbove3N = 0;
                    var OverAllBalance = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        Aging += val.Aging;
                        CurDay += val.CurDay;
                        Days1To1N += val.Days1To1N;
                        Days1NTo2N += val.Days1NTo2N;
                        Days2NTo3N += val.Days2NTo3N;
                        DaysAbove3N += val.DaysAbove3N;
                        OverAllBalance += val.OverAllBalance;

                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.PanelId + "</td>";                        
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td class='text-right'>" + val.Aging + "</td>";
                        tbody += "<td class='text-right'>" + val.CurDay + "</td>";
                        tbody += "<td class='text-right'>" + val.Days1To1N + "</td>";
                        tbody += "<td class='text-right'>" + val.Days1NTo2N + "</td>";
                        tbody += "<td class='text-right'>" + val.Days2NTo3N + "</td>";
                        tbody += "<td class='text-right'>" + val.DaysAbove3N + "</td>";
                        tbody += "<td class='text-right'>" + val.OverAllBalance + "</td>";
                        tbody += "<td><button data-logic='AgingReportDetail' onclick=DownloadExcel2(this) class='btn btn-warning btn-xs'><i class='fa fa-file-excel-o'>&nbsp;</i>Detailed Excel</button></td>";
                        tbody += "</tr>";
                    });
                    tbody += "<tr style='background:#ddd;font-size:14px;'>";
                    tbody += "<td><b class='text-right'>Total</b></td>";
                    tbody += "<td class='text-right'><b>" + Aging.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + CurDay.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + Days1To1N.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + Days1NTo2N.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + Days2NTo3N.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + DaysAbove3N.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>" + OverAllBalance.toFixed(2) + "</b></td>";
                    tbody += "<td class='text-right'><b>-</b></td>";
                    tbody += "</tr>";
                    $("#tblAgingInfo tbody").append(tbody);
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
    objBO.from = '-';
    objBO.to = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.ReportType = 'Excel';
    objBO.Logic = $(elem).data('logic');
    Global_DownloadExcel(url, objBO, $(elem).data('logic') + ".xlsx", elem);
}
function DownloadExcel2(elem) {
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = $(elem).closest('tr').find('td:eq(0)').text();
    objBO.from = '-';
    objBO.to = '-';
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