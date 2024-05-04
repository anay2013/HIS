
var logic = ''
var temp = ''
$(document).ready(function () {
    CloseSidebar();
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');
    GetAllDataBindPanel();
    FloorAndPanelList();
});
function FloorAndPanelList() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'FloorAndPanelList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlFloor').empty().append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlFloor').append($('<option></option>').val(val.FloorName).html(val.FloorName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DownloadExcelClosing() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = $('#ddlReportLogic option:selected').val();
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "BillRegister" + ".xlsx");
}
function GetDataAllClosingPending() {
    logic = 'ClosingPending:BetweenDate';
    $('#tblClosingPending tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = $('#ddlReportLogic option:selected').val();
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
                            tbody += "<tr style='background:#CCC'>";
                            tbody += "<td colspan='15' style='font-size:13px;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        if (val.IsErrorInCredit == "Y")
                            tbody += "<tr style='background:#f59696'>";
                        else {
                            if (val.BillClosedDate != null) {
                                tbody += "<tr style='background:#2cd52e'>";
                            }
                            else {
                                tbody += "<tr>";
                            }
                        }
                        //tbody += "<tr>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.IsCredit + "</td>";
                        tbody += "<td>" + val.TotalAmount + "</td>";
                        tbody += "<td>" + val.Discount + "</td>";
                        tbody += "<td>" + val.NetAmount + "</td>";
                        tbody += "<td>" + val.Tax + "</td>";
                        tbody += "<td>" + val.NetPayable + "</td>";
                        tbody += "<td>" + val.ApprovalAmount + "</td>";
                        tbody += "<td>" + val.Advance + "</td>";
                        tbody += "<td>" + val.Balance + "</td>";
                        tbody += "<td>" + val.BillClosedDate + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblClosingPending tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataAllIPDNO() {
    logic = 'ClosingPending:ByIPDNo';
    $('#tblClosingPending tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
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

                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#CCC;'>";
                            tbody += "<td colspan='15' style='font-size:13px;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        if (val.BillClosedDate != null) {
                            tbody += "<tr style='background:#2cd52e'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        //tbody += "<tr>";
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
                        tbody += "</tr>";
                    });
                    $('#tblClosingPending tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function GetAllDataBindPanel() {
    logic = 'LoadPanel';
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

