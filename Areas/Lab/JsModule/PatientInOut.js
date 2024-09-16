var _HoldAutoTestId = null;
var _currIdx = null;
$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    GetDepartmentInfo();
});
function GetDepartmentInfo() {
    $("#ddlCategory").empty().append($("<option></option>").val('ALL').html('ALL')).select2();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.prm_1 = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.logic = "GetDepartmentInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, value) {
                    $("#ddlCategory").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientInfoForInOut() {
    $('#tblPatientInfo tbody').empty();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.VisitNo = '-';
    objBO.BarcodeNo = $('#ddlStatus option:selected').val();
    objBO.DispatchLabCode = '-';
    objBO.TestCode = $('#ddlType option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#ddlCategory option:selected').val();
    objBO.logic = "PatientInfoForInOut";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            var patientname = "";
            var DispatchLab = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (val.InOutStatus == 'In')
                            tbody += "<tr style='background:#b8daff'>";
                        else if (val.InOutStatus == 'Out')
                            tbody += "<tr style='background:#bbebbb'>";
                        else if (val.InOutStatus == 'Hold')
                            tbody += "<tr style='background:#fdffb8'>";
                        else
                            tbody += "<tr>";


                        tbody += "<td>" + count + "</td>";
                        tbody += "<td><button onclick=Print(this) class='btn btn-warning btn-xs'><i class='fa fa-print'>&nbsp;</i></button>&nbsp;" + val.VisitNo + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.RegDate + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td>" + val.Dept + "</td>";

                        if ($.inArray(val.InOutStatus, ['Pending', 'Hold']) > -1)
                            tbody += "<td><button data-logic='LabPatientIn' onclick=InOutMarking(this) class='btn btn-success btn-xs'><i class='fa fa-sign-in'>&nbsp;</i>In</button></td>";
                        else
                            tbody += "<td>" + val.InFlag + "</td>";

                        tbody += "<td><button onclick=holdCred(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'>&nbsp;</i>Hold</button></td>";

                        if (val.OutFlag == null && val.InOutStatus != 'Hold')
                            tbody += "<td><button data-logic='LabPatientOut' onclick=InOutMarking(this) class='btn btn-danger btn-xs'><i class='fa fa-sign-out'>&nbsp;</i>Out</button></td>";
                        else
                            tbody += "<td>" + val.OutFlag + "</td>";

                        tbody += "<td>" + val.IPOPType + "</td>";
                        tbody += "<td style='display:none'>" + val.VisitNo + "</td>";
                        tbody += "<td style='display:none'>" + val.AutoTestId + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientInfo tbody').append(tbody);
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
function holdCred(elem) {
    _currIdx = elem;
    _HoldAutoTestId = $(elem).closest('tr').find('td:last').text();
    $('#modalHold').modal('show')
}
function Print(elem) {
    var visitNo = $(elem).closest('tr').find('td:nth-last-child(2)').text();
    var SubCat = 'ALL';
    var TestIds = $(elem).closest('tr').find('td:last').text();;
    var Logic = 'ByFinalPrint';
    var url = config.rootUrl + "/Lab/print/PrintWorkSheet?visitNo=" + visitNo + "&SubCat=" + SubCat + "&TestIds=" + TestIds + "&Logic=" + Logic;
    window.open(url, '_blank');
}
function InOutMarking(elem) {
    var url = config.baseUrl + "/api/Appointment/Opd_InOutMarking";
    var objBO = {};
    objBO.DoctorId = Active.doctorId;
    objBO.BookingNo = $(elem).closest('tr').find('td:nth-last-child(2)').text();
    objBO.inputDate = '1900/01/01';
    objBO.Prm1 = $(elem).closest('tr').find('td:nth-last-child(1)').text();
    objBO.LoginId = Active.userId;
    objBO.Logic = $(elem).data('logic');
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                var InOutStatus = data.split('|')[1];

                if (InOutStatus == 'In')
                    $(elem).closest('tr').removeAttr('style').css('background', '#b8daff');
                else if (InOutStatus == 'Out')
                    $(elem).closest('tr').removeAttr('style').css('background', '#bbebbb');
                else if (InOutStatus == 'Hold')
                    $(elem).closest('tr').removeAttr('style').css('background', '#fdffb8');
                else
                    $(elem).closest('tr').removeAttr('style');

                if ($.inArray(InOutStatus, ['Pending', 'Hold']) > -1)
                    $(elem).closest('tr').find('td:eq(8) button').show();
                else if (InOutStatus == 'In') {
                    $(elem).closest('tr').find('td:eq(10) button').show();
                    $(elem).closest('tr').find('td:eq(8) button').hide();
                }
                else
                    $(elem).closest('tr').find('td:eq(8) button').hide();

                if (InOutStatus == 'Hold')
                    $(elem).closest('tr').find('td:eq(10) button').hide();
                else if (InOutStatus == 'Out')
                    $(elem).closest('tr').find('td button').hide();
            }   
            else {
                alert(data);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LabPatientHold() {
    if ($('#txtHoldRemark').val() == '') {
        alert('Please Provide Remark')
        return;
    }
    var url = config.baseUrl + "/api/Appointment/Opd_InOutMarking";
    var objBO = {};
    objBO.DoctorId = Active.doctorId;
    objBO.BookingNo = _HoldAutoTestId;
    objBO.inputDate = '1900/01/01';
    objBO.Prm1 = $('#txtHoldRemark').val();
    objBO.LoginId = Active.userId;
    objBO.Logic = 'LabPatientHold';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                $('#modalHold').modal('hide')
                var InOutStatus = data.split('|')[1];

                if (InOutStatus == 'In')
                    $(_currIdx).closest('tr').removeAttr('style').css('background', '#b8daff');
                else if (InOutStatus == 'Out')
                    $(_currIdx).closest('tr').removeAttr('style').css('background', '#bbebbb');
                else if (InOutStatus == 'Hold')
                    $(_currIdx).closest('tr').removeAttr('style').css('background', '#fdffb8');
                else
                    $(_currIdx).closest('tr').removeAttr('style');

                if ($.inArray(InOutStatus, ['Pending', 'Hold']) > -1)
                    $(_currIdx).closest('tr').find('td:eq(8) button').show();
                else if (InOutStatus == 'In') {
                    $(_currIdx).closest('tr').find('td:eq(10) button').show();
                    $(_currIdx).closest('tr').find('td:eq(8) button').hide();
                }         
                else
                    $(_currIdx).closest('tr').find('td:eq(8) button').hide();

                if (InOutStatus == 'Hold')
                    $(_currIdx).closest('tr').find('td:eq(10) button').hide();
                else if (InOutStatus == 'Out')
                    $(_currIdx).closest('tr').find('td button').hide();
            }
            else {
                alert(data);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}