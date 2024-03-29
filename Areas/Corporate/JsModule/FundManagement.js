﻿var _panelId = '';
$(document).ready(function () {
    $('#btnSubmit').prop('disabled', true);
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    GetPanel();
});

function GetPanel() {
    $('#tblPanel tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'PanelInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.PanelId + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td><button onclick=selectedPanel(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblPanel tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetFundInfo() {
    $('#tblFundInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.UHID = $('#txtUHID').val();
    objBO.ReportType='';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'GetFundInfo';

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#d9d9d9'>";
                            tbody += "<td colspan='7'>" + val.PanelName + "</td>";
                            tbody += "</tr>";
                            temp = val.PanelName;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.trn_date + "</td>";
                        tbody += "<td>" + val.Amount + "</td>";
                        tbody += "<td>" + val.RefNo + "</td>";
                        tbody += "<td>" + val.RefDetail + "</td>";
                        tbody += "<td>" + val.UtrDate + "</td>";
                        tbody += "<td><button onclick=CancelFund(" + val.AutoId + ") class='btn btn-danger btn-xs disabled'><i class='fa fa-close'>&nbsp;</i>Cancel</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblFundInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FUNDEXCEL(elem) {
    $('#tblFundInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.UHID = $('#txtUHID').val();
    objBO.ReportType = 'Excel';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'FUND:EXCEL';
    Global_DownloadExcel(url, objBO,"FundReport.xlsx", elem);
}
function Global_DownloadExcel(Url, objBO, fileName, elem) {
    $(elem).addClass('loading');
    var ajax = new XMLHttpRequest();
    ajax.open("Post", Url, true);
    ajax.responseType = "blob";
    ajax.setRequestHeader("Content-type", "application/json")
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            saveAs(blob, fileName); //refernce by ~/JsModule/FileSaver.min.js
            $(elem).removeClass('loading');
        }
    };
    ajax.send(JSON.stringify(objBO));
}
function GetFundInfo() {
    $('#tblFundInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.UHID = $('#txtUHID').val();
    objBO.ReportType = '';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = 'GetFundInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#d9d9d9'>";
                            tbody += "<td colspan='7'>" + val.PanelName + "</td>";
                            tbody += "</tr>";
                            temp = val.PanelName;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.trn_date + "</td>";
                        tbody += "<td>" + val.Amount + "</td>";
                        tbody += "<td>" + val.RefNo + "</td>";
                        tbody += "<td>" + val.RefDetail + "</td>";
                        tbody += "<td>" + val.UtrDate + "</td>";
                        tbody += "<td><button onclick=CancelFund(" + val.AutoId + ") class='btn btn-danger btn-xs disabled'><i class='fa fa-close'>&nbsp;</i>Cancel</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblFundInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function selectedPanel(elem) {
    $('.panelInfo').empty();
    selectRow(elem)
    _panelId = $(elem).closest('tr').find('td:eq(0)').text();
    var panelinfo = $(elem).closest('tr').find('td:eq(1)').text();
    $('.panelInfo').html(panelinfo);
    $('.rightpanel').removeClass('lock');

    
}
function ValidateUHID() {
    if ($('#txtUHID').val() == '') {
        alert('Please Provide UHID');
        $('#txtUHID').focus();
        return;
    }
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.PanelId = _panelId;
    objBO.UHID = $('#txtUHID').val();
    objBO.from = '1900/01/01';
    objBO.ReportType = '';
    objBO.to = '1900/01/01';
    objBO.Logic = 'ValidateUHID';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var result = data.ResultSet.Table[0].result;
            var PatientInfo = data.ResultSet.Table[0].PatientInfo;
            var Balance = data.ResultSet.Table[0].Balance;

            $('.panelInfo').append(" , Patient Balance : "+ Balance);

            if (result == 'Y') {
                $('#PatientInfo').html(PatientInfo);
                $('#btnSubmit').prop('disabled', false);
                $('#txtUHID').prop('disabled', true);
                GetFundInfo();
            }
            else {
                alert('UHID Not Found.');
                $('#PatientInfo').html('');
                $('#txtUHID').val('');
                $('#btnSubmit').prop('disabled', true);
                $('#txtUHID').prop('disabled', false);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertFundManagement() {
    if (_panelId == '') {
        alert('Please Select Panel');
        return;
    }
    if ($('#txtUHID').val() == '') {
        alert('Please Provide UHID');
        $('#txtUHID').focus();
        return;
    }
    if ($('#ddlEntryType option:selected').text() == 'Select') {
        alert('Please Provide Entry Type');
        $('#ddlEntryType').focus();
        return;
    }
    if ($('#txtAmount').val() == '') {
        alert('Please Provide Amount');
        $('#txtAmount').focus();
        return;
    }
    if ($('#txtRefNo').val() == '') {
        alert('Please Provide Ref No');
        $('#txtRefNo').focus();
        return;
    }
    if ($('#txtRefDetail').val() == '') {
        alert('Please Provide Ref Detail');
        $('#txtRefDetail').focus();
        return;
    }
    if ($('#txtUTRNo').val() == '') {
        alert('Please Provide UTR No.');
        $('#txtUTRNo').focus();
        return;
    }
    if ($('#txtUTRDate').val() == '') {
        alert('Please Provide UTR Date');
        $('#txtUTRDate').focus();
        return;
    }
    var url = config.baseUrl + "/api/Corporate/InsertFundManagement";
    var objBO = {};
    objBO.AutoId = 0;
    objBO.PanelId = _panelId;
    objBO.trn_date = '2023/05/23';
    objBO.Amount = $('#txtAmount').val();
    objBO.RefDetail = $('#txtRefDetail').val();
    objBO.RefNo = $('#txtRefNo').val();
    objBO.UHID = $('#txtUHID').val();
    objBO.UTRNo = $('#txtUTRNo').val();
    objBO.UTRDate = $('#txtUTRDate').val();
    objBO.TnxType = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#ddlEntryType option:selected').text() == 'Received') ? 'Insert' : 'Insert:Adjustment';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                Clear()
                GetFundInfo();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function CancelFund(autoId) {
    if (confirm('Are you sure to Cancel')) {
        var url = config.baseUrl + "/api/Corporate/InsertFundManagement";
        var objBO = {};
        objBO.AutoId = autoId;
        objBO.PanelId = _panelId;
        objBO.trn_date = '2023/05/23';
        objBO.Amount = '-';
        objBO.RefDetail = '-';
        objBO.RefNo = '-';
        objBO.UHID = '-';
        objBO.UTRNo = '-';
        objBO.UTRDate = '2023/05/23';
        objBO.TnxType = '-';
        objBO.Prm1 = '-';
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'Cancel';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    GetFundInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function Clear() {
    $('#ddlEntryType').prop('selectedIndex', 0);
    var input = document.querySelectorAll('input');
    input.values = "";
}





