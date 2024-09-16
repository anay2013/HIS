$(document).ready(function () {
    CloseSidebar();
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');
    $('#txtPatientName').on('keyup', function () {
        var val = $(this).val().toLowerCase();
        $('#tblReport tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });
});

function GetDataAllList() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/OT/OTScheduleQueries";
    var objBO = {};
    objBO.OTId = '-';
    objBO.DoctorId = '-';
    objBO.IPDNo = "-";
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txttodate").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "SurgeryReport";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var count = 0;
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td style='width:3%;text-align:center'>" + count + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.UHID + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.IPDNo + "</td>";
                        tbody += "<td style='width:13%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:13%'>" + val.SurgeryDate + "</td>";
                        tbody += "<td style='width:13%'>" + val.SurgeonName + "</td>";
                        tbody += "<td style='width:13%'>" + val.SecondSurgeon + "</td>";
                        tbody += "<td style='width:13%'>" + val.AnestheticName + "</td>";
                        tbody += "<td style='width:13%'>" + val.SurgeryName + "</td>";
                        tbody += "<td style='width:9%;text-align:center'>" + val.Amount + "</td>";
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

function GetExecl() {
    var url = config.baseUrl + "/api/OT/OTScheduleQueries";
    var objBO = {};
    objBO.OTId = '-';
    objBO.DoctorId = '-';
    objBO.IPDNo = "-";
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txttodate").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "SurgeryReport";
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "SurgeryReport" + ".xlsx");
}