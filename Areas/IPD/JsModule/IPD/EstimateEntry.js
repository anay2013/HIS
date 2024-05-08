var sum = 0;
var _totalSum = '';
var _content = '';
var _TemplateId = '';
var _patient = '';
var _EstimateNo = '';
var _uhid = '';
$(document).ready(function () {
    CloseSidebar();
    CKEDITOR.replace("txtTemplate1");
    BillingTypeList();
    PanelList();
    TemplateList();
    $('#ddlTemplate').on('change', function () {
        EstimateContentUse()
    })
    DisableDropdowns();
    $('#tblEstimateEntry thead').on('keyup', 'input[type=text]', function () {
        var quant = $(this).val();
        $('#tblEstimateEntry tbody tr').find('td:eq(3) input').val(quant).trigger('keyup');
    });
});
function BillingTypeList() {
    $('#ddlBillingType').append($('<option></option>').text('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = '-';
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'GetEstimateMasters';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $.each(data.ResultSet.Table, function (key, val) {
                $("#ddlBillingType").append($("<option></option>").val(val.RoomBillingType).html(val.RoomBillingType));
            });
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function PanelList() {
    $('#ddlPanel').append($('<option></option>').text('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = '-';
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'PanelList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $.each(data.ResultSet.Table, function (key, val) {
                $("#ddlPanel").append($("<option></option>").val(val.PanelName).html(val.PanelName));
            });
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function TemplateList() {
    $('#ddlTemplate').append($('<option></option>').val('Select').html('Select')).select2();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = '-';
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'TemplateList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlTemplate').append($('<option></option>').val(val.TemplateId).html(val.TemplateName));
                _TemplateId = val.TemplateId;
            });
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function ShowCalGrid() {
    var ddlid1 = $('#ddlBillingType').val();
    $('#tblEstimateEntry tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = ddlid1;
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'ChargingCalList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            var tbody = "";
            var counter = 1;
            $.each(data.ResultSet.Table, function (key, val) {
                tbody += "<tr>";
                tbody += '<td><a onclick="EstimateRemove(this)"><i class="fa fa-remove" style="font-size:20px;color:red;"></i></a></td>';
                tbody += "<td>" + val.ItemName + "</td>";
                tbody += "<td><input type='text' value='" + val.unit_price + "' id='txtqty' onkeyup=Estimatecalvalue() style='width:70px;border:1px solid #bddeff;height:19px;' class='form-control'></td>";
                tbody += "<td style='text-align:center'><input type='text' id='txt " + counter + "' onkeyup=Estimatecalvalue() style='width:100px;border:1px solid #bddeff;height:19px;' class='form-control'></td>";
                tbody += "<td id='txttotal' style='width:70px;text-align:center' ></td>";
                tbody += "</tr>";
            });
            tbody += "<tr class='total'>";
            tbody += "<td colspan='4' style='text-align:right;id='total'>TOTAL</td>";
            tbody += "<td style='text-align:center' id='txttotalSumval'>0</td>";
            tbody += "</tr>";
            $('#tblEstimateEntry tbody').append(tbody);
            $('#txttotalSumval').text('');
            $('.txttotal').text('');
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function UseTable() {
    var tbody = "";
    tbody += "<div  style='margin-left:34px'><b>BREAKUP :-</b></div>";
    tbody += "<thead>";
    tbody += "<tr>";
    tbody += "<th style='text-align:left;border:1px solid #000;width:55%;padding-left;6px'>ITEM NAME</th>";
    tbody += "<th style='border:1px solid #000;padding:5px;width:15%;text-align:right'>UNIT PRICE</th>";
    tbody += "<th style='border:1px solid #000;padding:5px;width:15%;text-align:right'>QTY</th>";
    tbody += "<th style='border:1px solid #000;padding:5px;width:15%;text-align:right'>TOTAL</th>";
    tbody += "</tr>";
    tbody += "</thead>";
    $('#tblEstimateEntry tbody tr:not(.total)').each(function () {
        tbody += "<tr>";
        tbody += "<td style='margin-top:-50px;border:0;padding:0 5px;text-align:left'>" + $(this).find('td:eq(1)').text() + "</td>";
        tbody += "<td style='text-align:center;border:0;padding:0 5px;text-align:right'>" + $(this).find('td:eq(2) input').val() + "</td>";
        tbody += "<td style='text-align:center;border:0;padding:0 5px;text-align:right'>" + $(this).find('td:eq(3) input').val() + "</td>";
        tbody += "<td style='width:150px;text-align:center;border:0;padding:0 5px;text-align:right'>" + $(this).find('td:eq(4)').text() + "</td>";
        tbody += "</tr>";
    });
    tbody += "<tr>";
    tbody += "<td colspan='3' style='text-align:right;'>TOTAL</td>";
    tbody += "<td style='text-align:right' id='txttotalSumval'>" + $('#tblEstimateEntry tbody tr.total').find('td:eq(1)').text() + "</td>";
    tbody += "</tr>";
    var tblcal = "<div><table style='border:1px solid #000;width:90%;font-size:13px;padding:0 5px;border-collapse:collapse;margin-left:34px'>" + tbody + "</table></div>";
    CKEDITOR.instances['txtTemplate1'].insertHtml(tblcal);
}

function Estimatecalvalue() {
    var unitPrice = 0;
    var qty = 0;
    var total = 0;
    $('#tblEstimateEntry tbody tr:not(.total)').each(function () {
        unitPrice = parseFloat($(this).find('td:eq(2) input').val());
        qty = parseFloat($(this).find('td:eq(3) input').val());
        $(this).find('td:eq(4)').text(unitPrice * qty);
        total += parseFloat($(this).find('td:eq(4)').text());
        $('#txttotalSumval').text(total.toFixed(2));
    });
}

function EstimateRemove(element) {
    $(element).closest('tr').remove();
    var sum = 0;
    $('#tblEstimateEntry tbody #txttotal').each(function () {
        var value = parseFloat($(this).text());
        if (!isNaN(value)) {
            sum += value;
        }
    });
    $('#txttotalSumval').text(sum.toFixed(2));
}

function GetEmployeeUHID() {
    $('#btnUHIDNo').prop('disabled', true);
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    _uhid = $('#txtGetUHID').val();
    objBO.TemplateName = '-';
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.UHID = _uhid;
    objBO.estimateNo = '-';
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'GetEstimateUHID';
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
                        $('#txtName').text(val.FirstName);
                        $('#txtGender').text(val.gender);
                        $('#txtAddress').text(val.address);
                        $('#txtAge').text(val.age);
                        $('#txtUH').text(val.UHID);
                        _patient = val.patient_name;
                        EstimateEntryList();
                    });
                }
            }
            enableDropdowns();
        },
        error: function (err) {
            alert('server error');
        },
    })
}
function EstimateContentUse() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateId = $("#ddlTemplate option:selected").val();
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateName = '-';
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'EstimateContent';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    CKEDITOR.instances['txtTemplate1'].insertHtml(data.ResultSet.Table[0].TemplateContent);
                }
            }
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function InsertEstimateEntry() {
    var uhid1 = $('#txtGetUHID').val();
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_IPD_EstimatesEntry";
    var templatecontent = CKEDITOR.instances['txtTemplate1'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    if (result) {
        for (var i = 0; i < result.length; i++) {
            varList.push($(result[i]).text());
        }
    } else {
        console.log("No matches found in templatecontent.");
    }

    var billingType = $('#ddlBillingType option:selected').text();
    var panel = $('#ddlPanel option:selected').text();
    var templateName = $('#ddlTemplate option:selected').text();
    if (!uhid1 || billingType === 'Select' || panel === 'Select' || templateName === 'Select' || templatecontent.trim() === '') {
        alert("Please fill in all the required fields.");
        return;
    }
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = '-';
    objBO.uhid = uhid1;
    objBO.patientName = _patient;
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.BillingType = billingType;
    objBO.Panel = panel;
    objBO.estContent = templatecontent;
    objBO.TemplateName = templateName;
    objBO.result = '-';
    objBO.Logic = 'Insert';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            EstimateEntryList();
            Clear();
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function UpdatEstimateForm() {
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_IPD_EstimatesEntry";
    var templatecontent = CKEDITOR.instances['txtTemplate1'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    if (result) {
        for (var i = 0; i < result.length; i++) {
            varList.push($(result[i]).text());
        }
    } else {
        console.log("No matches found in templatecontent.");
    }
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = _EstimateNo;
    objBO.uhid = _uhid;
    objBO.patientName = _patient;
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.BillingType = $('#ddlBillingType Option:Selected').text();
    objBO.Panel = $('#ddlPanel Option:Selected').text();
    objBO.estContent = templatecontent;
    objBO.TemplateName = $('#ddlTemplate Option:Selected').text();
    objBO.result = '-';
    objBO.Logic = 'Update';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            EstimateEntryList();
            Clear();
        },
        error: function (err) {
            alert('server error');
        },
    });
}
function EstimateEntryList() {
    $('#tblEstimateEntryList tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = '-';
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.UHID = _uhid;
    objBO.estimateNo = '-';
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'EstimateEntryGrid';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            $.each(data.ResultSet.Table, function (key, val) {
                tbody += "<tr>";
                var estDate = new Date(val.estDate);
                var day = estDate.getDate().toString().padStart(2, '0');
                var month = (estDate.getMonth() + 1).toString().padStart(2, '0');
                var year = estDate.getFullYear();
                var formattedDate = day + '/' + month + '/' + year;
                tbody += "<td>" + formattedDate + "</td>";
                tbody += "<td>" + val.estimateNo + "</td>";
                tbody += "<td>" + val.BillingType + "</td>";
                tbody += "<td>" + val.Panel + "</td>";
                tbody += "<td class='hide'>" + val.autoId + "</td>";
                tbody += "<td class='hide'>" + val.estContent + "</td>";
                tbody += "<td class='hide'>" + val.uhid + "</td>";
                tbody += '<td class="text-center"><button class="btn btn-primary btn-sm" style="height: 21px;width: 38px;margin-bottom:2px;margin-top:-1px;" onclick=SeEstimateNo("' + val.estimateNo + '");EstimateInfo(this) data-billing="' + val.BillingType + '" data-tempname="' + val.TemplateId + '" data-panel="' + val.Panel + '" ><center style="font-size:11px;margin-top:-5px;margin-left:-1px;">Edit</center></button></td>';
                tbody += "</tr>";
            });
            $('#tblEstimateEntryList tbody').append(tbody);
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function Clear() {
    $('#tblEstimateEntry tbody').empty();
    $('#tblEstimateEntryList tbody').empty();
    $('select').prop('selectedIndex', '0').trigger('change.select2');
    $('#btnUHIDNo').prop('disabled', false);
    CKEDITOR.instances['txtTemplate1'].setData('');
    DisableDropdowns();
    $('#btnSubmit').text('Submit').addClass('btn-success').removeClass('btn-warning').attr('onclick', 'InsertEstimateEntry()');
}
function EstimateInfo(elem) {
    var content = $(elem).closest('tr').find('td:eq(5)').html();
    var Bilingtype = $(elem).data('billing');
    var PanelName1 = $(elem).data('panel');
    var Templatename = $(elem).data('tempname');
    $('#ddlBillingType').val(Bilingtype).trigger('change.select2');
    $('#ddlPanel').val(PanelName1).trigger('change.select2');
    $('#ddlTemplate').val(Templatename).trigger('change.select2');
    //$('#txttotalSumval').val(sum);
    CKEDITOR.instances['txtTemplate1'].setData(content);
    $('#btnSubmit').text('Update').addClass('btn-warning').removeClass('btn-success').attr('onclick', 'UpdatEstimateForm()');

}
function SeEstimateNo(est) {
    _EstimateNo = est;
}
function EstimateEntryPrint1() {
    var url = "../Print/PrintEstimateForm?UHIDNO=" + _uhid + "&estNO=" + _EstimateNo;
    window.open(url, '_blank');
}

function DisableDropdowns() {
    $('#ddlBillingType').prop('disabled', true);
    $('#ddlPanel').prop('disabled', true);
    $('#ddlTemplate').prop('disabled', true);
}

function enableDropdowns() {
    $('#ddlBillingType').prop('disabled', false);
    $('#ddlPanel').prop('disabled', false);
    $('#ddlTemplate').prop('disabled', false);
}
