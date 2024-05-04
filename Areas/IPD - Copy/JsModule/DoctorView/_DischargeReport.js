var _AutoId;
var _TemplateId = null;
$(document).ready(function () {
    //$('#dash-dynamic-section .title').removeAttr('style').hide();
    $('#ddlTemplates').empty().append($('<option></option>').val('Select').html('Select')).select2();
    HeaderList('Y');
    TemplateList('Y');
    DischargeSummary();
    $('#dash-dynamic-section').find('label.title').text('Patient Discharge').hide();
    $('select').select2();
    CKEDITOR.replace('txtTemplate', {
        enterMode: CKEDITOR.ENTER_BR,
        shiftEnterMode: CKEDITOR.ENTER_P,
    });
    //CKEDITOR.replace("txtTemplate");
    //CKEDITOR.editorConfig = function (config) {
    //    config.removePlugins = 'blockquote,save,flash,iframe,tabletools,pagebreak,templates,about,showblocks,newpage,language,print,div';
    //    config.removeButtons = 'Print,Form,TextField,Textarea,Button,CreateDiv,PasteText,PasteFromWord,Select,HiddenField,Radio,Checkbox,ImageButton,Anchor,BidiLtr,BidiRtl,Font,Format,Styles,Preview,Indent,Outdent';
    //};
    $('#ddlTemplates').on('change', function () {
        DischargeSummaryByTemplateId()
    });
    $('#tblHeaderMaster tbody').on('click', 'button', function () {
        selectRow($(this));
        var AutoId = $(this).closest('tr').find('td:eq(1)').text();
        var HeaderName = $(this).closest('tr').find('td:eq(2)').text();
        _AutoId = AutoId;
        $('#txtHeaderName').val(HeaderName);
        $('#btnSaveHeaderMaster i').removeClass('fa-plus-circle').addClass('fa-edit');
    });
    $('#tblTemplateMaster tbody').on('click', 'button', function () {
        selectRow($(this));
        var AutoId = $(this).closest('tr').find('td:eq(1)').text();
        var HeaderId = $(this).closest('tr').find('td:eq(2)').text();
        var TemplateName = $(this).closest('tr').find('td:eq(3)').text();
        _AutoId = AutoId;
        $('#txtTemplateName').val(TemplateName);
        $('#ddlHeaderMaster').val(HeaderId).change();
        $('#btnSaveTemplateMaster i').removeClass('fa-plus-circle').addClass('fa-edit');
    });
    $('#tblDischargeSummary tbody').on('click', 'button.edit', function () {
        selectRow($(this));
        var HeaderId = $(this).closest('tr').find('td:eq(1)').text();
        _AutoId = $(this).closest('tr').find('td:eq(2)').text();
        $('#ddlHeader').val(HeaderId).change();
        var TemplateContent = $(this).closest('tr').find('td:eq(3)').html();
        CKEDITOR.instances['txtTemplate'].setData(TemplateContent);
    });
});
function ExpandCK() {
    $('.expandCK .cke_contents').removeAttr('style')
    $('.ckDiv').toggleClass('expandCK');
    if ($('.ckDiv').hasClass('expandCK'))
        $('.expandCK .cke_contents').css('height', '56vh');
    else
        $('.expandCK .cke_contents').css('height', '150px');
}
function HeaderList(prm) {
    $('#tblHeaderMaster tbody').empty();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = prm;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'HeaderList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $('#ddlHeader').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $('#ddlHeaderMaster').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IsActive == 'Y') {
                            $('#ddlHeader').append($('<option></option>').val(val.HeaderId).html(val.HeaderName));
                            $('#ddlHeaderMaster').append($('<option></option>').val(val.HeaderId).html(val.HeaderName));
                        }
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td class='hide'>" + val.AutoId + "</td>";
                        tbody += "<td>" + val.HeaderName + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "<td><button class='btn btn-warning btn-xs'><i class='fa fa-edit'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblHeaderMaster tbody').append(tbody);
                    var DischargeReportHeader = data.ResultSet.Table1[0].DischargeReportHeader;
                    var IsDischSummaryLocked = data.ResultSet.Table1[0].IsDischSummaryLocked;
                    if (IsDischSummaryLocked == 'Y') {
                        $('#btnLock').addClass('lock');
                        $('#btnUnLock').removeClass('lock');
                    }
                    if (IsDischSummaryLocked == 'N') {
                        $('#btnLock').removeClass('lock');
                        $('#btnUnLock').addClass('lock');
                    }

                    if (DischargeReportHeader == '-')
                        $('#ddlReportHeader').prop('selectedIndex', 0).change();
                    else {
                        $('#ddlReportHeader option').each(function () {
                            if ($(this).text() == DischargeReportHeader)
                                $('#ddlReportHeader').prop('selectedIndex', '' + $(this).index() + '').change();
                        });
                    }


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function TemplateList(prm) {
    $('#tblTemplateMaster tbody').empty();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = prm;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'TemplateList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.HeaderName) {
                            tbody += "<tr class='bg-warning'>";
                            tbody += "<td colspan='4'><b>" + val.HeaderName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.HeaderName;
                        }
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td class='hide'>" + val.AutoId + "</td>";
                        tbody += "<td class='hide'>" + val.HeaderId + "</td>";
                        tbody += "<td>" + val.TemplateName + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "<td><button class='btn btn-warning btn-xs'><i class='fa fa-edit'></i></button></td>";
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
function GetPreviousDischargeInfo() {
    $('#tblPatientDischargeInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = $('#tblAdviceHeader tbody tr:last td:eq(5)').text();
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetPreviousDischargeInfo';
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
                        tbody += "<td>" + val.IPDSource + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td><button onclick=DischargeTemplateInfo(this) class='btn btn-warning btn-xs edit'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientDischargeInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetTemplateListByHeader() {
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $('#ddlHeader option:selected').val();
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'TemplateListByHeader';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlTemplates').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlTemplates').append($('<option></option>').val(val.TemplateId).html(val.TemplateName));
                    });
                }
            }
        },
        complete: function () {
            if (_TemplateId != null)
                $('#ddlTemplates').val(_TemplateId).trigger('change.select2');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DischargeSummaryByTemplateId() {
    CKEDITOR.instances['txtTemplate'].setData('');
    var ms = new Date().getUTCMilliseconds;
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $('#ddlTemplates option:selected').val();
    objBO.Prm2 = ms;
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetTemplateContent';
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
                        CKEDITOR.instances['txtTemplate'].setData(val.template_content);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertUpdateHeader() {
    var url = config.baseUrl + "/api/IPDDoctor/InsertDischargeTemplate";
    var objBO = {};
    objBO.AutoId = _AutoId;
    objBO.UHID = '-';
    objBO.IPDNo = _IPDNo;
    objBO.HeaderId = '-';
    objBO.HeaderName = $('#txtHeaderName').val();
    objBO.TemplateId = '-';
    objBO.TemplateName = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveHeaderMaster i').is('.fa-plus-circle') == true) ? "InsertHeader" : 'UpdateHeader';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                HeaderList('Y');
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
function InsertUpdateTemplate() {
    var url = config.baseUrl + "/api/IPDDoctor/InsertDischargeTemplate";
    var objBO = {};
    objBO.AutoId = _AutoId;
    objBO.UHID = '-';
    objBO.IPDNo = _IPDNo;
    objBO.HeaderId = $('#ddlHeaderMaster option:selected').val();
    objBO.HeaderName = '-',
        objBO.TemplateId = '-';
    objBO.TemplateName = $('#txtTemplateName').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveTemplateMaster i').is('.fa-plus-circle') == true) ? "InsertTemplate" : 'UpdateTemplate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                TemplateList('Y');
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
function LockUnLockDischargeSummary(logic) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDDoctor/IPD_InsertDischargeReportInfo";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.TemplateId = _AutoId;
        objBO.TemplateName = '-';
        objBO.TemplateContent = '-';
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = logic;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    if (logic == 'LockDischargeSummary') {
                        $('#btnLock').addClass('lock');
                        $('#btnUnLock').removeClass('lock');
                    }
                    if (logic == 'UnLockDischargeSummary') {
                        $('#btnLock').removeClass('lock');
                        $('#btnUnLock').addClass('lock');
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
}
function expandContent() {
    $('.dischargeSection').slideToggle('slow');
    //  $(this).parents('.section').slideToggle('slow');
    $('.TemplateInfo').toggleClass('fullHeight');
    $('.vertiscrl').toggleClass('fullHeight');
}
function UpdateReportHeader() {
    if (confirm('Are you sure to update?')) {
        if ($('#ddlReportHeader option:selected').text() == 'Select Report Header') {
            alert('Please Select Report Header Name');
            return
        }
        var url = config.baseUrl + "/api/IPDDoctor/IPD_InsertDischargeReportInfo";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.TemplateId = _AutoId;
        objBO.TemplateName = '-';
        objBO.TemplateContent = '-';
        objBO.Prm1 = $('#ddlReportHeader option:selected').text();
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'UpdateReportHeader';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
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
}
function InsertDischargeSummary(logic) {
    if ($('#ddlReportHeader option:selected').text() == 'Select Report Header') {
        alert('Please Update Report Header Name');
        return
    }
    var url = config.baseUrl + "/api/IPDDoctor/IPD_InsertDischargeReportInfo";
    var objBO = {};
    objBO.IPDNo = _IPDNo;
    objBO.TemplateId = _AutoId;
    objBO.TemplateName = '-';
    objBO.TemplateContent = CKEDITOR.instances['txtTemplate'].getData();
    objBO.Prm1 = $('#ddlHeader option:selected').val();
    objBO.Prm2 = $('#ddlHeader option:selected').text();
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                for (instance in CKEDITOR.instances) {
                    CKEDITOR.instances[instance].updateElement();
                    CKEDITOR.instances[instance].setData('');
                }
                DischargeSummary();
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
function DeleteDischarge(AutoId) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDDoctor/IPD_InsertDischargeReportInfo";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.TemplateId = AutoId;
        objBO.TemplateName = '-';
        objBO.TemplateContent = '-';
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'DeleteDischargeSummary';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    DischargeSummary();
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
function DischargeSummary() {
    $('#tblDischargeSummary tbody').empty();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'DischargeSummary';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (temp != val.HeaderName) {
                            tbody += "<tr class='bg-warning'>";
                            tbody += "<td colspan='4'><b>" + val.HeaderName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.HeaderName;
                        }
                        tbody += "<tr>";
                        tbody += "<td><button onclick=DeleteDischarge(" + val.AutoId + ") class='btn btn-danger btn-xs edit'><i class='fa fa-trash'></i></button></td>";
                        tbody += "<td class='hide'>" + val.HeaderId + "</td>";
                        tbody += "<td class='hide'>" + val.AutoId + "</td>";
                        tbody += "<td>" + val.template_content + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "<td><button class='btn btn-warning btn-xs edit'><i class='fa fa-edit'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDischargeSummary tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DischargeTemplateInfo(elem) {
    $('#tblDischargeTemplateInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = $(elem).closest('tr').find('td:eq(1)').text();
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $(elem).closest('tr').find('td:eq(0)').text();
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = ($(elem).closest('tr').find('td:eq(0)').text() == 'NewHIS') ? 'DischargeSummary' : 'DischargeSummaryOldHIS';
    debugger
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = '';
                    var count = 0;
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (temp != val.HeaderName) {
                            tbody += "<tr class='bg-warning'>";
                            tbody += "<td colspan='4'><b>" + val.HeaderName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.HeaderName;
                        }
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.HeaderId + "</td>";
                        tbody += "<td class='hide'>" + val.AutoId + "</td>";
                        tbody += "<td>" + val.template_content + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "<td><button onclick=copyPrevContent(this) class='btn btn-warning btn-xs edit'><i class='fa fa-copy' ></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDischargeTemplateInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function copyPrevContent(elem) {
    if (confirm('Do you want to paste selected content in Ediotr?')) {
        var data = $(elem).closest('tr').find('td:eq(2)').html();
        $('.nav-tabs li:first a[href=#DischargeReportSummary]').trigger('click');
        CKEDITOR.instances['txtTemplate'].setData(data);
    }
}
function PrintDischargeReport() {
    var url = config.rootUrl + "/ipd/print/IPDDischargeReport?_IPDNo=" + _IPDNo;
    window.open(url, '_blank')
}
function Clear() {
    $('#txtHeaderName').val('');
    $('#txtTemplateName').val('');
    _AutoId = 0;
    $('#btnSaveHeaderMaster i').removeClass('fa-edit').addClass('fa-plus-circle');
    $('#btnSaveTemplateMaster i').removeClass('fa-edit').addClass('fa-plus-circle');
}