var temp = ''
var totalamountTable = 0;
var totalamountTable1 = 0;
$(document).ready(function () {
    CloseSidebar();
    FillCurrentDate('txtfrom');
    FillCurrentDate('txtTo');

});

function DownloadExcelHOTODashboard() {
    var logic = 'HOTODashBoard';
    $('#tblLedgerBy tbody').empty();
    $('#tblphotoDeshboard tbody').empty();
    var url = config.baseUrl + "/api/Account/HIS_HOTO_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.shiftID = '-';
    objBO.from = $("#txtfrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = "HotoRawData";
    objBO.OutPutType = "Excel";
    Global_DownloadExcel(url, objBO, logic + ".xlsx");
}
function GetDataAllBinds(logic) {
    $('#tblLedgerBy tbody').empty();
    $('#tblphotoDeshboard tbody').empty();
    var url = config.baseUrl + "/api/Account/HIS_HOTO_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.shiftID = '-';
    objBO.from = $("#txtfrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = logic;
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
                        tbody += "<td style='width:60%;text-align:left'>" + val.ledgerName + "</td>";
                        tbody += "<td style='width:40%;text-align:center'>" + val.Amount + "</td>";
                        tbody += "</tr>";
                        totalamountTable += parseFloat(val.Amount);

                    });
                    $("#totalamount").text(totalamountTable);
                    $('#tblLedgerBy tbody').append(tbody);
                    PopulateChartTemperatures(data.ResultSet.Table);

                    if (Object.keys(data.ResultSet.Table1).length) {
                        tbody = "";
                        $.each(data.ResultSet.Table1, function (key, val) {
                            if (temp != val.shiftID) {
                                tbody += "<tr style='background:#CCC'>";
                                tbody += "<td colspan='9' style='font-size:13px;'><b>" + val.shiftID + "</b></td>";
                                tbody += "</tr>";
                                temp = val.shiftID
                            }
                            else {

                            }
                            if (val.Balance == "0") {
                                tbody += "<tr style='background-color:#86d789'>";
                            }
                            else {

                            }
                            //tbody += "<tr>";
                            tbody += "<td style='width:29%;text-align:left'>" + val.LedgerName + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.Work + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.NeedToBeReceive + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.Received + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.Trf + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.TrfNotRcvd + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.TrfRcvd + "</td>";
                            tbody += "<td style='width:9%;text-align:center'>" + val.Balance + "</td>";
                            tbody += "</tr>";
                            totalamountTable1 += parseFloat(val.Balance);

                        });
                        $("#totalamount1").text(totalamountTable1);
                        $('#tblphotoDeshboard tbody').append(tbody);
                    }
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PopulateChartTemperatures(response) {
    var xValues = [];
    var yValues = [];
    var barColors = [
        "#b91d47",
        "#00aba9",
        "#2b5797",
        "#1e7145",
        "#1e7145",
        "#A943F4",
        "#F44369",
        "#F4436E",
        "#F47B43",
        "#1e7145"

    ];
    $.each(response, function (key, val) {
        xValues.push(val.ledgerName);
        yValues.push(val.Amount);

    });
    new Chart("chartHoTo", {
        type: "pie",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            legend: {
                display: false,
            }
        }
    });
}
