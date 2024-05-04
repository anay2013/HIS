
var temp = "";
$(document).ready(function () {
    FillCurrentDate('txtfromdate');
    FillCurrentDate('txtTodate');
    CloseSidebar();
    GetDataItemList();
});

function GetDataItemList() {
    $('#tblAppliedChecklist tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_CheckListQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txttodate').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'LoadAutoChargeItems';
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
                        if (val.AutoSchFreq == "Daily") {
                            tbody += "<td style='width:10%;text-align:center'><input type='checkbox' style='width:15px; height:15px;' value='" + val.ItemId + "'checked /></td>";
                        }
                       
                        else {
                            tbody += "<td style='width:10%;text-align:center'><input type='checkbox' style='width:15px; height:15px;' value='" + val.ItemId + "' /></td>";
                        }
                        tbody += "<td style='width:40%;text-align:left'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:40%;text-align:left'>" + val.SubCatName + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblAppliedChecklist tbody').append(tbody);
                   
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetDataAppliedServiceCheckInfo() {
    $('#tblAppliedChecklistInfo tbody').empty(); 
    var checkvaluelist = "";
    var checkboxes = document.querySelectorAll('#tblAppliedChecklist tbody input[type=checkbox]:checked');
    checkboxes.forEach(function (checkbox) {
        var checkedValue = checkbox.closest('tr').querySelector('td:first-child input[type=checkbox]').value;
        checkvaluelist = checkvaluelist + checkedValue + "|";
    });
  // alert(checkvaluelist);
    var url = config.baseUrl + "/api/IPDBilling/IPD_CheckListQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txtTodate').val();
    objBO.Prm1 = checkvaluelist;
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'AppliedList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.IPDNoInfo) {
                            tbody += "<tr style='background:#e5e5e5;'>";
                            tbody += "<td colspan='16' style='font-size:13px;'><b>" + val.IPDNoInfo + "</b></td>";
                            tbody += "</tr>";
                            temp = val.IPDNoInfo
                        }
                        else {

                        }
                        tbody += "<tr>";
                       
                        tbody += "<td style='width:10%;text-align:left'>" + val.ItemId + "</td>";
                        tbody += "<td style='width:25%;text-align:left'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:25%;text-align:left'>" + val.emp_name + "</td>";
                        tbody += "<td style='width:20%;text-align:left'>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td style='width:10%;text-align:left'>" + val.tnxDate + "</td>";
                        tbody += "<td style='width:10%;text-align:center'>" + val.amount + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblAppliedChecklistInfo tbody').append(tbody);


                }
            }
        },
        error: function (response) {
            console.log('Server Error...!', response);
        }
    });
} 
function GetDataAppliedSearch() {
    $('#tblAppliedChecklist tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_CheckListQuerries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = $('#txtfromdate').val();
    objBO.to = $('#txtTodate').val();
    objBO.Prm1 = $("#txtItemname").val();
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'SearchItem';
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
                        tbody += "<td style='width:10%;text-align:center'><input type='checkbox' style='width:15px; height:15px;' value='" + val.ItemId + "' /></td>";
                        tbody += "<td style='width:40%;text-align:left'>" + val.ItemName + "</td>";
                        tbody += "<td style='width:40%;text-align:left'>" + val.SubCatName + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblAppliedChecklist tbody').append(tbody);
                   
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
