var _ActivePageName;
var _IPDNo;
var _PatientName;
$(document).ready(function () {
    $('select').select2();
    PanelList();
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
});
function PanelList() {
    $('#ddlPanel').empty().append($('<option></option>').val('ALL').html('ALL'));
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
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlPanel').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table2, function (key, val) {
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
function AdmittedPatientList(logic) {
    $('#tblServiceRegister tbody').empty();
    var url = config.baseUrl + "/api/Service/OPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.OpdNo = $('#txtIPDNo').val();
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = "";
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.OPD_ClaimNo != '')
                            tbody += "<tr style='background:#a4df9d'>";
                        else if (val.isCancelled == 'Y')
                            tbody += "<tr style='background:#ffbdbd'>";
                        else
                            tbody += "<tr>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.BillNo + "</td>";
                        tbody += "<td>";
                        tbody += "<div style='display:flex'>";
                        tbody += "<input type='text' value='" + val.OPD_ClaimNo + "' class='form-control' placeholder='Claim No' style='border-radius: 0;height: 24px;border-color: #06a08b;'/>";
                        if (val.isCancelled == 'N')
                            tbody += "<button data-logic='UpdateOPDClaimNo' onclick=UpdateOPDClaimNo(this) class='btn btn-primary btn-xs'>Update</button>";
                        else
                            tbody += "-";

                        tbody += "</div>";
                        tbody += "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td>" + val.BillDate + "</td>";
                        tbody += "<td class='text-right'>" + val.TotalAmount + "</td>";
                        tbody += "<td class='text-right'>" + val.TotalDiscount + "</td>";
                        tbody += "<td class='text-right'>" + val.NetPayable + "</td>";
                        tbody += "<td class='text-right'>" + val.Received + "</td>";
                        tbody += "<td class='text-right'>" + val.Balance + "</td>";
                        if (val.isCancelled == 'N')
                            tbody += "<td><button data-logic='CancelBill' onclick=UpdateOPDClaimNo(this) class='btn btn-danger btn-xs'>Cancel</button></td>";
                        else
                            tbody += "<td>-</td>";
                        tbody += "</tr>";
                    });
                    $('#tblServiceRegister tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateOPDClaimNo(elem) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalEntry";
        var objBO = [];
        objBO.push({
            'AutoId': 0,
            'IPDNo': $(elem).closest('tr').find('td:eq(1)').text(),
            'InsuranceId': "-",
            'ClaimNo': "-",
            'ApprovalDate': '1900/01/01',
            'ApprovalType': "-",
            'ApprovalAmount': 0,
            'CoPayAmount': 0,
            'Discount': 0,
            'Remark': "-",
            'doc_path': $(elem).closest('tr').find('input:text').val(),
            'login_id': Active.userId,
            'Logic': $(elem).data('logic')
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $(elem).closest('tr').css('background', '#a4df9d"')
                }
                else
                    alert(data);
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}