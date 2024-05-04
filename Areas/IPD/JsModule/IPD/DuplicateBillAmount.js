$(document).ready(function () {
    GetDataAllDuplicate();
});

function GetDataAllDuplicate() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = "1999-01-01";
    objBO.to = "1999-01-01";
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PharmacyDuplicateBill';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var temp = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td hidden>" + val.auto_id + "</td>";
                        tbody += "<td style='width:10%'>" + val.IPDNo + "</td>";
                        tbody += "<td style='width:25%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:15%'>" + val.TnxId + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.panel_rate + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.discount + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.adl_discount + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.amount + "</td>";
                        if (temp != val.TnxId) {
                            tbody += "<td style='width:10%;text-align:center'><button id='btnCancel' onclick=CancelDuplicateItem(" + val.auto_id + ") class='btn btn-danger btn-xs'><i class='fa fa-close'>&nbsp;</i>Cancel</button></td>";
                            temp = val.TnxId;
                        }
                        else {
                            tbody += "<td style='width:10%;height:28px'></td>";
                        }
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function CancelDuplicateItem(autoId) {
    if (confirm('Are you sure to Cancel')) {
        var url = config.baseUrl + "/api/IPDBilling/IPD_BillingCategoryUpdate";
        var objBO = {};
        objBO.AutoId = autoId;
        objBO.IPDNo = '-';
        objBO.RoomBillingCategory = '-';
        objBO.from = "1999-01-01";
        objBO.to = "1999-01-01";
        objBO.Prm1 = "";
        objBO.login_id = Active.userId;
        objBO.Logic = "CancelPharmacyBill";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
    else {

        alert("Cancelled Data");
    }
}