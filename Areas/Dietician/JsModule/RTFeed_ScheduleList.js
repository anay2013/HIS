
var servingDateTime = "";
$(document).ready(function () {
    FillCurrentDate('txtDate')
    $('#btnFreezed').prop('disabled', true);
    $('#btnprint').prop('disabled', true);

});
function GetAllRTFeedList() {
    $('#formList').empty();
    var url = config.baseUrl + "/api/Dietician/Diet_RTFeedQueries";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.DietId = '-';
    objBO.StartDateTime = '1900/01/01 00:00:00';
    objBO.FinishDateTime = '1900/01/01 00:00:00';
    objBO.Hourly = '-';
    objBO.from = $("#txtDate").val();
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetRTDietsList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            var html = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        html += '<div class="col-md-12 formInfo">';
                        html += '<label class="DetailsImg"><img src="/Content/logo/food.png" style="height:35px; margin-left: 10px;" alt="Food Logo"></label>';
                        html += '<label class="formName">' + val.ServingDateTime + '</label>';
                        html += ' <button id="btnView"  class="btn btn-success btn-sm" style="float:right;margin-top:4px;width: 75px;height: 35px;font-size: 12px; margin-right: 2px;" data-date="' + val.ServingDateTimeValue + '"  onclick=ViewData(this)><i class="fa fa-eye"></i>&nbsp;View</button>';

                        html += '</div>';
                        count++;
                    })
                    $('#formList').append(html);
                }
            }
        },
        error: function (error) {
            console.error("Error fetching data: " + error);
        }
    });
}
function ViewData(element) {
    $('#btnFreezed').prop('disabled', false);
    $('#tblDietdetails tbody').empty();
    servingDateTime = $(element).data('date');
    var url = config.baseUrl + "/api/Dietician/Diet_RTFeedQueries";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.DietId = '-';
    objBO.StartDateTime = servingDateTime;
    objBO.FinishDateTime = '1900/01/01 00:00:00';
    objBO.Hourly = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetRTDietsListDetails';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            var tbody = "";
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IsFreezed == "Y") {
                            tbody += "<tr style='background-color:#bdedbc'>";
                            $('#btnFreezed').prop('disabled', true);
                            $('#btnprint').prop('disabled', false);
                        }
                        else {
                            $('#btnprint').prop('disabled', true);
                            $('#btnFreezed').prop('disabled', false);
                            tbody += "<tr>";
                        }

                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";

                        tbody += "<td hidden>" + val.DietId + "</td>";
                        tbody += "<td>" + val.DietName + "</td>";
                        tbody += "<td hidden>" + val.ItemId + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "<td>" + val.IsLocked + "</td>";
                        tbody += "<td>" + val.IsFreezed + "</td>";
                        tbody += "<tr>";
                    })
                    $('#tblDietdetails tbody').append(tbody);
                }
            }
        },
        error: function (error) {
            console.error("Error fetching data: " + error);
        }
    });
}
function PrintData() {
    var url = "../Print/RTFeedSchedule?Date=" + servingDateTime;
    window.open(url, '_blank');
}
function UpdateFreezedData() {
    var url = config.baseUrl + "/api/Dietician/diet_RTFeedSchduleLockOrFreeze";
    var objBO = {};
    objBO.ipdno = '-';
    objBO.Prm1 = "-";
    objBO.Prm2 = '-';
    objBO.From = '1999-01-01 00:00';
    objBO.to = '1999-01-01 00:00';
    objBO.servingDateTime = servingDateTime;
    objBO.login_id = Active.userId;
    objBO.Logic = 'FreezeRTfeed';
    $.ajax({
        method: "POST",
        url: url,
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(objBO),
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                $('#btnprint').prop('disabled', false);
                $('#btnFreezed').prop('disabled', true);

            }
            else {
                alert(data);
            }
        }
    });

}
