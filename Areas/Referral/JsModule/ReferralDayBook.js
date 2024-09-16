
var logicExecls = "";
var refcode = "";
$(document).ready(function () {
    CloseSidebar();
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

    $('#tblReport tbody').on('click', '#btndetails', function () {
        debugger
        var TnxId = $(this).closest('tr').find('td:eq(0)').text();
        GetDataDayDetails(TnxId);
        $("#txttotal1").text('0');
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
function GetDataDayBook() {
    logicExecls = 'GetBusiness:DayBook';
    $('#tblReport tbody').empty();
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
    objBO.Logic = 'GetBusiness:DayBook';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var totalamount = 0; var temp = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        totalamount += parseInt(val.Amount);
                        if (temp != val.ref_code) {
                            tbody += "<tr class='pt' style='background:#b1d3e185;'>";
                            tbody += "<td colspan='5' style='font-size:13px;'><b>Ref Code: &nbsp;" + val.ref_code + " &nbsp; Ref Name: &nbsp; " + val.ref_name + "</b></td>";
                            tbody += "<td style='font-size:13px;float:right'><b>Total:</b></td>";
                            tbody += "<td style='font-size:13px;text-align:center;'><label>0</label></td>";
                            tbody += "<td></td>";
                            tbody += "</tr>"
                            temp = val.ref_code
                        }
                        else {

                        }
                        tbody += "<tr class='ptPatient'>";
                        tbody += "<td style='' hidden>" + val.TnxId + "</td>";
                        tbody += "<td style='width:8%'>" + val.IPOPType + "</td>";
                        tbody += "<td style='width:15%'>" + val.ProName + "</td>";
                        tbody += "<td style='width:10%'>" + val.UHID + "</td>";
                        tbody += "<td style='width:30%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:10%'>" + val.ipop_no + "</td>";
                        tbody += "<td style='width:10%'>" + val.tnxDate + "</td>";
                        tbody += "<td style='text-align:center;width:8%'>" + val.Amount.toFixed(0) + "</td>";
                        tbody += "<td style='width:8%;text-align:center'><button class='btn btn-warning btn-xs' id='btndetails'>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblReport tbody').append(tbody);
                    TotalCal();
                    $("#txttotal").text(totalamount);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataDayDetails(TnxId) {
    logicExecls = "DayBookIdWiseDetails";
    $('#modalDayBook').modal('show');
    $('#tblDayBookInfo tbody').empty();
    var url = config.baseUrl + "/api/master/Referral_BusinessQueries";
    var objBO = {};
    objBO.HospId = '-';
    objBO.IPOPType = '-';
    objBO.RefCode = '-';
    objBO.EmpName = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = TnxId;
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'DayBookIdWiseDetails';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = ""; var totalamount1 = 0; var temp = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        totalamount1 += parseInt(val.NetAmount);
                        if (temp != val.CatName) {
                            tbody += "<tr style='background:#c0def3;;'>";
                            tbody += "<td colspan='5' style='font-size:13px;'><b>" + val.CatName + "</b></td>";
                            tbody += "</tr>";
                            temp = val.CatName
                        }
                        else {
                            tbody += "<tr>";
                        }

                        tbody += "<td style='width:55%'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.Qty + "</td>";
                        tbody += "<td style='width:15%;text-align:center'>" + val.Rate + "</td>";
                        tbody += "<td style='width:15%;text-align:center'>" + val.discount + "</td>";
                        tbody += "<td style='width:15%;text-align:center'>" + val.NetAmount + "</td>";
                        tbody += "<td hidden>" + val.TnxId + "</td>";

                        tbody += "</tr>";
                    });
                    $('#tblDayBookInfo tbody').append(tbody);
                    $("#txttotal1").text(totalamount1);
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}
function ExcelDayBook() {
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
    objBO.ReportType = 'Excel';
    objBO.Logic = logicExecls;
    Global_DownloadExcel(url, objBO, "ReferralDayBookReport" + ".xlsx");
}
function TotalCal() {
    var tcount = 0;
    var count = 0;
    var amount = 0;
    $('#tblReport tbody tr').each(function () {
        if ($(this).attr('class') == 'ptPatient') {
            tcount++;
            amount += parseInt($(this).find('td:eq(7)').text());
            if (count == $('#tblReport tbody tr.pt').length)
                $('#tblReport tbody tr.pt:last').find('td:eq(2)').find('label').text(amount);
        }
        if ($(this).attr('class') == 'pt') {
            count++;
            if (count > 1 && count <= $('#tblReport tbody tr.pt').length) {
                $('#tblReport tbody tr.pt').eq((count == 2) ? 0 : count - 2).find('td:eq(2)').find('label').text(amount);
                amount = 0;
            }
            else {
                $('#tblReport tbody tr.pt:last').find('td:eq(2)').find('label').text(amount);
            }
        }
    });


}