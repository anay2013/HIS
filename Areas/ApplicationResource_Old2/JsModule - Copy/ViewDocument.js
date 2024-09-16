var _DocID = 0;
$(document).ready(function () {
    $('select').select2();
    DocInfo();
});
function DocInfo() {
    $('#tblDocMaster tbody').empty();
    var url = config.baseUrl + "/api/ApplicationResource/DocumentsQueries";
    var objBO = {};
    objBO.DocID = '-';
    objBO.docLocation = '-';
    objBO.docpath = '-';
    objBO.uploadBy = Active.userId;
    objBO.Logic = "DocInfoForUsers";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var temp = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        var date = new Date();
                        count++;
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.DocID + "</td>";
                        tbody += "<td class='hide'>" + val.docpath + "</td>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.DocName + "</td>";
                        tbody += "<td><button onclick=ViewDoc(this) class='btn btn-warning btn-xs pull-right'><i class='fa fa-eye'>&nbsp;</i>View</button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblDocMaster tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ViewDoc(elem) {
    selectRow(elem)
    var url = $(elem).closest('tr').find('td:eq(1)').text();
    var DocName = $(elem).closest('tr').find('td:eq(3)').text();
    $('#txtDocName').text(DocName);
    $('iframe').prop('src', url);
}
