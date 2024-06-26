﻿var _VisitNo;
var _SubCat;
var _ItemId;
var _selectedTextGroup;
var _currentSelectedReport;
var _testCode;
var _autoTestId;
var _observationId;
var _rowIndex;
$(document).ready(function () {

    CKEDITOR.replace("txtTestComment");
    CKEDITOR.editorConfig = function (config) {
        config.removePlugins = 'blockquote,save,flash,iframe,tabletools,pagebreak,templates,about,showblocks,newpage,language,print,div';
        config.removeButtons = 'Print,Form,TextField,Textarea,Button,CreateDiv,PasteText,PasteFromWord,Select,HiddenField,Radio,Checkbox,ImageButton,Anchor,BidiLtr,BidiRtl,Font,Format,Styles,Preview,Indent,Outdent';
    };
    $("#uploadFile").change(function () {
        readURL(this);
    });
    RowSequence(['#tblObservationDetails']);
    CloseSidebar();
    $('select').select2();
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
    $("#tblTestInfo tbody").on("change", 'select', function () {
        var value = $(this).find('option:selected').text();
        $(this).siblings('input:text').val(value);
    });
    $("#tblReport tbody").on("click", 'button', function () {
        _rowIndex = $(this).closest('tr').index();
        selectRow($(this));
        _currentSelectedReport = $(this);
        ReportDetail();
    });
    $("#tblTemplateInfo tbody").on("click", 'button', function () {
        var value = $(this).closest('tr').find('td:eq(0)').html();
        var id = $(_selectedTextGroup).closest('tr').next('tr.Text').find('td:eq(2)').find('textarea').attr('id');
        CKEDITOR.instances[id].setData(value);
        $('#modalTemplateInfo').modal('hide');
    });
    $("table thead").on("change", 'input:checkbox', function () {
        $(this).parents('table').find('tbody input:checkbox').prop('checked', $(this).is(':checked'));
    });
    $("table").on("click", '#testMore', function () {
        var value = $(this).data('test');
        $('#modalTestPreview').find('label').text(value);
        $('#modalTestPreview').modal('show');
    });
    $("table").on("click", 'i.test-comment', function () {
        //CKEDITOR.instances['txtTestComment'].setData();
        _autoTestId = $(this).closest('tr').find('td:eq(0)').text();
        _testCode = $(this).closest('tr').find('td:eq(1)').text();
        _observationId = $(this).closest('tr').find('td:eq(2)').text();
        var comment = $(this).closest('tr').find('td:last').html();
        CKEDITOR.instances['txtTestComment'].setData(comment);
        $('#modalTestComment').modal('show');
    });
    $("#tblTestInfo tbody").on("keyup", 'input.value', function () {
        var value = parseFloat($(this).val());
        var min = parseFloat($(this).data('min'));
        var max = parseFloat($(this).data('max'));
        if (value < 0) {
            $(this).closest('tr').find('td:eq(9)').text('-'); $(this).closest('tr').find('td:eq(9)').removeAttr('class');
        }
        else if (value < min) {
            $(this).closest('tr').find('td:eq(9)').text('L'); $(this).closest('tr').find('td:eq(9)').addClass('lowFlag');
        }
        else if (value > max) {
            $(this).closest('tr').find('td:eq(9)').text('H'); $(this).closest('tr').find('td:eq(9)').addClass('highFlag');
        }
        else if (value >= min && value <= max) {
            $(this).closest('tr').find('td:eq(9)').text('N'); $(this).closest('tr').find('td:eq(9)').removeAttr('class');
        }
    });

    GetAllDepartment();
});
function uploadFile(autoTestId) {
    _autoTestId = autoTestId;
    $('#modalUploadDocument').modal('show');
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var ext = $('#uploadFile').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'pdf']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgFile').removeAttr('src', '');
            $('#imgFile').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $('#uploadFile').get(0).files;
    }
}
function LabReporting(logic) {
    $('#tblReport tbody').empty();
    $('#tblTestInfo tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyQueries";   
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.ReportStatus = $('#ddlStatus option:selected').text();
    objBO.VisitNo = $('#txtInput').val();
    objBO.BarccodeNo = $('#txtInput').val();
    objBO.SubCat = $('#ddldepartment option:selected').val();
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {         
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    var srno = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        srno++;
                        if (temp != val.patient_name) {
                            tbody += "<tr style='background:#cfcfcf'>";
                            tbody += "<td colspan='5'>" + val.patient_name + "</td>";
                            tbody += "</tr>";
                            temp = val.patient_name;
                        }
                        if (val.ReportStatus == 'Approved')
                            tbody += "<tr style='background:#bbffc9'>";
                        else if (val.ReportStatus == 'Tested')
                            tbody += "<tr style='background:#fbc7a9'>";
                        else if (val.ReportStatus == 'Partialy-Approved')
                            tbody += "<tr style='background:#fbeda9'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td class='hide'>" + val.SubCatId + "</td>";                    
                        tbody += "<td>" + val.RegDate + "</td>";
                        tbody += "<td>" + val.VisitNo + "</td>";
                        tbody += "<td style='display:none'>" + val.barcodeNo + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td style=width:1%><button class='btn btn-success btn-xs'><span class='fa fa-arrow-right'></button></td>";
                        tbody += "<td class='hide'>" + val.ItemId + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetTestNameByDept() {
    $('#ddlTest').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyQueries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = '-';
    objBO.BarccodeNo = '-';
    objBO.SubCat = $('#ddldepartment option:selected').val();
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'GetTestNameByDept';
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
                        $('#ddlTest').append($('<option></option>').val(val.testcode).html(val.TestName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ApprovedTestInfo() {
    $('#tblApproveTestInfo tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyQueries";
    //var url = config.baseUrl + "/api/Lab/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = _VisitNo;
    objBO.BarccodeNo = '-';
    objBO.SubCat = _SubCat;
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'ApprovedTestInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    var srno = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        srno++;
                        tbody += "<tr>";
                        tbody += "<td style='width:1%;padding: 0'><input type='checkbox' checked/></td>";
                        tbody += "<td class='hide'>" + val.AutoTestId + "</td>";
                        tbody += "<td>" + val.TestName + "</td>";
                        tbody += "<td>" + val.ApproveBy + "</td>";
                        tbody += "<td>" + val.ApprovedDate + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblApproveTestInfo tbody').append(tbody);
                    $('#modalUnApproveInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReportDetail() {
    _SubCat = $(_currentSelectedReport).closest('tr').find('td:eq(0)').text();
    _ItemId = $(_currentSelectedReport).closest('tr').find('td:last').text();
    _VisitNo = $(_currentSelectedReport).closest('tr').find('td:eq(2)').text();
    $('#tblTestInfo tbody').empty();
    //var url = config.baseUrl + "/api/Lab/LabReporting_Queries";
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyQueries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.ReportStatus = $('#ddlStatus option:selected').text();
    objBO.SubCat = _SubCat;
    objBO.VisitNo = _VisitNo;
    objBO.BarccodeNo = $('#txtInput').val();
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = _ItemId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'ReportDetail';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var _testcode = '';
            console.log("TEST", data)
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('span[data-petientname]').text(val.patient_name);
                        $('span[data-gender]').text(val.gender);
                        $('span[data-age]').text(val.ageInfo);
                        $('span[data-ipop]').text(val.ipop_no);
                        $('span[data-visitno]').text(val.VisitNo);
                        $('span[data-regdate]').text(val.RegDate);
                        var more = "<button id='testMore' class='btn btn-warning btn-xs pull-right' data-test='" + val.TestList + "'>More</button>";
                        if (val.TestList.length > 110)
                            $('span[data-testname]').html("<b>Test Name : </b>" + val.TestList + more);
                        else
                            $('span[data-testname]').html("<b>Test Name : </b>" + val.TestList);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody = '';
                    var temp = '';
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (val.r_type == 'Text') {
                            tbody += "<tr>";
                            if (val.IsTested == 1)
                                tbody += "<tr style='background:#fbc7a9'>";
                            if (val.IsApproved == 1)
                                tbody += "<tr style='background:#bbffc9'>";
                            
                               


                            tbody += "<td colspan='8'><i onclick=ShowHideEditor(this) class='fa fa-snowflake-o'>&nbsp;</i>" + val.TestName;
                            tbody += "<button data-testcode=" + val.testcode + " onclick=ApproveTest(this) class='btn btn-success btn-xs pull-right'><i class='fa fa-check-circle'>&nbsp;</i>Approve</button>";
                            tbody += "<button data-testcode=" + val.testcode + " onclick=TemplateByTestCode(this) class='btn btn-warning btn-xs pull-right'><i class='fa fa-file'>&nbsp;</i>Template</button>";
                            tbody += "<button data-testcode=" + val.testcode + " onclick=uploadFile(" + val.AutoTestId + ") class='btn btn-primary btn-xs pull-right'><i class='fa fa-upload'>&nbsp;</i>Add</button>";
                            tbody += "</td>";
                            tbody += "</tr>";
                            tbody += "<tr class=" + val.r_type + ">";
                            tbody += "<td class='hide'>" + val.AutoTestId + "</td>";
                            tbody += "<td class='hide'>" + val.testcode + "</td>";
                            tbody += "<td colspan='8'><textarea id='txtTestContent" + val.AutoTestId + "' class='form-control'></textarea></td>";
                            tbody += "<td class='hide'>" + val.report_content + "</td>";
                            tbody += "</tr>";
                        }                 
                    });
                    $('#tblTestInfo tbody').append(tbody);
                }
                $("#ddlApproveByDoctor").empty().append($("<option></option>").val("Select").html("Select")).select2();
                if (Object.keys(data.ResultSet).length > 0) {
                    if (Object.keys(data.ResultSet.Table2).length > 0) {
                        $.each(data.ResultSet.Table2, function (key, value) {
                            $("#ddlApproveByDoctor").append($("<option></option>").val(value.DoctorId).html(value.DoctorName));
                        });
                    }
                }
                CKEditor();
            }
        },
        complete: function (response) {
            //$('#tblTestInfo tbody').find('tr.Text').each(function () {
            //    // $(this).find('td:eq(2)').find('.ck-content').html("<b>est Reporrkjr</b>");
            //    const editor = ClassicEditor.create(document.querySelector('#txtTestContent' + $(this).find('td:eq(0)').text()));               
            //    editor.data.set("<p>Testing</p>");              

            //});
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function TemplateByTestCode(elem) {
    _selectedTextGroup = $(elem);
    $('#tblTemplateInfo tbody').empty();
    //var url = config.baseUrl + "/api/Lab/LabReporting_Queries";
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyQueries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = '-';
    objBO.BarccodeNo = '-';
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = $(elem).data('testcode');
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'TemplateByTestCode';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    var srno = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        srno++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.template_content + "</td>";
                        tbody += "<td>" + srno + "</td>";
                        tbody += "<td>" + val.template_name + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "<td style=width:1%><button class='btn btn-warning btn-xs'><span class='fa fa-sign-in'>&nbsp;</span>Select</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblTemplateInfo tbody').append(tbody);
                    $('#modalTemplateInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ShowHideEditor(elem) {
    var id = $(elem).closest('tr').next('tr.Text').find('td:eq(2)').find('textarea').attr('id');
    $('.cke_top').fadeToggle();
    $('.ml15').fadeToggle();
    $('.col-md-8').toggleClass('col-md-12');
}
function CKEditor() {
    let editor1;
    $('#tblTestInfo tbody').find('tr.Text').each(function () {
        var data = $(this).find('td:eq(3)').html();
        var id = '#txtTestContent' + $(this).find('td:eq(0)').text();
        var id1 = 'txtTestContent' + $(this).find('td:eq(0)').text();

        //CKEDITOR.editorConfig = function (config) {
        //    // Define changes to default configuration here. For example:
        //    // config.language = 'fr';
        //    // config.uiColor = '#AADC6E';
        //    config.removePlugins = 'blockquote,save,flash,iframe,tabletools,pagebreak,templates,about,showblocks,newpage,language,print,div';
        //    config.removeButtons = 'Print,Form,TextField,Textarea,Button,CreateDiv,PasteText,PasteFromWord,Select,HiddenField,Radio,Checkbox,ImageButton,Anchor,BidiLtr,BidiRtl,Font,Format,Styles,Preview,Indent,Outdent';
        //};

        CKEDITOR.replace(id1, {
            toolbar:
                [
                    { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
                    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
                    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'SpellChecker', 'Scayt'] },
                    {
                        name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton',
                            'HiddenField']
                    },
                    '/',
                    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
                    {
                        name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv',
                            '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
                    },
                    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
                    { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
                    '/',
                    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                    { name: 'colors', items: ['TextColor', 'BGColor'] },
                    { name: 'tools', items: ['Maximize', 'ShowBlocks', '-', 'About'] }
                ]
        });
        CKEDITOR.instances[id1].setData(data);
    });
}

function SaveTestResultEntry(entrySaveType) {
    var objBO = [];
    if ($('#ddlApproveByDoctor option:selected').val() == 'Select' && entrySaveType == 'Approved') {
        alert('Please Select Doctor for Approval');
        return
    }
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyReportEntry";
    $('#tblTestInfo tbody tr').each(function () {
        if ($(this).attr('class') == 'Text') {
            objBO.push({
                'VisitNo': _VisitNo,
                'dispatchLab': Active.HospId,
                'SubCat': $(this).find('td:eq(1)').text(),
                'AutoTestId': $(this).find('td:eq(0)').text(),
                'TestCode': $(this).find('td:eq(1)').text(),
                'ObservationId': '-',
                'ab_flag': '-',
                'read_1': '-',
                'read_2': '-',
                'test_comment': '-',
                'min_value': '-',
                'max_value': '-',
                'nr_range': '-',
                'result_unit': '-',
                'method_name': '-',
                'r_type': $(this).attr('class'),
                'report_text_content': CKEDITOR.instances["txtTestContent" + $(this).find('td:eq(0)').text()].getData(),
                'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                'EntrySaveType': '-',
                'login_id': Active.userId,
                'EntrySaveType': entrySaveType,
                'Logic': 'TestResultEntry'
            });
        }
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                ReportDetail();
                if (data.split('|')[2] == 'Approved') {
                    $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#bbffc9');
                }
                if (data.split('|')[2] == 'Tested') {
                    $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbc7a9');
                }
                if (data.split('|')[2] == 'Partialy-Approved') {
                    $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbeda9');
                }
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
function ApproveTest(elem) {
    if (confirm('Are you sure to Approve?')) {
        var objBO = [];
        var url = config.baseUrl + "/api/Lab/Lab_RadiologyReportEntry";
        objBO.push({
            'VisitNo': _VisitNo,
            'dispatchLab': Active.HospId,
            'SubCat': $(elem).data('testcode'),
            'AutoTestId': 0,
            'TestCode': $(elem).data('testcode'),
            'ObservationId': '-',
            'ab_flag': '-',
            'read_1': '-',
            'read_2': '-',
            'test_comment': '-',
            'min_value': '-',
            'max_value': '-',
            'nr_range': '-',
            'result_unit': '-',
            'method_name': '-',
            'r_type': "Text",
            'report_text_content': '-',
            'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
            'EntrySaveType': '-',
            'login_id': Active.userId,
            'EntrySaveType': 'Approved',
            'Logic': 'TestResultEntry'
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    ReportDetail();
                    if (data.split('|')[2] == 'Approved') {
                        $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#bbffc9');
                    }
                    if (data.split('|')[2] == 'Tested') {
                        $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbc7a9');
                    }
                    if (data.split('|')[2] == 'Partialy-Approved') {
                        $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbeda9');
                    }
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
}
function UnApproveTest() {
    if (confirm('Are you sure to Un-Approve?')) {
        var objBO = [];
        var url = config.baseUrl + "/api/Lab/Lab_RadiologyReportEntry";
        $('#tblApproveTestInfo tbody tr').each(function () {
            if ($(this).find('td:eq(0)').find('input:checkbox:checked')) {
                objBO.push({
                    'VisitNo': _VisitNo,
                    'dispatchLab': Active.HospId,
                    'SubCat': $(this).find('td:eq(1)').text(),
                    'AutoTestId': $(this).find('td:eq(1)').text(),
                    'TestCode': '-',
                    'ObservationId': '-',
                    'ab_flag': '-',
                    'read_1': '-',
                    'read_2': '-',
                    'test_comment': '-',
                    'min_value': '-',
                    'max_value': '-',
                    'nr_range': '-',
                    'result_unit': '-',
                    'method_name': '-',
                    'r_type': "-",
                    'report_text_content': '-',
                    'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                    'EntrySaveType': '-',
                    'login_id': Active.userId,
                    'EntrySaveType': '-',
                    'Logic': 'Un-Approved'
                });
            }
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    $('#tblApproveTestInfo tbody').empty();
                    $('#modalUnApproveInfo').modal('hide');
                    ReportDetail();
                    if (data.split('|')[2] == 'Approved') {
                        $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#bbffc9');
                    }
                    if (data.split('|')[2] == 'Tested') {
                        $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbc7a9');
                    }
                    if (data.split('|')[2] == 'Partialy-Approved') {
                        $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbeda9');
                    }
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
}
function TestComment() {
    var objBO = [];
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyReportEntry";
    objBO.push({
        'VisitNo': _VisitNo,
        'SubCat': _autoTestId,
        'AutoTestId': _autoTestId,
        'TestCode': _testCode,
        'ObservationId': _observationId,
        'ab_flag': '-',
        'read_1': '-',
        'read_2': '-',
        'test_comment': CKEDITOR.instances['txtTestComment'].getData(),
        'min_value': '-',
        'max_value': '-',
        'nr_range': '-',
        'result_unit': '-',
        'method_name': '-',
        'r_type': "Text",
        'report_text_content': '-',
        'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
        'EntrySaveType': '-',
        'login_id': Active.userId,
        'EntrySaveType': '-',
        'Logic': 'InsertTestComment'
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
                CKEDITOR.instances['txtTestComment'].setData('');
                $('#modalTestComment').modal('hide');
                ReportDetail();
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
function UploadLabReport() {
    var objBO = {};
    var url = config.baseUrl + "/api/Lab/UploadLabReport";
    objBO.signid = _autoTestId;
    objBO.doctorid = '-';
    objBO.doctorname = "." + $('#uploadFile').val().split('.').pop();
    objBO.degree = '-';
    objBO.photo_path = ($('#imgFile').attr('src').length > 10) ? 'Y' : 'N';
    objBO.Base64String = $('#imgFile').attr('src');
    objBO.login_id = Active.userId;
    objBO.Logic = 'UploadLabReport';
    console.log(objBO)
    var UploadDocumentInfo = new XMLHttpRequest();
    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('ImageByte', objBO.Base64String);
    UploadDocumentInfo.onreadystatechange = function () {
        if (UploadDocumentInfo.status) {
            if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                var json = JSON.parse(UploadDocumentInfo.responseText);
                if (json.Message.includes('Success')) {
                    alert('Successfully Uploaded..!');
                    $('#modalUploadDocument').modal('hide');
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
function Prints() {
    var url = config.rootUrl + "/lab/Print/PrintLabReport?visitNo=" + _VisitNo + "&SubCat=" + _SubCat + "&TestIds='-'&Logic=ByReportEditing";
    window.open(url, '_blank');
}
function PrintAlls() {
    var url = config.rootUrl + "/lab/Print/PrintLabReport?visitNo=" + _VisitNo + "&SubCat=ALL&TestIds='-'&Logic=ByReportEditing";
    window.open(url, '_blank');
}

function GetAllDepartment() {
    var url = config.baseUrl + "/api/Lab/Lab_RadiologyQueries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = '-';
    objBO.BarccodeNo = '-';
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'LoadTestCategory';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    //$('#ddldepartment').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddldepartment').append($('<option></option>').val(val.SubCatID).html(val.SubCatName));
                    });
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

