$(document).ready(function () {
    Onload();
});
function Onload() {
    $("#tblReoprt tbody").empty();
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = "-";
    objBO.prm_2 = "-";
    objBO.prm_3 = "-";
    objBO.from = "1999-01-01";
    objBO.to = "1999-01-01";
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "LoadSubCategory";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            var htmldata = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        htmldata += '<tr>';
                        htmldata += '<td style="width:30%;text-align:center">' + val.SubCatID + '</td>';
                        htmldata += '<td style="width:60%;">' + val.SubCatName + '</td>';
                        htmldata += '<td style="width:10%;text-align:center"><input type="checkbox"/></td>';
                        htmldata += '</tr>';
                    });
                    $("#tblReoprt tbody").append(htmldata);
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
function GetEmpInfo() {
    $('#ddlEmployee').empty().append($("<option></option>").val("-").html("Select")).select2();
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = $("#txtempName").val();
    objBO.prm_2 = "-";
    objBO.prm_3 = "-";
    objBO.from = "1999-01-01";
    objBO.to = "1999-01-01";
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "SearchEmployeeName";
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
                        $("#ddlEmployee").append($("<option></option>").val(val.emp_code).html(val.emp_name));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LinkSubcatEmp(logic) {
    var url = config.baseUrl + "/api/Share/Share_InsertUpdateMaster";
    var objBO = {};
    var SubCatList = [];
    if (logic == 'LinkSubCatEmp') {
        SubCatList = [];
        $('#tblReoprt tbody tr input:checkbox:checked').each(function () {
            SubCatList.push($(this).closest('tr').find('td:eq(0)').text())
        });
    }
    //objBO.AutoId = '-';
    objBO.Emp_code = '-';
    objBO.SubCatId = '-';
    objBO.Cr_date = '1999-01-01'
    objBO.Prm1 = SubCatList.join('|');
    objBO.Prm2 = $('#ddlEmployee option:selected').val();
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                $('input:checkbox').removeAttr('checked');
                GetEmpLinkData();

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
function GetEmpLinkData() {
    $('#tblReoprtInfo tbody').empty();
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = $('#ddlEmployee option:selected').val();
    objBO.prm_2 = "-";
    objBO.prm_3 = "-";
    objBO.from = "1999-01-01";
    objBO.to = "1999-01-01";
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "GetEmpLinkInfo";
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td style="width:25%;">' + val.Emp_code + '</td>';
                        tbody += '<td style="width:25%;">' + val.SubCatId + '</td>';
                        tbody += '<td style="width:25%;">' + val.Cr_date + '</td>';
                        tbody += '<td style="width:25%;">' + val.Login_id + '</td>';
                        tbody += '</tr>';
                    });
                    $('#tblReoprtInfo tbody').append(tbody);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}