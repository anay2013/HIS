
$(document).ready(function () {
    $('#tblResetLab tbody').on('click', '.Resetbutton', function () {
        var itemid = $(this).closest('tr').find('td:eq(0)').text();
        ResetRowsWiseData(itemid);
    });

});
function VisitNoBySearch() {
    $('#tblResetLab tbody').empty();
    var url = config.baseUrl + "/api/sample/Lab_SampleCollectionQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.VisitNo = $("#txtVisitNo").val();
    objBO.BarcodeNo = '-';
    objBO.SampleCode = '-';
    objBO.TestCode = '-';
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.Prm1 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'InfoForTestReset';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        $("#_txtVisitNo").text(data.ResultSet.Table[0].visitNo);
                        $("#_txtPatientName").text(data.ResultSet.Table[0].patient_name);
                        $("#_txtGender").text(data.ResultSet.Table[0].gender);
                        $("#_txtAge").text(data.ResultSet.Table[0].Age);

                    });
                }
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table1, function (key, val) {

                        if (val.IsCancelled == '1') {
                            tbody += "<tr style='background:#efc0c0'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        tbody += "<td style='width:10%;'>" + val.ItemId + "</td>";
                        tbody += "<td style='text-align:left;width:20%;'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:10%;'>" + val.sample_collect_date + "</td>";
                        tbody += "<td style='width:10%;'>" + val.SampleDistributedDate + "</td>";
                        tbody += "<td style='width:10%;'>" + val.dispatch_date + "</td>";
                        tbody += "<td style='width:10%;'>" + val.LabReceivedDate + "</td>";
                        tbody += "<td style='width:10%;'>" + val.ApprovedDate + "</td>";
                        tbody += "<td style='width:10%;'>" + val.IsSampleRequired + "</td>";
                        tbody += '<td style="width:10%;"><button class="btn-success Resetbutton" style="border:none;height:25px;width:60%">Reset</button></td>';
                        tbody += "</tr>";

                    });
                    $('#tblResetLab tbody').append(tbody);
                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ResetRowsWiseData(Itemid) {
    var url = config.baseUrl + "/api/sample/Lab_TestReset";
    var objBO = {};
    objBO.VisitNo = $("#_txtVisitNo").text();
    objBO.ItemId = Itemid;
    objBO.login_id = Active.userId;
    objBO.Login = '';
    objBO.Logic = 'Reset';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
            }
            else {
                alert(data);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ResetALL() {
    var isConfirmed = confirm('Are you sure you want to Reset All the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/sample/Lab_TestReset";
        var objBO = {};
        objBO.VisitNo = $("#_txtVisitNo").text();
        objBO.ItemId = 'ALL';
        objBO.login_id = Active.userId;
        objBO.Login = '';
        objBO.Logic = 'Reset';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
    else {
        alert('Data Reset All canceled.');

    }
}