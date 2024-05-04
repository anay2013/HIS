
$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Bill MisMatch').show();
    GetDataBillMisMatch();

    $('#tblBillMisMatch tbody').on('click', '.billmismatchDetails', function (event) {
        event.preventDefault();
        debugger;
        var IPDno = $(this).closest('tr').find('td:eq(0)').text().trim();
        DetailsOpens(IPDno);
    });
});

function DetailsOpens(IPDno) {
    $('#_BillMismatchDetails').modal('show');
    GetDataBillMisMatchDetails(IPDno)
}
function GetDataBillMisMatch()
{
    $('#tblBillMisMatch tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/Audit_IPOPBillQuerise";
    var objBO = {};
    objBO.IPOPNo = _IPDNo
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.PanelId = '-';
    objBO.Logic = 'BillAndItemWiseMismatch';
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
                        if (val.ItemAndBillMismatch == "Y") {
                            tbody += "<tr style='background:#eda1a175'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                       // tbody += "<td style='width:10%'>" + val.IPDNo + "</td>";
                        tbody += "<td style='width:10%;'><a href='#' class='billmismatchDetails' style='color: blue;'>" + val.IPDNo + "</a></td>";
                        tbody += "<td style='width:15%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:15%;'>" + val.PanelName + "</td>";
                        tbody += "<td style='width:8%;text-align:center'>" + val.IsCredit + "</td>";
                        tbody += "<td style='width:8%;text-align:center'>" + val.IsBillClosed + "</td>";
                        tbody += "<td style='width:8%;text-align:center'>" + val.IsBillLocked + "</td>";
                        tbody += "<td style='width:8%;text-align:center'>" + val.ItemWiseAmt + "</td>";
                        tbody += "<td style='width:8%;text-align:center'>" + val.BilledAmount + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.ItemAndBillMismatch + "</td>";
                        tbody += "<td style='width:10%;'>" + val.BillDate + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblBillMisMatch tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function GetDataBillMisMatchDetails(IPDno) {
    debugger
    $('#tblBillMismatchDetails tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/Audit_IPOPBillQuerise";
    var objBO = {};
    objBO.IPOPNo = IPDno;
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.PanelId = '-';
    objBO.Logic = 'BillAndItemWiseMismatch:Details';
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
                        tbody += "<td style='width:7%;'>" + val.IpOpType + "</td>";
                        tbody += "<td style='width:9%;'>" + val.IPOPNo + "</td>";
                        tbody += "<td style='width:8%;'>" + val.RequestDate + "</td>";
                        tbody += "<td style='width:15%;'>" + val.RequestTo + "</td>";
                        tbody += "<td style='width:8%;'>" + val.AppStatus + "</td>";
                        tbody += "<td style='width:10%'>" + val.ApprovalType + "</td>";
                        tbody += "<td style='width:8%'>" + val.ApproveDate + "</td>";
                        tbody += "<td style='width:8%'>" + val.CancelDate + "</td>";
                        tbody += "<td style='width:12%'>" + val.login_id + "</td>";
                        tbody += "<td style='width:15%'>" + val.Remark + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblBillMismatchDetails tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}



