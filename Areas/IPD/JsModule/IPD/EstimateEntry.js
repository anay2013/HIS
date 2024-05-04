var sum = '';
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
});
function BillingTypeList() {
    $("#ddlBillingType").empty().append($("<option></option>").val("Select").html("Select"));
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
    $("#ddlPanel").empty().append($("<option></option>").val("Select").html("Select"));
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
                $("#ddlPanel").append($("<option></option>").val(val.PanelId).html(val.PanelName));
            });
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function TemplateList() {
    $("#ddlTemplate").empty().append($("<option></option>").val("Select").html("Select"));
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
                $("#ddlTemplate").append($("<option></option>").val(val.TemplateId).html(val.TemplateName));
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
                tbody += "<td>" + val.unit_price + "</td>";
                tbody += "<td style='text-align:center'><input type='text' id='txt " + counter + "' onkeyup=Estimatecalvalue(this) style='width:100px;border:1px solid #bddeff;height:19px;' class='form-control'></td>";
                tbody += "<td id='txttotal' style='width:70px' style='text-align:center'></td>";
                tbody += "</tr>";
            });
            $('#tblEstimateEntry tbody').append(tbody);
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function UseTable() {
    var tbody = "";
    tbody += "<thead>";
    tbody += "<tr>";
    tbody += "<th style='text-align:left'>Item Name</th>";
    tbody += "<th>Unit Price</th>";
    tbody += "<th>Quantity</th>";
    tbody += "<th style='width:70px'>Total</th>";
    tbody += "</tr>";
    tbody += "</thead>";
    $('#tblEstimateEntry tbody tr').each(function () {
        tbody += "<tr>";
        tbody += "<td>" + $(this).find('td:eq(1)').text() + "</td>";
        tbody += "<td style='text-align:center;'>" + $(this).find('td:eq(2)').text() + "</td>";
        tbody += "<td style='text-align:center'>" + $(this).find('td:eq(3) input').val() + "</td>";
        tbody += "<td style='width:150px;text-align:center'>" + $(this).find('td:eq(4)').text() + "</td>";
        tbody += "</tr>";
    });
    var tblcal = "<div><table style='border-collapse:collapse;width:100%;border:1px solid #fff'>" + tbody + "</table><table><b>Total :</b>" + sum + "</table><hr style='background-color:red'>__________________________________________________________________________________________<hr></div>";
    CKEDITOR.instances['txtTemplate1'].insertHtml(tblcal);
}
function Estimatecalvalue(input) {
    var row = $(input).closest('tr');
    var dailyTotal = parseFloat(row.find('td:eq(2)').text());
    var inputVal = parseFloat($(input).val());
    var total = dailyTotal * inputVal;
    row.find("#txttotal").text(isNaN(total) ? '' : total);
    sum = 0;
    $('#tblEstimateEntry tbody #txttotal').each(function () {
        var value = parseFloat($(this).text());
        if (!isNaN(value)) {
            sum += value;
        }
    });
    $('#txttotalSumval').text(sum);
}
$(document).on('blur', 'input[type="text"]', function () {
    var value = $(this).val().trim();
    if (value === '') {
        $(this).val('');
        $(this).closest('tr').find("#txttotal").text('');
    }
});
function InputValues() {
    var totalDaysValue = $('#txtTotalDays').val();
    $("table tbody tr").each(function () {
        $(this).find("input[type='text']").val(totalDaysValue).trigger('keyup');
    });
}
function EstimateRemove(element) {
    $(element).closest('tr').remove();
    sum = 0;
    $('#tblEstimateEntry tbody #txttotal').each(function () {
        var value = parseFloat($(this).text());
        if (!isNaN(value)) {
            sum += value;
        }
    });
    $('#txttotalSumval').text(sum);
}
function GetEmployeeUHID() {
    $('#btnUHIDNo').prop('disabled', true);
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    _uhid = $('#txtGetUHID').val();
    objBO.TemplateName = _uhid;
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
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
    if (!uhid1 || !billingType || !panel || !templateName || templatecontent.trim() === '' || templateName === 'Select') {
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
    objBO.TemplateName = _uhid;
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
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
                tbody += '<td class="text-center"><button class="btn btn-primary btn-sm" style="height: 21px;width: 38px;margin-bottom:2px;margin-top:-1px;" onclick=SeEstimateNo("' + val.estimateNo + '");EstimateInfo(this) data-billing="' + val.BillingType + '" data-tempname="' + val.TemplateName + '" data-panel="' + val.Panel + '" ><center style="font-size:11px;margin-top:-5px;margin-left:-1px;">Edit</center></button></td>';
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
    $('#ddlBillingType Option:Selected').text('');
    $('#ddlPanel Option:Selected').text('');
    $('#ddlTemplate Option:Selected').text('');
    $('#txttotalSumval').text('');
    $('#btnUHIDNo').prop('disabled', false);
    CKEDITOR.instances['txtTemplate1'].setData('');
    $('#btnSubmit').text('Submit').addClass('btn-success').removeClass('btn-warning').attr('onclick', 'InsertEstimateEntry()');

}
function EstimateInfo(elem) {
    $('#btnSubmit').text('Update').addClass('btn-warning').removeClass('btn-success').attr('onclick', 'UpdatEstimateForm()');
    var content = $(elem).closest('tr').find('td:eq(5)').html();
    var Bilingtype = $(elem).data('billing');
    var Templatename = $(elem).data('tempname');
    var PanelName = $(elem).data('panel');
    $('#ddlBillingType Option:Selected').text(Bilingtype)
    $('#ddlTemplate Option:Selected').text(Templatename);
    $('#ddlPanel Option:Selected').text(PanelName)
    CKEDITOR.instances['txtTemplate1'].setData(content);
}
function SeEstimateNo(est) {
    _EstimateNo = est;
}
function EstimateEntryPrint1() {
    var url = "../Print/PrintEstimateForm?UHIDNO=" + _uhid + "&estNO=" + _EstimateNo;
    window.open(url, '_blank');
}
