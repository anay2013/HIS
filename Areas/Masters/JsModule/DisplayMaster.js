$(document).ready(function () {
    /// alert("gff");

    $('#tblDisplayMaster tbody').on('click', '.DisplayMasterName', function () {
        var display = $('span[data-dname]').text($(this).data('dname'));
        $('span[data-ddid]').text($(this).data('ddid'));
        var displayId = $('span[data-ddid]').text();
        selectRow(this);
        DoctorList(displayId);
    });
    GetDisplayMaster();
});


function InsertDisplayMaster() {
    if ($('#txtDisplayName').val() == '') {
        alert('Please Enter Display Name');
        $('#txtDisplayName').focus();
        return
    }
    var url = config.baseUrl + "/api/RoomAndBed/InsertUpdateDisplayMaster";
    var objBO = {};
    objBO.DisplayId = '-';
    objBO.DisplayName = $('#txtDisplayName').val();
    objBO.DoctorId = '-';
    objBO.DoctorName = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "InsertDisplay";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetDisplayMaster();
                $('#txtDisplayName').val('');
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
function GetDisplayMaster() {
    $('#tblDisplayMaster tbody').empty();
    var url = config.baseUrl + "/api/RoomAndBed/DisplayMasterQueries";
    var objBO = {};
    objBO.DisplayId = '-';
    objBO.DisplayName = '-';
    objBO.DoctorId = '-';
    objBO.DoctorName = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "OnLoadForDisplay";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td>' + val.DisplayId + '</td>';
                        tbody += '<td>' + val.DisplayLocation + '</td>';
                        //tbody += '<td><button class="btn btn-success DisplayMasterName btn-xs"><i class="fa fa-arrow-right"></i></button></td>';
                        tbody += '<td><button class="btn btn-success DisplayMasterName btn-xs" data-dname="' + val.DisplayLocation + '", data-ddid="' + val.DisplayId + '"><i class="fa fa-arrow-right"></i></button></td>';

                        tbody += '</tr>';
                    });
                    $('#tblDisplayMaster tbody').append(tbody);
                }
            }

            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlDoctorList').empty().append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlDoctorList').append($('<option></option>').val(val.DoctorId).html(val.DoctorName));

                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DoctorList(displayid) {

    $('#tblDoctorDetails tbody').empty();
    var tbody = "";
    var url = config.baseUrl + "/api/RoomAndBed/DisplayMasterQueries";
    var objBO = {};
    objBO.DisplayId = '-';
    objBO.DisplayName = '-';
    objBO.DoctorId = '-';
    objBO.DoctorName = '-';
    objBO.prm_1 = displayid;
    objBO.prm_2 = '-';
    objBO.autoId = '-';
    objBO.hosp_id = '-';
    objBO.login_id = '-';

    objBO.Logic = 'DoctorListInDisplay';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td>' + val.DoctorId + '</td>';
                        tbody += '<td>' + val.DoctorName + '</td>';
                        tbody += "<td>";
                        tbody += "<label class='switch'>";
                        tbody += "<input type='checkbox' data-doctorid=" + val.DoctorId + " data-logic='ActiveStatus' onchange=ActiveDoctorListstatus(this) class='IsActive' id='chkActive' " + val.checked + ">";
                        tbody += "<span class='slider round'></span>";
                        tbody += "</label>";
                        tbody += "</td>";
                        tbody += '</tr>';
                    });
                    $('#tblDoctorDetails tbody').append(tbody);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertDoctorDisplay() {
    var ddid = $('[data-ddid]').text();

    var url = config.baseUrl + "/api/RoomAndBed/InsertUpdateDisplayMaster";
    var objBO = {};
    objBO.DisplayId = ddid;
    objBO.DisplayName = '-';
    objBO.DoctorId = $('#ddlDoctorList option:selected').val();
    objBO.DoctorName = $('#ddlDoctorList option:selected').text();;
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = "MapDoctorWithDisplay";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                DoctorList(ddid)
                //$('#ddlDoctorList').val('');
                //$('#ddlDoctorList option:selected').val('Select');
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
function ActiveDoctorListstatus(elem) {
    var ddid = $('[data-ddid]').text();
    var url = config.baseUrl + "/api/RoomAndBed/InsertUpdateDisplayMaster";
    var objBO = {};
    objBO.DisplayId = ddid;
    objBO.DisplayName = '-';
    objBO.DoctorId = $(elem).data('doctorid');
    objBO.DoctorName = '-';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'UpdateStatus';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            //if (data.includes('Success')) {
            //    if (objBO.Logic == 'ActiveStatus')
            //        DoctorList(ddid);
            //}
            DoctorList(ddid);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}