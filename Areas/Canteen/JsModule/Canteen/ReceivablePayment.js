$(document).ready(function () {
    FillCurrentDate("txtSearchFrom");
    FillCurrentDate("txtSearchTo");
    $("#chkallshift").change(function () {
        if (this.checked) {
            $(".shiftchk").each(function () {
                this.checked = true;
            })
        } else {
            $(".shiftchk").each(function () {
                this.checked = false;
            })
        }
    });
});


function GetDataAllList() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Canteen/CanteenReport_Queries";
    var objBO = {};
    objBO.unit_id = '-';
    objBO.from = $("#txtSearchFrom").val();
    objBO.to = $("#txtSearchTo").val();
    objBO.prm_1 = '-';
    objBO.logic = 'ReceivableReport';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            var tbody = '';
            if (data.ResultSet && data.ResultSet.Table) {
                $.each(data.ResultSet.Table, function (key, val) {
                    if (eval(val.PaidAmt)>0)
                        tbody += "<tr style='background-color:lightgreen'>";
                    else
                        tbody += "<tr>";

                    tbody += "<td><input type='checkbox' class='shiftchk' id='partyid' value='" + val.party_id + "'/></td>";
                    tbody += "<td>" + val.cust_name + "</td>";
                    tbody += "<td style='text-align:center;width:5%'><input type='text' style='height:20px; width:80px; margin-bottom:2px; background:#e7e7e7;text-align:center;' id='txtAmount' value='" + val.Amount + "' onkeyup=calvalue() ></td>";
                    tbody += "<td style='text-align:center'>" + val.PaidAmt + "</td>";
                    tbody += "</tr>";
                });
                $('#tblReport tbody').append(tbody);
                calvalue();

            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function SaveCanteen() {
    if ($('#txtRemark').val() == '') {
        alert('Enter Remark.');
        return;
    }
    var url = config.baseUrl + "/api/Canteen/InsertCanteen";
    var objBO = [];
    $('#tblReport tbody tr').find('input[type=checkbox]:checked').each(function () {

        if (eval($(this).closest('tr').find('td:eq(2) input#txtAmount').val()) > 0) {
            objBO.push({
                'Cust_id': $(this).closest('tr').find('td:eq(0) input#partyid').val(),
                'amount': $(this).closest('tr').find('td:eq(2) input#txtAmount').val(),
                'CantId': 'CHCANT',
                'Prm1': $("#ddlPaymode option:selected").val(),
                'remark': $("#txtRemark").val(),
                'loginId': Active.userId,
                'from': $("#txtSearchFrom").val(),
                'to': $("#txtSearchTo").val(),
                'Logic': 'SaveData:Canteen',
            });
        }
    });
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                alert(data);
                $("#txtRemark").val('')
                $('#btnsave').prop('disabled', true);
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

function calvalue() {
    var total = 0;
    $('#tblReport tbody tr').each(function () {
        total += parseFloat($(this).find('td:eq(2) input#txtAmount').val());
        $('#txtTotal').val(total.toFixed(2));
    });
}