
$(document).ready(function () {
    $("select").select2();
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    CloseSidebar();
    LoadDoctorAndPanel();
});
function CheckDateDiff() {
    var from = new Date($('#txtFrom').val()).getDate();
    var to = new Date($('#txtTo').val()).getDate();
    var diff = to - from;
    (eval(diff) > 2) ? $('#btnGet').prop('disabled', true) : $('#btnGet').prop('disabled', false);
}
function LoadDoctorAndPanel() {
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.loginId = '-';
    objBO.Logic = "LoadDoctorAndPanel";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $("#ddlPanel").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
                    $.each(data.ResultSet.Table1, function (key, value) {
                        $("#ddlPanel").append($("<option></option>").val(value.PanelId).html(value.PanelName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $("#ddlDoctor").empty().append($("<option></option>").val("ALL").html("ALL")).select2();
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlDoctor").append($("<option></option>").val(value.DoctorId).html(value.DoctorName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function WorkOrderReport() {
    $('#tblDailyWorkReport tbody').empty();
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = $('#ddlPanel option:selected').val();
    objBO.prm_2 = $('#ddlDoctor option:selected').val();
    objBO.prm_3 = $('#ddlTranType option:selected').val();
    objBO.prm_4 = $('#ddlIPOPType option:selected').val();
    objBO.loginId = Active.userId;
    objBO.Logic = "WorkOrderReport";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var Total = 0, discount = 0, Tax = 0, NetAmount = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        Total += val.Total, discount += val.discount, Tax += val.Tax, NetAmount += val.NetAmount;
                        tbody += '<tr>';
                        tbody += '<td>' + val.UHID + '</td>';
                        tbody += '<td>' + val.patient_name + '</td>';
                        tbody += '<td>' + val.PanelName + '</td>';
                        tbody += '<td>' + val.DoctorName + '</td>';
                        tbody += '<td>' + val.ref_name + '</td>';
                        tbody += '<td class="text-right">' + val.Total + '</td>';
                        tbody += '<td class="text-right">' + val.discount + '</td>';
                        tbody += '<td class="text-right">' + val.Tax + '</td>';
                        tbody += '<td class="text-right">' + val.NetAmount + '</td>';
                        tbody += '</tr>';
                    });
                    tbody += '<tr style="background:#ddd;font-size:14px;">';
                    tbody += '<td colspan="5"><b>Total</b></td>';
                    tbody += '<td class="text-right"><b>' + Total + '</b></td>';
                    tbody += '<td class="text-right"><b>' + discount + '</b></td>';
                    tbody += '<td class="text-right"><b>' + Tax + '</b></td>';
                    tbody += '<td class="text-right"><b>' + NetAmount + '</b></td>';
                    tbody += '</tr>';
                    $('#tblDailyWorkReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DownloadExcel(elem) {
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = $('#ddlPanel option:selected').val();
    objBO.prm_2 = $('#ddlDoctor option:selected').val();
    objBO.prm_3 = $('#ddlTranType option:selected').val();
    objBO.prm_4 = $('#ddlIPOPType option:selected').val();
    objBO.loginId = Active.userId;
    objBO.OutPutType = 'Excel';
    objBO.Logic = "WorkOrderReport";
    Global_DownloadExcel(url, objBO, "WorkOrderReport.xlsx", elem);
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