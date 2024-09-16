$(document).ready(function () {

});
function GetDataUnDischargeList() {
    $('#tblReport tbody').empty();
    $('#tblReportInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.IPDNo = $("#txtIPDNo").val();
    objBO.EntrySource = '';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'UnDischarge:List';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = '';
            var tbody1 = '';
            if (data.ResultSet && data.ResultSet.Table) {
                $.each(data.ResultSet.Table, function (key, val) {
                    tbody += "<tr>";
                    tbody += "<td>" + val.ipdno + "</td>";
                    tbody += "<td>" + val.roomname + "</td>";
                    tbody += "<td>" + val.patient_name + "</td>";
                    tbody += "<td>" + val.AdmitDate + "</td>";
                    tbody += "<td>" + val.DischargeDate + "</td>";
                    tbody += "<td>" + val.roomStatus + "</td>";

                    if (val.roomStatus == "Booked") {
                        $('#btnUndischarge').prop('disabled', true);
                    }
                    else {
                        $('#btnUndischarge').prop('disabled', false);
                    }
                    tbody += "</tr>";
                });
                $('#tblReport tbody').append(tbody);
            }
            if (data.ResultSet && data.ResultSet.Table1) {
                $.each(data.ResultSet.Table1, function (key, val) {
                    tbody1 += "<tr>";
                    tbody1 += "<td>" + val.BillNo + "</td>";
                    tbody1 += "<td>" + val.BillDate + "</td>";
                    tbody1 += "<td style='text-align:center'>" + val.TotalAmount + "</td>";
                    tbody1 += "<td>" + val.IsBillClosed + "</td>";
                    tbody1 += "</tr>";
                });
                $('#tblReportInfo tbody').append(tbody1);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function SaveUnDischarge() {
    if (confirm('Are you sure to UnDischarged?')) {
        var url = config.baseUrl + "/api/IPDNursingService/IPD_GenerateBill";
        var objBO = {};
        objBO.IPDNo = $("#txtIPDNo").val();
        objBO.BillingType = "-";
        objBO.Remark = "";
        objBO.login_id = Active.userId;
        objBO.Logic = "UnDischarged";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                $('#btnUndischarge').prop('disabled', true);
                alert(data);

            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}