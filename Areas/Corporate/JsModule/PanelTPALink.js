var _TPAId = '';
$(document).ready(function () {
    GetPanelInfo();
});
function GetPanelInfo() {
    $('#tblPanelInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetPanelInfoForTPALink';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.TPAName) {

                            tbody += '<tr style="background-color:#ddd">';
                            tbody += '<td colspan="3" >' + val.TPAName + '</td>';
                            tbody += '</tr>';
                            temp = val.TPAName;
                        }
                        tbody += "<tr>";
                        tbody += "<td class='hide'>" + val.auto_id + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td>" + val.PanelType + "</td>";                             
                        tbody += '<td>';
                        tbody += '<select>';
                        tbody += '<option value="-">Select</option>';
                        $.each(data.ResultSet.Table1, function (key, val1) {                            
                            tbody += (val1.TPAId == val.TPAId) ? '<option selected value=' + val1.TPAId + '>' + val1.TPAName : '<option value=' + val1.TPAId + '>' + val1.TPAName;
                        });
                        tbody += '</select>'; 
                        tbody += '<button onclick=LinkTPAToPanel(this) data-autoid=' + val.auto_id + ' class="btn btn-warning btn-xs"><i class="fa fa-sign-in">&nbsp;</i>Update</button>';
                        tbody += '</td>';
                        tbody += "</tr>";
                    });
                    $('#tblPanelInfo tbody').append(tbody);                    
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LinkTPAToPanel(elem) {
    if (confirm('Are you sure?')) {
        $(elem).addClass('loading');
        var url = config.baseUrl + "/api/Corporate/InsertTPAMaster";
        var objBO = {};
        objBO.TPAId = $(elem).closest('tr').find('select option:selected').val();
        objBO.TPAName = '-';
        objBO.ContactNo = '-';
        objBO.EmailId = '-';
        objBO.FaxNo = '-';
        objBO.Address = '-';
        objBO.Prm1 = $(elem).data('autoid');
        objBO.Prm2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'LinkTPAToPanel';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    $(elem).removeClass('loading');
                    GetPanelInfo();
                }
                else {
                    alert(data);
                };
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function Clear() {
    $('input:text').val('');
    $('#btnSave').text('Submit');
}





