var temp = "";

$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Cancel Report').show();    
    CancelReportList();
    $('#tblCancelReport tbody').on('mouseover', '.entryBy', function () {
        var entryBy = '<b>Entry By : </b>' + $(this).data('entryby');
        var doctorname = '<b>Doctor Name : </b>' + $(this).data('doctorname');
        var RoomBillingCategory = '<b>Room Billing Category : </b>' + $(this).data('roombillingcategory');
        var Remark = '<b>Remark: </b>' + $(this).data('remark');
        var TestApproved = '<b>Test Approved: </b>' + $(this).data('testapproved');
        var RateListName = '<b>Rate List Name: </b>' + $(this).data('ratelistname');
       
        $(this).siblings('span').html(entryBy + '<br>' + doctorname + '<br>' + RoomBillingCategory + '<br>' + Remark + '<br>' + TestApproved + '<br>' + RateListName).show('fast');
    }).on('mouseleave', '.entryBy', function () {
        $(this).siblings('span').empty().hide('fast');
    });
});
function CancelReportList() {
    $('#tblCancelReport tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'CancelItems';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.CatName) {
                            tbody += "<tr style='background:#CCC;'>";
                            tbody += "<td colspan='11' style='font-size:12px;'><b>" + val.CatName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.CatName
                        }
                        if (val.CancelDate) {
                            tbody += "<tr style='background:#f39b9b;'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td style='width:11%'>" + val.TnxId + "</td>";
                        tbody += "<td style='width:12%'>" + val.tnxDate1 + "</td>"; 
                        tbody += "<td style='width:15%'>" + val.ItemName + "<i class='fa fa-user-circle text-danger entryBy pull-right' data-entryby='" + val.EntryBy + "' data-doctorname='" + val.doctorName + "' data-roombillingcategory='" + val.RoomBillingCategory + "' data-remark='" + val.Remark + "' data-testapproved='" + val.IsTestApproved + "' data-ratelistname='" + val.RateListName + "'></i><span class='entryByName'></span></td>";  
                        tbody += "<td style='width:10%;text-align:center'>" + val.Qty + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.Amount + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.Tax + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.Net + "</td>";
                        tbody += "<td style='width:10%;'>" + val.CancelBy + "</td>";
                        tbody += "<td style='width:12%'>" + val.CancelDate + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblCancelReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}