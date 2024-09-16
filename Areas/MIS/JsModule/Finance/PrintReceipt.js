var logicexecl = ""

$(document).ready(function () {
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    $('#tblReceiptNo tbody').on('click', '#btnprint', function () {
        var receipttype = $(this).closest('tr').find('td:eq(3)').text();
        var receiptNo = $(this).closest('tr').find('td:eq(4)').text();
        var tnxidd = $(this).closest('tr').find('td:eq(5)').text();
        PrintReceipt(receiptNo, receipttype, tnxidd)
    });
});

function GetData(Logicname) {
    logicexecl = Logicname;
    $('#tblReceiptNo tbody').empty();
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = $("#txtByname").val();
    objBO.prm_2 = $("#txtSearchUHID").val();
    objBO.prm_3 = '-';
    objBO.prm_4 = '-';
    objBO.loginId = Active.loginId;
    objBO.Logic = Logicname;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            var tbody = "";
            var Total = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td>' + val.IPOPType + '</td>';
                        tbody += '<td>' + val.ipop_no + '</td>';
                        tbody += '<td>' + val.UHID + '</td>';
                        tbody += '<td>' + val.ReceiptType + '</td>';
                        tbody += '<td>' + val.ReceiptNo + '</td>';
                        tbody += '<td hidden>' + val.TnxId + '</td>';
                        tbody += '<td>' + val.receiptDate + '</td>';
                        tbody += '<td>' + val.patient_name + '</td>';
                        tbody += '<td>' + val.ageInfo + '</td>';
                        tbody += '<td>' + val.PayMode + '</td>';
                        tbody += '<td>' + val.Amount + '</td>';
                        tbody += '<td>' + val.EntryBy + '</td>';
                        if (val.ReceiptType == "Advance") {
                            tbody += '<td><button style="height:25px;" class="btn btn-warning btn-xs" id="btnprint"><i class="fa fa-print">&nbsp;</i>Print</buton></td>';
                        }
                        else if (val.ReceiptType == "Appointment") {
                            tbody += '<td><button style="height:25px;" class="btn btn-success btn-xs" id="btnprint"><i class="fa fa-print">&nbsp;</i>Print</buton></td>';
                        }
                        else if (val.ReceiptType == "Diagnostic-Procedure") {
                            tbody += '<td><button style="height:25px;" class="btn btn-info btn-xs" id="btnprint"><i class="fa fa-print">&nbsp;</i>Print</buton></td>';
                        }

                        tbody += '</tr>';
                        Total = Total + val.Amount;
                    });

                    $('#tblReceiptNo tbody').append(tbody);
                    $("#txttotal").text(Total.toFixed(0));

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DownloadExcelReceipt() {
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = $("#txtByname").val();
    objBO.prm_2 = $("#txtSearchUHID").val();
    objBO.prm_3 = '-';
    objBO.prm_4 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = logicexecl;
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel(url, objBO, "ReceiptInfoReport" + ".xlsx");
}
function PrintReceipt(receiptNo, receipttype, tnxidd) {
    if (receipttype == "Advance") {
        var url = config.documentServerUrl + "/OPD/Print/AdvanceReceipt?ReceiptNo=" + receiptNo + "&loginName=" + Active.userName;
        window.open(url, '_blank');
    }
    else if (receipttype == "Appointment") {
        var url = config.documentServerUrl + "/OPD/Print/AppointmentReceipt?TnxId=" + tnxidd + "&ActiveUser=" + Active.userName;
        window.open(url, '_blank');
    }
    else if (receipttype == "Diagnostic-Procedure") {
        var url = config.documentServerUrl + "/OPD/Print/ServiceReceipt?visitNo=" + tnxidd + "&ActiveUser=" + Active.userName;
        window.open(url, '_blank');
    }
}