var logic = ""
var temp = ''
$(document).ready(function () {
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');
    CloseSidebar();
    GetAllDataBindPanel();
});

function GetAllDataBindPanel() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'LoadPanel';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlPanel').append($('<option></option>').val('ALL').html('ALL')).select2();
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
function GetDataAllDateWise() {
    logic = 'IPD:CheckBillStatus';
    $('#tblCheckBillStatus tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/Audit_IPOPBillQuerise";
    var objBO = {};
    objBO.IPOPNo = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = $('#ddlReportType option:selected').val();
    objBO.Prm2 = '-';
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.Logic = 'IPD:CheckBillStatus';
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
                        if (temp != val.TPAPanelName) {
                            tbody += "<tr style='background:#CCC;'>";
                            tbody += "<td colspan='15' style='font-size:13px;'><b>" + val.TPAPanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.TPAPanelName
                        }
                        if (val.IsMisMatch == "Y") {
                            tbody += "<tr style='background:#eda1a1'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td style='width:7%'>" + val.IPDNo + "</td>";
                        tbody += "<td style='width:7%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.IsCredit + "</td>";
                        tbody += "<td style='width:7%'>" + val.InsuranceCompanyName + "</td>";
                        tbody += "<td style='width:7%'>" + val.BillDate + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.NetPayable + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.ApprovalAmount + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.AppliedApproval + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.OutStanding + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Receivable + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.TDS + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Payment + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.BadDebt + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Received + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblCheckBillStatus tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ExcelCheckBillStatus() {
    var url = config.baseUrl + "/api/IPDBilling/Audit_IPOPBillQuerise";
    var objBO = {};
    objBO.IPOPNo = $("#txtIPDNo").val();
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = $('#ddlReportType option:selected').val();
    objBO.Prm2 = '-';
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.Logic = logic;
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "CheckBillStatus" + ".xlsx");
}
function GetDataAllIPDNOWise() {
    logic = 'CheckBillStatus:ByIPDNo';
    $('#tblCheckBillStatus tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/Audit_IPOPBillQuerise";
    var objBO = {};
    objBO.IPOPNo = $("#txtIPDNo").val();
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.PanelId = '-'
    objBO.Logic = 'CheckBillStatus:ByIPDNo';
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
                        if (temp != val.TPAPanelName) {
                            tbody += "<tr style='background:#CCC;'>";
                            tbody += "<td colspan='15' style='font-size:13px;'><b>" + val.TPAPanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.TPAPanelName
                        }
                        if (val.IsMisMatch == "Y") {
                            tbody += "<tr style='background:#eda1a1'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td style='width:7%'>" + val.IPDNo + "</td>";
                        tbody += "<td style='width:7%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.IsCredit + "</td>";
                        tbody += "<td style='width:7%'>" + val.InsuranceCompanyName + "</td>";
                        tbody += "<td style='width:7%'>" + val.BillDate + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.NetPayable + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.ApprovalAmount + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.AppliedApproval + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.OutStanding + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Receivable + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.TDS + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Payment + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.BadDebt + "</td>";
                        tbody += "<td style='width:7%;text-align:center'>" + val.Received + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblCheckBillStatus tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

