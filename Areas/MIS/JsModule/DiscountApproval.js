var temp = "";
$(document).ready(function () {
    FillCurrentDate("txtfromdate");
    FillCurrentDate("txttodate");
    CloseSidebar();

    $('#tblDiscountApproval tbody').on('click', '#DiscountApproval', function () {
        TnxIdd = $(this).closest('tr').find('td:eq(0)').text();
        Remark = $(this).closest('tr').find('td:eq(1)').text();
        PopOpen(TnxIdd)
        $("#TnxIdd").val(TnxIdd);
        $("#txtRemark").text(Remark);


    });
    $('#tblDiscountApproval tbody').on('click', '.DiscountApprovalDetails', function (event) {
        event.preventDefault();
        debugger;
        var ipop_no = $(this).closest('tr').find('td:eq(2)').text().trim();
        DetailsPopOpen(ipop_no);
    });

});

function PopOpen(ipopno) {
    $('#DiscountApprovalPop').modal('show');

}
function GetDataApprovalOrCancel() {
    if ($('#ddlType').val() === 'Select') {
        alert('Please Select Type');
        $('#ddlType').focus();
        return;
    }
    $('#tblDiscountApproval tbody').empty();
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
    objBO.Logic = 'DiscountReport';
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
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#d3d0d0;'>";
                            tbody += "<td colspan='18' style='font-size:13px; text-align:left;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        if (val.isDiscountConfirm == "Approve") {
                            tbody += "<tr style='background:#b4f1ae'>";
                        }
                        else if (val.isDiscountConfirm == "Reject") {
                            tbody += "<tr style='background:#efc6c6'>";
                        }
                        else if (val.isDiscountConfirm == "Quarried") {
                            tbody += "<tr style='background:#d3c99799'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        //tbody += "<tr>";
                        tbody += "<td hidden>" + val.TnxId + "</td>";
                        tbody += "<td hidden>" + val.DiscountConfirmRemark + "</td>";
                        tbody += "<td style='width:10%;'><a href='#' class='DiscountApprovalDetails' style='color: blue;'>" + val.ipop_no + "</a></td>";
                        tbody += "<td style='width:20%;text-align:left'>" + val.tnxDate + "</td>";
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
                        tbody += "<td style='width:10%;text-align:left'>" + val.discountBy + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.discountType + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.discountReason + "</td>";
                        tbody += "<td style='width:10%'>" + val.IsCredit + "</td>";
                        tbody += "<td style='width:10%'><button class='btn btn-success btn-xs' id='DiscountApproval'>Approve</button></td>";
                        tbody += "</tr>";


                    });
                    $('#tblDiscountApproval tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Approvedata(action) {
    debugger;
    var confirmationMessage;
    var isConfirmed;
    if (action === 'Approve') {
        confirmationMessage = 'Are you sure you want to Approve the data?';
    } else if (action === 'Quarried') {
        confirmationMessage = 'Are you sure you want to Quarried the data?';
    } else if (action === 'Reject') {
        confirmationMessage = 'Are you sure you want to Reject the data?';
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
        objBO.isDiscountConfirm = action;
        objBO.discountConfirmBy = Active.userId;
        objBO.discountConfirmDate = $("#txtfromdate").val();
        objBO.DiscountConfirmRemark = $("#txtRemark").val();
        objBO.isCancelConfirm = '-';
        objBO.CancelConfirmBy = '-';
        objBO.CancelConfirmDate = $("#txtfromdate").val();
        objBO.CancelConfirmRemark = '-';
        objBO.CancelAgainst = '-';
        objBO.Login_id = Active.userId;
        objBO.Logic = "DiscountOrCancelApproval";
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
                        var $cell = $("#tblDiscountApproval tbody td:contains('" + txtid + "')");
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
function DownloadExcelDiscount() {
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
    objBO.Logic = 'DiscountReport';
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "DiscountApproval" + ".xlsx");
}
function DetailsPopOpen(ipop_no) {
    $('#DiscountApprovaldetailspop').modal('show');
    GetDataApprovalOrCancelDetails(ipop_no)
}
function GetDataApprovalOrCancelDetails(ipopno) {
    $('#tblDiscountApprovalDetails tbody').empty();
    var url = config.baseUrl + "/api/IPOPAudit/IPOP_ApprovalOrCancelQuries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.IPOPNo = '-'
    objBO.PanelId = '-';
    objBO.prm_1 = ipopno;
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
                        tbody += "<td style='width:5%;text-align:center;width:20%'>" + val.discount + "</td>";
                        tbody += "<td style='width:5%;text-align:center;width:10%'>" + val.adl_discount + "</td>";
                        tbody += "<td style='width:5%;text-align:center;width:10%'>" + val.amount + "</td>";

                        tbody += "</tr>";


                    });
                    $('#tblDiscountApprovalDetails tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}