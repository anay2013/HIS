var _photo_url = null;
$(document).ready(function () {
    //$('.patientinfo-sectio    $('input:text').attr('autocomplete', 'off');
    FillCurrentDate('txtOnlineBookingDate')
    FillCurrentDate('hiddenDOBId');
    let canvas = document.querySelector("#canvas");
    $('#ImgCaptured').hide();

    $('#start-camera').on('click', async function () {
        let stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
        video.srcObject = stream;
        $('#ImgCaptured').hide("slide", { direction: "right" }, 500, function () {
            $('#liveCamera').show("slide", { direction: "left" }, 500);
        });
        $('#liveCamera label').text('Recording..');
    });
    $('#click-photo').on('click', function () {
        canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
        let image_data_url = canvas.toDataURL('image/jpeg');
        $('#liveCamera').hide("slide", { direction: "left" }, 500, function () {
            $('#ImgCaptured').show("slide", { direction: "right" }, 500);
        });
        _photo_url = image_data_url;
    });
    $('#stop-camera').on('click', function () {
        _photo_url = null;
        $('#liveCamera').hide();
        $('#ImgCaptured').hide();
    });
    $('#btnOldPatient').on('click', function () {
        $('#tblOldPatient tbody').empty();
        $('#txtSearchValue').val('');
        $('#modalOldPatient').modal('show');
        Clear();

    });
    $('#btnSearchOldPatient').on('click', function () {
        GetOldPatient('GetOldPatient');
    });
    $('#btnSearchKiosk').on('click', function () {
        GetOldPatient('GetOldPatientByKiosk');
    });
    $('select').select2();
    FillCurrentDate("txtAppointmentOn");
    $('#ddlCountry').on('change', function () {
        var cId = $(this).val();
        GetStateByCountry(cId, 'N');
    });
    $('#ddlCity').on('change', function () {
        GetLocality();
    });
    $('#txtDOB').on('change', function () {
        var todayDate = new Date($('#hiddenDOBId').val());
        var dobDate = new Date($(this).val());
        if (dobDate > todayDate) {
            alert('DOB should not be greater than current Date');
            $(this).val('')
            return
        }
        var currentDate = new Date().getFullYear();
        var dob = $(this).val();
        var year = dob.split('-');
        var age = parseInt(currentDate) - parseInt(year[0]);
        $('#txtAge').val(age);
    });
    $('#ddlState').on('change', function () {
        var sId = $(this).val();
        GetCityByState(sId, 'N');
    });
    $('#ddlReferralState').on('change', function () {
        var sId = $(this).val();
        GetCityByState(sId, 'N');
    });

    $('#tblOldPatient tbody').on('click', 'button.select', function () {
        var uhid = $(this).closest('tr').find('td:eq(1)').text();
        $('#BasicInformation').find('.infosection').removeClass('blockInfo');
        //if (uhid.includes('New'))
        //    GetOldPatientByUHID(uhid.split('-')[1]);
        //else
        GetOldPatientByUHID(uhid);
    });
    $('#tblOnlinePatientBooking tbody').on('click', 'button', function () {
        var info = JSON.parse($(this).closest('tr').find('td:eq(1)').text());
        FillOnlinePatientInfo(info);
        $('#modalOnlinePatient').hide();
    });
    $('#btnNewPatient').on('click', function () {
        $('#BasicInformation').find('.infosection').removeClass('blockInfo');
        $('#txtUHID').val('New');
        $('#txtMobileNo').val($('#txtSearchValue').val());
        $('#txtSearchValue').val('');
        $('#modalOldPatient').modal('hide');
        $('#btnNewPatient').hide();
        $('.patientinfo-section-1,.patientinfo-section-2').removeClass('blockUI');
    });
    $('#btnPrintReceipt').on('click', function () {
        var tnxid = $('#txtTnxIdForPrint').text();
        Receipt(tnxid);
    });
    GetCountry();
});
function CreateLocality() {
    var stateName = $('#ddlState option:selected').text();
    var districtName = $('#ddlCity option:selected').text();
    if (stateName == 'Select State') {
        alert('Please Select State Name');
        return;
    }
    if (districtName == 'Select City') {
        alert('Please Select City Name');
        return;
    }
    $('#modalLocality').modal('show');
}
function CancelPatientRecord(elem) {
    if (confirm('Are you sure to Cancel?')) {
        var url = config.baseUrl + "/api/Appointment/Opd_InsertAppointmentAssets";
        var objBO = {};
        objBO.hosp_id = Active.unitId;
        objBO.prm_1 = $(elem).closest('tr').find('td:eq(1)').text();
        objBO.login_id = Active.userId;
        objBO.Logic = 'CancelPatientRecord';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $(elem).closest('tr').remove();
                }
                else {
                    alert(data);
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function InsertLocality() {
    if ($('#txtNewLocality').val() == '') {
        alert('Please Provide Locality Name');
        return;
    }
    var url = config.baseUrl + "/api/Appointment/Opd_InsertAppointmentAssets";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.c_address = $('#ddlState option:selected').text();
    objBO.prm_1 = $('#ddlCity option:selected').text();
    objBO.SourceName = $('#txtNewLocality').val();
    objBO.login_id = Active.userId;
    objBO.Logic = 'InsertLocality';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                $('#txtNewLocality').val('');
                $('#modalLocality').modal('hide');
                GetLocality();
            }
            else {
                alert(data);
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FillOnlinePatientInfo(val) {
    $('#txtUHID').val(val.UHID);
    $('#txtFirstName').val(val.patient_name);
    $('#ddlGender option').map(function () {
        if ($(this).text() == val.gender) {
            $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
        }
    });
    $('#txtMobileNo').val(val.mobile_no);
    if (val.dob != null) {
        //var d = val.dob.split('-');
        //var newDOB = d[0] + '-' + d[1] + '-' + d[2];
        $('#txtDOB').val(val.dob.split('T')[0]).trigger('change.select2');
    }
    $('#txtAge').val(val.age);
    $('#ddlAgeType option').map(function () {
        if ($(this).text() == val.age_type) {
            $('#ddlAgeType').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
        }
    });
    $('#txtAddress').val(val.address);
    $('#txtEmailId').val(val.email_id);
}
function GetOldPatientByUHID(uhid) {
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    var date = new Date();
    objBO.hosp_id = Active.unitId;
    objBO.UHID = (uhid.includes('New')) ? uhid.split('-')[1] : uhid;
    objBO.MobileNo = '-';
    objBO.SearcKey = '-';
    objBO.SearchValue = '-';
    objBO.from = date;
    objBO.to = date;
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = (uhid.includes('New')) ? 'GetOldPatientInfoKiosk' : 'GetOldPatientByUHID';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtBarcode').val(val.barcodeno);
                        (uhid.includes('New')) ? $('#txtUHID').val('New') : $('#txtUHID').val(val.UHID);
                        $('#ddlTitle option').map(function () {
                            if ($(this).text() == val.Title) {
                                $('#ddlTitle').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#txtFirstName').val(val.FirstName);
                        $('#txtLastName').val(val.LastName);
                        $('#ddlGender option').map(function () {
                            if ($(this).text() == val.gender) {
                                $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#txtMobileNo').val(val.mobile_no);
                        if (val.dob != null) {
                            var d = val.dob.split('-');
                            var newDOB = d[0] + '-' + d[1] + '-' + d[2];
                            $('#txtDOB').val(newDOB).trigger('change.select2');
                        }
                        $('#txtAge').val(val.age);
                        $('#ddlAgeType option').map(function () {
                            if ($(this).text() == val.age_type) {
                                $('#ddlAgeType').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        //$('#ddlCountry option').map(function () {
                        //	if ($(this).text() == val.country) {
                        //		$('#ddlCountry').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                        //	}
                        //});
                        $('#ddlState option').map(function () {
                            if ($(this).data('text') == val.state) {
                                $('#ddlState').val($(this).val()).trigger('change.select2');
                                GetCityByState($(this).val(), 'N');
                            }
                        });
                        $('#txtAddress').val(val.address);
                        $('#txtEmailId').val(val.email_id);
                        $('#txtMembershipNo').val(val.member_id);
                        $('#ddlMaritalStatus option').map(function () {
                            if ($(this).text() == val.marital_status) {
                                $('#ddlMaritalStatus').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#ddlReligion option').map(function () {
                            if ($(this).text() == val.religion) {
                                $('#ddlReligion').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#ddlIdProof option').map(function () {
                            if ($(this).text() == val.idType) {
                                $('#ddlIdProof').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#txtIdNo').val(val.IDNo);
                        $('#ddlRelationOf option').map(function () {
                            if ($(this).text() == val.relation_of) {
                                $('#ddlRelationOf').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#txtRelationName').val(val.relation_name);
                        $('#txtRelationContact').val(val.relation_phone);
                        $('#modalOldPatient input:text').val('');
                        $('#modalOldPatient').modal('hide');
                        $('.patientinfo-section-2').removeClass('blockUI');
                    });
                }
            }
        },
        complete: function (com) {
            if (Object.keys(com.responseJSON.ResultSet).length > 0) {
                if (Object.keys(com.responseJSON.ResultSet.Table).length > 0) {
                    $.each(com.responseJSON.ResultSet.Table, function (key, val) {
                        $('#ddlCity option').map(function () {
                            if ($(this).data('text') == val.district) {
                                $('#ddlCity').val($(this).val()).trigger('change.select2');
                            }
                        });
                    });
                }
            }
            PaymentAll();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetBookingByAppNo(appno) {
    var logic = 'Test';
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    var date = new Date();
    objBO.MobileNo = '-';
    objBO.SearcKey = '-';
    objBO.SearchValue = appno;
    objBO.from = date;
    objBO.to = date;
    objBO.Logic = 'GetBookingByAppNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        async: true,
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#txtUHID').val(val.UHID);
                        $('#txtFirstName').val(val.patient_name);
                        $('#ddlGender option').map(function () {
                            if ($(this).text() == val.gender) {
                                $('#ddlGender').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#txtMobileNo').val(val.mobile_no);
                        $('#txtAge').val(val.age);
                        $('#ddlAgeType option').map(function () {
                            if ($(this).text() == val.age_type) {
                                $('#ddlAgeType').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        //$('#ddlVisitType option').map(function () {
                        //	if ($(this).text() == val.visitType) {
                        //		$('#ddlVisitType').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                        //	}
                        //});
                        $('#ddlDoctor option').map(function () {
                            if ($(this).val() == val.DoctorId) {
                                $('#ddlDoctor').prop('selectedIndex', '' + $(this).index() + '').trigger('change.select2');
                            }
                        });
                        $('#txtTime').val(val.AppInTime + '-' + val.AppOutTime);
                        $('#txtAppointmentOn').val(val.AppDate.split('T')[0]);
                        $('#txtUHID').val(val.UHID);
                    });
                }
            }
        },
        complete: function (response) {
            $('#ddlDoctor').prop('disabled', true).change();
            $('#txtAppointmentOn').prop('disabled', true);
            $('input[value=Availability]').prop('disabled', true).css('background', 'green');
            $('#txtTime').prop('disabled', true);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetOldPatient(logic) {
    if ($('#txtSearchValue').val() == '') {
        alert('Please Provide Search Value');
        return
    }
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    var date = new Date();
    objBO.hosp_id = Active.unitId;
    objBO.UHID = '-';
    objBO.MobileNo = '-';
    objBO.SearcKey = $('#ddlSearchKey option:selected').val();
    objBO.SearchValue = $('#txtSearchValue').val();
    objBO.from = date;
    objBO.to = date;
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = logic;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#tblOldPatient tbody').empty();
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    var tbody = "";
                    if (data.ResultSet.Table.length > 0) {
                        $('#btnNewPatient').show();
                        $.each(data.ResultSet.Table, function (key, val) {
                            tbody += "<tr>";
                            tbody += "<td><button class='btn-success btn-xs select'>Select</button></td>";
                            tbody += "<td>" + val.UHID + "</td>";
                            tbody += "<td>" + val.patient_name + "</td>";
                            tbody += "<td>" + val.gender + "</td>";
                            tbody += "<td>" + val.dob + "</td>";
                            tbody += "<td>" + val.age + ' ' + val.age_type + "</td>";
                            tbody += "<td>" + val.mobile_no + "</td>";
                            tbody += "<td>" + val.address + "</td>";
                            tbody += "<td>" + val.cr_date + "</td>";
                            tbody += "<td><button onclick=CancelPatientRecord(this) class='btn-danger btn-xs cancel'><i class='fa fa-close'>&nbsp;</i>Cancel</button></td>";
                            if (logic != 'GetOldPatientByKiosk')
                                tbody += "<td><button data-abha=" + val.ABHANo + " onclick=LinkABHA(this) class='btn-" + ((val.ABHANo == null) ? 'warning' : 'success') + " btn-xs cancel'><i class='fa fa-" + ((val.ABHANo == null) ? 'link' : 'eye') +"'>&nbsp;</i>" + ((val.ABHANo == null)?'Link':'View') + " ABHA</button></td>";
                            else
                                tbody += "<td>-</td>";

                            tbody += "</tr>";
                        });
                        $('#tblOldPatient tbody').append(tbody);
                    }
                    else {
                        $('#btnNewPatient').show();
                        tbody += "<tr>";
                        tbody += "<td class='text-danger text-center' colspan='8'>Patient Record Not Found..</td>";
                        tbody += "</tr>";
                        $('#tblOldPatient tbody').append(tbody);
                    }
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function LinkABHA(elem) {
    var obj = {};
    var abhaAddress = $(elem).data('abha');
    var uhid = $(elem).closest('tr').find('td:eq(1)').text();
    var name = $(elem).closest('tr').find('td:eq(2)').text();
    if (abhaAddress == null) {
        obj.abhaAddress = abhaAddress;
        obj.uhid = uhid;
        obj.name = name;
        obj.IsABHALink = false;
        sessionStorage.setItem('ABHA_UHID_Info',JSON.stringify(obj));  
        var url = config.rootUrl + '/Admin/ManageABHA1?mid=SM446';
        window.open(url, '_blank')
    }
    else {
        obj.abhaAddress = abhaAddress;
        obj.uhid = uhid;
        obj.name = name;
        obj.IsABHALink = true;
        sessionStorage.setItem('ABHA_UHID_Info', JSON.stringify(obj));       
        var url = config.rootUrl + '/Admin/abha_Dashboard?mid=SM446';
        window.open(url, '_blank')
    }
}
function GetOnlinePatient() {
    $('#tblOnlinePatientBooking tbody').empty();
    var url = config.baseUrl + "/api/Appointment/Patient_MasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.UHID = '-';
    objBO.MobileNo = '-';
    objBO.SearcKey = '-';
    objBO.SearchValue = '-';
    objBO.from = $('#txtOnlineBookingDate').val();
    objBO.to = '1900/01/01';
    objBO.prm_1 = $('#ddlVisitType option:selected').text();
    objBO.prm_2 = '-';
    objBO.login_id = Active.userId;
    objBO.Logic = 'GetOnlinePatientByDate';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            var tbody = "";
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (val.IsConfirmed == '1')
                            tbody += "<tr style='background:#b4e7ba;'>";
                        else
                            tbody += "<tr>";

                        tbody += "<td style='width:1%;padding:2px 0'><button class='btn-success btn-flat'>Select</button></td>";
                        tbody += "<td class='hide'>" + JSON.stringify(data.ResultSet.Table[count]) + "</td>";
                        tbody += "<td>" + val.UHID + "</td>";
                        tbody += "<td>" + val.patient_name + "</td>";
                        tbody += "<td>" + val.ageInfo + "</td>";
                        tbody += "<td>" + val.mobile_no + "</td>";
                        tbody += "<td>" + val.address + "</td>";
                        tbody += "<td>" + val.AppDate + "</td>";                      
                        tbody += "</tr>";
                        count++;
                    });
                    $('#tblOnlinePatientBooking tbody').append(tbody);
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
                    $('#ddlReferralState').empty().append($('<option value="00">Select State</option>'));
                    $.each(data.ResultSet.Table13, function (key, val) {
                        $('#ddlState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                        $('#ddlReferralState').append($('<option data-text="' + val.state_name + '" data-countryid=' + val.country_id + '></option>').val(val.state_code).html(val.state_name));
                    });
                }
            }
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table14).length) {
                    $('#ddlCity').empty().append($('<option>Select City</option>'));
                    $('#ddlReferralCity').empty().append($('<option>Select City</option>'));
                    $.each(data.ResultSet.Table14, function (key, val) {
                        $('#ddlCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
                        $('#ddlReferralCity').append($('<option data-text="' + val.distt_name + '" data-stateid=' + val.state_code + '></option>').val(val.dist_code).html(val.distt_name));
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
            $('#ddlCountry').val(14).trigger('change.select2');
            $('#ddlState').val(32).trigger('change.select2');
            $('#ddlReferralState').val(32).trigger('change.select2');

            $('#ddlCity').val(45).trigger('change.select2');
            $('#ddlReferralCity').val(45).trigger('change.select2');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function getDOB(days) {
    var url = config.baseUrl + "/api/Service/Opd_ServiceQueries";
    var objBO = {};
    objBO.prm_1 = days;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Logic = 'GetDOB';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        async: false,
        success: function (data) {
            if (Object.keys(data.ResultSet).length) {
                if (Object.keys(data.ResultSet.Table).length) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        var d = val.dob.split('T')[0];
                        $('#txtDOB').val(d);
                    });
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Receipt(tnxid) {
    var url = "../Print/AppointmentReceipt?TnxId=" + tnxid + "&ActiveUser=" + Active.userName;
    window.open(url, '_blank');
}