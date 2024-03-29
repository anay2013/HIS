﻿$(document).ready(function () {
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    CloseSidebar();
    $("#tblReport tbody").on('change', 'input:checkbox', function () {
        if ($(this).closest('tr').hasClass('pt')) {
            $(this).parents('table').find('tbody').find('input:checkbox').not($(this)).prop('checked', false);
            var pt = $(this)
            $(this).parents('table').find('tbody tr:eq(' + $(this).closest('tr').index() + ')').nextUntil('tr.pt').each(function () {
                $(this).find('input:checkbox').prop('checked', $(pt).is(':checked'));
            })
        }
        if ($(this).closest('tr').hasClass('g')) {
            var pt1 = $(this)
            $(this).parents('table').find('tbody tr:eq(' + $(this).closest('tr').index() + ')').nextUntil('tr.g').each(function () {
                $(this).find('input:checkbox').prop('checked', $(pt1).is(':checked'));
            })
        }
        //if ($(this).parents('div').hasClass('testCategory'))
        //    $(this).closest('tr.g').nextUntil('tr.g').css
    });
    LoadTestCategory();
});
function LoadTestCategory() {
    $("#ddlDepartment").append($("<option></option>").val("ALL").html("ALL")).select2();
    var url = config.baseUrl + "/api/Lab/Lab_ReportPrintingQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.DoctorId = '-';
    objBO.VisitNo = '-';
    objBO.TestCategory = $('#ddlDepartment option:selected').val();
    objBO.TestIds = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "LoadTestCategory";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, value) {
                        $("#ddlDepartment").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
                    });
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function ReportInfo() {
    $("#tblReport tbody").empty();
    var url = config.baseUrl + "/api/Lab/Lab_ReportPrintingQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.DoctorId = '-';
    objBO.VisitNo = '-';
    objBO.TestCategory = $('#ddlDepartment option:selected').val();
    objBO.TestIds = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'TestWiseReport';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    ReportList(data, objBO.Logic);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReportInfoBySearchKey() {
    $("#tblReport tbody").empty();
    var url = config.baseUrl + "/api/Lab/Lab_ReportPrintingQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.DoctorId = '-';
    objBO.VisitNo = '-';
    objBO.TestCategory = '-';
    objBO.TestIds = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('#txtSeachValue').val();
    objBO.Prm2 = $('#ddlSeachBy option:selected').val();
    objBO.Logic = 'ByInputId';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    ReportList(data, "TestWiseReport");
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReportList(data, Logic) {
    var tbody = "";
    var visitNo = "";
    var testCategory = "";
    $.each(data.ResultSet.Table, function (key, val) {
        if (visitNo != val.VisitNo) {
            tbody += "<tr style='background:#f7ecd3' class='pt'>";
            tbody += "<td>" + val.UHID + "</td>";
            tbody += "<td>" + val.VisitNo + "</td>";
            tbody += "<td>" + val.VisitDate + "</td>";
            tbody += "<td>" + val.patient_name + "</td>";
            tbody += "<td>" + val.ageInfo + "</td>";
            tbody += "<td>" + val.DoctorName + "</td>";
            tbody += "<td>" + val.ref_name + "</td>";
            tbody += "<td><input type='checkbox' name='pgroup'/>&nbsp;Select All&nbsp;&nbsp;<button onclick=PrintInHouse(this) class='btn btn-success btnPrint btn-xs'>Print Report</button></td>";
            tbody += "<td></td>";
            tbody += "</tr>";
            visitNo = val.VisitNo;
        }
        if (testCategory != val.TestCat2) {
            tbody += "<tr class='g'>";
            tbody += "<td colspan='7'></td>";
            tbody += "<td>";
            tbody += "<div class='testCategory " + val.RepStatus + "Group'>";
            tbody += "<input type='checkbox'/>";
            tbody += "<label data-ids=" + val.TestIds + ">" + val.testCategory + "</label>";
            tbody += "<label>" + val.TApprCount + '/' + val.TCount + "</label>";
            tbody += "</div>";
            tbody += "</td>";
            tbody += "<td>" + val.IsLocalTest + "</td>";
            tbody += "</tr>";
            testCategory = val.TestCat2;
        }
        tbody += "<tr class=" + val.IsLocalTest + ">";
        tbody += "<td colspan='7'></td>";
        tbody += "<td>";
        tbody += "<div class='testGroup'>";
        tbody += "<input data-ids=" + val.TestIds + " type='checkbox'/>";
        tbody += "<label class=" + val.RepStatus + ">" + val.TestName + "</label>";
        tbody += "</div>";
        tbody += "</td>";
        tbody += "<td>" + val.IsLocalTest + "</td>";
        tbody += "</tr>";
    });
    $("#tblReport tbody").append(tbody);
}

function PrintInHouse(elem) {
    var visitNo = $(elem).closest('tr').find('td:eq(1)').text();
    var SubCat = 'ALL';
    var TestIds = [];
    TestIds = [];
    $("#tblReport tbody tr:not(.g,.pt).In-House input:checkbox:checked").each(function () {
        TestIds.push($(this).data('ids'));
    });
    var Logic = 'ByFinalPrint';
    var url = config.rootUrl + "/Lab/print/PrintLabReport?visitNo=" + visitNo + "&SubCat=" + SubCat + "&TestIds=" + TestIds.join() + "&Source=In-House&Logic=" + Logic;
    window.open(url, '_blank');
}