﻿$(document).ready(function () {
    FillCurrentDate('txtfrom');
    FillCurrentDate('txtTo');
    $('#tblMedicalCertificate tbody').on('click', '.EditRow', function () {
        debugger
        var Autoidd = $(this).closest('tr').find('td:eq(1)').text();
        var UhidNo = $(this).closest('tr').find('td:eq(2)').text();
        EditRowSingleBy(Autoidd, UhidNo);

    });
});
function Printdata(Autoid) {
    debugger
    var UHIDNo = $("#txtUHIDNo").val();
    var url = "../Print/MedicalCertificate?UHID_No=" + UHIDNo + "&Autoid=" + Autoid;
    window.open(url, '_blank');

}
function GetdataByUHID() {
    $('#tblPatientByUHID tbody').empty();
    var url = config.baseUrl + "/api/Appointment/OPD_MedicalCertificateQueries";
    var objBO = {};
    objBO.Auto_id = '0';
    objBO.UHID = $("#txtUHIDNo").val();
    objBO.IPOPD_NO = '';
    objBO.Chief_Complaint = '-';
    objBO.Remark = '-';
    objBO.login_id = Active.userId;
    objBO.dFrom = $("#txtfrom").val();
    objBO.dTo = $("#txtTo").val();
    objBO.Logic = 'PatientListByUHID';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.PatientName + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.relation_name + "</td>";
                        tbody += "<td>" + val.address + "</td>";
                        tbody += "<td>" + val.state + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientByUHID tbody').append(tbody);
                    GetMedicalCertificateByUHID();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetMedicalCertificateByUHID() {
    $('#tblMedicalCertificate tbody').empty();
    var url = config.baseUrl + "/api/Appointment/OPD_MedicalCertificateQueries";
    var objBO = {};
    objBO.Auto_id = '0';
    objBO.UHID = $("#txtUHIDNo").val();
    objBO.IPOPD_NO = '';
    objBO.Chief_Complaint = '-';
    objBO.Remark = '-';
    objBO.login_id = Active.userId;
    objBO.dFrom = $("#txtfrom").val();
    objBO.dTo = $("#txtTo").val();
    objBO.Logic = 'OnLoadMedicalCertificate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += '<td><button class="btn-warning" onclick="Printdata(' + val.Auto_id + ')" style="border:none;height:20px;margin-bottom: 3px;">Print</button></td>';
                        tbody += "<td hidden>" + val.Auto_id + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPOPD_NO + "</td>";
                        tbody += "<td>" + val.Chief_Complaint + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "<td>" + val.dFrom + "</td>";
                        tbody += "<td>" + val.dTo + "</td>";
                        tbody += '<td><button class="btn-success EditRow" style="border:none;height:20px;margin-bottom: 3px;">Edit</button></td>';

                        tbody += "</tr>";
                    });
                    $('#tblMedicalCertificate tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertMedicalData() {
    var isConfirmed = confirm('Are you sure you want to Save the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/Appointment/OPD_InsertMedicalCertificate";
        var objBO = {};
        objBO.Auto_id = '0';
        objBO.UHID = $("#txtUHIDNo").val();
        objBO.IPOPD_NO = $("#txtIPDNO").val();
        objBO.Chief_Complaint = $("#txtChiefComplaint").val();
        objBO.Remark = $("#txtRemark").val();
        objBO.login_id = Active.userId;
        objBO.dFrom = $("#txtfrom").val();
        objBO.dTo = $("#txtTo").val();
        objBO.Logic = "Insert";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    GetMedicalCertificateByUHID();
                    $("#txtIPDNO").val('');
                    $("#txtChiefComplaint").val('');
                    $("#txtRemark").val('');
                    $("#txtDate").val('');


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
    else {
        alert('Data Save canceled.');
    }

}
function EditRowSingleBy(Autoidd, UhidNo) {
    debugger
    var url = config.baseUrl + "/api/Appointment/OPD_MedicalCertificateQueries";
    var objBO = {};
    objBO.Auto_id = Autoidd;
    objBO.UHID = UhidNo;
    objBO.IPOPD_NO = '-';
    objBO.Chief_Complaint = '-';
    objBO.Remark = '-';
    objBO.login_id = Active.userId;
    objBO.dFrom = $("#txtfrom").val();
    objBO.dTo = $("#txtTo").val();
    objBO.Logic = 'EditMedicalCertificateByUHID';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.Auto_id != "") {
                            $("#txtautoid").text(data.ResultSet.Table[0].Auto_id);
                            var dateFrom = data.ResultSet.Table[0].dFrom.split('/')[2] + '-' + data.ResultSet.Table[0].dFrom.split('/')[1] + '-' + data.ResultSet.Table[0].dFrom.split('/')[0];
                            $("#txtfrom").val(dateFrom);
                            var dateto = data.ResultSet.Table[0].dTo.split('/')[2] + '-' + data.ResultSet.Table[0].dTo.split('/')[1] + '-' + data.ResultSet.Table[0].dTo.split('/')[0];
                            $("#txtTo").val(dateto);
                            $("#txtIPDNO").val(data.ResultSet.Table[0].IPOPD_NO);
                            $("#txtChiefComplaint").val(data.ResultSet.Table[0].Chief_Complaint);
                            $("#txtRemark").val(data.ResultSet.Table[0].Remark);
                            $("#btnUpdate").show();
                            $("#btnSave").hide();

                        }
                        else {
                            $("#btnSave").show();
                            $("#btnUpdate").hide();
                        }

                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateMedical() {
    var isConfirmed = confirm('Are you sure you want to Update the data?');
    if (isConfirmed) {
        var id = $("#txtautoid").text();
        var url = config.baseUrl + "/api/Appointment/OPD_InsertMedicalCertificate";
        var objBO = {};
        objBO.Auto_id = id;
        objBO.UHID = $("#txtUHIDNo").val();
        objBO.IPOPD_NO = $("#txtIPDNO").val();
        objBO.Chief_Complaint = $("#txtChiefComplaint").val();
        objBO.Remark = $("#txtRemark").val();
        objBO.login_id = Active.userId;
        objBO.dFrom = $("#txtfrom").val();
        objBO.dTo = $("#txtTo").val();
        objBO.Logic = "Update";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    GetMedicalCertificateByUHID();
                    $("#txtIPDNO").val('');
                    $("#txtChiefComplaint").val('');
                    $("#txtRemark").val('');
                    $("#txtDate").val('');
                    $("#btnUpdate").hide();
                    $("#btnSave").show();
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
    else {
        alert('Data Update canceled.');
    }

}