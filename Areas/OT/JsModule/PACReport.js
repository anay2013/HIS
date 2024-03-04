var logic = ''
$(document).ready(function () {
    CloseSidebar();
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');

})

function DownloadExcelPacOTReport() {
    var url = config.baseUrl + "/api/OT/OTScheduleQueries";
    var objBO = {};
    objBO.OTId = '-';
    objBO.DoctorId = '-';
    objBO.IPDNo = "-";
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = $("#txttext").val();
    objBO.Prm2 = "-";
    objBO.Logic = logic;
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "PackOTReport" + ".xlsx");
}
function SearchByTextKey() {
    debugger
    logic = 'PACOT:ReportByKeySearch';
    $('#tblPackOTReport tbody').empty();
    var url = config.baseUrl + "/api/OT/OTScheduleQueries";
    var objBO = {};
    objBO.OTId = '-';
    objBO.DoctorId = '-';
    objBO.IPDNo = "-";
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = $("#txttext").val();
    objBO.Prm2 = "-";
    objBO.Logic = "PACOT:ReportByKeySearch";
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
                        tbody += "<tr>";
                        tbody += "<td>" + val.ipopType + "</td>";
                        tbody += "<td>" + val.ipop_no + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td style='text-align: left'>" + val.DoctorName + "</td>";
                        tbody += "<td style='text-align: left'>" + val.patient_name + "</td>";
                        tbody += "<td style='text-align: left'>" + val.ageinfo + "</td>";
                        tbody += "<td style='text-align: left'>" + val.address + "</td>";
                        tbody += "<td style='text-align: left'>" + val.emp_name + "</td>";
                        tbody += "<td style='text-align: left'>" + val.ItemName + "</td>";
                        tbody += "<td style='text-align: left'>" + val.tnxDate + "</td>";
                        tbody += "<td>" + val.amount + "</td>";
                        tbody += "</tr>";

                    });
                    $('#tblPackOTReport tbody').append(tbody);

                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function SearchByDateWise() {
    debugger
    logic = 'PACOT:ReportByDate';
    $('#tblPackOTReport tbody').empty();
    var url = config.baseUrl + "/api/OT/OTScheduleQueries";
    var objBO = {};
    objBO.OTId = '-';
    objBO.DoctorId = '-';
    objBO.IPDNo = "-";
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "PACOT:ReportByDate";
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
                        tbody += "<tr>";
                        tbody += "<td>" + val.ipopType + "</td>";
                        tbody += "<td>" + val.ipop_no + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td style='text-align: left'>" + val.DoctorName + "</td>";
                        tbody += "<td style='text-align: left'>" + val.patient_name + "</td>";
                        tbody += "<td style='text-align: left'>" + val.ageinfo + "</td>";
                        tbody += "<td style='text-align: left'>" + val.address + "</td>";
                        tbody += "<td style='text-align: left'>" + val.emp_name + "</td>";
                        tbody += "<td style='text-align: left'>" + val.ItemName + "</td>";
                        tbody += "<td style='text-align: left'>" + val.tnxDate + "</td>";
                        tbody += "<td>" + val.amount + "</td>";
                        tbody += "</tr>";

                    });
                    $('#tblPackOTReport tbody').append(tbody);

                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

