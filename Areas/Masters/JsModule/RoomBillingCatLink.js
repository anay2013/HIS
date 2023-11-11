var _floorId = "";
$(document).ready(function () {
    GetFloorInfo();
    $("#tblRoomInfo tbody").on('change', 'select[id=billingCat]', function () {
        var category = $(this).find('option:selected').text();
        var group = $(this).closest('tr').attr('class');
        $(this).parents('table').find('tbody').find('tr[class=g' + group + ']').each(function () {
            $(this).find('select').find('option').each(function () {
                var elemSelect = $(this).closest('select');
                var val = $(this).text();
                if (val == category)
                    $(elemSelect).prop('selectedIndex', '' + $(this).index() + '').change();
            });
        });
    });
});
function GetFloorInfo() {
    $("#ddlRoomType").empty().append($("<option></option>").val('ALL').html('ALL')).select2();
    var url = config.baseUrl + "/api/RoomAndBed/RoomMasterQueries";
    var objBO = {};
    objBO.floor_name = '-';
    objBO.Logic = 'GetFloorInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $.each(data.ResultSet.Table1, function (key, val) {
                $("#ddlRoomType").append($("<option></option>").val(val.RoomTypeId).html(val.RoomTypeName));
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetRoomInfo() {
    $("#tblRoomInfo tbody").empty();
    var url = config.baseUrl + "/api/RoomAndBed/RoomMasterQueries";
    var objBO = {};
    objBO.room_type = $("#ddlRoomType option:selected").val();
    objBO.Logic = 'GetRoomInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                var tbody = "";
                var temp = "";
                var count = 0;
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++
                        if (temp != val.RoomTypeName) {
                            tbody += '<tr style="background:#f5e9c4" class="' + val.RoomTypeId + '">';
                            tbody += '<td colspan="5">' + val.RoomTypeName + '</td>';
                            tbody += '<td>';
                            tbody += '<select id="billingCat">';
                            $.each(data.ResultSet.Table1, function (key, val) {
                                tbody += '<option>' + val.RoomBillingType;
                            });
                            tbody += '</select>';
                            tbody += '</td>';
                            tbody += "<td><button onclick=UpdateRoomMaster(this) class='btn btn-success btn-xs'><i class='fa fa-check-circle'>&nbsp;</i>Submit</button></td>";
                            tbody += '</tr>';
                            temp = val.RoomTypeName;
                            count = 1;
                        }
                        tbody += '<tr class="g' + val.RoomTypeId + '">';
                        tbody += '<td>' + count + '</td>';
                        tbody += '<td class="hide">' + val.RoomTypeId + '</td>';
                        tbody += '<td class="hide">' + val.RoomBedId + '</td>';
                        tbody += '<td class="hide">' + val.FloorName + '</td>';
                        tbody += '<td><input type="text" class="form-control" value="' + val.RoomName + '"/></td>';
                        tbody += '<td><input type="text" class="form-control" value="' + val.RoomNo + '"/></td>';
                        tbody += '<td><input type="text" class="form-control" value="' + val.bedNo + '"/></td>';
                        tbody += '<td>' + val.roomFullName + '</td>';
                        tbody += '<td>';
                        tbody += '<select>';
                        $.each(data.ResultSet.Table1, function (key, val1) {
                            tbody += (val.RoomBillingCategory == val1.RoomBillingType) ? '<option selected>' + val1.RoomBillingType : '<option>' + val1.RoomBillingType;
                        });
                        tbody += '</select>';
                        tbody += '</td>';
                        tbody += "<td><button onclick=UpdateRoomMaster(this) class='btn btn-success btn-xs'><i class='fa fa-check-circle'>&nbsp;</i>Submit</button></td>";
                        tbody += '</tr>';
                    });
                    $("#tblRoomInfo tbody").append(tbody);
                    $("#tblRoomInfo select").select2();
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateRoomMaster(elem) {
    var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
    var objBO = [];
    var lenght = $(elem).closest('tr').find('td').length;
    var cls = $(elem).closest('tr').attr('class');
    var indx = $(elem).closest('tr').index();
    if (eval(lenght)<5) {
        $('#tblRoomInfo tbody tr[class=g' + cls + ']').each(function () {
            objBO.push({
                'hosp_id': Active.unitId,
                'room_id': $(this).find('td:eq(2)').text(),
                'room_name': $(this).find('td:eq(4)').find('input[type=text]').val(),
                'RoomNo': $(this).find('td:eq(5)').find('input[type=text]').val(),
                'bedNo': $(this).find('td:eq(6)').find('input[type=text]').val(),
                'billing_category': $(this).find('td:eq(8)').find('select option:selected').val(),
                'RoomTypeId': $(this).find('td:eq(1)').text(),
                'floor_name': $(this).find('td:eq(3)').text(),
                'login_id': Active.userId,
                'Logic': 'UpdateRoomMaster',
            });
        });
    } else {
        $('#tblRoomInfo tbody tr').eq(indx).each(function () {
            objBO.push({
                'hosp_id': Active.unitId,
                'room_id': $(this).find('td:eq(2)').text(),
                'room_name': $(this).find('td:eq(4)').find('input[type=text]').val(),
                'RoomNo': $(this).find('td:eq(5)').find('input[type=text]').val(),
                'bedNo': $(this).find('td:eq(6)').find('input[type=text]').val(),
                'billing_category': $(this).find('td:eq(8)').find('select option:selected').val(),
                'RoomTypeId': $(this).find('td:eq(1)').text(),
                'floor_name': $(this).find('td:eq(3)').text(),
                'login_id': Active.userId,
                'Logic': 'UpdateRoomMaster',
            });
        });
    }   
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data == 'Success') {
                alert(data);
                $('#tblRoom tbody').empty();
                $('input[id=txtRoomNo],input[id=txtRoomName],input[id=txtTotalBed]').val('');
                $('#ddlFloor,#ddlRoomType').prop('selectedIndex', '0').change();
            }
            else {
                alert(data);
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}



