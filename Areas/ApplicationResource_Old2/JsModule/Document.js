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
    objBO.Logic = "DocInfo";
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
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.DocName + "</td>";
                        tbody += "<td>" + val.docLocation + "<button onclick=window.open('" + val.docpath + '?v=' + date.getMilliseconds() + "','_blank') class='btn btn-warning btn-xs pull-right'><i class='fa fa-eye'>&nbsp;</i>View</button></td>";
                        tbody += "<td>";
                        tbody += "<label class='switch'>";
                        tbody += "<input type='checkbox' onchange=UpdateStatus('" + val.DocID + "') class='IsActive' " + val.checked + ">";
                        tbody += "<span class='slider round'></span>";
                        tbody += "</label>";
                        tbody += "</td>";
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
function UpdateStatus(docID) {
    var url = config.baseUrl + "/api/ApplicationResource/Insert_Modify_Documents";
    var objBO = {};
    objBO.DocID = docID;
    objBO.DocType = '-';
    objBO.DocName = '-';
    objBO.DocDescription = '-';
    objBO.docLocation = '-';
    objBO.docpath = '-';
    objBO.uploadBy = Active.userId;
    objBO.Logic = "UpdateStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
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
function Validation() {
    var docName = $('#txtDocumentName').val();
    var Description = $('#txtDescription').val();
    if (docName == '') {
        alert('Please Provide Document Name');
        $('#txtDocumentName').focus()
        return false
    }
    if (Description == '') {
        alert('Please Provide Description');
        $('#txtDescription').focus()
        return false
    }
    return true
}
function readURL(elem) {
    if (elem.files && elem.files[0]) {
        var ext = $(elem).val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['jpg', 'png', 'pdf']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgFile').removeAttr('src', '');
            $('#imgFile').attr('src', e.target.result);
        }
        reader.readAsDataURL(elem.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $(elem).get(0).files;
    }
}
function UploadFile(elem) {
    if ($(elem).siblings('input[type=file]').val() == '') {
        alert('Please Choose File')
        return
    }
    $(elem).addClass('loading');
    var objBO = {};
    var url = config.baseUrl + "/api/ApplicationResource/UploadDocument";
    objBO.DocID = 'New';
    objBO.DocType = $('#ddlDocumentType option:selected').val();
    objBO.DocName = $('#txtDocumentName').val();
    objBO.DocDescription = $('#txtDescription').val();
    objBO.docLocation = $('#ddlDocumentLocation option:selected').val();
    objBO.docpath = '-';
    objBO.hasfile = ($('#imgFile').attr('src').length > 10) ? 'Y' : 'N';
    objBO.fileExtention = $('#uploadFile').val().split('.').pop();
    objBO.Base64String = $('#imgFile').attr('src');
    objBO.uploadBy = Active.userId;
    objBO.Logic = "Merge";

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
                    window.open(FilePath, '_blank');
                    $(elem).removeClass('loading');
                    $('input:text,textarea').val('');
                    $('select').prop('selectedIndex', '0').trigger('change.select2');
                    DocInfo();
                }
                else {
                    alert(json);
                    $(elem).removeClass('loading');
                }
            }
        }
    }
    UploadDocumentInfo.open('POST', url, true);
    UploadDocumentInfo.send(data);
}