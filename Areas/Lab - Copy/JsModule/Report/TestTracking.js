
$(document).ready(function () {

    CloseSidebar();

});
function VisitNoBySearchdata() {
    debugger
    $('#tblTestTrackingReport tbody').empty();
    debugger
    var url = "http://192.168.0.11/HISWebApi/api/sample/Lab_SampleCollectionQueries";
    // var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.VisitNo = $("#txtVisitNo").val();
    objBO.BarcodeNo = '-';
    objBO.SampleCode = '-';
    objBO.TestCode = '-';
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.Prm1 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'RecordTracking';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        $("#_txtVisitNo").text(data.ResultSet.Table[0].visitNo);
                        $("#_txtPatientName").text(data.ResultSet.Table[0].patient_name);
                        $("#_txtGender").text(data.ResultSet.Table[0].gender);
                        $("#_txtAge").text(data.ResultSet.Table[0].Age);

                    });
                }
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        debugger
                        tbody += "<tr>";
                        tbody += "<td class='hard_left'>" + val.IPOPType + "</td>";
                        tbody += "<td class='next_left'>" + val.barcodeNo + "</td>";
                        tbody += "<td class='next_left' style='margin-left:120px'>" + val.RegDate + "</td>";
                        tbody += "<td class='next_left' style='margin-left:240px'>" + val.ItemId + "</td>";
                        tbody += "<td class='next_left' style='margin-left:360px;'>" + val.ItemName + "</td>";
                        tbody += "<td class='next_left' style='margin-left:480px;'>" + val.testCategory + "</td>";
                        tbody += "<td>" + val.samp_code + "</td>";
                        tbody += "<td>" + val.sample_collect_date + "</td>";
                        tbody += "<td>" + val.sample_collect_by + "</td>";
                        tbody += "<td>" + val.SampleDistributedDate + "</td>";
                        tbody += "<td>" + val.SampleDistributedBy + "</td>";
                        tbody += "<td>" + val.dispatch_date + "</td>";
                        tbody += "<td>" + val.SampleDispatchBy + "</td>";
                        tbody += "<td>" + val.DispatchReceivedTime + "</td>";
                        tbody += "<td>" + val.DispatchReceivedBy + "</td>";
                        tbody += "<td>" + val.LabReceivedDate + "</td>";
                        tbody += "<td>" + val.LabReceivedBy + "</td>";
                        tbody += "<td>" + val.max_reptime + "</td>";
                        tbody += "<td>" + val.DelivaryTime + "</td>";
                        tbody += "<td>" + val.ApprovedDate + "</td>";
                        tbody += "<td>" + val.ApproveBy + "</td>";
                        tbody += "<td>" + val.IsSampleRequired + "</td>";
                        tbody += "<td>" + val.IsLocalTest + "</td>";
                        tbody += "<td>" + val.IsCancelled + "</td>";
                        tbody += "<td>" + val.r_type + "</td>";
                        tbody += "<td>" + val.InOutStatus + "</td>";
                        tbody += "</tr>";

                    });
                    $('#tblTestTrackingReport tbody').append(tbody);
                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

