var _catId = "";
var _subCatId = "";
var _areaId = "";
var _questId = "";
var _AutoId = 0;
$(document).ready(function () {
    CloseSidebar();
    $('select').select2();
    LoadALL();
});
function LoadALL() {
    $('#tblCategoryMaster tbody').empty();
    $('#tblSubCatMaster tbody').empty();
    $('#tblAreaMaster tbody').empty();
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
    objBO.Logic = "LoadALL";
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
                        tbody += "<td>";
                        tbody += "<label class='switch'>";
                        tbody += "<input type='checkbox' data-id=" + val.CatId + " data-logic='UpdateCategoryStatus' onchange=UpdateStatus(this) class='IsActive' id='chkActive' " + val.checked + ">";
                        tbody += "<span class='slider round'></span>";
                        tbody += "</label>";
                        tbody += "</td>";
                        tbody += '<td>' + val.CatId + '</td>';
                        tbody += '<td>' + val.CatName + '</td>';
                        tbody += '<td><button onclick=editCategory(this) class="btn btn-warning btn-xs btnDoc"><i class="fa fa-edit">&nbsp;</i></button></td>';
                        tbody += '</tr>';

                        if (val.checked != '-')
                            $("#ddlCategory").append($("<option></option>").val(val.CatId).html(val.CatName));
                    });
                    $('#tblCategoryMaster tbody').append(tbody);
                    $("#ddlCategory").prop('selectedIndex', '0').change();
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    var tbody1 = "";
                    var temp1 = "";
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (temp1 != val.CatId) {
                            tbody1 += '<tr style="background:#ddd">';
                            tbody1 += '<td colspan="4"><b>Category  :</b> ' + val.CatName + '</td>';
                            tbody1 += '</tr>';
                            temp1 = val.CatId;
                        }
                        tbody1 += '<tr>';
                        tbody1 += "<td>";
                        tbody1 += "<label class='switch'>";
                        tbody1 += "<input type='checkbox' data-id=" + val.SubCatId + " data-logic='UpdateSubCategoryStatus' onchange=UpdateStatus(this) class='IsActive' id='chkActive' " + val.checked + ">";
                        tbody1 += "<span class='slider round'></span>";
                        tbody1 += "</label>";
                        tbody1 += "</td>";
                        tbody1 += '<td class="hide">' + val.CatId + '</td>';
                        tbody1 += '<td>' + val.SubCatId + '</td>';
                        tbody1 += '<td>' + val.SubCatName + '</td>';
                        tbody1 += '<td><button onclick=editSubCategory(this) class="btn btn-warning btn-xs btnDoc"><i class="fa fa-edit">&nbsp;</i></button></td>';
                        tbody1 += '</tr>';
                    });
                    $('#tblSubCatMaster tbody').append(tbody1);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    var tbody2 = "";
                    var temp = "";
                    $.each(data.ResultSet.Table2, function (key, val) {
                        tbody2 += '<tr>';
                        tbody2 += "<td>";
                        tbody2 += "<label class='switch'>";
                        tbody2 += "<input type='checkbox' data-id=" + val.AreaId + " data-logic='UpdateAreaStatus' onchange=UpdateStatus(this) class='IsActive' id='chkActive' " + val.checked + ">";
                        tbody2 += "<span class='slider round'></span>";
                        tbody2 += "</label>";
                        tbody2 += "</td>";
                        tbody2 += '<td>' + val.AreaId + '</td>';
                        tbody2 += '<td>' + val.AreaName + '</td>';
                        tbody2 += '<td><button onclick=editArea(this) class="btn btn-warning btn-xs btnDoc"><i class="fa fa-edit">&nbsp;</i></button>';
                        tbody2 += '&nbsp;<button onclick=ShiftInfo(this) class="btn btn-primary btn-xs btnDoc">Shift</button>';
                        tbody2 += '</td>';
                        tbody2 += '</tr>';
                    });
                    $('#tblAreaMaster tbody').append(tbody2);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function editCategory(elem) {
    _catId = $(elem).closest('tr').find('td:eq(1)').text();
    $('#txtCategory').val($(elem).closest('tr').find('td:eq(2)').text());
    $('#btnSaveCategory').text('Update').addClass('btn-warning').removeClass('btn-success');
}
function InsertCategory() {
    if ($('#txtCategory').val() == '') {
        alert('Please Provide Category Name');
        $('#txtCategory').focus();
        return
    }
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.CatId = _catId;
    objBO.CatName = $('#txtCategory').val();
    objBO.SubCatId = '-';
    objBO.SubCatName = '-';
    objBO.AreaId = '-';
    objBO.AreaName = '-';
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveCategory').text() == 'Submit') ? 'InsertCategory' : 'UpdateCategory';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                Clear();
                LoadALL();
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

function editSubCategory(elem) {
    var catId = $(elem).closest('tr').find('td:eq(1)').text();
    _subCatId = $(elem).closest('tr').find('td:eq(2)').text();
    $('#ddlCategory option').each(function () {
        if ($(this).val() == catId)
            $('#ddlCategory').prop('selectedIndex', '' + $(this).index() + '').change();
    });
    $('#txtSubCategory').val($(elem).closest('tr').find('td:eq(3)').text());
    $('#btnSaveSubCategory').text('Update').addClass('btn-warning').removeClass('btn-success');
}
function InsertSubCategory() {
    if ($('#ddlCategory option:selected').text() == 'Select') {
        alert('Please Select Category');
        return
    }
    if ($('#txtSubCategory').val() == '') {
        alert('Please Provide Sub Category Name');
        $('#txtSubCategory').focus();
        return
    }
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.CatId = $('#ddlCategory option:selected').val();
    objBO.CatName = '-';
    objBO.SubCatId = _subCatId;
    objBO.SubCatName = $('#txtSubCategory').val();
    objBO.AreaId = '-';
    objBO.AreaName = '-';
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveSubCategory').text() == 'Submit') ? 'InsertSubCategory' : 'UpdateSubCategory';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                Clear();
                LoadALL();
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

function editArea(elem) {
    _areaId = $(elem).closest('tr').find('td:eq(1)').text();
    $('#txtArea').val($(elem).closest('tr').find('td:eq(2)').text());
    $('#btnSaveArea').text('Update').addClass('btn-warning').removeClass('btn-success');
}
function InsertAreaMaster() {
    if ($('#txtArea').val() == '') {
        alert('Please Provide Area');
        $('#txtArea').focus();
        return
    }
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.CatId = '-';
    objBO.CatName = '-';
    objBO.SubCatId = '-';
    objBO.SubCatName = '-';
    objBO.AreaId = _areaId;
    objBO.AreaName = $('#txtArea').val();
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveArea').text() == 'Submit') ? 'InsertAreaMaster' : 'UpdateAreaMaster';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                Clear();
                LoadALL();
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
    _AutoId = -1;
    _catId = "";
    _subCatId = "";
    _areaId = "";
    _questId = "";
    $('input:text:not(#txtAreaName)').val('');
    //$('select').prop('selectedIndex', '0').change();
    $('#btnSaveCategory,#btnSaveSubCategory,#btnSaveArea,#btnSaveShift').text('Submit').addClass('btn-success').removeClass('btn-warning');
}

function ShiftInfo(elem) {
    _areaId = $(elem).closest('tr').find('td:eq(1)').text();
    var area = $(elem).closest('tr').find('td:eq(1)').text() + " : " + $(elem).closest('tr').find('td:eq(2)').text()
    $('#txtAreaName').val(area);
    GetAreaShiftInfo();
    $('#modalAreaShift').modal('show')
}
function GetAreaShiftInfo() {
    $('#tblAreaShift tbody').empty();
    var url = config.baseUrl + "/api/master/Care_MasterQueries";
    var objBO = {};
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.AreaId = _areaId;
    objBO.QuestId = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetAreaShiftForMaster";
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
                        tbody += "<td>";
                        tbody += "<label class='switch'>";
                        tbody += "<input type='checkbox' data-id=" + val.AutoId + " data-logic='UpdateAreaShiftStatus' onchange=UpdateStatus(this) class='IsActive' id='chkActive' " + val.checked + ">";
                        tbody += "<span class='slider round'></span>";
                        tbody += "</label>";
                        tbody += "</td>";
                        tbody += '<td class="hide">' + val.AutoId + '</td>';
                        tbody += '<td>' + val.ShiftId + '</td>';
                        tbody += '<td>' + val.AreaName + '</td>';
                        tbody += '<td>' + val.shift_from + '</td>';
                        tbody += '<td>' + val.shift_to + '</td>';
                        tbody += '<td><button onclick=editAreaShift(this) class="btn btn-warning btn-xs btnDoc"><i class="fa fa-edit">&nbsp;</i></button></td>';
                        tbody += '</tr>';
                    });
                    $('#tblAreaShift tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertAreaShiftMaster() {
    if ($('#txtShiftFrom').val() == '') {
        alert('Please Provide Shift From');
        $('#txtShiftFrom').focus();
        return
    }
    if ($('#txtShiftTo').val() == '') {
        alert('Please Provide Shift To');
        $('#txtShiftTo').focus();
        return
    }
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.AutoId = _AutoId;
    objBO.CatId = '-';
    objBO.CatName = '-';
    objBO.SubCatId = '-';
    objBO.SubCatName = '-';
    objBO.AreaId = _areaId;
    objBO.AreaName = '-';
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = $('#txtShiftFrom').val();
    objBO.Prm2 = $('#txtShiftTo').val();
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSaveShift').text() == 'Submit') ? 'InsertAreaShiftMaster' : 'UpdateAreaShiftMaster';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                GetAreaShiftInfo()
                Clear();
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
function editAreaShift(elem) {
    _AutoId = $(elem).closest('tr').find('td:eq(1)').text();
    var From = $(elem).closest('tr').find('td:eq(4)').text();
    var To = $(elem).closest('tr').find('td:eq(5)').text();
    $('#txtShiftFrom').val(From);
    $('#txtShiftTo').val(To);
    $('#btnSaveShift').text('Update').addClass('btn-warning').removeClass('btn-success');
}
function UpdateStatus(elem) {
    var url = config.baseUrl + "/api/master/Care_InsertUpdateMaster";
    var objBO = {};
    objBO.AutoId = $(elem).data('id');
    objBO.CatId = $(elem).data('id');
    objBO.CatName = '-';
    objBO.SubCatId = $(elem).data('id');
    objBO.SubCatName = '-';
    objBO.AreaId = $(elem).data('id');
    objBO.AreaName = '-';
    objBO.QuestId = '-';
    objBO.Question = '-';
    objBO.QuestType = '-';
    objBO.QuestAccessBy = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = $(elem).data('logic');;
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