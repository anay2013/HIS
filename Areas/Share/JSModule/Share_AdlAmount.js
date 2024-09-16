$(document).ready(function () {
    CurrentMonth();
    OnLoadDoctorList();
});
function CurrentMonth() {
    var date = new Date();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    if (month < 10) month = "0" + month;
    var currentMonth = year + "-" + month;
    $("#txtMonth").attr("value", currentMonth);
}
function OnLoadDoctorList() {
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = "-";
    objBO.prm_2 = "-";
    objBO.prm_3 = "-";
    objBO.from = "1999-01-01";
    objBO.to = "1999-01-01";
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "OnLoadDoctor";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#ddlDoctor').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName)).select2();
                    });
                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function SaveAmount() {
    if ($('#ddlDoctor').val() == 'Select') {
        alert('Please Select Doctor Name');
        $('#ddlDoctor').focus();
        return
    }
    if ($('#ddlType').val() == 'Select') {
        alert('Please Select Type Name');
        $('#ddlType').focus();
        return
    }
    var url = config.baseUrl + "/api/Share/Share_AdlAmountInsert";
    var objBO = {};
    objBO.month_name = $("#txtMonth").val()
    objBO.DoctorId = $('#ddlDoctor option:selected').val();
    objBO.TnxType = $("#ddlType option:selected").val();
    objBO.share_amount = $("#txtAmout").val()
    objBO.ChairpersonShare = "0";
    objBO.adj_amount = "0";
    objBO.Vice_ChairpersonShare = '0';
    objBO.DeptName = '-';
    objBO.Designation = '-';
    objBO.tnxSource = '-';
    objBO.cr_date = '1999-01-01'
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'insert:AdlAmout';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetAllListAmount();
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
function GetAllListAmount() {
    $("#tblReoprt tbody").empty();
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = "-";
    objBO.prm_2 = "-";
    objBO.prm_3 = "-";
    objBO.from = $("#txtMonth").val();
    objBO.to = "1999-01-01";
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "Share:AdlAmountList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td>' + val.month_name + '</td>';
                        tbody += '<td>' + val.DoctorId + '</td>';
                        tbody += '<td>' + val.TnxType + '</td>';
                        tbody += '<td style="text-align:center">' + val.share_amount + '</td>';
                        tbody += '<td style="text-align:center">' + val.ChairpersonShare + '</td>';
                        tbody += '<td style="text-align:center">' + val.adj_amount + '</td>';
                        tbody += '<td style="text-align:center">' + val.Vice_ChairpersonShare + '</td>';
                        tbody += '<td>' + val.DeptName + '</td>';
                        tbody += '<td>' + val.Designation + '</td>';
                        tbody += '<td>' + val.tnxSource + '</td>';
                        tbody += '<td>' + val.cr_date + '</td>';
                        tbody += '</tr>';
                    });
                    $("#tblReoprt tbody").append(tbody);

                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}