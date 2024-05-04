var _ActivePageName;
var _IPDNo;
var _PatientName;
$(document).ready(function () {
    FloorAndPanelList();
    $('select').select2();
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    FillCurrentDate('txtApprDate');
    _ActivePageName = window.location.pathname.split('/')[3].toLowerCase()
});
function FloorAndPanelList() {
    $('#ddlPanel').empty().append($('<option></option>').val('ALL').html('ALL'));
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PanelListforTPApproval';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length > 0) {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table2).length) {
                    $('#ddlInsuranceList').append($('<option></option>').val('Select').html('Select')).select2();
                    $.each(data.ResultSet.Table2, function (key, val) {
                        $('#ddlInsuranceList').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AdmittedPatientList(logic) {
    $('#tblServiceRegister tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = $('#txtIPDNo').val();
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = "";
    objBO.Prm2 = '';
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
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr style='background:" + val.EntryStatus + "'>";
                        tbody += "<td><button onclick=SelectPatient(this) class='btn btn-warning btn-xs'><i class='fa fa-sign-in'></i></button></td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.BillNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td>" + val.BillDate + "</td>";
                        tbody += "<td class='text-right'>" + val.TotalAmount + "</td>";
                        tbody += "<td class='text-right'>" + val.TotalDiscount + "</td>";
                        tbody += "<td class='text-right'>" + val.NetPayable + "</td>";
                        tbody += "<td class='text-right'>" + val.Received + "</td>";
                        tbody += "<td class='text-right'>" + val.Balance + "</td>";
                        if (val.isCancelled == 'N')
                            tbody += "<td><button data-logic='IPDCancelBill' onclick=UpdateOPDClaimNo(this) class='btn btn-danger btn-xs'>Cancel</button></td>";
                        else
                            tbody += "<td>Cancelled</td>";
                        tbody += "</tr>";
                    });
                    $('#tblServiceRegister tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateOPDClaimNo(elem) {
    if (confirm('Are you sure?')) {
        var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalEntry";
        var objBO = [];
        objBO.push({
            'AutoId': 0,
            'IPDNo': $(elem).closest('tr').find('td:eq(1)').text(),
            'InsuranceId': "-",
            'ClaimNo': "-",
            'ApprovalDate': '1900/01/01',
            'ApprovalType': "-",
            'ApprovalAmount': 0,
            'CoPayAmount': 0,
            'Discount': 0,
            'Remark': "-",
            'doc_path': $(elem).closest('tr').find('input:text').val(),
            'login_id': Active.userId,
            'Logic': $(elem).data('logic')
        });
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $(elem).closest('tr').css('background', '#a4df9d"')
                }
                else
                    alert(data);
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function AddTPApprovalEntry() {
    var ApprovalType = $('#ddlApprType option:selected').text();
    var ApprovalDate = $('#txtApprDate').val();
    var InsuranceId = $('#ddlInsuranceList option:selected').val();
    var InsuranceCompany = $('#ddlInsuranceList option:selected').text();
    var ClaimNo = $('#txtClaimNo').val();
    var ApprovalAmount = $('#txtAmount').val();
    var CoPayAmount = $('#txtCoPayAmt').val();
    var Discount = $('#txtDiscount').val();
    var Remark = $('#txtRemark').val();
    if (ApprovalType == 'Select') {
        alert('Please Select Approval Type');
        return;
    }
    if (InsuranceId == 'Select') {
        alert('Please Select Insurance Id');
        return;
    }
    if (ApprovalAmount == '') {
        alert('Please Select Insurance Id');
        return;
    }
    if (parseFloat($('#txtAmount').val()) <= 0) {
        alert("Amount should be greater than Zero")
        return;
    }

    $('#tblTpaApprInfo tbody tr').each(function () {
        if ($(this).find('td:eq(3)').text() == InsuranceCompany) {
            alert('This Insurance Company Already Added');
            return
        }
    });
    var tbody = "";
    tbody += "<tr class='NewEntry'>";
    tbody += "<td><button onclick=$(this).closest('tr').remove() class='btn btn-danger btn-xs'><i class='fa fa-trash'></i></button></td>";
    tbody += "<td class='hide'>" + InsuranceId + "</td>";
    tbody += "<td class='hide'>" + ApprovalDate + "</td>";
    tbody += "<td>" + InsuranceCompany + "</td>";
    tbody += "<td>" + ApprovalType + "</td>";
    tbody += "<td>" + ClaimNo + "</td>";
    tbody += "<td>" + ApprovalAmount + "</td>";
    tbody += "<td>" + CoPayAmount + "</td>";
    tbody += "<td>" + Discount + "</td>";
    tbody += "<td>" + Remark + "</td>";
    tbody += "<td>" + ApprovalDate + "</td>";
    tbody += "<td>-</td>";
    tbody += "</tr>";

    $('#tblTpaApprInfo tbody').append(tbody);
    $('#ddlInsuranceList').prop('selectedIndex', '0').change();
    $('#txtClaimNo').val('-');
    $('#txtAmount').val(0);
    $('#txtCoPayAmt').val(0);
    $('#txtDiscount').val(0);
    $('#txtRemark').val('-');
}
function IPD_TPApprovalEntry() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalEntry";
    var objBO = [];
    var totalAmountPay = 0;
    $('#tblTpaApprInfo tbody tr').each(function () {
        totalAmountPay += parseFloat($(this).find('td:eq(6)').text()) + parseFloat($(this).find('td:eq(7)').text()) + parseFloat($(this).find('td:eq(8)').text());
        if ($(this).hasClass('NewEntry')) {
            objBO.push({
                'AutoId': 0,
                'IPDNo': _IPDNo,
                'InsuranceId': $(this).find('td:eq(1)').text(),
                'ClaimNo': $(this).find('td:eq(5)').text(),
                'ApprovalDate': $(this).find('td:eq(2)').text(),
                'ApprovalType': $(this).find('td:eq(4)').text(),
                'ApprovalAmount': $(this).find('td:eq(6)').text(),
                'CoPayAmount': $(this).find('td:eq(7)').text(),
                'Discount': $(this).find('td:eq(8)').text(),
                'Remark': $(this).find('td:eq(9)').text(),
                'doc_path': '-',
                'login_id': Active.login_id,
                'Logic': 'Insert'
            });
        }
    });
    if (totalAmountPay > parseFloat($('#txtNetAmt').val())) {
        alert('Approval Amount should be equal to Balance Amount')
        return
    }

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
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

function SelectPatient(element) {
    selectRow($(element))
    _IPDNo = $(element).closest('tr').find('td:eq(1)').text();
    _PatientName = $(element).closest('tr').find('td:eq(3)').text();
    NetAmount = parseFloat($(element).closest('tr').find('td:eq(8)').text());
    AdvanceAmount = parseFloat($(element).closest('tr').find('td:eq(9)').text());
    BalanceAmount = parseFloat($(element).closest('tr').find('td:eq(10)').text());

    $('#txtNetAmt').val(NetAmount.toFixed(2));
    $('#txtAdvanceAmt').val(AdvanceAmount.toFixed(2));
    $('#txtBalanceAmt').val(BalanceAmount.toFixed(2));
    $('#txtPatientName').val(_IPDNo + " : " + _PatientName);
    TPAApprovalList();
}

function TPAApprovalList() {
    $('#tblTpaApprInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDBilling/IPD_BillingQuerries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = _IPDNo;
    objBO.DoctorId = '';
    objBO.Floor = '';
    objBO.PanelId = '';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = "";
    objBO.Prm2 = '';
    objBO.login_id = Active.userId;
    objBO.Logic = 'TPAApprovalListByIpdNo';
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
                        if (val.IsRejected == 'Y')
                            tbody += "<tr style='background:#ffaa52' class='added'>";
                        else if (val.IsSettled == 'Y')
                            tbody += "<tr style='background:#c9e9c9' class='added'>";
                        else
                            tbody += "<tr class='added'>";
                        tbody += "<td>-</td>";
                        tbody += "<td class='hide'>" + val.AdmitDate + "</td>";
                        tbody += "<td class='hide'>-</td>";
                        tbody += "<td>" + val.PanelName + "</td>";
                        tbody += "<td>" + val.ApprovalType + "</td>";
                        tbody += "<td>" + val.ClaimNo + "</td>";
                        tbody += (parseFloat(val.ApprovalAmount) == 0) ? "<td><input type='text' value=" + val.ApprovalAmount + " class='form-control'/></td>" : "<td>" + val.ApprovalAmount + "</td>";
                        tbody += (parseFloat(val.ApprovalAmount) == 0) ? "<td><input type='text' value=" + val.CoPayAmount + " class='form-control'/></td>" : "<td>" + val.CoPayAmount + "</td>";
                        tbody += (parseFloat(val.ApprovalAmount) == 0) ? "<td><input type='text' value=" + val.Discount + " class='form-control'/></td>" : "<td>" + val.Discount + "</td>";
                        tbody += "<td>" + val.Remark + "</td>";
                        tbody += "<td>" + val.ApprovalDate + "</td>";
                        tbody += "<td><div class='flex'>";
                        if (val.IsSettled == 'N') {
                            tbody += "<input type='file' onchange=readURL(this) id='uploadFile'/><img class=hide id='imgFile'/>";
                            if (val.doc_path == 'N/A')
                                tbody += "<button data-id=" + val.auto_id + " onclick=UploadFile(this) class='btn btn-warning btn-xs'><i class='fa fa-upload'>&nbsp;</i>Upload</button>&nbsp;&nbsp;";
                            else
                                tbody += "<button data-id=" + val.auto_id + " onclick=UploadFile(this) class='btn btn-warning btn-xs'><i class='fa fa-upload'>&nbsp;</i>Re-Upload</button>&nbsp;&nbsp;";
                        }
                        if (val.doc_path != 'N/A')
                            tbody += "<button onclick=viewDoc('" + val.doc_path + "') class='btn btn-success btn-xs'><i class='fa fa-eye'>&nbsp;</i>View</button>";
                        else
                            tbody += "-";

                        tbody += "</div></td>";
                        tbody += "<td class='hide'>" + val.InsuranceProvId + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblTpaApprInfo tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function viewDoc(link) {
    window.open(link, '_blank');
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
            $(elem).closest('tr').find('#imgFile').removeAttr('src', '');
            $(elem).closest('tr').find('#imgFile').attr('src', e.target.result);
        }
        reader.readAsDataURL(elem.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $(elem).get(0).files;
    }
}
function RejectEntry(AutoId) {
    var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalEntry";
    var objBO = {};
    objBO.AutoId = AutoId;
    objBO.IPDNo = _IPDNo;
    objBO.ApprovalDate = $('#txtApprDate').val();
    objBO.ApprovalType = $('#ddlApprType option:selected').val();
    objBO.ApprovalAmount = "0";
    objBO.CoPayAmount = "0";
    objBO.ClaimNo = "-";
    objBO.Remark = "-";
    objBO.login_id = Active.userId;
    objBO.Logic = "RejectEntry";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success'))
                TPAApprovalList();
            else
                alert(data);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateInsCompAmt() {
    var url = config.baseUrl + "/api/IPDBilling/IPD_TPApprovalEntry";
    var objBO = [];
    var totalAmountPay = 0;
    $('#tblTpaApprInfo tbody tr').each(function () {
        totalAmountPay += parseFloat($(this).find('td:eq(6)').find('input').val()) + parseFloat($(this).find('td:eq(7)').find('input').val()) + parseFloat($(this).find('td:eq(8)').find('input').val());
        objBO.push({
            'AutoId': 0,
            'IPDNo': _IPDNo,
            'InsuranceId': $(this).find('td:last').text(),
            'ClaimNo': $(this).find('td:eq(5)').text(),
            'ApprovalDate': $(this).find('td:eq(2)').text(),
            'ApprovalType': $(this).find('td:eq(4)').text(),
            'ApprovalAmount': $(this).find('td:eq(6)').find('input').val(),
            'CoPayAmount': $(this).find('td:eq(7)').find('input').val(),
            'Discount': $(this).find('td:eq(8)').find('input').val(),
            'Remark': '-',
            'doc_path': '-',
            'login_id': Active.login_id,
            'Logic': 'UpdateInsCompAmt'
        });
    });
    if (totalAmountPay > parseFloat($('#txtBalanceAmt').val())) {
        alert('Approval Amount should be equal to Balance Amount')
        return
    }

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data)
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
function UploadFile(elem) {
    if ($(elem).siblings('input[type=file]').val() == '') {
        alert('Please Choose File')
        return
    }
    $(elem).addClass('loading');
    var objBO = [];
    var url = config.baseUrl + "/api/IPDBilling/UploadInsuranceDocument";
    var apprDate = $(elem).closest('tr').find('td:eq(10)').text().split('-');
    objBO.push({
        'IPDNo': _IPDNo,
        'InsuranceId': '-',
        'ClaimNo': '-',
        'ApprovalDate': apprDate[2] + '-' + apprDate[1] + '-' + apprDate[0],
        'ApprovalType': $(elem).closest('tr').find('td:eq(4)').text(),
        'ApprovalAmount': $(elem).closest('tr').find('td:eq(6)').text(),
        'CoPayAmount': $(elem).closest('tr').find('td:eq(7)').text(),
        'Discount': $(elem).closest('tr').find('td:eq(8)').text(),
        'Remark': $(elem).closest('tr').find('td:eq(9)').text(),
        'hasfile': ($(elem).closest('tr').find('#imgFile').attr('src').length > 10) ? 'Y' : 'N',
        'fileExtention': $(elem).closest('tr').find('#uploadFile').val().split('.').pop(),
        'Base64String': $(elem).closest('tr').find('#imgFile').attr('src'),
        'AdmitDate': $(elem).closest('tr').find('td:eq(1)').text(),
        'AutoId': $(elem).data('id'),
        'doc_path': '-',
        'login_id': Active.login_id,
        'Logic': 'UpdateDocument'
    });
    var UploadDocumentInfo = new XMLHttpRequest();
    var data = new FormData();
    data.append('obj', JSON.stringify(objBO));
    data.append('ImageByte', objBO[0].Base64String);
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