﻿var dataDoseAndInTake = [];
var dataVal = [];
var activeSeachbox;
var IsActiveSeachbox = false;
var _DoctorId = null;
$(document).ready(function () {
    CloseSidebar()
    PatientHeaderInfo();
    GetPrescriptionInfo();
    GetVisualAcuityInfo();
    PresMedicineInfo();
    FreqAndIntake();
    GetSpecInfo();
    GetPresTestInfo();
    GetTemplateMaster();
    GetOtherInfo();
    searchTable('txtSearchGroup', 'tblGroup');
    searchList('txtSearchLaboratoryRadiology', 'LaboratoryRadiologyList');
    var vaVal;
    $('#tblTemplateMaster tbody').on('click', '.fa-edit', function () {
        debugger
        selectRow($(this));
        var templateId = $(this).data('templateid');
        var templateName = $(this).closest('tr').find('td:eq(0)').text();
        var Description = $(this).closest('tr').find('td:eq(1)').text();
        $('#txtTemplateName').val(templateName);
        $('#txtDescription').val(Description);
        $('#btnSavePresTemplate').switchClass('btn-success', 'btn-warning').text('Update');
        $('#hiddenTemplateMasterId').text(templateId);
    })
    $('#tblTemplateMaster tbody').on('click', '.fa-arrow-right', function () {
        selectRow($(this));
        var templateId = $(this).data('templateid');
        $('#hiddenTemplateMasterId').text(templateId);
    })
    $('table thead').find('input[type=text]').attr('placeholder', 'Search..');
    $('#tblVisualAcuity').on('click', 'button.fillValue', function () {
        vaVal = $(this);
        $('#modalVisualAcuity').modal('show');
    });
    $('#modalVisualAcuity table td').on('click', function () {
        var val = $(this).text();
        $(vaVal).siblings('input:text').val(val);
        InsertVisualAcuity();
        $('#modalVisualAcuity').modal('hide');
    });//Medicine Template On Row Editing
    $('.MedicineTemplate:eq(0) tbody').on('mouseover', 'tr', function () {
        $(this).find('td:first').find('remove').show()
    }).on('mouseleave', 'tr', function () {
        $(this).find('td:first').find('remove').hide()
    });

    $(document).on('click', function (e) {
        var imgRx = $('#imgRx');
        if ($(imgRx).is(e.target)) {
            if (confirm('Are you sure to delete all Medicine?')) {
                $('.MedicineTemplate tbody').empty();
            }
        }
        var container = $('.MedicineTemplate:eq(0)');
        if ($(container).has(e.target).length === 0) {
            if (IsActiveSeachbox)
                return

            $('.MedicineTemplate:eq(0) tbody td').each(function () {
                if ($(this).index() == 0)
                    return

                if ($(this).index() == 1) {
                    if ($(this).find('label').length > 0) {
                        if ($(this).text().trim() == '') {
                            $(this).closest('tr').remove();
                            return
                        }
                    }
                }

                //$(this).find('.delRow').remove();

                $(this).removeAttr('style');
                var content = '';
                if ($(this).find('input:text').length > 0)
                    content = $(this).find('input:text').val();
                else if ($(this).find('select').length > 0)
                    content = $(this).find('label').text() + ' ' + $(this).find('select option:selected').text();
                else if ($(this).find('label').length > 0)
                    content = $(this).find('label').text();
                else
                    content = $(this).text();

                if ($(this).index() == 0)
                    $(this).html("<remove class='delRow'>X</remove>" + content);
                else
                    $(this).html(content);

            });
        }
    });
    $(document).find('.MedicineTemplate:eq(0) tbody').on('click', '.delRow', function () {
        $(this).closest('tr').remove();
        if ($('#PrescribedMedicine .MedicineTemplate:eq(0) tbody tr').length == 0)
            $('.OPDPrintPreview #PrescribedMedicine').hide();
    });
    $('.MedicineTemplate:eq(0) tbody').on('click', 'td', function (e) {
        if ($(this).index() == 0)
            return
        if ($(this).find('label.editable').length === 1)
            return
        $(this).closest('tr').find('td').each(function () {
            if ($(this).index() == 0)
                return

            $(this).find('.delRow').remove();
            var content = $(this).text();
            $(this).empty().css('padding', '2px');
            var data = "<label class='editable' contenteditable='true'>" + content + "</label>";

            if ($(this).index() == 0)
                $(this).html("<remove class='delRow'>X</remove>" + data);
            else
                $(this).html(data);
        })
        $(this).find('.editable').focus();
    });
    $(document).find('.MedicineTemplate:eq(0) thead').on('click', '.addmedNewRow', function () {
        var tbody = "";
        tbody += "<tr data-itemid='newId'>";
        tbody += "<td style='padding:2px;'><remove class='delRow'>X</remove><button id='btnEye' class='btn btn-success btn-xs'>BE</button></td>";
        tbody += "<td style='padding:2px;'><label class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='padding:2px;'><label id='med1' onkeyup=Dose(this) class='editable' contenteditable='true'>ONE DROP</label></td>";
        tbody += "<td style='padding:2px;'><label id='med2' class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='display: flex;'><label id='med3' class='editable' contenteditable='true'></label>";
        tbody += "<select class='editable'>";
        tbody += "<option>Day</option>";
        tbody += "<option>Week</option>";
        tbody += "<option>Month</option>";
        tbody += "</select>";
        tbody += "</td>";
        tbody += "<td style='padding:2px;'><label id='med5' onkeyup=Route(this) class='editable' contenteditable='true'>TOPICAL</label></td>";
        tbody += "<td style='padding:2px;'><label id='med6' class='editable' contenteditable='true'></label></td>";
        tbody += "</tr>";
        $('.MedicineTemplate:eq(0) tbody').append(tbody);
    });
    $('.MedicineTemplate:eq(0) tbody').on('keyup', 'label', function (e) {
        if ($('input[id=IsDB]').is(':checked') && $(this).closest('td').index() == 1) {
            var val = $(this).text();
            if (val.length > 2)
                SearchPresMedicine(val, 'ETHICAL', this);

            $(this).autocomplete({
                source: dataVal,
                focus: function (event, ui) {
                    $(this).text(ui.item.value.split('~')[0])
                    return false;
                },
                select: function (event, ui) {
                    $(this).text(ui.item.value.split('~')[0])
                    return false;
                }
            });
        }
        //if (e.keyCode == 13 && $(this).closest('td').index() == 1) {
        //    e.preventDefault();
        //    activeSeachbox = $(this);
        //    IsActiveSeachbox = true;
        //    $('#modalMedicineSeach').modal('show')
        //    $('#modalMedicineSeach').on('shown.bs.modal', function () {
        //        $('#txtSearchProduct').focus();
        //    })
        //}       
    });
    $('#PatientVisits #tblPatientVisits tbody').on('click', '.currentVisit', function () {
        var appno = $(this).closest('tr').find('td:eq(0)').text();
        sessionStorage.setItem('AppId', appno);
        $('.modal-body').find('input[name=Lens]').prop('checked', false);
        $('input:checkbox').prop('checked', false);
        $('input:text').val('');
        PatientHeaderInfo();
        GetPrescriptionInfo();
        GetVisualAcuityInfo();
        PresMedicineInfo();
        FreqAndIntake();
        GetSpecInfo();
        GetPresTestInfo();
        GetTemplateMaster();
        GetOtherInfo();
    });
    $('#btnNewTestTemplate').on('click', function () {
        $('#ddlDoctorTemplate option').each(function () {
            if ($(this).val() == 'T00009') {
                $('#ddlDoctorTemplate').prop('selectedIndex', '' + $(this).index() + '').change();
            }
        });
        $('#divTemplate').slideToggle('show');
    });
    $('#btnSaveDoctorTemplate').on('click', function () {
        var val = $(this).find('i').hasClass('fa-plus');
        if (val == true) {
            InsertTemplateInfo();
        }
        else {
            UpdateTemplateInfo();
        }
    });
    $('.divCustom').on('click', '.btnCMEyesInfo', function () {
        var val = $(this).text();
        switch (val) {
            case (val = 'LE'):
                $(this).text('RE').switchClass('btn-success', 'btn-warning');
                return;
            case (val = 'RE'):
                $(this).text('BE').switchClass('btn-warning', 'btn-danger');
                return;
            default:
                $(this).text('LE').switchClass('btn-danger', 'btn-success');
                return;
        }
    });
    //$('table tbody').on('click', '#btnEye', function () {
    //    var val = $(this).text();
    //    switch (val) {
    //        case (val = 'LE'):
    //            $(this).text('RE').switchClass('btn-success', 'btn-warning').siblings('input:checkbox').prop('checked', true);
    //            return;
    //        case (val = 'RE'):
    //            $(this).text('BE').switchClass('btn-warning', 'btn-danger').siblings('input:checkbox').prop('checked', true);
    //            return;
    //        default:
    //            $(this).text('LE').switchClass('btn-danger', 'btn-success').siblings('input:checkbox').prop('checked', true);
    //            return;
    //    }
    //}); 
    $('table tbody').on('click', '#btnEye', function () {
        var val = $(this).text();
        switch (val) {
            case (val = 'NA'):
                $(this).text('BE').switchClass('btn-info', 'btn-success');
                return;
            case (val = 'BE'):
                $(this).text('RE').switchClass('btn-success', 'btn-warning');
                return;
            case (val = 'RE'):
                $(this).text('LE').switchClass('btn-warning', 'btn-danger');
                return;
            default:
                $(this).text('NA').switchClass('btn-danger', 'btn-info');
                return;
        }
    });
    $('ul').on('mouseenter', 'li', function () {
        $('.delete').hide();
        $(this).find('.delete').show();
    }).on('mouseleave', 'li', function () {
        $('.delete').hide();
    });
    $('select').on('change', function () {
        var val = $(this).find('option:selected').text();
        $(this).nextAll('input[name=format]').val(val);
    });
    $('#modalDistance table td').on('click', function () {
        var glass = $('#ChooseGlass button.SelectedGlass').data('glass');
        var type = $(this).parents('table').attr('id').replace('tbl', '');
        var val = $(this).text().replace('°', '');
        $('#txt' + glass + type).val(val);
    });
    $('#modalNear table td').on('click', function () {
        var type = $(this).parents('table').attr('id').replace('tbl', '');
        var val = $(this).text();
        $('#txt' + type).val(val);
    });
    $('#ChooseGlass').on('click', 'button', function () {
        $('#ChooseGlass button:eq(0)').text('Right Glass');
        $('#ChooseGlass button:eq(1)').text('Left Glass');
        $('#ChooseGlass button').removeClass('SelectedGlass', 1000);
        $(this).addClass('SelectedGlass', 1000).append('&nbsp;<i class="fa fa-check-circle">&nbsp;</i>');
    });
    $('#txtSearchProduct').keydown(function (e) {
        debugger
        var tbody = $('#tblnavigate').find('tbody');
        var selected = tbody.find('.selected');
        var KeyCode = e.keyCode;
        switch (KeyCode) {
            case (KeyCode = 40):
                tbody.find('.selected').removeClass('selected');
                if (selected.next().length == 0) {
                    tbody.find('tr:first').addClass('selected');
                }
                else {
                    tbody.find('.selected').removeClass('selected');
                    selected.next().addClass('selected');
                }
                break;
            case (KeyCode = 38):
                tbody.find('.selected').removeClass('selected');
                if (selected.prev().length == 0) {
                    tbody.find('tr:last').addClass('selected');
                }
                else {
                    selected.prev().addClass('selected');
                }
                break;
            case (KeyCode = 13):
                var itemName = $('#tblnavigate').find('tbody').find('.selected').text().split('#')[0];
                var itemid = $('#tblnavigate').find('tbody').find('.selected').data('itemid');
                var IsCash = $('#tblnavigate').find('tbody').find('.selected').data('iscash');
                var msg = $('#tblnavigate').find('tbody').find('.selected').data('alertmsg');
                if (msg != '-') {
                    $('.msg').html('<p>' + msg + '</p><span>X</span>').show();
                }
                e.preventDefault()
                $('#txtQuantity').focus();
                $('#txtItemID').val(itemid);
                $('#txtIsCash').val(IsCash);
                $(activeSeachbox).text(itemName.trim().split('~')[0]);
                $('#modalMedicineSeach').modal('hide');
                $('#tblPresMedicineInfo tbody').find('label').eq($(activeSeachbox).index()).focus();
                IsActiveSeachbox = false;
                $('#ItemList').hide();
                break;
            default:
                var val = $('#txtSearchProduct').val();
                if (val == '') {
                    $('#ItemList').hide();
                }
                else {
                    $('#ItemList').show();
                    var key = $(this).val();
                    var type = $('#ddlSearchType option:selected').val();
                    if (ValidateSearch()) {
                        SearchMedicine(key, type);
                    };
                }
                break;
        }
    });
    $('div.msg').on('click', 'span', function () {
        $(this).closest('div').remove();
    });
    $('#tblnavigate tbody').on('click', 'tr', function () {
        var itemName = $(this).find('td:eq(0)').text();
        var itemid = $(this).data('itemid');
        var IsCash = $(this).data('iscash');
        var msg = $(this).data('alertmsg');
        if (msg != '-') {
            $('.msg').html('<p>' + msg + '</p><span>X</span>').show();
        }
        $('#txtSearchProduct').val(itemName).blur();
        $('#txtQuantity').focus();
        $('#txtItemID').val(itemid);
        $('#txtIsCash').val(IsCash);
        $('#tblnavigate tbody').empty();
        $('#ItemList').hide();
    });
    $('#LaboratoryRadiologyList').on('click', 'a.fa-heart', function () {
        var templateId = $(this).data('templateid');
        var itemid = $(this).data('itemid');
        var fav = $(this).data('fav');
        var temp = $(this).data('temp');
        var ul = $(this).parents('ul').attr('id');
        var itemName = $(this).closest('li').find('label').text();
        var isfav = (fav == false) ? 1 : 0;
        MarkAsFavourite(templateId, itemid, itemName, isfav, temp, ul);
    });
    $('#LaboratoryRadiologyList').on('click', 'a.fa-trash', function () {
        var templateId = $(this).data('templateid');
        var itemid = $(this).data('itemid');
        if (confirm('Are you sure want to delete this Item from Template..!')) {
            DeleteTemplateInfo(templateId, itemid);
        }
    });
    $('#LaboratoryRadiologyList').on('click', 'a.fa-pencil', function () {
        var templateId = $(this).data('templateid');
        var itemName = $(this).data('itemname');
        var itemid = $(this).data('itemid');
        var fav = $(this).data('fav');
        if (fav == true) {
            $('input[name=IsFavourite]').prop('checked', true);
        }
        else {
            $('input[name=IsFavourite]').prop('checked', false);
        }
        $('#ddlDoctorTemplate option').each(function () {
            if ($(this).val() == templateId) {
                $('#ddlDoctorTemplate').prop('selectedIndex', '' + $(this).index() + '').change();
            }
        });
        $('#hiddenDoctorTempItemId').text(itemid);
        $('#txtDoctorItemName').val(itemName);
        $('#btnSaveDoctorTemplate').switchClass('btn-success', 'btn-warning');
        $('#btnSaveDoctorTemplate i').switchClass('fa-plus', 'fa-edit');
        $('#divTemplate').slideToggle('show');
    });
    $('.search-box').on('click', 'button', function () {
        $('button').removeClass('rotate');
        $(this).addClass('rotate');
    });
    //select Checkbox Item from Template To Prev
    $('#LaboratoryRadiologyList').on('change', 'input:checkbox', function () {
        var template = $(this).parents('ul').attr('id');//get parent UL id on template item check
        var PrevTemplateId = template.replace('List', 'Items');//get id of related Template from Prev Side
        var itemId = $(this).data('itemid');//get item id on check
        var itemName = $(this).closest('li').find('label').text();//get item name on check
        var list = "";
        list += "<span id='" + itemId + "'>" + itemName + "</span><close class='remove'>X</close>";//create item list
        $('#' + PrevTemplateId).show();//show related template on prev side [default all template hide in prev side]
        if (this.checked)//on check
        {
            $('#' + PrevTemplateId).append(list);//add item to prev side template group
        }
        else//on un-check
        {
            $('#' + PrevTemplateId + ' span').each(function ()//on uncheck remove particular items from prev template group
            {
                if ($(this).text() == itemName) {
                    $(this).remove();
                }
            });
        }
    });
});

function Dose(elem) {
    var data = [];
    $.each(dataDoseAndInTake[0], function (key, val) {
        data.push(val.Descriptions)
    })
    $(elem).autocomplete({
        source: data
    });
}
function InTake(elem) {
    var data = [];
    $.each(dataDoseAndInTake[1], function (key, val) {
        data.push(val.instruction)
    })
    $(elem).autocomplete({
        source: data
    });
}
function Route(elem) {
    var data = [
        "Oral",
        "Nose",
        "Tropical"
    ];
    $(elem).autocomplete({
        source: data
    });
}
function SearchPresMedicine(key, type, elem) {
    disableLoading();
    dataVal = [];
    var url = config.baseUrl + "/api/IPDNursing/SearchMedicine";
    var objBO = {};
    objBO.searchKey = key;
    objBO.searchType = type;
    objBO.PanelId = $('span[data-panelid]').text();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        dataVal.push(val.item_name)
                    });
                }
            }
        },
        complete: function (response) {

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InOutMarking() {
    if (sessionStorage.getItem('AppId') == '') {
        alert('Appointment No Not Found.');
        return
    }
    var url = config.baseUrl + "/api/Appointment/Opd_InOutMarking";
    var objBO = {};
    objBO.DoctorId = _DoctorId;
    objBO.BookingNo = sessionStorage.getItem('AppId');
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
                window.location.href = config.rootUrl + '/OPD/Appointment/OPD_ViewConsultation';
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
function NextFollowUpVisit() {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentUpdate";
    var objBO = {};
    if ($('#txtFollowUpDate').val() == '') {
        alert('Please Provide Next Follow Up Date');
        return
    }
    objBO.UHID = $('#txtHeaderUHID').text();
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.DoctorId = _DoctorId;
    objBO.AppDate = $('#txtFollowUpDate').val();
    objBO.prm_1 = $('#txtFollowUpRemark').val();
    objBO.AppInTime = '00:00';
    objBO.AppOutTime = '00:00';
    objBO.login_id = Active.userId;
    objBO.Logic = 'NextFollowUpVisit';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Successfully')) {
                alert('Saved Successfully..');
                $('#txtFollowUpDate').val('');
                $('#txtFollowUpRemark').val('');
                $('#modalNextFollowUp').modal('hide');
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
        objBO.app_no = sessionStorage.getItem('AppId');
        objBO.DoctorId = _DoctorId;
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
function PrintReceipt() {
    var url = config.documentServerUrl + "/OPD/Print/OPTHReceipt?app_no=" + sessionStorage.getItem('AppId');
    window.open(url, '_blank');
}
function PatientHeaderInfo() {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.AppointmentId = sessionStorage.getItem('AppId');
    objBO.DoctorId = '-';
    objBO.UHID = '-';
    objBO.Logic = 'PatientForAdvice';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#tblOPTHHeaderInfo tbody').find('tr:eq(0)').find('td:eq(2)').text(val.patient_name);
                    $('#tblOPTHHeaderInfo tbody').find('tr:eq(0)').find('td:eq(6)').text(val.Age);
                    $('#tblOPTHHeaderInfo tbody').find('tr:eq(0)').find('td:eq(10)').text(val.app_no);
                    $('#tblOPTHHeaderInfo tbody').find('tr:eq(0)').find('td:eq(14)').text(val.AppDate);

                    $('#tblOPTHHeaderInfo').find('tr:eq(1)').find('td:eq(2)').text(val.DoctorName);
                    _DoctorId = val.DoctorId;
                    $('#tblOPTHHeaderInfo').find('tr:eq(1)').find('td:eq(6)').text(val.UHID);
                    $('#tblOPTHHeaderInfo').find('tr:eq(1)').find('td:eq(10)').text(val.PanelName);

                    //OPD Preview                   
                    if (val.IsLocked == 1) {
                        $('button[id=btnCloseApp]').attr('disabled', true);
                        $('button[id=btnSaveApp]').attr('disabled', true);
                    }
                });
                $('#PatientVisits #tblPatientVisits tbody').empty();
                var currentvisits = "";
                var visits = "";
                var tbody = "";
                var AppId = sessionStorage.getItem('AppId');
                $.each(data.ResultSet.Table6, function (key, val) {
                    if (val.app_no == AppId)
                        tbody += "<tr class='select-row'>";
                    else
                        tbody += "<tr>";

                    tbody += "<td>" + val.app_no + "</td>";
                    tbody += "<td>" + val.PatientVisits + "</td>";
                    tbody += "<td>" + val.doctorName + "</td>";
                    tbody += "<td class='currentVisit'><i class='fa fa-print'></i></td>";
                    tbody += "</tr>";

                });
                currentvisits += visits;
                $('#PatientVisits #tblPatientVisits tbody').append(tbody);
            }
            else {
                alert('No Record Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
    });
}
function GetGroupInfo(template) {
    $('#txtTemplate').text(template.split('|')[0]);
    $('#txtTemplate').data('templateid', template.split('|')[1]);
    $('#tblTemplateInfo tbody').empty();
    $('#tblGroup tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetGroupInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.CPOE_GrpName + "</td>";
                        tbody += "<td><button onclick=selectRow(this);GetItemInfoByGroup() class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblGroup tbody').append(tbody);
                }
            }
            $('#modalExamination').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetItemInfoByGroup() {
    $('#txtSelectedGroupName').text($('#tblGroup tbody tr.select-row').find('td:eq(1)').text());
    $('#tblTemplateInfo tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = $('#tblGroup tbody tr.select-row').find('td:eq(1)').text();
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetItemInfoByGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><button id='btnEye' class='btn btn-success btn-xs'>LE</button><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td class='flex'>";
                        tbody += "<input type='number' id='txtDuration' value='0' class='form-control duration'/>";
                        tbody += "<select id='ddlDay' class='form-control duration'>";
                        tbody += "<option>Days</option>";
                        tbody += "<option>Month</option>";
                        tbody += "<option>Year</option>";
                        tbody += "</select>";
                        tbody += "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTemplateInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPrescriptionInfo() {
    $('#ulExamination').empty();
    $('#ulDiagnosis').empty();
    $('#ulHistory').empty();
    $('#ulAdvice').empty();
    $('#ulChiefComplaints').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPrescriptionInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (temp != val.GroupName) {
                            tbody += "<li class='group'>" + val.GroupName + "</li> ";
                            temp = val.GroupName;
                        }
                        tbody += "<li data-itemid=" + val.ItemId + ">" + val.ItemName + "<span class='delete' onclick=DeleteItem(" + val.auto_id + ")>X</span></li> ";
                    });
                    $('#ulExamination').append(tbody);
                }
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody1 = "";
                    var temp1 = "";
                    var count = 0;
                    $.each(data.ResultSet.Table1, function (key, val) {
                        count++;
                        if (temp1 != val.GroupName) {
                            tbody1 += "<li class='group'>" + val.GroupName + "</li> ";
                            temp1 = val.GroupName;
                        }
                        tbody1 += "<li data-itemid=" + val.ItemId + ">" + val.ItemName + "<span class='delete' onclick=DeleteItem(" + val.auto_id + ")>X</span></li> ";
                    });
                    $('#ulDiagnosis').append(tbody1);
                }
                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    var tbody2 = "";
                    var temp2 = "";
                    var count = 0;
                    $.each(data.ResultSet.Table2, function (key, val) {
                        count++;
                        if (temp2 != val.GroupName) {
                            tbody2 += "<li class='group'>" + val.GroupName + "</li> ";
                            temp2 = val.GroupName;
                        }
                        tbody2 += "<li data-itemid=" + val.ItemId + ">" + val.ItemName + "<span class='delete' onclick=DeleteItem(" + val.auto_id + ")>X</span></li> ";
                    });
                    $('#ulAdvice').append(tbody2);
                }
                if (Object.keys(data.ResultSet.Table3).length > 0) {
                    var tbody2 = "";
                    var temp2 = "";
                    var count = 0;
                    tbody2 += "<li class='group'>Chief Complaint</li> ";
                    $.each(data.ResultSet.Table3, function (key, val) {
                        count++;
                        tbody2 += "<li data-itemid=" + val.ItemId + ">" + val.ItemName + "<span class='delete' onclick=DeleteItem(" + val.auto_id + ")>X</span></li> ";
                    });
                    $('#ulChiefComplaints').append(tbody2);
                }
                if (Object.keys(data.ResultSet.Table3).length > 0) {
                    var tbodyHistory = "";
                    var temp2 = "";
                    var count = 0;
                    tbodyHistory += "<li class='group'>History</li> ";
                    $.each(data.ResultSet.Table4, function (key, val) {
                        count++;
                        tbodyHistory += "<li data-itemid=" + val.ItemId + ">" + val.ItemName + "<span class='delete' onclick=DeleteItem(" + val.auto_id + ")>X</span></li> ";
                    });
                    $('#ulHistory').append(tbodyHistory);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertGroup() {
    if ($('#txtGroupName').val() == '') {
        alert('Please Provide Group Name.')
        return;
    }
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = 'Opthalmic';
    objBO.TemplateType = '';
    objBO.GroupName = $('#txtGroupName').val();
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data == 'success') {
                tempId = $('#txtTemplate').text() + '|' + objBO.TemplateId;
                GetGroupInfo(tempId);
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateGroupStatus(AutoId) {
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = '-';
    objBO.TemplateType = '-';
    objBO.GroupName = '-';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '-';
    objBO.ItemName = '-';
    objBO.prm_1 = AutoId;
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'UpdateGroupStatus';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data == 'success') {
                GetGroupInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertTemplateInfoAsGroup() {
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = 'Opthalmic';
    objBO.TemplateType = 'Opthalmic';
    objBO.GroupName = $('#txtSelectedGroupName').text();
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = $('#txtTemplate').data('templateid');
    objBO.ItemId = '';
    objBO.ItemName = $('#txtItemName').val();
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertTemplateInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                GetItemInfoByGroup();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeleteItem(AutoId) {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = AutoId;
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = '';
        objBO.app_no = '';
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = 'DeleteOPTHItems';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    GetPrescriptionInfo();
                    GetPresTestInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function DeleteAllItem(TemplateId) {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = 0;
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = TemplateId;
        objBO.app_no = sessionStorage.getItem('AppId');
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = "DeleteAllOPTHPresItems";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    GetPrescriptionInfo();
                    GetPresTestInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function DeleteAllPresMedicine() {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = 0;
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = '';
        objBO.app_no = sessionStorage.getItem('AppId');
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = "DeleteAllOPTHPresMedicine";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    PresMedicineInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function DeleteAllSpecInfo() {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = 0;
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = '';
        objBO.app_no = sessionStorage.getItem('AppId');
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = "DeleteAllOPTHSpecInfo";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    GetSpecInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function DeleteAllVisualAcuity() {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = 0;
        objBO.DoctorId = '';
        objBO.TemplateId = '';
        objBO.app_no = sessionStorage.getItem('AppId');
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = "DeleteAllOPTHVisualAcuity";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    GetVisualAcuityInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function DeleteAllReport() {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = 0;
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = '';
        objBO.app_no = sessionStorage.getItem('AppId');
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = "DeleteAllOPTHReport";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    GetOtherInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function InsertPresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};

    if ($('#tblTemplateInfo tbody input:checkbox:checked').length == 0) {
        alert('Item Not Selected.');
        return;
    };
    $('#tblTemplateInfo tbody tr').each(function () {
        if ($(this).closest('tr').find('td:eq(0)').find('input:checkbox').is(':checked')) {
            var duration = $(this).closest('tr').find('td:eq(3)').find('#txtDuration').val() + ' ' + $(this).closest('tr').find('td:eq(3)').find('select option:selected').text();
            objItems.push({
                'TemplateId': $('#txtTemplate').data('templateid'),
                'Duration': duration,
                'EyesInfo': $(this).closest('tr').find('td:eq(0)').find('button').text(),
                'ItemId': $(this).closest('tr').find('td:eq(1)').text(),
                'ItemName': $(this).closest('tr').find('td:eq(2)').text(),
                'Remark': '-',
            });
        }
    })
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItemsForOPTH';

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
                //alert(data)
                GetPrescriptionInfo();
                $('#modalExamination').modal('hide');
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
//Chief Complaints operation
function GetChiefComplaintInfo() {
    $('#modalChiefComplaint').modal('show');
    $('#tblChiefComplaintItem tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.ItemId = '';
    objBO.ItemName = 'Chief Complaint';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetItemInfoByGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><button id='btnEye' class='btn btn-success btn-xs'>BE</button><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td class='flex'>";
                        tbody += "<input type='number' id='txtDuration' value='0' class='form-control duration'/>";
                        tbody += "<select id='ddlDay' class='form-control duration'>";
                        tbody += "<option>Days</option>";
                        tbody += "<option>Month</option>";
                        tbody += "<option>Year</option>";
                        tbody += "</select>";
                        tbody += "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblChiefComplaintItem tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertChiefComplaintItem() {
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = 'Chief Complaint';
    objBO.TemplateType = 'Chief Complaint';
    objBO.GroupName = 'Chief Complaint';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = 'T00014';
    objBO.ItemId = '';
    objBO.ItemName = $('#txtChiefComplaintItemName').val();
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertTemplateInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                GetChiefComplaintInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertChiefComplaintPresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    if ($('#tblChiefComplaintItem tbody input:checkbox:checked').length == 0) {
        alert('Item Not Selected.');
        return;
    };
    $('#tblChiefComplaintItem tbody tr').each(function () {
        if ($(this).closest('tr').find('td:eq(0)').find('input:checkbox').is(':checked')) {
            var duration = $(this).closest('tr').find('td:eq(3)').find('#txtDuration').val() + ' ' + $(this).closest('tr').find('td:eq(3)').find('select option:selected').text();
            objItems.push({
                'TemplateId': 'T00014',
                'Duration': duration,
                'EyesInfo': $(this).closest('tr').find('td:eq(0)').find('button').text(),
                'ItemId': $(this).closest('tr').find('td:eq(1)').text(),
                'ItemName': $(this).closest('tr').find('td:eq(2)').text(),
                'Remark': '-',
            });
        }
    })
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItemsForOPTH';

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
                //alert(data)
                GetPrescriptionInfo();
                $('#modalChiefComplaint').modal('hide')
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertCustomMedicine(elem) {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    objItems.push({
        'TemplateId': $(elem).data('template'),
        'Duration': '0 Days',
        'EyesInfo': $(elem).closest('.divCustom').find('.btnCMEyesInfo').text(),
        'ItemId': 'fromtxt',
        'ItemName': $(elem).closest('.divCustom').find('#txtCustomMedicine').val(),
        'Remark': '-',
    });
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItemsForOPTH';

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
                //alert(data)
                GetPrescriptionInfo();
                $(elem).closest('.divCustom').find('#txtCustomMedicine').val('')
                $('.divCustom').hide('slow')
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
//Diagnosis Items operation
function InsertDiagnosisItem() {
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = 'Diagnosis';
    objBO.TemplateType = 'Diagnosis';
    objBO.GroupName = 'Diagnosis';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = 'T00012';
    objBO.ItemId = '';
    objBO.ItemName = $('#txtDiagnosisItemName').val();
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertTemplateInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                GetDiagnosisInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDiagnosisInfo() {
    $('#modalDiagnosis').modal('show');
    $('#tblDiagnosisItem tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.ItemName = 'Diagnosis';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetItemInfoByGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><button id='btnEye' class='btn btn-success btn-xs'>BE</button><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td class='flex'>";
                        tbody += "<input type='number' id='txtDuration' value='0' class='form-control duration'/>";
                        tbody += "<select id='ddlDay' class='form-control duration'>";
                        tbody += "<option>Days</option>";
                        tbody += "<option>Month</option>";
                        tbody += "<option>Year</option>";
                        tbody += "</select>";
                        tbody += "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDiagnosisItem tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertDiagnosisPresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    if ($('#tblDiagnosisItem tbody input:checkbox:checked').length == 0) {
        alert('Item Not Selected.');
        return;
    };
    $('#tblDiagnosisItem tbody tr').each(function () {
        if ($(this).closest('tr').find('td:eq(0)').find('input:checkbox').is(':checked')) {
            var duration = $(this).closest('tr').find('td:eq(3)').find('#txtDuration').val() + ' ' + $(this).closest('tr').find('td:eq(3)').find('select option:selected').text();
            objItems.push({
                'TemplateId': 'T00012',
                'Duration': duration,
                'EyesInfo': $(this).closest('tr').find('td:eq(0)').find('button').text(),
                'ItemId': $(this).closest('tr').find('td:eq(1)').text(),
                'ItemName': $(this).closest('tr').find('td:eq(2)').text(),
                'Remark': '-',
            });
        }
    })
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItemsForOPTH';

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
                //alert(data)
                GetPrescriptionInfo();
                $('#modalDiagnosis').modal('hide');
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
//History Items operation
function InsertHistoryItem() {
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = 'History';
    objBO.TemplateType = 'History';
    objBO.GroupName = 'History';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = 'T00016';
    objBO.ItemId = '';
    objBO.ItemName = $('#txtHistoryItemName').val();
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertTemplateInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                GetHistoryInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetHistoryInfo() {
    $('#modalHistory').modal('show');
    $('#tblHistoryItem tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.ItemName = 'History';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetItemInfoByGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><button id='btnEye' class='btn btn-success btn-xs'>NA</button><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td class='flex'>";
                        tbody += "<input type='number' id='txtDuration' value='0' class='form-control duration'/>";
                        tbody += "<select id='ddlDay' class='form-control duration'>";
                        tbody += "<option>Days</option>";
                        tbody += "<option>Month</option>";
                        tbody += "<option>Year</option>";
                        tbody += "</select>";
                        tbody += "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblHistoryItem tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertHistoryPresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    if ($('#tblHistoryItem tbody input:checkbox:checked').length == 0) {
        alert('Item Not Selected.');
        return;
    };
    $('#tblHistoryItem tbody tr').each(function () {
        if ($(this).closest('tr').find('td:eq(0)').find('input:checkbox').is(':checked')) {
            var duration = $(this).closest('tr').find('td:eq(3)').find('#txtDuration').val() + ' ' + $(this).closest('tr').find('td:eq(3)').find('select option:selected').text();
            objItems.push({
                'TemplateId': 'T00016',
                'Duration': duration,
                'EyesInfo': $(this).closest('tr').find('td:eq(0)').find('button').text(),
                'ItemId': $(this).closest('tr').find('td:eq(1)').text(),
                'ItemName': $(this).closest('tr').find('td:eq(2)').text(),
                'Remark': '-',
            });
        }
    })
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItemsForOPTH';

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
                //alert(data)
                GetPrescriptionInfo();
                $('#modalHistory').modal('hide');
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
//Visual Acuity operation
function GetVisualAcuityInfo() {
    $('#tblVisualAcuity tbody').find('input').val('');
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetVisualAcuityInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var count = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        $('#tblVisualAcuity tbody tr:eq(0)').find('td:eq(2)').find('input:text').val(val.left_unaided);
                        $('#tblVisualAcuity tbody tr:eq(0)').find('td:eq(1)').find('input:text').val(val.right_unaided);

                        $('#tblVisualAcuity tbody tr:eq(1)').find('td:eq(2)').find('input:text').val(val.left_withPinHole);
                        $('#tblVisualAcuity tbody tr:eq(1)').find('td:eq(1)').find('input:text').val(val.right_withPinHole);

                        $('#tblVisualAcuity tbody tr:eq(2)').find('td:eq(2)').find('input:text').val(val.left_withPrvGlass);
                        $('#tblVisualAcuity tbody tr:eq(2)').find('td:eq(1)').find('input:text').val(val.right_withPrvGlass);

                        $('#tblVisualAcuity tbody tr:eq(3)').find('td:eq(2)').find('input:text').val(val.left_PrvSpectPowerDist);
                        $('#tblVisualAcuity tbody tr:eq(3)').find('td:eq(1)').find('input:text').val(val.right_PrvSpectPowerDist);

                        $('#tblVisualAcuity tbody tr:eq(4)').find('td:eq(2)').find('input:text').val(val.left_PrvSpectPowerNear);
                        $('#tblVisualAcuity tbody tr:eq(4)').find('td:eq(1)').find('input:text').val(val.right_PrvSpectPowerNear);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertVisualAcuity() {
    var url = config.baseUrl + "/api/Prescription/CPOE_OPTHVisualAcuityInsertUpdate";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.DoctorId = _DoctorId;
    objBO.left_unaided = $('#tblVisualAcuity tbody tr:eq(0)').find('td:eq(2)').find('input:text').val();
    objBO.right_unaided = $('#tblVisualAcuity tbody tr:eq(0)').find('td:eq(1)').find('input:text').val();
    objBO.left_withPinHole = $('#tblVisualAcuity tbody tr:eq(1)').find('td:eq(2)').find('input:text').val();
    objBO.right_withPinHole = $('#tblVisualAcuity tbody tr:eq(1)').find('td:eq(1)').find('input:text').val();
    objBO.left_withPrvGlass = $('#tblVisualAcuity tbody tr:eq(2)').find('td:eq(2)').find('input:text').val();
    objBO.right_withPrvGlass = $('#tblVisualAcuity tbody tr:eq(2)').find('td:eq(1)').find('input:text').val();
    objBO.left_PrvSpectPowerDist = $('#tblVisualAcuity tbody tr:eq(3)').find('td:eq(2)').find('input:text').val();
    objBO.right_PrvSpectPowerDist = $('#tblVisualAcuity tbody tr:eq(3)').find('td:eq(1)').find('input:text').val();
    objBO.left_PrvSpectPowerNear = $('#tblVisualAcuity tbody tr:eq(4)').find('td:eq(2)').find('input:text').val();
    objBO.right_PrvSpectPowerNear = $('#tblVisualAcuity tbody tr:eq(4)').find('td:eq(1)').find('input:text').val();

    objBO.PosteriorRatinaSegment = ($('input[id=PosteriorRatina]').is(':checked') == true) ? 'Y' : 'N';
    objBO.EyeLens = ($('input[id=EyeLens]').is(':checked') == true) ? 'Y' : 'N';
    objBO.left_MCT = $('#tblOtherInfo tbody tr:eq(0)').find('td:eq(2)').find('input:text').val();
    objBO.right_MCT = $('#tblOtherInfo tbody tr:eq(0)').find('td:eq(1)').find('input:text').val();
    objBO.left_AT = $('#tblOtherInfo tbody tr:eq(1)').find('td:eq(2)').find('input:text').val();
    objBO.right_AT = $('#tblOtherInfo tbody tr:eq(1)').find('td:eq(1)').find('input:text').val();
    objBO.left_Gonioscopy = $('#tblOtherInfo tbody tr:eq(2)').find('td:eq(2)').find('select option:selected').text();
    objBO.right_Gonioscopy = $('#tblOtherInfo tbody tr:eq(2)').find('td:eq(1)').find('select option:selected').text();

    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertVisualAcuity';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                //alert(data)
                GetVisualAcuityInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateEyeOtherInfo() {
    var url = config.baseUrl + "/api/Prescription/CPOE_OPTHVisualAcuityInsertUpdate";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.PosteriorRatinaSegment = ($('input[id=PosteriorRatina]').is(':checked') == true) ? 'Y' : 'N';
    objBO.EyeLens = ($('input[id=EyeLens]').is(':checked') == true) ? 'Y' : 'N';
    objBO.left_MCT = $('#tblOtherInfo tbody tr:eq(0)').find('td:eq(2)').find('input:text').val();
    objBO.right_MCT = $('#tblOtherInfo tbody tr:eq(0)').find('td:eq(1)').find('input:text').val();
    objBO.left_AT = $('#tblOtherInfo tbody tr:eq(1)').find('td:eq(2)').find('input:text').val();
    objBO.right_AT = $('#tblOtherInfo tbody tr:eq(1)').find('td:eq(1)').find('input:text').val();
    objBO.left_Gonioscopy = $('#tblOtherInfo tbody tr:eq(2)').find('td:eq(2)').find('select option:selected').text();
    objBO.right_Gonioscopy = $('#tblOtherInfo tbody tr:eq(2)').find('td:eq(1)').find('select option:selected').text();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'UpdateEyeOtherInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                //alert(data)
                GetOtherInfo();
            }
            else {
                // alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
//Other Services operation
function GetOtherInfo() {
    $('input[id=PosteriorRatina]').prop('checked', false);
    $('input[id=EyeLens]').prop('checked', false);
    $('#tblSpecDetail tbody').find('input:checkbox').prop('checked', false);
    $('#tblOtherInfo tbody').find('input').val('');
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetOtherInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var count = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (val.PosteriorRatinaSegment == 'Y')
                            $('input[id=PosteriorRatina]').prop('checked', true);
                        else
                            $('input[id=PosteriorRatina]').prop('checked', false);

                        if (val.EyeLens == 'Y')
                            $('input[id=EyeLens]').prop('checked', true);
                        else
                            $('input[id=EyeLens]').prop('checked', false);

                        $('#tblOtherInfo tbody tr:eq(0)').find('td:eq(2)').find('input:text').val(val.left_MCT);
                        $('#tblOtherInfo tbody tr:eq(0)').find('td:eq(1)').find('input:text').val(val.right_MCT);

                        $('#tblOtherInfo tbody tr:eq(1)').find('td:eq(2)').find('input:text').val(val.left_AT);
                        $('#tblOtherInfo tbody tr:eq(1)').find('td:eq(1)').find('input:text').val(val.right_AT);

                        $('#tblOtherInfo tbody tr:eq(2)').find('td:eq(2)').find('input:text').val(val.left_Gonioscopy);
                        $('#tblOtherInfo tbody tr:eq(2)').find('td:eq(1)').find('input:text').val(val.right_Gonioscopy);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

//Advice Items operation
function InsertAdviceItem() {
    var url = config.baseUrl + "/api/master/CPOE_OPTHInsertUpdateMaster";
    var objBO = {};
    objBO.GroupType = 'Advice';
    objBO.TemplateType = 'Advice';
    objBO.GroupName = 'Advice';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = 'T00013';
    objBO.ItemId = '';
    objBO.ItemName = $('#txtAdviceItemName').val();
    objBO.IsFavourite = 0;
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertTemplateInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                GetAdviceInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetAdviceInfo() {
    $('#modalAdvice').modal('show');
    $('#tblAdviceItem tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = 'Advice';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetItemInfoByGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td><button id='btnEye' class='btn btn-success btn-xs'>BE</button><input type='checkbox'/></td>";
                        tbody += "<td>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td class='flex'>";
                        tbody += "<input type='number' id='txtDuration' value='0' class='form-control duration'/>";
                        tbody += "<select id='ddlDay' class='form-control duration'>";
                        tbody += "<option>Days</option>";
                        tbody += "<option>Month</option>";
                        tbody += "<option>Year</option>";
                        tbody += "</select>";
                        tbody += "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblAdviceItem tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertAdvicePresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    if ($('#tblAdviceItem tbody input:checkbox:checked').length == 0) {
        alert('Item Not Selected.');
        return;
    };
    $('#tblAdviceItem tbody tr').each(function () {
        if ($(this).closest('tr').find('td:eq(0)').find('input:checkbox').is(':checked')) {
            var duration = $(this).closest('tr').find('td:eq(3)').find('#txtDuration').val() + ' ' + $(this).closest('tr').find('td:eq(3)').find('select option:selected').text();
            objItems.push({
                'TemplateId': 'T00013',
                'Duration': duration,
                'EyesInfo': $(this).closest('tr').find('td:eq(0)').find('button').text(),
                'ItemId': $(this).closest('tr').find('td:eq(1)').text(),
                'ItemName': $(this).closest('tr').find('td:eq(2)').text(),
                'Remark': '-',
            });
        }
    })
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'NonMedicineItemsForOPTH';

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
                // alert(data)
                GetPrescriptionInfo();
                $('#modalAdvice').modal('hide');
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
//Medicine Items Operation
function AddMedicineTemplatge() {
    var itemid = $('#txtItemID').val();
    var itemName = $('#txtSearchProduct').val();
    var dose = $('#txtFreqMaster').val();
    var duration = $('#txtDuration').val();
    var days = $('#txtDays option:selected').text();
    var qty = $('#txtQty').val();
    var whenTake = $('#txtIntake').val();
    var route = $('#txtRoute').val();
    var remark = $('#txtRemark').val();
    var len = parseInt($('#MedicineTemplate tbody tr').length);
    var tbody = "";
    tbody += "<tr data-itemid=" + itemid + ">";
    tbody += "<td><button id='btnEye' class='btn btn-info btn-xs'>NA</button></td>";
    tbody += "<td>" + itemName + "</td>";
    tbody += "<td>" + dose + "</td>";
    tbody += "<td>" + duration + "</td>";
    tbody += "<td>" + duration + " Days</td>";
    tbody += "<td>" + whenTake + "</td>";
    tbody += "<td>" + route + "</td>";
    tbody += "<td>" + qty + "</td>";
    tbody += "<td>" + remark + "</td>";
    tbody += "<td><button class='btn btn-danger btn-xs' onclick=$(this).closest('tr').remove()><i class='fa fa-trash'></i></button></td>";
    tbody += "</tr>";
    if (ValidateProduct()) {
        var ids = $('#MedicineTemplate tbody').find('tr').filter(function () {
            if ($(this).data('itemid') == itemid) return true;
        });
        if (ids.length <= 0) {
            $('#MedicineTemplate tbody').append(tbody);
            $('.OPDPrintPreview #PrescribedMedicine').show();
            $('.modal-body').find('input:text').val('');
            $('.modal-body').find('select').prop('selectedIndex', '0').change();
            InsertMedicinePresItems();
        }
        else {
            alert('Product Already Selected...! Try Aother One.')
            $('.modal-body').find('input:text').val('');
            $('.modal-body').find('select').prop('selectedIndex', '0').change();
        }
    }
}
function FreqAndIntake() {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.Logic = 'FreqAndIntake';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            dataDoseAndInTake.push(data.ResultSet.Table);
            dataDoseAndInTake.push(data.ResultSet.Table1);
            $('#ddlFreMaster').empty().append($('<option></option>').val(00).html('Select'));
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlFreMaster').append($('<option></option>').val(val.qty).html(val.Descriptions)).select2();
            });
            $('#ddlIntake').empty().append($('<option></option>').val(00).html('Select'));
            $.each(data.ResultSet.Table1, function (key, val) {
                $('#ddlIntake').append($('<option></option>').val(val.instruction).html(val.instruction)).select2();
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDoctor(DeptId) {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.DeptId = DeptId;
    objBO.Logic = 'GetDoctor';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#ddlTransferReferTo').empty().append($('<option></option>').val(00).html('Select Refer To'));
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlTransferReferTo').append($('<option></option>').val(val.DoctorId).html(val.DoctorName)).select2();
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function QtyCalculate() {
    var dose = parseInt($('#ddlFreMaster option:selected').val());
    var duration = parseInt($('#txtDuration').val());
    var qty = (dose) * (duration);
    $('#txtQty').val(qty);

}
function ValidateSearch() {
    var searchType = $('#ddlSearchType option:selected').text();

    if (searchType == 'Choose Type') {
        $('#ddlSearchType').css({ 'border-color': 'red' });
        alert('Please Select Search Type..');
        $('#txtSearchProduct').val('');
        return false;
    }
    else {
        $('#ddlSearchType').removeAttr('style').siblings('span').empty();
    }
    return true;
}
function SearchMedicine(key, type) {
    var url = config.baseUrl + "/api/IPDNursing/SearchMedicine";
    var objBO = {};
    objBO.searchKey = key;
    objBO.searchType = type;
    objBO.PanelId = $('span[data-panelid]').text();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblnavigate tbody').empty();
            if (data != '') {

                $.each(data.ResultSet.Table, function (key, val) {
                    $('#tblnavigate').show();
                    $('<tr class="searchitems" data-AlertMsg="' + val.AlertMsg + '" data-itemid=' + val.item_id + ' data-iscash=' + val.IsCash + '><td>' + val.item_name + "</td></tr>").appendTo($('#tblnavigate tbody'));
                });
            }
            else {
                alert("Error");
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ValidateProduct() {
    var itemid = $('#txtItemID').val();
    var itemName = $('#txtSearchProduct').val();
    var dose = $('#ddlFreMaster option:selected').text();
    var duration = $('#txtDuration').val();
    var days = $('#txtDays option:selected').text();
    var qty = $('#txtQty').val();
    var whenTake = $('#ddlIntake option:selected').text();
    var route = $('#ddlRoute option:selected').text();
    var remark = $('#txtRemark').val();

    if (itemid == '') {
        alert('Please Choose Any Product..!');
        return false;
    }
    if (itemName == '') {
        $('#txtSearchProduct').css('border-color', 'red').focus();
        alert('Please Choose Any Product..!');
        return false;
    }
    else {
        $('#txtSearchProduct').removeAttr('style');
    }
    if (dose == 'Select') {
        alert('Please Choose Dose..!');
        return false;
    }
    if (duration == '') {
        $('#txtDuration').css('border-color', 'red').focus();
        alert('Please Provide Duration..!');
        return false;
    }
    else {
        $('#txtDuration').removeAttr('style');
    }
    if (qty == '') {
        $('#txtQty').css('border-color', 'red').focus();
        alert('Please Provide Quantity..!');
        return false;
    }
    else {
        $('#txtQty').removeAttr('style');
    }
    if (whenTake == 'Select') {
        alert('Please Choose when Take..!');
        return false;
    }
    if (route == 'Select Route') {
        alert('Please Choose Route..!');
        return false;
    }
    return true;
}
function GetOPTHMedicineInfo() {
    $('#MedicineTemplate tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = 'Diagnosis';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetMedicinePresForOPTH';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr data-itemid=" + val.Item_id + ">";
                        tbody += "<td><button id='btnEye' class='btn btn-success btn-xs'>" + val.EyesInfo + "</button></td>";
                        tbody += "<td>" + val.Item_name + "</td>";
                        tbody += "<td>" + val.med_dose + "</td>";
                        tbody += "<td>" + val.med_times + "</td>";
                        tbody += "<td>" + val.med_duration + " Days</td>";
                        tbody += "<td>" + val.med_intake + "</td>";
                        tbody += "<td>" + val.med_route + "</td>";
                        tbody += "<td>" + val.qty + "</td>";
                        tbody += "<td>" + val.remark + "</td>";
                        var msg = 'Are you sure?';
                        tbody += "<td><button class='btn btn-danger btn-xs' onclick=$(this).closest('tr').remove()><i class='fa fa-trash'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#MedicineTemplate tbody').append(tbody);
                }
            }
            $('#modalPresMediTemplate').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPrevSpecInfo() {
    $('#divPrevSpecInfo').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.UHID = $('#tblOPTHHeaderInfo tbody').find('#txtHeaderUHID').text();
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPrevSpecInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (temp != val.app_no) {
                            tbody += "<table class='table table-bordered tblPrevSpecInfo'>";
                            tbody += "<thead>";
                            tbody += "<tr>";
                            tbody += "<th style='background:#ddd;color:#000' colspan='9'><b>App Date : </b>" + val.AppDate + ", <b>App No : </b>" + val.app_no + "</th>";
                            tbody += "</tr>";
                            tbody += "<tr>";
                            tbody += "<th style='width: 1%;background:#2478a9;color:#fff'></th>";
                            tbody += "<th style='background:#2478a9;color:#fff' class='text-center' colspan='4'>Left Glass</th>";
                            tbody += "<th style='background:#2478a9;color:#fff' class='text-center' colspan='4'>Right Glass</th>";
                            tbody += "</tr>";
                            tbody += "<tr>";
                            tbody += "<th class='text-center'></th>";
                            tbody += "<th class='text-center'>Sph</th>";
                            tbody += "<th class='text-center'>Cyl</th>";
                            tbody += "<th class='text-center'>Axis</th>";
                            tbody += "<th class='text-center'>VA</th>";
                            tbody += "<th class='text-center'>Sph</th>";
                            tbody += "<th class='text-center'>Cyl</th>";
                            tbody += "<th class='text-center'>Axis</th>";
                            tbody += "<th class='text-center'>VA</th>";
                            tbody += "</tr>";
                            tbody += "<tbody>";
                            temp = val.app_no;
                        }
                        tbody += "<tr>";
                        tbody += "<td class='text-center'><b>" + val.SpecType + "</b></td>";
                        tbody += "<td class='text-center'>" + val.left_Sph + "</td>";
                        tbody += "<td class='text-center'>" + val.left_Cyl + "</td>";
                        tbody += "<td class='text-center'>" + val.left_Axis + "</td>";
                        tbody += "<td class='text-center'>" + val.left_VA + "</td>";
                        tbody += "<td class='text-center'>" + val.right_Sph + "</td>";
                        tbody += "<td class='text-center'>" + val.right_Cyl + "</td>";
                        tbody += "<td class='text-center'>" + val.right_Axis + "</td>";
                        tbody += "<td class='text-center'>" + val.right_VA + "</td>";
                        tbody += "</tr>";
                    });
                    $('#divPrevSpecInfo').append(tbody);
                }
            }
            $('#modalPrevSpecInfo').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPrevVisualAcuity() {
    $('#divPrevVisualAcuityInfo').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.UHID = $('#tblOPTHHeaderInfo tbody').find('#txtHeaderUHID').text();
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPrevVisualAcuity';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = "";
            var temp = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (temp != val.app_no) {
                            tbody += "<table class='table table-bordered tblPrevSpecInfo'>";
                            tbody += "<thead>";
                            tbody += "<tr>";
                            tbody += "<th style='background:#ddd;color:#000' colspan='3'><b>App Date : </b>" + val.AppDate + ", <b>App No : </b>" + val.app_no + "</th>";
                            tbody += "</tr>";
                            tbody += "<tr>";
                            tbody += "<th style='width: 40%;background:#2478a9;color:#fff'></th>";
                            tbody += "<th style='background:#2478a9;color:#fff' class='text-center'>Left</th>";
                            tbody += "<th style='background:#2478a9;color:#fff' class='text-center'>Right</th>";
                            tbody += "</tr>";
                            tbody += "<tbody>";
                            temp = val.app_no;
                        }
                        tbody += "<tr>";
                        tbody += "<td class='text-center'><b>Un-Aided</b></td>";
                        tbody += "<td class='text-center'>" + val.left_unaided + "</td>";
                        tbody += "<td class='text-center'>" + val.right_unaided + "</td>";
                        tbody += "</tr>";
                        tbody += "<tr>";
                        tbody += "<td class='text-center'><b>With Pin Hole</b></td>";
                        tbody += "<td class='text-center'>" + val.left_withPinHole + "</td>";
                        tbody += "<td class='text-center'>" + val.right_withPinHole + "</td>";
                        tbody += "</tr>";
                        tbody += "<tr>";
                        tbody += "<td class='text-center'><b>With Prv Glass</b></td>";
                        tbody += "<td class='text-center'>" + val.left_withPrvGlass + "</td>";
                        tbody += "<td class='text-center'>" + val.right_withPrvGlass + "</td>";
                        tbody += "</tr>";
                        tbody += "<tr>";
                        tbody += "<td class='text-center'><b>Prv Spect Power</b></td>";
                        tbody += "<td class='text-center'>" + val.left_PrvSpectPower + "</td>";
                        tbody += "<td class='text-center'>" + val.right_PrvSpectPower + "</td>";
                        tbody += "</tr>";
                    });
                    $('#divPrevVisualAcuityInfo').append(tbody);
                }
            }
            $('#modalPrevVisualAcuityInfo').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PresMedicineInfo() {
    $('#tblPresMedicineInfo tbody').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = 'Diagnosis';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetMedicinePresForOPTH';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr data-itemid=" + val.Item_id + ">";
                        tbody += "<td><remove class='delRow'>X</remove><button id='btnEye' class='btn btn-success btn-xs'>" + val.EyesInfo + "</button></td>";
                        tbody += "<td>" + val.Item_name + "</td>";
                        tbody += "<td>" + val.med_dose + "</td>";
                        tbody += "<td>" + val.med_times + "</td>";
                        tbody += "<td>" + val.med_duration + "</td>";
                        tbody += "<td>" + val.med_route + "</td>";
                        tbody += "<td>" + val.remark + "</td>";
                        tbody += "</tr>";


                    });
                    $('#tblPresMedicineInfo tbody').append(tbody);
                }
            }
            $('#MedicineTemplate tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                var tbody1 = "";
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody1 += "<tr data-itemid=" + val.Item_id + ">";
                        tbody1 += "<td><button id='btnEye' class='btn btn-success btn-xs'>" + val.EyesInfo + "</button></td>";
                        tbody1 += "<td>" + val.Item_name + "</td>";
                        tbody1 += "<td>" + val.med_dose + "</td>";
                        tbody1 += "<td>" + val.med_times + "</td>";
                        tbody1 += "<td>" + val.med_duration + " Days</td>";
                        tbody1 += "<td>" + val.med_intake + "</td>";
                        tbody1 += "<td>" + val.med_route + "</td>";
                        tbody1 += "<td>" + val.qty + "</td>";
                        tbody1 += "<td>" + val.remark + "</td>";
                        var msg = 'Are you sure?';
                        tbody1 += "<td><button class='btn btn-danger btn-xs' onclick=$(this).closest('tr').remove()><i class='fa fa-trash'></i></button></td>";
                        tbody1 += "</tr>";
                    });
                    $('#MedicineTemplate tbody').append(tbody1);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetSpecInfo() {
    $('#tblSpecDetail tbody tr:eq(0)').find('input').val('');
    $('#tblSpecDetail tbody tr:eq(1)').find('input').val('');

    $('#txtNearLeftEyeSph').val('')
    $('#txtNearLeftEyeVA').val('')
    $('#txtNearRightEyeSph').val('')
    $('#txtNearRightEyeVA').val('')


    $('#txtLeftSph').val('')
    $('#txtLeftCyl').val('')
    $('#txtLeftAxis').val('')
    $('#txtLeftVA').val('')

    $('#txtRightSph').val('')
    $('#txtRightCyl').val('')
    $('#txtRightAxis').val('')
    $('#txtRightVA').val('')
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetSpecInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var count = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (val.SpecType == 'Distance') {
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(5)').find('input').val(val.left_Sph);
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(6)').find('input').val(val.left_Cyl);
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(7)').find('input').val(val.left_Axis);
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(8)').find('input').val(val.left_VA);

                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(1)').find('input').val(val.right_Sph);
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(2)').find('input').val(val.right_Cyl);
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(3)').find('input').val(val.right_Axis);
                            $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(4)').find('input').val(val.right_VA);

                            $('input[value=Const]').prop('checked', (val.Const == 'Y') ? true : false);
                            $('input[value=Bifocal]').prop('checked', (val.Bifocal == 'Y') ? true : false);
                            $('input[value=Monofocal]').prop('checked', (val.Monofocal == 'Y') ? true : false);
                            $('input[value=PhotoSun]').prop('checked', (val.PhotoSun == 'Y') ? true : false);
                            $('input[value=ARC]').prop('checked', (val.ARC == 'Y') ? true : false);
                            $('input[value=Progressive]').prop('checked', (val.Progressive == 'Y') ? true : false);
                            $('input[value=Near]').prop('checked', (val.Near == 'Y') ? true : false);

                            $('#txtLeftSph').val(val.left_Sph);
                            $('#txtLeftCyl').val(val.left_Cyl);
                            $('#txtLeftAxis').val(val.left_Axis);
                            $('#txtLeftVA').val(val.left_VA);

                            $('#txtRightSph').val(val.right_Sph);
                            $('#txtRightCyl').val(val.right_Cyl);
                            $('#txtRightAxis').val(val.right_Axis);
                            $('#txtRightVA').val(val.right_VA);
                        }
                        if (val.SpecType == 'Near-Add') {
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(1)').find('input').val(val.right_Sph);
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(2)').find('input').val(val.right_Cyl);
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(3)').find('input').val(val.right_Axis);
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(4)').find('input').val(val.right_VA);

                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(5)').find('input').val(val.left_Sph);
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(6)').find('input').val(val.left_Cyl);
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(7)').find('input').val(val.left_Axis);
                            $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(8)').find('input').val(val.left_VA);

                            $('#txtNearLeftEyeSph').val(val.left_Sph)
                            $('#txtNearLeftEyeVA').val(val.left_VA)
                            $('#txtNearRightEyeSph').val(val.right_Sph)
                            $('#txtNearRightEyeVA').val(val.right_VA)                         
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
function expandMedicine() {
    $('#expandMedicine').toggleClass('expandMedicine');
    $('.divContainer').toggleClass('expandMedicine-divContainer');
}
function InsertMedicinePresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItems";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    $('#tblPresMedicineInfo tbody tr').each(function () {
        objMedicine.push({
            'app_no': sessionStorage.getItem('AppId'),
            'DoctorId': _DoctorId,
            'Item_id': $(this).data('itemid'),
            'EyesInfo': $(this).find('td:eq(0)').find('button').text().trim(),
            'Item_name': $(this).find('td:eq(1)').text().trim(),
            'med_dose': $(this).find('td:eq(2)').text().trim(),
            'med_times': $(this).find('td:eq(3)').text().trim(),
            'med_duration': $(this).find('td:eq(4)').text(),
            'med_intake': '-',
            'med_route': $(this).find('td:eq(5)').text(),
            'qty': '0',
            'remark': $(this).find('td:eq(6)').text(),
        });
    });
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
    ipPrescription.login_id = Active.userId;
    ipPrescription.Logic = 'MedicineItemsForOPTH';

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
                //alert(data)
                PresMedicineInfo();
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function DistanceSpecInfo() {
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(1)').find('input').val($('#txtRightSph').val());
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(2)').find('input').val($('#txtRightCyl').val());
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(3)').find('input').val($('#txtRightAxis').val());
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(4)').find('input').val($('#txtRightVA').val());

    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(5)').find('input').val($('#txtLeftSph').val());
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(6)').find('input').val($('#txtLeftCyl').val());
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(7)').find('input').val($('#txtLeftAxis').val());
    $('#tblSpecDetail tbody tr:eq(0)').find('td:eq(8)').find('input').val($('#txtLeftVA').val());
    $('#modalDistance').modal('hide');

    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(1)').find('input').val($('#txtLeftSph').val());
    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(2)').find('input').val($('#txtLeftCyl').val());
    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(3)').find('input').val($('#txtLeftAxis').val());
    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(4)').find('input').val($('#txtLeftVA').val());

    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(5)').find('input').val($('#txtRightSph').val());
    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(6)').find('input').val($('#txtRightCyl').val());
    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(7)').find('input').val($('#txtRightAxis').val());
    //$('#tblSpecDetail tbody tr:eq(0)').find('td:eq(8)').find('input').val($('#txtRightVA').val());


    InsertDistanceSpecInfo()
}
function NearSpecInfo() {
    $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(5)').find('input').val($('#txtNearLeftEyeSph').val());
    //$('#tblSpecDetail tbody tr:eq(1)').find('td:eq(2)').find('input').val($('#txtLeftCyl').val());
    //$('#tblSpecDetail tbody tr:eq(1)').find('td:eq(3)').find('input').val($('#txtLeftAxis').val());
    $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(8)').find('input').val($('#txtNearLeftEyeVA').val());

    $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(1)').find('input').val($('#txtNearRightEyeSph').val());
    //$('#tblSpecDetail tbody tr:eq(1)').find('td:eq(6)').find('input').val($('#txtRightCyl').val());
    //$('#tblSpecDetail tbody tr:eq(1)').find('td:eq(7)').find('input').val($('#txtRightAxis').val());
    $('#tblSpecDetail tbody tr:eq(1)').find('td:eq(4)').find('input').val($('#txtNearRightEyeVA').val());
    $('#modalNear').modal('hide');
    InsertDistanceSpecInfo()
}
function checkEmpty(index) {
    var count = 0;
    $('#tblSpecDetail tbody tr:eq(' + index + ')').find('td').each(function () {
        if ($.trim($(this).find('input').val()) != "" && $(this).index() != 0) count += 1; else count += 0;
    })
    return count;
}
function InsertDistanceSpecInfo() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertSpecInfo";
    var objBO = {};
    var SpecInfo = [];
    $('#tblSpecDetail tbody tr').each(function () {
        if (checkEmpty($(this).index()) < 1) return
        SpecInfo.push({
            'AutoId': 0,
            'DoctorId': _DoctorId,
            'app_no': sessionStorage.getItem('AppId'),
            'SpecType': $(this).find('td:eq(0)').find('button').text(),
            'left_Sph': $(this).find('td:eq(5)').find('input:text').val(),
            'left_Cyl': $(this).find('td:eq(6)').find('input:text').val(),
            'left_Axis': $(this).find('td:eq(7)').find('input:text').val(),
            'left_VA': $(this).find('td:eq(8)').find('input:text').val(),
            'right_Sph': $(this).find('td:eq(1)').find('input:text').val(),
            'right_Cyl': $(this).find('td:eq(2)').find('input:text').val(),
            'right_Axis': $(this).find('td:eq(3)').find('input:text').val(),
            'right_VA': $(this).find('td:eq(4)').find('input:text').val(),
            'Const': ($('input[value=Const]').is(':checked') == true) ? 'Y' : 'N',
            'Bifocal': ($('input[value=Bifocal]').is(':checked') == true) ? 'Y' : 'N',
            'Monofocal': ($('input[value=Monofocal]').is(':checked') == true) ? 'Y' : 'N',
            'PhotoSun': ($('input[value=PhotoSun]').is(':checked') == true) ? 'Y' : 'N',
            'ARC': ($('input[value=ARC]').is(':checked') == true) ? 'Y' : 'N',
            'Progressive': ($('input[value=Progressive]').is(':checked') == true) ? 'Y' : 'N',
            'Near': ($('input[value=Near]').is(':checked') == true) ? 'Y' : 'N',
            'login_id': Active.userId,
            'Logic': 'InsertSpecInfo'
        });
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(SpecInfo),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Successfully')) {
                //alert(data)
                GetSpecInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetLabTestInfo() {
    //$('#LaboratoryRadiologyList').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHMasterQueries";
    var objBO = {};
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetLabTestInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#LaboratoryRadiologyList').empty();
                    var li = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        li += "<li>";
                        li += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                        li += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                        li += "</li>";
                    });
                    $('#LaboratoryRadiologyList').append(li);
                }
            }
            $('#modalTest').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPresTestInfo() {
    $('#PrescLaboratoryRadiologyItems').empty();
    var url = config.baseUrl + "/api/master/CPOE_OPTHQueries";
    var objBO = {};
    objBO.app_no = sessionStorage.getItem('AppId');
    objBO.TemplateType = '';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = '';
    objBO.ItemId = '';
    objBO.ItemName = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPresTestInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.TemplateId == 'T00009') {
                            var item = "<li id='" + val.ItemId + "'>" + val.ItemName + "<span class='delete' onclick=DeleteItem(" + val.auto_id + ")>X</span></li>";
                            $('#PrescLaboratoryRadiologyItems').append(item);
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
function MarkAsFavourite(TemplateId, ItemId, ItemName, IsFav, temp, ul) {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
    var objBO = {};
    objBO.TemplateType = 'Doctor';
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = TemplateId;
    objBO.ItemId = ItemId;
    objBO.prm_1 = ItemName;
    objBO.IsFav = IsFav;
    objBO.login_id = Active.userId;
    objBO.Logic = 'ProvisionalDiagnosisMarkAsFav';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                if (temp.includes('HospitalList')) {
                    GetLabTestInfo();
                }
                else {
                    GetLabTestInfo(temp, TemplateId, ul);
                }
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
function FilteredTemplate(FilterType, TemplateId, ul) {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.TemplateId = TemplateId;
    objBO.FilterType = FilterType;
    objBO.DoctorId = _DoctorId;
    objBO.Logic = 'FilteredTemplateOfDoctor';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#' + ul + '').empty();
            var li = "";
            $.each(data.ResultSet.Table, function (key, val) {
                li += "<li>";
                li += "<a><label><input type='checkbox' data-itemid=" + val.ItemId + "/>&nbsp;" + val.ItemName + "</label></a>";
                if (FilterType == 'HospitalList') {
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                }
                else if (FilterType == 'FavouriteList') {
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                }
                else {
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " data-itemname='" + val.ItemName + "' class='fa fa-trash'></a>";
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " data-itemname='" + val.ItemName + "' class='fa fa-pencil' style='color:#1483a5'></a>";
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                }
                li += "</li>";
            });
            $('#' + ul + '').append(li);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeleteTemplateInfo(TemplateId, ItemId) {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
        var objBO = {};
        objBO.TemplateType = 'Doctor';
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = TemplateId;
        objBO.ItemId = ItemId;
        objBO.ItemName = '';
        objBO.IsFavourite = 0;
        objBO.login_id = Active.userId;
        objBO.Logic = 'DeleteTemplateInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 'success') {
                    $('button.rotate').trigger('click');
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function DeleteMedicineItems(AutoId) {
    if (confirm('Are you sure to delete?')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertDeletePrescribedItems";
        var objBO = {};
        objBO.AutoId = AutoId;
        objBO.DoctorId = '';
        objBO.TemplateId = '';
        objBO.app_no = '';
        objBO.ItemId = '';
        objBO.prm_1 = '';
        objBO.prm_2 = '';
        objBO.login_id = Active.userId;
        objBO.Logic = 'DeleteMedicineItemsForOPTH';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Successfully')) {
                    PresMedicineInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function InsertTestPresItems() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertPrescribedItemsOPTH";
    var ipPrescription = {};
    var objItems = [];
    var objMedicine = [];
    var objBO = {};
    $('#LaboratoryRadiologyItems span').each(function () {
        objItems.push({
            'TemplateId': $(this).parents('.prescribedItem').find('templategroup').attr('id'),
            'ItemId': $(this).attr('id'),
            'ItemName': $(this).text().trim(),
            'Remark': '-',
        });
    })
    ipPrescription.DoctorId = _DoctorId;
    ipPrescription.app_no = sessionStorage.getItem('AppId');
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
                GetPresTestInfo();
                $('#modalTest').modal('hide');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function searchList(txt, ul) {
    $('#' + txt).on('keyup', function () {
        var val = $(this).val().toLocaleLowerCase();
        $('#' + ul + ' li').filter(function () {
            $(this).toggle($(this).find('a label').text().toLocaleLowerCase().indexOf(val) > -1);
        });
    });
}
function GetTemplateMaster() {
    var url = config.baseUrl + "/api/master/CPOE_MasterQueries";
    var objBO = {};
    objBO.Logic = 'GetTemplateMaster';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#ddlDoctorTemplate').empty().append($('<option></option>').val(00).html('Select Template'));
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlDoctorTemplate').append($('<option></option>').val(val.TemplateId).html(val.TemplateName));
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertTemplateInfo() {
    if (Validate()) {
        var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
        var objBO = {};
        objBO.TemplateType = 'Doctor';
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = $('#ddlDoctorTemplate option:selected').val();
        objBO.ItemId = '';
        objBO.ItemName = $('#txtDoctorItemName').val();
        var fav = $('input[name=IsFavourite]').is(':checked');
        objBO.IsFavourite = (fav == true) ? 1 : 0;
        objBO.login_id = Active.userId;
        objBO.Logic = 'InsertTemplateInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 'success') {
                    Clear();
                    alert('Created Successfully..!');
                    $('.panel-body').find('button.rotate').trigger('click');
                    $('#modalTemplate').modal('hide');
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function UpdateTemplateInfo() {
    if (Validate()) {
        var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
        var objBO = {};
        objBO.TemplateType = 'Doctor';
        objBO.DoctorId = _DoctorId;
        objBO.TemplateId = $('#ddlDoctorTemplate option:selected').val();
        objBO.ItemId = $('#hiddenDoctorTempItemId').text();
        objBO.ItemName = $('#txtDoctorItemName').val();
        var fav = $('input[name=IsFavourite]').is(':checked');
        objBO.IsFavourite = (fav == true) ? 1 : 0;
        objBO.login_id = Active.userId;
        objBO.Logic = 'UpdateTemplateInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 'success') {
                    Clear();
                    alert('Updated Successfully..!');
                    $('.panel-body').find('button.rotate').trigger('click');
                    $('#modalTemplate').modal('hide');
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
//Validation
function Validate() {
    var name = $('#txtDoctorItemName').val();
    var Template = $('#ddlDoctorTemplate option:selected').text();

    if (Template == 'Select Template') {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctorTemplate-container]').css('border-color', 'red').focus();
        alert('Please Select Template..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlDoctorTemplate-container]').removeAttr('style').focus();
    }
    if (name == '') {
        $('#txtItemName').css('border-color', 'red').focus();
        alert('Please Provide Item Name..');
        return false;
    }
    else {
        $('#txtItemName').removeAttr('style').focus();
    }
    return true;
}
function Clear() {
    $('input[type=text]').val('');
    //$('select').prop('selectedIndex', '0').change();
    $('#btnSaveDoctorTemplate').switchClass('btn-warning', 'btn-success');
    $('#btnSaveDoctorTemplate i').switchClass('fa-edit', 'fa-plus');
}
function Test() {
    var col = [];
    for (var i = 0; i < data.ResultSet.Table.length; i++) {
        for (var key in data.ResultSet.Table[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }
    var t = "";
    t += "<table style='width:100%' class='table-bordered tblBorder'>";
    t += "<tr>";
    for (var i = 0; i < col.length; i++) {
        t += "<th style='text-align:center;width:30%;white-space:nowrap'>" + col[i] + "</th>";
    }
    t += "</tr>";
    $.each(data.ResultSet.Table, function (key, val) {
        t += "<tr>";
        for (var i = 0; i < col.length; i++) {
            if (val[col[i]].includes('OT'))
                t += "<td style='text-align:center;vertical-align:middle'>" + val[col[i]] + "</td>";
            else {
                var str = val[col[i]];
                console.log(str);
                if (str.length > 5) {
                    var slots = val[col[i]].split('|');
                    var s = "";
                    for (var j = 0; j < slots.length; j++) {
                        if (slots[j].length > 5) {
                            var str2 = slots[j];
                            var tr = str2.split('$');
                            s += "<span id='" + tr[0] + "' onclick='OTScheduleInfo(this.id);'  class='badge  mr-1' style='font-size:10pt;background-color:#4caed1;'>" + tr[1] + "</span> ";
                        }

                    }
                    t += "<td style='text-align:center;vertical-align:middle'>" + s + "</td>";
                }
                else
                    t += "<td style='text-align:center;vertical-align:middle'></td>";

            }
        }
        t += "</tr>";
    });
    t += "</table>";
    $('#divEntryForm').empty();
    $('#divEntryForm').html(t);
}

//Medicine template Section
function UseMediTempInfo() {
    var tbody = "";
    $('#MedicineTemplateForDB tbody tr').each(function () {
        tbody += "<tr data-itemid='newId'>";
        tbody += "<td style='padding:2px;'><remove class='delRow'>X</remove><button id='btnEye' class='btn btn-success btn-xs'>BE</button></td>";
        tbody += "<td>" + $(this).find('td:eq(0)').text().split('~')[0] + "</td>";
        tbody += "<td>" + $(this).find('td:eq(1)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(2)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(3)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(4)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(5)').text() + "</td>";
        tbody += "</tr>";
    });
    $('#tblPresMedicineInfo tbody').append(tbody);
    $('#modalPresMediTemplate').modal('hide');
}
function GetMedicineTemplate() {
    $('#MedicineTemplateForDB tbody').empty();
    $('#tblTemplateMaster tbody').empty();
    $('#hiddenTemplateMasterId').text('');
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.DoctorId = _DoctorId;
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
            $('#modalPresMediTemplate').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function InsertMedicineTemplate() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
    var objBO = {};
    objBO.DoctorId = _DoctorId;
    objBO.TemplateId = $('#hiddenTemplateMasterId').text();
    objBO.TemplateName = $('#txtTemplateName').val();
    objBO.Description = $('#txtDescription').val();
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSavePresTemplate').text() == 'Save') ? 'InsertCPOEMedicineTemplate' : 'UpdateCPOEMedicineTemplate';
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

function GetCPOEMedicineTemplateInfo(templateId) {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.TemplateId = templateId;
    objBO.DoctorId = _DoctorId;
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
function DeleteMediTemplate(tempid, elem) {
    if (confirm('Are you sure to delete this Template? All Related Medicine Items will be deleted from this Template Group.')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
        var objBO = {};
        objBO.DoctorId = _DoctorId;
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
        objBO.DoctorId = _DoctorId;
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
        objBO.DoctorId = _DoctorId;
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
    $('#btnSavePresTemplate').switchClass('btn-warning', 'btn-success').text('Save');

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