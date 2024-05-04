var ipopno = "";
var itemid = "";
var amountvalue = "";
var qtyValue = "";
var bookedvalue = "";

var IPDNOUsed = "";
var ITEMIDUsed = "";
var AMOUNTUsed = "";
var Qtys = "1";
var BookedUsed = "N";
var BookedQtyUsed = ""; 
var UsedQty = ""; 
$(document).ready(function () { 
    $('#tblServiceBooked tbody').on('click', '.SubmitRow', function () {
        debugger
         ipopno = $(this).closest('tr').find('td:eq(1)').text();
         itemid = $(this).closest('tr').find('td:eq(2)').text();
         amountvalue = $(this).closest('tr').find('td:eq(6)').text();
         qtyValue = $(this).closest('tr').find('td:eq(7) input#txtqty').val();
         bookedvalue = $(this).closest('tr').find('td:eq(8)').text();
         Submitbooked();
    });

    $('#tblServiceUsed tbody').on('click', '#btnUsedQty', function () {
        debugger
        IPDNOUsed = $(this).closest('tr').find('td:eq(1)').text();
        ITEMIDUsed = $(this).closest('tr').find('td:eq(2)').text();
        AMOUNTUsed = $(this).closest('tr').find('td:eq(3)').text();
        BookedQtyUsed = $(this).closest('tr').find('td:eq(6)').text();
        UsedQty = parseFloat($(this).closest('tr').find('td:eq(7)').text());
        Qtys= parseFloat($(this).closest('tr').find('td:eq(8) input#txtqtyt').val());
        SubmitUsedQTY();
    });
    $('#tblServiceUsed tbody').on('click', '.selectrow', function () {
        debugger
      var IPDNO = $(this).closest('tr').find('td:eq(1)').text();
      var ITEMID = $(this).closest('tr').find('td:eq(2)').text();
        GetQtyUsedListBind(IPDNO, ITEMID);
    });
});
function GetSearchData() {
    debugger;
    $('#tblServiceBooked tbody').empty();
    var url = config.baseUrl + "/api/Service/Opd_ServiceConsumption";
    var objBO = {};
    objBO.UHID = '-';
    objBO.IPOPNo = '-';
    objBO.ItemId = '-';
    objBO.Value = '-';
    objBO.from = '1990/12/01';
    objBO.to = '1990/12/01';
    objBO.Prm1 = $("#txtUHIDNo").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "PatientItemInfoBySearch";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                //var tbodyy = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td style='width:10%'>" + val.UHID + "</td>";
                        tbody += "<td style='width:10%'>" + val.ipop_no + "</td>";
                        tbody += "<td hidden>" + val.ItemId + "</td>";
                        tbody += "<td style='width:10%'>" + val.EntryDate + "</td>";
                        tbody += "<td style='width:20%'>" + val.patient_name + "</td>";
                        tbody += "<td style='width:20%'>" + val.ItemName + "</td>";
                        tbody += "<td style='text-align:center;width:10%'>" + val.ItemAmount + "</td>";
                        tbody += "<td style='text-align:center;width:5%'><input type='text' style='height:20px; width:100px; margin-bottom:2px; background:#d9d4d4;text-align:center;' id='txtqty'></td>";

                        tbody += "<td style='width:5%;text-align:center;'>" + val.IsBlocked + "</td>";
                        var isBlocked = val.IsBlocked === "Y";
                        var buttonHtml = "<td style='width:5%'><button class='btn-success SubmitRow' id='btnSubmitBooked' style='border:none;height:20px;margin-bottom: 3px;'";
                        if (isBlocked) {
                            buttonHtml += " disabled";
                        }
                        buttonHtml += ">Submit</button></td>";
                        tbody += buttonHtml;
                        tbody += "</tr>";

                    });
                }
                $('#tblServiceBooked tbody').append(tbody);
                GetSearchDataUsed();
                
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetSearchDataUsed() {
    $('#tblServiceUsed tbody').empty();
    var url = config.baseUrl + "/api/Service/Opd_ServiceConsumption";
    var objBO = {};
    objBO.UHID = '-';
    objBO.IPOPNo = '-';
    objBO.ItemId = '-';
    objBO.Value = '-';
    objBO.from = '1990/12/01';
    objBO.to = '1990/12/01';
    objBO.Prm1 = $("#txtUHIDNo").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "PatientItemInfoBySearch";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbodyy = "";
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        tbodyy += "<tr>";
                        tbodyy += "<td style='width:10%'>" + val.UHID + "</td>";
                        tbodyy += "<td style='width:20%'>" + val.ipop_no + "</td>";
                        tbodyy += "<td hidden>" + val.ItemId + "</td>";
                        tbodyy += "<td hidden>" + val.Amount + "</td>";
                        tbodyy += "<td style='width:20%'>" + val.patient_name + "</td>";
                        tbodyy += "<td style='width:20%'>" + val.ItemName + "</td>";
                        tbodyy += "<td style='width:5%;text-align:center;'>" + val.BookedQty + "</td>";
                        tbodyy += "<td style='width:5%;text-align:center;'>" + val.UsedQty + "</td>";
                        tbodyy += "<td style='text-align:center;width:5%'><input type='text' style='height:20px; width:80px; margin-bottom:2px; background:#d9d4d4;text-align:center;' id='txtqtyt' value='1' disabled></td>";
                        tbodyy += "<td hidden>" + val.IsBlocked + "</td>";
                        var checkvalue = val.BookedQty === val.UsedQty;
                        var buttonUsed = "<td style='width:5%'><button class='btn-success' id='btnUsedQty' style='border:none;height:20px;margin-bottom: 3px;'";
                        if (checkvalue) {
                            buttonUsed += " disabled";
                        }
                        buttonUsed += ">Used</button></td>";
                        tbodyy += buttonUsed;
                        tbodyy += "<td style='width:5%'><button class='btn-warning selectrow' style='border:none;height:20px;margin-bottom: 3px;'>View</button></td>";
                        tbodyy += "</tr>";
                    });
                }
                $('#tblServiceUsed tbody').empty().append(tbodyy);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Submitbooked() {
    var url = config.baseUrl + "/api/Service/InsertUpdateService";
    var objBO = {};
    objBO.EntryType = 'Booked';
    objBO.ipop_no = ipopno;
    objBO.ItemId = itemid;
    objBO.Amount = amountvalue;
    objBO.Qty = qtyValue;
    objBO.IsCancelled = bookedvalue;
    objBO.LoginId = Active.userId;
    objBO.Logic = "InsertServiceConsumption";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetSearchData();
                GetSearchDataUsed();
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
function SubmitUsedQTY() {
    var checkqty = UsedQty + Qtys;
    if (BookedQtyUsed < checkqty) {
        alert("Please Enter a Value less than or Equal to the Booked Quantity.");
    } else {
        var url = config.baseUrl + "/api/Service/InsertUpdateService";
        var objBO = {};
        objBO.EntryType = 'Used';
        objBO.ipop_no = IPDNOUsed;
        objBO.ItemId = ITEMIDUsed;
        objBO.Amount = AMOUNTUsed;
        objBO.Qty = Qtys;
        objBO.IsCancelled = BookedUsed;
        objBO.LoginId = Active.userId;
        objBO.Logic = "UpdateUsedQty";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    debugger
                    alert(data);
                    GetSearchDataUsed();
                    GetQtyUsedListBind(IPDNOUsed, ITEMIDUsed);
                    
                }
                else {
                    alert(data);
                    //GetQtyUsedListBind(IPDNOUsed, ITEMIDUsed);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
 
}
function GetQtyUsedListBind(IPDNO, ITEMID) {
    $('#tblServiceUsedList tbody').empty();
    var url = config.baseUrl + "/api/Service/Opd_ServiceConsumption";
    var objBO = {};
    objBO.UHID = '-';
    objBO.IPOPNo = IPDNO;
    objBO.ItemId = ITEMID;
    objBO.Value = '-';
    objBO.from = '1990/12/01';
    objBO.to = '1990/12/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "GetQTYUsedList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                var tbody = "";
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.EntryDate + "</td>";
                        tbody += "<td style='text-align:center'>" + val.UsedQty + "</td>";
                        tbody += "<td>" + val.LoginId + "</td>";
                        tbody += "</tr>";

                    });
                }
                $('#tblServiceUsedList tbody').append(tbody);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
