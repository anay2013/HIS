﻿var _IPOPNo, _IPOPType, _IndentNo, _PatientName, _IndentAutoId,_IndentItemId,_IndentQty;
$(document).ready(function () {
    
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    $('#tblDonorInfo tbody').on('click', 'button.get', function () {
        $('.StockInfo').find('label span').text(0);
        $('#tblStockInfo input:checkbox').prop('checked', false)
        var IPOPNo = $(this).closest('tr').find('td:eq(5)').text();
        var IPOPType = $(this).closest('tr').find('td:eq(2)').text();
        var ComponentId = $(this).closest('tr').find('td:eq(1)').text();
        var IndentNo = $(this).closest('tr').find('td:eq(4)').text();
        var PatientName = $(this).closest('tr').find('td:eq(6)').text();
        var IndentAutoId = $(this).closest('tr').find('td:eq(12)').text();
        var IndentItemId = $(this).closest('tr').find('td:eq(13)').text();
        var IndentQty = $(this).closest('tr').find('td:eq(8)').text();
        var BalanceQty = $(this).closest('tr').find('td:eq(10)').text();
        $('.StockInfo').find('label:eq(1) span').text(BalanceQty);
        selectRow($(this))
        _IPOPNo = IPOPNo;
        _IPOPType = IPOPType;
        _IndentNo = IndentNo;
        _IndentAutoId = IndentAutoId;
        _IndentItemId = IndentItemId;
        _IndentQty = IndentQty;
        _PatientName = PatientName;
        StockInfo($(this));
    });
    $('#tblStockInfo').on('change','input:checkbox', function () {
        $('.StockInfo').find('label:last span').text($('#tblStockInfo tbody input:checkbox:checked').length);
    });
    DonorInfo();
});
function DonorInfo() {   
    $('.StockInfo').find('label span').text(0);
    $('#tblStockInfo input:checkbox').prop('checked', false)
    $('#tblStockInfo tbody').empty();
    $('#tblIssuedBloodIndent tbody').empty();
    $('#tblDonorInfo tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.IndentNo = '-';
    objBO.Prm1 = $('#ddlBloodIndentBy option:selected').text();
    objBO.Prm2 = $('#ddlStatus option:selected').val();
    objBO.login_id = Active.userId;
    objBO.Logic = "BloodIssue:DonorInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    var counter = 0;
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        counter++;
                        if (val.IssueFlag == 'X')
                            tbody += "<tr class='IsRejected'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td style='padding:0px 5px'><button style='height: 17px;line-height:0;' class='btn btn-warning btn-xs get'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "<td class='hide'>" + val.ComponentId + "</td>";
                        tbody += "<td class='hide'>" + val.IPOPType + "</td>";
                        tbody += "<td>" + val.IndentDate + "</td>";
                        tbody += "<td>" + val.IndentNo + "</td>";
                        tbody += "<td>" + val.IPOPNo + "</td>";
                        tbody += "<td>" + val.PatientName + "</td>";              
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td class='text-right'>" + val.IndentQty + "</td>";
                        tbody += "<td class='text-right'>" + val.IssueQuantity + "</td>";
                        tbody += "<td class='text-right'>" + val.balance + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td class='hide'>" + val.IndentAutoId + "</td>";
                        tbody += "<td class='hide'>" + val.ItemId + "</td>";
                        if (val.IssueFlag != 'X')
                            tbody += "<td style='padding:0px 5px'><button onclick=RejectIndent(this) style='height: 17px;line-height:0;' class='btn btn-danger btn-xs'><i class='fa fa-close'></i></button></td>";
                        else
                            tbody += "<td style='padding:0px 5px'>-</td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblDonorInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function StockInfo(elem) {  
    $('#tblStockInfo tbody').empty();
    $('#tblIssuedBloodIndent tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.IndentNo = $(elem).closest('tr').find('td:eq(4)').text(); //Indent No carries
    objBO.Prm1 = $(elem).closest('tr').find('td:eq(1)').text(); //Component Id carries
    objBO.Prm2 = $(elem).closest('tr').find('td:eq(7)').text(); //Blood Group Allotted carries
    objBO.login_id = Active.userId;
    objBO.Logic = "BloodIssue:StockInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    var counter = 0;
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        counter++;
                        if (val.IsIssueLocked == 'Y')
                            tbody += "<tr class='hold'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td><input type='checkbox' /></td>";
                        tbody += "<td class='hide'>" + val.itemId + "</td>";
                        tbody += "<td class='hide'>" + val.ComponentID + "</td>";
                        tbody += "<td><a href='#' onclick=GetStockInfo('" + val.Stock_Id + "') style='color: #2a5ded;font-weight:bold;'>" + val.Stock_Id + "</a></td>";
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td class='text-right'>" + val.Qty + "</td>";
                        tbody += "<td>" + val.ExpDate + "</td>";
                        tbody += "<td>" + val.BBTubeNo + "</td>";
                        tbody += "<td>" + val.BloodGroup + "</td>";
                        //tbody += "<td><button class='btn btn-warning btn-xs' data-autoid=" + val.AutoId + " onclick=Issue(this) style='height: 17px;line-height: 0;'><i class='fa fa-check-circle'>&nbsp;</i>Issue</button></td>";
                        //if (val.IsHold != 'Y') {
                        //    if (val.IsIssueLocked != 'Y')
                        //        tbody += "<td><button class='btn btn-danger btn-xs' onclick=HoldUnHold(" + val.AutoId + ",'Hold') style='height: 17px;line-height: 0;'><i class='fa fa-clock-o'>&nbsp;</i>Hold</button></td>";
                        //    else
                        //        tbody += "<td>Hold</td>";
                        //}
                        //else
                        //    tbody += "<td><button class='btn btn-danger btn-xs' onclick=HoldUnHold(" + val.AutoId + ",'UnHold') style='height: 17px;line-height: 0;'><i class='fa fa-clock-o'>&nbsp;</i>Un-Hold</button></td>";

                        tbody += "<td>" + val.IssueHoldTo + "</td>";
                        tbody += "<td>" + val.Visit_ID + "</td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblStockInfo tbody').append(tbody);
                    $('.StockInfo').find('label:eq(0) span').text(data.ResultSet.Table.length);
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    var tbody = "";
                    var counter = 0;
                    var count = 0;
                    $.each(data.ResultSet.Table1, function (key, val) {
                        counter++;
                        tbody += "<tr>";
                        tbody += "<td>" + counter + "</td>";
                        tbody += "<td>" + val.IndentNo + "</td>";
                        tbody += "<td><a href='#' onclick=GetStockInfo('" + val.Stock_Id + "') style='color: #2a5ded;font-weight:bold;'>" + val.Stock_Id + "</a></td>";
                        tbody += "<td>" + val.PatientName + "</td>";
                        tbody += "<td>" + val.BloodGroup + "</td>";
                        tbody += "<td>" + val.BBTubeNo + "</td>";
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblIssuedBloodIndent tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetStockInfo(stockId) {
    $('#modalStockInfo').modal('show');
}
function RejectIndent(elem) {
    if (confirm('Are you sure to Reject?')) {
        var url = config.baseUrl + "/api/BloodBank/BB_Insert_ModifyBloodIssue";
        var objBO = {};
        objBO.AutoId = $(elem).closest('tr').find('td:eq(12)').text();
        objBO.hosp_id = Active.HospId;
        objBO.Stock_ID = '-';
        objBO.ItemID = '-';
        objBO.IndentNo = '-';
        objBO.IPOPNo = '-';
        objBO.IPOPType = '-';
        objBO.Quantity = '-';
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "RejectIssueIndent";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    $(elem).closest('tr').addClass('IsRejected');
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function Issue() {
    if (confirm('Are you sure to Issue?')) {
        var url = config.baseUrl + "/api/BloodBank/BB_Insert_ModifyBloodIssue";
        var objBO = {};
        var stockId = [];
        $('#tblStockInfo tbody input:checkbox:checked').each(function () {
            stockId.push($(this).closest('tr').find('td:eq(3) a').text())
        })
        if (stockId.length == 0) {
            alert('Plese Choose Stock.')
            return
        }
        var IQty = parseInt( $('.StockInfo').find('label:eq(1) span').text())
        var SQty = parseInt($('.StockInfo').find('label:last span').text())
        if (SQty > IQty) {
            alert('Select Qty Should not be greater than Balance Qty');
            return
        }
        objBO.AutoId = 0;
        objBO.hosp_id = Active.HospId;
        objBO.Stock_ID = stockId.join('|');
        objBO.ItemID = _IndentItemId;
        objBO.IndentNo = _IndentNo;
        objBO.IPOPNo = _IPOPNo;
        objBO.IPOPType = _IPOPType;
        objBO.Quantity = _IndentQty;
        objBO.Prm1 = _IPOPNo + ' : ' + _PatientName;
        objBO.Prm2 = _IndentAutoId;
        objBO.login_id = Active.userId;
        objBO.Logic = "Issue";    
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    $('#tblDonorInfo tbody tr.select-row').css('background-color', 'green');
                    $('#tblDonorInfo tbody tr.select-row').find('button.get').click();
                    alert(data)
                }
                else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function HoldUnHold(logic) {
    if (confirm('Are you sure to ' + logic + '?')) {

        var stockId = [];
        $('#tblStockInfo tbody input:checkbox:checked').each(function () {
            stockId.push($(this).closest('tr').find('td:eq(3) a').text())
        })
        if (stockId.length == 0) {
            alert('Plese Choose Stock.')
            return
        }
        var IQty = parseInt($('.StockInfo').find('label:eq(1) span').text())
        var SQty = parseInt($('.StockInfo').find('label:last span').text())
        if (SQty > IQty) {
            alert('Select Qty Should not be greater than Balance Qty');
            return
        }
        var url = config.baseUrl + "/api/BloodBank/BB_Insert_ModifyBloodIssue";
        var objBO = {};
        objBO.AutoId = 0;
        objBO.hosp_id = Active.HospId;
        objBO.Stock_ID = stockId.join('|');
        objBO.ItemID = '-';
        objBO.IndentNo = _IndentNo;
        objBO.IPOPNo = _IPOPNo;
        objBO.IPOPType = _IPOPType;
        objBO.Quantity = 0;
        objBO.Prm1 = _IPOPNo + ' : ' + _PatientName;
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = logic;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert('Successfully ' + logic + '');
                    $('#tblDonorInfo tbody tr.select-row').find('button.get').click();
                    stockId = [];                 
                    $('.StockInfo').find('label:last span').text(0);
                    $('#tblStockInfo tbody input:checkbox').prop('checked', false)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}