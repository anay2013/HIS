var _questId = "";
$(document).ready(function () {
    $('select').select2();
    GetQuestInfo();
});
function trans() {
    google.load("elements", "1", {
        packages: "transliteration"
    });

    var englishText = $("#txtQuestion").val();
    var translate = new google.translate.Translate();
    var hindiText = translate.translate(englishText, {
        "target": "hi"
    });
    $("#txtQuestion").val(hindiText);
}
function GetQuestInfo() {
    $('#tblQuestMaster tbody').empty();
    $('#ddlCategory').empty().append($("<option></option>").val("-").html("Select"));
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.AreaId = '-';
    objBO.QuestId = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetQuestInfoForMaster";
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.SubCatId) {
                            tbody += '<tr style="background:#ddd">';
                            tbody += '<td colspan="6"><b>Sub Category  :</b> ' + val.SubCatName + '</td>';
                            tbody += '</tr>';
                            temp = val.SubCatId;
                        }
                        tbody += '<tr>';
                        tbody += "<td>";
                        tbody += "<label class='switch'>";
                        tbody += "<input type='checkbox' onchange=UpdateStatus('" + val.QuestId + "') class='IsActive' id='chkActive' " + val.checked + ">";
                        tbody += "<span class='slider round'></span>";
                        tbody += "</label>";
                        tbody += "</td>";
                        tbody += '<td class="hide" data-subcatid=' + val.SubCatId + '>' + val.SubCatName + '</td>';
                        tbody += '<td>' + val.QuestId + '</td>';
                        tbody += '<td>' + val.Question + '</td>';
                        tbody += '<td>' + val.QuestType + '</td>';
                        tbody += '<td>' + val.QuestAccessBy + '</td>';
                        tbody += '<td><button onclick=editQuest(this) class="btn btn-warning btn-xs btnDoc"><i class="fa fa-edit">&nbsp;</i></button></td>';
                        tbody += '</tr>';
                    });
                    $('#tblQuestMaster tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $("#ddlCategory").append($("<option></option>").val(val.CatId).html(val.CatName));
                    });
                    $("#ddlCategory").prop('selectedIndex', '0').change();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetSubCat() {
    $('#ddlSubCategory').empty().append($("<option></option>").val("-").html("Select"));
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = $('#ddlCategory option:selected').val();;
    objBO.SubCatId = '-';
    objBO.AreaId = '-';
    objBO.QuestId = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetSubCatByCatId";
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
                        $("#ddlSubCategory").append($("<option></option>").val(val.SubCatId).html(val.SubCatName));
                    });
                    $("#ddlSubCategory").prop('selectedIndex', '0').change();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function editQuest(elem) {
    selectRow(elem)
    var subCatId = $(elem).closest('tr').find('td:eq(1)').data('subcatid');
    var subCatName = $(elem).closest('tr').find('td:eq(1)').text();
    $("#ddlSubCategory").empty().append($("<option></option>").val(subCatId).html(subCatName)).change();
    _questId = $(elem).closest('tr').find('td:eq(2)').text();
    var question = $(elem).closest('tr').find('td:eq(3)').text();
    var QuestType = $(elem).closest('tr').find('td:eq(4)').text();
    var AccessBy = $(elem).closest('tr').find('td:eq(5)').text();
    $('#txtQuestion').val(question);
    $('#ddlQuestType option').each(function () {
        if ($(this).val() == QuestType)
            $('#ddlQuestType').prop('selectedIndex', '' + $(this).index() + '').change();
    });
    $('#ddlAccessBy option').each(function () {
        if ($(this).val() == AccessBy)
            $('#ddlAccessBy').prop('selectedIndex', '' + $(this).index() + '').change();
    });
    $('#btnSave').text('Update').addClass('btn-warning').removeClass('btn-success');
}
function UpdateStatus(questId) {
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.CatId = '-';
    objBO.CatName = '-';
    objBO.SubCatId = '-';
    objBO.SubCatName = '-';
    objBO.AreaId = '-';
    objBO.AreaName = '-';
    objBO.QuestId = questId;
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'UpdateQuestStatus';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                //alert(data);             
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
function InsertQuest() {
    if ($('#txtCategory').val() == '') {
        alert('Please Provide Category Name');
        $('#txtCategory').focus();
        return
    }
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.CatId = '-';
    objBO.CatName = '-';
    objBO.SubCatId = $('#ddlSubCategory option:selected').val();
    objBO.SubCatName = '-';
    objBO.AreaId = '-';
    objBO.AreaName = '-';
    objBO.QuestId = _questId;
    objBO.Question = $('#txtQuestion').val();
    objBO.QuestType = $('#ddlQuestType option:selected').val();
    objBO.QuestAccessBy = $('#ddlAccessBy option:selected').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSave').text() == 'Submit') ? 'InsertQuestMaster' : 'UpdateQuestMaster';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                Clear();    
                alert(data);
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
function Clear() {
    _questId = "";
    $('textarea').val('');
    $('input:text').val('');
    $('#ddlCategory,#ddlSubCategory,#ddlQuestType,#ddlAccessBy').prop('selectedIndex', '0').change();
    $('#btnSave').text('Submit').addClass('btn-success').removeClass('btn-warning');
}