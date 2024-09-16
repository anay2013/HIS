var _formId = 0;
$(document).ready(function () {
    $('select').select2();
    $('.selection span[aria-labelledby=select2-ddlHeading-container]').css('border-color', '#f39c12');
    GetMenuInfo();
    IT_FormDocMaster();
});
function GetMenuInfo() {
    $("#ddlMenu").empty().append($("<option></option>").val("0").html("Select")).select2();
    $("#ddlHeading").empty().append($("<option></option>").val("0").html("Select")).select2();
    var url = config.baseUrl + "/api/ApplicationResource/IT_FormDocQueries";
    var objBO = {};
    objBO.AutoId = 0;
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetMenuInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlMenu").append($("<option></option>").val(value.sub_menu_id).html(value.sub_menu_name));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, value) {
                        $("#ddlHeading").append($("<option></option>").val(value.AutoId).html(value.GroupName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function IT_FormDocMaster() {
    $('#tblDocMaster tbody').empty();
    var url = config.baseUrl + "/api/ApplicationResource/IT_FormDocQueries";
    var objBO = {};
    objBO.AutoId = 0;
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "IT_FormDoc:Master";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var temp = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        //if (temp != val.FormName) {
                        //    tbody += "<tr style='background:#ffe7c2'>";
                        //    tbody += "<td colspan='3'>" + val.FormName + "</td>";
                        //    tbody += "</tr>";
                        //    temp = val.FormName
                        //}
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.FormId + "</td>";
                        tbody += "<td class='hide'>" + val.FormName + "</td>";
                        tbody += "<td class='hide'>" + val.FormDescription + "</td>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.FormName + "</td>";
                        tbody += "<td><button onclick=ViewDocInfo(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'>&nbsp;</i>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDocMaster tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ViewDocInfo(elem) {
    selectRow($(elem))
    _formId = $(elem).closest('tr').find('td:eq(0)').text();
    var formName = $(elem).closest('tr').find('td:eq(1)').text();
    var AboutForm = $(elem).closest('tr').find('td:eq(2)').text();
    $('#selectedForm').empty().html('<b>Form Info : </b>' + formName);
    $('#txtAboutForm').val(AboutForm);
    DocInfoByFormId()
}
function InsertDocMaster() {
    var url = config.baseUrl + "/api/ApplicationResource/IT_InsertFormDocMaste";
    var objBO = {};
    objBO.AutoId = 0;
    objBO.GroupName = '-';
    objBO.FormName = $('#txtFormName').val();
    objBO.ProjectName = $('#ddlProject option:selected').val();
    objBO.FormMenuId = $('#ddlMenu option:selected').val();
    objBO.FormDescription = $('#txtAboutForm').val();
    objBO.textContent = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "InsertDocMaster";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
                IT_FormDocMaster();
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
function InsertDocGroup() {
    var url = config.baseUrl + "/api/ApplicationResource/IT_InsertFormDocMaste";
    var objBO = {};
    objBO.AutoId = 0;
    objBO.GroupName = $('#txtGroupName').val();
    objBO.FormName = '-';
    objBO.ProjectName = '-';
    objBO.FormMenuId = '-';
    objBO.FormDescription = '-';
    objBO.textContent = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "InsertDocGroup";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                GetMenuInfo();
                $('#modalGroup').modal('hide')
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
function InsertDocInfo() {
    if (Object.keys(_formId).length < 1) {
        alert('Please Select Form')
        return
    }
    if ($('#ddlHeading option:selected').text() == 'Select') {
        alert('Please Select Heading')
        return
    }
    if ($('#txtTextContent').val() == '') {
        alert('Please Provide Heading')
        return
    }
    var url = config.baseUrl + "/api/ApplicationResource/IT_InsertFormDocMaste";
    var objBO = {};
    objBO.AutoId = _formId;
    objBO.GroupName = $('#ddlHeading option:selected').text();
    objBO.FormName = '-';
    objBO.ProjectName = '-';
    objBO.FormMenuId = '-';
    objBO.FormDescription = '-';
    objBO.textContent = $('#txtTextContent').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "InsertDocInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
                DocInfoByFormId();
                $('#txtTextContent').val('');
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
function DocInfoByFormId() {
    $('#tblDocInfo tbody').empty();
    var url = config.baseUrl + "/api/ApplicationResource/IT_FormDocQueries";
    var objBO = {};
    objBO.AutoId = _formId;
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "DocInfoByFormId";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var temp = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (temp != val.GroupName) {
                            tbody += "<tr style='background:#ffe7c2'>";
                            tbody += "<td colspan='3'>" + val.GroupName + "</td>";
                            tbody += "</tr>";
                            temp = val.GroupName
                        }
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.textContentId + "</td>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.textContent + "</td>";
                        tbody += "<td><button onclick=DeleteDocInfo(" + val.textContentId + ") class='btn btn-danger btn-xs'><i class='fa fa-trash'>&nbsp;</i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDocInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeleteDocInfo(textContentId) {
    if (confirm('are you sure?')) {
        var url = config.baseUrl + "/api/ApplicationResource/IT_InsertFormDocMaste";
        var objBO = {};
        objBO.AutoId = textContentId;
        objBO.GroupName = '-';
        objBO.FormName = '-';
        objBO.ProjectName = '-';
        objBO.FormMenuId = '-';
        objBO.FormDescription = '-';
        objBO.textContent = $('#txtTextContent').val();
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "DeleteDocInfo";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    DocInfoByFormId();
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