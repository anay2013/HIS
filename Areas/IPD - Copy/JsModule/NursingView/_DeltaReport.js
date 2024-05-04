var Testcode = "";
var Testname = "";

    $(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Delta Report').show();
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txtTodate');
    GetDataDeltaReport();
    $('#tblDeltaReport tbody').on('click', '#DeltaReportIdWise', function () {
        Testcode = $(this).closest('tr').find('td:eq(0)').text();
        Testname = $(this).closest('tr').find('td:eq(1)').text();
        $("#txttestname").text(Testname);
        GetDataDeltaReportInfo(Testcode);
    });

        LockPrvDate()   
});

function LockPrvDate() {
    $("#txtfromdate,#txtTodate").each(function () {
        $(this).attr("min", _AdmitDateServer.split('T')[0]);
        $(this).attr("max", sessionStorage.getItem('ServerTodayDate').split('T')[0]);
    });
}
function GetDataDeltaReport() {
    $('#tblDeltaReport tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_DeltaQueries";
    var objBO = {};
    objBO.IpOpType = '-';
    objBO.IPDNo = _IPDNo;
    objBO.UHID = '-';
    objBO.TestCode = '-';
    objBO.ObservationId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txtTodate').val();
    objBO.Logic = 'TestPerformedList';
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
                        tbody += "<td style='width:20%;text-align:center' hidden>" + val.TestCode + "</td>";
                        tbody += "<td style='width:40%;text-align:left'>" + val.TestName + "</td>";
                        tbody += "<td style='width:40%;text-align:left'>" + val.LatRegDate + "</td>";
                        tbody += "<td style='width:10%;text-align:center'><button style='border-radius: 50%;' class='btn btn-warning btn-xs'>" + val.tCount + "</button></td></td>";
                        tbody += "<td style='width:10%; height:20px; width:25px;'><button class='btn btn-success btn-xs' id='DeltaReportIdWise'><i class='fa fa-arrow-circle-right' style='font-size:12px'></i></button></td>";
                        tbody += "</tr>";


                    });
                    $('#tblDeltaReport tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataDeltaReportInfo(Testcode) {
    var url = config.baseUrl + "/api/Lab/Lab_DeltaQueriesBindings";
    var objBO = {};
    objBO.IpOpType = '-';
    objBO.IPDNo = _IPDNo;
    objBO.UHID = '-';
    objBO.TestCode = Testcode;
    objBO.ObservationId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txtTodate').val();
    objBO.Logic = 'TestPerformedPivot';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#divDelta').html(data);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeltaReportPrint() {
    var url = config.documentServerUrl+ "/Lab/Print/DeltaReport?IPDNO=" + _IPDNo + "&Testcodevalue=" + Testcode + "&Testname=" + Testname;
    window.open(url, '_blank');

}