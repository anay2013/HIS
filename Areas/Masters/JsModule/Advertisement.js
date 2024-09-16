var Autoidd = 0;
$(document).ready(function () {
    GetAdvertiesment();
    $('#tblReport tbody').on('click', '#btndetails', function () {
        Autoidd = $(this).closest('tr').find('td:eq(1)').text();
        EditAdvertiesment(Autoidd);
    });
});

function InsertAdvertisement(logic) {
    var depname = $("#ddlDepartment option:selected").val();
    if (depname == "Select") {
        alert("Please Select Department ");
        return;
        $("#ddlDepartment").focus();
    }
    var url = config.baseUrl + "/api/master/InsertUpdateAdvertiment";
    var objBO = {};
    objBO.Autoid = Autoidd;
    objBO.Department = $("#ddlDepartment option:selected").val();
    objBO.Remark = $("#txtcontent").val();
    objBO.Cr_date = '1999-01-01';
    objBO.IsActive = 'Y';
    objBO.Url = $('#txtfileUpload').val().split('.').pop();
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    var UploadDocumentInfo = new XMLHttpRequest();
    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('imageByte', $('input[name=fileUpload]')[0].files[0]);
    UploadDocumentInfo.onreadystatechange = function () {
        if (UploadDocumentInfo.status) {
            if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                var json = JSON.parse(UploadDocumentInfo.responseText);
                console.log(json);
                if (json.includes('Success')) {
                    alert(json);
                    GetAdvertiesment();
                    $("#ddlDepartment").val('Select');
                    $("#txtcontent").val('');
                    $('#txtfileUpload').val('').split('.').pop();

                }
                else {
                    alert(json);

                }
            }
        }
    }
    UploadDocumentInfo.open('POST', url, true);
    UploadDocumentInfo.send(data);
}
function GetAdvertiesment() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/master/AdvertisementQueries";
    var objBO = {};
    objBO.Autoid = '0';
    objBO.Department = '-';
    objBO.Remark = '-';
    objBO.Cr_date = '1999-01-01';
    objBO.IsActive = 'Y';
    objBO.Url = '-';
    objBO.login_id = Active.userId;
    objBO.Prm1 = '-';
    objBO.Logic = "GetAdvertisement";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += "<td>";
                        tbody += "<label class='switch'>";
                        tbody += "<input type='checkbox' data-ddid=" + val.Autoid + " data-logic='ActiveStatus' onchange=ActivestatusListUpdate(this) class='IsActive' id='chkActive' " + val.IsActive + ">";
                        tbody += "<span class='slider round'></span>";
                        tbody += "</label>";
                        tbody += "</td>";
                        tbody += '<td hidden>' + val.Autoid + '</td>';
                        tbody += '<td>' + val.Department + '</td>';
                        if (val.Url !== '') {
                            tbody += "<td><img src='" + val.Url + "'  style='height: 25px; width:25px;'></td>";
                        } else {
                            console.log(val.Url);
                        }

                        tbody += '<td>' + val.Cr_date + '</td>';
                        tbody += '<td>' + val.Remark + '</td>';
                        tbody += '<td hidden>' + val.IsActive + '</td>';
                        tbody += "<td><button class='btn btn-warning btn-xs' id='btndetails'><i class='fa fa-edit'></i></button></td>";
                        tbody += '</tr>';


                    });
                    $('#tblReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }

    });
}
function EditAdvertiesment(Autoidd) {
    var url = config.baseUrl + "/api/master/AdvertisementQueries";
    var objBO = {};
    objBO.Autoid = Autoidd;
    objBO.Department = '-';
    objBO.Remark = '-';
    objBO.Cr_date = '1999-01-01';
    objBO.IsActive = 'Y';
    objBO.Url = '-';
    objBO.login_id = Active.userId;
    objBO.Prm1 = '-';
    objBO.Logic = "EditAdvertisement";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.Auto_id != "") {
                            $("#ddlDepartment").val(data.ResultSet.Table[0].Department);
                            // $("#txtfileUpload").val(data.ResultSet.Table[0].Url);
                            $("#txtcontent").val(data.ResultSet.Table[0].Remark);
                            $("#btnUpdate").show();
                            $("#btnSave").hide();

                        }
                        else {
                            $("#btnSave").show();
                            $("#btnUpdate").hide();
                        }

                    });

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function ActivestatusListUpdate(elem) {
    var url = config.baseUrl + "/api/master/AdvertisementInsert";
    var objBO = {};
    objBO.Autoid = $(elem).data('ddid');
    objBO.Department = '-';
    objBO.Remark = '-';
    objBO.Cr_date = '1999-01-01';
    objBO.IsActive = '-';
    objBO.Url = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = "UpdateStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                GetAdvertiesment();
            }
            else {
                alert(data);
                GetAdvertiesment();
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}