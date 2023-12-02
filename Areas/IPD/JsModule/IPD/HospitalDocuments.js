$(document).ready(function () {

    $('#IPDNo').on('keyup', function () {
        var val = $(this).val().toLowerCase();

        $('#tblPatientRegister tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });
    $('#txtPatientName').on('keyup', function () {
        var val = $(this).val().toLowerCase();

        $('#tblPatientRegister tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });
    $('#tblPatientRegister tbody').on('click', '.PatientReports', function () {

        $('span[data-pname]').text($(this).data('pname'));
        $('span[data-ipdno]').text($(this).data('ipdno'));
        $('span[data-uhidno]').text($(this).data('uhidno'));
        $('span[data-admitdate]').text($(this).data('admitdate'));
        $('span[data-doctor]').text($(this).data('doctor'));
        $('span[data-gender]').text($(this).data('gender'));

        //$('#formList').find("button").prop('disabled', false);

        $('#ddlDoumentList').prop('disabled', false);

    });

    $('#ddlDoumentList').on('change', function () {
        var val = $(this).find('option:selected').text();
        SurgeryDocList(val)

    });

    DoucmentListGroup();

    FloorAndPanelList();
    $('select').select2();
    FillCurrentDate('txtSearchFrom');
    FillCurrentDate('txtSearchTo');
    $('#txtSearchFrom,#txtSearchTo').prop('disabled', true);
    $('#ddlActive').on('change', function () {
        var val = $(this).find('option:selected').text();
        if (val == 'Active')
            $('#txtSearchFrom,#txtSearchTo').prop('disabled', true);
        else
            $('#txtSearchFrom,#txtSearchTo').prop('disabled', false);
    });
    $('#txtSearchFrom,#txtSearchTo').prop('disabled', true);
    $('#btnPatient').on('click', function () {
        var logic = "PatientRegister:InPatient";
        if ($('#ddlActive option:selected').text() == 'Active')
            logic = "PatientRegister:ITDose";
        else
            logic = "PatientRegister:ITDose";

        PatientRegister(logic);

    });


});



function DoucmentListGroup() {
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
    objBO.Logic = 'FormListGroup';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    //$('#ddlDoumentList').append($('<option></option>').val('Select').html('Select')).select2();
                    $('#ddlDoumentList').append($('<option></option>').val('Select').html('Select')).select2().prop('disabled', true);
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlDoumentList').append($('<option></option>').val(val.FormFor).html(val.FormFor));

                    });
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function patientDetails(ipdNo) {
    $('#tblPatientInfo tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = ipdNo;
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '-';
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'PatientRegister:ByIPDNo';
    //objBO.Logic = 'SurgeryDocList:ByIPDNo';
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
                        tbody += "<td>" + val.AdmissionType + "</td>";
                        tbody += "<td>" + val.RoomType + "</td>";
                        tbody += "<td>" + val.roomFullName + "</td>";
                        tbody += "<td>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td>" + val.MLCType + "</td>";
                        tbody += "<td>" + val.RefName + "</td>";
                        tbody += "<td>" + val.PatientType + "</td>";
                        tbody += "<td>" + val.PolicyNo + "</td>";
                        tbody += "<td>" + val.SourceName + "</td>";
                        tbody += "<td>" + val.DischargeDate + "</td>";
                        tbody += "<td>" + val.DischargeBy + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblPatientInfo tbody').append(tbody);
                    $('#modalPatientInfo').modal('show');
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });


}
function SurgeryDocList(selectvalue) {
    $('#formList').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '-';
    objBO.UHID = '-';
    objBO.IPDNo = '-';
    objBO.Floor = '-';
    objBO.PanelId = '-';
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = selectvalue;
    objBO.Prm2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'FormList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            var html = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        html += '<div class="col-md-12 formInfo">';
                        html += '<label class="hide">' + JSON.stringify(data.ResultSet.Table[count]) + '</label>';
                        html += '<button id="btnPrintSurgery" data-type="Single" onclick=PrintData(this) class="btn btn-warning btn-sm" style="float:right; margin-top:2px;width: 63px;height: 26px;font-size: 12px;"><i class="fa fa-print"></i>&nbsp;Print</button>';
                        html += '<label class="formName">' + val.DocName + '</label>';
                        html += '<label class="Details">' + val.DocDescription + '</label>';
                        html += '</div>';
                        count++;
                    })
                    $('#formList').append(html);
                }
                var printAllButton = $('<button class="btn btn-success btn-sm" data-type="Multi" id="btnPrintAll" onclick=PrintData(this) style="margin-top:10px;">Print All</button>');
                $('#formList').append(printAllButton);

            }
        },
        error: function (error) {
            console.error("Error fetching data: " + error);
        }
    });
}
function PrintData(element) {
    var pname = $('[data-pname]').text();
    var uhidno = $('[data-uhidno]').text();
    var gender = $('[data-gender]').text();
    var admitdate = $('[data-admitdate]').text();
    var ipdno = $('[data-ipdno]').text();
    var doctor = $('[data-doctor]').text();
    var Diagnosis = "";
    var objBO = [];

    if ($(element).data('type') == 'Single') {
        var info = JSON.parse($(element).closest('.formInfo').find('label').eq(0).text());
        objBO.push({
            'UsedIn': $('#ddlDoumentList option:selected').val(),
            'templateName': info.templateName,
            'DocName': info.DocName,
            'PageName': info.PageName,
            'PageIndex': info.PageIndex,
            'pname': pname,
            'uhidno': uhidno,
            'gender': gender,
            'admitdate': admitdate,
            'ipdno': ipdno,
            'doctor': doctor,
            'Diagnosis': Diagnosis,
            'PageOrientation': info.PageOrientation,
            'FormHeader': info.FormHeader
        });
    }
    if ($(element).data('type') == 'Multi') {
        $("#formList .formInfo").each(function () {
            var info = JSON.parse($(this).find('label').eq(0).text());
            objBO.push({
                'UsedIn': $('#ddlDoumentList option:selected').val(),
                'templateName': info.templateName,
                'DocName': info.DocName,
                'PageName': info.PageName,
                'PageIndex': info.PageIndex,
                'pname': pname,
                'uhidno': uhidno,
                'gender': gender,
                'admitdate': admitdate,
                'ipdno': ipdno,
                'doctor': doctor,
                'Diagnosis': Diagnosis,
                'PageOrientation': info.PageOrientation,
                'FormHeader': info.FormHeader

            });
        })
    }

    $.ajax({
        method: "POST",
        url: config.rootUrl + '/IPD/Print/PrintFormsList',
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        complete: function () {
            window.open(config.rootUrl + '/IPD/Print/PrintForms', '_blank');
        },
    });
}
function FloorAndPanelList() {
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
    objBO.Logic = 'FloorAndPanelList';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $('#ddlFloor').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlFloor').append($('<option></option>').val(val.FloorName).html(val.FloorName));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $('#ddlPanel').append($('<option></option>').val('ALL').html('ALL')).select2();
                    $.each(data.ResultSet.Table1, function (key, val) {
                        $('#ddlPanel').append($('<option></option>').val(val.PanelId).html(val.PanelName));
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PatientRegister(logic) {
    $('#tblPatientRegister tbody').empty();
    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
    var objBO = {};
    objBO.hosp_id = '';
    objBO.UHID = '';
    objBO.IPDNo = '';
    objBO.ageInfo = '';

    objBO.Floor = $('#ddlFloor option:selected').val();
    objBO.PanelId = $('#ddlPanel option:selected').val();
    objBO.from = $('#txtSearchFrom').val();
    objBO.to = $('#txtSearchTo').val();
    objBO.Prm1 = $('#ddlActive option:selected').val();
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
            console.log(data);
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IpdStatus == 'Out')
                            tbody += "<tr style='background:#ffb9b9'>";
                        else
                            tbody += "<tr>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.IPDNo + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.DoctorName + "</td>";
                        tbody += "<td>" + val.RoomName + "</td>";
                        //tbody += "<td>" + val.RoomBillingCategory + "</td>";
                        tbody += "<td>" + val.RoomTypeRequest + "</td>";
                        tbody += "<td>" + val.AdmitDate + "</td>";

                        tbody += '<td><button class="btn btn-success PatientReports btn-xs" data-pname="' + val.patient_name + '", data-ipdno="' + val.IPDNo + '", data-Uhidno="' + val.UHID + '", data-admitdate="' + val.AdmitDate + '", data-doctor="' + val.DoctorName + '" data-gender="' + val.ageInfo + '"><i class="fa fa-arrow-right"></i></button></td>';
                        tbody += "</tr>";

                    });
                    $('#tblPatientRegister tbody').append(tbody);
                }

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

//function patientDetail(ipdNo) {

//    //$('#tblPatientRegister').empty(); 
//    var url = config.baseUrl + "/api/IPDNursingService/IPD_PatientQueries";
//    var objBO = {};
//    objBO.hosp_id = '-';
//    objBO.UHID = '-';
//    objBO.IPDNo = ipdNo;
//    objBO.Floor = '-';
//    objBO.PanelId = '-';
//    objBO.from = '1900/01/01';
//    objBO.to = '1900/01/01';
//    objBO.Prm1 = '-';
//    objBO.Prm2 = '-';
//    objBO.login_id = Active.userId;
//    objBO.Logic = 'PatientDetail';
//    //objBO.Logic = 'SurgeryDocList:ByIPDNo';
//    $.ajax({
//        method: "POST",
//        url: url,
//        data: JSON.stringify(objBO),
//        contentType: "application/json;charset=utf-8",
//        dataType: "JSON",
//        success: function (data) {
//            console.log(data)
//            if (Object.keys(data.ResultSet).length) {
//                var tbody = "";
//                if (Object.keys(data.ResultSet.Table).length) {
//                    $.each(data.ResultSet.Table, function (key, val) {

//                        tbody = data.ResultSet.Table[0];
//                        $('span[data-pname]').text(val.patient_name);
//                        $('span[data-gender]').text(val.ageInfo);
//                        $('span[data-Uhidno]').text(val.UHID);
//                        $('span[data-ipdno]').text(val.IPDNo);



//                    });
//                    $('#tblPatientRegister').append(tbody);

//                }
//            }
//        },
//        error: function (response) {
//            alert('Server Error...!');
//        }
//    });


//}


