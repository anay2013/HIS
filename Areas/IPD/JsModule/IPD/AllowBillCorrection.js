
var logic = ''
var temp = ''
$(document).ready(function () {
    CloseSidebar();
});

function GetDataAllIPDNO() {
    logic = 'ClosingPending:ByIPDNo';
    $('#tblAllowBillCorrection tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1990/12/01';
    objBO.to = '1990/12/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'ClosingPending:ByIPDNo';
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
                        $("#xyz").val(data.ResultSet.Table[0].BillNo);
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#CCC;'>";
                            tbody += "<td colspan='16' style='font-size:13px;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        if (val.BillClosedDate != null) {
                            tbody += "<tr style='background:#95e996'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.IsCredit + "</td>";
                        tbody += "<td>" + val.TotalAmount + "</td>";
                        tbody += "<td>" + val.Discount + "</td>";
                        tbody += "<td>" + val.NetAmount + "</td>";
                        tbody += "<td>" + val.Tax + "</td>";
                        tbody += "<td>" + val.Payble + "</td>";
                        tbody += "<td>" + val.ApprovalAmount + "</td>";
                        tbody += "<td>" + val.PanelApprovedAmount + "</td>";
                        tbody += "<td>" + val.Advance + "</td>";
                        tbody += "<td>" + val.Balance + "</td>";
                        tbody += "<td>" + val.BillClosedDate + "</td>";
                        tbody += "<td>" + val.IsBillLocked + "</td>";
                        tbody += "<td>" + val.IsBillClosed + "</td>";
                        tbody += "</tr>";
                        if (val.IsBillLocked == "Y" && val.IsBillClosed == "Y") {
                            $('#btnCorrectionRequest').prop('disabled', false)
                        }
                        else {
                            $('#btnCorrectionRequest').prop('disabled', true)
                        }
                        if (val.IsBillLocked == "Y" && val.IsBillClosed == "N") {
                            $('#btnAllowbillCorrection').prop('disabled', false);

                        }
                        else {
                            $('#btnAllowbillCorrection').prop('disabled', true);
                        }

                    });
                    $('#tblAllowBillCorrection tbody').append(tbody);
                    GetDataOpenbill();


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertRemarkIPD() {
    var id = $("#xyz").val();
    var url = config.baseUrl + "/api/IPDBilling/IPD_AllowBillCorrection";
    var objBO = {};
    objBO.IpdNo = $("#txtIPDNo").val();
    objBO.BillNo = id;
    objBO.remark = $("#txtRemark").val();
    objBO.LoginId = Active.userId;
    objBO.Prm1 = '-';
    objBO.Logic = "AllowBillCorrection";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                $('#txtRemark').val('');
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

function SearchEmployeeList() {
    debugger
    $("#ddlemployee").empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1990/12/01';
    objBO.to = '1990/12/01';
    objBO.Prm1 = $("#txtemployeeName").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'EmployeeSearch';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlemployee').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlemployee').append($('<option></option>').val(val.emp_code).html(val.emp_name));
                    });
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}


function InsertOpenBill() {
    debugger
    if ($("#txtRemark").val() == '') {
        alert('Please Enter Remak.');
        $("#txtRemark").focus();
        return
    }
    if ($("#txtemployeeName").val() == '') {
        alert('Please Search Employee.');
        $("#txtemployeeName").focus();
        return
    }

    if ($("#ddlemployee").val() == 'Select') {
        alert('Please Select Employee .');
        $("#ddlemployee").focus();
        return
    }
    var url = config.baseUrl + "/api/IPDBilling/IPD_AllowBillCorrection";
    var objBO = {};
    objBO.IpdNo = $("#txtIPDNo").val();
    objBO.BillNo = '-';
    objBO.remark = $("#txtRemark").val();
    objBO.Prm1 = $("#ddlemployee option:selected").val();
    objBO.LoginId = Active.userId;
    objBO.Logic = "ClosedBillCorrectionReq";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetDataOpenbill();
                $('#txtRemark').val('');
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

function GetDataOpenbill() {
    $('#tblCloseBillOpenRequest tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1990/12/01';
    objBO.to = '1990/12/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'CloseBillOpenRequest';
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
                        if (val.AppStatus == "Approve") {
                            tbody += "<tr style='background:#95e996'>";

                        }
                        else {
                            tbody += "<tr>";

                        }
                        //tbody += "<tr>";
                        tbody += "<td>" + val.IpOpType + "</td>";
                        tbody += "<td>" + val.IPOPNo + "</td>";
                        tbody += "<td>" + val.emp_code + "</td>";
                        tbody += "<td>" + val.emp_name + "</td>";
                        tbody += "<td>" + val.RequestDate + "</td>";
                        tbody += "<td>" + val.ApprovalType + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblCloseBillOpenRequest tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}