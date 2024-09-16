var logicexecl = ""
$(document).ready(function () {
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
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
                        tbody += '<td>' + val.receiptDate + '</td>';
                        tbody += '<td>' + val.patient_name + '</td>';
                        tbody += '<td>' + val.ageInfo + '</td>';
                        tbody += '<td>' + val.PayMode + '</td>';
                        tbody += '<td>' + val.Amount + '</td>';
                        tbody += '<td>' + val.EntryBy + '</td>';
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

function PrintData() {
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var Nameby = $("#txtByname").val();
    var unidno = $("#txtSearchUHID").val();

    if (logicexecl == "ReceptInfoByNo") {
        var url = "../Print/ReceiptInfo?fromdate=" + from + "&todate=" + to + "&UHIDNO=" + unidno + "&Username=" + Nameby + "&Logicexcel=" + logicexecl;
        window.open(url, '_blank');
    }
    else if (logicexecl == "ReceptInfoByDate") {
        var url = "../Print/ReceiptInfo?fromdate=" + from + "&todate=" + to + "&UHIDNO=" + unidno + "&Username=" + Nameby + "&Logicexcel= " + logicexecl;
        window.open(url, '_blank');
    }
    else if (logicexecl == "ReceptInfoByName") {
        var url = "../Print/ReceiptInfo?fromdate=" + from + "&todate=" + to + "&UHIDNO=" + unidno + "&Username=" + Nameby + "&Logicexcel= " + logicexecl;
        window.open(url, '_blank');
    }
} 