var _patient = '';
var _EstimateNo = '';
var _Uhid = '';
var _estDate = '';
var sum = '';
$(document).ready(function () {
    CloseSidebar();
    $('#dash-dynamic-section').find('label.title').text('Estimate To Patiant').show();
    CKEDITOR.replace("txtTemplate1");
    $('#tblEstimateVariable tbody').on('change', 'input:checkbox', function () {
        var var1 = $(this).closest('tr').find('td:eq(1)').text();
        if ($(this).is(':checked'))
            CKEDITOR.instances['txtTemplate1'].insertHtml('{<strong class="var">' + var1 + '</strong>}');
        else
            CKEDITOR.instances['txtTemplate1'].insertText('');
    });
    EstimateList();
    searchEmployeeByUHID();

});
function searchEmployeeByUHID() {
    var objBO = {};
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateQueries";
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = '-';
    _Uhid = $('#txtGetUHID').val();
    objBO.uhid = _Uhid;
    objBO.patientName = '-';
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.toWhom = '-';
    objBO.amount = 0;
    objBO.result = '-';
    objBO.estContent = '-';
    objBO.Logic = 'GetEstimate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtName').text(val.FirstName);
                        $('#txtGender').text(val.gender);
                        $('#txtAddress').text(val.address);
                        $('#txtAge').text(val.age);
                        $('#txtUH').text(val.UHID);
                        _patient = val.patient_name;
                        EstimateList();
                    });
                }
            }
        },
        error: function (err) {
            alert('server error');
        },
    })
}


function InsertEstimate() {
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_IPD_Estimates";
    var templatecontent = CKEDITOR.instances['txtTemplate1'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    if (result) {
        for (var i = 0; i < result.length; i++) {
            varList.push($(result[i]).text());
        }
    } else {
        console.log("No matches found in templatecontent.");
    }

    if (!_Uhid || !_patient || !$('#ToWhom').val() || !$('#txtAmount').val() || !templatecontent) {
        alert("Please fill in all required fields.");
        return;
    }
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = '-';
    objBO.uhid = _Uhid;
    objBO.patientName = _patient;
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.toWhom = $('#ToWhom').val();
    objBO.amount = $('#txtAmount').val();
    objBO.estContent = templatecontent;
    objBO.var_list = varList.join(',');
    objBO.result = '-';
    objBO.Logic = 'Insert';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            Clear();
            EstimateList();
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}


function UpdatEstimateForm() {
    var objBO = {};
    var varList = [];
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_IPD_Estimates";
    var templatecontent = CKEDITOR.instances['txtTemplate1'].getData();
    var result = templatecontent.match(/[^{]+(?=\})/g);
    if (result) {
        for (var i = 0; i < result.length; i++) {
            varList.push($(result[i]).text());
        }
    } else {
        console.log("No matches found in templatecontent.");
    }
    objBO.hospId = '-';
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = _EstimateNo;
    objBO.uhid = '-';
    objBO.patientName = '-';
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.toWhom = '-';
    objBO.amount = $('#txtAmount').val();
    objBO.estContent = templatecontent;
    objBO.var_list = varList.join(',');
    objBO.result = '-';
    objBO.Logic = 'Update';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            Clear();
            EstimateList();
        },
        error: function (err) {
            alert('server error');
        },
    });
}
function EstimateList() {
    $('#tblEstimate tbody').empty();
    var objBO = {};
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateQueries";
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = '-';
    objBO.uhid = _Uhid;
    objBO.patientName = '-';
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.toWhom = '-';
    objBO.amount = 0;
    objBO.result = '-';
    objBO.estContent = '-';
    objBO.Logic = 'GetEstimateList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody1 = "";
            $.each(data.ResultSet.Table, function (key, val) {
                tbody1 += "<tr>";
                tbody1 += "<td class='hide'>" + val.var_list + "</td>";
                tbody1 += "<td class='hide'>" + val.estContent + "</td>";
                tbody1 += "<td class='hide'>" + val.estimateNo + "</td>";
                tbody1 += "<td class='hide'>" + val.autoId + "</td>";
                var estDate = new Date(val.estDate);
                var day = estDate.getDate().toString().padStart(2, '0');
                var month = (estDate.getMonth() + 1).toString().padStart(2, '0');
                var year = estDate.getFullYear();
                var hours = estDate.getHours().toString().padStart(2, '0');
                var minutes = estDate.getMinutes().toString().padStart(2, '0');
                var formattedDate = day + '/' + month + '/' + year + ' ' + hours + ':' + minutes;
                tbody1 += "<td>" + formattedDate + "</td>";
                tbody1 += "<td>" + val.toWhom + "</td>";
                tbody1 += "<td>" + val.amount + "</td>";
                _estDate = val.estDate;
                tbody1 += '<td class="text-center"><button class="btn btn-primary btn-sm" style="height: 21px;width: 38px;margin-bottom:2px;margin-top:-1px;" onclick=SeEstimateNo("' + val.estimateNo + '");EstimateInfo(this) data-whome="' + val.toWhom + '" data-am="' + val.amount + '"><center style="font-size:11px;margin-top:-5px;margin-left:-1px;">Edit</center></button></td>';
                tbody1 += "</tr>";

            });
            $('#tblEstimate tbody').append(tbody1);
        },
        error: function (response) {
            console.error("Error:", respon);
            alert('Server Error...!');
        }
    });
}
function EstimateInfo(elem) {
    $('#btnSubmit').text('Update').addClass('btn-warning').removeClass('btn-success').attr('onclick', 'UpdatEstimateForm()');
    selectRow($(elem));
    var varList = $(elem).closest('tr').find('td:eq(0)').text();
    var result = varList.split(',');
    var content = $(elem).closest('tr').find('td:eq(1)').html();
    var ToWhome = $(elem).data('whome');
    var Amount = $(elem).data('am');
    $('#txtAmount').val(Amount)
    $('#ToWhom').val(ToWhome)
    CKEDITOR.instances['txtTemplate1'].setData(content);
    $('#tblEstimateVariable tbody').find('input:checkbox').prop('checked', false);
    for (var i = 0; i <= result.length; i++) {
        $('#tblEstimateVariable tbody tr').each(function () {
            if ($(this).find('td:eq(1)').text() == result[i])
                $(this).find('td:eq(2)').find('input:checkbox').prop('checked', true);
        });
    }
}
function SeEstimateNo(est) {
    _EstimateNo = est;
}
function Clear() {
    $('#txtAmount').val('');
    $('#ToWhom').val('');
    $('#tblEstimateVariable tbody').find('input:checkbox').prop('checked', false);
    CKEDITOR.instances['txtTemplate1'].setData('');
    $('#btnSubmit').text('Submit').addClass('btn-success').removeClass('btn-warning');
}

function EstimatePrint() {
    var formattedDate = _estDate.split('T')[0];
    var url = "../Print/PrintEstimateForm?UHIDNO=" + _Uhid + "&estDate=" + formattedDate;
    window.open(url, '_blank');
}
function openPopup() {
    var popup = $("#popup");
    popup.show();
    EstimateCal();
}
function closePopup() {
    var popup = $("#popup");
    popup.hide();
}
function EstimateCal() {
    $('#tblEstimateCal tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateQueries";
    var objBO = {
        hospId: Active.HospId,
        estDate: '1900-01-01',
        estimateNo: '-',
        uhid: _Uhid,
        patientName: '-',
        isActive: '-',
        createdBy: '-',
        toWhom: '-',
        amount: 0,
        result: '-',
        estContent: '-',
        Logic: 'RoomCharges'
    };
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
                tbody += "<tr>";
                tbody += '<td><a onclick="EstimateRemove(this)"><i class="fa fa-remove" style="font-size:20px;color:red;margin-left: 6px;"></i></a></td>';
                tbody += "<td>" + val.Category + "</td>";
                tbody += "<td>" + val.Tariff + "</td>";
                tbody += "<td>" + val.ConsultationCharges + "</td>";
                tbody += "<td>" + val.NursingCharges + "</td>";
                tbody += "<td>" + val.RMO_IntensivistCharges + "</td>";
                tbody += "<td>" + val.DieticianCharge + "</td>";
                tbody += "<td>" + val.DailyTotal + "</td>";
                tbody += "<td style='text-align:center'><input type='text' id='txt " + counter + "' onkeyup=Estimatecalvalue(this)  style='width: 70px;text-align:center' class='form - control'></td>";
                tbody += "<td id='txttotal' style='width:70px'></td>";
                tbody += "</tr>";
            });

            $('#tblEstimateCal tbody').append(tbody);
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function EstimateCalSub() {
    var tbody = "";
    var counter = 1;
    tbody += "<thead>";
    tbody += "<tr>";
    tbody += "<th>Categoty</th>";
    tbody += "<th>Tariff/day</th>";
    tbody += "<th>Consultation Charge/day</th>";
    tbody += "<th>Nursing Charge/day</th>";
    tbody += "<th>RMO/Intensivist charge/day</th>";
    tbody += "<th>Diectician Charge</th>";
    tbody += "<th>Daily Total</th>";
    tbody += "<th>Days</th>";
    tbody += "<th style='width:70px'>Total</th>";
    tbody += "</tr>";
    tbody += "</thead>";
    $('#tblEstimateCal tbody tr').each(function () {
        tbody += "<tr>";
        tbody += "<td>" + $(this).find('td:eq(1)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(2)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(3)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(4)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(5)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(6)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(7)').text() + "</td>";
        tbody += "<td>" + $(this).find('td:eq(8) input').val() + "</td>";
        tbody += "<td style='width:70px'>" + $(this).find('td:eq(9)').text() + "</td>";
        tbody += "</tr>";
    });
    var tblcal = "<table style='border:1px solid #000;border-collapse:collapse;text-align: center;'>" + tbody + "</table><table style= 'float:right'><b >Total :</b> " + sum + "</table>  ";
    CKEDITOR.instances['txtTemplate1'].setData(tblcal);
    closePopup();
}
function EstimateRemove(element) {
    $(element).closest('tr').remove();
}
function Estimatecalvalue(input) {
    var row = $(input).closest('tr');
    var dailyTotal = parseFloat(row.find('td:eq(7)').text());
    var inputVal = parseFloat($(input).val());
    var total = dailyTotal * inputVal;
    row.find("#txttotal").text(isNaN(total) ? '' : total);
    var sum = 0;
    $('#tblEstimateCal tbody #txttotal').each(function () {
        var value = parseFloat($(this).text());
        if (!isNaN(value)) {
            sum += value;
        }
    });
    $('#txttotalSumval').text(sum);
}
$(document).on('blur', 'input[type="text"]', function () {
    var value = $(this).val().trim();
    if (value === '') {
        $(this).val('');
        $(this).closest('tr').find("#txttotal").text('');
    }
});

function EstimateRemove(element) {
    $(element).closest('tr').remove();
    sum = 0;
    $('#tblEstimateCal tbody #txttotal').each(function () {
        var value = parseFloat($(this).text());
        if (!isNaN(value)) {
            sum += value;
        }
    });
    $('#txttotalSumval').text(sum);
}
function InputValues() {
    var totalDaysValue = $('#txtTotalDays').val();
    $("table tbody tr").each(function () {
        $(this).find("input[type='text']").val(totalDaysValue).trigger('keyup');
    });
}





