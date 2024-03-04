var _IPOPNo, _IPOPType, _IndentNo;
$(document).ready(function () {
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    IssuedIndentList('IssuedIndentByDate');
});
function IssuedIndentList(logic) {
    $('#tblIssueIndentInfo tbody').empty();
    var url = config.baseUrl + "/api/BloodBank/BB_SelectQueries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.DonorId = '-';
    objBO.VisitId = '-';
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Prm1 = $('input[name=IndentBy]:checked').val();
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
            console.log(data)
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    var counter = 0;
                    var count = 0;
                    $.each(data.ResultSet.Table, function (key, val) {
                        counter++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + JSON.stringify(data.ResultSet.Table[count]) + "</td>";
                        tbody += "<td>" + counter + "</td>";
                        tbody += "<td>" + val.IPOPNo + "</td>";
                        tbody += "<td>" + val.PatientName + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.IssuedDate + "</td>";
                        tbody += "<td>" + val.issueBy + "</td>";
                        tbody += "<td>" + val.BBTubeNo + "</td>";
                        tbody += "<td>" + val.ComponentName + "</td>";
                        tbody += "<td>" + val.Visit_ID + "</td>";
                        tbody += "<td class='text-center'>" + val.Quantity + "</td>";
                        tbody += "<td>" + val.BloodGroupAllotted + "</td>";
                        tbody += "<td style='padding:0px 5px'><button data-autoid=" + val.AutoId + " style='height: 17px;line-height:0;' onclick=RetakeIssueIndent(this) class='btn btn-danger btn-xs'>Retake</button></td>";
                        tbody += "<td>" + val.IsActive + "</td>";
                        tbody += "<td class='hide'>" + val.Stock_ID + "</td>";
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblIssueIndentInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function RetakeIssueIndent(elem) {
    if (confirm('Are you sure to Retake?')) {
        var url = config.baseUrl + "/api/BloodBank/BB_Insert_ModifyBloodIssue";
        var objBO = {};
        objBO.AutoId = $(elem).data('autoid');
        objBO.hosp_id = Active.HospId;
        objBO.Stock_ID = $(elem).closest('tr').find('td:last').text();
        objBO.ItemID = '-';
        objBO.IndentNo = '-';
        objBO.IPOPNo = $(elem).closest('tr').find('td:eq(5)').text();
        objBO.IPOPType = '-';
        objBO.Quantity = '-';
        objBO.Prm1 = $(elem).closest('tr').find('select option:selected').val();
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = "RetakeIssueIndent";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    IssuedIndentList('IssuedIndentByDate');
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}