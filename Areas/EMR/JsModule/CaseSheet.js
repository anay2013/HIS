
$(document).ready(function () {
    CloseSidebar();
    GetRackInfo();
    FillCurrentDate('txtFrom')
    FillCurrentDate('txtTo')
    $("select").select2()
    $("#uploadFile").change(function () {
        readURL(this);
    });
});
function GetPatientInfo(logic) {
    if ($("#txtIPDNo").val() == '') {
        alert('Please Provide IPD No.');
        return
    }
    $("#tblPatientInfo tbody").empty();
    var url = config.baseUrl + "/api/EMR/EMR_DocumentQueries";
    var objBO = {};
    objBO.IPDNo = $("#txtIPDNo").val();
    objBO.from = $("#txtFrom").val();
    objBO.to = $("#txtTo").val();
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = '-';
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.FilePath != null)
                            tbody += '<tr style="background:#bbf1b3">';
                        else
                            tbody += '<tr>';

                        tbody += '<td class="hide">' + JSON.stringify(data.ResultSet.Table[count]) + '</td>';
                        tbody += '<td>' + val.IPDNo + '</td>';
                        tbody += '<td>' + val.PatientName + '</td>';
                        tbody += '<td>' + val.AdmitDate + '</td>';
                        tbody += '<td>' + val.uploadBy + '</td>';
                        tbody += '<td>' + val.DischargeDateTime + '</td>';
                        tbody += "<td><button onclick=ViewPatient(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += '</tr>';
                        count++;
                    });
                    $("#tblPatientInfo tbody").append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function ViewPatient(elem) {
    var date = new Date();
    var info = JSON.parse($(elem).closest('tr').find('td:eq(0)').text());
    $("#txtPatientIPDNO").text(info.IPDNo);
    $("#txtPatientName").text(info.PatientName);
    $("#txtAdmitDate").text(info.AdmitDate);
    $("#txtDischargeDateTime").text(info.DischargeDateTime);
    debugger
    var FilePath = info.FilePath + "?v=" + date.getMilliseconds();
    $("#filePath").prop('src', FilePath);
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var ext = $('#uploadFile').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['jpg', 'png', 'pdf']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgFile').removeAttr('src', '');
            $('#imgFile').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $('#uploadFile').get(0).files;
    }
}
function UploadCaseSheet(elem) {
    if ($("#ddlFileRack option:selected").text() == 'Select') {
        alert('Please Select File Rack.');
        return
    }
    $(elem).addClass('loading');
    var objBO = {};
    var url = config.baseUrl + "/api/EMR/UploadCaseSheet";
    objBO.IPDNo = $("#txtPatientIPDNO").text();
    objBO.RackId = $("#ddlFileRack option:selected").val();
    objBO.hasfile = ($('#imgFile').attr('src').length > 10) ? 'Y' : 'N';
    objBO.fileExtention = $('#uploadFile').val().split('.').pop();
    objBO.Base64String = $('#imgFile').attr('src');
    objBO.AdmitDate = $("#txtAdmitDate").text().split('-')[2];
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertCaseSheet';
    var UploadDocumentInfo = new XMLHttpRequest();
    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('ImageByte', objBO.Base64String);
    UploadDocumentInfo.onreadystatechange = function () {
        if (UploadDocumentInfo.status) {
            if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                var json = JSON.parse(UploadDocumentInfo.responseText);
                if (json.includes('Success')) {
                    var date = new Date();
                    alert('Successfully Uploaded..!');
                    var FilePath = json.split('|')[1] + "?v=" + date.getMilliseconds();
                    $("#filePath").prop('src', FilePath);
                    $(elem).removeClass('loading');
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

//rack master
var _rackId = "";
function InsertRackInfo() {
    if ($("#txtRackName").val() == '') {
        alert('Please Provide Rack Name.');
        return
    }
    var url = config.baseUrl + "/api/EMR/EMR_InsertUpdateDocument";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.RackId = _rackId;
    objBO.Prm1 = $("#txtRackName").val();
    objBO.Prm2 = $("#txtDescription").val();
    objBO.login_id = '-';
    objBO.Logic = ($('#btnSaveRack').text() == 'Save') ? "InsertRackInfo" : 'UpdateRackInfo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                GetRackInfo();
                $("#txtRackName").val('');
                $("#txtDescription").val('');
                $('#btnSaveRack').text('Save');
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetRackInfo() {
    if ($("#txtIPDNo").val() == '') {
        alert('Please Provide IPD No.');
        return
    }
    $("#tblRackInfo tbody").empty();
    $('#ddlFileRack').empty().append($('<option>Select</option>')).change();
    var url = config.baseUrl + "/api/EMR/EMR_DocumentQueries";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = '-';
    objBO.Logic = "GetRackInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += "<td>" +
                            '<label class="switch">' +
                            '<input onchange=UpdatStatus(' + val.RackId + ') type="checkbox" id="chkActive" ' + val.checked + '>' +
                            '<span class="slider round"></span>' +
                            '</label>' +
                            "</td>";
                        tbody += '<td class="hide">' + val.RackId + '</td>';
                        tbody += '<td>' + val.RackName + '</td>';
                        tbody += '<td>' + val.Description + '</td>';
                        tbody += '<td>' + val.cr_date + '</td>';
                        tbody += "<td><button onclick=editRackInfo(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += '</tr>';
                        count++;
                    });
                    $("#tblRackInfo tbody").append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlFileRack').append($('<option></option>').val(val.RackId).html(val.RackName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function editRackInfo(elem) {
    selectRow(elem)
    _rackId = $(elem).closest('tr').find('td:eq(1)').text();
    $('#btnSaveRack').text('Update');
    $('#txtRackName').val($(elem).closest('tr').find('td:eq(2)').text());
    $('#txtDescription').val($(elem).closest('tr').find('td:eq(3)').text());
}
function UpdatStatus(rackId) {
    var url = config.baseUrl + "/api/EMR/EMR_InsertUpdateDocument";
    var objBO = {};
    objBO.IPDNo = '-';
    objBO.RackId = rackId;
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = '-';
    objBO.Logic = "UpdateRackStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                GetRackInfo()
            }
            else {
                alert(data)
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}