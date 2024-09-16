$(document).ready(function () {
    FillCurrentDate('txtSearchFrom')
    FillCurrentDate('txtSearchTo')
    $("#txtTaxableValue").text('');
    $("#txtTaxAmount").text('');

});

function GetReports() {
    $("#txtTaxableValue").text('0');
    $("#txtTaxAmount").text('0');
    $('#btnAll').append("<i class='fa fa-spinner fa-spin' style='font-size:24px; margin-left:10px'></i>");
    $("#tblReport tbody").empty();
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'BaseSheet';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = ""; var temp = ""; var taxabletotalAmount = 0; var taxtotalAmount = 0; var totaltwovalue = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        taxabletotalAmount += parseFloat(val.TaxableValue);
                        taxtotalAmount += parseFloat(val.TaxAmount);
                        if (temp != val.UnitName) {
                            tbody += "<tr class='pr' style='background:#d7d7d7;'>";
                            tbody += "<td style='font-size:13px;'><b>" + val.UnitName + "</b></td>";
                            tbody += "<td style='font-size:13px;text-align:right;'><b>Total:</b></td>";
                            tbody += "<td style='font-size:13px;text-align:right;'><label>0</label></td>";
                            tbody += "<td style='font-size:13px;text-align:right;'><label>0</label></td>";
                            tbody += "<td style='font-size:13px;text-align:right;'><label>0</label></td>";
                            tbody += "</tr>";
                            temp = val.UnitName;
                        }
                        else {
                        }
                        tbody += "<tr class='pt'>";
                        tbody += "<td>" + val.PrimaryGroup + "</td>";
                        tbody += "<td>" + val.SecondryGroup + "</td>";
                        tbody += "<td style='text-align:right'>" + val.TaxableValue.toFixed(2) + "</td>";
                        tbody += "<td style='text-align:right'>" + val.TaxAmount.toFixed(2) + "</td>";
                        totaltwovalue = parseFloat(val.TaxableValue) + parseFloat(val.TaxAmount);
                        tbody += "<td style='text-align:right'>" + totaltwovalue.toFixed(2) + "</td>";
                        tbody += "</tr>";
                    });
                    $("#tblReport tbody").append(tbody);
                    $("#txtTaxableValue").text(taxabletotalAmount.toFixed(2));
                    $("#txtTaxAmount").text(taxtotalAmount.toFixed(2));
                    TotalCalamount();
                    $('#btnAll i').remove();

                } else {
                    alert("Data Not Found");
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
            $('#btnAll i').remove();
        }
    });
}

function TotalCalamount() {
    var tcount = 0;
    var count = 0;
    var taxabletotalamount = 0;
    var TaxAmounttotal = 0;
    var totalvalue2 = 0;
    $('#tblReport tbody tr').each(function () {
        if ($(this).attr('class') == 'pt') {
            tcount++;
            taxabletotalamount += parseFloat($(this).find('td:eq(2)').text());
            TaxAmounttotal += parseFloat($(this).find('td:eq(3)').text());
            totalvalue2 = taxabletotalamount + TaxAmounttotal;
            if (count == $('#tblReport tbody tr.pr').length)
                $('#tblReport tbody tr.pr:last').find('td:eq(2)').find('label').text(taxabletotalamount.toFixed(2));
            $('#tblReport tbody tr.pr:last').find('td:eq(3)').find('label').text(TaxAmounttotal.toFixed(2));
            $('#tblReport tbody tr.pr:last').find('td:eq(4)').find('label').text(totalvalue2.toFixed(2));
        }

        if ($(this).attr('class') == 'pr') {
            count++;
            if (count > 1 && count <= $('#tblReport tbody tr.pr').length) {
                $('#tblReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(2)').find('label').text(taxabletotalamount.toFixed(2));
                $('#tblReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(3)').find('label').text(TaxAmounttotal.toFixed(2));
                $('#tblReport tbody tr.pr').eq((count == 2) ? 0 : count - 2).find('td:eq(4)').find('label').text(totalvalue2.toFixed(2));
                taxabletotalamount = 0; TaxAmounttotal = 0; totalvalue2 = 0;
            }
            else {
                $('#tblReport tbody tr.pr:last').find('td:eq(2)').find('label').text(taxabletotalamount.toFixed(2));
                $('#tblReport tbody tr.pr:last').find('td:eq(3)').find('label').text(TaxAmounttotal.toFixed(2));
                $('#tblReport tbody tr.pr:last').find('td:eq(4)').find('label').text(totalvalue2.toFixed(2));
            }
        }
    });


}
function DownloadExcels() {
    $('#btnexcel').append("<i class='fa fa-spinner fa-spin' style='font-size:24px; margin-left:10px'></i>");
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'BaseSheet';
    objBO.ReportType = 'Excel';
    Global_DownloadExcel(url, objBO, "AuditServiceChargeReport.xlsx");
}
function Global_DownloadExcel(Url, objBO, fileName) {
    var ajax = new XMLHttpRequest();
    ajax.open("Post", Url, true);
    ajax.responseType = "blob";
    ajax.setRequestHeader("Content-type", "application/json")
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            console.log(this.response);
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            saveAs(blob, fileName); //refernce by ~/JsModule/FileSaver.min.js
            $('#btnexcel i').remove();
        }
    };
    ajax.send(JSON.stringify(objBO));
}

//party wise excel
function PartyWiseExcel() {
    $('#btnPartyExcel').append("<i class='fa fa-spinner fa-spin' style='font-size:24px; margin-left:10px'></i>");
    var url = config.baseUrl + "/api/Account/AC_CreditControlQueries";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.Logic = 'Over-SingleReceivable-DateWise';
    objBO.ReportType = 'Excel';
    Global_DownloadExcelParty(url, objBO, "PartyWiseReport.xlsx");
}
function Global_DownloadExcelParty(Url, objBO, fileName) {
    var ajax = new XMLHttpRequest();
    ajax.open("Post", Url, true);
    ajax.responseType = "blob";
    ajax.setRequestHeader("Content-type", "application/json")
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            console.log(this.response);
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            saveAs(blob, fileName);
            $('#btnPartyExcel i').remove();
        }
    };
    ajax.send(JSON.stringify(objBO));
}