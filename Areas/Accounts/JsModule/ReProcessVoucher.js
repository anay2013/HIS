$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');

    $('#tblReport tbody').on('click', '.Reprocess', function () {
        debugger
        var unitid = $(this).closest('tr').find('td:eq(1)').text();
        var Date = $(this).closest('tr').find('td:eq(2)').text();
        UpdateReProcess(unitid, Date);
    });
});

function GetReProcessList() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Account/SearchLedger";
    var objBO = {};
    objBO.UnitId = Active.unitId;
    objBO.LoginId = Active.userId;
    objBO.prm1 = '-';
    objBO.From = $('#txtSearchFrom').val();;
    objBO.To = $('#txtSearchTo').val();
    objBO.Division = "-";
    objBO.Logic = "DateBetweenRange";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var count = 0;
            if (Object.keys(data.ResultSet.Table).length > 0) {
                $.each(data.ResultSet.Table, function (key, val) {
                    count++;
                    tbody += "<tr>";
                    tbody += "<td style='width:15%;text-align: center'>" + count + "</td>";
                    tbody += "<td hidden>" + val.HospId + "</td>";
                    tbody += "<td style='width:60%;'>" + val.Date + "</td>";
                    tbody += '<td style="width:25%; text-align:center"><button class="btn-success Reprocess" style="border:none;height:20px;margin-bottom: 3px;">Reprocess</button></td>';
                    tbody += "</tr>";
                });
                $('#tblReport tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}



function UpdateReProcess(UnitId, Date) {
    var isconfirmed = confirm('are you sure you want to reprocess the data?');
    if (isconfirmed) {
        var url = config.baseUrl + "/api/Account/AC_GenerateAllVouchers";
        var objbo = {};
        objbo.UnitId = UnitId;
        objbo.From = Date;
        objbo.To = Date;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objbo),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('success')) {
                    alert(data);
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('server error...!');
            }
        });
    }
    else {
        alert('data update canceled.');
    }

}