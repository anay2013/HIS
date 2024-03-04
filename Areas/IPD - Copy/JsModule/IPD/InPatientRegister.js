var _ActivePageName;
var _IPDNoForPrint;
$(document).ready(function () {
    CloseSidebar();
    FloorAndPanelList();
    $('select').select2();
    FillCurrentDate('txtFilterFrom');
    FillCurrentDate('txtFilterTo');
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    _ActivePageName = window.location.pathname.split('/').pop().toLowerCase()
    GetDoctorList();
});
function GetDoctorList() {
    $('#ddlDoctor').empty().select2();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = Active.doctorId;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetDoctorForConsult';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName)).change();
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FloorAndPanelList() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'FloorAndPanelList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlFloor').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlFloor').append($('<option></option>').val(val.FloorName).html(val.FloorName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlPanel').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlWard').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlWard').append($('<option></option>').val(val.RoomTypeId).html(val.RoomTypeName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AdmittedPatientList(logic) {
    $('#tblServiceRegister tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = $('#txtUHID').val();
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = (logic != 'PatientInfoByPatientName') ? $('#txtSearchFrom').val() : $('#txtFilterFrom').val();
    objBO.to = (logic != 'PatientInfoByPatientName') ? $('#txtSearchTo').val() : $('#txtFilterTo').val();
    objBO.Prm1 = (logic == 'PatientInfoByPatientName') ? $('#txtPatientName').val() : $('#ddlIpdStatus option:selected').val();
    objBO.Prm2 = $('#ddlDoctor option:selected').val();
    objBO.Prm3 = $('#ddlWard option:selected').val();
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#modalSearch').modal('hide');
            $('#txtUHID').val('')
            $('#txtPatientName').val('')
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.PaymentStatus == 'Zero-Advance')
                            tbody += "<tr style='background:#ffb9b9'>";
                        else if (val.PaymentStatus == 'Below-Threshold')
                            tbody += "<tr style='background:#f2fba6'>";
                        else if (val.IpdStatus == 'Out')
                            tbody += "<tr style='background:#ffaeae'>";
                        else
                            tbody += "<tr>";

                        if (_ActivePageName == 'ipd_nursingpatientregister')
                            tbody += "<td><button onclick=NursingGateway('" + val.IPDNo + "') class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        else if (_ActivePageName == 'ipd_billingpatientregister')
                            tbody += "<td><button onclick=BillingGateway('" + val.IPDNo + "') class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        else if (_ActivePageName == 'ipd_doctorpatientregister')
                            tbody += "<td><button onclick=DoctorGateway('" + val.IPDNo + "') class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";

                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td><button onclick=patientDetails(this) class='btn btn-warning btn-xs'><i class='fa fa-user'></i></button></td>";
                        tbody += "<td class='hide'>" + val.IsDischSummaryLocked + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblServiceRegister tbody').append(tbody);
                }

                var tCount = Object.keys(data.ResultSet.Table).length
                $('#tCount').html("Total Ipd: "+tCount)  
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function patientDetails(elem) {
    _IPDNoForPrint = $(elem).closest('tr').find('td:eq(2)').text();
    var islocked = $(elem).closest('tr').find('td:last').text();
    if (islocked == 'Y')
        $('#btnPrintBill').prop('disabled', false);
    else
        $('#btnPrintBill').prop('disabled', true);

    $('#tblPatientInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $(elem).closest('tr').find('td:eq(2)').text();;
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PatientRegister:ByIPDNo';
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
                        tbody += "<td>" + val.AdmissionType + "</td>";
                        tbody += "<td>" + val.RoomType + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.AttendantContactNo + "</td>";
                        tbody += "<td>" + val.MLCType + "</td>";
                        tbody += "<td>" + val.RefName + "</td>";
                        tbody += "<td>" + val.PatientType + "</td>";
                        tbody += "<td>" + val.PolicyNo + "</td>";
                        tbody += "<td>" + val.SourceName + "</td>";
                        tbody += "<td>" + val.AdmittedDate + "</td>";
                        tbody += "<td>" + val.AdmittedBy + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.DischargeBy + "</td>";

                        tbody += "</tr>";
                    });
                    $('#tblPatientInfo tbody').append(tbody);
                    $('#modalPatientInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Receipt_IPDDischargeReport() {
    var url = "../Print/IPDDischargeReport?_IPDNo=" + _IPDNoForPrint;
    window.open(url, '_blank');
}
function NursingGateway(ipdNo) {
    //SM229 --Nursing Ward 
    var url = "GatewayNursing?mid=SM229&IPDNo=" + ipdNo;
    window.location.href = url;
}
function DoctorGateway(ipdNo) {
    //SM229 --Nursing Ward 
    var url = "GatewayDoctor?mid=SM264&IPDNo=" + ipdNo;
    window.location.href = url;
}
function BillingGateway(ipdNo) {
    //SM229 --Nursing Ward 
    var url = "GatewayBilling?mid=SM263&IPDNo=" + ipdNo;
    window.location.href = url;
}