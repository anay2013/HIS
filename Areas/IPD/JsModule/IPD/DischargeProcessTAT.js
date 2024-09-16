var patientname = "";
var sectionName = "";
$(document).ready(function () {
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    $('#txtcountRow').val('0');
    CloseSidebar();
    $("#tblReport tbody").on('click', '#rowdata', function () {
        sectionName = $(this).closest('tr').find('td:eq(1)').text();
        GetListDataInfo(sectionName);
    })

    $('#txtSearchtable').on('keyup', function () {
        var val = $(this).val().toLowerCase();

        $('#tblReportInfo tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });

    $('#tblReportInfo tbody').on('click', '#btndelete', function () {
        var ipdno = $(this).closest('tr').find('td:eq(0)').text();
        deleteRow(ipdno);
    });

});
function GetListData() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.EntrySource = $("#ddlpayment option:selected").val();
    objBO.Prm1 = $("#txtSearchFrom").val();
    objBO.Prm2 = $("#txtSearchTo").val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'SectionalTAT';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = '';
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='text-align:center; width:8%'>" + val.seq + "</td>";
                        tbody += "<td>" + val.Section + "</td>";
                        tbody += "<td style='text-align:center; width:15%'>" + val.IPDCount + "</td>";
                        tbody += "<td style='text-align:center; width:22%'>" + val.TotTime + "</td>";
                        tbody += "<td style='text-align:center; width:20%'>" + val.AvgTat + "</td>";
                        tbody += "<td style='text-align:center; width:8%'><button class='btn btn-warning btn-xs' style='font-size:14px;width:20%;' id='rowdata'><i class='fa fa-sign-in'>&nbsp;</i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                }
            }
        },

    });

}
function GetListDataInfo(sectionName) {
    debugger
    $('#tblReportInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.IPDNo = sectionName;
    objBO.EntrySource = $("#ddlpayment option:selected").val();
    objBO.Prm1 = $("#txtSearchFrom").val();
    objBO.Prm2 = $("#txtSearchTo").val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'SectionalDetailTAT';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = ''; var temp = ""; var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count = + data.ResultSet.Table.length;
                        $('#txtcountRow').val(count);
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#e5e5e5;'>";
                            tbody += "<td colspan='10' style='font-size:13px;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        tbody += "<td style='width:10%'>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td style='width:8%'>" + val.gender + "</td>";
                        tbody += "<td style='width:14%'>" + val.AdmitDate + "</td>";
                        tbody += "<td style='width:14%'>" + val.DischargeDate + "</td>";
                        tbody += "<td style='width:14%'>" + val.InTime + "</td>";
                        tbody += "<td style='width:14%'>" + val.OutTime + "</td>";
                        tbody += "<td style='text-align:center; width:5%'>" + val.AvgTat + "</td>";
                        tbody += "<td style='text-align:center; width:6%'><button class='btn btn-success btn-xs' onclick=selectRow(this);PatientDetails(this) style='font-size:15px;width:60%;' id='rowdata'><i class='fa fa-eye'>&nbsp;</i></button></td>";
                        tbody += "<td hidden>" + val.PanelName + "</td>";
                        if (val.IpdStatus == "IN") {
                            tbody += "<td style='text-align: center;'><button class='btn btn-danger btn-xs' id='btndelete'><i class='fa fa-remove'></i></button></td>";
                        }
                        else {
                            tbody += "<td style='text-align: center;'>-</button></td>";
                        }

                        tbody += "</tr>";
                    });
                    $('#tblReportInfo tbody').append(tbody);

                }


            }
        },

    });

}
function PatientDetails(elem) {
    $('#GetPatient').modal('show');
    var _IPDNo = $(elem).closest('tr').find('td:eq(0)').text();
    patientName = $(elem).closest('tr').find('td:eq(1)').text();
    PatientDetailsInfo(_IPDNo)
}
function PatientDetailsInfo(ipdNo) {
    _IPDNo = ipdNo;
    $('#patientName').text('Patient Name : ' + patientName);
    $('#patientInfo').text('IPD No : ' + _IPDNo + '  ,');
    $('#tblRemark tbody').empty();
    $('.statusBar').find('div').attr('class', 'status');
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.IPDNo = _IPDNo;
    objBO.EntrySource = '';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'SectionalDetailTATIpdno';
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
                            tbody += ", <b>TAT : </b>" + val.TAT + ' Min';
                            // tbody += ", <b><tat>TAT : " + val.TAT + ' Min</tat></b>';
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
function ExcelDownloads() {
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.IPDNo = sectionName;
    objBO.EntrySource = $("#ddlpayment option:selected").val();
    objBO.Prm1 = $("#txtSearchFrom").val();
    objBO.Prm2 = $("#txtSearchTo").val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'SectionalDetailTAT';
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "DischargeProcessTATDetails" + ".xlsx");
}
function SearchIPDNO() {
    $('#tblReportInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcessQueries";
    var objBO = {};
    objBO.IPDNo = $("#txtipdno").val();
    objBO.EntrySource = $("#ddlpayment option:selected").val();
    objBO.Prm1 = $("#txtSearchFrom").val();
    objBO.Prm2 = $("#txtSearchTo").val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'SectionalSearchTATIPDNO';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = ''; var temp = ""; var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count = + data.ResultSet.Table.length;
                        $('#txtcountRow').val(count);
                        if (temp != val.PanelName) {
                            tbody += "<tr style='background:#e5e5e5;'>";
                            tbody += "<td colspan='10' style='font-size:13px;'><b>" + val.PanelName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.PanelName
                        }
                        else {

                        }
                        tbody += "<td style='width:10%'>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td style='width:8%'>" + val.gender + "</td>";
                        tbody += "<td style='width:14%'>" + val.AdmitDate + "</td>";
                        tbody += "<td style='width:14%'>" + val.DischargeDate + "</td>";
                        tbody += "<td style='width:14%'>" + val.InTime + "</td>";
                        tbody += "<td style='width:14%'>" + val.OutTime + "</td>";
                        tbody += "<td style='text-align:center; width:5%'>" + val.AvgTat + "</td>";
                        tbody += "<td style='text-align:center; width:6%'><button class='btn btn-success btn-xs' onclick=selectRow(this);PatientDetails(this) style='font-size:15px;width:60%;' id='rowdata'><i class='fa fa-eye'>&nbsp;</i></button></td>";
                        tbody += "<td hidden>" + val.PanelName + "</td>";

                        tbody += "</tr>";
                    });
                    $('#tblReportInfo tbody').append(tbody);

                }


            }
        },

    });

}

function deleteRow(ipdno) {
    if (confirm('Are you sure to Delete?')) {
        var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcess";
        var objBO = {};
        objBO.IPDNo = ipdno;
        objBO.EntrySource = '-';
        objBO.Remark = '-';
        objBO.Prm1 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'DeleteDischargeProcess';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
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
        alert("Cancel");
    }
}
