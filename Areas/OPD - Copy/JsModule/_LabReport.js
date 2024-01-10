$(document).ready(function () { 
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
    objBO.TestCategory = '-';
    objBO.TestIds = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
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
            console.log(data)
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
    objBO.VisitNo = ($('#txtHeaderUHID').text() == '') ? $('#tblAdviceHeader tbody tr:eq(1)').find('td:eq(5)').text() : $('#txtHeaderUHID').text();
    objBO.TestCategory = $('#ddlDepartment option:selected').val();
    objBO.TestIds = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $('#ddlIPOPType option:selected').text();
    objBO.Prm2 = '-';
    objBO.Logic = $('input[name=reportBy]:checked').val();
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
            tbody += "<td><input type='checkbox' name='pgroup'/>&nbsp;Select All&nbsp;&nbsp;<button onclick=Print(this) class='btn btn-success btnPrint btn-xs'>Print</button></td>";
            tbody += "</tr>";
            visitNo = val.VisitNo;
        }
        if (Logic == 'OPD-IPD:TestCategoryWise') {
            tbody += "<tr>";
            tbody += "<td colspan='7'></td>";
            tbody += "<td>";
            tbody += "<div class='testCategory " + val.RepStatus + "Group'>";
            tbody += "<input data-ids=" + val.TestIds + " type='checkbox'/>";
            tbody += "<label>" + val.testCategory + "</label>";
            tbody += "<label>" + val.TApprCount + '/' + val.TCount + "</label>";
            tbody += "</div>";
            tbody += "</td>";
            tbody += "</tr>";
        }
        else {
            if (testCategory != val.TestCat2) {
                tbody += "<tr class='g'>";
                tbody += "<td colspan='7'></td>";
                tbody += "<td>";
                tbody += "<div class='testCategory " + val.RepStatus + "Group'>";
                tbody += "<input type='checkbox'/>";
                tbody += "<label data-ids=" + val.TestIds + " onclick=alert('" + val.TestIds + "')>" + val.testCategory + "</label>";
                tbody += "<label>" + val.TApprCount + '/' + val.TCount + "</label>";
                tbody += "</div>";
                tbody += "</td>";
                tbody += "</tr>";
                testCategory = val.TestCat2;
            }
            tbody += "<tr>";
            tbody += "<td colspan='7'></td>";
            tbody += "<td>";
            tbody += "<div class='testGroup'>";
            tbody += "<input data-ids=" + val.TestIds + " type='checkbox'/>";
            tbody += "<label class=" + val.RepStatus + ">" + val.TestName + "</label>";
            tbody += "</div>";
            tbody += "</td>";
            tbody += "</tr>";
        }
    });
    $("#tblReport tbody").append(tbody);
}

function Print(elem) {
    var visitNo = $(elem).closest('tr').find('td:eq(1)').text();
    var SubCat = 'ALL';
    var TestIds = [];
    if ($('input[name=reportBy]:checked').val() == 'OPD-IPD:TestWiseReport') {
        TestIds = [];
        $("#tblReport tbody tr:not(.g,.pt) input:checkbox:checked").each(function () {
            TestIds.push($(this).data('ids'));
        });
    }
    if ($('input[name=reportBy]:checked').val() == 'OPD-IPD:TestCategoryWise') {
        TestIds = [];
        $("#tblReport tbody tr:not(.pt) input:checkbox:checked").each(function () {
            TestIds.push($(this).data('ids'));
        });
    }
    var Logic = 'ByFinalPrint';
    var url = config.rootUrl + "/Lab/print/PrintLabReport?visitNo=" + visitNo + "&SubCat=" + SubCat + "&TestIds=" + TestIds.join() + "&Logic=" + Logic;
    window.open(url, '_blank');
}