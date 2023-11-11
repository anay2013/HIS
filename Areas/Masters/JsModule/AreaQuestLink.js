$(document).ready(function () {
    GetAreaList();
});
function GetAreaList() {
    $('#ddlArea').empty().append($("<option></option>").val("-").html("Select")).select2();
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.AreaId = '-';
    objBO.QuestId = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetAreaList";
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
                        $("#ddlArea").append($("<option></option>").val(val.AreaId).html(val.AreaName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetQuestLinkInfo() {
    $('#tblQuestInfo tbody').empty();
    $('#tblAllotedQuestInfo tbody').empty();
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.AreaId = $('#ddlArea option:selected').val();
    objBO.QuestId = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetQuestLinkInfo";
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
                        if (temp != val.CatId) {
                            tbody += '<tr style="background:#c0eafe">';
                            tbody += '<td colspan="5"><b>Category  :</b> ' + val.CatName + '</td>';
                            tbody += '</tr>';
                            temp = val.CatId;
                        }
                        if (temp1 != val.SubCatId) {
                            tbody += '<tr style="background:#fff6cf">';
                            tbody += '<td colspan="5"><b>Sub Category  :</b> ' + val.SubCatName + '</td>';
                            tbody += '</tr>';
                            temp1 = val.SubCatId;
                        }
                        tbody += '<tr>';
                        tbody += "<td><input type='checkbox'/></td>";
                        tbody += '<td>' + val.QuestId + '</td>';
                        tbody += '<td>' + val.Question + '</td>';
                        tbody += '<td>' + val.QuestType + '</td>';
                        tbody += '<td>' + val.QuestAccessBy + '</td>';
                        tbody += '</tr>';
                    });
                    $('#tblQuestInfo tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody1 = "";
                    var temp2 = "";
                    var temp3 = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (temp2 != val.CatId) {
                            tbody1 += '<tr style="background:#c0eafe">';
                            tbody1 += '<td colspan="5"><b>Category  :</b> ' + val.CatName + '</td>';
                            tbody1 += '</tr>';
                            temp2 = val.CatId;
                        }
                        if (temp3 != val.SubCatId) {
                            tbody1 += '<tr style="background:#fff6cf">';
                            tbody1 += '<td colspan="5"><b>Sub Category  :</b> ' + val.SubCatName + '</td>';
                            tbody1 += '</tr>';
                            temp3 = val.SubCatId;
                        }
                        tbody1 += '<tr>';
                        tbody1 += "<td><input type='checkbox'/></td>";
                        tbody1 += '<td>' + val.QuestId + '</td>';
                        tbody1 += '<td>' + val.Question + '</td>';
                        tbody1 += '<td>' + val.QuestType + '</td>';
                        tbody1 += '<td>' + val.QuestAccessBy + '</td>';
                        tbody1 += '</tr>';
                    });
                    $('#tblAllotedQuestInfo tbody').append(tbody1);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LinkAreaAndQuest(logic) {
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    var questList = [];
    if (logic == 'LinkAreaAndQuest') {
        questList = [];
        $('#tblQuestInfo tbody tr input:checkbox:checked').each(function () {
            questList.push($(this).closest('tr').find('td:eq(1)').text())
        });
    } 
    if (logic == 'DelinkAreaAndQuest') {
        questList = [];
        $('#tblAllotedQuestInfo tbody tr input:checkbox:checked').each(function () {
            questList.push($(this).closest('tr').find('td:eq(1)').text())
        });
    } 
    objBO.CatId = '-';
    objBO.CatName = '-';
    objBO.SubCatId = '-';
    objBO.SubCatName = '-';
    objBO.AreaId = $('#ddlArea option:selected').val();
    objBO.AreaName = '-';
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = questList.join('|');
    objBO.Prm2 = '-';
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
                GetQuestLinkInfo();
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