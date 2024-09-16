
var LabCodes = "";
var _LabCode = "";
var _vistcode = "";
var _testcode = "";
var _barcode = "";
$(document).ready(function () {
    GetDispatchLabInfo();
    GetDispatchList();
    $('#btnGetEmp').on('click', function () {
        var name = $('#txtEmpName').val();
        GetEmployeeList(name);
    });

    $('#tblDispatch tbody').on('click', '.btnDispatch', function () {
        debugger
        $('#modalDispatch').modal('show');
        LabCodes = $(this).closest('tr').find('td:eq(1)').text();

    });
    $('#tblDispatch tbody').on('click', '#btndelete', function () {
        debugger
        _LabCode = $(this).closest('tr').find('td:eq(2)').text();
        _vistcode = $(this).closest('tr').find('td:eq(3)').text();
        _testcode = $(this).closest('tr').find('td:eq(4)').text();
        _barcode = $(this).closest('tr').find('td:eq(5)').text();
        deleteRow();
    });

});
function GetEmployeeList(name) {
    var url = config.baseUrl + "/api/warehouse/wh_ConsumptionQueries";
    var objConsumpBO = {};
    objConsumpBO.prm_1 = name;
    objConsumpBO.Logic = "GetEmployeeList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objConsumpBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $("#ddlEmployee").empty().append($("<option></option>").val(0).html('Select Employee'));
                $('#txtEmpName').val('');
                $.each(data.ResultSet.Table, function (key, value) {
                    $("#ddlEmployee").append($("<option></option>").val(value.emp_code).html(value.emp_name)).select2();
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDispatchLabInfo() {
    var url = config.baseUrl + "/api/Lab/LabOutSourceQueries";
    var objBO = {};
    objBO.subcatId = $('#ddlCategory option:selected').val();
    objBO.prm1 = "Y";
    objBO.logic = "GetTestBySubCategory";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $("#ddlCategory").append($("<option></option>").val("ALL").html("ALL"));
                    $.each(data.ResultSet.Table1, function (key, value) {
                        $("#ddlCategory").append($("<option></option>").val(value.LabCode).html(value.LabName)).select2();
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
function AddDispatchNo() {
    var labname = $('#ddlCategory option:selected').val();
    if (labname == "ALL") {
        alert("Please select Lab");
        $('#ddlCategory').focus();
        return;
    }
    var url = config.baseUrl + "/api/Lab/Lab_DispatchOneByOne";
    var objBO = {};
    objBO.labCode = '-';
    objBO.DispatchLab = $('#ddlCategory option:selected').val();
    objBO.DispatchNo = '-';
    objBO.BarCodeNo = $("#txtBarcode").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'Insert:dispatchNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            debugger
            if (data != true) {
                $('#btnadd').prop('disabled', true);
                $("#txtcode").val(data).prop('disabled', true);
                $('#ddlCategory').prop('disabled', true);
                GetDispatchList();
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
function GetDispatchList() {
    $('#tblDispatch tbody').empty();
    var url = config.baseUrl + "/api/Lab/SampleDispatchQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.VisitNo = '-';
    objBO.BarcodeNo = '-';
    objBO.DispatchLabCode = '-';
    objBO.TestCode = '-';
    objBO.from = '1999-01-01';
    objBO.to = '1999-01-01';
    objBO.Prm1 = $("#txtcode").val();
    objBO.logic = 'DispatchNoList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = '';
            var count = 0;
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (val.IsLocalTest == 0) {
                            if (temp != val.LabName) {
                                tbody += "<tr style='background:#60d9606b;'>";
                                tbody += "<td style='font-size:13px;' colspan='20'><b>" + val.LabName + "</b><button class='pull-right btn btn-warning btn-xs btnDispatch'>Dispatch</button></td>";
                                tbody += "<td hidden>" + val.LabCode + "</td>";
                                tbody += "</tr>";
                                temp = val.LabName
                            }
                            tbody += "</tr>";
                            tbody += "<td>" + count + "</td>";
                            tbody += "<td hidden>" + val.AutoTestId + "</td>";
                            tbody += "<td hidden>" + val.LabCode + "</td>";
                            tbody += "<td>" + val.visitNo + "</td>";
                            tbody += "<td hidden>" + val.TestCode + "</td>";
                            tbody += "<td>" + val.barcodeNo + "</td>";
                            tbody += "<td>" + val.patient_name + "</td>";
                            tbody += "<td>" + val.date + "</td>";
                            tbody += "<td>" + val.gender + "</td>";
                            tbody += "<td>" + val.ageInfo + "</td>";
                            tbody += "<td>" + val.testList + "</td>";
                            tbody += "<td hidden>" + val.LabName + "</td>";
                            tbody += "<td style='width:6%'><button class='btn btn-danger btn-xs' id='btndelete'><i class='fa fa-remove'></i></button></td>";
                            tbody += "</tr>";
                        }
                        else {
                            tbody += "<tr>";
                            tbody += "<td colspan='20'>No Data Found</td>";
                            tbody += "</tr>";
                        }
                    });
                    $('#tblDispatch tbody').append(tbody);
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
function FinalSubmitDispatch() {
    if ($('#ddlemployee option:selected').text() == 'select employee') {
        alert('Please select an employee.');
        return;
    }
    var url = config.baseUrl + "/api/Lab/Lab_DispatchOneByOne";
    var objBO = {};
    objBO.labCode = LabCodes;
    objBO.DispatchLab = '-';
    objBO.DispatchNo = '-';
    objBO.BarCodeNo = $("#txtBarcode").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'SampleDispatchOneByOne';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            debugger
            if (data.includes('Success')) {
                alert(data);
                $("#modalDispatch").modal('hide');
                GetDispatchList();
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
function deleteRow() {
    var url = config.baseUrl + "/api/Lab/Lab_DispatchOneByOne";
    var objBO = {};
    objBO.labCode = _LabCode;
    objBO.DispatchLab = '-';
    objBO.DispatchNo = '-';
    objBO.BarCodeNo = _barcode;
    objBO.Prm1 = _vistcode;
    objBO.Prm2 = _testcode;
    objBO.login_id = Active.userId;
    objBO.Logic = 'DeleteDispatchOneByOne';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            debugger
            if (data.includes('Success')) {
                //alert("Delete successfully");
                GetDispatchList();
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