
$(document).ready(function () {
    Loaddata();

});

function Loaddata() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1999/01/01';
    objBO.to = '1999/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = "RoomStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                       if (val.IsActive == 'Y') {
                            $('#ddlRoomStatus').val(data.ResultSet.Table[0].roomStatus);
                            $(':button[type="submit"]').prop('disabled', false);

                        }
                        else {
                            $('#ddlRoomStatus').val(data.ResultSet.Table[0].roomStatus);
                            $(':button[type="submit"]').prop('disabled', true);
                        }

                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateRoomStatus() {
    if ($("#ddlRoomStatus").val() == "0") {
        alert("please Select Room Status");
        $("#ddlRoomStatus").focus();
        return
    }
    var isConfirmed = confirm('Are you sure you want to Update the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/IPDNursingService/IPD_RoomAndDoctorShift";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.DoctorId = '-';
        objBO.RoomBedId = '-';
        objBO.RoomBillingCategory = '-';
        objBO.Prm1 = $('#ddlRoomStatus option:selected').val();
        objBO.Prm2 = '-';
        objBO.RoomChangeDateTime = '1999/01/01';
        objBO.login_id = Active.userId;
        objBO.Logic = 'UpdateRoomStatus';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#ddlRoomStatus").val("0");
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
    else {
        alert('Data Update canceled.');
    }
}

