var _DoctorId;
var _FloorName;
var _SelectedFloor;
var _SelectedRoomType;
var _SelectedNew;

$(document).ready(function () {
    CloseSidebar();
    LoadDietList();
    FillCurrentDateTime1();
    $('#btnInsertSchedule').prop('disabled', true);
    $('#btnRTFeedLog').prop('disabled', true);
    $('#btnsave').prop('disabled', true);
});
function FillCurrentDateTime1() {
    var today = '';
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();
    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;
    if (hour < 10) hour = "0" + hour;
    if (minute < 10) minute = "0" + minute;
    if (second < 10) second = "0" + second;
    today = year + "-" + month + "-" + day;
    $('#txtEndDate').val(today);
    $('#txtEndDate').attr('min', today);
    $('#txtstartDate').val(today);
    $('#txtstartDate').attr('min', today);
    return today;
}
function updateFilters() {
    $('#IPDPatientList .section').hide().filter(function () {
        var self = $(this),
            result = true;

        if (_SelectedFloor && (_SelectedFloor != 'ALL')) {
            result = result && _SelectedFloor === self.data('floor');
        }
        if (_SelectedRoomType && (_SelectedRoomType != 'ALL')) {
            result = result && _SelectedRoomType === self.data('roomtype');
        }
        if (_SelectedNew && (_SelectedNew != 'ALL')) {
            result = result && _SelectedNew === self.data('new');
        }
        return result;
    }).show();
}
function totalCount() {
    var totalLen = 0;
    $('#IPDPatientList .section').each(function () {
        if ($(this).css('display') != 'none')
            totalLen++;
    });
    $('#totaFilterCount').text('Total : ' + totalLen);
}
function PatientInfo(ipd) {
    $('#tblScheduledInfo tbody').empty();
    $('#txtHour').val('');
    $('#btnRTFeedLog').prop('disabled', false);
    $('#btnInsertSchedule').prop('disabled', false);
    var url = config.baseUrl + "/api/Dietician/diet_DiticianQueries";
    var objBO = {};
    objBO.IPDNo = ipd;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPatientInfoByIPDNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, patientInfo) {
                        $('#txtIPDNo').text(patientInfo.IPDNo);
                        $('#txtPatientName').text(patientInfo.PatientName);
                        $('#txtGender').text(patientInfo.Gender);
                        $('#txtContactNo').text(patientInfo.Mobile);
                        $('#txtAge').text(patientInfo.Age);
                        $('#txtDoctorName').text(patientInfo.DoctorName);
                        $('#txtDoctorId').text(patientInfo.DoctorId);
                        $('#txtMedicalProcedure').text(patientInfo.medicalProcedure);
                        $('#txtPreExistingDisease').text(patientInfo.PreExistingDisease);
                        $('#txtDietRecommended').html("<i class='fa fa-check-circle'>&nbsp;</i>" + patientInfo.DietType);
                        $('#txtFloorName').text(patientInfo.FloorName);
                        $('#txtRoomNo').text(patientInfo.RoomNo);
                        $('#txtBedNo').text(patientInfo.BedNo);
                    });

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Clear() {
    $('#txtUHID').text('');
    $('#txtIPDNo').text('');
    $('#txtAdmitDate').text('');
    $('#txtPatientName').text('');
    $('#txtAge').text('');
    $('#txtAgeType').text('');
    $('#txtGender').text('');
    $('#txtPreExistingDisease').text('');
    $('#txtDietRecommended').html('');
    $('#txtDoctorId').text('');
    $('#txtDoctorName').text('');
    $('#txtFloorName').text('');
    $('#txtContactNo').text('');
    $('#txtRoomType').text('');
    $('#txtRoomNo').text('');
    $('#txtBedNo').text('');
    $('#txtMedicalProcedure').text('');
}
function LoadDietList() {
    var url = config.baseUrl + "/api/Dietician/Diet_RTFeedQueries";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.DietId = '-';
    objBO.StartDateTime = '1900/01/01';
    objBO.FinishDateTime = '1900/01/01';
    objBO.Hourly = '0';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'LoadRTDiets';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddldiet').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddldiet').append($('<option></option>').val(val.DietId).html(val.DietName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ScheduledList() {
    $('#tblScheduledInfo tbody').empty();
    var hour = $('#txtHour').val();
    if (hour == '') {
        alert("Please Enter hour Duration")
        $("#txtHour").focus();
        return;
    }
    var url = config.baseUrl + "/api/Dietician/Diet_RTFeedQueries";
    var objBO = {};
    objBO.IPDNo = $('#txtIPDNo').text();
    objBO.DietId = $("#ddldiet option:selected").val();
    objBO.StartDateTime = $('#txtstartDate').val() + ' ' + $("#ddlCounthour").val() + ':00';
    objBO.FinishDateTime = $('#txtEndDate').val() + ' ' + $("#ddlCounthour1").val() + ':00';
    objBO.Hourly = $('#txtHour').val();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetSchedule';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var droplist = [];
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        droplist.push({ id: val.ItemId, name: val.ItemName });
                    });
                }
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width:10%;padding: 8px;'>" + val.Final_InShift + "</td>";
                        tbody += "<td style='width:15%'><select style='width:100%;margin-bottom: 3px;padding: 4px;' class='form-control'>";
                        $.each(droplist, function (index, item) {
                            tbody += "<option value='" + item.id + "'>" + item.name + "</option>";
                        });
                        tbody += "</select></td>";
                        tbody += "<td><input type='text' id='txtRemark' class='form-control' style='border:#2492b3 1px solid'></td>";
                        tbody += "<td style='width:6%'><button class='btn btn-danger btn-xs' onclick='deleteRow(this)'><i class='fa fa-remove'></i></button></td>";
                        tbody += "<td style='width:10%;padding: 8px;' hidden>" + val.ShiftValue + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblScheduledInfo tbody').append(tbody);
                    $('#btnsave').prop('disabled', false);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function deleteRow(el) {
    $(el).closest('tr').remove();
}
function InsertSchedule() {
    debugger
    var url = config.baseUrl + "/api/Dietician/diet_InsertScheduleRTFeed";
    var objBO = [];
    $('#tblScheduledInfo tbody tr').each(function () {
        objBO.push({
            'IPDNo': $('#txtIPDNo').text(),
            'DietId': $('#ddldiet option:selected').val(),
            'ItemId': $(this).find('td:eq(1) select option:selected').val(),
            'StartDate': $('#txtstartDate').val() + ' ' + $("#ddlCounthour").val() + ':00',
            'FinishDate': $('#txtEndDate').val() + ' ' + $("#ddlCounthour1").val() + ':00',
            'ServingDateTime': $(this).closest('tr').find('td:eq(4)').text(),
            'Remark': $(this).closest('tr').find('td:eq(2) input#txtRemark').val(),
            'login_id': Active.userId,
            'Logic': 'InsertScheduleRTFeed',

        });
    });
    $.ajax({
        method: "POST",
        url: url,
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(objBO),
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                $('#btnsave').prop('disabled', true);
                $('#txtHour').val('');
                $('select:not(#ddldiet)').prop('selectedIndex', '0').change();
                $('#tblScheduledInfo tbody').empty();
            }
            else {
                alert(data);
            }
        }
    });
}
function RTFeedDetailsLog() {
    $('#RTFeedDetailsLog').modal('show');
}

function IPDNOWiseInfo(ipdno) {
    debugger
    $('#tblIDNOWiseInfo tbody').empty();
    var url = config.baseUrl + "/api/Dietician/Diet_RTFeedQueries";
    var objBO = {};
    objBO.IPDNo = ipdno;
    objBO.DietId = '-';
    objBO.StartDateTime = '1900/01/01 00:00';
    objBO.FinishDateTime = '1900/01/01 00:00';
    objBO.Hourly = '0';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetRTFeedIPdNoWise';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var temp = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtIPDNoInfo').text(val.IPDNo);
                        $('#txtPatientNameInfo').text(val.patient_name);
                        $('#txtStartdateInfo').text(val.StartDate);
                        $('#txtEnddateInfo').text(val.FinishDate);
                        if (temp != val.ServingDateTime1) {
                            tbody += "<tr style='background:#e5e5e5;'>";
                            tbody += "<td colspan='12' style='font-size:13px;'><b>" + val.ServingDateTime1 + "</b></td>";
                            tbody += "</tr>";
                            temp = val.ServingDateTime1
                        }
                        tbody += "<tr>";

                        tbody += "<td>" + val.ServingDateTime + "</td>";
                        tbody += "<td>" + val.DietName + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "<td>" + val.Scheduledate + "</td>";
                        tbody += "<td style='text-align:center'>" + val.IsFreezed + "</td>";
                        tbody += "<td>" + val.FreezedBy + "</td>";
                        tbody += "<td>" + val.FreezedDate + "</td>";
                        tbody += "<td style='text-align:center'>" + val.IsLocked + "</td>";
                        tbody += "<td>" + val.IsLockedBy + "</td>";
                        tbody += "<td>" + val.LockedDate + "</td>";

                        tbody += "</tr>";
                    });
                    $('#tblIDNOWiseInfo tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

