$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')  
    LockPrvDate()
});
function LockPrvDate() {
    $("#txtFrom,#txtTo").each(function () {
        $(this).attr("min", _AdmitDateServer.split('T')[0]);
        $(this).attr("max", sessionStorage.getItem('ServerTodayDate').split('T')[0]);
    });
}

function PatientTestInfo() {
    $('#tblTestInfo tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = _IPDNo;
    objBO.BarccodeNo = '-';
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = 0;
    objBO.TestCode = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'PatientTestInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            if (Object.keys(data.ResultSet).length > 0) {
                var tbody = "";
                var count =0;
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.testCode + "</td>";
                        tbody += "<td>" + val.TestName + "</td>";
                        tbody += "<td>" + val.t_type + "</td>";
                        tbody += "<td class='text-right'>" + val.tCount + "</td>";
                        tbody += "<td><button onclick=PatientDeltaReport(" + val.testCode+") class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "<tr>";
                    });
                    $("#tblTestInfo tbody").append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientDeltaReport(testCode) {
    $('#deltaReport').empty();
    $('#tblReportSummary tbody').empty();
    var url = config.baseUrl + "/api/sample/LabReporting_Queries";
    var objBO = {};
    objBO.LabCode = Active.HospId;
    objBO.IpOpType = '-';
    objBO.ReportStatus = '-';
    objBO.VisitNo = _IPDNo;
    objBO.BarccodeNo = '-';
    objBO.SubCat = '-';
    objBO.TestCategory = '-';
    objBO.AutoTestId = '-';
    objBO.TestCode = testCode;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'PatientDeltaReportTestWise';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = '';
                    var temp = '';
                    var flag = '';
                    var counter = 0;
                    var count = 0;
                    var graph = [];
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.ab_flag == 'L')
                            flag = 'lowReading';

                        if (val.ab_flag == 'H')
                            flag = 'highReading';

                        if (val.ab_flag == 'N')
                            flag = 'normalReading';

                        if (temp != val.ObservationId) {
                            count++;
                            if (count > 1) {
                                tbody += "<div class='divChartTemp'><canvas id='chartDelta'></canvas></div>";
                            }
                            //tbody += "<label class='labelGroup'>" + val.ObservationName + " Report <b class='pull-right'>Ref. Range : " + val.ref_range + "</b></label>";
                            temp = val.ObservationId;
                        }
                        //tbody += "<div class='reportRound " + flag + "'>" + val.read_1 + "<hr />" + val.result_date + "</div>";
                    });
                    tbody += "<div class='divChartTemp'><canvas id='chartDelta'></canvas></div>";
                    $('#deltaReport').append(tbody);

                    var temp1 = '';
                    var tbody1 = '';
                    //table bind
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp1 != val.ObservationId) {
                            tbody1 += "<tr class='group'>";
                            tbody1 += "<td colspan='6'>" + val.ObservationName + "</td>";
                            tbody1 += "</tr>";
                            temp1 = val.ObservationId;
                        }
                        count++;
                        tbody1 += "<tr>";
                        tbody1 += "<td>" + val.rowNo + "</td>";
                        tbody1 += "<td>" + val.RegDate.split('T')[0] + "</td>";
                        tbody1 += "<td>" + val.read_1 + "</td>";
                        tbody1 += "<td>" + val.min_value + "</td>";
                        tbody1 += "<td>" + val.max_value + "</td>";
                        tbody1 += "<td>" + val.ref_range + "</td>";
                        tbody1 += "</tr>";
                    });                    
                    $('#tblReportSummary tbody').append(tbody1);
                }
            }
        },
        complete: function (response) {
            var temp = "";
            var counter = 0;
            var count = 0;
            var graph = [];
            $.each(response.responseJSON.ResultSet.Table, function (key, val) {
                if (temp != val.ObservationId) {
                    count++;
                    if (count > 1) {
                        PopulateChart(graph, counter);
                        counter++;
                        graph = [];
                    }
                    temp = val.ObservationId;
                }
                graph.push(response.responseJSON.ResultSet.Table[key])
            });
            PopulateChart(graph, count - 1);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PopulateChart(response, elem) {
    ctxL = $('#deltaReport').find('.divChartTemp').eq(elem).find('#chartDelta')[0].getContext('2d');
    var xValues = [];
    var TempArr = [];
    var minValue = 0;
    var maxValue = 0;
    for (var i in response) {
        xValues.push(response[i].result_date);
        TempArr.push(response[i].read_1);
        minValue = response[i].min_value
        maxValue = response[i].max_value
    }
    var config = {
        type: 'line',
        data: {
            labels: xValues,
            datasets: [
                {
                    label: "Reading",
                    data: TempArr,
                    backgroundColor: ['rgba(0, 137, 132, .2)',], borderColor: ['rgba(0, 10, 130, .7)',],
                    borderWidth: 2
                }
            ]
        },
        options: {
            responsive: true,
            scales: {
                yAxes: [{
                    ticks: {
                        min: minValue / 2,
                        max: maxValue
                    }
                }]
            }
        }
    };
    var myLineChart = new Chart(ctxL, config);
}