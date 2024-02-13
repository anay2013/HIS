var temp1 = ''
var temp = ''
var _IpdNo = ''

$(document).ready(function () {
    //alert("hello");
    CloseSidebar();
    GetPanelList();
});
function GetPanelList() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_PanelChangeQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPDNo = '-';
    objBO.PanelId = '-';
    objBO.Prm1 = '-';
    objBO.from = '1990-01-01';
    objBO.to = '1990-01-01';
    objBO.login_id = Active.userId;
    objBO.Logic = 'LoadPanel';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {

                    $('#ddlPanel').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));

                    });
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetAllDataIPDNoByPanel() {
    if ($('#ddlPanel').val() === 'Select') {
        alert('Please Select Panel Name');
        $('#ddlPanel').focus();
        return;
    }
    _IpdNo = $("#txtIPDNo").val();
    $('#tblExitingAmount tbody').empty();
    $('#tblChangeAmount tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_PanelChangeQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPDNo = $("#txtIPDNo").val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.Prm1 = '-';
    objBO.from = '1990-01-01';
    objBO.to = '1990-01-01';
    objBO.login_id = Active.userId;
    objBO.Logic = 'AmountsForPanelChange';

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);

            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                var totalAmountExiting = 0;
                var totalAmountChange = 0;

                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#Pname').text(val.patient_name);
                        $('#Rfullname').text(val.roomFullName);
                        $('#IPDNo').text(val.IPDNo);
                        $('#UHIDNo').text(val.UHID);
                        $('#RoomBillingname').text(val.RoomBillingCategory);
                        $('#CurentPanel').text(val.OldPanelName);

                    });

                    if (Object.keys(data.ResultSet.Table1).length) {
                        var totalAmountExiting = 0;
                        var groupedData = {};

                        $.each(data.ResultSet.Table1, function (key, val) {
                            // Group by CatName
                            if (!groupedData[val.CatName]) {
                                groupedData[val.CatName] = { count: 0, items: [], sumAmount: 0 };
                            }

                            // Increment the count, store the item, and update the sum of amounts
                            groupedData[val.CatName].count++;
                            groupedData[val.CatName].items.push(val);
                            groupedData[val.CatName].sumAmount += parseFloat(val.amount);

                            totalAmountExiting += parseFloat(val.amount);
                        });
                        $("#txtExitingamount").val(totalAmountExiting);
                        $("#txtExitingamount").prop("disabled", true);

                        // Display grouped data in the table
                        var tbody = '';
                        $.each(groupedData, function (catName, group) {
                            tbody += "<tr style='background:#CCC'>";
                            tbody += "<td colspan='5' style='font-size:13px;'><b>" + catName + "</b></td>";
                            //tbody += "<td colspan='1' style='font-size:13px;'><b>" + group.count + "</b></td>";
                            tbody += "<td colspan='1' style='font-size:13px;'><b>" + group.sumAmount + "</b></td>";
                            tbody += "</tr>";
                            $.each(group.items, function (index, item) {
                                tbody += "<tr>";
                                tbody += "<td>" + item.tnxDate.split('T')[0] + "</td>";
                                tbody += "<td>" + item.ItemName + "</td>";
                                tbody += "<td>" + item.panel_rate + "</td>";
                                tbody += "<td>" + item.discount + "</td>";
                                tbody += "<td>" + item.adl_discount + "</td>";
                                tbody += "<td>" + item.amount + "</td>";
                                tbody += "</tr>";
                            });
                        });
                        $('#tblExitingAmount tbody').html(tbody);
                    }

                    if (Object.keys(data.ResultSet.Table2).length) {
                        var totalAmountChange = 0;
                        var groupedData = {};
                        $.each(data.ResultSet.Table2, function (key, val) {
                            // Group by CatName
                            if (!groupedData[val.CatName]) {
                                groupedData[val.CatName] = { count: 0, items: [], sumAmount2: 0 };
                            }
                            // Increment the count, store the item, and update the sum of amounts
                            groupedData[val.CatName].count++;
                            groupedData[val.CatName].items.push(val);
                            groupedData[val.CatName].sumAmount2 += parseFloat(val.amount);
                            totalAmountChange += parseFloat(val.amount);
                        });
                        // Display totalAmountExiting
                        $("#txtChangedAmount").val(totalAmountChange);
                        $("#txtChangedAmount").prop("disabled", true);
                        // Display grouped data in the table
                        var tbody = '';
                        $.each(groupedData, function (catName, group) {
                            tbody += "<tr style='background:#CCC'>";
                            tbody += "<td colspan='5' style='font-size:13px;'><b>" + catName + "</b></td>";
                            //tbody += "<td colspan='1' style='font-size:13px;'><b>" + group.count + "</b></td>";
                            tbody += "<td colspan='1' style='font-size:13px;'><b>" + group.sumAmount2 + "</b></td>";
                            tbody += "</tr>";
                            $.each(group.items, function (index, item) {
                                tbody += "<tr>";
                                tbody += "<td>" + item.tnxDate.split('T')[0] + "</td>";
                                tbody += "<td>" + item.ItemName + "</td>";
                                tbody += "<td>" + item.panel_rate + "</td>";
                                tbody += "<td>" + item.panel_discount + "</td>";
                                tbody += "<td>" + item.adl_discount + "</td>";
                                tbody += "<td>" + item.amount + "</td>";
                                tbody += "<td hidden>" + item.SurgeryId + "</td>";
                                tbody += "<td hidden>" + item.auto_id + "</td>";
                                tbody += "<td hidden>" + item.auto_id1 + "</td>";
                                tbody += "</tr>";
                            });
                        });
                        $('#tblChangeAmount tbody').html(tbody);
                    }

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Submitdata() {
    var isConfirmed = confirm('Are you sure you want to submit the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/IPDBilling/IPD_BillingChanges";
        var objBO = {};
        debugger
        objBO.HospId = '-';
        objBO.IPDNo = _IpdNo;
        objBO.PanelId = $('#ddlPanel option:selected').val();
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.ipAddress = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "ChangePanel";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);

                    GetAllDataIPDNoByPanel();
                    $("#txtChangedAmount").val('');
                    $("#txtExitingamount").val('');
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });

        //alert('Data submitted successfully!');
    } else {

        alert('Data submission canceled.');
    }
};