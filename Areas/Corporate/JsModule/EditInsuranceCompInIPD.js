
$(document).ready(function () {
    CloseSidebar();
    GetPanelList();
    $(':button[type="submit"]').prop('disabled', true);
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
    objBO.Logic = 'CreditPanelList';
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
function GetAllDataByIPDNo() {
    debugger
    $('#tblFristInsurance tbody').empty();
    $('#tblsecondInsurance tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_PanelChangeQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPDNo = $("#txtIPDNo").val();
    objBO.PanelId = '-';
    objBO.Prm1 = '-';
    objBO.from = '1990-01-01';
    objBO.to = '1990-01-01';
    objBO.login_id = Active.userId;
    objBO.Logic = 'InfoForInsuranceCompanyChange';
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
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#Pname').text(val.patient_name);
                        $('#Rfullname').text(val.roomFullName);
                        $('#IPDNo').text(val.IPDNo);
                        $('#UHIDNo').text(val.UHID);
                        $('#RoomBillingname').text(val.RoomBillingCategory);
                        $('#CurentPanel').text(val.OldPanelName);
                        $(':button[type="submit"]').prop('disabled', false);

                    });
                    if (Object.keys(data.ResultSet.Table1).length) {
                        $.each(data.ResultSet.Table1, function (key, val) {
                            tbody += "<tr>";
                            tbody += "<td style='width:30%;text-align:left'>" + val.InsuranceCompanyName + "</td>";
                            tbody += "<td style='width:10%;'>" + val.ApprovalAmount + "</td>";
                            tbody += "<td style='width:10%;'>" + val.ApprovalDate + "</td>";
                            tbody += "<td style='width:10%;'>" + val.ApprovalType + "</td>";
                            tbody += "<td style='width:10%;'>" + val.ClaimNo + "</td>";
                            tbody += "<td style='width:10%;'>" + val.CoPayAmount + "</td>";
                            tbody += "<td style='width:10%;'>" + val.Discount + "</td>";
                            tbody += "<td style='width:10%;'>" + val.IsSettled + "</td>";
                            tbody += "</tr>";
                        });
                        $('#tblFristInsurance tbody').append(tbody);
                    }
                    if (Object.keys(data.ResultSet.Table2).length) {
                        var ttbody = "";
                        $.each(data.ResultSet.Table2, function (key, val) {
                            ttbody += "<tr>";
                            ttbody += "<td style='width:30%;text-align:left'>" + val.InsuranceCompanyName + "</td>";
                            ttbody += "<td style='width:15%;'>" + val.Amount + "</td>";
                            ttbody += "<td style='width:15%;'>" + val.Narration + "</td>";
                            ttbody += "<td style='width:15%;'>" + val.TnxDate + "</td>";
                            ttbody += "<td style='width:15%;'>" + val.TnxType + "</td>";
                            ;
                            ttbody += "</tr>";
                        });
                        $('#tblsecondInsurance tbody').append(ttbody);
                    }

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SavedataByInsurance() {
    if ($('#ddlPanel').val() === 'Select') {
        alert('Please Select Panel Name');
        $('#ddlPanel').focus();
        return;
    }
    var isConfirmed = confirm('Are you sure you want to submit the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/IPDBilling/IPD_BillingChanges";
        var objBO = {};
        debugger
        objBO.HospId = '-';
        objBO.IPDNo = $("#txtIPDNo").val();
        objBO.PanelId = $('#ddlPanel option:selected').val();
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.ipAddress = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "ChangeInsuranceCompany";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#txtIPDNo").val('');
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });

    } else {

        alert('Data submission canceled.');
    }
}