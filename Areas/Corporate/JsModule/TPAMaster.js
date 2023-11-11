var _TPAId = '';
$(document).ready(function () {
    GetTPAMasterInfo();
});
function GetTPAMasterInfo() {
    $('#tblTPAInfo tbody').empty();
    var url = config.baseUrl + "/api/Corporate/PanelQuerie";
    var objBO = {};
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetTPAMasterInfo';
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
                        tbody += "<tr>";
                        tbody += "<td>" + val.TPAId + "</td>";
                        tbody += "<td>" + val.TPAName + "</td>";
                        tbody += "<td>" + val.contactNo + "</td>";
                        tbody += "<td>" + val.emailId + "</td>";
                        tbody += "<td>" + val.FaxNo + "</td>";
                        tbody += "<td>" + val.Address + "</td>";
                        tbody += "<td><button onclick=EditTPA(this) class='btn btn-warning btn-xs'><i class='fa fa-edit'>&nbsp;</i></button></td>";
                        tbody += "</tr>";
                    });
                    $('#tblTPAInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function EditTPA(elem) {
    _TPAId = $(elem).closest('tr').find('td:eq(0)').text();
    var name = $(elem).closest('tr').find('td:eq(1)').text();
    var contact = $(elem).closest('tr').find('td:eq(2)').text();
    var emailId = $(elem).closest('tr').find('td:eq(3)').text();
    var faxNo = $(elem).closest('tr').find('td:eq(4)').text();
    var address = $(elem).closest('tr').find('td:eq(5)').text();

    $('#txtTPAName').val(name);
    $('#txtContactNo').val(contact);
    $('#txtEmailId').val(emailId);
    $('#txtFaxNo').val(faxNo);
    $('#txtAddress').val(address);
    $('#btnSave').text('Update');
}
function InsertTPA() {
    if ($('#txtTPAName').val() == '') {
        alert('Please Provide TPA Name');
        $('#txtTPAName').focus();
        return;
    }
    if ($('#txtContactNo').val() == '') {
        alert('Please Provide Contact No');
        $('#txtContactNo').focus();
        return;
    }
    var url = config.baseUrl + "/api/Corporate/InsertTPAMaster";
    var objBO = {};
    objBO.TPAId = _TPAId;
    objBO.TPAName = $('#txtTPAName').val();
    objBO.ContactNo = $('#txtContactNo').val();
    objBO.EmailId = $('#txtEmailId').val();
    objBO.FaxNo = $('#txtFaxNo').val();
    objBO.Address = $('#txtAddress').val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = ($('#btnSave').text() == 'Submit') ? 'InsertTPA' : 'UpdateTPA';
    debugger
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                Clear()
                GetTPAMasterInfo();
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
function Clear() {
    $('input:text').val('');
    $('#btnSave').text('Submit');
}





