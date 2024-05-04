var counterP = 300;
var _ipdNo = "";
var selectedRow;
$(document).ready(function () {
    CloseSidebar();
    FloorAndPanelList();
    GetDoctorList();
    $(document).on("click", "#tblPatientList button ", function () {
        _ipdNo = $(this).closest("tr").find('td:eq(2)').text();
        if ($(this).text() == "Call") {
            selectedRow = $(this).closest("tr");
            var PatientName = $(this).closest("tr").find('td:eq(3)').text();
            var PanelName = $(this).closest("tr").find('td:eq(14)').text();
            var AdmitDATE = $(this).closest("tr").find('td:eq(15)').text();
            var s = '<table class="table table-bordered">';
            s += '<tr>';
            s += '<td> IPD No </td>';
            s += '<td>' + _ipdNo + '</td>';
            s += '</tr>';
            s += '<tr>';
            s += '<td> Patient Name </td>';
            s += '<td>' + PatientName + '</td>';
            s += '</tr>';
            s += '<tr>';
            s += '<td> Panel Name </td>';
            s += '<td>' + PanelName + '</td>';
            s += '</tr>';
            s += '<td> Admission Date </td>';
            s += '<td>' + AdmitDATE + '</td>';
            s += '</tr>';
            s += '</table>';

            $("#pInfo").html(s)
            $('#myModal').modal('show');

            CallLog();
        }
        else {
            var MinAlertAmount = $(this).closest("tr").find('input[name="MinAlertAmount"]').val();
            var RepeatCallMinute = $(this).closest("tr").find('input[name="RepeatCallMinute"]').val();
            UpdateTimeAndAmount(MinAlertAmount, RepeatCallMinute);

        }
        //let a =tr.find('input[name="fullname"]').val(); -- For Getting Row
    });
});
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
            //if (Object.keys(data.ResultSet).length) {
            //    if (Object.keys(data.ResultSet.Table2).length) {
            //        $('#ddlWard').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
            //        $.each(data.ResultSet.Table2, function (key, val) {
            //            $('#ddlWard').append($('<option></option>').val(val.RoomTypeId).html(val.RoomTypeName));
            //        });
            //    }
            //}
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
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
function OverDuePatientList() {
    var ct = 1;
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '-';
    objBO.DoctorId = $('#ddlDoctor option:selected').val();
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = Active.userName;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'CallOverDuePatientList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var htmldata = "";
            $('#tblPatientList tbody').empty();
            if (data.ResultSet.Table.length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    htmldata += '<tr style="background-color:' + val.CallStatus + '">';
                    htmldata += '<td style="width:2%;text-align:left">' + ct + '</td>';
                    htmldata += '<td style="width:3%;text-align:center"><button type="button" class="btn btn-warning btn-block" style="height:20px;font-size:9px;" " >Call</button></td>';
                    htmldata += '<td style="width:4%;text-align:left">' + val.IPDNo + '</td>';
                    htmldata += '<td style="width:14%;text-align:left">' + val.patient_name + '</td>';
                    htmldata += '<td style="width:5%;text-align:left">' + val.mobile_no + '</td>';
                    htmldata += '<td style="width:12%;text-align:left">' + val.DoctorName + '</td>';
                    htmldata += '<td style="width:8%;text-align:left">' + val.RoomBillingCategory + '</td>';
                    htmldata += '<td style="width:5%;text-align:right">' + val.Advance + '</td>';
                    htmldata += '<td style="width:5%;text-align:right">' + val.BillAmt.toFixed(0) + '</td>';
                    htmldata += '<td style="width:5%;text-align:right">' + val.ExtraPayment.toFixed(0) + '</td>';
                    htmldata += "<td style='width:5%'><input type='text' name='MinAlertAmount' class='text-center' style='background-color:#d8f1f5;width:100%' value='" + val.MinAlertAmount + "'/></td>";
                    htmldata += "<td style='width:5%'><input type='text' name='RepeatCallMinute' class='text-center' style='background-color:#d8f1f5;width:100%' value='" + val.RepeatCallMinute + "'/></td>";
                    htmldata += '<td style="width:3%;text-align:center"><button  type="button" class="btn btn-info btn-block" style="height:20px;font-size:9px;">Save</button></td>';
                    htmldata += '<td style="width:10%;text-align:center">' + val.lastCallDate + '</td>';
                    htmldata += '<td style="width:15%;text-align:left;">' + val.PanelName + '</td>';
                    htmldata += '<td style="width:0%;text-align:left;display:none;">' + val.AdmitDate + '</td>';
                    htmldata += '</tr>';
                    ct++;
                });
                $('#tblPatientList tbody').append(htmldata);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}

function ExcelOverPatient() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '-';
    objBO.DoctorId = $('#ddlDoctor option:selected').val();
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = Active.userName;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'CallOverDuePatientList';
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "CallOverDuePatientList" + ".xlsx");
}
function CallOverDueInsertUpdate() {
    var ct = 1;
    var url = config.baseUrl + "/api/Patient/CallOverDueInsertUpdate";
    var objBO = {};
    objBO.IpdNo = _ipdNo;
    objBO.Remark = $("#txtRemark").val();
    objBO.LoginName = Active.userName;
    objBO.Prm1 = '-';
    objBO.MinAlertAmount = '-';
    objBO.RepeatCallMinute = '0';
    objBO.Logic = 'MarkCallDone';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            selectedRow.css("background-color", "#ceee90");
            CallLog();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function UpdateTimeAndAmount(MinAlertAmount, RepeatCallMinute) {
    debugger
    var ct = 1;
    var url = config.baseUrl + "/api/Patient/CallOverDueInsertUpdate";
    var objBO = {};
    objBO.IpdNo = _ipdNo;
    objBO.Remark = '-';
    objBO.LoginName = Active.userName;
    objBO.Prm1 = '-';
    objBO.MinAlertAmount = MinAlertAmount;
    objBO.RepeatCallMinute = RepeatCallMinute;
    objBO.Logic = 'UpdateAmountAndTime';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function CallLog() {
    var ct = 1;
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _ipdNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = Active.userName;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetCallLog';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblCallLog tbody').empty();
            if (Object.keys(data.ResultSet).length) {
                var htmldata = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        htmldata += '<tr>';
                        htmldata += '<td style="width:3%;text-align:left">' + ct + '</td>';
                        htmldata += '<td style="width:5%;text-align:left" hidden>' + val.IPDNo + '</td>';
                        htmldata += '<td style="width:5%;text-align:left">' + val.CallDate + '</td>';
                        htmldata += '<td style="width:15%;text-align:left">' + val.Remark + '</td>';
                        htmldata += '<td style="width:5%;text-align:left">' + val.login_name + '</td>';
                        htmldata += '</tr>';
                        ct++;
                    });
                    $('#tblCallLog tbody').append(htmldata);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}


