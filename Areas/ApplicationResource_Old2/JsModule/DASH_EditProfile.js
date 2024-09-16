$(document).ready(function () {
    var data = JSON.parse(sessionStorage.getItem('abha_login_info'));
    $('#txtName').text(data.name)
    $('#txtGender').text((data.gender == 'M') ? 'Male' : 'Female')
    var gender = data.dayOfBirth + '-' + data.monthOfBirth + '-' + data.yearOfBirth;
    $('#txtDOB').text(gender)
    $('#txtAddress').text(data.address)
    $('#txtDistrict').text(data.districtName)
    $('#txtState').text(data.stateName)
    $('#txtPinCode').text(data.pincode)
    $('#txtMobileNo').val(data.mobile)
    $('#txtEmail').val(data.email)
    $('#profileImage').attr('src', 'data:image/png;base64,' + data.profilePhoto)    
})