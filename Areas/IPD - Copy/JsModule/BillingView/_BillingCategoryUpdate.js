$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    GetRoomChargesInfo();
});

function GetRoomChargesInfo() {
    $('#ddlCategory').empty().append($('<option></option>').val('Select').html('Select')).select2();
    $('#tblRoomCharges tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetRoomChargesInfo';
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
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.AutoId + "</td>";
                        tbody += "<td>" + val.trn_date + "</td>";
                        tbody += "<td>" + val.RoomType + "</td>";
                        tbody += "<td>";
                        tbody += "<select>";
                        $.each(data.ResultSet.Table1, function (key, val1) {
                            tbody += (val.RoomBillingCategory == val1.RoomBillingType) ? "<option selected>" + val1.RoomBillingType + "</option>" : "<option>" + val1.RoomBillingType + "</option>";
                        });
                        tbody += "</select>";
                        tbody += "</td>";
                        tbody += "<td class='text-right'>" + val.amount + "</td>";
                        tbody += "<td><button data-logic='-' onclick=UpdateCategory(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'>&nbsp;</i>Update</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblRoomCharges tbody').append(tbody);
                    $('#tblRoomCharges tbody select').select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlCategory').append($('<option></option>').val(val.RoomBillingType).html(val.RoomBillingType));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateCategory(elem) {
    if (confirm('Are you sure to update?')) {
        var url = config.baseUrl + "/api/IPDBilling/UpdateIPD_RoomBillingCategory";
        var objBO = [];        
        if ($(elem).data('logic') == 'ALL') {            
            $('#tblRoomCharges tbody tr').each(function () {
                objBO.push({
                    'AutoId': $(this).find('td:eq(0)').text(),
                    'RoomBillingCategory': $(this).find('select option:selected').text(),
                    'from': '1900/01/01',
                    'to': '1900/01/01',
                    'Prm1': '-',
                    'login_id': Active.userId,
                    'Logic': 'UpdateBillingCat',
                });
            });
        }
        else {
            objBO.push({
                'AutoId': $(elem).closest('tr').find('td:eq(0)').text(),
                'RoomBillingCategory': $(elem).closest('tr').find('select option:selected').text(),
                'from': '1900/01/01',
                'to': '1900/01/01',
                'Prm1': '-',
                'login_id': Active.userId,
                'Logic': 'UpdateBillingCat',
            });
        }        
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
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
function UpdateCatBetweenDate() {
    if (confirm('Are you sure to update?')) {
        var url = config.baseUrl + "/api/IPDBilling/UpdateIPD_RoomBillingCategory";
        var objBO = [];
        objBO.push({
            'AutoId': 0,
            'RoomBillingCategory': '-',
            'from': $('#txtFrom').val(),
            'to': $('#txtTo').val(),
            'Prm1': $('#ddlCategory option:selected').text(),
            'login_id': Active.userId,
            'Logic': 'UpdateBillingCatBetweenDate',
        });                 
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
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