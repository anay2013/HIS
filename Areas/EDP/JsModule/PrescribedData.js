var doctorid = "";
var doctorname = "";

$(document).ready(function () {
    FillCurrentDate('txtSearchFrom')
    FillCurrentDate('txtSearchTo')
    DoctorList();
    $('#tblDoctorsList tbody').on('click', '#Selectvalue', function () {
        doctorid = $(this).closest('tr').find('td:eq(0)').text();
        doctorname = $(this).closest('tr').find('td:eq(1)').text();
        $("#txtdoctorid").text(doctorid);
        $("#txtDoctorName").text(doctorname);

    });

    $('#txtdoc').on('keyup', function () {
        var val = $(this).val().toLowerCase();

        $('#tblDoctorsList tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });
});
function DoctorList() {
    $('#tblDoctorsList tbody').empty();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentSearch";
    var objBO = {};
    objBO.SearchValue = '-';
    objBO.prm_1 = '-';
    objBO.DoctorId = '-';
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Logic = 'OnLoadDoctor';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td>' + val.DoctorId + '</td>';
                        tbody += '<td>' + val.DoctorName + '</td>';
                        tbody += '<td><button class="btn-warning btn-tbl" id="Selectvalue"><i class="fa fa-sign-in"></i></button></td>';
                        tbody += '</tr>';
                    });
                    $('#tblDoctorsList tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function ExeclReport() {
    $('#btnAll').append("<i class='fa fa-spinner fa-spin' style='font-size:24px; margin-left:10px'></i>");
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentSearch";
    var objBO = {};
    objBO.SearchValue = '-';
    objBO.prm_1 = '-';
    objBO.DoctorId = doctorid;
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.ReportType = "Excel";
    objBO.Logic = "DownloadPrescribedData";
    Global_DownloadExcel(url, objBO, "PrescribedData" + ".xlsx");
}

function Global_DownloadExcel(Url, objBO, fileName) {
    var ajax = new XMLHttpRequest();
    ajax.open("POST", Url, true);
    ajax.responseType = "blob";
    ajax.setRequestHeader("Content-type", "application/json");
    ajax.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            saveAs(blob, fileName);
            stopLoadings();
        }
    };

    ajax.onerror = function () {
        alert("Error downloading file");
        stopLoadings();
    };

    ajax.send(JSON.stringify(objBO));
}

function stopLoadings() {
    $('#btnAll i').remove();
}