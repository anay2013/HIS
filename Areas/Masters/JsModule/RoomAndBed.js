var _floorId = "";
$(document).ready(function () {
    GetFloorInfo();
});
function GetFloorInfo() {
    $("#ddlFloor").empty().append($('<option>Select Floor</option>')).select2();
    $("#ddlRoomType").empty().append($('<option>Select Room Type</option>')).select2();
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
            $.each(data.ResultSet.Table, function (key, val) {
                $("#ddlFloor").append($("<option></option>").val(val.FloorID).html(val.FloorName));
            });
            $.each(data.ResultSet.Table1, function (key, val) {
                $("#ddlRoomType").append($("<option></option>").val(val.RoomTypeId).html(val.RoomTypeName));
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function floorTask() {
    GetFloorMaster();
    $('#modalFloorMaster').modal('show');
}
function GetFloorMaster() {
    $("#tblFloorMaster tbody").empty();
    var url = config.baseUrl + "/api/RoomAndBed/RoomMasterQueries";
    var objBO = {};
    objBO.floor_name = '-';
    objBO.Logic = 'GetFloorMaster';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td class="hide">' + val.FloorID + '</td>';
                        tbody += "<td>" +
                            '<label class="switch">' +
                            '<input onchange=FloorStatus("' + val.FloorID + '") type="checkbox" id="chkActive" ' + val.checked + '>' +
                            '<span class="slider round"></span>' +
                            '</label>' +
                            "</td>";
                        tbody += '<td>' + val.FloorName + '</td>';
                        tbody += '<td><button onclick=floorInfo(this) class="btn btn-warning btn-xs"><i class="fa fa-sign-in"></i></button></td>';
                    });
                    $("#tblFloorMaster tbody").append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function floorInfo(elem) {
    var floorId = $(elem).closest('tr').find('td:eq(0)').text();
    var floorName = $(elem).closest('tr').find('td:eq(2)').text();
    $('#txtFloorName').val(floorName);
    $('#btnSaveFloor').html('<i class="fa fa-edit">&nbsp;</i>Update');
    _floorId = floorId;
}
function Clear() {
    $('#txtFloorName').val('');
    $('#btnSaveFloor').html('<i class="fa fa-plus-circle">&nbsp;</i>Add');
    _floorId = "";
}
function FloorStatus(floorId) {
    var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
    var objBO = [];
    objBO.push({
        'FloorID': floorId,
        'room_id': '-',
        'room_name': '-',
        'room_type': '-',
        'floor_name': '-',
        'room_full_name': '-',
        'billing_category': '-',
        'description': '-',
        'login_id': Active.userId,
        'Logic': 'UpdateFloorStatus',
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {

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
function InsertUpdateFloor() {
    if ($('#txtFloorName').val() == '') {
        alert('Please Provide Floor Name');
        return;
    }
    var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
    var objBO = [];
    objBO.push({
        'FloorID': _floorId,
        'room_id': '-',
        'room_name': '-',
        'room_type': '-',
        'floor_name': $('#txtFloorName').val(),
        'room_full_name': '-',
        'billing_category': '-',
        'description': '-',
        'login_id': Active.userId,
        'Logic': (_floorId == '') ? 'InsertFloor' : 'UpdateFloor',
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                GetFloorMaster();
                Clear();
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
//Create Room 
function CreateRoom() {
    if (Validation()) {
        debugger
        var floor = $('#ddlFloor option:selected').text();
        var roomType = $('#ddlRoomType option:selected').text();
        var roomName = $('#txtRoomName').val();
        var roomNo = $('#txtRoomNo').val();
        var TotalBed = parseInt($('#txtTotalBed').val());
        var tbody = "";
        $('#tblRoom tbody').empty();
        for (var i = 1; i <= TotalBed; i++) {           
            tbody += "<tr>";
            tbody += "<td>" + i + "</td>";
            tbody += "<td>" + floor + "</td>";
            tbody += "<td>" + roomType + "</td>";           
            tbody += "<td><input type='text' style='width:100%;' class='form-control' placeholder='Room Name' value='" + roomName + "'/></td>";
            tbody += "<td><input type='text' style='width:100%;' class='form-control' placeholder='Room No' value='" + i + "'/></td>";
            //tbody += "<td>" + floor + "/" + roomType + "/<span class='roomid'>" + roomId.toUpperCase() + "</span></td>";
            tbody += "</tr>";
        }
        $('#tblRoom tbody').append(tbody);
    }
}
function InsertRoom() {
    if (ValidateRoomInsert()) {
        var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
        var objBO = [];
        $('#tblRoom tbody tr').each(function () {
            objBO.push({
                'hosp_id': Active.unitId,
                'room_id': '-',
                'RoomTypeId': $('#ddlRoomType option:selected').val(),
                'room_name': $(this).find('td:eq(3)').find('input[type=text]').val(),
                'RoomNo': $('#txtRoomNo').val(),              
                'floor_name': $('#ddlFloor option:selected').text(),
                'bedNo': $(this).find('td:eq(4)').find('input[type=text]').val(),
                'room_full_name': '-',                
                'login_id': Active.userId,
                'Logic': 'InsertRoom',
            });
        });
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
}
function Validation() {
    var floor = $('#ddlFloor option:selected').text();
    var roomType = $('#ddlRoomType option:selected').text();
    var roomName = $('#txtRoomName').val();
    var roomNo = $('#txtRoomNo').val();

    if (floor == 'Select Floor') {
        $('span.selection').find('span[aria-labelledby=select2-ddlFloor-container]').css('border-color', 'red').focus();
        alert('Please Select Floor..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlFloor-container]').removeAttr('style');
    }
    if (roomType == 'Select Room Type') {
        $('span.selection').find('span[aria-labelledby=select2-ddlRoomType-container]').css('border-color', 'red').focus();
        alert('Please Select Room Type..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlRoomType-container]').removeAttr('style');
    }  
    if (roomName == '') {       
        alert('Please Provide Room Name..');
        return false;
    }    
    if (roomNo == '') {       
        alert('Please Provide Room No..');
        return false;
    }   
    return true;
}
//Create Bed
function RoomInfoByFloor(floor) {
    var url = config.baseUrl + "/api/RoomAndBed/RoomMasterQueries";
    var objBO = {};
    objBO.floor_name = floor;
    objBO.Logic = 'RoomInfoByFloor';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $("#ddlRoomForBed").empty().append($('<option>Select Room</option>'));
            $.each(data.ResultSet.Table, function (key, val) {
                $("#ddlRoomForBed").append($("<option data-roomid=" + val.RoomId + "></option>").val(val.RoomId).html(val.roomFullName)).select2();
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function CreateBed() {
    if (ValidationBed()) {
        floor = $('#ddlFloorForBed option:selected').text();
        room = $('#ddlRoomForBed option:selected').text();
        bedNo = parseInt($('#txtBedNo').val());
        tbody = "";
        $('#tblBed tbody').empty();
        for (var i = 0; i < bedNo; i++) {
            bedId = 'F-' + i;
            tbody += "<tr>";
            tbody += "<td><button class='btn-danger btn-action'><i class='fa fa-trash'></i></button></td>";
            tbody += "<td>" + floor + "</td>";
            tbody += "<td>" + room + "</td>";
            tbody += "<td><input type='text' style='width:70%;' class='text-uppercase' value='" + bedId.toUpperCase() + "'/></td>";
            tbody += "</tr>";
        }
        $('#tblBed tbody').append(tbody);
    }
}
function InsertBed() {
    if (ValidateBedInsert()) {
        var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
        var objBO = [];
        $('#tblBed tbody tr').each(function () {
            objBO.push({
                'hosp_id': Active.unitId,
                'room_id': $('#ddlRoomForBed option:selected').val(),
                'bedName': $(this).find('td:eq(3)').find('input[type=text]').val(),
                'IPDNo': '-',
                'login_id': Active.userId,
                'Logic': 'InsertBed',
            });
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data == 'Success') {
                    alert(data);
                    $('#tblBed tbody').empty();
                    $('input[id=txtBedNo]').val('');
                    $('#ddlRoomForBed,#ddlFloorForBed').prop('selectedIndex', '0').change();
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
}
function ValidationBed() {
    floor = $('#ddlFloorForBed option:selected').text();
    room = $('#ddlRoomForBed option:selected').text();
    bedNo = $('#txtBedNo').val();

    if (floor == 'Select Floor') {
        $('span.selection').find('span[aria-labelledby=select2-ddlFloorForBed-container]').css('border-color', 'red').focus();
        alert('Please Select Floor..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlFloorForBed-container]').removeAttr('style');
    }
    if (room == 'Select Room') {
        $('span.selection').find('span[aria-labelledby=select2-ddlRoomForBed-container]').css('border-color', 'red').focus();
        alert('Please Select Room..');
        return false;
    }
    else {
        $('span.selection').find('span[aria-labelledby=select2-ddlRoomForBed-container]').removeAttr('style');
    }
    if (bedNo == '') {
        $('#txtBedNo').css('border-color', 'red').focus();
        alert('Please Provide Bed Number..');
        return false;
    }
    else {
        $('#txtBedNo').removeAttr('style');
    }
    return true;
}
function ValidateBedInsert() {
    var isBed = $('#tblBed tbody tr').length;
    if (isBed <= 0) {
        alert('Oops! You have not Created Beds. First Create Beds..')
        return false;
    }
    return true;
}
function ValidateRoomInsert() {
    var isBed = $('#tblRoom tbody tr').length;
    if (isBed <= 0) {
        alert('Oops! You have not Created Rooms. First Create Rooms..')
        return false;
    }
    return true;
}

