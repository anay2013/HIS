
$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Room Shifted Log').show();
    GetDataRoomShiftLog();
});



function GetDataRoomShiftLog() {
    $('#tblRoomshiftedLog tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = _IPDNo;
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1999/01/01';
    objBO.to = '1999/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Prm3 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'RoomShiftedLog';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.rStatus == "IN") {
                            tbody += "<tr style='background:#bdf3bd'>";
                        }
                        else {
                            tbody += "<tr>";
                        }
                        //tbody += "<tr>";
                        tbody += "<td hidden>" + val.IPDNo + "</td>";
                        tbody += "<td style='width:15%;'>" + val.RoomTypeName + "</td>";
                        tbody += "<td style='width:15%;'>" + val.roomFullName + "</td>";
                        tbody += "<td style='width:15%;'>" + val.StayFrom + "</td>";
                        tbody += "<td style='width:15%;'>" + val.StayTo + "</td>";
                        tbody += "<td style='width:15%;'>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td style='width:15%;'>" + val.emp_name + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblRoomshiftedLog tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}