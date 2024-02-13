var _photo_url = null;
var isApp = '';
var roundOff = 0;
var _empDiscountInfo = {};
var _otp;
var _GenFrom = "Hospital";
var _online_app_no = "-";
$(document).ready(function () {
    // data = { "messages": [{ "messageId": "40089288737037212625", "status": { "description": "Message sent to next instance", "groupId": 1, "groupName": "PENDING", "id": 7, "name": "PENDING_ENROUTE" }, "to": "919670244590", "smsCount": 1 }] }
    TriggerEnter();
    CloseSidebar();
    isApp = window.location.href;
    if (isApp.includes('?appno')) {
        var appno = atob(query()["appno"]);
        if (appno != '') {
            GetBookingByAppNo(appno);
            _GenFrom = 'Online-Phone';
            _online_app_no = appno;
        }
    }
    $('.paymentSection').slideToggle('slow'); $('#PaymentDivision,.serviceInfo').toggleClass('itemHeight')
    BookAppointmentQueries();
    $('input[type=checkbox]').on('change', function () {
        isCheck = $(this).is(':checked');
        var val = $(this).val();
        var len = $('input[name=PaymentMode]:checked').length;
        var pay = $('#txtPayable').val();

        var tbody = "";
        if (isCheck) {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == val.toLowerCase()) {
                    $(this).show();
                    $(this).addClass('pay');
                }
            });
            $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
            $('#tblPaymentDetails tbody').find('tr').filter('.pay').first().find('td:eq(1)').find('input[type=text]').val(pay);
        }
        else {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == val.toLowerCase()) {
                    $(this).hide();
                    $(this).removeClass('pay');
                }
            });
            $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
            $('#tblPaymentDetails tbody').find('tr').filter('.pay').first().find('td:eq(1)').find('input[type=text]').val(pay);
        }
    });
    $('#ddlPanel').on('change', function () {
        var val = $(this).val();
        var IsCredit = $(this).find('option:selected').data('iscredit');
        if (IsCredit) {
            $('input[name=PaymentMode]').prop('checked', false).trigger('change.select2');
            $('input[name=PaymentMode]').prop('disabled', true);
            $('input[value=Credit]').prop('checked', true).trigger('change.select2');
            $('input[name=PaymentMode]').removeClass('pay');
            $('#ddlItemName').prop('selectedIndex', '0').trigger('change.select2');
            $('#tblPaymentDetails').removeClass('pay');
            $('#tblPaymentDetails tbody tr').hide();
        }
        else {
            $('#tblPaymentDetails tbody tr').each(function () {
                if ($(this).find('td:eq(0)').text().toLowerCase() == 'cash') {
                    $(this).show();
                }
                //$(this).addClass('pay');	
            });
            $('input[value=Cash]').prop('checked', true).trigger('change.select2');
            $('input[value=Credit]').prop('checked', false).trigger('change.select2');
            $('input[name=PaymentMode]').prop('disabled', false);
            $('#ddlItemName').prop('selectedIndex', '0').trigger('change.select2');
        }
        PaymentAll();
    });
    $('button[id=btnDoctorSchedule]').on('click', function () {
        doctor = $('#ddlDoctor option:selected').val();
        GetDoctorSchedule(doctor);
    });
    $('#ddlDoctor').on('change', function () {
        var DoctorId = $(this).find('option:selected').val();
        GetVisitTypeByDoctor(DoctorId);
    });
    $('#ddlItemName').on('change', function () {
        item = $(this).val();
        panelId = $('#ddlPanel option:selected').val();
        var UHID = $('#txtUHID').val();
        GetAppointmentRate(UHID, panelId, item);
        $('.employeeDiscount').removeClass('lock');
    });
    $('input[value=Availbility]').on('click', function () {
        var val = $('#ddlState option:selected').text();
        alert(val);
    });
    $('#tblPaymentDetails tbody').on('keyup', 'input[type=text]', function () {
        var netAmount = parseFloat($('#txtPayable').val());
        var cash = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val());
        var cheque = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(1)').find('td:eq(1)').find('input[type=text]').val());
        var cc = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(2)').find('td:eq(1)').find('input[type=text]').val());
        var ntfs = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(3)').find('td:eq(1)').find('input[type=text]').val());
        var total = cash + cheque + cc + ntfs;
        if (total > netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be more than Payable Amount...!').css({ 'color': 'red', 'font-size': '11px' });
        }
        else if (total < netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be less than Payable Amount...!').css({ 'color': '#bb8202', 'font-size': '11px' });
        }
        else {
            $('#tblPaymentDetails tbody').find('tr:eq(0),tr:eq(1),tr:eq(2),tr:eq(3)').find('td:eq(1)').find('input[type=text]').removeAttr('style');
            $('#txtError').text('').removeAttr('style');
        }
    });
    $('#tblOnlinePatientBooking tbody').on('click', 'button', function () {
        var info = JSON.parse($(this).closest('tr').find('td:eq(1)').text());
        FillOnlinePatientInfo(info);
        $('#modalOnlinePatient').hide();
        PaymentAll();
    });
    $('#ddlDept').on('change', function () {
        GetDoctorByDept();
    });
    $('#btnNewPatient').on('click', function () {
        $('#BasicInformation').find('.infosection').removeClass('blockInfo');
        $('#txtUHID').val('New');
        $('#txtMobileNo').val($('#txtSearchValue').val());
        $('#txtSearchValue').val('');
        $('#modalOldPatient').modal('hide');
        $('#btnNewPatient').hide();
        $('.patientinfo-section-1,.patientinfo-section-2').removeClass('blockUI');
    });
    $('#ddlVisitType').on('change', function () {
        val = $(this).val();
        if (val != 'Walk-In') {
            $(".paymentSection").addClass('disabled');
            $(".paymentSection input:text").val(0);
            $(".paymentSection select").prop('selectedIndex', '0').trigger('change.select2').removeClass('border-imp');
            $('.paymentSection input[name=PaymentMode]').prop('checked', false).trigger('change.select2');
            $('#txtError').text('');
        }
        else {
            $(".paymentSection").removeClass('disabled');
        }
        //if (val != 'Walk-In') {
        //	$("#PaymentDivision").hide("slide", { direction: "right" }, 800, function () {
        //		$('#ControlUnit').show("slide", { direction: "right" }, 800);
        //	});
        //}
        //else {
        //	$("#PaymentDivision").show("slide", { direction: "right" }, 800, function () {
        //		$('#ControlUnit').hide("slide", { direction: "right" }, 800);
        //	});
        //}
    });
    $('.TimeFrame').on('click', 'span.TimeSlot:not([disabled])', function () {
        //var time = $(this).not('span').text();
        var time = $(this).text().substring(0, 17);
        var date = $('#txtTimeSlotsByDate').val();
        $('#txtTime').val(time);
        $('#txtAppointmentOn').val(date);
        $('#modalTimeAvailability').modal('hide');
        $('#txtTimeSlotsByDate').val('');
        $('.TimeFrame').empty('');
        $('#modalTimeAvailability .TimeFrame').empty();
        PaymentAll();
    });
    $('#txtTimeSlotsByDate').on('change', function () {
        var date = $(this).val();
        DoctorTimeSlots(date);
    });
    $('#txtSearchReferral').on('keyup', function (e) {
        var referral = $(this).val();
        ReferralSearch(referral);
        var tbody = $('#tblReferral tbody');
        var tr = $(tbody).find('tr.select-row');
        if (e.keyCode == 40) {
            if (tr.length == 0) {
                $(tbody).removeClass('select-row');
                $(tbody).find('tr:first').addClass('select-row');
            }
            $(tbody).removeClass('select-row');
            $(tr).next().find('tr:eq(' + index + ')').addClass('select-row');
        }
        else if (e.keyCode == 38) {
            index--;
            $(tbody).removeClass('select-row');
            $(tbody).find('tr:eq(' + index + ')').addClass('select-row');
        }
    });
    $('#tblReferral tbody').on('click', 'button', function () {
        var referral = $(this).closest('tr').find('td:eq(1)').text();
        var referralName = $(this).closest('tr').find('td:eq(1)').text();
        $('#ddlReferral').append($('<option></option>').val(referral).html(referralName));
        $('#ddlReferral').val(referral).trigger('change.select2');
        $('#modalReferral').modal('hide');
        $('#modalReferral input[type=text]').val('');
        $('#tblReferral tbody').empty();
    });
    $('input[value=Availability]').on('click', function () {
        var doctorid = $('#ddlDoctor option:selected').text();
        var date = $('#txtAppointmentOn').val();
        if (doctorid == 'Select') {
            alert('Please Select Doctor..');
            $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').css('border-color', 'red').focus();
        }
        else {
            $('#modalTimeAvailability').modal('show');
            $('#txtTimeSlotsByDate').val(date);
            DoctorTimeSlots(date);
        }
    });
    $('#btnPrintReceipt').on('click', function () {
        var tnxid = $('#txtTnxIdForPrint').text();
        Receipt(tnxid);
    });

    $('#tblItemInfo tbody').on('keyup', 'input:text', function () {
        var elem = $(this);
        var tag = $(this).data('tag');
        ItemWiseDiscount(elem, tag);
    });
    //When Is Zero 1
    $('#tblItemInfo tbody').on('blur keyup paste input', 'td:eq(4)', function () {
        var elem = $(this);
        let val = ($(this).text() == '') ? 0 : $(this).text();
        $(this).closest('tr').find('td:eq(2)').text(val);
        ItemWiseDiscount(elem, 'ByAmt');
        totalCal();
    });
    $('.nullInfo').on('click', function () {
        $('#tblItemInfoNull').slideToggle('slow');
    });
    $('#tblItemInfo tbody').on('click', 'button', function () {
        let itemid = $(this).closest('tr').find('td:eq(0)').text();
        let index = itemIds.indexOf(itemid);
        itemIds.splice(index, 1);
        $(this).closest('tr').remove();
        totalCal();
    });
    $('#tblItemInfoNull tbody').on('click', 'button', function () {
        $(this).closest('tr').remove();
        if ($('#tblItemInfoNull tbody tr').length > 0) {
            $('.nullInfo').show();
        }
        else {
            $('.nullInfo').hide();
            $('#tblItemInfoNull').hide();
        }
    });
});
function checkAmount() {
    if ($('#txtUHID').val() == 'New') {
        alert('Please Provide UHID');
        $('#txtUHID').focus();
        return;
    }
    if ($('#ddlPanel option:selected').val() == '000') {
        alert('Please Select Panel');
        return;
    }
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.UHID = $('#txtUHID').val();
    objBO.from = '1900/01/01';
    objBO.ReportType = '';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetAmountForBooking';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var Balance = data.ResultSet.Table[0].Balance;
            $('#amountInfo span:last').text(Balance);
            $('#amountInfo').toggleClass('grid');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PaymentAll() {
    $('#tblItemInfo tbody').empty();
    $('#tblPaymentDetails tbody input').val('');
    $('.amount-section input').val('');
    $('.amount-section input,.discountSection input').val('');
}
function ReferralSearch(referral) {
    var url = config.baseUrl + "/api/Appointment/Referral_Search";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.referral_name = referral;
    objBO.Logic = 'ReferralSearch';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            $('#tblReferral tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td><button class='btn-success btn-flat'>Select</button></td>";
                        tbody += "<td>" + val.ref_code + "</td>";
                        tbody += "<td>" + val.ref_name + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.degree + "</td>";
                        tbody += "<td>" + val.address + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReferral tbody').append(tbody);
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DoctorTimeSlots(date) {
    $('#modalTimeAvailability .TimeFrame').empty();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.from = date;
    objBO.DoctorId = $('#ddlDoctor option:selected').val();
    objBO.Logic = 'DoctorTimeSlots';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {

                    var slots = "";
                    $('#txtDrTimeSlots').text($('#ddlDoctor option:selected').text());
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.slotStatus == 'Booked') {
                            slots += "<span class='TimeSlot' disabled>" + val.Final_InShift + "-" + val.Final_outShift + "<br/><span class='TimeSlot-Status'>" + val.slotStatus + "</span></span>";
                        }
                        else if (val.slotStatus == 'OnLeave') {
                            slots += "<span class='TimeSlot' disabled>" + val.Final_InShift + "-" + val.Final_outShift + "<br/><span class='TimeSlot-Status' style='background:#de2424 !important'>" + val.slotStatus + "</span></span>";
                        }
                        else if ((val.slotStatus == 'Available') && (val.allowBooking == 1)) {
                            slots += "<span class='TimeSlot' style='background:#036f98 !important'>" + val.Final_InShift + "-" + val.Final_outShift + "<br/><span class='TimeSlot-Status'>" + val.slotStatus + "</span></span>";
                        }
                        else {
                            slots += "<span disabled class='TimeSlot'>" + val.Final_InShift + "-" + val.Final_outShift + "<br/><span class='TimeSlot-Status'>Time Elapsed</span></span>";
                        }
                    });
                    $('#modalTimeAvailability .TimeFrame').append(slots);
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BookAppointmentQueries() {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.Logic = 'All';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {

            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlPanel').empty().append($('<option value="000">Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlPanel').append($('<option data-iscredit=' + val.IsCredit + '></option>').val(val.panelid).html(val.PanelName));
                    });
                }
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlDept').empty().append($('<option>All</option>'));
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlDept').append($('<option></option>').val(val.DeptId).html(val.DepartmentName));
                    });
                }
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlDoctor').empty().append($('<option>Select</option>'));
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName));
                    });
                }
                if (Object.keys(data.ResultSet.Table3).length) {
                    $('#ddlSource').empty().append($('<option>Select</option>'));
                    $.each(data.ResultSet.Table3, function (key, val) {
                        $('#ddlSource').append($('<option></option>').val(val.auto_id).html(val.source_name));
                    });
                }
                if (Object.keys(data.ResultSet.Table4).length) {
                    $('#ddlVisitPurpose').empty().append($('<option>Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table4, function (key, val) {
                        $('#ddlVisitPurpose').append($('<option></option>').val(val.visit_purpose).html(val.visit_purpose));
                    });
                }
                if (Object.keys(data.ResultSet.Table6).length) {
                    $('#ddlReferral').empty().append($('<option>Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table6, function (key, val) {
                        $('#ddlReferral').append($('<option></option>').val(val.ref_code).html(val.ref_name));
                    });
                }
                if (Object.keys(data.ResultSet.Table7).length) {
                    $('#ddlApprovedBy').empty().append($('<option>Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table7, function (key, val) {
                        $('#ddlApprovedBy').append($('<option></option>').val(val.emp_code).html(val.emp_name));
                    });
                }
                if (Object.keys(data.ResultSet.Table8).length) {
                    $('#tblPaymentDetails tbody .BankName').empty().append($('<option></option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table8, function (key, val) {
                        $('#tblPaymentDetails tbody .BankName').append($('<option></option>').val(val.BankId).html(val.BankName));
                    });
                }
                if (Object.keys(data.ResultSet.Table9).length) {
                    $('#tblPaymentDetails tbody .MachineName').empty().append($('<option value="Select">Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table9, function (key, val) {
                        if (val.usedFor == 'SwipeCard')
                            $('#tblPaymentDetails tbody .MachineName:eq(0)').append($('<option></option>').val(val.machineId).html(val.machineName));

                        if (val.usedFor == 'Online')
                            $('#tblPaymentDetails tbody .MachineName:eq(1)').append($('<option></option>').val(val.machineId).html(val.machineName));
                    });
                }
                if (Object.keys(data.ResultSet.Table10).length) {
                    $('#ddlDiscountType').empty().append($('<option>Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table10, function (key, val) {
                        $('#ddlDiscountType').append($('<option></option>').val(val.DiscountType).html(val.DiscountType));
                    });
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        complete: function (response) {
            $('#ddlPanel option').map(function () {
                if ($(this).val() == '1') {
                    $('#ddlPanel').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                }
            });
            $('#ddlDept').prop('selectedIndex', '0').trigger('change.select2');
            $('#ddlReferral').val('RC1459').trigger('change.select2');
            $('.selection').find('#select2-ddlReferral-container').text('CHANDAN HOSPITAL');
            //$('#ddlCountry').val(14).trigger('change.select2');
            //GetStateByCountry(14, 'Y');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctorByDept() {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.DeptId = $('#ddlDept option:selected').val();
    objBO.Logic = 'GetDoctorByDept';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet.Table).length) {
                $('#ddlDoctor').empty().append($('<option>Select</option>')).trigger('change.select2');;
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName));
                });
            }
            else {
                //alert('Data Not Found...!');
            }
        },
        complete: function (resp) {
            //if (isApp.includes('?appno')) {
            //    var appno = atob(query()["appno"]);
            //    if (appno != '') {
            //        GetBookingByAppNo(appno);
            //    }
            //}
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetVisitTypeByDoctor(DoctorId) {
    $('#ddlItemName').empty().append($('<option>Select</option>')).trigger('change.select2');
    PaymentAll();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.UHID = $('#txtUHID').val();
    objBO.DoctorId = DoctorId;
    objBO.Logic = 'GetVisitTypeByDoctor';
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
                        $('#ddlItemName').append($('<option></option>').val(val.ItemId).html(val.SubCatName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        var time = val.Final_InShift + '-' + val.Final_outShift;
                        $('#txtTime').val(time);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('.paymentSection span:eq(3) span:last').text((eval(val.TokenNo) < 10 ? '0' + val.TokenNo : val.TokenNo));
                        //$('.paymentSection span:eq(3) span').fadeOut(500).fadeIn(500).fadeOut(500).fadeIn(500);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table3).length > 0) {
                    $.each(data.ResultSet.Table3, function (key, val) {
                        $('.paymentSection span:eq(3) span:first').text(val.LastAppDate);
                        //$('.paymentSection span:eq(3) span').fadeOut(500).fadeIn(500).fadeOut(500).fadeIn(500);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctorSchedule(doctor) {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.prm_1 = doctor;
    objBO.Logic = 'GetDoctorSchedule';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $('#tblDoctorSchedule tbody').empty();
                    var tbody = "";
                    var temp = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (temp != val.shift_name) {
                            tbody += "<tr style='background:#addbff'>";
                            tbody += "<td colspan='4'>" + val.shift_name + "</td>";
                            tbody += "<tr>";
                            temp = val.shift_name;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + val.day_name + "</td>";
                        tbody += "<td>" + val.shift_start + "</td>";
                        tbody += "<td>" + val.shift_end + "</td>";
                        tbody += "<td>" + val.patient_limit + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDoctorSchedule tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#tblDoctorLeave tbody').empty();
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.input_date + "</td>";
                        tbody += "<td>" + val.start_time + "</td>";
                        tbody += "<td>" + val.end_time + "</td>";
                        tbody += "<td>" + val.status + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDoctorLeave tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetAppointmentRate(UHID, panelId, item) {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.UHID = UHID;
    objBO.DoctorId = $('#ddlDoctor option:selected').val();
    objBO.PanelId = panelId;
    objBO.ItemId = item;
    objBO.from = $('#txtAppointmentOn').val();
    objBO.Logic = 'GetAppointmentRate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblItemInfoNull').slideUp();
            $('#tblItemInfo tbody').empty();
            var tbody = '';
            itemIds = [];
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width: 10%;padding:1px'>" + val.ItemId + "</td>";
                        tbody += "<td style='width: 30%;padding: 1px;'><span class='pover'>" + val.ItemName + "</span></td>";
                        tbody += "<td style='width:8%;'>" + val.panel_rate + "</td>";
                        tbody += "<td style='width:8%;'>" + val.panel_discount + "</td>";

                        if (val.isZeroRateAllowed == 1) {
                            tbody += "<td contenteditable='true' style='border: 1px solid #65bbff;width:8%'>" + val.panel_net + "</td>";
                        }
                        else {
                            tbody += "<td style='width:8%;'>" + val.panel_net + "</td>";
                        }

                        tbody += "<td style='width:9%;'><input type='text' value='0' style='width:100%' maxlength='5' data-tag='ByPercentage'></td>";
                        tbody += "<td style='width:8%;'><input type='text' value='0' style='width:100%' maxlength='5' data-tag='ByAmt'></td>";
                        tbody += "<td style='width:8%;'>" + val.panel_discount + "</td>";
                        tbody += "<td style='width:8%;'>" + val.panel_net + "</td>";
                        tbody += "<td style='text-align:center;padding:2px;width:2%'><button class='btn-danger'>x</button></td>";
                        tbody += "<td style='padding:0;width:2%'><input type='checkbox'/></td>";
                        tbody += "<td style='display:none'>" + val.RateListId + "</td>";
                        tbody += "<td style='display:none'>" + val.ItemSection + "</td>";
                        tbody += "<td style='display:none'>" + val.IsPackage + "</td>";
                        tbody += "<td style='display:none'>" + val.TaxRate + "</td>";
                        tbody += "<td style='display:none'>" + val.TaxAboveAmount + "</td>";
                        var tax = 0;
                        if (val.TaxRate > 0 && (val.panel_net > val.TaxAboveAmount)) {
                            tax = (val.panel_net * val.TaxRate) / 100;
                        }
                        else {
                            tax = 0;
                        }
                        tbody += "<td style='display:none'>" + tax + "</td>";
                        tbody += "</tr>";
                        itemIds.push(val.ItemId);
                    });
                    $('#tblItemInfo tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $('#tblItemInfoNull tbody').empty();
                    var tbody1 = '';
                    var counter = 0;
                    $.each(data.ResultSet.Table1, function (key, val) {
                        counter++;
                        tbody1 += "<tr>";
                        tbody1 += "<td>" + val.ItemId + "</td>";
                        tbody1 += "<td style='width: 25%'><span class='pover'>" + val.ItemName + "</span></td>";
                        tbody1 += "<td style='width:75%'>" + val.Remark + "</td>";
                        tbody1 += "<td style='text-align:center;padding:2px'><button class='btn-danger'>x</button></td>";
                        tbody1 += "</tr>";
                        itemIds.push(val.ItemId);
                    });
                }
            }
            $('.nullInfo counter').text(counter);
            $('#tblItemInfoNull tbody').append(tbody1);
            if ($('#tblItemInfoNull tbody tr').length > 0) {
                $('.nullInfo').show();
            }
            else {
                $('.nullInfo').hide();
                $('#tblItemInfoNull').hide();
            }
        },
        complete: function (e) {
            totalCal();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ItemWiseDiscount(elem, dislogic) {
    $('#txtPayable').val(0);
    $('#txtADiscount').val(0);
    $('#txtDiscountByAmt').val(0);
    $('#txtDiscountByPer').val(0);
    if (dislogic == 'ByPercentage') {
        var disPercent = parseFloat($(elem).closest('tr').find('td:eq(5)').find('input:text').val());
        let panelRate = parseFloat($(elem).closest('tr').find('td:eq(4)').text());
        const TRate = $(elem).closest('tr').find('td:eq(2)').text();
        const PDis = $(elem).closest('tr').find('td:eq(3)').text();
        const TaxRate = parseFloat($(elem).closest('tr').find('td:eq(14)').text());
        const TaxAboveAmount = parseFloat($(elem).closest('tr').find('td:eq(15)').text());
        const prate = (panelRate == '') ? 0 : panelRate;
        var dis = (disPercent == '') ? 0 : disPercent;
        if (dis > 100) {
            $(elem).closest('tr').find('td:eq(5)').find('input:text').val(0).keyup();
            //alert('Discount can not be greater than 100%');
            return
        }
        const disAmt = parseFloat(prate) * parseFloat(dis) / 100;
        var TDis = parseFloat(PDis) + parseFloat(disAmt);
        var NetAmount = parseFloat(TRate) - parseFloat(TDis);
        var tax = 0;
        if (TaxRate > 0 && (NetAmount > TaxAboveAmount)) {
            tax = (NetAmount * TaxRate) / 100;
        }
        else {
            tax = 0;
        }
        $(elem).closest('tr').find('td:eq(6)').find('input:text').val(disAmt.toFixed(2));
        $(elem).closest('tr').find('td:eq(7)').text(TDis.toFixed(2));
        $(elem).closest('tr').find('td:eq(8)').text(NetAmount.toFixed(2));
        $(elem).closest('tr').find('td:eq(16)').text(tax);
    }
    else {
        var disAmount = parseFloat($(elem).closest('tr').find('td:eq(6)').find('input:text').val());
        let panelRate = parseFloat($(elem).closest('tr').find('td:eq(4)').text());
        const TRate = $(elem).closest('tr').find('td:eq(2)').text();
        const PDis = $(elem).closest('tr').find('td:eq(3)').text();
        const TaxRate = parseFloat($(elem).closest('tr').find('td:eq(14)').text());
        const TaxAboveAmount = parseFloat($(elem).closest('tr').find('td:eq(15)').text());
        const prate = (isNaN(panelRate)) ? 0 : panelRate;
        const disAmt = (disAmount == '') ? 0 : disAmount;
        if (parseFloat(disAmt) > parseFloat(prate)) {
            $(elem).closest('tr').find('td:eq(6)').find('input:text').val(0).keyup();
            //alert('Discount Amount can not be greater than Personal Rate');
            return
        }

        const disPerc = parseFloat(disAmt) * 100 / parseFloat(prate);
        const TDis = parseFloat(PDis) + parseFloat(disAmt);
        const NetAmount = parseFloat(TRate) - parseFloat(TDis);
        var tax = 0;
        if (TaxRate > 0 && (NetAmount > TaxAboveAmount)) {
            tax = (NetAmount * TaxRate) / 100;
        }
        else {
            tax = 0;
        }

        $(elem).closest('tr').find('td:eq(5)').find('input:text').val((isNaN(disPerc)) ? 0 : disPerc);
        $(elem).closest('tr').find('td:eq(7)').text(TDis.toFixed(2));
        $(elem).closest('tr').find('td:eq(8)').text(NetAmount.toFixed(2));
        $(elem).closest('tr').find('td:eq(16)').text(tax);
    }
    totalCal();
}
function OverAllDiscount(dislogic) {
    var TPRate = 0;
    var TDisc = 0;
    $('#tblItemInfo tbody tr').each(function () {
        TPRate += parseFloat($(this).closest('tr').find('td:eq(4)').text());
    });
    if (dislogic == 'ByPercent') {
        $('#txtDiscountByAmt').val(0);
        var discountByPer = parseInt(($('#txtDiscountByPer').val() == '') ? 0 : $('#txtDiscountByPer').val());
        if (discountByPer > 100) {
            $('#txtDiscountByPer').val(0).keyup();
            //alert('Discount can not be greater than 100%');
            return
        }
        TDisc = (TPRate * discountByPer / 100)
    }
    else {
        $('#txtDiscountByPer').val(0);
        var grossAmount = parseFloat($('#txtGrossAmount').val());
        TDisc = parseInt(($('#txtDiscountByAmt').val() == '') ? 0 : $('#txtDiscountByAmt').val());
        if (TDisc > grossAmount) {
            $('#txtDiscountByAmt').val(0).keyup();
            //alert('Discount can not be greater than 100%');
            return
        }
    }
    $('#tblItemInfo tbody tr').each(function () {
        var perc_hold = eval($(this).closest('tr').find('td:eq(4)').text()) * 100 / TPRate;
        var adl_disc = (TDisc * perc_hold / 100)
        $(this).find('td:eq(6)').find('input:text').val(adl_disc.toFixed(4));
        var TRate = eval($(this).closest('tr').find('td:eq(2)').text());
        var PDis = eval($(this).closest('tr').find('td:eq(3)').text());
        var PRate = eval($(this).closest('tr').find('td:eq(4)').text());
        var disPerc = eval(adl_disc) * 100 / eval(PRate);
        $(this).find('td:eq(5)').find('input:text').val(disPerc.toFixed(4));
        var TDis = eval(PDis) + eval(adl_disc);
        var NetAmount = eval(TRate) - eval(TDis);
        $(this).find('td:eq(5)').find('input:text').val(disPerc.toFixed(4));
        $(this).find('td:eq(7)').text(TDis.toFixed(4));
        $(this).find('td:eq(8)').text(NetAmount.toFixed(4));
        const TaxRate = eval($(this).find('td:eq(14)').text());
        const TaxAboveAmount = eval($(this).find('td:eq(15)').text());
        var tax = 0;
        if (TaxRate > 0 && (NetAmount > TaxAboveAmount)) {
            tax = (NetAmount * TaxRate) / 100;
        }
        else {
            tax = 0;
        }
        $(this).find('td:eq(16)').text(tax);
    });
    totalCal();
}
function totalCal() {
    var TRate = 0;
    var PDis = 0;
    var AdlDisc = 0;
    var netAmt = 0;
    var totalTax = 0;

    $('#tblItemInfo tbody tr').each(function () {
        TRate += parseFloat($(this).closest('tr').find('td:eq(2)').text());
        totalTax += parseFloat($(this).closest('tr').find('td:eq(16)').text());
        PDis += parseFloat($(this).closest('tr').find('td:eq(3)').text());
        netAmt += parseFloat($(this).closest('tr').find('td:eq(8)').text());
        AdlDisc += parseFloat($(this).closest('tr').find('td:eq(6)').find('input:text').val());

    });
    $('#txtGrossAmount').val(TRate);
    $('#txtPDiscount').val(PDis);
    $('#txtTDiscount').val(PDis + AdlDisc);
    $('#txtADiscount').val(AdlDisc);
    $('#txtNetAmount').val(netAmt);
    debugger
    var payable = parseInt(netAmt + totalTax);
    roundOff = 0;
    roundOff = (parseFloat(netAmt + totalTax).toFixed(2)) - (parseInt(netAmt + totalTax).toFixed(2));
    $('#txtPayable').val(payable);
    $('#txtTotalTax').val(totalTax.toFixed(2));
    $(this).find('option:selected').data('iscredit')
    if ($('#ddlPanel option:selected').data('iscredit'))
        $('#txtBalance').val(payable);

    if (!$('#ddlPanel option:selected').data('iscredit'))
        $('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val(payable);

    $('#tblPayInfo tbody').empty();
    var tbody = "";
    tbody += "<tr>";
    tbody += "<td>" + TRate + "</td>";
    tbody += "<td>" + PDis + "</td>";
    tbody += "<td>" + AdlDisc + "</td>";
    tbody += "<td>" + PDis + AdlDisc + "</td>";
    tbody += "<td>" + netAmt + "</td>";
    tbody += "<td>" + totalTax + "</td>";
    tbody += "<td>" + netAmt + "</td>";
    tbody += "<td>" + netAmt + "</td>";
    tbody += "<td>" + netAmt + "</td>";
    tbody += "</tr>";
    $('#tblPayInfo tbody').append(tbody);
}
function InsertSource() {
    if (ValidateSource()) {
        var url = config.baseUrl + "/api/Appointment/Opd_InsertAppointmentAssets";
        var objBO = {};
        objBO.hosp_id = Active.unitId;
        objBO.SourceName = $('#txtNewSourceName').val().toUpperCase();
        objBO.login_id = Active.userId;
        objBO.Logic = 'InsertSource';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data == 'Success') {
                    alert(data);
                    $('#ddlSource').append($('<option></option>').val('new').html(objBO.SourceName));
                    $('#ddlSource').val('new').trigger('change.select2');
                    $('#modalSource').modal('hide');
                    $('#modalSource input[type=text]').val('');
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
}
function InsertDiscountType() {
    if (ValidateDiscountType()) {
        var url = config.baseUrl + "/api/Appointment/Opd_InsertAppointmentAssets";
        var objBO = {};
        objBO.hosp_id = Active.unitId;
        objBO.prm_1 = $('#txtNewDiscountType').val().toUpperCase();
        objBO.login_id = Active.userId;
        objBO.Logic = 'InsertDiscountType';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data == 'Success') {
                    alert(data);
                    $('#ddlDiscountType').append($('<option></option>').val('new').html(objBO.prm_1));
                    $('#ddlDiscountType').val('new').trigger('change.select2');
                    $('#modalDiscountType').modal('hide');
                    $('#modalDiscountType input[type=text]').val('');
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
}
function InsertVisitPurpose() {
    if (ValidateVisitPurpose()) {
        var url = config.baseUrl + "/api/Appointment/Opd_InsertAppointmentAssets";
        var objBO = {};
        objBO.hosp_id = Active.unitId;
        objBO.visit_purpose = $('#txtVisitPurpose').val().toUpperCase();
        objBO.login_id = Active.userId;
        objBO.Logic = 'InsertVisitPurpose';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data == 'Success') {
                    alert(data);
                    $('#ddlVisitPurpose').append($('<option></option>').val('new').html(objBO.visit_purpose));
                    $('#ddlVisitPurpose').val('new').trigger('change.select2');
                    $('#modalPurpose').modal('hide');
                    $('#modalPurpose input[type=text]').val('');
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
}
function InsertReferal() {
    if (ValidateReferal()) {
        var url = config.baseUrl + "/api/Appointment/Opd_InsertAppointmentAssets";
        var objBO = {};
        objBO.ref_name = $('#txtReferalName').val().toUpperCase();
        objBO.degree = $('#txtReferalDegree').val().toUpperCase();
        objBO.mobile_no = $('#txtReferalMobile').val().toUpperCase();
        objBO.c_address = $('#txtReferalAddress').val().toUpperCase();
        objBO.c_city = $('#ddlReferralCity option:selected').text().toUpperCase();
        objBO.state = $('#ddlReferralState option:selected').text().toUpperCase();
        objBO.hosp_id = Active.unitId;
        objBO.login_id = Active.userId;
        objBO.Logic = 'InsertReferal';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data == 'Success') {
                    alert(data);
                    $('#ddlReferral').append($('<option></option>').val('new').html(objBO.ref_name));
                    $('#ddlReferral').val('new').trigger('change.select2');
                    $('#modalReferral').modal('hide');
                    $('#modalReferral').modal('hide');
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
}
function InsertPatientMaster() {
    if (ValidatePatientMaster()) {
        var url = config.baseUrl + "/api/Appointment/InsertPatientMaster";
        var objBO = {};
        objBO.hosp_id = Active.unitId;
        objBO.barcodeno = $('#txtBarcode').val().toUpperCase();
        objBO.Title = $('#ddlTitle option:selected').text();
        objBO.FirstName = $('#txtFirstName').val().toUpperCase();
        objBO.LastName = $('#txtLastName').val().toUpperCase();
        objBO.patient_name = objBO.FirstName + ' ' + objBO.LastName;
        if ($('#ddlGender option:selected').text() == 'select')
            objBO.gender = '-';
        else
            objBO.gender = $('#ddlGender option:selected').text();
        objBO.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
        objBO.age = $('#txtAge').val();
        objBO.age_type = $('#ddlAgeType option:selected').text();
        objBO.mobile_no = $('#txtMobileNo').val();
        objBO.email_id = $('#txtEmailId').val();
        if ($('#ddlMaritalStatus option:selected').text() == 'select')
            objBO.marital_status = '-';
        else
            objBO.marital_status = $('#ddlMaritalStatus option:selected').text();
        if ($('#ddlReligion option:selected').text() == 'select')
            objBO.religion = '-';
        else
            objBO.religion = $('#ddlReligion option:selected').text();
        if ($('#ddlRelationOf option:selected').text() == 'select')
            objBO.relation_of = '-';
        else
            objBO.relation_of = $('#ddlRelationOf option:selected').text();
        objBO.relation_name = $('#txtRelationName').val();
        objBO.relation_phone = $('#txtRelationContact').val();
        objBO.country = $('#ddlCountry option:selected').text();
        objBO.state = $('#ddlState option:selected').text();
        objBO.district = $('#ddlCity option:selected').text();
        objBO.locality = '-';
        objBO.village = '-';
        objBO.address = $('#txtAddress').val();
        if ($('#ddlIdProof option:selected').text() == 'select')
            objBO.idType = '-';
        else
            objBO.idType = $('#ddlIdProof option:selected').text();
        objBO.IDNo = $('#txtIdNo').val();
        objBO.CardNo = '-';
        objBO.member_id = $('#txtMembershipNo').val().toUpperCase();
        objBO.IPNo = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'Insert';
        debugger
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data == 'Successfully Saved') {
                    alert(data);
                    Clear();
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
}
function Opd_AppointmentBooking() {
    if (confirm('Are you sure to Book Appointment?')) {
        if (ValidateBooking()) {
            var url = config.baseUrl + "/api/Appointment/Opd_AppointmentBooking";
            var objPatient = {};
            var objBooking = {};
            var objPayment = [];

            objPatient.hosp_id = Active.unitId;
            objPatient.UHID = $('#txtUHID').val().toUpperCase();
            objPatient.barcodeno = $('#txtBarcode').val().toUpperCase();
            objPatient.Title = $('#ddlTitle option:selected').text();
            objPatient.FirstName = $('#txtFirstName').val().toUpperCase();
            objPatient.LastName = $('#txtLastName').val().toUpperCase();
            objPatient.patient_name = objPatient.FirstName + ' ' + objPatient.LastName;
            if ($('#ddlGender option:selected').text() == 'Select')
                objPatient.gender = '-';
            else
                objPatient.gender = $('#ddlGender option:selected').text();
            objPatient.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
            objPatient.age = $('#txtAge').val();
            objPatient.ageType = $('#ddlAgeType option:selected').text();
            objPatient.mobile_no = $('#txtMobileNo').val();
            objPatient.email_id = $('#txtEmailId').val();
            if ($('#ddlMaritalStatus option:selected').text() == 'Select')
                objPatient.marital_status = '-';
            else
                objPatient.marital_status = $('#ddlMaritalStatus option:selected').text();
            if ($('#ddlReligion option:selected').text() == 'Select')
                objPatient.religion = '-';
            else
                objPatient.religion = $('#ddlReligion option:selected').text();
            if ($('#ddlRelationOf option:selected').text() == 'Select')
                objPatient.relation_of = '-';
            else
                objPatient.relation_of = $('#ddlRelationOf option:selected').text();
            objPatient.relation_name = $('#txtRelationName').val();
            objPatient.relation_phone = $('#txtRelationContact').val();
            objPatient.country = $('#ddlCountry option:selected').text();
            objPatient.state = $('#ddlState option:selected').text();
            objPatient.district = $('#ddlCity option:selected').text();
            objPatient.locality = '-';
            objPatient.village = '-';
            objPatient.address = $('#txtAddress').val();
            if ($('#ddlIdProof option:selected').text() == 'Select')
                objPatient.idType = '-';
            else
                objPatient.idType = $('#ddlIdProof option:selected').text();
            objPatient.IDNo = $('#txtIdNo').val();
            objPatient.CardNo = '-';
            objPatient.member_id = $('#txtMembershipNo').val().toUpperCase();
            objPatient.IPNo = '-';
            objPatient.login_id = Active.userId;
            objPatient.Logic = 'Insert';
            $('#tblPaymentDetails tbody tr.pay').each(function () {
                objPayment.push({
                    'ReceiptNo': '-',
                    'PayMode': $(this).find('td:eq(0)').text(),
                    'CardNo': '-',
                    'BankName': $(this).find('td:eq(3)').find('select option:selected').text(),
                    'RefNo': $(this).find('td:eq(4)').find('input[type=text]').val(),
                    'MachineId': $(this).find('td:eq(5)').find('select option:selected').val(),
                    'MachineName': $(this).find('td:eq(5)').find('select option:selected').text(),
                    'Amount': $(this).find('td:eq(1)').find('input[type=text]').val(),
                    'OnlPaymentId': '',
                    'OnlPayStatus': '',
                    'OnlPayResponse': '',
                    'OnlPaymentDate': new Date(),
                    'login_id': Active.userId
                });
            });
            objBooking.hosp_id = Active.unitId;
            objBooking.UHID = $('#txtUHID').val().toUpperCase();
            objBooking.IsNewPatient = $('#txtUHID').val().toUpperCase();
            objBooking.Title = objPatient.Title;
            objBooking.patient_name = objPatient.patient_name;
            objBooking.age = $('#txtAge').val();
            objBooking.ageType = $('#ddlAgeType option:selected').text();
            if ($('#ddlGender option:selected').text() == 'Select')
                objBooking.gender = '-';
            else
                objBooking.gender = $('#ddlGender option:selected').text();
            objBooking.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
            objBooking.mobile_no = $('#txtMobileNo').val();
            objBooking.IDProofType = $('#ddlIdProof option:selected').text();
            objBooking.IDProofNO = $('#txtIdNo').val();
            objBooking.email = '-';
            objBooking.DoctorId = $('#ddlDoctor option:selected').val();
            objBooking.AppDate = $('#txtAppointmentOn').val();
            var time = $('#txtTime').val().split('-');
            objBooking.AppInTime = time[0];
            objBooking.AppOutTime = time[1];
            objBooking.ItemId = $('#ddlItemName option:selected').val();
            objBooking.visitType = $('#ddlVisitType option:selected').text();
            if ($('#ddlSource option:selected').text() == 'Select')
                objBooking.visitSource = '-';
            else
                objBooking.visitSource = $('#ddlSource option:selected').text();
            if ($('#ddlVisitPurpose option:selected').text() == 'Select')
                objBooking.visit_purpose = '-';
            else
                objBooking.visit_purpose = $('#ddlVisitPurpose option:selected').text();
            objBooking.GrossAmount = $('#txtGrossAmount').val();
            objBooking.discount = $('#txtDiscount').val();
            objBooking.discount_remark = $('#txtDisResason').val();
            if ($('#ddlDiscountType option:selected').text() == 'Select')
                objBooking.discountType = '-';
            else
                objBooking.discountType = $('#ddlDiscountType option:selected').text();
            if ($('#ddlApprovedBy option:selected').text() == 'Select')
                objBooking.discountBy = '-';
            else
                objBooking.discountBy = $('#ddlApprovedBy option:selected').text();
            objBooking.login_id = Active.userId;
            objBooking.refDoctorCode = $('#ddlReferral option:selected').val();
            objBooking.PanelId = $('#ddlPanel option:selected').val();
            objBooking.RateList_Id = '-';
            objBooking.IsConfirmed = true;
            objBooking.GenFrom = 'Hospital';
            objBooking.online_app_no = '-';
            objBooking.ipAddress = '-';
            objBooking.Logic = '-';
            var MasterObject = {};
            MasterObject.objPatient = objPatient;
            MasterObject.objBooking = objBooking;
            MasterObject.objPayment = objPayment;
            $.ajax({
                method: "POST",
                url: url,
                data: JSON.stringify(MasterObject),
                contentType: "application/json;charset=utf-8",
                dataType: "JSON",
                traditional: true,
                success: function (data) {
                    var tnxid = data.split('|')[1];
                    if (data.includes('Success')) {
                        alert('Successfully Booking Confirmed..!');
                        if (data.includes('|')) {
                            $('#txtTnxIdForPrint').text(tnxid);
                            Receipt(tnxid);
                            $('#start-camera').trigger('click');
                        }
                        Clear();
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
    }
}
function Opd_AppointmentBooking1() {
    if (confirm('Are you sure to Book Appointment?')) {
        if (ValidateBooking()) {
            var swipeSelect = $('#tblPaymentDetails tbody').find('tr:eq(2).pay').find('td:eq(5)').find('select option:selected').text();
            var onlineSelect = $('#tblPaymentDetails tbody').find('tr:eq(3).pay').find('td:eq(5)').find('select option:selected').text();
            if (swipeSelect == 'Select') {
                alert('Please Select Bank Machine for Swipe Card');
                return
            }
            if (onlineSelect == 'Select') {
                alert('Please Select Bank Machine NEFT/RTGS/Online');
                return
            }
            var netAmount = parseFloat($('#txtPayable').val());
            var cash = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val());
            var cheque = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(1)').find('td:eq(1)').find('input[type=text]').val());
            var cc = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(2)').find('td:eq(1)').find('input[type=text]').val());
            var ntfs = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(3)').find('td:eq(1)').find('input[type=text]').val());
            var total = cash + cheque + cc + ntfs;
            if (total > netAmount) {
                alert('Paid Amount can not be more than Net Amount...!');
                return
            }
            else if (total < netAmount) {
                alert('Paid Amount can not be less than Net Amount...!').css({ 'color': '#bb8202', 'font-size': '11px' });
                return
            }

            var url = config.baseUrl + "/api/Appointment/Opd_AppointmentBooking1";
            var objPatient = {};
            var objBooking = {};
            var objRateList = [];
            var objPayment = [];
            var MasterObject = {};

            objPatient.hosp_id = Active.unitId;
            objPatient.UHID = $('#txtUHID').val().toUpperCase();
            objPatient.barcodeno = $('#txtBarcode').val().toUpperCase();
            objPatient.Title = $('#ddlTitle option:selected').text();
            objPatient.FirstName = $('#txtFirstName').val().toUpperCase();
            objPatient.LastName = $('#txtLastName').val().toUpperCase();
            objPatient.patient_name = objPatient.FirstName + ' ' + objPatient.LastName;
            if ($('#ddlGender option:selected').text() == 'Select')
                objPatient.gender = '-';
            else
                objPatient.gender = $('#ddlGender option:selected').text();
            objPatient.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
            objPatient.age = $('#txtAge').val();
            objPatient.ageType = $('#ddlAgeType option:selected').text();
            objPatient.mobile_no = $('#txtMobileNo').val();
            objPatient.email_id = $('#txtEmailId').val();
            objPatient.marital_status = ($('#ddlMaritalStatus option:selected').text() == 'Select') ? '-' : $('#ddlMaritalStatus option:selected').text();
            objPatient.religion = ($('#ddlReligion option:selected').text() == 'Select') ? '-' : $('#ddlReligion option:selected').text();
            objPatient.relation_of = ($('#ddlRelationOf option:selected').text() == 'Select') ? '-' : $('#ddlRelationOf option:selected').text();
            objPatient.relation_name = $('#txtRelationName').val();
            objPatient.relation_phone = $('#txtRelationContact').val();
            objPatient.country = ($('#ddlCountry option:selected').text() == 'Select') ? '-' : $('#ddlCountry option:selected').text();
            objPatient.state = ($('#ddlState option:selected').text() == 'Select') ? '-' : $('#ddlState option:selected').text();
            objPatient.district = ($('#ddlCity option:selected').text() == 'Select') ? '-' : $('#ddlCity option:selected').text();
            objPatient.locality = ($('#ddlLocality option:selected').text() == 'Select') ? '-' : $('#ddlLocality option:selected').text();
            objPatient.village = '-';
            objPatient.address = $('#txtAddress').val();
            objPatient.idType = ($('#ddlIdProof option:selected').text() == 'Select') ? '-' : $('#ddlIdProof option:selected').text();
            objPatient.IDNo = $('#txtIdNo').val();
            objPatient.CardNo = '-';
            objPatient.member_id = $('#txtMembershipNo').val().toUpperCase();
            objPatient.IPNo = '-';
            objPatient.login_id = Active.userId;
            objPatient.Logic = 'ItdoseNewEntry';
            $('#tblItemInfo tbody tr').each(function () {
                objRateList.push({
                    'RateListId': $(this).find('td:eq(11)').text(),
                    'ItemId': $(this).find('td:eq(0)').text(),
                    'RateListName': $(this).find('td:eq(1)').text(),
                    'ItemSection': $(this).find('td:eq(12)').text(),
                    'IsPackage': $(this).find('td:eq(13)').text(),
                    'panel_discount': $(this).find('td:eq(3)').text(),
                    'panel_rate': $(this).find('td:eq(2)').text(),
                    'qty': '1',
                    'adl_disc_perc': $(this).find('td:eq(5)').find('input:text').val(),
                    'adl_disc_amount': $(this).find('td:eq(6)').find('input:text').val(),
                    'net_amount': $(this).find('td:eq(8)').text(),
                    'IsUrgent': ($(this).find('td:eq(10)').find('input:checkbox').is(':checked')) ? 'Y' : 'N',
                    'Remark': '-',
                    'TaxRate': $(this).find('td:eq(14)').text(),
                    'TaxAmt': $(this).find('td:eq(16)').text()
                });
            });
            $('#tblPaymentDetails tbody tr.pay').each(function () {
                if (parseFloat($(this).find('td:eq(1)').find('input[type=text]').val()) > 0) {
                    objPayment.push({
                        'ReceiptNo': '-',
                        'PayMode': $(this).find('td:eq(0)').text(),
                        'CardNo': '-',
                        'BankName': $(this).find('td:eq(3)').find('select option:selected').text(),
                        'RefNo': $(this).find('td:eq(4)').find('input[type=text]').val(),
                        'MachineId': $(this).find('td:eq(5)').find('select option:selected').val(),
                        'MachineName': $(this).find('td:eq(5)').find('select option:selected').text(),
                        'Amount': $(this).find('td:eq(1)').find('input[type=text]').val(),
                        'OnlPaymentId': '',
                        'OnlPayStatus': '',
                        'OnlPayResponse': '',
                        'OnlPaymentDate': new Date(),
                        'login_id': Active.userId
                    });
                }
            });
            objBooking.hosp_id = Active.unitId;
            objBooking.UHID = $('#txtUHID').val().toUpperCase();
            objBooking.IsNewPatient = $('#txtUHID').val().toUpperCase();
            objBooking.Title = objPatient.Title;
            objBooking.patient_name = objPatient.patient_name;
            objBooking.age = $('#txtAge').val();
            objBooking.ageType = $('#ddlAgeType option:selected').text();
            objBooking.gender = ($('#ddlGender option:selected').text() == 'Select') ? '-' : $('#ddlGender option:selected').text();
            objBooking.dob = ($('#txtDOB').val() == '') ? null : $('#txtDOB').val();
            objBooking.mobile_no = $('#txtMobileNo').val();
            objBooking.IDProofType = $('#ddlIdProof option:selected').text();
            objBooking.IDProofNO = $('#txtIdNo').val();
            objBooking.email = '-';
            objBooking.DoctorId = $('#ddlDoctor option:selected').val();
            objBooking.AppDate = $('#txtAppointmentOn').val();
            var time = $('#txtTime').val().split('-');
            objBooking.AppInTime = time[0];
            objBooking.AppOutTime = time[1];
            objBooking.ItemId = $('#ddlItemName option:selected').val();
            objBooking.visitType = $('#ddlVisitType option:selected').text();
            objBooking.visitSource = ($('#ddlSource option:selected').text() == 'Select') ? '-' : $('#ddlSource option:selected').text();
            objBooking.visit_purpose = ($('#ddlVisitPurpose option:selected').text() == 'Select') ? '-' : $('#ddlVisitPurpose option:selected').text();
            objBooking.GrossAmount = $('#txtGrossAmount').val() || 0;
            objBooking.TaxAmount = $('#txtTotalTax').val() || 0;
            objBooking.discount = $('#txtTDiscount').val() || 0;
            objBooking.roundOff = roundOff.toFixed(2);
            objBooking.discount_remark = $('#txtDisResason').val();
            objBooking.discountType = ($('#ddlDiscountType option:selected').text() == 'Select') ? '-' : $('#ddlDiscountType option:selected').text();
            objBooking.discountBy = ($('#ddlApprovedBy option:selected').text() == 'Select') ? '-' : $('#ddlApprovedBy option:selected').text();
            objBooking.login_id = Active.userId;
            objBooking.refDoctorCode = $('#ddlReferral option:selected').val();
            objBooking.PanelId = $('#ddlPanel option:selected').val();
            objBooking.RateList_Id = '-';
            objBooking.IsConfirmed = true;
            objBooking.GenFrom = _GenFrom;
            objBooking.online_app_no = _online_app_no;
            objBooking.ipAddress = '-';
            objBooking.Logic = '-';
            MasterObject.objPatient = objPatient;
            MasterObject.objBooking = objBooking;
            MasterObject.objPayment = objPayment;
            MasterObject.objRateList = objRateList;

            MasterObject.Base64String = _photo_url;
            MasterObject.photo_path = (_photo_url == null) ? 'N' : 'Y';

            var UploadDocumentInfo = new XMLHttpRequest();
            var data = new FormData();
            data.append('obj', JSON.stringify(MasterObject));
            data.append('ImageByte', _photo_url);
            UploadDocumentInfo.onreadystatechange = function () {
                if (UploadDocumentInfo.status) {
                    if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                        var json = JSON.parse(UploadDocumentInfo.responseText);
                        if (json.Message.includes('Success')) {
                            var tnxid = json.Message.split("|")[1];
                            PaymentAll();
                            alert('Successfully Booking Confirmed..!');
                            $('#txtTnxIdForPrint').text(tnxid);
                            if ($('#ddlVisitType option:selected').text() == 'Walk-In') {
                                Receipt(tnxid);
                            }
                            Clear();
                            _photo_url = null;
                            $('#liveCamera').hide();
                            $('#ImgCaptured').hide();
                            _GenFrom = "Hospital";

                        }
                        else {
                            alert(json.Message);
                        }
                    }
                }
            }
            UploadDocumentInfo.open('POST', url, true);
            UploadDocumentInfo.send(data);
        }
    }
}
function ValidateSource() {
    newSource = $('#txtNewSourceName').val();
    if (newSource == '') {
        $('#txtNewSourceName').css('border-color', 'red');
        alert('Please Provide Source Name..!');
        return false;
    }
    else {
        $('#txtNewSourceName').removeAttr('style');
    }
    return true;
}
function ValidateDiscountType() {
    newDiscountType = $('#txtNewDiscountType').val();
    if (newDiscountType == '') {
        $('#txtNewDiscountType').css('border-color', 'red');
        alert('Please Provide Discount Type..!');
        return false;
    }
    else {
        $('#txtNewDiscountType').removeAttr('style');
    }
    return true;
}
function ValidateVisitPurpose() {
    VisitPurpose = $('#txtVisitPurpose').val();

    if (VisitPurpose == '') {
        $('#txtVisitPurpose').css('border-color', 'red');
        alert('Please Provide Purpose Of Visit..!');
        return false;
    }
    else {
        $('#txtVisitPurpose').removeAttr('style');
    }
    return true;
}
function ValidateReferal() {
    var ref_name = $('#txtReferalName').val();
    var degree = $('#txtReferalDegree').val();
    var mobile_no = $('#txtReferalMobile').val();
    var c_address = $('#txtReferalAddress').val();
    var c_city = $('#ddlReferralCity option:selected').text();
    var state = $('#ddlReferralState option:selected').text();
    if (ref_name == '') {
        $('#txtReferalName').css('border-color', 'red').focus();
        alert('Please Provide Referral Name..!');
        return false;
    }
    else {
        $('#txtReferalName').removeAttr('style');
    }
    //if (mobile_no == '') {
    //	$('#txtReferalMobile').css('border-color', 'red').focus();
    //	alert('Please Provide Mobile No..!');
    //	return false;
    //}
    //else {
    //	$('#txtReferalMobile').removeAttr('style');
    //}
    //if (c_address == '') {
    //	$('#txtReferalAddress').css('border-color', 'red').focus();
    //	alert('Please Provide Address..!');
    //	return false;
    //}
    //else {
    //	$('#txtReferalAddress').removeAttr('style');
    //}
    if (state == 'Select State') {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferralState-container]').css('border-color', 'red').focus();
        alert('Please Select State..!');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferralState-container]').removeAttr('style');
    }
    if (c_city == 'Select City') {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferralCity-container]').css('border-color', 'red').focus();
        alert('Please Select City..!');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferralCity-container]').removeAttr('style');
    }
    return true;
}
function paymentCalculationByAmt() {
    //Initialize
    var grossAmt = parseFloat($('#txtGrossAmount').val());
    $('#txtDiscountByPer').val(0);
    var discountByAmt = parseInt($('#txtDiscountByAmt').val());

    if ($('#txtDiscountByAmt').val() == '') {
        paymentCal('amount', 0);
    }
    else if (discountByAmt > grossAmt) {
        $('#txtDiscountByAmt').val(0);
        paymentCal('amount', 0);
        alert('Discount Amount can not be greater then Gross Amount');
    }
    else {
        paymentCal('amount', discountByAmt);
    }

    if (discountByAmt > 0) {
        $('#txtError').text('');
        $('#txtDisResason').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').addClass('border-imp');
    }
    else {
        $('#txtDisResason').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').removeClass('border-imp');
    }
}
function paymentCalculationByPer() {
    $('#txtDiscountByAmt').val(0);
    var discountByPer = parseInt($('#txtDiscountByPer').val());

    if ($('#txtDiscountByPer').val() == '') {
        paymentCal('percent', 0);
    }
    else if (discountByPer > 100) {
        $('#txtDiscountByPer').val(0);
        paymentCal('percent', 0);
        alert('Discount can not be greater then 100%');
    }
    else {
        paymentCal('percent', discountByPer);
    }
    if (discountByPer > 0) {
        $('#txtError').text('');
        $('#txtDisResason').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').addClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').addClass('border-imp');
    }
    else {
        $('#txtDisResason').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').removeClass('border-imp');
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').removeClass('border-imp');
    }
}
function paymentCal(logic, val) {
    var grossAmt = parseFloat($('#txtGrossAmount').val());
    if (logic == 'amount') {
        var discountByAmt = parseFloat(val);
        var discountedAmt = grossAmt - parseFloat(val);
        $('#txtDiscount').val(discountByAmt);
        var netAmt = discountedAmt;
        $('#txtNetAmount').val(netAmt);
        $('input[name=PaymentMode]:not(.cash)').prop('checked', false).trigger('change.select2');
        $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
        $('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val(parseInt(netAmt));
        var payable = parseInt(netAmt);
        roundOff = (parseFloat(netAmt)) - (parseInt(netAmt));
        $('#txtPayable').val(payable);
    }
    else if (logic == 'percent') {
        var discountByPer = parseInt(val);
        var discountAmt = grossAmt - (grossAmt * discountByPer / 100);
        $('#txtDiscount').val(grossAmt * discountByPer / 100);
        var netAmt = discountAmt;
        $('#txtNetAmount').val(netAmt);
        var payable = parseInt(netAmt);
        roundOff = parseFloat(netAmt) - parseInt(netAmt);
        $('input[name=PaymentMode]:not(.cash)').prop('checked', false).trigger('change.select2');
        $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
        $('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val(parseInt(payable));
        $('#txtPayable').val(payable);
    }
}
function ValidateBooking() {
    var Title = $('#ddlTitle option:selected').text();
    var FirstName = $('#txtFirstName').val();
    var Gender = $('#ddlGender option:selected').text();
    var MobileNo = $('#txtMobileNo').val();
    var Age = $('#txtAge').val();
    var Country = $('#ddlCountry option:selected').text();
    var State = $('#ddlState option:selected').text();
    var City = $('#ddlCity option:selected').text();
    var Address = $('#txtAddress').val();
    var Referral = $('#ddlReferral option:selected').text();
    var Panel = $('#ddlPanel option:selected').text();
    var Doctor = $('#ddlDoctor option:selected').text();
    var ItemName = $('#ddlItemName option:selected').text();
    var Discount = parseFloat($('#txtDiscount').val());
    var DisResason = $('#txtDisResason').val();
    var ApprovedBy = $('#ddlApprovedBy option:selected').text();
    var DiscountType = $('#ddlDiscountType option:selected').text();
    var VisitType = $('#ddlVisitType option:selected').text();
    var Error = $('#txtError').text();
    var Payable = parseFloat($('#txtPayable').val());

    if (FirstName == '') {
        $('#txtFirstName').css('border-color', 'red').focus();
        alert('Please Provide First Name..');
        return false;
    }
    else {
        $('#txtFirstName').removeAttr('style');
    }
    if (Gender == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlGender-container]').css('border-color', 'red').focus();
        alert('Please Select Gender..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlGender-container]').removeAttr('style');
    }
    if (MobileNo == '') {
        $('#txtMobileNo').css('border-color', 'red').focus();
        alert('Please Provide Mobile No..');
        return false;
    }
    else {
        $('#txtMobileNo').removeAttr('style');
    }
    if (Age == '') {
        $('#txtAge').css('border-color', 'red').focus();
        alert('Please Provide Age..');
        return false;
    }
    else {
        $('#txtAge').removeAttr('style');
    }
    if (Country == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlCountry-container]').css('border-color', 'red').focus();
        alert('Please Select Country..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlCountry-container]').removeAttr('style');
    }
    if (State == 'Select State') {
        $('span.selection').find('span[aria-labelledby=select2-ddlState-container]').css('border-color', 'red').focus();
        alert('Please Select State..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlState-container]').removeAttr('style');
    }
    if (City == 'Select City') {
        $('span.selection').find('span[aria-labelledby=select2-ddlCity-container]').css('border-color', 'red').focus();
        alert('Please Select City..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlCity-container]').removeAttr('style');
    }
    //if (Address == '') {
    //	$('#txtAddress').css('border-color', 'red').focus();
    //	alert('Please Provide Address..');
    //	return false;
    //}
    //else {
    //	$('#txtAddress').removeAttr('style');
    //}
    if (Referral == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferral-container]').css('border-color', 'red').focus();
        alert('Please Select Referral..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlReferral-container]').removeAttr('style');
    }
    if (Panel == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlPanel-container]').css('border-color', 'red').focus();
        alert('Please Select Panel..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlPanel-container]').removeAttr('style');
    }
    if (Doctor == 'Select') {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').css('border-color', 'red').focus();
        alert('Please Select Doctor..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctor-container]').removeAttr('style');
    }
    if ($('#ddlVisitType option:selected').text() == 'Walk-In') {
        if (ItemName == 'Select') {
            $('span.selection').find('span[aria-labelledby=select2-ddlItemName-container]').css('border-color', 'red').focus();
            alert('Please Select Item Name..');
            return false;
        }
        else {
            $('span.selection').find('span[aria-labelledby=select2-ddlItemName-container]').removeAttr('style');
        }
    }

    if (Discount > 0 && DiscountType == 'Select' && VisitType == 'Walk-In') {
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').css('border-color', 'red').focus();
        alert('Please Select Discount Type..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDiscountType-container]').removeAttr('style');
    }
    if (Discount > 0 && DisResason == '' && VisitType == 'Walk-In') {
        $('#txtDisResason').css('border-color', 'red').focus();
        alert('Please Provide Discount Reason..');
        return false;
    }
    else {
        $('#txtDisResason').removeAttr('style');
    }
    if (Discount > 0 && ApprovedBy == 'Select' && VisitType == 'Walk-In') {
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').css('border-color', 'red').focus();
        alert('Please Select Approved By..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlApprovedBy-container]').removeAttr('style');
    }
    if (Error.length > 0 && VisitType == 'Walk-In') {
        alert('Please Provide Correct Payment Amount Details..');
        return false;
    }
    if (Payable < 0 && VisitType == 'Walk-In') {
        alert('Please Provide Payment Amount Details..');
        return false;
    }
    return true;
}
function Clear() {
    roundOff = 0;
    $('#BasicInformation').find('input[type=text],input[type=date]').val('');
    $('#BasicInformation').find('select:not(#ddlCountry)').prop('selectedIndex', '0').trigger('change.select2');
    $('#ddlCountry').val(14).trigger('change.select2');
    GetStateByCountry(14, 'Y');
    $('#ddlReferral').val('RC1459').trigger('change.select2');
    $('#AnonymousInformation  #ddlPatientType').val(1).trigger('change.select2');
    $('#AnonymousInformation #ddlPanel').val(1).trigger('change.select2');
    $('#AnonymousInformation #ddlSource').prop('selectedIndex', '0').trigger('change.select2');
    $('#AnonymousInformation #ddlVisitPurpose').prop('selectedIndex', '0').trigger('change.select2');
    $('#AnonymousInformation #ddlDept').prop('selectedIndex', '0').trigger('change.select2');
    $('#AnonymousInformation #ddlDoctor').prop('selectedIndex', '0').trigger('change.select2');
    $('#AnonymousInformation #ddlItemName').empty();
    $('#AnonymousInformation #ddlItemName').append($('<option></option>').val('Select').html('Select')).trigger('change.select2');;
    $('#AnonymousInformation #ddlVisitType').prop('selectedIndex', '0').trigger('change.select2');
    $('#AnonymousInformation').find('input[type=text],input[type=date]:not(#txtAppointmentOn)').val('');
    $('#PaymentInformation').find('input[type=text]:not(#txtDisResason)').val(0);
    $('#PaymentInformation').find('input[id=txtDisResason]').val('');
    $('#PaymentInformation').find('select').prop('selectedIndex', '0').trigger('change.select2');
    $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
    $('input[name=PaymentMode]:not(.cash)').prop('checked', false).change();
    FillCurrentDate("txtAppointmentOn");
    $('#txtUHID').val('New');
    $('.paymentSection span:eq(3) span:last').text('00');
    $('.paymentSection span:eq(3) span:first').text('----------');
}
function query() {
    var vars = [], hash;
    var url = window.location.href.replace('#', '');
    var hashes = url.slice(url.indexOf('?') + 1).split('&');
    for (i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[decodeURIComponent(hash[0])] = decodeURIComponent(hash[1]);
    }
    return vars;
}
function TriggerEnter() {
    $('input[id=txtSearchValue]').on('keyup', function (e) {

        if (e.keyCode == 13) {
            $('#btnSearchOldPatient').trigger('click');
        }
    });
}
function VerifyEmpForDiscount() {
    if ($('#txtEmpCodeForDiscount').val() == '') {
        alert('Please Provide Employee Code.');
        $('#txtEmpCodeForDiscount').focus();
        return
    }
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.prm_1 = $('#txtEmpCodeForDiscount').val();
    objBO.SearchValue = "AppointmentBooking";
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'VerifyEmpForDiscount';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            if (data.Msg.includes('Mobile No not Found')) {
                alert("Mobile No not Found");
                return
            }
            if (JSON.parse(data.Msg.split('|')[3]).messages[0].status.groupName.includes('PENDING')) {
                _empDiscountInfo.emp_name = data.Msg.split('|')[0];
                _empDiscountInfo.perc = data.Msg.split('|')[1];
                _empDiscountInfo.otp = data.Msg.split('|')[2];
                alert("Sent");
                $('#txtVerifyOTP').focus();
            }
            else {
                alert(JSON.parse(data.Msg.split('|')[3]).messages[0].status.groupName);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AuthenticateOTP() {
    var val = $('#txtVerifyOTP').val();
    if (val == '') {
        alert('Please Provide OTP');
        $('#txtVerifyOTP').focus();
        return;
    }
    if (val != _empDiscountInfo.otp) {
        alert('Incorrect OTP.');
        $('#txtVerifyOTP').val('');

        return;
    }
    if (val == _empDiscountInfo.otp) {
        otp = '';
        $('#txtDiscountByPer').val(_empDiscountInfo.perc).keyup();
        $('#txtDisResason').val(_empDiscountInfo.emp_name + ':' + _empDiscountInfo.perc).addClass('lock');
        $('#txtVerifyOTP').val('');
        _empDiscountInfo = {};
    }
}
