var _IPDNo = "";
$(document).ready(function () {
    OnLoadQueries();
    $('#tblPaymentDetails tbody').on('keyup', 'input[type=text]', function () {
        var netAmount = parseFloat($('#txtNetAmount').val());
        var cash = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(0)').find('td:eq(1)').find('input[type=text]').val());
        var cheque = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(1)').find('td:eq(1)').find('input[type=text]').val());
        var cc = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(2)').find('td:eq(1)').find('input[type=text]').val());
        var ntfs = parseFloat($('#tblPaymentDetails tbody').find('tr:eq(3)').find('td:eq(1)').find('input[type=text]').val());
        var total = cash + cheque + cc + ntfs;
        if (total > netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be more than Net Amount...!').css({ 'color': 'red', 'font-size': '11px' });
        }
        else if (total < netAmount) {
            $(this).css('border-color', 'red');
            $('#txtError').text('Paid Amount can not be less than Net Amount...!').css({ 'color': '#bb8202', 'font-size': '11px' });
        }
        else {
            $('#tblPaymentDetails tbody').find('tr:eq(0),tr:eq(1),tr:eq(2),tr:eq(3)').find('td:eq(1)').find('input[type=text]').removeAttr('style');
            $('#txtError').text('').removeAttr('style');
        }
    });
    $('#tblOldPatient tbody').on('click', 'button', function () {
        _IPDNo = $(this).closest('tr').find('td:eq(1)').text();
        debugger
        ReceiptInfo()
    });
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
});
function ReceiptInfo() {
    $('#tblPaymentInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'fin_ReceiptInfo:OPD';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        count++;
                        if (temp != val.ReceiptNo) {
                            tbody += "<tr style='background:#ddd'>";
                            tbody += "<td colspan='5'><b>Receipt No : </b>" + val.ReceiptNo;
                            tbody += "<button style='height: 15px;line-height: 0;' onclick=PrintReceipt('" + val.ReceiptNo + "') class='btn btn-warning btn-xs pull-right'><i class='fa fa-print'>&nbsp;</i>Print</buton>";
                            tbody += "</td>";
                            tbody += "</tr>";
                            temp = val.ReceiptNo;
                            count = 1;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.receiptDate + "</td>";
                        tbody += "<td class='text-right'>" + val.Amount + "</td>";
                        tbody += "<td>" + val.PayMode + "</td>";
                        tbody += "<td>" + val.remark + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPaymentInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PrintReceipt(receiptNo) {
    var url = "../Print/AdvanceReceipt?ReceiptNo=" + receiptNo + "&loginName=" + Active.userName;
    window.open(url, '_blank');
}
function OnLoadQueries() {
    var url = config.baseUrl + "/api/IPDNursing/IPD_RegistrationQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = '';
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
                if (Object.keys(data.ResultSet.Table3).length) {
                    $('#tblPaymentDetails tbody .BankName').empty().append($('<option></option>')).change();
                    $.each(data.ResultSet.Table3, function (key, val) {
                        $('#tblPaymentDetails tbody .BankName').append($('<option></option>').val(val.BankId).html(val.BankName));
                    });
                }
                if (Object.keys(data.ResultSet.Table4).length) {
                    $('#tblPaymentDetails tbody .MachineName').empty().append($('<option value="Select">Select</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table4, function (key, val) {
                        if (val.usedFor == 'SwipeCard')
                            $('#tblPaymentDetails tbody .MachineName:eq(0)').append($('<option></option>').val(val.machineId).html(val.machineName));

                        if (val.usedFor == 'Online')
                            $('#tblPaymentDetails tbody .MachineName:eq(1)').append($('<option></option>').val(val.machineId).html(val.machineName));
                    });
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
function PatientAdvance() {
    if (confirm('Are you sure?')) {
        if ($('#txtUHID').val().toLowerCase() == 'new' || $('#txtUHID').val() == '') {
            alert('UHID Not Found.');
            return
        }
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
        var url = config.baseUrl + "/api/IPDNursing/IPD_TakeAdvance";
        var objBooking = {};
        var objPayment = [];
        var totalAmount = 0;

        $('#tblPaymentDetails tbody tr.pay').each(function () {
            var Amount = parseFloat($(this).find('td:eq(1)').find('input[type=text]').val());
            totalAmount += parseFloat($(this).find('td:eq(1)').find('input[type=text]').val());

            if ($('input[name=Advance-type]:checked').data('adv') == 'AdvanceReturn')
                Amount = -Amount;

            objPayment.push({
                'ReceiptNo': '-',
                'PayMode': $(this).find('td:eq(0)').text(),
                'CardNo': '-',
                'BankName': $(this).find('td:eq(3)').find('select option:selected').text(),
                'RefNo': $(this).find('td:eq(4)').find('input[type=text]').val(),
                'MachineId': $(this).find('td:eq(5)').find('select option:selected').val(),
                'MachineName': $(this).find('td:eq(5)').find('select option:selected').text(),
                'Amount': Amount,
                'OnlPaymentId': '',
                'OnlPayStatus': '',
                'OnlPayResponse': '',
                'OnlPaymentDate': new Date(),
                'login_id': Active.userId
            });
        });
        if ($('input[name=Advance-type]:checked').data('adv') == 'AdvanceReturn') {
            if (totalAmount > parseFloat($('.advnc-amount-section #txtAdvanceAmt').val())) {
                alert('Advance Return Amount Shound not be greater than Advance Amount.');
                return;
            }
        }
        if ($('input[name=Advance-type]:checked').data('adv') == 'FinalSettlement') {
            if (totalAmount != Math.abs(parseFloat($('.advnc-amount-section #txtBalanceAmt').val()))) {
                alert('Final Settlement Amount Shound be Equal to Balance Amount.');
                return;
            }
        }

        objBooking.IPDNo = $('#txtUHID').val();
        objBooking.hosp_id = Active.unitId;
        objBooking.IPOPType = 'OPD';
        objBooking.ReceiptType = $('input[name=Advance-type]:checked').val();
        objBooking.Prm1 = '-';
        objBooking.Prm2 = '-';
        objBooking.Remark = '-';
        objBooking.login_id = Active.userId;
        objBooking.Logic = 'IPD-PatientAdvance';
        var MasterObject = {};
        MasterObject.objBooking = objBooking;
        MasterObject.objPayment = objPayment;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(MasterObject),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert('Successfully Saved..!');
                    $('#tblPaymentDetails tbody').find('input[type=text]').val(0);
                    $('#tblPaymentDetails tbody tr').hide();
                    $('#tblPaymentDetails tbody tr:eq(0)').show();
                    $('input[name=PaymentMode]').prop('checked', false);
                    $('input[name=PaymentMode]:eq(0)').prop('checked', true);
                    $('.left-section input:text').val('');
                    $('.left-section #txtUHID').val('NEW');
                    $('.left-section #txtDOB').val('');
                    ReceiptInfo();
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
function removeMinus(elem) {
    $(elem).val().replace('-');
}

function InsertPatientMaster() {
    if ($('#txtMobileNo').val() == '') {
        alert('Please Provide Mobile No');
        $('#txtMobileNo').focus()
        return;
    }
    var url = config.baseUrl + "/api/Appointment/InsertPatientMaster";
    var objPatient = {};
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
    objPatient.dob = $('#txtDOB').val();
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
    objPatient.locality = $('#ddlLocality option:selected').text();
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
    objPatient.Logic = 'ItdoseNewEntry';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objPatient),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (data.includes('Success')) {
                alert(data);
                _IPDNo = data.split('|')[0];
                $('#txtUHID').val(_IPDNo);
                GetOldPatientByUHID(_IPDNo);
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