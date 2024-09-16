var depname = "";
var depid = "";
var Autoid = "";
var filepath = "";

$(document).ready(function () {
    $('#tblReport tbody').on('click', '#btndetails', function () {
        debugger
        Autoid = $(this).closest('tr').find('td:eq(0)').text();
        depid = $(this).closest('tr').find('td:eq(1)').text();
        depname = $(this).closest('tr').find('td:eq(2)').text();
        Urlrow = $(this).closest('tr').find('td:eq(3)').text();
        $("#txtDeparmentName").text(depname);
        $("#txtDepid").text(depid);
        var FilePath = Urlrow;
        $("#filePath").prop('src', FilePath);
    });
    GetDepartmentList();

    $("#txtfileUpload").change(function () {
        var file = this.files[0];
        if (file) {
            var url = URL.createObjectURL(file);
            $("#filePath").prop('src', url).show();
        }
    });
});




function GetDepartmentList() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Doctors/Web_DepartmentQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.login_id = Active.userId;
    objBO.DeptName = '-';
    objBO.DeptId = '-';
    objBO.Logic = "GetDeparmentList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            if (Object.keys(data.ResultSet.Table).length > 0) {

                $.each(data.ResultSet.Table, function (key, val) {
                    tbody += '<tr>';
                    tbody += '<td style="display:none">' + val.autoId + '</td>';
                    tbody += '<td style="width: 18%;text-align:center">' + val.DeptId + '</td>';
                    tbody += '<td style="width:57%;text-align:left">' + val.dept_name + '</td>';
                    tbody += "<td style='width:20%; text-align:center' hidden>'" + val.Img_url + "'</td>";
                    if (val.Img_url !== null && val.Img_url != '') {
                        tbody += "<td style='width:20%; text-align:center'><img src='" + val.Img_url + "'  style='height: 25px; width:25px;'></td>";
                    } else {
                        tbody += "<td></td>";
                    }
                    tbody += '<td style="width:57%;text-align:left" hidden>' + val.seqNo + '</td>';
                    tbody += "<td style='width:5%; text-align:center'><button class='btn btn-warning btn-xs' id='btndetails'><i class='fa fa-eye'></i></button></td>";
                    tbody += '</tr>';

                });
                $('#tblReport tbody').append(tbody);

            }
        }
    })
}

function UploadSheet(elem) {
    var url = config.baseUrl + "/api/Doctors/UploadDepartmentDoc";
    var objBO = {};
    objBO.autoId = Autoid;
    objBO.DeptId = depid;
    objBO.ImgPostion = '-';
    objBO.Description = '-';
    objBO.IsActive = '-';
    objBO.ImgPath = $('#txtfileUpload').val().split('.').pop();
    objBO.login_id = Active.userId;
    objBO.fileUpload = '-';
    objBO.Logic = "URLUploadDepartment";
    var UploadDocumentInfo = new XMLHttpRequest();
    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('imageByte', $('input[name=fileUpload]')[0].files[0]);
    UploadDocumentInfo.onreadystatechange = function () {
        if (UploadDocumentInfo.status) {
            if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                var json = JSON.parse(UploadDocumentInfo.responseText);
                if (json.includes('Success')) {
                    alert(json);
                    GetDepartmentList();
                    $('#txtfileUpload').val('');
                    $("#filePath").prop('src', '').show();
                }
                else {
                    alert(json);
                    GetDepartmentList();
                }
            }
        }
    }
    UploadDocumentInfo.open('POST', url, true);
    UploadDocumentInfo.send(data);
}
