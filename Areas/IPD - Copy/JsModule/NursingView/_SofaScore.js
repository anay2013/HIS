var entrydate = '';
$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('SOFA SCORE').show();
    GetSofaScore();
    GetSofaScoreList();

});
function GetSofaScore() {
    var objBO = {};
    var url = config.baseUrl + "/api/IPDNursing/SOFA_ScoreQueries";
    objBO.ObservationId = '-';
    objBO.Sofasystem = '-';
    objBO.ObservationName = '-';
    objBO.Value = '-';
    objBO.from = '1900-01-01';
    objBO.to = '1900-01-01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'GetObservation';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var counter = 1;
            $.each(data.ResultSet.Table, function (key, val) {
                tbody += "<tr>"
                tbody += "<td>" + val.ObservationName + "</td>";
                tbody += "<td style='display: none;'>" + val.Score + "</td>";
                tbody += "<td style='display: none;'>" + val.ObservationId + "</td>";
                tbody += "<td style='display: none;'>" + val.valueType + "</td>";
                tbody += "<td style='display: none;'>" + val.TextValue + "</td>";
                if (val.valueType == "Text") {
                    var TextValue = val.TextValue;
                    if (TextValue.length > 1) {
                        tbody += "<td><select id='ddlRange" + counter + "' data-type='Text' onchange=SofaRangeValues(this) style='width:187px' class='form-control'>";
                        tbody += "<option value='' selected> Select</option>";
                        var arr = TextValue.split('|')
                        for (var i = 0; i < arr.length; i++) {
                            tbody += "<option value='" + arr[i] + "'>" + arr[i] + "</option>";
                        }
                        tbody += "</select></td>";
                    }
                }
                else {
                    tbody += '<td><input id="txt' + counter + '" onblur=SofaRangeValues(this) data-type="Numeric" type="text" class="form-control"/></td>';
                }
                tbody += "<td style='width:10%;text-align:center'><button style='border-radius: 50%;' class='btn btn-warning btn-xs' id='txtScore'></button></td>";
                tbody += "</tr>"
                counter++;
            });
            $('#tblSofaScore tbody').append(tbody);

        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function SofaRangeValues(element) {
    var objBO = {};
    var observationName = $(element).closest('tr').find('td:eq(0)').text();
    var value = ($(element).data('type') == 'Text') ? $('#' + $(element).attr('id') + ' option:selected').val() : $('#' + $(element).attr('id')).val()
    if (value.length > 0) {
        var url = config.baseUrl + "/api/IPDNursing/SOFA_ScoreQueries";
        objBO.ObservationId = '-';
        objBO.Sofasystem = '-',
            objBO.ObservationName = observationName;
        objBO.Value = value
        objBO.from = '1900-01-01';
        objBO.to = '1900-01-01';
        objBO.Prm1 = $(element).data('type');
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'GetScore';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                console.log(data);
                if (Object.keys(data.ResultSet).length) {
                    if (Object.keys(data.ResultSet.Table).length) {
                        $.each(data.ResultSet.Table, function (key, val) {
                            $(element).closest('tr').find("#txtScore").text(val.Score);
                        });
                    }
                }
            },
            error: function (response) {
                console.error("Error:", response);
                alert('Server Error...!');
            }
        });
    }
}
function InsertSofaCore() {
    var url = config.baseUrl + "/api/IPDNursing/SOFA_InsertUpdateScore";
    var listSofaValues = [];
    $('#tblSofaScore tbody tr').each(function () {
        var ObservationId = parseFloat($(this).find('td:eq(2)').text());
        var ValueType = $(this).find('td:eq(3)').text();
        var Value = '';
        if (ValueType == "Text") {
            Value = $(this).find('select').val();
        } else {
            Value = $(this).find('input[type="text"]').val();
        }
        var score = parseFloat($(this).find("#txtScore").text());

        // Check if Value is not empty
        if (Value.trim() !== "") {
            listSofaValues.push({
                'IPDNo': _IPDNo,
                'SofaType': 'Quick Sofa',
                'ObservationId': ObservationId,
                'ValueType': ValueType,
                'Value': Value,
                'Score': score,
                'Prm1': '-',
                'Prm2': '-',
                'LoginId': Active.userId,
                'Logic': 'Insert:SofaScore'
            });
        }
    });
    if (listSofaValues.length > 0) {
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(listSofaValues),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                console.log("Insertion successful:", data);
                GetSofaScoreList();
                Clear();
            },
            error: function (response) {
                console.error("Error:", response);
                alert('Server Error...!');
            }
        });
    } else {
        alert("Please Fill The values to insert.");
    }
}
function GetSofaScoreList() {
    $('#tblScore tbody').empty();
    var objBO = {};
    objBO.ObservationId = '-';
    objBO.Sofasystem = '-';
    objBO.ObservationName = '-';
    objBO.Value = '-';
    objBO.from = '1900-01-01';
    objBO.to = '1900-01-01';
    objBO.Prm1 = _IPDNo;
    objBO.Prm2 = '-';
    objBO.Logic = 'Get:SofaScore';
    var url = config.baseUrl + "/api/IPDNursing/SOFA_ScoreQueries";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            $.each(data.ResultSet.Table, function (key, val) {
                tbody += "<tr>";
                tbody += "<td>" + val.EntryDate + "</td>";
                tbody += "<td>" + val.Score + "</td>";
                tbody += "<td>" + val.MortalityPerc + "</td>";
                tbody += "<td>" + val.EntryDate + "</td>";
                tbody += "<td><button style='height: 21px; line-height: 0;' class='btn btn-warning btn-xs' onclick='SofaScorePrint(\"" + val.EntryDate + "\")'><i class='fa fa-eye'>&nbsp;</i>View</button></td>";
                tbody += "</tr>";
            });
            $('#tblScore tbody').append(tbody);
        },
        error: function (error) {
            console.error('Error fetching data:', error);
        }
    });
}
function SofaScorePrint(entrydate) {
    var formattedDate = entrydate.split('-')[2] + '-' + entrydate.split('-')[1] + '-' + entrydate.split('-')[0];
    var url = "../Print/SofaScorePrint?IPDNO=" + _IPDNo + "&EntryDate=" + formattedDate;
    window.open(url, '_blank');
}
function Clear() {
    $('#tblSofaScore tbody tr').each(function () {
        $(this).find('#txtScore').text('');
        $(this).find('[data-type="Numeric"]').val('');
        var element = $(this).find('[data-type="Text"]');
        if (element.length > 0) {
            element.val('');
        }
    });
}
