﻿
$(document).ready(function () {
    //FillCurrentDate("txtFrom");
    //FillCurrentDate("txtTo");
    FillCurrentDateTime1();
});
function FillCurrentDateTime1() {
    var today = '';
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var hour = date.getHours();
    var Minute = date.getMinutes();
    var Second = date.getSeconds();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    today = year + "-" + month + "-" + day + 'T' + "00" + ':' + "00" + ':' + "01";
    console.log(today)
    $('#txtFrom').val(today)
    $('#txtTo').val(year + "-" + month + "-" + day + 'T' + "23" + ':' + "59" + ':' + "59")
    return today;
}
function CollectionSummaryReport() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    debugger
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = $('#ddlUsers option:selected').val();
    objBO.prm_2 = '-';
    objBO.loginId = 'ALL';
    objBO.Logic = "CollectionSummaryReportByUser";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var p_Cash = 0;
            var p_SwipeCard = 0;
            var p_Cheque = 0;
            var p_NEFT_RTGS = 0;
            var n_Cash = 0;
            var n_SwipeCard = 0;
            var n_Cheque = 0;
            var n_NEFT_RTGS = 0;
            var f_cash = 0;
            var f_SwipeCard = 0;
            var f_Cheque = 0;
            var f_NEFT_RTGS = 0;
            var FinalTotal = 0;

            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        p_Cash += val.p_Cash;
                        p_SwipeCard += val.p_SwipeCard;
                        p_Cheque += val.p_Cheque;
                        p_NEFT_RTGS += val.p_NEFT_RTGS;
                        n_Cash += val.n_Cash;
                        n_SwipeCard += val.n_SwipeCard;
                        n_Cheque += val.n_Cheque;
                        n_NEFT_RTGS += val.n_NEFT_RTGS;
                        f_cash += val.f_cash;
                        f_SwipeCard += val.f_SwipeCard;
                        f_Cheque += val.f_Cheque;
                        f_NEFT_RTGS += val.f_NEFT_RTGS;
                        FinalTotal += val.FinalTotal;

                        tbody += '<tr>';
                        tbody += '<td>' + val.StaffName + '</td>';
                        tbody += '<td class="text-right">' + val.p_Cash + '</td>';
                        tbody += '<td class="text-right">' + val.p_SwipeCard + '</td>';
                        tbody += '<td class="text-right">' + val.p_Cheque + '</td>';
                        tbody += '<td class="text-right">' + val.p_NEFT_RTGS + '</td>';
                        tbody += '<td class="text-right">' + val.n_Cash + '</td>';
                        tbody += '<td class="text-right">' + val.n_SwipeCard + '</td>';
                        tbody += '<td class="text-right">' + val.n_Cheque + '</td>';
                        tbody += '<td class="text-right">' + val.n_NEFT_RTGS + '</td>';
                        tbody += '<td class="text-right">' + val.f_cash + '</td>';
                        tbody += '<td class="text-right">' + val.f_SwipeCard + '</td>';
                        tbody += '<td class="text-right">' + val.f_Cheque + '</td>';
                        tbody += '<td class="text-right">' + val.f_NEFT_RTGS + '</td>';
                        tbody += '<td class="text-right">' + val.FinalTotal + '</td>';
                        tbody += '</tr>';
                    });
                    tbody += '<tr>';
                    tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>Total</b></td>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_Cash + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_SwipeCard + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_Cheque + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_NEFT_RTGS + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_Cash + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_SwipeCard + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_Cheque + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_NEFT_RTGS + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + f_cash + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + f_SwipeCard + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + f_Cheque + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + f_NEFT_RTGS + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + FinalTotal + '</b></th>';
                    tbody += '</tr>';
                    $('#tblReport tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DownloadExcels(elem) {
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = $('#ddlUsers option:selected').val();
    objBO.prm_2 = '-';
    objBO.OutPutType = "Excel";
    objBO.loginId = 'ALL';
    objBO.Logic = "CollectionSummaryReportByUser";
    Global_DownloadExcel(url, objBO, "CollectionSummaryReportByUser.xlsx", elem);
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
function PrintReports() {
    var hosp_id = Active.HospId;
    debugger
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var prm_1 = $('#ddlUsers option:selected').val();
    var loginId = 'ALL';
    var url = "../Print/CollectionSummaryReport?hosp_id=" + hosp_id + "&from=" + from + "&to=" + to + "&prm_1=" + prm_1 + "&loginId=" + loginId;
    window.open(url, '_blank');
}
function GetUserLists() {
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.loginId = 'ALL';
    objBO.Logic = "UserList";
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
                    $('#ddlUsers').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlUsers').append($('<option></option>').val(val.emp_code).html(val.emp_name));

                    });
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}