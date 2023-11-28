var _AutoId;
var _TemplateId = null;
$(document).ready(function () {
    TemplateList();
    HeaderInfo();
    CKEDITOR.replace('txtTemplate', {
        enterMode: CKEDITOR.ENTER_BR,
        shiftEnterMode: CKEDITOR.ENTER_P,
    });
    $('#tblHeaderMaster tbody').on('click', 'button', function () {
        selectRow($(this));
        var AutoId = $(this).closest('tr').find('td:eq(1)').text();
        var HeaderName = $(this).closest('tr').find('td:eq(2)').text();
        _AutoId = AutoId;
        $('#txtHeaderName').val(HeaderName);
        $('#btnSaveHeaderMaster i').removeClass('fa-plus-circle').addClass('fa-edit');
    });
});
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
                HeaderInfo();
                Clear();
            }
            else {
                alert(data);
                HeaderInfo();
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function HeaderInfo() {
    $('#ddlTemplate').empty().append($('<option></option>').val('Select').html('Select')).select2();
    $('#ddlHeader').empty().append($('<option></option>').val('Select').html('Select')).select2();
    $('#tblHeaderMaster tbody').empty();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'HeaderInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            var tbody = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {             
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IsActive == 'Y') {
                            $('#ddlHeader').append($('<option></option>').val(val.HeaderId).html(val.HeaderName));
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
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function TemplateList() {
    $('#txtTemplateName').val('Select');
    $('#ddlTemplate').empty().append($('<option></option>').val('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlTemplate').append($('<option data-id=' + val.AutoId + '></option>').val(val.TemplateId).html(val.TemplateName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetTemplateContent() {
    CKEDITOR.instances['txtTemplate'].setData('');
    var url = config.baseUrl + "/api/IPDDoctor/IPD_DoctorQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $('#ddlTemplate option:selected').val();
    objBO.Prm2 = '';
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
                        $('#btnSaveTemplateMaster').text('Update').removeClass('btn-success').addClass('btn-warning');
                    });
                }
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
    objBO.HeaderId = $('#ddlHeader option:selected').val();
    objBO.HeaderName = '-';
    objBO.TemplateId = $('#ddlTemplate option:selected').val();
    objBO.TemplateName = $('#txtTemplateName').val();
    objBO.Prm1 = CKEDITOR.instances['txtTemplate'].getData();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveTemplateMaster').text() == 'Save') ? "InsertTemplate" : 'UpdateTemplate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                Clear()
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
function Clear() {
    _AutoId = 0;
    $('#ddlHeader').prop('selectedIndex', '0').change();
    CKEDITOR.instances['txtTemplate'].setData('');
    $('#btnSaveTemplateMaster').text('Save').addClass('btn-success').removeClass('btn-warning');
}