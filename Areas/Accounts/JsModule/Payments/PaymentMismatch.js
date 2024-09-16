$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
});

function GetPaymentMisMatch() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Account/Ac_AuditVoucherQueries";
    var objBO = {};
    objBO.hospiId = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.prm_3 = '-';
    objBO.loginId = Active.userId;
    objBO.logic = 'PaymentMismatch';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet.Table).length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                   
                    if (val.IsMisMatch == "Y") {
                        tbody += "<tr style='background:#f9bbbb'>";
                    }
                    else {
                        tbody += "<tr>";
                    }
                    tbody += "<td>" + val.PanelName + "</td>";
                    tbody += "<td>" + val.TnxDate + "</td>";
                    tbody += "<td style='text-align:center;width:10%'>" + val.Amount + "</td>";
                    tbody += "<td style='text-align:center;width:10%'>" + val.VchAmount + "</td>";
                    tbody += "<td style='text-align:center;width:10%'>" + val.DIFF + "</td>";
                    tbody += "<td style='text-align:center;width:10%'>" + val.IsMisMatch + "</td>";
                    tbody += "</tr>";		
                });
                $('#tblReport tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ExeclPaymentMisMatch() {
    var url = config.baseUrl + "/api/Account/Ac_AuditVoucherQueries";
    var objBO = {};
    objBO.hospiId = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.prm_3 = '-';
    objBO.loginId = Active.userId;
    objBO.logic = 'PaymentMismatch';
    objBO.ReportType = "Excel";
    Global_DownloadExcel(url, objBO, "PaymentMismatch" + ".xlsx");
   
}