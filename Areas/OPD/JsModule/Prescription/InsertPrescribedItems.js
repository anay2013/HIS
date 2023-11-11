var counterP = 11;
$(document).ready(function () {
    $('#tblTemplateMaster tbody').on('click', '.fa-edit', function () {
        selectRow($(this));
        var templateId = $(this).data('templateid');
        var templateName = $(this).closest('tr').find('td:eq(0)').text();
        var Description = $(this).closest('tr').find('td:eq(1)').text();
        $('#txtTemplateName').val(templateName);
        $('#txtDescription').val(Description);
        $('#btnSaveTemplate').switchClass('btn-success', 'btn-warning').text('Update');
        $('#hiddenTemplateMasterId').text(templateId);
    })
    $('#tblTemplateMaster tbody').on('click', '.fa-arrow-right', function () {
        selectRow($(this));
        var templateId = $(this).data('templateid');
        $('#hiddenTemplateMasterId').text(templateId);
    })
    $('select').on('change', function () {
        var val = $(this).find('option:selected').text();
        $(this).nextAll('input[name=format]').val(val);
    });
    StartCountDown();
})
function StartCountDown() {
    var counter = counterP;
    var interval = setInterval(function () {
        counter--;
        $("#txtSubmitCounter").html((counter < 10) ? counter = '0' + counter : counter);
        // Display 'counter' wherever you want to display it.
        if (counter <= 0) {
            clearInterval(interval);
            InsertItemsForPres();
            StartCountDown();
            return;
        } else {
            // $('#txtRefDuration').val(counter);

        }
    }, 1000);
}

function InsertPresItems() {
    disableLoading();
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    $('.prescribedItem:not(templategroup[id=VitalSign]) span').each(function () {
        if ($(this).parents('.prescribedItem').attr('id') != 'VitalSignItems') {
            objItems.push({
                'TemplateId': $(this).parents('.prescribedItem').find('templategroup').attr('id'),
                'ItemId': $(this).attr('id'),
                'ItemName': $(this).html().trim().replace(',', ''),
                'Remark': '-',
            });
        }
    })

    $('#PrescribedMedicine .MedicineTemplate tbody tr').each(function () {
        if ($(this).find('td:eq(0)').text().length > 3) {
            objMedicine.push({
                'app_no': Active.AppId,
                'DoctorId': Active.doctorId,
                'Item_id': $(this).data('itemid'),
                'Item_name': $(this).find('td:eq(0)').text().replace('X', '').trim(),
                'med_dose': $(this).find('td:eq(1)').text().trim(),
                'med_times': $(this).find('td:eq(2)').text().trim(),
                'med_duration': $(this).find('td:eq(3)').text(),
                'med_intake': $(this).find('td:eq(4)').text(),
                'med_route': $(this).find('td:eq(5)').text(),
                'qty': $(this).find('td:eq(6)').text(),
                'remark': $(this).find('td:eq(7)').text(),
            });
        }
    });
    ipPrescription.DoctorId = Active.doctorId;
    ipPrescription.app_no = Active.AppId;
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItems';

    objBO.objItems = objItems;
    objBO.objMedicine = objMedicine;
    objBO.ipPrescription = ipPrescription;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
            }
        },
        complete: function () { loading(); },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertMedicineItems() {
    $('div#skill').show();
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    $('.prescribedItem:not(templategroup[id=VitalSign]) span').each(function () {
        if ($(this).parents('.prescribedItem').attr('id') != 'VitalSignItems') {
            objItems.push({
                'TemplateId': $(this).parents('.prescribedItem').find('templategroup').attr('id'),
                'ItemId': $(this).attr('id'),
                'ItemName': $(this).html().trim().replace(',', ''),
                'Remark': '-'
            });
        }
    });
    $('#PrescribedMedicine .MedicineTemplate tbody tr').each(function () {
        if ($(this).find('td:eq(0)').text().length > 3) {
            objMedicine.push({
                'app_no': Active.AppId,
                'DoctorId': Active.doctorId,
                'Item_id': $(this).data('itemid'),
                'Item_name': $(this).find('td:eq(0)').text().replace('X', '').trim(),
                'med_dose': $(this).find('td:eq(1)').text().trim(),
                'med_times': $(this).find('td:eq(2)').text().trim(),
                'med_duration': $(this).find('td:eq(3)').text(),
                'med_intake': $(this).find('td:eq(4)').text(),
                'med_route': $(this).find('td:eq(5)').text(),
                'qty': $(this).find('td:eq(6)').text(),
                'remark': $(this).find('td:eq(7)').text(),
            });
        }
    });
    ipPrescription.DoctorId = Active.doctorId;
    ipPrescription.app_no = Active.AppId;
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'MedicineItems';

    objBO.objItems = objItems;
    objBO.objMedicine = objMedicine;
    objBO.ipPrescription = ipPrescription;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {

            }
        },
        complete: function () {
            setInterval(function () {
                $('div#skill').hide();
            }, 1000)
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertItemsForPres() {
    InsertPresItems();
    InsertMedicineItems();
}
function GetCPOEMedicineTemplateInfo(templateId) {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.TemplateId = templateId;
    objBO.DoctorId = Active.doctorId;
    objBO.Logic = 'GetCPOEMedicineTemplateInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            $('#MedicineTemplateForDB tbody').empty();
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr data-itemid=" + val.Item_id + ">";
                        tbody += "<td>" + val.Item_name + "</td>";
                        tbody += "<td>" + val.med_dose + "</td>";
                        tbody += "<td>" + val.med_times + "</td>";
                        tbody += "<td>" + val.med_duration + " Days</td>";
                        tbody += "<td>" + val.med_intake + "</td>";
                        tbody += "<td>" + val.med_route + "</td>";
                        tbody += "<td>" + val.remark + "</td>";
                        tbody += "<td><button class='btn btn-danger btn-xs' onclick=DeleteMedicineInfo('" + val.med_TemplateId + "','" + val.Item_id + "',this)><i class='fa fa-trash'>&nbsp;</i>Delete</button></td>";
                        tbody += "</tr>";
                    });
                    $('#MedicineTemplateForDB tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetMedicineTemplate() {
    $('#tblTemplateMaster tbody').empty();
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.DoctorId = Active.doctorId;
    objBO.Logic = 'DoctorTemplateItems';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table9).length > 0) {
                    $.each(data.ResultSet.Table9, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.med_TemplateName + "</td>";
                        tbody += "<td>" + val.Description + "</td>";
                        tbody += "<td>";
                        tbody += "<span data-templateid=" + val.med_TemplateId + " onclick=DeleteMediTemplate('" + val.med_TemplateId + "',this) class='btn-danger fa fa-trash btnTempMaster'></span>&nbsp;";
                        tbody += "<span data-templateid=" + val.med_TemplateId + " class='btn-warning fa fa-edit btnTempMaster'></span>&nbsp;";
                        tbody += "<span data-templateid=" + val.med_TemplateId + " onclick=GetCPOEMedicineTemplateInfo('" + val.med_TemplateId + "') class='btn-primary fa fa-arrow-right btnTempMaster'></span>";
                        tbody += "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTemplateMaster tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertMedicineTemplate() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
    var objBO = {};
    objBO.DoctorId = Active.doctorId;
    objBO.TemplateId = $('#hiddenTemplateMasterId').text();
    objBO.TemplateName = $('#txtTemplateName').val();
    objBO.Description = $('#txtDescription').val();
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveTemplate').text() == 'Save') ? 'InsertCPOEMedicineTemplate' : 'UpdateCPOEMedicineTemplate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                GetMedicineTemplate();
                ClearTemplateMaster();
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
function CloseAppointment() {
    if (confirm('Are you sure want to close this Appointment.')) {
        var url = config.baseUrl + "/api/Appointment/Opd_AppointmentUpdate";
        var objBO = {};
        var d = new Date();
        objBO.app_no = Active.AppId;
        objBO.AppDate = d;
        objBO.AppInTime = '02:49';
        objBO.AppOutTime = '02:55';
        objBO.Logic = 'CloseAppointment';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    alert('Appointment Closed Successfully..');
                    window.location.href = 'OPD_ViewConsultation';
                    sessionStorage.removeItem('AppId');
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
function InOutMarking() {
    if (Active.AppId == '') {
        alert('Appointment No Not Found.');
        return
    }
    var url = config.baseUrl + "/api/Appointment/Opd_InOutMarking";
    var objBO = {};
    objBO.DoctorId = Active.doctorId;
    objBO.BookingNo = Active.AppId;
    objBO.inputDate = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = "DoctorRoom_OUT";

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert('Appointment Closed Successfully..');
                window.location.href = config.rootUrl + '/OPD/Appointment/OPD_ViewConsultation?mid=SM186';
                sessionStorage.removeItem('AppId');
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
function DeleteMediTemplate(tempid, elem) {
    if (confirm('Are you sure to delete this Template? All Related Medicine Items will be deleted from this Template Group.')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
        var objBO = {};
        objBO.DoctorId = Active.doctorId;
        objBO.TemplateId = tempid;
        objBO.login_id = Active.userId;
        objBO.Logic = 'DeleteCPOEMedicineTemplate';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('success')) {
                    GetCPOEMedicineTemplateInfo(tempid);
                    $(elem).closest('tr').remove();
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
function InsertMedicineTemplateInfo() {
    if (ValidationMediInfo()) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertMedicineTemplateInfo";
        var objBO = {};
        objBO.med_TemplateId = $('#hiddenTemplateMasterId').text();
        objBO.DoctorId = Active.doctorId;
        objBO.Item_id = $('#txtItemID').val();
        objBO.Item_name = $('#txtSearchProduct').val();
        objBO.med_dose = $('#txtFreqMaster').val();
        objBO.med_times = $('#txtDuration').val();
        objBO.med_duration = $('#txtDuration').val();
        objBO.med_intake = $('#txtIntake').val();
        objBO.med_route = $('#txtRoute').val();
        objBO.qty = $('#txtQty').val();
        objBO.remark = $('#txtRemark').val();
        objBO.login_id = Active.userId;
        objBO.Logic = 'InsertMedicineTemplateInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('success')) {
                    GetCPOEMedicineTemplateInfo(objBO.med_TemplateId);
                    $('.modal-body').find('input:text').val('');
                    $('.modal-body').find('select').prop('selectedIndex', '0').change();
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
function DeleteMedicineInfo(tempid, itemid, elem) {
    if (confirm('are you sure?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertMedicineTemplateInfo";
        var objBO = {};
        objBO.med_TemplateId = tempid;
        objBO.DoctorId = Active.doctorId;
        objBO.Item_id = itemid;
        objBO.Logic = 'DeleteMedicineInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('success')) {
                    $(elem).closest('tr').remove();
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
function ClearTemplateMaster() {

    $('#txtTemplateName').val('');
    $('#txtDescription').val('');
    $('#hiddenTemplateMasterId').text('');
    $('#btnSaveTemplate').switchClass('btn-warning', 'btn-success').text('Save');

}
function ValidationMediInfo() {
    var hiddenTemplateId = $('#hiddenTemplateMasterId').text();
    var Item_id = $('#txtItemID').val();
    var Item_name = $('#txtSearchProduct').val();
    var med_dose = $('#txtFreqMaster').val();
    var med_times = $('#txtDuration').val();
    var med_duration = $('#txtDuration').val();
    var med_intake = $('#txtIntake').val();
    var med_route = $('#txtRoute').val();
    var qty = $('#txtQty').val();
    var remark = $('#txtRemark').val();

    if (hiddenTemplateId == '') {
        alert('Please Choose Template From Left Template List');
        return false;
    }
    if (Item_id == '') {
        alert('Please Choose Medicine..');
        $('#txtSearchProduct').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtSearchProduct').removeAttr('style');
    }
    if (Item_name == '') {
        alert('Please Choose Item Name');
        $('#txtSearchProduct').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtSearchProduct').removeAttr('style');
    }
    if (med_dose == 'Select') {
        alert('Please Choose Dose..');
        $('#txtFreqMaster').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtFreqMaster').removeAttr('style');
    }
    if (med_intake == 'Select') {
        alert('Please Choose In Take..');
        $('#txtIntake').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtIntake').removeAttr('style');
    }
    //if (med_duration=='') {
    //	alert('Please Provide Duration..');
    //	$('#txtDuration').css('border-color', 'red');
    //	return false;
    //}
    //else {
    //	$('#txtDuration').removeAttr('style');
    //}
    //if (med_duration == '') {
    //	alert('Please Provide Duration..');
    //	$('#txtDuration').css('border-color', 'red');
    //	return false;
    //}
    //else {
    //	$('#txtDuration').removeAttr('style');
    //}
    //if (qty == '') {
    //	alert('Please Provide Qty..');
    //	$('#txtQty').css('border-color', 'red');
    //	return false;
    //}
    //else {
    //	$('#txtQty').removeAttr('style');
    //}
    return true;
}
