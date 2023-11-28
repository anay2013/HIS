$(document).ready(function () {
    GetAreaList();
});
function GetEmpInfo() {
    $('#ddlEmployee').empty().append($("<option></option>").val("-").html("Select")).select2();
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.AreaId = '-';
    objBO.QuestId = '-';
    objBO.Prm1 = $('#txtEmpName').val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetEmpInfo";
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
function GetAreaLinkInfo() {
    $('#tblAreaInfo tbody').empty();
    $('#tblAllotedAreaInfo tbody').empty();
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.AreaId = '-';
    objBO.QuestId = '-';
    objBO.Prm1 = $('#ddlEmployee option:selected').val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetAreaLinkInfo";
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
                    var temp1 = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += "<td><input type='checkbox'/></td>";
                        tbody += '<td>' + val.AreaId + '</td>';
                        tbody += '<td>' + val.AreaName + '</td>';
                        tbody += '</tr>';
                    });
                    $('#tblAreaInfo tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody1 = "";
                    var temp2 = "";
                    var temp3 = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        tbody1 += '<tr>';
                        tbody1 += "<td><input type='checkbox'/></td>";
                        tbody1 += '<td>' + val.AreaId + '</td>';
                        tbody1 += '<td>' + val.AreaName + '</td>';
                        tbody1 += '</tr>';
                    });
                    $('#tblAllotedAreaInfo tbody').append(tbody1);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LinkAreaAndEmp(logic) {
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    var areaList = [];
    if (logic == 'LinkAreaAndEmp') {
        areaList = [];
        $('#tblAreaInfo tbody tr input:checkbox:checked').each(function () {
            areaList.push($(this).closest('tr').find('td:eq(1)').text())
        });
    }
    if (logic == 'DelinkAreaAndEmp') {
        areaList = [];
        $('#tblAllotedAreaInfo tbody tr input:checkbox:checked').each(function () {
            areaList.push($(this).closest('tr').find('td:eq(1)').text())
        });
    }
    objBO.CatId = '-';
    objBO.CatName = '-';
    objBO.SubCatId = '-';
    objBO.SubCatName = '-';
    objBO.AreaId = '-';
    objBO.AreaName = '-';
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = areaList.join('|');
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
                GetAreaLinkInfo();
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