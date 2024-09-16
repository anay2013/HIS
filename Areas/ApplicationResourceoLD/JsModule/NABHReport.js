$(document).ready(function () {
    FillCurrentDate('txtFromDate')
    FillCurrentDate('txtToDate')
});

//function GetAllNABHReport() {
//    $('#tblReport tbody').empty();
//    var reporttype = $('#ddlReport option:selected').val();
//    if (reporttype == 'Select') {
//        alert("Please Select Report Type")
//        $("#ddlReport").focus();
//        return;
//    }
//    var url = config.baseUrl + "/api/Admin/NABH_Reports";
//    var objBO = {};
//    objBO.HospId = Active.HospId;
//    objBO.from = $('#txtFromDate').val();
//    objBO.to = $('#txtToDate').val();
//    objBO.Prm1 = '-';
//    objBO.Prm2 = '-';
//    objBO.Prm3 = '-';
//    objBO.Logic = 'DoctorAppointment' + ':' + $("#ddlReport option:selected").val();
//    $.ajax({
//        method: "POST",
//        url: url,
//        data: JSON.stringify(objBO),
//        contentType: "application/json;charset=utf-8",
//        dataType: "JSON",
//        success: function (data) {
//            if (Object.keys(data.ResultSet).length) {
//                var tbody = "";
//                if (Object.keys(data.ResultSet.Table).length) {
//                    $.each(data.ResultSet.Table, function (key, val) {
//                        if (val.apStatus == "Done")
//                            tbody += "<tr style='background:#adf1b9'>";
//                        else {
//                            tbody += "<tr>";
//                        }

//                        tbody += "<td>" + val.UHID + "</td>";
//                        tbody += "<td>" + val.app_no + "</td>";
//                        tbody += "<td>" + val.patient_name + "</td>";
//                        tbody += "<td>" + val.gender + "</td>";
//                        tbody += "<td>" + val.ageInfo + "</td>";
//                        tbody += "<td>" + val.DepartmentName + "</td>";
//                        tbody += "<td>" + val.DoctorName + "</td>";
//                        tbody += "<td>" + val.AppDateTime + "</td>";
//                        tbody += "<td>" + val.BookingDateTime + "</td>";
//                        tbody += "<td>" + val.InTime + "</td>";
//                        tbody += "<td>" + val.WaitingTime + "</td>";
//                        tbody += "<td>" + val.apStatus + "</td>";

//                        tbody += "</tr>";
//                    });
//                    $('#tblReport tbody').append(tbody);


//                }
//            }
//        },
//        error: function (response) {
//            alert('Server Error...!');
//        }
//    });
//}

function GetExeclNABHReport() {
    var url = config.baseUrl + "/api/Admin/NABH_Reports";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.from = $('#txtFromDate').val();
    objBO.to = $('#txtToDate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Prm3 = '-';
    objBO.OutputType = 'Excel';
    objBO.Logic = $("#ddlReport option:selected").val();
    Global_DownloadExcel(url, objBO, "NABHReport" + ".xlsx");
}