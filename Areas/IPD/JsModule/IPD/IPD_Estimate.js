var _TemplateId = '';
$(document).ready(function () {
    CloseSidebar();
    $('#dash-dynamic-section').find('label.title').text('Estimate To Patiant').show();
    CKEDITOR.replace("txtTemplate1");
    $('#tblEstimateVariable tbody').on('change', 'input:checkbox', function () {
        var var1 = $(this).closest('tr').find('td:eq(1)').text();
        if ($(this).is(':checked'))
            CKEDITOR.instances['txtTemplate1'].insertHtml('{<strong class="var">' + var1 + '</strong>}');
        else
            CKEDITOR.instances['txtTemplate1'].insertText('');
    });
    EstimateTemplateMasterList();
});
function InsertEstimateTemplateMaster() {
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_IPD_EstimateTemplate";
    var templatecontent = CKEDITOR.instances['txtTemplate1'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    if (result) {
        for (var i = 0; i < result.length; i++) {
            varList.push($(result[i]).text());
        }
    } else {
        console.log("No matches found in templatecontent.");
    }

    if (!$('#txtEstimateTemplateName').val() || !templatecontent) {
        alert("Please fill in all required fields.");
        return;
    }
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = $('#txtEstimateTemplateName').val();
    objBO.TemplateContent = templatecontent;
    objBO.var_list = varList.join(',');
    objBO.TemplateId = 0;
    objBO.login_id = Active.userId;
    objBO.result = '-';
    objBO.Logic = 'Insert';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            EstimateTemplateMasterList();
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
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_IPD_EstimateTemplate";
    var templatecontent = CKEDITOR.instances['txtTemplate1'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    if (result) {
        for (var i = 0; i < result.length; i++) {
            varList.push($(result[i]).text());
        }
    } else {
        console.log("No matches found in templatecontent.");
    }

    if (!$('#txtEstimateTemplateName').val() || !templatecontent) {
        alert("Please fill in all required fields.");
        return;
    }
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = $('#txtEstimateTemplateName').val();
    objBO.TemplateContent = templatecontent;
    objBO.var_list = varList.join(',');
    objBO.TemplateId = _TemplateId;
    objBO.login_id = Active.userId;
    objBO.result = '-';
    objBO.Logic = 'Update';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            Clear();
            EstimateTemplateMasterList();
        },
        error: function (err) {
            alert('server error');
        },
    });
}
function EstimateTemplateMasterList() {
    $('#tblEstimateTemplateMaster tbody').empty();
    var objBO = {};
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = '-';
    objBO.TemplateContent = 'test';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'GetEstimateTemplateMasterList';
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
                tbody += "<td>";
                tbody += "<label class='switch'>";
                tbody += "<input type='checkbox' data-id=" + val.TemplateId + " data-logic='ActiveStatus' onchange=ActiveEstimate(this) class='IsActive' id='chkActive' " + val.checked + ">";
                tbody += "<span class='slider round'></span>";
                tbody += "</label>";
                tbody += "</td>";
                tbody += "<td class='hide'>" + val.VarList + "</td>";
                tbody += "<td class='hide'>" + val.TemplateContent + "</td>";
                tbody += "<td class='hide'>" + val.TemplateId + "</td>";
                tbody += "<td>" + val.cr_date + "</td>";
                tbody += "<td>" + val.TemplateName + "</td>";
                tbody += '<td class="text-center"><button class="btn btn-warning btn-sm" style="height: 21px;width:30px;margin-bottom:2px;margin-top:-1px;" onclick=SeEstimateNo("' + val.TemplateId + '");EstimateInfo(this) data-tempname="' + val.TemplateName + '" ><center style="font-size:17px;margin-top:-5px;margin-left:-1px;"><i class="fa fa-sign-in"></i></center></button></td>';
                tbody += "</tr>";

            });
            $('#tblEstimateTemplateMaster tbody').append(tbody);
        },
        error: function (response) {
            console.error("Error:", respon);
            alert('Server Error...!');
        }
    });
}
function EstimateInfo(elem) {
    $('#btnSubmit').text('Update').addClass('btn-warning').removeClass('btn-success').attr('onclick', 'UpdatEstimateForm()');
    var varList = $(elem).closest('tr').find('td:eq(1)').text();
    var result = varList.split(',');
    var content = $(elem).closest('tr').find('td:eq(2)').html();
    var Tname = $(elem).data('tempname');
    $('#txtEstimateTemplateName').val(Tname);
    CKEDITOR.instances['txtTemplate1'].setData(content);
    $('#tblEstimateVariable tbody').find('input:checkbox').prop('checked', false);
    for (var i = 0; i <= result.length; i++) {
        $('#tblEstimateVariable tbody tr').each(function () {
            if ($(this).find('td:eq(1)').text() == result[i])
                $(this).find('td:eq(2)').find('input:checkbox').prop('checked', true);
        });
    }
}
function SeEstimateNo(est) {
    _TemplateId = est;
}
function Clear() {
    _TemplateId = null;
    $('#txtEstimateTemplateName').val('');
    $('#tblEstimateVariable tbody').find('input:checkbox').prop('checked', false);
    CKEDITOR.instances['txtTemplate1'].setData('');
    $('#btnSubmit').text('Submit').addClass('btn-success').removeClass('btn-warning');
}
function ActiveEstimate(elem) {
    var objBO = {};
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = '-';
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = $(elem).data('id');;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'Update:isActiveEstimate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            EstimateTemplateMasterList()
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

