$(document).ready(function () {
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');

});

function tatReport() {
    $('#tblTATReoprt tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.IndentNo = '-';
    objBO.Prm1 = $("#ddlIPOPType option:Selected").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'BloodTATReport';
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
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IssueFlag == "N") {
                            tbody += "<tr style='background:#e9c6c6'>";
                        }
                        else {
                            tbody += "<tr>";
                        }

                        //   tbody += "<tr>";
                        tbody += "<td>" + val.IPOPType + "</td>";
                        tbody += "<td>" + val.IPOPNo + "</td>";
                        tbody += "<td>" + val.PatientName + "</td>";
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td>" + val.IndentNo + "</td>";
                        tbody += "<td style='text-align:center'>" + val.IndentQty + "</td>";
                        tbody += "<td>" + val.IndentDate + "</td>";
                        tbody += "<td>" + val.IssueDate + "</td>";
                        tbody += "<td style='text-align:center'>" + val.Tat + "</td>";
                        tbody += "<td style='text-align:center'>" + val.IssueFlag + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTATReoprt tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ExceltatReport() {
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.IndentNo = '-';
    objBO.Prm1 = $("#ddlIPOPType option:Selected").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'BloodTATReport';
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "TATReport" + ".xlsx");
}