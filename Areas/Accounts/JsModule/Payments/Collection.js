﻿
$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    GetStatus();
    OnLoadInfo()
    $('#btnGetEmployee').on('click', function () {
        var empName = $('#txtEmpName').val();
        GetEmpDetails(empName);
    });
    $('#tblReceived tbody').on('click', 'button', function () {
        if ($(this).hasClass('ind')) {
            var Index = $(this).closest('tr').index();
            var autoId = $(this).closest('tr').find('td:last').text();
            Received(autoId, Index)
        }
        else {
            var classGroup = $(this).closest('tr').attr('class');
            $('#tblReceived tbody tr[data-group="' + classGroup + '"]').each(function () {
                var Index = $(this).index();
                var autoId = $(this).find('td:last').text();
                Received(autoId, Index)
            });
        }
    });
});
function GetEmpDetails(empName) {
    var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
    var objBO = {};
    objBO.Prm1 = empName,
        objBO.Logic = 'GetEmployee'
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: 'application/json;charset=utf-8',
        dataType: "JSON",
        success: function (data) {
            if (data != '') {
                $('#ddlEmployee').empty().append($('<option>Select Employee</option>')).select2();
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#txtEmpName').val('');
                    $('#ddlEmployee').append($('<option></option>').val(val.emp_code).html(val.emp_name));
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function OnLoadInfo() {
    $('#tblTransfer tbody').empty();
    var url = config.baseUrl + "/api/Account/HIS_HOTO_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.shiftID = '-';
    objBO.from = '2023-09-26';
    objBO.to = '2023-09-26';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = 'OnLoadInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "<tr>";
                    var total = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        total += (val.Cash + val.SwipeCard + val.Cheque + val.NEFT_RTGS_Online)
                        tbody += "<td>" + val.Cash + "</td>";
                        tbody += "<td>" + val.SwipeCard + "</td>";
                        tbody += "<td>" + val.Cheque + "</td>";
                        tbody += "<td>" + val.NEFT_RTGS_Online + "</td>";
                        tbody += "<td>" + total + "</td>";
                    });
                    $.each(data.ResultSet.Table1, function (key, val) {
                        tbody += "<td>" + val.Trf + "</td>";
                        tbody += "<td>" + val.Received + "</td>";
                        tbody += "<td>" + ((total + val.Received) - val.Trf) + "</td>";
                        tbody += "<td>" + val.Trf_InTran + "</td>";
                        tbody += "<td>" + val.Rec_InTran + "</td>";
                    });
                    tbody += "</tr>";
                }
                $('#tblHOTOInfo tbody').append(tbody);
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    var tbody1 = "";
                    var WorkAmount = 0;
                    var Balance = 0;
                    $.each(data.ResultSet.Table2, function (key, val) {
                        WorkAmount += val.Received;
                        Balance += val.Received;

                        tbody1 += "<tr>";
                        tbody1 += "<td><input type='checkbox' checked/></td>";
                        tbody1 += "<td>" + val.PayMode + "</td>";
                        tbody1 += "<td class='text-right'>" + val.Received + "</td>";
                        tbody1 += "<td class='text-right'><input data-bal=" + val.Received + " onkeyup=totalCal(this) style='width:90%' type='text' class='text-right' value=" + val.Received + " /></td>";
                        tbody1 += "</tr>";
                    });
                    tbody1 += "<tr style='background:#ddd;font-size:12px;' class='total'>";
                    tbody1 += "<td colspan='2'><b class='text-right'>Total</b></td>";
                    tbody1 += "<td class='text-right'><b>" + WorkAmount + "</b></td>";
                    tbody1 += "<td class='text-right'><b>" + Balance + "</b></td>";
                    tbody1 += "</tr>";
                }
                $('#tblTransfer tbody').append(tbody1);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PendingReceipt() {
    $('#tblReceived tbody').empty();
    var url = config.baseUrl + "/api/Account/HIS_HOTO_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.shiftID = '-';
    objBO.from = '2023-09-26';
    objBO.to = '2023-09-26';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = 'PendingReceipt';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    var temp = "";
                    var temp = "";
                    var Received = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        Received += val.Amount;
                        if (temp != val.TransferBy) {
                            tbody += "<tr style='background:#ddd' class='" + val.TransferBy + "'>";
                            tbody += "<td colspan='4'>" + val.TransferBy + ', ' + val.collDate + "";
                            tbody += "<button class='btn btn-success btn-xs pull-right'>Receive</button>";
                            tbody += "</td>";
                            tbody += "</tr>";
                            temp = val.TransferBy
                        }
                        tbody += "<tr data-group='" + val.TransferBy + "'>";
                        tbody += "<td>" + val.PayMode + "</td>";
                        tbody += "<td>" + val.TnxType + "</td>";
                        tbody += "<td class='text-right'>" + val.Amount + "</td>";
                        tbody += "<td><button class='btn btn-success btn-xs ind'>Receive</button></td>";
                        tbody += "<td class='hide'>" + val.autoId + "</td>";
                        tbody += "</tr>";
                    });
                    tbody += "<tr style='background:#ddd;font-size:12px;' class='total'>";
                    tbody += "<td colspan='2'><b class='text-right'>Total</b></td>";
                    tbody += "<td class='text-right'><b>" + Received + "</b></td>";
                    tbody += "<td class='text-right'>-</td>";
                    tbody += "</tr>";
                }
                $('#tblReceived tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetStatus() {
    var url = config.baseUrl + "/api/Account/HIS_HOTO_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.shiftID = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = 'GetStatus';
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
                        $('#txtShiftId').val(val.shiftID);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InitiateClose(logic) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/Account/HOTO_InsertModifyCollection";
        var objBO = [];
        objBO.push({
            'AutoId': 0,
            'hosp_id': Active.HospId,
            'tnxDate': '1900/01/01',
            'tnxBy': Active.userId,
            'tnxTo': '-',
            'PayMode': '-',
            'Amount': 0,
            'Prm1': '-',
            'login_id': Active.userId,
            'Logic': logic,
        })
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Sucess')) {
                    alert('Success')
                    GetStatus();
                }
                else {
                    alert(data);
                    GetStatus();
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function Transfer() {
    if (confirm('Are you sure?')) {
        if ($('#ddlEmployee option:selected').text() == 'Select Employee') {
            alert('Please Select Transfer To')
            return
        }
        var url = config.baseUrl + "/api/Account/HOTO_InsertModifyCollection";
        var objBO = [];
        $('#tblTransfer tbody tr:not(.total)').each(function () {
            if (parseFloat($(this).find('td:eq(3) input').val()) > 0) {
                objBO.push({
                    'AutoId': 0,
                    'PayMode': $(this).find('td:eq(1)').text(),
                    'Amount': $(this).find('td:eq(3) input').val(),
                    'hosp_id': Active.HospId,
                    'tnxBy': Active.userId,
                    'tnxTo': $('#ddlEmployee option:selected').val(),
                    'Prm1': '-',
                    'remark': '-',
                    'login_id': Active.userId,
                    'Logic': 'Transfer',
                })
            }
        })
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                    OnLoadInfo();
                }
                else {
                    alert(data);
                    GetStatus();
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function totalCal(elem) {
    var received = parseFloat($(elem).closest('tr').find('td:eq(2)').text());
    var balance = parseFloat($(elem).val());
    if (received < balance) {
        alert('You tried to transfer more amount than Received.')
        $(elem).val($(elem).data('bal'))
    }
    var totalBalance = 0;
    $('#tblTransfer tbody tr:not(.total)').each(function () {
        totalBalance += parseFloat($(this).find('td:eq(3) input').val());
    });
    $('#tblTransfer tbody tr:last').find('td:last').html('<b>' + totalBalance + '</b>');
}
function Received(autoId, Index) {
    var url = config.baseUrl + "/api/Account/HOTO_InsertModifyCollection";
    var objBO = [
        {
            'AutoId': autoId,
            'hosp_id': Active.HospId,
            'tnxDate': '1900/01/01',
            'tnxBy': Active.userId,
            'tnxTo': '-',
            'PayMode': '-',
            'Amount': 0,
            'Prm1': '-',
            'login_id': Active.userId,
            'Logic': 'Received'
        }];
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                $('#tblReceived tbody').find('tr:eq(' + Index + ')').addClass('bg-success');
                $('#tblReceived tbody').find('tr:eq(' + Index + ')').find('button').prop('disabled', true);
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
function ReportsInfo() {
    $('#tblReports tbody').empty();
    var url = config.baseUrl + "/api/Account/HIS_HOTO_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.shiftID = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.LoginId = Active.userId;
    objBO.Logic = 'Reports';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "<tr>";
                    var total = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        total += (val.Cash + val.SwipeCard + val.Cheque + val.NEFT_RTGS_Online)
                        tbody += "<tr>";
                        tbody += "<td>" + val.ShiftID + "</td>";
                        tbody += "<td>" + val.CollDate + "</td>";
                        tbody += "<td>" + val.Cash + "</td>";
                        tbody += "<td>" + val.SwipeCard + "</td>";
                        tbody += "<td>" + val.Cheque + "</td>";
                        tbody += "<td>" + val.NEFT_RTGS_Online + "</td>";
                        tbody += "<td>" + total + "</td>";
                        tbody += "<td>" + val.Trf + "</td>";
                        tbody += "<td>" + val.Received + "</td>";
                        tbody += "<td>" + ((total + val.Received) - val.Trf) + "</td>";
                        tbody += "<td>" + val.Trf_InTran + "</td>";
                        tbody += "<td>" + val.Rec_InTran + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReports tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}