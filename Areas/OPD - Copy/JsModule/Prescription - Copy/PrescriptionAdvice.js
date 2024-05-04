
$(document).ready(function () {
    $('.OPDPrintPreview .prescribedItem').on('focus', 'input:text', function (e) {
        $(this).select();
    });
    $('.circle-add').on('click', function () {
        var templateId = $(this).attr('id');
        $('#ddlDoctorTemplate option').each(function () {
            if ($(this).val() == templateId) {
                $('#ddlDoctorTemplate').prop('selectedIndex', '' + $(this).index() + '');
            }
        });
        Clear();
        $('#modalTemplate').modal('show');
    });
    $('.panel-body').on('click', 'a.fa-trash', function () {
        var templateId = $(this).data('templateid');
        var itemid = $(this).data('itemid');
        if (confirm('Are you sure want to delete this Item from Template..!')) {
            DeleteTemplateInfo(templateId, itemid);
        }
    });
    $('.panel-body').on('click', 'a.fa-pencil', function () {
        var templateId = $(this).data('templateid');
        var itemName = $(this).data('itemname');
        var itemDesc = ($(this).closest('li').find('label:first').html() == null) ? itemName : $(this).closest('li').find('label:first').html();
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
        $('#txtDoctorTempDescription').val(itemDesc.replace(/<br>/g, '\n'));
        $('#btnSaveDoctorTemplate').switchClass('btn-success', 'btn-warning');
        $('#btnSaveDoctorTemplate').text('Update');
        $('#modalTemplate').modal('show');
    });
    $('.panel-title a').on('click', function () {
        currentRemark = null;
        $('.panel-title a').find('span.circle').removeClass('circle-open');
        $(this).find('span.circle').addClass('circle-open');
        $('.panel-title a').find('span.circle i').removeClass('circle-open fa-minus-circle');
        $(this).find('span.circle i').addClass('circle-open fa-minus-circle');
    });
    $('.left-panel button').on('click', function () {
        $('.left-panel button').removeClass('active-item');
        $(this).addClass('active-item');
    });
    $('.search-box').on('click', 'button', function () {
        $('button').removeClass('rotate');
        $(this).addClass('rotate');
    });
    GetVitalSign();
    GetDepartment();
    FillHospitalTemplateItems();
    searchList('txtSearchProvisionalDiagnosis', 'ProvisionalDiagnosisList');
    searchList('txtSearchChiefComplaint', 'ChiefComplaintList');
    searchList('txtSearchSignSymptoms', 'SignSymptomsList');
    searchList('txtSearchLaboratoryRadiology', 'LaboratoryRadiologyList');
    searchList('txtSearchVaccinationStatus', 'VaccinationStatusList');
    searchList('txtSearchDoctorNotes', 'DoctorNotesList');
    $('#ddlTransferDept').on('change', function () {
        var DeptId = $(this).val();
        GetDoctor(DeptId);
    });
    $('#PatientVisits #tblPatientVisits tbody').on('click', '.copyVisit', function () {
        var appno = $(this).closest('tr').find('td:eq(1)').text();
        CopyVisitsInfo(appno)
    });
    $('#tblOldHISData tbody').on('click', '.copyVisit', function () {
        var appno = $(this).closest('tr').find('td:eq(1)').text();
        CopyVisitsInfoOldHIS(appno)
    });
    $('.panel-body').on('click', 'a.fa-heart', function () {
        var templateId = $(this).data('templateid');
        var itemid = $(this).data('itemid');
        var fav = $(this).data('fav');
        var temp = $(this).data('temp');
        var ul = $(this).parents('ul').attr('id');
        var itemName = $(this).closest('li').find('label').text();
        var isfav = (fav == false) ? 1 : 0;
        MarkAsFavourite(templateId, itemid, itemName, isfav, temp, ul);
    });

    //Prescribed Medicine Add To Preview
    $('.panel-body').on('change', 'input:checkbox', function () {
        var isCheck = $(this).is(':checked');
        var template = $(this).parents('ul').attr('id');
        var TemplateId = $(this).data('templateid');
        if (isCheck)
            PresMediInfoByTempId(TemplateId);
        else {
            $('.MedicineTemplate tbody tr').each(function () {
                if ($(this).find('td:last').text() == TemplateId)
                    $(this).remove();
            });
        }
        if ($('#PrescribedMedicine .MedicineTemplate tbody tr').length == 0)
            $('.OPDPrintPreview #PrescribedMedicine').hide();
    });

    //select Checkbox Item from Template To Prev
    $('.panel-body').on('change', 'input:checkbox', function () {
        var listArr = ['LaboratoryRadiologyList', 'PrescribedProcedureList'];
        var template = $(this).parents('ul').attr('id');//get parent UL id on template item check
        var PrevTemplateId = template.replace('List', 'Items');//get id of related Template from Prev Side
        var itemId = $(this).data('itemid');//get item id on check
        var listName = $(this).closest('li').parents('ul').attr('id');
        if ($.inArray(template, listArr) > -1) {
            var itemName1 = $(this).closest('label').text();
            var list = "";
            list += " <span id='" + itemId + "'>," + itemName1 + "</span><close class='remove'>X</close>";//create item list
            $('#' + PrevTemplateId).show();//show related template on prev side [default all template hide in prev side]
            if (this.checked)//on check
            {
                if ($('#' + PrevTemplateId).is('p'))
                    $(list).insertBefore($('#' + PrevTemplateId + ' p'));//add item to prev side template group
                else
                    $('#' + PrevTemplateId).append(list);
            }
            else//on un-check
            {
                $('#' + PrevTemplateId + ' span').each(function ()//on uncheck remove particular items from prev template group
                {
                    if ($(this).text() == itemName1) {
                        $(this).remove();
                    }
                });
            }
            return
        }

        // var itemName = ($.inArray($(this).closest('li').parents('ul').attr('id'), listArr) == -1) ? $(this).closest('li').find('label input:checkbox').data('desc') : $(this).closest('li').find('label').html();//get item name on check       
        var itemName = ($.inArray($(this).closest('li').parents('ul').attr('id'), listArr) == -1) ? $(this).closest('li').find('label:first').html() : $(this).closest('li').find('label').html();//get item name on check       
        $('#' + PrevTemplateId).show();//show related template on prev side [default all template hide in prev side]
        if (this.checked)//on check
        {
            var data = $('textarea[data-id=' + PrevTemplateId + ']').val();//add item to prev side template group
            $('textarea[data-id=' + PrevTemplateId + ']').val(data + ', ' + itemName.replace(/<br>/g, '\n'));//add item to prev side template group
        }
        FillPreview(PrevTemplateId);
    });
    //$('.OPDPrintPreview .prescribedItem').on('click', 'span', function (e) {      
    //    if ($(this).hasClass('fromtxt') || $(this).attr('id') == 'fromtxt') {           
    //        var unq = $(this).parents('div.prescribedItem').attr('id');
    //        var val = $(this).html().replace(/<br>/g, '\n');
    //        $('textarea[data-id=' + unq + ']').val(val);
    //        $('.panel-title a[href=#' + unq.replace('Items', '') + ']').trigger('click');
    //        return
    //    }

    //    var itemid = $(this).attr('id');
    //    var itemName = $(this).text();
    //    $(this).replaceWith('<input type="text" value="' + itemName + '"/>');
    //    $('input:text').focus();
    //});
    //Add Items from textarea to prev     
    $('.panel-body').on('keyup', 'textarea', function (e) {
        if ($(this).data('id') != '') {
            var PrevTemplateId = $(this).data('id');//get id of related Template 	
            var itemName = $(this).val();//get item name on check	
        }
        else {
            var template = $(this).parents('.panel-body').find('ul').attr('id');//get parent UL id on template item check
            var PrevTemplateId = template.replace('List', 'Items');//get id of related Template from Prev Side		
            var itemName = $(this).val();//get item name on check	
        }
        //$('#' + PrevTemplateId).append(itemName.replace(/\n/g, '<br/>'));                    
        FillPreview(PrevTemplateId);
        //$(this).val('');
        //if (e.keyCode === 13)//on press enter
        //{
        //    e.preventDefault();            
        //    list += "<span id='fromtxt'>" + itemName + "</span><close class='remove'>X</close>";
        //  $('#' + PrevTemplateId).append(list);
        //  $('#' + PrevTemplateId).show();
        //    $(this).val('');
        //}
    });
    //Edit Item Inline
    $('.OPDPrintPreview .prescribedItem').on('mouseover', 'span', function () {
        $('.remove').hide();
        $(this).next('.remove').show();
    });
    $('.OPDPrintPreview').on('click', function () {
        $('.remove').hide();
    });
    $('.OPDPrintPreview .prescribedItem').on('click', 'close', function () {
        var itemid = $(this).prev('span').attr('id');//get item id of dbl clicked item
        var template = $(this).prev('span').parents('.prescribedItem').attr('id');//get templateGroup id of dbl clicked item
        var templateId = template.replace('Items', 'List');//get id of related Template from Template Side		
        $('.panel-body #' + templateId + ' li').each(function ()//uncheck related items from template side afetr remove item from prev side
        {
            if ($(this).find('input:checkbox').data('itemid') == itemid) {
                $(this).find('input:checkbox').prop('checked', false);
            }
        });
        $(this).prev('span').remove();
        $(this).remove();
    });
    $('.OPDPrintPreview .prescribedItem').on('blur', 'input:text', function () {
        var itemName = $(this).val();
        $(this).replaceWith("<span id='fromtext'>" + itemName + "</span>");
        //$(this).remove()
    });
    $('.OPDPrintPreview .prescribedItem').on('keydown', 'input:text', function (e) {
        if (e.keyCode == 13) {
            var itemName = $(this).val();
            $(this).replaceWith("<span id='fromtext'>" + itemName + "</span>");
            //$(this).remove()
        }
    });
    //remove all slected items from prev side and uncheck from template side
    $('.OPDPrintPreview .prescribedItem').on('dblclick', 'TemplateGroup', function () {
        var template = $(this).parents('.prescribedItem').attr('id');//get template id
        var templateId = template.replace('Items', 'List');//get UL id of template list
        $(this).siblings('span').remove();//remove all this template group items
        $('textarea[data-id=' + template + ']').val('');
        $(this).closest('.prescribedItem').find('p,span,close').remove();//hide this template group of prev side
        $('.panel-body #' + templateId).find('input:checkbox').prop('checked', false);//uncheck all related items from template side
    });
    $('#PatientVisits #tblPatientVisits tbody').on('click', '.currentVisit', function () {
        $('#PresPreview').attr('src', config.baseUrl + '/images/pres-loading.gif');
        var appno = $(this).closest('tr').find('td:eq(1)').text();
        AdvicePreview(appno);
        $('.nav-tabs li').removeClass('active')
        $('.tab-content div[id="PatientVisits1"]').removeClass('active in')
        $('a[href="#menu11"]').closest('li').addClass('active');
        $('.tab-content div[id="menu11"]').addClass('active in')
    });
    $('#tblOldHISData tbody').on('click', '.currentVisitOldHIS', function () {
        $('#PresPreview').attr('src', config.baseUrl + '/images/pres-loading.gif');
        var appno = $(this).closest('tr').find('td:eq(1)').text();
        AdvicePreviewOldHIS(appno);
        $('.nav-tabs li').removeClass('active')
        $('.tab-content div[id="PatientVisits1"]').removeClass('active in')
        $('a[href="#menu11"]').closest('li').addClass('active');
        $('.tab-content div[id="menu11"]').addClass('active in')
    });
    $('#tblIPDDischargeSummary tbody').on('click', '.IPDDisList', function () {
        var ipdNo = $(this).closest('tr').find('td:eq(0)').text();
        DischargeReportContent(ipdNo)
    });
    navigateControl();
});
function FillPreview(PrevTemplateId) {
    var data = $('textarea[data-id=' + PrevTemplateId + ']').val();

    if ($('#' + PrevTemplateId).find('p').length == 0)
        $('#' + PrevTemplateId).append('</p>');

    $('#' + PrevTemplateId).find('p').html(data.replace(/\n/g, '<br/>'));
    $('#' + PrevTemplateId).show();
}
function DischargeReportContent(ipdNo) {
    $('#tblDischargeReportContent tbody').empty();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.UHID = '-';
    objBO.AppointmentId = '-';
    objBO.prm_1 = ipdNo;
    objBO.DoctorId = '-';
    objBO.Logic = 'DischargeReportContent';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var TemplateId = "";
                    var tbody = "";
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.HeaderName) {
                            tbody += "<tr style='background:#f7f0cf'>"
                            tbody += "<td colspan='2'>" + val.HeaderName + "</td>";
                            tbody += "</tr>"
                            temp = val.HeaderName;
                        }
                        tbody += "<tr>"
                        tbody += "<td>" + val.template_content + "</td>";
                        tbody += "<td><i class='fa fa-copy contentCopy' onclick=copyContent(this)></i></td>";
                        tbody += "</tr>"
                    });
                    $('#tblDischargeReportContent tbody').append(tbody);
                    $('#modalDischargeReportContent').modal('show');
                }
                else {
                    alert('No Record Found..');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        },
    });
}
function copyContent(elem) {
    var content = $(elem).closest('tr').find('td:eq(0)').text();
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val(content).select();
    document.execCommand("copy");
    $temp.remove();
    $('#tblDischargeReportContent tbody').find('.fa-check-circle-o').remove();
    $(elem).append('<i class="fa fa-check-circle-o text-success"></i>');
}
function GetVitalSign() {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.app_no = Active.AppId;
    objBO.Logic = 'GetVitalSign';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('.OPDPrintPreview TemplateGroup[id=VitalSign] span').empty();
            var tbody = "";
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlTransferDept').append($('<option></option>').val(val.DeptId).html(val.DepartmentName)).select2();
                tbody += "<span>Weight :" + val.WT + " kg</span>";
                tbody += "<span>Temperature : " + val.Temprarture + " °C</span>";
                tbody += "<span>Pulse : " + val.Pulse + " p-m</span>";
                tbody += "<span>B/P : " + val.BP_Sys + '/' + val.BP_Dys + " mm/Hg</span>";
                tbody += "<span>SPO2 : " + val.SPO2 + "</span>";
                tbody += "<span>Height : " + val.HT + " CM</span>";
            });
            $('.OPDPrintPreview TemplateGroup[id=VitalSign]').append(tbody);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDepartment() {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.Logic = 'GetDept';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#ddlTransferDept').empty().append($('<option></option>').val(00).html('Select Template'));
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlTransferDept').append($('<option></option>').val(val.DeptId).html(val.DepartmentName)).select2();
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
function FillHospitalTemplateItems() {
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
            $('#ProvisionalDiagnosisList').empty();
            var li = "";
            $.each(data.ResultSet.Table, function (key, val) {
                li += "<li>";
                li += "<label class='hide'>'" + val.ItemDescription + "'</label>";
                li += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li += "</li>";
            });
            $('#ProvisionalDiagnosisList').append(li);

            $('#ChiefComplaintList').empty();
            var li1 = "";
            $.each(data.ResultSet.Table1, function (key, val) {
                li1 += "<li>";
                li1 += "<label class='hide'>'" + val.ItemDescription + "'</label>";
                li1 += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li1 += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li1 += "</li>";
            });
            $('#ChiefComplaintList').append(li1);

            $('#SignSymptomsList').empty();
            var li2 = "";
            $.each(data.ResultSet.Table2, function (key, val) {
                li2 += "<li>";
                li2 += "<label class='hide'>'" + val.ItemDescription + "'</label>";
                li2 += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li2 += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li2 += "</li>";
            });
            $('#SignSymptomsList').append(li2);

            $('#VaccinationStatusList').empty();
            var li3 = "";
            $.each(data.ResultSet.Table3, function (key, val) {
                li3 += "<li>";
                li3 += "<label class='hide'>'" + val.ItemDescription + "'</label>";
                li3 += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li3 += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li3 += "</li>";
            });
            $('#VaccinationStatusList').append(li3);

            $('#DoctorNotesList').empty();
            var li4 = "";
            $.each(data.ResultSet.Table4, function (key, val) {
                li4 += "<li>";
                li4 += "<label class='hide'>'" + val.ItemDescription + "'</label>";
                li4 += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li4 += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li4 += "</li>";
            });
            $('#DoctorNotesList').append(li4);

            $('#LaboratoryRadiologyList').empty();
            var li5 = "";
            $.each(data.ResultSet.Table8, function (key, val) {
                li5 += "<li>";
                li5 += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li5 += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li5 += "</li>";
            });
            $('#LaboratoryRadiologyList').append(li5);

            $('#PrescribedMedicineList').empty();
            var li6 = "";
            $.each(data.ResultSet.Table9, function (key, val) {
                li6 += "<li>";
                li6 += "<a><label><input type='checkbox' data-templateid='" + val.med_TemplateId + "'/>&nbsp;" + val.med_TemplateName + "</label></a>";
                li6 += "</li>";
            });
            $('#PrescribedMedicineList').append(li6);

            $('#PrescribedProcedureList').empty();
            var li7 = "";
            $.each(data.ResultSet.Table10, function (key, val) {
                li7 += "<li>";
                li7 += "<label class='hide'>'" + val.ItemDescription + "'</label>";
                li7 += "<a><label><input type='checkbox' data-itemid='" + val.ItemId + "'/>&nbsp;" + val.ItemName + "</label></a>";
                li7 += "<a data-temp='HospitalList' data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                li7 += "</li>";
            });
            $('#PrescribedProcedureList').append(li7);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PresMediInfoByTempId(templateId) {
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.TemplateId = templateId;
    objBO.DoctorId = Active.doctorId;
    objBO.Logic = 'PresMediInfoByTempId';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    // $('.MedicineTemplate tbody').empty();
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr data-itemid=" + val.Item_id + ">";
                        tbody += "<td><remove class='delRow'>X</remove>" + val.Item_name + "</td>";
                        tbody += "<td>" + val.med_dose + "</td>";
                        tbody += "<td>" + val.med_times + "</td>";
                        tbody += "<td>" + val.med_duration + " Days</td>";
                        tbody += "<td>" + val.med_intake + "</td>";
                        tbody += "<td>" + val.med_route + "</td>";
                        tbody += "<td>" + val.remark + "</td>";
                        tbody += "<td class='hide'>" + templateId + "</td>";
                        tbody += "</tr>";
                    });
                    $('.MedicineTemplate tbody').append(tbody)
                    $('.OPDPrintPreview #PrescribedMedicine').show();
                }
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
    objBO.DoctorId = Active.doctorId;
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
                li += "<label class='hide'>" + val.ItemDescription + "</label>";
                li += "<a><label><input type='checkbox' data-itemid=" + val.ItemId + "/>&nbsp;" + val.ItemName + "</label></a>";
                if (FilterType == 'HospitalList') {
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                }
                else if (FilterType == 'FavouriteList') {
                    li += "<a data-temp=" + FilterType + " data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='fa fa-heart' style='color:" + val.fav + "'></a>";
                }
                else {
                    li += "<a data-temp=" + FilterType + "  data-fav=" + val.IsFavourite + " data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " data-itemname='" + val.ItemName + "' class='fa fa-trash'></a>";
                    if (ul != 'LaboratoryRadiologyList' && ul != 'PrescribedProcedureList')
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
function InsertPrescAdvice() {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
    var objBO = {};
    objBO.UHID = Active.AppId;
    objBO.app_no = Active.AppId;
    objBO.DoctorId = 'doctorId-Test';
    objBO.DeptId = $('#ddlTransferDept option:selected').val();
    objBO.DoctorId_Trf = $('#ddlTransferReferTo option:selected').val();
    objBO.caseType = $('input[name=TransferRefertype]:checked').val();
    objBO.consultType = $('input[name=TransferConsultationType]:checked').val();
    objBO.doctor_diagnosis = $('#txtTransferDoctorDiagnosis').val();
    objBO.doctor_remark = $('#txtTransferDoctorRemark').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertPrescAdvice';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('success')) {
                alert(data);

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
function CopyVisitsInfo(OldAppNo) {
    if (confirm('Are you sure to copy Prescription in current Appointment?\nNote : It will delete all Prescription in current Appointment.')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
        var objBO = {};
        objBO.UHID = OldAppNo;
        objBO.app_no = Active.AppId;
        objBO.DoctorId = '-';
        objBO.DeptId = '-';
        objBO.DoctorId_Trf = '-';
        objBO.caseType = '-';
        objBO.consultType = '-';
        objBO.doctor_diagnosis = '-';
        objBO.doctor_remark = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'CopyVisitsInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    window.location.reload();
                }
                else {
                    alert(data);
                    window.location.reload();
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function CopyVisitsInfoOldHIS(OldAppNo) {
    if (confirm('Are you sure to copy Prescription in current Appointment?\nNote : It will delete all Prescription in current Appointment.')) {
        var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
        var objBO = {};
        objBO.UHID = OldAppNo;
        objBO.app_no = Active.AppId;
        objBO.DoctorId = '-';
        objBO.DeptId = '-';
        objBO.DoctorId_Trf = '-';
        objBO.caseType = '-';
        objBO.consultType = '-';
        objBO.doctor_diagnosis = '-';
        objBO.doctor_remark = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'CopyVisitsInfoOldHIS';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    window.location.reload();
                }
                else {
                    alert(data);
                    window.location.reload();
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function MarkAsFavourite(TemplateId, ItemId, ItemName, IsFav, temp, ul) {
    var url = config.baseUrl + "/api/Prescription/CPOE_InsertUpdateAdviceProcess";
    var objBO = {};
    objBO.TemplateType = 'Doctor';
    objBO.DoctorId = Active.doctorId;
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
                    FillHospitalTemplateItems();
                }
                else {
                    FilteredTemplate(temp, TemplateId, ul);
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
function searchList(txt, ul) {
    $('#' + txt).on('keyup', function () {
        var val = $(this).val().toLocaleLowerCase();
        $('#' + ul + ' li').filter(function () {
            $(this).toggle($(this).find('a label').text().toLocaleLowerCase().indexOf(val) > -1);
        });
    });
}
function DeleteTemplateInfo(TemplateId, ItemId) {
    var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
    var objBO = {};
    objBO.TemplateType = 'Doctor';
    objBO.DoctorId = Active.doctorId;
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
function AdvicePreviewOldHIS(appno) {
    $('#PresPreview').attr('src', config.rootUrl + '/loading.html');
    setTimeout(function () {
        var url = config.rootUrl + "/opd/print/AdvicePreviewOldHIS?app_no=" + appno;
        $('#PresPreview').attr('src', url);
    }, 200);
}
function AdvicePreview(appno) {
    InsertItemsForPres();
    $('#PresPreview').attr('src', config.rootUrl + '/loading.html');
    setTimeout(function () {
        var url = config.rootUrl + "/opd/print/AdvicePreview?app_no=" + appno;
        $('#PresPreview').attr('src', url);
    }, 200);
}
function PresPreview() {
    InsertItemsForPres();
    $('#PresPreview').attr('src', config.rootUrl + '/loading.html');
    setTimeout(function () {
        var url = config.rootUrl + "/opd/print/AdvicePreview?app_no=" + Active.AppId;
        $('#PresPreview').attr('src', url);
    }, 200);
}
function navigateControl() {
    $('#TemplateMasterRight').on('keyup', 'input,select', function (e) {
        console.log(e.keyCode)
        if (e.keyCode == 13) {
            $(this).next('.form-control').focus();
        }
    });;
}
