var temp = "";
$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Investigation Tracking').show();
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txtTodate');
    LockPrvDate();
});
function LockPrvDate() {
    $("#txtfromdate,#txtTodate").each(function () {
        $(this).attr("min", _AdmitDateServer.split('T')[0]);
        $(this).attr("max", sessionStorage.getItem('ServerTodayDate').split('T')[0]);
    });
}
function InvestigationTracking() {    
    $('#tblInvestigationTracking tbody').empty();

    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.VisitNo = _IPDNo;
    objBO.BarcodeNo = '-';
    objBO.SampleCode = '-';
    objBO.TestCode = '-';
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txtTodate").val();
    objBO.Prm1 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'IPD:InvestigationTracking';
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
                    
                        if (temp != val.VisitNo) {
                            tbody += "<tr style='background:#efe0e0'>";
                            tbody += "<td colspan='12' style='font-size:14px;text-align:left;'>LabNo : &nbsp;<b>" + val.VisitNo + " &nbsp;Date :&nbsp; " + val.RegDate + "</b></td>";
                            tbody += "</tr>";
                            temp = val.VisitNo, val.RegDate
                        }
                        else {

                        }
                        //tbody += "<tr>";
                        //tbody += "<td>" + val.VisitNo + "</td>";
                        //tbody += "<td>" + val.RegDate + "</td>";
                        tbody += "<td style='width:9%'>" + val.testCategory + "</td>";
                        tbody += "<td style='width:5%'>" + val.ItemId + "</td>";
                        tbody += "<td style='width:24%'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:6%'>" + val.samp_code + "</td>";
                        tbody += "<td style='width:9%'>" + val.SampleOrInBy + "</td>";
                        tbody += "<td style='width:9%'>" + val.SampleOrInTime + "</td>";
                        tbody += "<td style='width:9%'>" + val.ApprovedDate + "</td>";
                        tbody += "<td style='width:9%'>" + val.ApproveBy + "</td>";
                        tbody += "<td style='width:9%'>" + val.IsLocalTest + "</td>";
                        tbody += "<td style='width:5%'>" + val.IsCancelled + "</td>";
                        tbody += "<td style='width:5%'>" + val.InOutStatus + "</td>";
                       
                       
                        tbody += "</tr>";

                    });
                    $('#tblInvestigationTracking tbody').append(tbody);

                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}