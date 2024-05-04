$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    CloseSidebar();
});
function LabRegister(logic) {
    $('#tblLabRegister tbody').empty();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.VisitNo = $('#txtInput').val();
    objBO.BarcodeNo = '-';
    objBO.DispatchLabCode = '-';
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#ddlIPOPType option:selected').val();
    objBO.logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = '';
            var count = 0;
            var patientname = "";
            var DispatchLab = "";
            var consentList = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.ConsentId + "</td>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.IPOPType + "</td>";
                        tbody += "<td>" + val.ipop_no + "</td>";
                        tbody += "<td>" + val.VisitNo + "</td>";
                        tbody += "<td>" + val.RegDate + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        (val.ConsentId == null) ? tbody += "<td>-</td>" :
                            tbody += "<td><button onclick=ViewConsentForm(this) class='btn btn-warning btn-xs'><i class='fa fa-eye'>&nbsp;</i>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblLabRegister tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ViewConsentForm(elem) {
    $('.consentForm').html('');
    var consentView = "";
    var visitNo = $(elem).closest('tr').find('td:eq(4)').text().split(',');
    var consentList = $(elem).closest('tr').find('td:eq(0)').text().split(',');

    if (consentList.length == 1)
        window.open(config.rootUrl + "/Lab/Print/PrintConsentForm?VisitNo=" + visitNo + "&consentId=" + consentList[0], '_blank');
    else {
        for (var i in consentList) {
            consentView += "<iframe src=" + config.rootUrl + "/Lab/Print/PrintConsentForm?consentId=" + i + " />";
        }
        $('.consentForm').html(consentView);
        $('#modalViewConsent').modal('show');
    }
}