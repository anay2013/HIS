var temp = "";
$(document).ready(function () {
    FillCurrentDate("txtfromdate");
    FillCurrentDate("txttodate");
    CloseSidebar();

    $('#tblCancelRefundApproval tbody').on('click', '#CancelRefundApproval', function () {
        TnxIdd = $(this).closest('tr').find('td:eq(0)').text();
        remark = $(this).closest('tr').find('td:eq(1)').text();
        CaneclPopOpen(TnxIdd)
        $("#TnxIdd").val(TnxIdd);
        $("#txtRemark").val(remark);

    });
    $('#tblCancelRefundApproval tbody').on('click', '.CancelRefundApprovalDetails', function (event) {
        event.preventDefault();
        ipop_no = $(this).closest('tr').find('td:eq(2)').text().trim();
        DetailsPop(ipop_no);
    });
});

function CaneclPopOpen(ipopno) {
    $('#CancelRefundApprovalPop').modal('show');

}
function GetDataCanelRefundApproval() {
    if ($('#ddlType').val() === 'Select') {
        alert('Please Select Type');
        $('#ddlType').focus();
        return;
    }
    $('#tblCancelRefundApproval tbody').empty();
    var url = config.baseUrl + "/api/IPOPAudit/IPOP_ApprovalOrCancelQuries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPOPNo = $('#ddlType option:selected').val();
    objBO.PanelId = '-';
    objBO.prm_1 = $('#ddlStatus option:selected').val();
    objBO.prm_2 = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'CancelReport';
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
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#d3d0d0;'>";
                            tbody += "<td colspan='20' style='font-size:13px; text-align:left;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        if (val.isCancelConfirm == "Approve") {
                            tbody += "<tr style='background:#b4f1ae'>";
                        }

                        else if (val.isCancelConfirm == "Quarried") {
                            tbody += "<tr style='background:#d3c99799'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        //tbody += "<tr>";
                        tbody += "<td hidden>" + val.TnxId + "</td>";
                        tbody += "<td hidden>" + val.CancelConfirmRemark + "</td>";
                        tbody += "<td style='width:10%;'><a href='#' class='CancelRefundApprovalDetails' style='color: blue;'>" + val.ipop_no + "</a></td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.tnxDate + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.UHID + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.tnxType + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.DoctorName + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.ref_name + "</td>";
                        tbody += "<td style='width:10%'>" + val.GrossAmount + "</td>";
                        tbody += "<td style='width:10%'>" + val.discount + "</td>";
                        tbody += "<td style='width:10%'>" + val.adl_discount + "</td>";
                        tbody += "<td style='width:10%'>" + val.NetAmount + "</td>";
                        tbody += "<td style='width:10%'>" + val.TotalTax + "</td>";
                        tbody += "<td style='width:10%'>" + val.Payable + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.CancelBy + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.CancelDate + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.CancelReason + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.CancelAgainst + "</td>";
                        tbody += "<td>" + val.EmpName + "</td>";
                        tbody += "<td style='width:10%'>" + val.IsCredit + "</td>";
                        tbody += "<td style='width:6%'><button class='btn btn-success btn-xs' id='CancelRefundApproval'>Approve</button></td>";
                        tbody += "</tr>";


                    });
                    $('#tblCancelRefundApproval tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function CancelApprovaldata(action) {
    debugger;
    var confirmationMessage;
    var isConfirmed;

    if (action === 'Approve') {
        confirmationMessage = 'Are you sure you want to approve the data?';
    } else if (action === 'Quarried') {
        confirmationMessage = 'Are you sure you want to Quarried the data?';
    }
    isConfirmed = confirm(confirmationMessage);
    if (isConfirmed) {
        var txtid = $("#TnxIdd").val();
        var url = config.baseUrl + "/api/IPOPAudit/DiscountOrCancelApproval";
        var objBO = {};
        objBO.auto_id = '-';
        objBO.hosp_id = '-';
        objBO.ipop_no = '-';
        objBO.TnxId = txtid;
        objBO.isDiscountConfirm = '-';
        objBO.discountConfirmBy = '-';
        objBO.discountConfirmDate = $("#txtfromdate").val();
        objBO.DiscountConfirmRemark = '-';
        objBO.isCancelConfirm = action;
        objBO.CancelConfirmBy = Active.userId;
        objBO.CancelConfirmDate = $("#txtfromdate").val();
        objBO.CancelConfirmRemark = $("#txtRemark").val();
        objBO.CancelAgainst = txtid;
        objBO.Login_id = Active.userId;
        objBO.Logic = "CancelRefundApproval";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    //alert(data);
                    $('#txtRemark').val('');
                    if (txtid != null && txtid !== "") {
                        var $cell = $("#tblCancelRefundApproval tbody td:contains('" + txtid + "')");
                        var $row = $cell.closest('tr');
                        $row.remove();
                    }

                } else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
    else {
        alert('Data submission canceled.');
    }
}
function DownloadExcelCancel() {
    var url = config.baseUrl + "/api/IPOPAudit/IPOP_ApprovalOrCancelQuries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPOPNo = $('#ddlType option:selected').val();
    objBO.PanelId = '-';
    objBO.prm_1 = $('#ddlStatus option:selected').val();
    objBO.prm_2 = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'CancelReport';
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "CancelRefundApproval" + ".xlsx");
}
function DetailsPop(ipop_no) {
    debugger
    $('#CancelRefundApprovalDetailsPop').modal('show');
    GetDataCancelRefundApprovalDetails(ipop_no)
}
function GetDataCancelRefundApprovalDetails(ipop_no) {
    debugger
    $('#tblCancelRefundApprovalDetails tbody').empty();
    var url = config.baseUrl + "/api/IPOPAudit/IPOP_ApprovalOrCancelQuries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPOPNo = '-'
    objBO.PanelId = '-';
    objBO.prm_1 = ipop_no;
    objBO.prm_2 = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'ItemDetail';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {

                        tbody += "<tr>";
                        tbody += "<td style='width:10%;text-align:center;width:15%'>" + val.ItemId + "</td>";
                        tbody += "<td style='width:10%;text-align:left;width:35%'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:5%;text-align:center;width:10%'>" + val.panel_rate + "</td>";
                        tbody += "<td style='width:5%;text-align:center;width:10%'>" + val.discount + "</td>";
                        tbody += "<td style='width:5%;text-align:center;width:20%'>" + val.adl_discount + "</td>";
                        tbody += "<td style='width:5%;text-align:center;width:10%'>" + val.amount + "</td>";

                        tbody += "</tr>";


                    });
                    $('#tblCancelRefundApprovalDetails tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}