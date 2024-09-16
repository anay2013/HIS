
var logicExecls = "";
var refcode = "";
$(document).ready(function () {
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txttodate');
    GetAllProList();
    $('#ddlReferral').append($('<option></option>').val('ALL').html('ALL')).select2();
    $('#txtSearchReferral').on('keyup', function (e) {
        var referral = $(this).val();
        ReferralSearchname(referral);
        var tbody = $('#tblReferral tbody');
        var tr = $(tbody).find('tr.select-row');
        if (e.keyCode == 40) {
            if (tr.length == 0) {
                $(tbody).removeClass('select-row');
                $(tbody).find('tr:first').addClass('select-row');
            }
            $(tbody).removeClass('select-row');
            $(tr).next().find('tr:eq(' + index + ')').addClass('select-row');
        }
        else if (e.keyCode == 38) {
            index--;
            $(tbody).removeClass('select-row');
            $(tbody).find('tr:eq(' + index + ')').addClass('select-row');
        }
    });
    $('#tblReferral tbody').on('click', 'button', function () {
        var referral = $(this).closest('tr').find('td:eq(1)').text();
        var referralName = $(this).closest('tr').find('td:eq(2)').text();
        $('#ddlReferral').append($('<option></option>').val(referral).html(referralName));
        $('#ddlReferral').val(referral).change();
        $('#modalReferral').modal('hide');
        $('#modalReferral input[type=text]').val('');
        $('#tblReferral tbody').empty();
    });
    $('#tblDoctorReport tbody').on('click', '.Viewrow', function () {
        refcode = $(this).closest('tr').find('td:eq(3)').text();
        GetDataPatientWise(refcode);
    });

});
function ReferralOpen() {
    $('#modalReferral').modal('show');
    ReferralSearchs();
}
function ReferralSearchs() {
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = $("#ddlIPOPtype option:selected").val();
    objBO.RefCode = '-';
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = $("#ddlProList option:selected").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetReferralByProCode';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            $('#tblReferral tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td><button class='btn btn-success bn' style='height:25px'>Select</button></td>";
                        tbody += "<td>" + val.ref_code + "</td>";
                        tbody += "<td>" + val.ref_name + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.degree + "</td>";
                        tbody += "<td>" + val.address + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReferral tbody').append(tbody);
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ReferralSearchname(referral) {
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = $("#ddlIPOPtype option:selected").val();
    objBO.RefCode = '-';
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = referral;
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'SearchByReferralName';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            $('#tblReferral tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td><button class='btn btn-success bn' style='height:25px'>Select</button></td>";
                        tbody += "<td>" + val.ref_code + "</td>";
                        tbody += "<td>" + val.ref_name + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.degree + "</td>";
                        tbody += "<td>" + val.address + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblReferral tbody').append(tbody);
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetAllProList() {
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = '-';
    objBO.RefCode = '-';
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetProList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlProList').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlProList').append($('<option></option>').val(val.ProCode).html(val.ProName));

                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataProWise(logic) {
    logicExecls = logic;
    $("#txttotalPro").text('0');
    $('#DoctorWiseReport').removeClass('active in');
    $('a[href="#DoctorWiseReport"]').parent().removeClass('active');
    $('#PatientWiseReport').removeClass('active in');
    $('a[href="#PatientWiseReport"]').parent().removeClass('active');
    $('#ProWiseReport').addClass('active in');
    $('a[href="#ProWiseReport"]').parent().addClass('active');
    $('#tblProReport tbody').empty();
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = $("#ddlIPOPtype option:selected").val();
    objBO.RefCode = $("#ddlReferral option:selected").val();
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = $("#ddlProList option:selected").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var totalproamount = 0;
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        totalproamount += parseInt(val.Amount);
                        tbody += "<tr>";
                        tbody += "<td style='width:20%;'>" + val.ProCode + "</td>";
                        tbody += "<td style='width:60%;'>" + val.ProName + "</td>";
                        tbody += "<td style='width:20%;text-align:center'>" + val.Amount + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblProReport tbody').append(tbody);
                    $("#txttotalPro").text(totalproamount);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataDoctorWise(logic) {
    logicExecls = logic;
    $("#txttotalDoctor").text('0');
    $('#ProWiseReport').removeClass('active in');
    $('a[href="#ProWiseReport"]').parent().removeClass('active');
    $('#PatientWiseReport').removeClass('active in');
    $('a[href="#PatientWiseReport"]').parent().removeClass('active');
    $('#DoctorWiseReport').addClass('active in');
    $('a[href="#DoctorWiseReport"]').parent().addClass('active');
    $('#tblDoctorReport tbody').empty();
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = $("#ddlIPOPtype option:selected").val();
    objBO.RefCode = $("#ddlReferral option:selected").val();
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = $("#ddlProList option:selected").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var totalAmount1 = 0; var dtemp = "";
                var tcount = 0; var count = 0; var amount = 0;
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        totalAmount1 += parseInt(val.Amount);
                        if (dtemp != val.ProName) {
                            tbody += "<tr class='Dp' style='background:#b1d3e185;'>";
                            tbody += "<td colspan='3' style='font-size:13px;'><b>" + val.ProName + "</b></td>";
                            tbody += "<td style='font-size:13px;float:right'><b>Total:</b></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'><label>0</label></td>";
                            tbody += "</tr>";
                            dtemp = val.ProName
                        }
                        tbody += "<tr class='ptdoctor'>";
                        tbody += "<td style='width:10%'><button class='btn-warning Viewrow' style='border:none;height:20px;margin-bottom: 3px;'>View</button></td>";
                        tbody += "<td hidden>" + val.ProCode + "</td>";
                        tbody += "<td hidden>" + val.ProName + "</td>";
                        tbody += "<td style='width:10%'>" + val.ref_code + "</td>";
                        tbody += "<td style='width:35%;'>" + val.ref_name + "</td>";
                        tbody += "<td style='width:35%;'>" + val.Address + "</td>";
                        tbody += "<td style='width:10%;text-align:center;'>" + val.Amount + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDoctorReport tbody').append(tbody);
                    $('#tblDoctorReport tbody tr').each(function () {
                        if ($(this).attr('class') == 'ptdoctor') {
                            tcount++;
                            amount += parseInt($(this).find('td:eq(6)').text());
                            if (count == $('#tblDoctorReport tbody tr.Dp').length)
                                $('#tblDoctorReport tbody tr.Dp:last').find('td:eq(2)').find('label').text(amount);
                        }
                        if ($(this).attr('class') == 'Dp') {
                            count++;
                            if (count > 1 && count <= $('#tblDoctorReport tbody tr.Dp').length) {
                                $('#tblDoctorReport tbody tr.Dp').eq((count == 2) ? 0 : count - 2).find('td:eq(2)').find('label').text(amount);
                                amount = 0;
                            }
                            else {
                                $('#tblDoctorReport tbody tr.Dp:last').find('td:eq(2)').find('label').text(amount);
                            }
                        }
                    });
                    $("#txttotalDoctor").text(totalAmount1);
                }
            }
        },

        complete: function (response) {

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataPatientWise(refcode) {
    logicExecls = 'GetBusiness:ByPatient';
    $("#txttotalPatient").text('0');
    $('#DoctorWiseReport').removeClass('active in');
    $('a[href="#DoctorWiseReport"]').parent().removeClass('active');
    $('#ProWiseReport').removeClass('active in');
    $('a[href="#ProWiseReport"]').parent().removeClass('active');
    $('#PatientWiseReport').addClass('active in');
    $('a[href="#PatientWiseReport"]').parent().addClass('active');
    $('#tblPatientReport tbody').empty();
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = $("#ddlIPOPtype option:selected").val();
    objBO.RefCode = '-';
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = refcode;
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetBusiness:ByPatient';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var count = 0;
            var totalAmount = 0;
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var Totalpatient = 0; var temp = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        totalAmount += parseInt(val.Amount);
                        if (temp != val.ipop_no) {
                            count++;
                            tbody += "<tr class='pr' style='background:#ffc0cba8;'>";
                            tbody += "<td colspan='7' style='font-size:13px;'><b>IPOPNo : " + val.ipop_no + "</b></td>";
                            tbody += "<td style='font-size:13px;text-align:right;'><b>Total :</b></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'><label>0</label></td>";

                            tbody += "</tr>";
                            temp = val.ipop_no
                        }

                        tbody += "<tr class='pt'>";
                        tbody += "<td>" + val.IPOPType + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td>" + val.ProName + "</td>";
                        tbody += "<td>" + val.ipop_no + "</td>";
                        tbody += "<td>" + val.tnxDate + "</td>";
                        tbody += "<td>" + val.CatName + "</td>";
                        tbody += "<td>" + val.ItemName + "</td>";
                        tbody += "<td style='width:9%;text-align:center;height:25px'>" + val.CatTotal + "</td>";
                        tbody += "<td style='width:9%;text-align:center;height:25px'>" + val.Amount.toFixed(0) + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientReport tbody').append(tbody);
                    $("#txttotalPatient").text(totalAmount);
                }
            }
        },
        complete: function (response) {
            TotalCal()
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function TotalCal() {
    var tcount = 0;
    var count = 0;
    var count1 = 0;
    var amount = 0;
    var amount1 = 0;
    $('#tblPatientReport tbody tr').each(function () {
        if ($(this).attr('class') == 'pt') {
            tcount++;
            amount += parseInt($(this).find('td:eq(8)').text());
            if (count == $('#tblPatientReport tbody tr.pr').length)
                $('#tblPatientReport tbody tr.pr:last').find('td:eq(2)').find('label').text(amount);
        }

        if ($(this).attr('class') == 'pr') {
            count++;
            if (count > 1 && count <= $('#tblPatientReport tbody tr.pr').length) {
                $('#tblPatientReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(2)').find('label').text(amount);
                amount = 0;
            }
            else {
                $('#tblPatientReport tbody tr.pr:last').find('td:eq(2)').find('label').text(amount);
            }
        }
    });


}
function ExcelReferral() {
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = $("#ddlIPOPtype option:selected").val();
    objBO.RefCode = $("#ddlReferral option:selected").val();
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = $("#ddlProList option:selected").val();
    objBO.Prm2 = refcode;
    objBO.login_id = Active.userId;
    objBO.ReportType = 'Excel';
    objBO.Logic = logicExecls;
    Global_DownloadExcel(url, objBO, "ReferralReport" + ".xlsx");
}