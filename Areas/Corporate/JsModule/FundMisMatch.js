$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
});

function GetFundMisMatchInfo() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Corporate/CanelFundMGM_Queries";
    var objBO = {};
    objBO.hospiId = '-';
    objBO.PanelId = '-';
    objBO.UHID = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.prm_3 = '-';
    objBO.loginId = Active.userId;
    objBO.logic = 'Audit:FundManagement';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            if (Object.keys(data.ResultSet.Table).length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {

                    if (val.IsMisMatch == "Y") {
                        tbody += "<tr style='background:#f9bbbb'>";
                    }
                    else {
                        tbody += "<tr>";
                    }
                    tbody += "<td>" + val.TnxDate + "</td>";
                    tbody += "<td style='text-align: center'>" + val.IPD_FundAmt + "</td>";
                    tbody += "<td style='text-align: center'>" + val.OPD_FundAmt + "</td>";
                    tbody += "<td style='text-align: center'>" + val.Phar_FundAmt + "</td>";
                    tbody += "<td style='text-align: center'>" + val.IPD_InReceivable + "</td>";
                    tbody += "<td style='text-align: center'>" + val.IPD_VCH + "</td>";
                    tbody += "<td style='text-align: center'>" + val.OPD_VCH + "</td>";
                    tbody += "<td style='text-align: center'>" + val.Phar_VCH + "</td>";
                    tbody += "<td style='text-align: center'>" + val.IsMisMatch + "</td>";
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

function ExeclFundMisMatch() {
    var url = config.baseUrl + "/api/Corporate/CanelFundMGM_Queries";
    var objBO = {};
    objBO.hospiId = '-';
    objBO.PanelId = '-';
    objBO.UHID = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.prm_3 = '-';
    objBO.loginId = Active.userId;
    objBO.logic = 'Audit:FundManagement';
    objBO.OutPutType = "Excel";
    Global_DownloadExcel(url, objBO, "FundMisMatch" + ".xlsx");
}