var _UHID = "";
$(document).ready(function () {
    $('select').select2();
    $("#uploadFile").change(function () {
        readURL(this);
    });
    $('#ddlCountry').on('change', function () {
        var cId = $(this).val();
        GetStateByCountry(cId, 'N');
    });
    $('#ddlState').on('change', function () {
        var sId = $(this).val();
        GetCityByState(sId, 'N');
    });
    $('#ddlCity').on('change', function () {
        GetLocality();
    });
    GetCountry();
});
function GetPatient() {
    if ($('#txtUHIDNo').val() == '') {
        alert('UHID Not Found');
        return
    }
    $('#tblDetails tbody').empty();
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.UHID = $('#txtUHIDNo').val();
    objBO.Logic = 'GetPatientByUHIDForEdit';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            _UHID = objBO.UHID;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td><input type='checkbox' checked disabled/></td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.gender + "</td>";
                        tbody += "<td>" + val.age + "</td>";
                        tbody += "<td>" + val.age_type + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.ipop_no + "</td>";
                        tbody += "<td>" + val.cr_date + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblDetails tbody').append(tbody);
                }
            }
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table1).length) {
                    $.each(data.ResultSet.Table1, function (key, val) {

                        $('#ddlTitle').val(val.Title).change();
                        $('#txtFirstName').val(val.FirstName);
                        $('#txtLastName').val(val.LastName);
                        $('#ddlGender option').map(function () {
                            if ($(this).text() == val.gender) {
                                $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        var dob = val.dob.split('-')[2] + '-' + val.dob.split('-')[1] + '-' + val.dob.split('-')[0];
                        $('#txtDOB').val(dob);
                        $('#txtMobileNo').val(val.mobile_no);
                        $('#ddlCountry option').map(function () {
                            if ($(this).text() == val.country) {
                                $('#ddlCountry').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#ddlState option').map(function () {
                            if ($(this).data('text') == val.state) {
                                $('#ddlState').val($(this).val()).trigger('change.select2');
                                GetCityByState($(this).val(), 'N');
                            }
                        });
                        $('#ddlCity option').map(function () {
                            if ($(this).data('text') == val.district) {
                                $('#ddlCity').val($(this).val()).trigger('change.select2');
                            }
                        });
                        $('#txtAddress').val(val.address);
                        $('#txtEmailId').val(val.email_id);
                        $('#txtMembershipNo').val(val.member_id);
                        $('#ddlMarital').val(val.marital_status).change();
                        $('#ddlReligion').val(val.religion).change();
                        $('#ddlIdProof').val(val.idType).change();
                        $('#txtIdNo').val(val.IDNo);
                        $('#ddlRelationOf').val(val.relation_of).change();
                        $('#txtRelationName').val(val.relation_name);
                        $('#txtRelationContact').val(val.relation_phone);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetCityByState(sId, logic) {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.prm_1 = sId;
    objBO.Logic = 'GetCityByState';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#ddlCity').empty().append($('<option>Select City</option>'));
                    $('#ddlReferralCity').empty().append($('<option>Select City</option>'));
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
                        $('#ddlReferralCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
                    });
                    $('#ddlCity').val(45).trigger('change.select2');
                    $('#ddlReferralCity').val(45).trigger('change.select2');
                }
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetStateByCountry(cId, logic) {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.prm_1 = cId;
    objBO.Logic = 'GetStateByCountry';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $('#ddlState').empty().append($('<option value="00">Select State</option>')).trigger('change.select2');
                    $('#ddlReferralState').empty().append($('<option value="00">Select State</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                        $('#ddlReferralState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                    });
                }
            }
        },
        complete: function (data) {
            if (logic == 'N')
                return
            else
                $('#ddlState').val(32).trigger('change.select2');
            GetCityByState(32, 'Y');
            $('#ddlReferralState').val(32).trigger('change.select2');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetLocality() {
    $('#ddlLocality').empty().append($('<option>Select Locality</option>')).trigger('change.select2');
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.SearcKey = $('#ddlState option:selected').text();
    objBO.SearchValue = $('#ddlCity option:selected').text();
    objBO.prm_1 = '-';
    objBO.Logic = 'GetLocality';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: false,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#ddlLocality').append($('<option></option>').val(val.locality_name).html(val.locality_name));
                    });
                }
            }
            else {
                alert("Data Not Found! Create New..");
            };
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetCountry() {
    var url = config.baseUrl + "/api/Appointment/Opd_AppointmentQueries";
    var objBO = {};
    objBO.Logic = 'All';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table5).length) {
                    $('#ddlCountry').empty().append($('<option>Select</option>'));
                    $.each(data.ResultSet.Table5, function (key, val) {
                        $('#ddlCountry').append($('<option></option>').val(val.CountryID).html(val.contry_Name));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table13).length) {
                    $('#ddlState').empty().append($('<option value="00">Select State</option>'));
                    $.each(data.ResultSet.Table13, function (key, val) {
                        $('#ddlState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table14).length) {
                    $('#ddlCity').empty().append($('<option>Select City</option>'));
                    $.each(data.ResultSet.Table14, function (key, val) {
                        $('#ddlCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table15).length) {
                    $('#ddlLocality').empty().append($('<option>Select Locality</option>')).trigger('change.select2');
                    $.each(data.ResultSet.Table15, function (key, val) {
                        $('#ddlLocality').append($('<option></option>').val(val.locality_name).html(val.locality_name));
                    });
                }
            }
            else {
                alert('No Record Found..');
            }
        },
        complete: function (response) {
            //$('#ddlCountry').val(14).trigger('change.select2');
            //$('#ddlState').val(32).trigger('change.select2');        

            //$('#ddlCity').val(45).trigger('change.select2');       
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Opd_EditPatientDetails(elem) {
    if (_UHID == '') {
        alert('UHID Not Found');
        return
    }
    if ($('#tblDetails tbody tr').length > 1) {
        if ($('input[type=file]').val() == '') {
            alert('Consent Not Found.');
            return
        }
    }

    if (confirm('Are you sure to Update this Information?')) {
        $(elem).addClass('loading');
        var url = config.baseUrl + "/api/Appointment/UploadEditPatientDetails";
        var objBO = {};
        var appNo = [];
        objBO.UHID = _UHID;
        objBO.Title = $('#ddlTitle option:selected').text();
        objBO.FirstName = $('#txtFirstName').val();
        objBO.LastName = $('#txtLastName').val();
        objBO.patient_name = ($('#txtLastName').val() != '') ? $('#txtFirstName').val() + ' ' + $('#txtLastName').val() : $('#txtFirstName').val();
        objBO.gender = $('#ddlGender option:selected').text();
        objBO.dob = ($('#txtDOB').val() == '') ? '1900/01/01' : $('#txtDOB').val();
        objBO.mobile_no = $('#txtMobileNo').val();
        $('#tblDetails tbody tr').each(function () {
            appNo.push($(this).find('td:eq(8)').text());
        });
        objBO.prm_1 = appNo.join('|');
        objBO.prm_2 = '-';
        objBO.hasfile = ($('input[type=file]').val() != '') ? 'Y' : 'N';
        objBO.FileExtention = $('input[type=file]').val().split('.').pop().toLowerCase();
        objBO.FileType = (objBO.FileExtention == 'pdf') ? 'application/pdf' : 'Image';
        objBO.Base64String = $('#imgFile').attr('src');
        objBO.login_id = Active.userId;
        objBO.Logic = 'UpdateCoreInfo';
        var UploadDocumentInfo = new XMLHttpRequest();
        var data = new FormData();
        data.append('obj', JSON.stringify(objBO));
        data.append('ImageByte', objBO.Base64String);
        UploadDocumentInfo.onreadystatechange = function () {
            if (UploadDocumentInfo.status) {
                if (UploadDocumentInfo.status == 200 && (UploadDocumentInfo.readyState == 4)) {
                    var json = JSON.parse(UploadDocumentInfo.responseText);
                    if (json.includes('Success')) {
                        alert('Successfully Uploaded..!');
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
}
function Opd_EditPatientDetails2() {
    if (_UHID == '') {
        alert('UHID Not Found');
        return
    }
    if (confirm('Are you sure to Update this Information?')) {
        var url = config.baseUrl + "/api/Appointment/Opd_EditPatientDetails";
        var objBO = {};
        var appNo = [];
        objBO.UHID = _UHID;
        objBO.country = $('#ddlCountry option:selected').text();
        objBO.state = $('#ddlState option:selected').text();
        objBO.district = $('#ddlCity option:selected').text();
        objBO.address = $('#txtAddress').val();
        objBO.email_id = $('#txtEmailId').val();
        objBO.member_id = $('#txtMembershipNo').val();
        objBO.marital_status = $('#ddlMarital option:selected').text();
        objBO.religion = $('#ddlReligion option:selected').text();
        objBO.idType = $('#ddlIdProof option:selected').text();
        objBO.IDNo = $('#txtIdNo').val();
        objBO.relation_of = $('#ddlRelationOf option:selected').text();
        objBO.relation_name = $('#txtRelationName').val();
        objBO.relation_phone = $('#txtRelationContact').val();
        $('#tblDetails tbody tr').each(function () {
            appNo.push($(this).find('td:eq(5)').text());
        });
        objBO.prm_1 = appNo.join();
        objBO.prm_2 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'UpdatePatientInfo';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            async: false,
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