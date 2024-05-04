var _srno = '';
$(document).ready(function () {
    CloseSidebar();
    GetCategoryList();
    EstimateMasterList();

});
function GetCategoryList() {
    $("#ddlCategory").empty().append($("<option></option>").val("Select").html("Select"));
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateQueries";
    var objBO = {};
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = '-';
    objBO.uhid = '-';
    objBO.patientName = '-';
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.toWhom = '-';
    objBO.var_list = '-';
    objBO.amount = 0;
    objBO.result = '-';
    objBO.estContent = '-';
    objBO.Logic = 'GetCategoryList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $.each(data.ResultSet.Table, function (key, val) {
                $("#ddlCategory").append($("<option></option>").val(val.Category).html(val.Category));
            });
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}

function EstimateMasterList() {
    $('#tblEstimateMaster tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_EstimateQueries";
    var objBO = {};
    objBO.hospId = Active.HospId;
    objBO.estDate = '1900-01-01';
    objBO.estimateNo = '-';
    objBO.uhid = '-';
    objBO.patientName = '-';
    objBO.isActive = '-';
    objBO.createdBy = '-';
    objBO.toWhom = '-';
    objBO.var_list = '-';
    objBO.amount = 0;
    objBO.result = '-';
    objBO.estContent = '-';
    objBO.Logic = 'GetEstimateMaster';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            $.each(data.ResultSet.Table, function (key, val) {
                tbody += "<tr>";
                tbody += "<td>" + val.Category + "</td>";
                tbody += "<td>" + val.Tariff + "</td>";
                tbody += "<td>" + val.ConsultationCharges + "</td>";
                tbody += "<td>" + val.NursingCharges + "</td>";
                tbody += "<td>" + val.RMO_IntensivistCharges + "</td>";
                tbody += "<td>" + val.DieticianCharge + "</td>";
                tbody += "<td>" + val.DailyTotal + "</td>";
                tbody += "<td class='hide'>" + val.SeqNo + "</td>";
                tbody += '<td class="text-center"><button class="btn btn-warning btn-sm" style="height: 21px;width: 38px;margin-bottom:2px;margin-top:-1px;" onclick=EstimateMasterInfo(this)><center style="font-size:11px;margin-top:-5px;margin-left:-1px;">Edit</center></button></td>';
                tbody += "</tr>";
            });

            $('#tblEstimateMaster tbody').append(tbody);
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}
function InsertEstimateMaster() {
    var objBO = {};
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_EstimateMaster";
    objBO.cr_date = '1900-01-01';
    objBO.upd_date = '1900-01-01';
    var category = $('#ddlCategory').val().trim();
    if ($("select#ddlCategory option[value='" + category + "']").length > 0) {
        alert('Category already exists. Please select a different category.');
        Clear();
        return;
    }
    //objBO.Category = $('#ddlCategory').val();
     objBO.Category = category;
    objBO.Tariff = $('#txtTariff').val();
    objBO.ConsultationCharges = $('#txtConsult').val();
    objBO.NursingCharges = $('#txtNursing').val();
    objBO.RMO_IntensivistCharges = $('#txtRMO').val();
    objBO.DieticianCharge = $('#txtDietician').val();
    objBO.SeqNo = $('#txtsrno').val();
    objBO.isActive = '-';
    objBO.createdBy = Active.userId;
    objBO.UpdatedBy = Active.userId;
    objBO.result = '-';
    objBO.Logic = 'Insert';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            Clear();
            EstimateMasterList();
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}

function UpdatEstimateMaster() {
    var objBO = {};
    var url = config.baseUrl + "/api/IPDBilling/Insert_Modify_EstimateMaster";
    objBO.cr_date = '1900-01-01';
    objBO.upd_date = '1900-01-01';
    objBO.Category = $('#ddlCategory').val();
    objBO.Tariff = $('#txtTariff').val();
    objBO.ConsultationCharges = $('#txtConsult').val();
    objBO.NursingCharges = $('#txtNursing').val();
    objBO.RMO_IntensivistCharges = $('#txtRMO').val();
    objBO.DieticianCharge = $('#txtDietician').val();
    objBO.SeqNo = _srno;
    objBO.isActive = '-';
    objBO.createdBy = Active.userId;
    objBO.UpdatedBy = Active.userId;
    objBO.result = '-';
    objBO.Logic = 'Update';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            Clear();
            EstimateMasterList();
        },
        error: function (response) {
            console.error("Error:", response);
            alert('Server Error...!');
        }
    });
}

function EstimateMasterInfo(elem) {
    $('#btnSubmit').text('Update').addClass('btn-warning').removeClass('btn-success').attr('onclick', 'UpdatEstimateMaster()');
    selectRow($(elem));
    var Category = $(elem).closest('tr').find('td:eq(0)').text();
    var Tariff = $(elem).closest('tr').find('td:eq(1)').text();
    var ConsultationCharges = $(elem).closest('tr').find('td:eq(2)').text();
    var NursingCharges = $(elem).closest('tr').find('td:eq(3)').text();
    var RMO_IntensivistCharges = $(elem).closest('tr').find('td:eq(4)').html();
    var DieticianCharge = $(elem).closest('tr').find('td:eq(5)').html();
     _srno = $(elem).closest('tr').find('td:eq(7)').html();
    $('#ddlCategory').val(Category);
    $('#txtTariff').val(Tariff);
    $('#txtConsult').val(ConsultationCharges);
    $('#txtNursing').val(NursingCharges);
    $('#txtRMO').val(RMO_IntensivistCharges);
    $('#txtDietician').val(DieticianCharge);
    $('#txtsrno').val(_srno);
}

function Clear() {
    $('#ddlCategory').val('');
    $('#txtTariff').val('');
    $('#txtConsult').val('');
    $('#txtNursing').val('');
    $('#txtRMO').val('');
    $('#txtDietician').val('');
    $('#btnSubmit').text('Submit').addClass('btn-success').removeClass('btn-warning');
}

