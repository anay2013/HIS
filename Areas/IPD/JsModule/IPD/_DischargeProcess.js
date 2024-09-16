var _IPDNo = '';
var _panelType = '';
$(document).ready(function () {

    $('.statusBar').find('div').attr('class', 'status');
    BlockUI();
    $('.Partial').html($('.common').html());
    $('.common').closest('.row').remove();
    $('.Partial').addClass('common');
    DischargeInfo();

})

function PatientDischarge(elem) {
    _panelType = $(elem).closest('tr').find('td:eq(1)').text();
    _IPDNo = $(elem).closest('tr').find('td:eq(0)').text();
    PatientDischargeInfo(_IPDNo)
}
function PatientDischargeInfo(ipdNo) {
    _IPDNo = ipdNo;
    $('#patientInfo').text('IPD No : ' + _IPDNo);
    $('#tblRemark tbody').empty();
    $('.statusBar').find('div').attr('class', 'status');
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};

    objBO.IPDNo = _IPDNo;
    objBO.EntrySource = '';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'DischargeInfoByIPD';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = '';
            var temp = '';
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var Dept = Object.keys(data.ResultSet.Table[0]);
                    $.each(data.ResultSet.Table, function (key, val) {
                        for (var i = 0; i < Dept.length; i++) {
                            $('.statusBar').find('div.status').eq($.inArray(Dept[i], Object.keys(data.ResultSet.Table[0]))).addClass(val[Dept[i]]);
                        }
                    });
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (temp != val.EntrySource) {
                            tbody += "<tr class='group' style='background:#ddd'>";
                            tbody += "<td colspan='3'>" + val.EntrySource;
                            tbody += "<span class='pull-right'><b>In Time : </b>" + val.InTime;
                            tbody += ", <b>Out Time : </b>" + val.OutTime;
                            tbody += ", <b><tat>TAT : " + val.TAT + ' Min</tat></b>';
                            tbody += "</span></td>";
                            tbody += "</tr>";
                            temp = val.EntrySource;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "<td>" + val.RemarkDate + "</td>";
                        tbody += "<td>" + val.remarkBy + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblRemark tbody').append(tbody);
                }
            }
        },
        complete: function (response) {
            BlockUI()
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BlockUI() {
    var dept = $('.common').find('label.catName').text();
    var parentStatus = $('.statusBar div.status').eq($.inArray(dept, ['RMO', 'Consultant', 'Medicine', 'Nursing', 'IpdManager', 'Pharmacy', 'Billing', 'TPA']) - 1).attr('class').split(' ');
    $('.statusBar div.status').each(function () {
        var tag = $(this).data('val');
        if (tag == dept) {
            var status = $(this).attr('class').split(' ');
            if ($.inArray('Pending', status) > -1 && $.inArray('Out', parentStatus) > -1 && dept != 'RMO') {
                $('.common').find('button').eq(0).prop('disabled', false);
                $('.common').find('button').eq(2).prop('disabled', false);
            } else if ($.inArray('Pending', status) > -1 && dept == 'RMO') {
                $('.common').find('button').eq(0).prop('disabled', false);
                $('.common').find('button').eq(2).prop('disabled', true);
            }
            //else {
            //    $('.common').find('button').eq(0).prop('disabled', false);
            //    $('.common').find('button').eq(2).prop('disabled', true);
            //}

            if ($.inArray('In', status) > -1) {
                $('.common').find('button').eq(0).prop('disabled', true);
                $('.common').find('button').eq(2).prop('disabled', false);
            }
            if ($.inArray('Out', status) > -1) {
                $('.common').find('button').eq(0).prop('disabled', true);
                $('.common').find('button').eq(2).prop('disabled', true);
            }

        }
    });
}

function DischargeInfo() {
    $('#tblPatientInfo tbody').empty();
    $('#ddlFilter').empty().append($('<option></option>').val('ALL').html('ALL'));
    $('#ddlPanel').empty().append($('<option></option>').val('ALL').html('ALL'));
    $('#ddlFloor').empty().append($('<option></option>').val('ALL').html('ALL'));
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '-';
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = $('#ddlFloor option:selected').val();
    objBO.Prm2 = $('#ddlPanel option:selected').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'DischargeInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            var tbody = '';
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('.total').eq(0).find('span').text((parseInt(val.totalDischage) < 10) ? '0' + val.totalDischage : val.totalDischage);
                        $('.total').eq(1).addClass((parseInt(val.totalDischage) == parseInt(val.RMO)) ? 'totalDone' : '-').find('span').text((parseInt(val.RMO) < 10) ? '0' + val.RMO : val.RMO);
                        $('.total').eq(2).addClass((parseInt(val.totalDischage) == parseInt(val.Consultant)) ? 'totalDone' : '-').find('span').text((parseInt(val.Consultant) < 10) ? '0' + val.Consultant : val.Consultant);
                        $('.total').eq(3).addClass((parseInt(val.totalDischage) == parseInt(val.Medicine)) ? 'totalDone' : '-').find('span').text((parseInt(val.Medicine) < 10) ? '0' + val.Medicine : val.Medicine);
                        $('.total').eq(4).addClass((parseInt(val.totalDischage) == parseInt(val.Nursing)) ? 'totalDone' : '-').find('span').text((parseInt(val.Nursing) < 10) ? '0' + val.Nursing : val.Nursing);
                        $('.total').eq(5).addClass((parseInt(val.totalDischage) == parseInt(val.IpdManager)) ? 'totalDone' : '-').find('span').text((parseInt(val.IpdManager) < 10) ? '0' + val.IpdManager : val.IpdManager);
                        $('.total').eq(6).addClass((parseInt(val.totalDischage) == parseInt(val.Pharmacy)) ? 'totalDone' : '-').find('span').text((parseInt(val.Pharmacy) < 10) ? '0' + val.Pharmacy : val.Pharmacy);
                        $('.total').eq(7).addClass((parseInt(val.totalDischage) == parseInt(val.Billing)) ? 'totalDone' : '-').find('span').text((parseInt(val.Billing) < 10) ? '0' + val.Billing : val.Billing);
                        $('.total').eq(8).addClass((parseInt(val.totalDischage) == parseInt(val.TPA)) ? 'totalDone' : '-').find('span').text((parseInt(val.TPA) < 10) ? '0' + val.TPA : val.TPA);
                    });
                }
            }
            var temp = "";
            var filter = []; var filterpanel = []; var filterFloor = [];
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        if (temp != val.dischargeStaus) {
                            filter.push(val.dischargeStaus)
                            temp = val.dischargeStaus;
                        }
                        filterpanel.push(val.PanelType)
                        filterFloor.push(val.FloorName)
                        tbody += "<tr>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td class='hide'>" + val.PanelType + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.dischargeInit + "</td>";
                        tbody += "<td><span>" + val.dischargeStaus + "</span></td>";
                        tbody += "<td><button onclick=selectRow(this);PatientDischarge(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'>&nbsp;</i></button></td>";
                        tbody += "<td class='hide'>" + val.FloorName + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientInfo tbody').append(tbody);
                    const set = new Set(filter);
                    const uniqueArr = Array.from(set);
                    for (var i = 0; i < uniqueArr.length; i++) {
                        $('#ddlFilter').append($('<option></option>').val(uniqueArr[i]).html(uniqueArr[i]));
                    }

                    //panel
                    const set1 = new Set(filterpanel);
                    const uniqueArr1 = Array.from(set1);
                    for (var i = 0; i < uniqueArr1.length; i++) {
                        $('#ddlPanel').append($('<option></option>').val(uniqueArr1[i]).html(uniqueArr1[i]));
                    }

                    //floor
                    const set2 = new Set(filterFloor);
                    const uniqueArr2 = Array.from(set2);
                    for (var i = 0; i < uniqueArr2.length; i++) {
                        $('#ddlFloor').append($('<option></option>').val(uniqueArr2[i]).html(uniqueArr2[i]));
                    }
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ItemInsert(logic) {
    if (_IPDNo == '') {
        alert('Please Choose Patient')
        return
    }
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcess";
    var objBO = {};
    objBO.IPDNo = _IPDNo;
    objBO.EntrySource = $('.catName').text();
    objBO.Remark = $('#txtRemark').val();
    objBO.Prm1 = _panelType;
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        traditional: true,
        success: function (data) {
            if (data.includes('Success')) {
                alert('Successfully Submited');
                PatientDischargeInfo(_IPDNo)
                $('#txtRemark').val('');
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

function floorfilter() {
    if ($('#ddlFloor option:selected').text() != 'ALL') {
        $('#tblPatientInfo tbody tr').filter(function () {
            $(this).toggle($(this).text().toLocaleLowerCase().indexOf($('#ddlFloor option:selected').text().toLocaleLowerCase()) > -1)
        })
    }
    else { $('#tblPatientInfo tbody tr').removeAttr('style') };
}
function Filter() {
    if ($('#ddlFilter option:selected').text() != 'ALL') {
        $('#tblPatientInfo tbody tr').filter(function () {
            $(this).toggle($(this).text().toLocaleLowerCase().indexOf($('#ddlFilter option:selected').text().toLocaleLowerCase()) > -1)
        })
    }
    else if ($('#ddlPanel option:selected').text() != 'ALL') {
        $('#tblPatientInfo tbody tr').filter(function () {
            $(this).toggle($(this).text().toLocaleLowerCase().indexOf($('#ddlPanel option:selected').text().toLocaleLowerCase()) > -1)
        })
    }


    else { $('#tblPatientInfo tbody tr').removeAttr('style') };
}