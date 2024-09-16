$(document).ready(function () {
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');
    OnloadList();
    OnLoadDoctorList();
    $("#chkallshift").change(function () {
        if (this.checked) {
            $(".shiftchk").each(function () {
                this.checked = true;
            })
        } else {
            $(".shiftchk").each(function () {
                this.checked = false;
            })
        }
    });
});
function OnloadList() {
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
    objBO.Logic = "OnLoadData";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#ddlDoctor').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDoctor').append($('<option></option>').val(val.DoctorId).html(val.DoctorName)).select2();
                    });
                }

                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $("#ddlCategory").append($("<option selected></option>").val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table1, function (key, value) {
                        $("#ddlCategory").append($("<option></option>").val(value.CatID).html(value.CatName));
                    });
                }

                if (Object.keys(data.ResultSet.Table2).length > 0) {
                    $("#ddlSubCategory").append($("<option selected></option>").val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table2, function (key, value) {
                        $("#ddlSubCategory").append($("<option></option>").val(value.SubCatID).html(value.SubCatName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function OnLoadDoctorList() {
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
    objBO.Logic = "OnLoadDoctor";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#ddlDoctorShift').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDoctorShift').append($('<option></option>').val(val.DoctorId).html(val.DoctorName)).select2();
                    });
                }
               
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function GetDoctorShiftList() {
    debugger
    $("#tblReoprt tbody").empty();
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = $('#ddlDoctor option:selected').val();
    objBO.prm_2 = $('#ddlSubCategory option:selected').val();
    objBO.prm_3 = $('#ddlCategory option:selected').val();
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txttodate").val();
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "DoctorListShift";
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
                        tbody += '<tr>';
                        tbody += '<td style="width:5%;text-align:center;"><input id="chkshift" data-autoid="' + val.auto_id + '" type="checkbox" class="shiftchk"> </td>';
                        tbody += '<td style="width:13%">' + val.TnxId + '</td>';
                        tbody += '<td style="width:12%">' + val.tnxDate + '</td>';
                        tbody += '<td hidden>' + val.DoctorId + '</td>';
                        tbody += '<td style="width:15%">' + val.DoctorName + '</td>';
                        tbody += '<td style="width:20%">' + val.ShiftedDoctorName + '</td>';
                        tbody += '<td hidden>' + val.ItemId + '</td>';
                        tbody += '<td style="width:15%">' + val.ItemName + '</td>';
                        tbody += '<td style="width:8%;text-align:center;">' + val.panel_rate + '</td>';
                        tbody += '<td style="width:8%;text-align:center;">' + val.discount + '</td>';
                        tbody += '<td style="width:8%;text-align:center;">' + val.amount + '</td>';
                        tbody += '</tr>';
                    });
                    $("#tblReoprt tbody").append(tbody);
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
function GetItemListWise() {
    $("#tblReoprt tbody").empty();
    var url = config.baseUrl + "/api/Share/ShareQueries";
    var objBO = {};
    objBO.hosp_id = "-";
    objBO.prm_1 = $('#txtName').val();
    objBO.prm_2 = "-";
    objBO.prm_3 = "-";
    objBO.from = $("#txtfromdate").val();
    objBO.to = $("#txttodate").val();
    objBO.subcatid = "-";
    objBO.login_id = "-";
    objBO.Logic = "ItemShiftSearch";
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
                        tbody += '<tr>';
                        tbody += '<td style="width:5%;text-align:center;"><input id="chkshift" data-autoid="' + val.auto_id + '" type="checkbox" class="shiftchk"> </td>';
                        tbody += '<td style="width:13%">' + val.TnxId + '</td>';
                        tbody += '<td style="width:12%">' + val.tnxDate + '</td>';
                        tbody += '<td hidden>' + val.DoctorId + '</td>';
                        tbody += '<td style="width:15%">' + val.DoctorName + '</td>';
                        tbody += '<td style="width:15%">' + val.ShiftedDoctorName + '</td>';
                        tbody += '<td hidden>' + val.ItemId + '</td>';
                        tbody += '<td style="width:10%">' + val.ItemName + '</td>';
                        tbody += '<td style="width:10%;text-align:center;">' + val.panel_rate + '</td>';
                        tbody += '<td style="width:10%;text-align:center;">' + val.discount + '</td>';
                        tbody += '<td style="width:10%;text-align:center;">' + val.amount + '</td>';
                        tbody += '</tr>';
                    });
                    $("#tblReoprt tbody").append(tbody);
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
function SaveDoctorShift() {
    var url = config.baseUrl + "/api/Share/Share_InsertUpdateMaster";
    var objBO = {};
    var DoctorShiftListData = []
    $('#tblReoprt tbody').find('tr').each(function () {
        var ischecked = $(this).find('input[type="checkbox"]').is(':checked');
        if (ischecked) {
            DoctorShiftListData.push($(this).find('td:eq(0)').find('input').data('autoid'));
        }
    });
    if ($('#ddlDoctorShift').val() == 'Select') {
        alert('Please Select Doctor Name');
        $('#ddlDoctorShift').focus();
        return
    }
    objBO.AutoId = '0';
    objBO.Emp_code = '-';
    objBO.SubCatId = '-';
    objBO.Cr_date = '1999-01-01'
    objBO.Prm1 = $('#ddlDoctorShift option:selected').val();
    objBO.Prm2 = DoctorShiftListData.join('|');
    objBO.login_id = Active.userId;
    objBO.Logic = 'UpdateDoctorShift';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetDoctorShiftList();
                $('input:checkbox').removeAttr('checked');
                $('#ddlDoctorShift').prop('selectedIndex', '0').change();
                
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
