var _visitNo;
$(document).ready(function () {
    FillCurrentDate('')
    $('#txtBarcode').on('keyup', function (e) {
        var key = e.keyCode;
        if (key == 13) {

            GetSampleInfo("DoSDR");
        }
        else {

        }

    });

});
function GetSampleInfo(Logic) {
    var barcode;
    if (Logic == "DoSDR")
        barcode = $("#txtBarcode").val();
    else
        barcode = $("#txtBarcodeScan").val();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.BarcodeNo = barcode;
    objBO.from = "1900/01/01";
    objBO.to = "1900/01/01";
    objBO.login_id = Active.userId;
    objBO.Logic = Logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#tblInHouseTest tbody").empty();
            $("#tblOutSourceTest tbody").empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody1 = "";
                    var count1 = 0;
                    var tbody2 = "";
                    var count2 = 0;
                    var PatientName1 = "";
                    var PatientName2 = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        _visitNo = val.visitNo;
                        if (val.IsLocalTest == '1') {
                            if (PatientName1 != val.patient_name) {
                                tbody1 += "<tr class='pat-group'>";
                                tbody1 += "<td colspan='6' class='.pat-group'>" + val.patient_name + ' [ ' + val.visitNo + ' ]' + "</td>";
                                tbody1 += "</tr>";
                                PatientName1 = val.patient_name;
                                count1 = 0;
                            }
                            count1++;
                            tbody1 += "<tr style='background:" + val.SampleColor + "'>";
                            tbody1 += "<td>" + count1 + "</td>";
                            tbody1 += "<td>" + val.TestCode + "</td>";
                            tbody1 += "<td>" + val.testName + "</td>";
                            tbody1 += "<td>" + val.samp_type + "</td>";
                            tbody1 += "<td>" + val.SampleStatus + "</td>";
                            tbody1 += "<td><input type='checkbox' checked/></td>";
                            tbody1 += "<td class='hide'>" + val.AutoTestId + "</td>";
                            tbody1 += "</tr>";
                            if (val.IsSampleDistributed == '1') {
                                $('#btnSDR').hide();
                            }
                            else {
                                $('#btnSDR').show();
                            }
                        }
                        else if (val.IsLocalTest == '0') {
                            if (PatientName2 != val.patient_name) {
                                tbody2 += "<tr class='pat-group'>";
                                tbody2 += "<td colspan='7' class='.pat-group'>" + val.patient_name + ' [ ' + val.visitNo + ' ]' + "</td>";
                                tbody2 += "</tr>";
                                PatientName2 = val.patient_name;
                                count2 = 0;
                            }
                            count2++;
                            tbody2 += "<tr style='background:" + val.SampleColor + "'>";
                            tbody2 += "<td>" + count2 + "</td>";
                            tbody2 += "<td>" + val.TestCode + "</td>";
                            tbody2 += "<td>" + val.testName + "</td>";
                            tbody2 += "<td>" + val.samp_type + "</td>";
                            tbody2 += "<td>" + val.DispatchLab + "</td>";
                            tbody2 += "<td>" + val.SampleStatus + "</td>";
                            tbody2 += "<td><input type='checkbox' checked /></td>";
                            tbody2 += "<td class='hide'>" + val.AutoTestId + "</td>";
                            tbody2 += "</tr>";
                        }
                    });

                    $("#tblInHouseTest tbody").append(tbody1);
                    $("#tblOutSourceTest tbody").append(tbody2);
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
function Print(logic) {
    var visitNo = _visitNo;
    var SubCat = 'ALL';
    var TestIds = [];
    if (logic == 'InHouse') {
        TestIds = [];
        $("#tblInHouseTest tbody tr:not(.pat-group) input:checkbox:checked").each(function () {
            TestIds.push($(this).closest('tr').find('td:last').text());
        });
    }
    if (logic == 'OutSource') {
        TestIds = [];
        $("#tblOutSourceTest tbody tr:not(.pat-group) input:checkbox:checked").each(function () {
            TestIds.push($(this).closest('tr').find('td:last').text());
        });
    }
    var Logic = 'ByFinalPrint';
    var url = config.rootUrl + "/Lab/print/PrintWorkSheet?visitNo=" + visitNo + "&SubCat=" + SubCat + "&TestIds=" + TestIds.join() + "&Logic=" + Logic;
    window.open(url, '_blank');
}





