$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
});

function GetDataMedicineBill() {
    var url = config.baseUrl + "/api/Pharmacy/Hospital_Queries";
    var objBO = {};
    objBO.unit_id = '-';
    objBO.uhid = '-';
    objBO.prm_1 = $('#txtSearchFrom').val();
    objBO.prm_2 = $('#txtSearchTo').val();
    objBO.prm_3 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "OPDPanelMedicineReport";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width:10%'>" + val.sale_date + "</td>";
                        tbody += "<td style='width:10%'>" + val.hosp_cr_no + "</td>";
                        tbody += "<td style='width:15%'>" + val.pt_name + "</td>";
                        tbody += "<td style='width:10%'>" + val.sale_inv_no + "</td>";
                        tbody += "<td style='width:15%'>" + val.ref_name + "</td>";
                        tbody += "<td style='width:10%'>" + val.panelName + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.total + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.discount + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.net + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function DownloadExcelBill() {
    var url = config.baseUrl + "/api/Pharmacy/Hospital_Queries";
    var objBO = {};
    objBO.unit_id = '-';
    objBO.uhid = '-';
    objBO.prm_1 = $('#txtSearchFrom').val();
    objBO.prm_2 = $('#txtSearchTo').val();
    objBO.prm_3 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "OPDPanelMedicineReport";
    objBO.ReportType = 'Excel';
    Global_DownloadExcel(url, objBO, "MedicineBillReport.xlsx");
}