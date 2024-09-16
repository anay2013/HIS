var temp = "";
$(document).ready(function () {
    $('#dash-dynamic-section').find('label.title').text('Refer Patient').show();
    GetDoctor()
    ReferralInfoByIPDNo()
});
function GetDoctor() {
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'Service:CategoryList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#ddlDoctor').empty().append($("<option data-id='Select'></option>").val('Select').html('Select')).select2();
            $.each(data.ResultSet.Table1, function (key, val) {
                $("#ddlDoctor").append($("<option></option>").val(val.DoctorId).html(val.DoctorName));
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReferPatient() {
    var url = config.baseUrl + "/api/DoctorApp/IPD_ReferenceInsertUpdate";
    var objBO = {};
    objBO.HospId = Active.HospId;
    objBO.RefId = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.RefBy = _doctorId;
    objBO.RefTo = $('#ddlDoctor option:selected').val();
    objBO.RefType = $('#ddlReferType option:selected').val();
    objBO.RefRemark = $('#txtRemark').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'Insert';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
                ReferralInfoByIPDNo();
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReferralInfoByIPDNo() {
    $('#tblReferInfo tbody').empty();
    var url = config.baseUrl + "/api/DoctorApp/IPD_ReferenceAndDischargeQueries";
    var objBO = {};
    objBO.DoctorId = '';
    objBO.IPDNo = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.Logic = 'ReferralInfoByIPDNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = "";
            $.each(data.ResultSet.Table, function (key, val) {
                tbody += "<tr>";
                tbody += "<td style='width:15%;'>" + val.RefId + "</td>";
                tbody += "<td style='width:15%;'>" + val.PatientName + "</td>";
                tbody += "<td style='width:15%;'>" + val.RoomName + "</td>";
                tbody += "<td style='width:15%;'>" + val.RefType + "</td>";
                tbody += "<td style='width:15%;'>" + val.RefTo + "</td>";
                tbody += "<td style='width:15%;'>" + val.RefDate + "</td>";
                tbody += "<td style='width:15%;'>" + val.RefRemark + "</td>";
                tbody += "</tr>";
            });
            $('#tblReferInfo tbody').append(tbody);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}