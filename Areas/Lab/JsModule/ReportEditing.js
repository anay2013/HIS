var _VisitNo;
var _BarCodeNo;
var _SubCat;
var _selectedTextGroup;
var _currentSelectedReport;
var _testCode;
var _autoTestId;
var _observationId;
var _rowIndex;
var _elem;

$(document).ready(function () {

    CKEDITOR.replace("txtTestComment");
    CKEDITOR.editorConfig = function (config) {
        config.removePlugins = 'blockquote,save,flash,iframe,tabletools,pagebreak,templates,about,showblocks,newpage,language,print,div';
        config.removeButtons = 'Print,Form,TextField,Textarea,Button,CreateDiv,PasteText,PasteFromWord,Select,HiddenField,Radio,Checkbox,ImageButton,Anchor,BidiLtr,BidiRtl,Font,Format,Styles,Preview,Indent,Outdent';
    };
    $("#uploadFile").change(function () {
        readURL(this);
    });
    $("#tblTestInfo tbody").on('click', 'a', function () {
        var testName = $(this).text();
        var testCode = $(this).closest('tr').find('td:eq(1)').text();
        var ObservationId = $(this).closest('tr').find('td:eq(2)').text();
        RecordTracking(ObservationId, testCode);
        $('#modalDelta').modal('show')
        $('#modalDelta').find('h5').text(testName)
    });
    $("#tblReport tbody").on('mouseover', '.barcode', function () {
        $(this).closest('td').append('<div class="colDate"></div>')
        CollecDateByBarcode(this)
        $(this).closest('td').find('.colDate').show();
    }).on('mouseout', '.barcode', function () {
        CollecDateByBarcode(this)
        $(this).closest('td').find('.colDate').remove();
        $(this).closest('td').find('.colDate').hide();
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
        var comment = $(this).closest('tr').find('td:nth-last-child(4)').html();
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
function CollecDateByBarcode(elem) {
    $(elem).siblings('div.colDate').html('');
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = '-';
    objBO.BarccodeNo = $(elem).text();
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'CollecDateByBarcode';
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
                        $(elem).siblings('div.colDate').html('Sample Coll. Date<br>' + val.collect_date);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LabReporting(logic) {
    $('#tblReport tbody').empty();
    $('#tblTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.ReportStatus = $('#ddlStatus option:selected').text();
    objBO.VisitNo = $('#txtInput').val();
    objBO.BarccodeNo = $('#txtInput').val();
    objBO.SubCat = $('#ddldepartment option:selected').val();
    objBO.TestCategory = $('#txtInput').val();
    objBO.AutoTestId = 0;
    objBO.Prm1 = $('#txtInput').val();
    objBO.TestCode = $('#ddlTest option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = ($('#ddlStatus1 option:selected').text() == 'Patient Name') ? 'ByPatientName:Report' : logic;
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
                        tbody += "<td><a href='#' class='barcode'>" + val.barcodeNo + "</a></td>";
                        tbody += (eval(val.IsUrgent) > 0) ? "<td><i class='urgent'>U</i>&nbsp;" + val.testCategory + "" : "<td>" + val.testCategory;
                        tbody += "<td style=width:1%><button class='btn btn-success btn-xs'><span class='fa fa-arrow-right'></button></td>";
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
function ApprovedTestInfo() {
    $('#tblApproveTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
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

function TemplateByTestCode(elem) {
    _selectedTextGroup = $(elem);
    $('#tblTemplateInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
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
function ReportDetail() {
    _SubCat = $(_currentSelectedReport).closest('tr').find('td:eq(0)').text();
    _VisitNo = $(_currentSelectedReport).closest('tr').find('td:eq(2)').text();
    _BarCodeNo = $(_currentSelectedReport).closest('tr').find('td:eq(3)').text();
    $('#tblTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = $('#ddlIpOpType option:selected').text();
    objBO.ReportStatus = $('#ddlStatus option:selected').text();
    objBO.SubCat = _SubCat;
    objBO.VisitNo = _VisitNo;
    objBO.BarccodeNo = _BarCodeNo;// $('#txtInput').val();
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
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
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        _UHIDForInfo = val.UHID;
                        $('span[data-petientname]').text(val.patient_name);
                        $('span[data-gender]').text(val.gender);
                        $('span[data-age]').text(val.ageInfo);
                        $('span[data-ipop]').text(val.ipop_no);
                        $('span[data-visitno]').text(val.VisitNo);
                        $('span[data-regdate]').text(val.RegDate);
                        var more = "<button id='testMore' class='btn btn-warning btn-xs pull-right' data-test='" + val.TestList + "'>More</button>";
                        if (val.TestList.length > 110)
                            $('span[data-testname]').html("<b>IPD Info : </b>" + val.TestList + more);
                        else
                            $('span[data-testname]').html("<b>IPD Info : </b>" + val.TestList);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody = '';
                    var temp = '';
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (val.r_type == 'Text') {
                            if (val.IsTested == 1)
                                tbody += "<tr style='background:#fbc7a9'>";
                            else
                                tbody += "<tr>";

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
                            tbody += "<td><input type='checkbox' checked/></td>";
                            tbody += "</tr>";
                        } else {
                            if (val.ObsCount == 1) {
                                $.each(data.ResultSet.Table2, function (key, val1) {
                                    if (val.testcode == val1.testcode) {
                                        tbody += "<tr class=" + val.r_type + ">";
                                        if (val1.IsTested == 1)
                                            tbody += "<tr style='background:#fbc7a9' class=" + val.r_type + ">";

                                        if (val1.IsApproved == 1)
                                            tbody += "<tr style='background:#bbffc9' class=" + val.r_type + ">";

                                        tbody += "<td class='hide'>" + val1.AutoTestId + "</td>";
                                        tbody += "<td class='hide'>" + val1.testcode + "</td>";
                                        tbody += "<td class='hide'>" + val1.ObservationId + "</td>";
                                        tbody += "<td class='hide'>" + val1.min_value + "</td>";
                                        tbody += "<td class='hide'>" + val1.max_value + "</td>";
                                        tbody += "<td class='hide'>" + val1.result_unit + "</td>";
                                        var bgComment = (val1.test_comment != '') ? '#f98a01' : '#b3b0b0';
                                        if (val1.IsApproved == 1)
                                            tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i><a href='#'>" + val1.ObservationName + "</a><i class='fa fa-check-circle pull-right text-success'></i></td>";
                                        else
                                            tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i><a href='#'>" + val1.ObservationName + "</a></td>";

                                        tbody += "<td><input type='text' value='" + val1.read_1 + "' data-min='" + val1.min_value + "' data-max='" + val1.max_value + "' class='form-control value'/></td>";
                                        tbody += "<td>";
                                        tbody += "<select class='form-control textValue'>";

                                        for (var i = 0; i < val1.DefaultValue.split('|').length; i++)
                                            tbody += "<option>" + val1.DefaultValue.split('|')[i];

                                        tbody += "</select>";
                                        tbody += "<input value='" + val1.read_2 + "' class='form-control textValue'/>";
                                        tbody += "</td>";
                                        if (val1.ab_flag == 'L')
                                            tbody += "<td class='lowFlag'>" + val1.ab_flag + "</td>";
                                        else if (val1.ab_flag == 'H')
                                            tbody += "<td class='highFlag'>" + val1.ab_flag + "</td>";
                                        else
                                            tbody += "<td>" + val1.ab_flag + "</td>";

                                        tbody += "<td>" + val1.min_value + ' - ' + val1.max_value + ' ' + val1.result_unit + "</td>";
                                        tbody += "<td class='hide'>" + val1.method_name + "</td>";
                                        tbody += "<td class='hide'>" + val1.mac_name + "</td>";
                                        tbody += "<td >" + val1.mac_reading + "<button data-testcode=" + val.testcode + " onclick=DeltaReportByVisitNo(" + val.testcode + ") class='btn btn-warning btn-xs pull-right'><i class='fa fa-bar-chart'>&nbsp;</i></button></td>";
                                        tbody += "<td class='hide'>" + val1.test_comment + "</td>";
                                        tbody += "<td><button data-testcode=" + val.testcode + " onclick=uploadFile(" + val.AutoTestId + ") class='btn btn-primary btn-xs pull-right'><i class='fa fa-upload'>&nbsp;</i>Add</button></td>";
                                        tbody += "<td class='hide'>" + val1.nr_range + "</td>";
                                        tbody += (val1.read_1 != '' || val1.read_2 != '' && val1.IsApproved == 0) ? "<td><input type='checkbox' checked/></td>" : "<td>-</td>";
                                        tbody += "</tr>";
                                    }
                                });
                            }
                            else {
                                tbody += "<tr style='background:#ddd'>";
                                tbody += "<td colspan='5'>" + val.TestName + "</td>";
                                tbody += "<td><button data-testcode=" + val.testcode + " onclick=DeltaReportByVisitNo(" + val.testcode + ") class='btn btn-warning btn-xs pull-right'><i class='fa fa-bar-chart'>&nbsp;</i></button></td>";
                                tbody += "<td><button data-testcode=" + val.testcode + " onclick=uploadFile(" + val.AutoTestId + ") class='btn btn-primary btn-xs pull-right'><i class='fa fa-upload'>&nbsp;</i>Add</button></td>";
                                tbody += "<td><input type='checkbox' checked/></td>";
                                tbody += "</tr>";

                                $.each(data.ResultSet.Table2, function (key, val1) {
                                    if (val.testcode == val1.testcode) {
                                        _testcode = val1.testcode;

                                        if (val1.IsGroup == "Y") {
                                            tbody += "<tr style='background:#f9e7bf'>";
                                            tbody += "<td colspan='6'>" + val1.ObservationName + "</td>";
                                            tbody += "<td>-</td>";
                                            tbody += "<td><input type='checkbox' checked/></td>";
                                            tbody += "</tr>";
                                        }
                                        else {
                                            tbody += "<tr class=" + val.r_type + ">";
                                            if (val1.IsTested == 1)
                                                tbody += "<tr style='background:#fbc7a9' class=" + val.r_type + ">";

                                            if (val1.IsApproved == 1)
                                                tbody += "<tr style='background:#bbffc9' class=" + val.r_type + ">";

                                            tbody += "<td class='hide'>" + val1.AutoTestId + "</td>";
                                            tbody += "<td class='hide'>" + val1.testcode + "</td>";
                                            tbody += "<td class='hide'>" + val1.ObservationId + "</td>";
                                            tbody += "<td class='hide'>" + val1.min_value + "</td>";
                                            tbody += "<td class='hide'>" + val1.max_value + "</td>";
                                            tbody += "<td class='hide'>" + val1.result_unit + "</td>";
                                            var bgComment = (val1.test_comment != '') ? '#f98a01' : '#b3b0b0';

                                            if (val1.IsApproved == 1)
                                                tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i><a href='#'>" + val1.ObservationName + "</a><i class='fa fa-check-circle pull-right text-success'></i></td>";
                                            else
                                                tbody += "<td><i style='background:" + bgComment + "' class='test-comment'>cm</i><a href='#'>" + val1.ObservationName + "</a></td>";

                                            tbody += "<td><input type='text' value='" + val1.read_1 + "' data-min='" + val1.min_value + "' data-max='" + val1.max_value + "' class='form-control value'/></td>";
                                            tbody += "<td>";
                                            tbody += "<select class='form-control textValue'>";

                                            for (var i = 0; i < val1.DefaultValue.split('|').length; i++)
                                                tbody += "<option>" + val1.DefaultValue.split('|')[i];

                                            tbody += "</select>";
                                            tbody += "<input value='" + val1.read_2 + "' class='form-control textValue'/>";
                                            tbody += "</td>";
                                            if (val1.ab_flag == 'L')
                                                tbody += "<td class='lowFlag'>" + val1.ab_flag + "</td>";
                                            else if (val1.ab_flag == 'H')
                                                tbody += "<td class='highFlag'>" + val1.ab_flag + "</td>";
                                            else
                                                tbody += "<td>" + val1.ab_flag + "</td>";

                                            tbody += "<td>" + val1.min_value + ' - ' + val1.max_value + ' ' + val1.result_unit + "</td>";
                                            tbody += "<td class='hide'>" + val1.method_name + "</td>";
                                            tbody += "<td class='hide'>" + val1.mac_name + "</td>";
                                            tbody += "<td >" + val1.mac_reading + "</td>";
                                            tbody += "<td class='hide'>" + val1.test_comment + "</td>";
                                            tbody += "<td >-</td>";
                                            tbody += "<td class='hide'>" + val1.nr_range + "</td>";
                                            tbody += (val1.read_1 != '' || val1.read_2 != '' && val1.IsApproved == 0) ? "<td><input type='checkbox' checked/></td>" : "<td>-</td>";
                                            tbody += "</tr>";
                                        }
                                    }

                                })
                                if (_testcode == val.testcode) {
                                    tbody += "<tr style='background:#2478a9'>";
                                    tbody += "<td colspan='8' style='line-height: 0px;padding: 1px;'></td>";
                                    tbody += "</tr>";
                                }
                            }
                        }
                    });
                    $('#tblTestInfo tbody').append(tbody);
                }
                $("#ddlApproveByDoctor").empty().append($("<option></option>").val("Select").html("Select")).select2();
                if (Object.keys(data.ResultSet).length > 0) {
                    if (Object.keys(data.ResultSet.Table3).length > 0) {
                        $.each(data.ResultSet.Table3, function (key, value) {
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
function FlipLeftBlock() {
    $('#LeftBlock').toggleClass('fliped');
}
function SaveTestResult() {
    var objBO = [];
    $('#tblTestInfo tbody tr').each(function () {
        if ($(this).find('td').length > 0) {
            if ($(this).attr('class') == 'Text') {
                objBO.push({
                    'VisitNo': _VisitNo,
                    'dispatchLab': Active.HospId,
                    'SubCat': _SubCat,
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
                    'EntrySaveType': 'Tested',
                    'Logic': 'TestResultEntry'
                });
            }
            if ($(this).attr('class') == 'Value') {
                var read1 = $(this).find('td:eq(7)').find('input').val();
                var read2 = $(this).find('td:eq(8)').find('input.textValue').val();
                if (read1.trim() != '' || read2.trim() != '') {
                    objBO.push({
                        'VisitNo': _VisitNo,
                        'dispatchLab': Active.HospId,
                        'SubCat': _SubCat,
                        'AutoTestId': $(this).find('td:eq(0)').text(),
                        'TestCode': $(this).find('td:eq(1)').text(),
                        'ObservationId': $(this).find('td:eq(2)').text(),
                        'ab_flag': $(this).find('td:eq(9)').text(),
                        'read_1': $(this).find('td:eq(7)').find('input').val(),
                        'read_2': $(this).find('td:eq(8)').find('input.textValue').val(),
                        'test_comment': '-',
                        'min_value': $(this).find('td:eq(3)').text(),
                        'max_value': $(this).find('td:eq(4)').text(),
                        'nr_range': $(this).find('td:eq(16)').text(),
                        'result_unit': $(this).find('td:eq(5)').text(),
                        'method_name': $(this).find('td:eq(11)').text(),
                        'r_type': $(this).attr('class'),
                        'report_text_content': '-',
                        'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                        'EntrySaveType': 'Tested',
                        'login_id': Active.userId,
                        'Logic': 'TestResultEntry'
                    });
                }
            }
        }
    });
    if (objBO.length > 0)
        submitTestResult(objBO, 'Tested')
}
function ApproveTestResult() {
    var objBO = [];
    if ($('#tblTestInfo tbody').find('input:checkbox').is(':checked').length == 0) {
        alert('Please Select Test For Approval');
        return
    }
    if ($('#ddlApproveByDoctor option:selected').text() == 'Select') {
        alert('Please Select Approve By Doctor');
        return
    }
    $('#tblTestInfo tbody tr').each(function () {
        if ($(this).find('input:checkbox').is(':checked')) {
            if ($(this).attr('class') == 'Text') {
                objBO.push({
                    'VisitNo': _VisitNo,
                    'dispatchLab': Active.HospId,
                    'SubCat': _SubCat,
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
                    'EntrySaveType': 'Approved',
                    'Logic': 'TestResultEntry'
                });
            }
            if ($(this).attr('class') == 'Value') {
                var read1 = $(this).find('td:eq(7)').find('input').val();
                var read2 = $(this).find('td:eq(8)').find('input.textValue').val();
                if (read1.trim() != '' || read2.trim() != '') {
                    objBO.push({
                        'VisitNo': _VisitNo,
                        'dispatchLab': Active.HospId,
                        'SubCat': _SubCat,
                        'AutoTestId': $(this).find('td:eq(0)').text(),
                        'TestCode': $(this).find('td:eq(1)').text(),
                        'ObservationId': $(this).find('td:eq(2)').text(),
                        'ab_flag': $(this).find('td:eq(9)').text(),
                        'read_1': $(this).find('td:eq(7)').find('input').val(),
                        'read_2': $(this).find('td:eq(8)').find('input.textValue').val(),
                        'test_comment': '-',
                        'min_value': $(this).find('td:eq(3)').text(),
                        'max_value': $(this).find('td:eq(4)').text(),
                        'nr_range': $(this).find('td:eq(16)').text(),
                        'result_unit': $(this).find('td:eq(5)').text(),
                        'method_name': $(this).find('td:eq(11)').text(),
                        'r_type': $(this).attr('class'),
                        'report_text_content': '-',
                        'DoctorSignId': $('#ddlApproveByDoctor option:selected').val(),
                        'EntrySaveType': 'Approved',
                        'login_id': Active.userId,
                        'Logic': 'TestResultEntry'
                    });
                }
            }
        }
    });
    if (objBO.length > 0)
        submitTestResult(objBO, 'Approved')
}
function submitTestResult(objBOData, entrySaveType) {
    var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBOData),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                ReportDetail();
                if (entrySaveType == 'Approved' && data.split('|')[2] == 'Approved') {
                    $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#bbffc9');
                }
                if (entrySaveType == 'Approved' && data.split('|')[2] == 'Tested') {
                    $('#tblReport tbody').find('tr').eq(_rowIndex).css('background', '#fbc7a9');
                }
                if (entrySaveType == 'Approved' && data.split('|')[2] == 'Partialy-Approved') {
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

function UnApproveTest() {
    if (confirm('Are you sure to Un-Approve?')) {
        var objBO = [];
        var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
        $('#tblApproveTestInfo tbody tr').each(function () {
            if ($(this).find('td:eq(0)').find('input:checkbox').is(':checked')) {
                objBO.push({
                    'VisitNo': _VisitNo,
                    'dispatchLab': Active.HospId,
                    'SubCat': _SubCat,
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
function TestComment(logic) {
    var objBO = [];
    var url = config.baseUrl + "/api/sample/Lab_ResultEntry";
    objBO.push({
        'VisitNo': _VisitNo,
        'SubCat': _SubCat,
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
        'EntrySaveType': logic,
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
function Print() {
    var url = config.rootUrl + "/lab/Print/PrintLabReport?visitNo=" + _VisitNo + "&SubCat=" + _SubCat + "&TestIds='-'&Logic=ByReportEditing";
    window.open(url, '_blank');
}
function PrintAll() {
    var url = config.rootUrl + "/lab/Print/PrintLabReport?visitNo=" + _VisitNo + "&SubCat=ALL&TestIds='-'&Logic=ByReportEditing";
    window.open(url, '_blank');
}
function GetTestNameByDept() {
    $('#ddlTest').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
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
function GetAllDepartment() {
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
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
//Delta Info
function RecordTracking(ObservationId, testCode) {
    $('#tblTestTrackingReport tbody').empty();
    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    // var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.VisitNo = $('#tblReport tbody').find('tr.select-row').find('td:eq(2)').text();
    objBO.BarcodeNo = '-';
    objBO.SampleCode = '-';
    objBO.TestCode = testCode;
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.Prm1 = ObservationId;
    objBO.login_id = Active.userId;
    objBO.Logic = 'RecordTrackingByTestCode';
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
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.IPOPType + "</td>";
                        tbody += "<td>" + val.barcodeNo + "</td>";
                        tbody += "<td>" + val.RegDate + "</td>";
                        tbody += "<td>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td>" + val.testCategory + "</td>";
                        tbody += "<td>" + val.samp_code + "</td>";
                        tbody += "<td>" + val.sample_collect_date + "</td>";
                        tbody += "<td>" + val.sample_collect_by + "</td>";
                        tbody += "<td>" + val.SampleDistributedDate + "</td>";
                        tbody += "<td>" + val.SampleDistributedBy + "</td>";
                        tbody += "<td>" + val.dispatch_date + "</td>";
                        tbody += "<td>" + val.SampleDispatchBy + "</td>";
                        tbody += "<td>" + val.DispatchReceivedTime + "</td>";
                        tbody += "<td>" + val.DispatchReceivedBy + "</td>";
                        tbody += "<td>" + val.LabReceivedDate + "</td>";
                        tbody += "<td>" + val.LabReceivedBy + "</td>";
                        tbody += "<td>" + val.max_reptime + "</td>";
                        tbody += "<td>" + val.DelivaryTime + "</td>";
                        tbody += "<td>" + val.ApprovedDate + "</td>";
                        tbody += "<td>" + val.ApproveBy + "</td>";
                        tbody += "<td>" + val.IsSampleRequired + "</td>";
                        tbody += "<td>" + val.IsLocalTest + "</td>";
                        tbody += "<td>" + val.IsCancelled + "</td>";
                        tbody += "<td>" + val.r_type + "</td>";
                        tbody += "<td>" + val.InOutStatus + "</td>";
                        tbody += "</tr>";

                    });
                    $('#tblTestTrackingReport tbody').append(tbody);
                    DeltaReport(data)
                }

            }
        },
        complete: function (response) {
            var temp = "";
            var counter = 0;
            var count = 0;
            var graph = [];
            if (Object.keys(response.responseJSON.ResultSet).length > 0) {
                if (Object.keys(response.responseJSON.ResultSet.Table1).length > 0) {
                    $.each(response.responseJSON.ResultSet.Table1, function (key, val) {
                        if (temp != val.ObservationId) {
                            count++;
                            if (count > 1) {
                                PopulateChart(graph, counter);
                                counter++;
                                graph = [];
                            }
                            temp = val.ObservationId;
                        }
                        graph.push(response.responseJSON.ResultSet.Table1[key])
                    });
                    PopulateChart(graph, count - 1);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeltaReport(data) {
    $('#tblReportSummary tbody').empty();
    $('#deltaReport').empty();
    if (Object.keys(data.ResultSet).length > 0) {
        if (Object.keys(data.ResultSet.Table1).length > 0) {
            var tbody = '';
            var temp = '';
            var flag = '';
            var counter = 0;
            var count = 0;
            var graph = [];
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (val.ab_flag == 'L')
                            flag = 'lowReading';

                        if (val.ab_flag == 'H')
                            flag = 'highReading';

                        if (val.ab_flag == 'N')
                            flag = 'normalReading';

                        if (temp != val.ObservationId) {
                            count++;
                            if (count > 1) {
                                tbody = "<div class='divChartTemp'><canvas id='chartDelta'></canvas></div>";
                            }
                            //tbody += "<label class='labelGroup'>" + val.ObservationName + " Report <b class='pull-right'>Ref. Range : " + val.ref_range + "</b></label>";
                            temp = val.ObservationId;
                        }
                        //tbody += "<div class='reportRound " + flag + "'>" + val.read_1 + "<hr />" + val.result_date + "</div>";
                    });
                }
            }

            tbody = "<div class='divChartTemp'><canvas id='chartDelta'></canvas></div>";
            $('#deltaReport').append(tbody);

            var temp1 = '';
            var tbody1 = '';
            //table bind
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (temp1 != val.ObservationId) {
                            tbody1 += "<tr class='group'>";
                            tbody1 += "<td colspan='6'>" + val.ObservationName + "</td>";
                            tbody1 += "</tr>";
                            temp1 = val.ObservationId;
                        }
                        count++;
                        tbody1 += "<tr>";
                        tbody1 += "<td>" + val.rowNo + "</td>";
                        tbody1 += "<td>" + val.RegDate + "</td>";
                        if (val.ab_flag != 'N')
                            tbody1 += "<td style='font-weight:bold' >" + val.read_1 + "</td>";
                        else
                            tbody1 += "<td>" + val.read_1 + "</td>";
                        if (val.ab_flag != 'N')
                            tbody1 += "<td>" + val.ab_flag + "</td>";
                        else
                            tbody1 += "<td></td>";

                        tbody1 += "<td>" + val.ref_range + "</td>";
                        tbody1 += "</tr>";
                    });
                    $('#tblReportSummary tbody').append(tbody1);
                }
            }
        }
    }
}
function PopulateChart(response, elem) {
    ctxL = $('#deltaReport').find('.divChartTemp').find('#chartDelta')[0].getContext('2d');
    var xValues = [];
    var TempArr = [];
    var minValue = 0;
    var maxValue = 0;
    for (var i in response) {
        xValues.push(response[i].result_date);
        TempArr.push(response[i].read_1);
        minValue = parseFloat(response[i].min_value);
        maxValue = parseFloat(response[i].max_value)
    }
    var config = {
        type: 'line',
        data: {
            labels: xValues,
            datasets: [
                {
                    label: "Reading",
                    data: TempArr,
                    backgroundColor: ['rgba(0, 137, 132, .2)',], borderColor: ['rgba(0, 10, 130, .7)',],
                    borderWidth: 2
                }
            ]
        },
        options: {
            responsive: true,
            scales: {
                yAxes: [{
                    ticks: {
                        min: minValue / 2,
                        max: maxValue
                    }
                }]
            }
        }
    };
    var myLineChart = new Chart(ctxL, config);
}
//Delta 2
function DeltaReportByVisitNo(Testcode) {
    $('#tblDelta2 thead').empty();
    $('#tblDelta2 tbody').empty();
    var url = config.baseUrl + "/api/Lab/Lab_DeltaQueries";
    var objBO = {};
    objBO.IpOpType = '-';
    objBO.IPDNo = _VisitNo;
    objBO.UHID = '-';
    objBO.TestCode = Testcode;
    objBO.ObservationId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'DeltaReportByVisitNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length == 1)
                return

            var thead = "";
            var tbody = "";
            var lblPatientInfo = "";
            $.each(data.ResultSet.Table, function (key, val) {
                lblPatientInfo += "<b>UHID : " + val.UHID + "</b>, ";
                lblPatientInfo += "<b>Patient Name : " + val.patient_name + "</b>, ";
                lblPatientInfo += "<b>Age Info : " + val.ageInfo + "</b>, ";
                lblPatientInfo += "<b>Admit Date : " + val.AdmitDate + "</b>";
            });
            var col = Object.keys(data.ResultSet.Table1[0]);
            thead += "<tr>";
            for (var i = 0; i < col.length; i++) { thead += "<th>" + col[i] + "</th>" };
            thead += "</tr>";
            $.each(data.ResultSet.Table1, function (key, val) {
                tbody += "<tr>";
                for (var i = 0; i < col.length; i++) {
                    tbody += "<td>" + val[col[i]] + "</td>";
                }
                tbody += "</tr>";
            });
            $('#tblDelta2 thead').append(thead);
            $('#tblDelta2 tbody').append(tbody);
            $('#lblPatientInfo').html(lblPatientInfo);
            $('#modalDelta2').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeltaReportPrint() {
    var url = config.documentServerUrl + "/Lab/Print/DeltaReport?IPDNO=" + _IPDNo + "&Testcodevalue=" + Testcode + "&Testname=" + Testname;
    window.open(url, '_blank');

}