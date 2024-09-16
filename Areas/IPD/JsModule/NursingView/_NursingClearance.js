$(document).ready(function () {

});
function DischargeInit() {
    if (confirm('are you sure to discharge?')) {
        if (_IPDNo == '') {
            alert('Please Choose Patient')
            return
        }
        if ($('#txtRemark').val() == '') {
            alert('Please Provide Remark.')
            $('#txtRemark').focus()
            return
        }
        var url = config.baseUrl + "/api/IPDNursing/IPD_DischargeProcess";
        var objBO = {};
        objBO.IPDNo = _IPDNo;
        objBO.EntrySource = '-';
        objBO.Remark = $('#txtRemark').val();
        objBO.Prm1 = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'Discharge_Init';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            traditional: true,
            success: function (data) {
                if (data.includes('Success')) {
                    alert('Successfully Submited');
                    $('#txtRemark').val('')
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