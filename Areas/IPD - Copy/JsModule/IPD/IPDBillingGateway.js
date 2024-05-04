var _doctorId = '';
var _panelId = '';
var _roomNo = '';
var _IPDNo = '';
var _deptName = '';
var _AdmitDate = '';
var _AdmitDateServer = '';
var _UHID = '';
var _sectionForCommunication = '';
$(document).ready(function () {
    _sectionForCommunication = 'Billing';
    CloseSidebar();
    EmployeeIPDMenuRights();
    PatientInfoByIPDNo();
    $('#menuPanel').on('click', 'button', function () {
        $('#menuPanel button').removeClass('activePage');
        $(this).addClass('activePage');
    });
    $('#tblAdviceHeader').on('mouseover', 'i.fa-info', function () {
        $(this).siblings('span').removeClass('hide');
    }).on('mouseleave', 'i.fa-info', function () {
        $(this).siblings('span').addClass('hide');
    });
});
function GoBack() {
    window.parent.CloseGatewayIframe();
}
function loadBody(page) {
    $.ajax({
        type: 'POST',
        url: 'IPDBody',
        data: { page: '../BillingView/' + page },
        success: function (data) {
            $('#dash-dynamic').empty();
            $('#dash-dynamic').html(data);
        }
    });
}
function EmployeeIPDMenuRights() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'EmployeeIPDMenuRights';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var menus = "";
                    $('#menuPanel').empty();
                    $.each(data.ResultSet.Table, function (key, val) {
                        menus += "<button onclick=loadBody('" + val.sub_menu_link + "')>" + val.sub_menu_name + "<i class='fa fa-angle-double-right'></i></button>";
                    });
                    $('#menuPanel').append(menus);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientInfoByIPDNo() {
    $('#btnRemarkLog .count').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = query()['IPDNo'];
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = _sectionForCommunication;
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PatientInfoByIPDNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        _doctorId = val.DoctorId;
                        _panelId = val.PanelId;
                        _IPDNo = val.IPDNo;
                        _deptName = val.DepartmentName;
                        _roomNo = val.RoomNo;
                        _UHID = val.UHID;
                        _AdmitDate = val.AdmitDate;
                        _AdmitDateServer = val.AdmitDateServer;
                        _RoomBillingCategory = val.RoomBillingCategory;
                        $('#tblAdviceHeader tbody').find('tr:eq(0)').find('td:eq(3)').text(val.patient_name);
                        $('#tblAdviceHeader tbody').find('tr:eq(0)').find('td:eq(6)').text(val.ageInfo);
                        $('#tblAdviceHeader tbody').find('tr:eq(0)').find('td:eq(9)').text(val.IPDNo);
                        $('#tblAdviceHeader tbody').find('tr:eq(0)').find('td:eq(12)').text(val.AdmitDate);
                        $('#tblAdviceHeader tbody').find('tr:eq(0)').find('td:eq(15)').text(val.DischargeDate);

                        $('#tblAdviceHeader tbody').find('tr:eq(1)').find('td:eq(2)').text(val.DoctorName);
                        $('#tblAdviceHeader tbody').find('tr:eq(1)').find('td:eq(5)').text(val.UHID);
                        if (parseInt(val.PanelName.length) > 12)
                            $('#tblAdviceHeader tbody').find('tr:eq(1)').find('td:eq(8)').html(val.PanelName.substring(0,9)+'..<i class="fa fa-info"></i><span class="hide">' + val.PanelName + '</span>');
                        else
                            $('#tblAdviceHeader tbody').find('tr:eq(1)').find('td:eq(8)').text(val.PanelName);
                        $('#tblAdviceHeader tbody').find('tr:eq(1)').find('td:eq(9)').text(val.RoomFullDetail);
                        $('#tblAdviceHeader tbody').find('tr:eq(1)').find('td:eq(10)').text(val.Info);
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (eval(val.pendingSeenCount) == 0) {
                            $('#btnRemarkLog .count').text(val.pendingSeenCount);
                            $('#btnRemarkLog .count').removeClass('hvr-pulse-auto');
                        }
                        else {
                            $('#btnRemarkLog .count').text(val.pendingSeenCount);
                            $('#btnRemarkLog .count').addClass('hvr-pulse-auto');
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