$(document).ready(function () {
    CloseSidebar();
    GetBillingCategoryList();
});
function GetBillingCategoryList() {
    $("#ddlBilling").empty().append($("<option></option>").val("Select").html("Select"));
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
                $("#ddlBilling").append($("<option></option>").val(val.RoomBillingType).html(val.RoomBillingType));
            });
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function SearchIPDBillingCategory() {
    var templateName = $('#txtbillings').val();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = templateName;
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'ItemSearch';
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
                tbody += '<td class="text-center"><input type="checkbox" id="singlechk"></td>';
                tbody += "<td>" + val.ItemId + "</td>";
                tbody += "<td>" + val.ItemName + "</td>";
                tbody += '<td class="text-center"><input type="text" class="form-control" id="inputvalues" style="border:1px solid #4f7ba1"></td>';
                tbody += "</tr>";
            });
            $('#tblBillingCat tbody').append(tbody);
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function InsertEstimateMaster() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_InsertEstimateMaster";
    var listEstimateValues = [];
    $('#tblBillingCat tbody tr').each(function () {
        var itemidId = ($(this).find('td:eq(1)').text());
        var itemidname = $(this).find('td:eq(2)').text();
        var ValueType = $(this).find('td:eq(3)').text();
        var Value = '';
        if (ValueType == "Text") {
            Value = $(this).find('input[type="text"]').val();
        } else {
            Value = $(this).find('input[type="text"]').val();
        }
        var isSingleCheckbox = $(this).find('#singlechk').is(':checked');
        if (Value.trim() !== "") {
            if (isSingleCheckbox) {
                listEstimateValues.push({
                    'AutoId': '-',
                    'RoomBillingCategory': $("#ddlBilling").val(),
                    'ItemId': itemidId,
                    'ItemName': itemidname,
                    'Amount': Value,
                    'result': '-',
                    'login_id': Active.userId,
                    'Logic': 'Insert'
                });
            }
        }
    });
    if (listEstimateValues.length > 0) {
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(listEstimateValues),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                BillingCategoryList();
                Clear();
            },
            error: function (response) {
                console.error("Error:", response);
                alert('Server Error...!');
            }
        });
    }
    else {
        alert("Please Fill The values to insert.");
    }
}
function DeleteEstimateMaster() {
    var confirmation = confirm("Are you sure want to delete the selected records?");
    if (!confirmation) return;
    var url = config.baseUrl + "/api/IPDBilling/IPD_InsertEstimateMaster";
    var listEstimateValues = [];
    $('#tblAddBilling tbody input:checkbox:checked').each(function () {
        listEstimateValues.push({
            'AutoId': $(this).closest('tr').find('td:first').text(),
            'RoomBillingCategory': '-',
            'ItemId': '-',
            'ItemName': '-',
            'Amount': 0,
            'result': '-',
            'login_id': '-',
            'Logic': 'Delete'
        });
    });
    if (listEstimateValues.length > 0) {
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(listEstimateValues),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                BillingCategoryList();
                Clear();
            },
            error: function (response) {
                console.error("Error:", response);
                alert('Server Error...!');
            }
        });
    }
}
function BillingCategoryList() {
    var ddlid1 = $('#ddlBilling').val();
    $('#tblAddBilling tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateTemplateQueries";
    var objBO = {};
    objBO.cr_date = '1900-01-01';
    objBO.TemplateName = ddlid1;
    objBO.TemplateContent = '-';
    objBO.var_list = '-';
    objBO.TemplateId = 0;
    objBO.login_id = '-';
    objBO.result = '-';
    objBO.Logic = 'ItemLinkWithBillCategory';
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
                tbody += "<td style='display:none;'>" + val.AutoId + "</td>";
                tbody += '<td class="text-center" style="width:1%"><input type="checkbox" id="singlechk1"></td>';
                tbody += "<td>" + val.RoomBillingCategory + "</td>";
                tbody += "<td>" + val.ItemName + "</td>";
                tbody += "<td>" + val.unit_price + "</td>";
                tbody += "</tr>";
            });
            $('#tblAddBilling tbody').append(tbody);
        },
        error: function (error) {
            console.error('Error fetching data:', error);
        }
    });
}
function Clear() {
    $('#tblBillingCat tbody tr').each(function () {
        $('#txtbillings').val('');
        $(this).find('#singlechk').prop('checked', false);
        $(this).find('#inputvalues').val('');
    });
}
function Clear1() {
        //$('#ddlBilling').val('');
        $('#txtbillings').val('');
        $('#txtunitprice').val('');
}
function AddAmount() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_InsertEstimateMaster";
    var listEstimateValues = [];
    var roomBillingCategory = $("#ddlBilling").val();
    var itemName = $('#txtbillings').val();
    var amount = $('#txtunitprice').val();
    if (!roomBillingCategory || !itemName || !amount) {
        alert("Please fill out all required fields.");
        return;
    }
    listEstimateValues.push({
        'AutoId': '-',
        'RoomBillingCategory': $("#ddlBilling").val(),
        'ItemId': '-',
        'ItemName': $('#txtbillings').val(),
        'Amount': $('#txtunitprice').val(),
        'result': '-',
        'login_id': Active.userId,
        'Logic': 'Insert'
    });
    if (listEstimateValues.length > 0) {
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(listEstimateValues),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                BillingCategoryList();
                Clear1();
            },
            error: function (response) {
                console.error("Error:", response);
                alert('Server Error...!');
            }
        });
    }
}
$(document).on('input', '#inputvalues', function () {
    var inputValue = $(this).val();
    var numericValue = inputValue.replace(/\D/g, '');
    $(this).val(numericValue);
});
$(document).on('input', '#txtunitprice', function () {
    var inputValue = $('#txtunitprice').val();
    var numericValue = inputValue.replace(/\D/g, '');
    $(this).val(numericValue);
});