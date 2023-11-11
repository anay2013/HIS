$(document).ready(function () {
	CloseSidebar();
	$('select').select2();
	$('#ddlRoomForBed').append($('<option>Select Room</option>')).select2();
	searchTable('txtSearch', 'tblRoom');
	searchTable('txtSearchBed', 'tblBeds');
	$('#ddlFloor').on('change', function () {
		floor = $(this).find('option:selected').text();
		RoomInfoByFloor(floor);
	});
	$('#ddlFloorForBed').on('change', function () {
		floor = $(this).find('option:selected').text();
		BedInfoByFloor(floor);
	});
	$('#ddlUpdateFloorForBed').on('change', function () {  
		floor = $(this).find('option:selected').text();
		RoomInfoForUpdate(floor, '');
	});

	$('#btnNew').on('click', function () {
		$('#update').slideUp();
		$('.basic').slideDown();
	});
	$('#btnNewBed').on('click', function () {
		$('#updateBed').slideUp();
		$('#BasicBed').slideDown();
	});
	$('#tblRoom tbody').on('click', 'button', function () {
		$('#update').slideDown();
		$('.basic').slideUp();
		floorName = $(this).closest('tr').find('td:eq(1)').text();
		roomId = $(this).closest('tr').find('td:eq(2)').text();
		roomName = $(this).closest('tr').find('td:eq(3)').text();
		roomType = $(this).closest('tr').find('td:eq(4)').text();
		billingCategory = $(this).closest('tr').find('td:eq(5)').text();
		selectRow($(this));
		//Floor Fill
		$('#ddlUpdateFloor option').map(function () {
			if ($(this).text().toLowerCase() == floorName.toLowerCase()) {
				$('#ddlUpdateFloor').prop('selectedIndex', '' + $(this).index() + '').change();
			}
		});
		//Room Type
		$('#ddlUpdateRoomType option').map(function () {
			if ($(this).text().toLowerCase() == roomType.toLowerCase()) {
				$('#ddlUpdateRoomType').prop('selectedIndex', '' + $(this).index() + '').change();
			}
		});
		//Room Type
		$('#ddlUpdateBillingCategory option').map(function () {
			if ($(this).text().toLowerCase() == billingCategory.toLowerCase()) {
				$('#ddlUpdateBillingCategory').prop('selectedIndex', '' + $(this).index() + '').change();
			}
		});
		$('#txtUpdateRoomName').val(roomName);
		$('#txtRoomId').val(roomId);
	});
	$('#btnUpdateRoom').on('click', function () {
		roomid = $('#txtRoomId').val();
		if (Validation()) {
			UpdateRoomDetails(roomid);
		}
	});

	$('#tblRoom tbody').on('click', '.switch', function () {
		isCheck = $(this).find('input[type=checkbox]').is(':checked');
		var roomid = $(this).closest('tr').find('td:eq(2)').text();
		var statusflag = $(this).find('input[type=checkbox]').data("status");
		if (isCheck) {
			if (statusflag == 'checked') {
				$(this).find('input[type=checkbox]').data('status', 'unchecked');
				DeleteRoom(roomid, false);
			}
			else if (statusflag == 'unchecked') {
				$(this).find('input[type=checkbox]').data('status', 'checked');
				DeleteRoom(roomid, true);
			}
		}
	});
	$('#tblBeds tbody').on('click', '.switch', function () {
		isCheck = $(this).find('input[type=checkbox]').is(':checked');
		var bedid = $(this).closest('tr').find('td:eq(1)').text();
		var statusflag = $(this).find('input[type=checkbox]').data("status");
		if (isCheck) {
			if (statusflag == 'checked') {
				$(this).find('input[type=checkbox]').data('status', 'unchecked');
				DeleteBed(bedid, false);
			}
			else if (statusflag == 'unchecked') {
				$(this).find('input[type=checkbox]').data('status', 'checked');
				DeleteBed(bedid, true);
			}
		}
	});
	$('#tblBeds tbody').on('click', 'button', function () {
		$('#updateBed').slideDown();
		$('#BasicBed').slideUp();
		floorName = $('#ddlFloorForBed option:selected').text();
		bedNo = $(this).closest('tr').find('td:eq(1)').text();
		bedName = $(this).closest('tr').find('td:eq(2)').text();
		roomid = $(this).data('roomid');
		selectRow($(this));
		//Floor Fill
		$('#ddlUpdateFloorForBed option').map(function () {
			if ($(this).text().toLowerCase() == floorName.toLowerCase()) {
				$('#ddlUpdateFloorForBed').prop('selectedIndex', '' + $(this).index() + '').change();
			}
		});
		debugger
		RoomInfoForUpdate(floorName, roomid);
		$('#txtBedNo').val(bedNo);
		$('#txtBedName').val(bedName);
	});
	$('#btnUpdateBed').on('click', function () {
		bedNo = $('#txtBedNo').val();
		if (ValidationBed()) {
			UpdateBedDetails(bedNo);
		}
	});
});

//Room
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
			$("#tblRoom tbody").empty();
			var tbody = "";
			var temp = "";
			$.each(data.ResultSet.Table, function (key, val) {
				tbody += "<tr>";
				tbody += "<td><button class='btn-danger btn-action'><i class='fa fa-edit'></i></button></td>";
				tbody += "<td>" + val.FloorName + "</td>";
				tbody += "<td>" + val.RoomId + "</td>";
				tbody += "<td>" + val.RoomName + "</td>";
				tbody += "<td>" + val.RoomType + "</td>";
				tbody += "<td>" + val.BillingCategory + "</td>";
				tbody += "<td>" + val.roomFullName + "</td>";
				tbody += "<td>" +
					'<label class="switch">' +
					'<input type="checkbox" class="IsActive" data-status=' + val.IsActive + ' id="chkActive" ' + val.IsActive + '>' +
					'<span class="slider round"></span>' +
					'</label>'
					+ "</td>";
				tbody += "</tr>";
			});
			$("#tblRoom tbody").append(tbody);
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function UpdateRoomDetails(roomid) {
	var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
	var objBO = [];
	debugger;
	objBO.push({
		'hosp_id': Active.unitId,
		'room_id': roomid,
		'room_name': $('#txtUpdateRoomName').val().toUpperCase(),
		'room_type': $('#ddlUpdateRoomType option:selected').text(),
		'floor_name': $('#ddlUpdateFloor option:selected').text(),
		'room_full_name': $('#tblRoom tbody').find('tr.select-row').find('td:eq(6)').text(),
		'billing_category': $('#ddlUpdateBillingCategory option:selected').text(),
		'description': '-',
		'login_id': Active.userId,
		'Logic': 'UpdateRoom'
	});
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data == 'Success') {
				alert('Record Updated Successfully..');
				$('#update input[type=text]').val('');
				$('#update select').prop('selectedIndex', '0').change();
				$('#update').slideUp();
				$('.basic').slideDown();
				$('#tblRoom tbody').find('tr.select-row').find('td:eq(1)').text(objBO[0].floor_name);
				$('#tblRoom tbody').find('tr.select-row').find('td:eq(2)').text(objBO[0].room_id);
				$('#tblRoom tbody').find('tr.select-row').find('td:eq(3)').text(objBO[0].room_name);
				$('#tblRoom tbody').find('tr.select-row').find('td:eq(4)').text(objBO[0].room_type);
				$('#tblRoom tbody').find('tr.select-row').find('td:eq(5)').text(objBO[0].billing_category);
				fullName = objBO[0].floor_name + "/" + objBO[0].room_type + "/" + objBO[0].room_name;
				$('#tblRoom tbody').find('tr.select-row').find('td:eq(6)').text(fullName);
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
function DeleteRoom(roomid, isActive) {
	var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
	var objBO = [];
	objBO.push({
		'IsActive': isActive,
		'room_id': roomid,
		'Logic': 'DeleteRoom'
	});
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data == 'Success') {
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
function Validation() {
	floor = $('#ddlUpdateFloor option:selected').text();
	roomType = $('#ddlUpdateRoomType option:selected').text();
	billingCategory = $('#ddlUpdateBillingCategory option:selected').text();
	roomName = $('#txtUpdateRoomName').val();

	if (floor == 'Select Floor') {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateFloor-container]').css('border-color', 'red').focus();
		alert('Please Select Floor..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateFloor-container]').removeAttr('style');
	}
	if (roomType == 'Select Room Type') {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateRoomType-container]').css('border-color', 'red').focus();
		alert('Please Select Room Type..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateRoomType-container]').removeAttr('style');
	}
	if (billingCategory == 'Select Billing Category') {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateBillingCategory-container]').css('border-color', 'red').focus();
		alert('Please Select Billing Category..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateBillingCategory-container]').removeAttr('style');
	}
	if (roomName == '') {
		$('#txtUpdateRoomName').css('border-color', 'red').focus();
		alert('Please Provide Room Name..');
		return false;
	}
	else {
		$('#txtUpdateRoomName').removeAttr('style');
	}
	return true;
}

//Bed
function BedInfoByFloor(floor) {
	var url = config.baseUrl + "/api/RoomAndBed/RoomMasterQueries";
	var objBO = {};
	objBO.floor_name = floor;
	objBO.Logic = 'BedInfoByFloor';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			$("#tblBeds tbody").empty();
			var tbody = "";
			var temp = "";
			$.each(data.ResultSet.Table, function (key, val) {
				if (temp != val.roomFullName) {
					tbody += "<tr style='background:#addbff'>";
					tbody += "<td colspan='4'>" + val.roomFullName + "</td>";
					tbody += "</tr>";
					temp = val.roomFullName;
				}
				tbody += "<tr>";
				tbody += "<td><button data-roomid='" + val.RoomId + "' class='btn-danger btn-action'><i class='fa fa-edit'></i></button></td>";
				tbody += "<td>" + val.bedNo + "</td>";
				tbody += "<td>" + val.bedName + "</td>";
				tbody += "<td>" +
					'<label class="switch">' +
					'<input type="checkbox" class="IsActive" data-status=' + val.IsActive + ' id="chkActive" ' + val.IsActive + '>' +
					'<span class="slider round"></span>' +
					'</label>'
					+ "</td>";
				tbody += "</tr>";
			});
			$("#tblBeds tbody").append(tbody);
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function UpdateBedDetails(bedNo) {
	var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
	var objBO = [];
	objBO.push({
		'hosp_id': Active.unitId,
		'room_id': $('#ddlUpdateRoom option:selected').val(),
		'bedNo': bedNo,
		'bedName': $('#txtBedName').val().toUpperCase(),
		'login_id': Active.userId,
		'Logic': 'UpdateBed'
	});
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data == 'Success') {
				alert('Record Updated Successfully..');
				$('#updateBed input[type=text]').val('');
				$('#updateBed select').prop('selectedIndex', '0').change();
				$('#updateBed').slideUp();
				$('#BasicBed').slideDown();
				var floor = $('#ddlFloorForBed option:selected').text();
				BedInfoByFloor(floor);
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
function RoomInfoForUpdate(floor, roomid) {
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
			$("#ddlUpdateRoom").empty().append($('<option>Select Room</option>'));
			$.each(data.ResultSet.Table, function (key, val) {
				$("#ddlUpdateRoom").append($("<option data-roomid=" + val.RoomId + "></option>").val(val.RoomId).html(val.roomFullName)).select2();
			});
		},
		complete: function (response) {
			if (roomid != '') {
				$('#ddlUpdateRoom option').map(function () {
					if ($(this).val() == roomid) {
						debugger;
						$('#ddlUpdateRoom').prop('selectedIndex', '' + $(this).index() + '').change();
					}
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function DeleteBed(bedid, isActive) {
	var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
	var objBO = [];
	objBO.push({
		'IsActive': isActive,
		'bedNo': bedid,
		'Logic': 'DeleteBed'
	});
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data == 'Success') {

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
function ValidationBed() {
	floor = $('#ddlUpdateFloorForBed option:selected').text();
	room = $('#ddlUpdateRoom option:selected').text();
	bedName = $('#txtBedName').val();

	if (floor == 'Select Floor') {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateFloorForBed-container]').css('border-color', 'red').focus();
		alert('Please Select Floor..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateFloorForBed-container]').removeAttr('style');
	}
	if (room == 'Select Room') {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateRoom-container]').css('border-color', 'red').focus();
		alert('Please Select Room..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlUpdateRoom-container]').removeAttr('style');
	}
	if (bedName == '') {
		$('#txtBedName').css('border-color', 'red').focus();
		alert('Please Provide Bed Name..');
		return false;
	}
	else {
		$('#txtBedName').removeAttr('style');
	}
	return true;
}