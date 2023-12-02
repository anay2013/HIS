
$(document).ready(function () {
    $("select").select2();
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    CloseSidebar();
});
function BugReport() {
    $('#tblBugReport tbody').empty();
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.prm_3 = '-';
    objBO.prm_4 = '-';
    objBO.loginId = Active.userId;
    objBO.Logic = "BugReport";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var GrossAmount = 0, Discount = 0, NetAmount = 0, Received = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        GrossAmount += val.GrossAmount, Discount += val.Discount, NetAmount += val.NetAmount, Received += val.Received;
                        var IsFault = (val.IsFault == 'Y') ? '#ffa6a6' : '-';
                        tbody += '<tr style="background:' + IsFault + '">';
                        tbody += '<td>' + val.ipop_no + '</td>';
                        tbody += '<td>' + val.TnxId + '</td>';
                        tbody += '<td>' + val.patient_name + '</td>';
                        tbody += '<td>' + val.PanelName + '</td>';
                        tbody += '<td>' + val.tnxDate + '</td>';
                        tbody += '<td class="text-right">' + val.GrossAmount + '</td>';
                        tbody += '<td class="text-right">' + val.Discount + '</td>';
                        tbody += '<td class="text-right">' + val.NetAmount + '</td>';
                        tbody += '<td class="text-right">' + val.Received + '</td>';
                        tbody += '<td>' + val.emp_name + '</td>';
                        tbody += '</tr>';
                    });
                    tbody += '<tr style="background:#ddd;font-size:14px;">';
                    tbody += '<td colspan="5"><b>Total</b></td>';
                    tbody += '<td class="text-right"><b>' + GrossAmount + '</b></td>';
                    tbody += '<td class="text-right"><b>' + Discount + '</b></td>';
                    tbody += '<td class="text-right"><b>' + NetAmount + '</b></td>';
                    tbody += '<td class="text-right"><b>' + Received + '</b></td>';
                    tbody += '<td class="text-right">-</td>';
                    tbody += '</tr>';
                    $('#tblBugReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
