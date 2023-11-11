
$(document).ready(function () {
    FloorAndPanelList();
    $('select').select2();
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');  
});
function FloorAndPanelList() {
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
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlInsuranceList').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlInsuranceList').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ApprovalAuditInfo() {
    $('#tblApprovalAuditInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = "";
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'ApprovalAuditInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data)
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    var temp = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#b9e0ff'>";
                            tbody += "<td colspan='10'>" + val.PanelName + "</td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        if (val.ApprovalAmount != val.Receivable)
                            tbody += "<tr style='background:#ffa8a8'>";                     
                        else
                            tbody += "<tr>";                                         
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";               
                        tbody += "<td>" + val.ApprovalType + "</td>";
                        tbody += "<td>" + val.ApprovalDate + "</td>";
                        tbody += "<td>" + val.ClaimNo + "</td>";
                        tbody += "<td class='text-right'>" + val.ApprovalAmount + "</td>";
                        tbody += "<td class='text-right'>" + val.CoPayAmount + "</td>";
                        tbody += "<td class='text-right'>" + val.Discount + "</td>";                                                                                   
                        tbody += "<td class='text-right'>" + val.Receivable + "</td>";                                            
                        if (val.doc_path != 'N/A')
                            tbody += "<td><button onclick=viewDoc('" + val.doc_path + "') class='btn btn-success btn-xs'><i class='fa fa-eye'>&nbsp;</i>View</button></td>";
                        else
                            tbody += "<td>-</td>";
                        tbody += "</tr>";
                    });
                    $('#tblApprovalAuditInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function viewDoc(link) {
    window.open(link, '_blank');
}