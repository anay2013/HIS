var length = 0;
var length1 = 0;
var _doctorId = '';
$(document).ready(function () {
    CloseSidebar();
    DiagnosticDisplay();
});
function DiagnosticDisplay() {
    var count = 0;
    $('.DoctorDisplay').hide();
    $('.DoctorDisplay').css('height', '99vh');
    $('.AllPending').css('height', '90vh');
    $('#myMarquee').css('height', '100%');
    $('#myMarquee').isNan;
    var url = config.baseUrl + "/api/master/DoctorMasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.prm_1 = '-';
    objBO.Logic = "DiagnosticDisplay";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('.DoctorDisplay').show();
            $('.DoctorDisplay').css('height', '99vh');
            $('.AllPending').css('height', '90vh');
            $('#myMarquee').css('height', '100%');
            var tbody = "";

            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        tbody += '<tr>';
                        tbody += '<td class="text-center">' + val.DeptRoomNo + '</td>';
                        tbody += '<td>' + val.patient_name + '</td>';
                        tbody += (parseInt(val.Token_No) < 10) ? '<td class="text-center">' + '0' + val.Token_No : '<td class="text-center">' + val.Token_No + '</td>';
                        tbody += '</tr>';
                    });
                    if (count < 1) {
                        $('.DoctorDisplay .tblAllPendingAbsent tbody').empty();
                    }
                    if (count < 7) {
                        length = 0;
                        $('.DoctorDisplay .tblAllPendingAbsent tbody').empty();
                        //$('.DoctorDisplay .tblAllPendingAbsent1 tbody').empty().append(tbody);
                        tbody += '<tr>';
                        tbody += "<td colspan='3' class='text-center'>End Of List</td>";
                        tbody += "</tr>"
                        tbody += "<tr><td colspan='3' style='background:#fff' class='text-center'><img src='" + config.rootUrl + "/Content/logo/line.png'</td></tr>"
                        $('.DoctorDisplay .tblConsulting tbody').empty().append(tbody);
                        $('.AllConsulting').css('height', '90vh');
                    }
                    else {
                        length++;
                        tbody += '<tr>';
                        tbody += "<td colspan='3' class='text-center'>End Of List</td>";
                        tbody += "</tr>"
                        tbody += "<tr><td colspan='3' style='background:#fff' class='text-center'><img src='" + config.rootUrl + "/Content/logo/line.png'</td></tr>"
                        $('.DoctorDisplay .tblAllPendingAbsent tbody').empty();
                        $('.DoctorDisplay .tblAllPendingAbsent tbody').empty().append(tbody + tbody + tbody + tbody + tbody + tbody + tbody + tbody);
                        if (length > 0) {
                            document.getElementById('myMarquee').stop();
                            document.getElementById('myMarquee').start();
                        }
                    }
                }
            }
        },
        complete: function () {
            if (count > 7) {
                var consWidth0 = $('.DoctorDisplay .tblAllPendingAbsent tbody').find('td:eq(0)').width();
                var consWidth1 = $('.DoctorDisplay .tblAllPendingAbsent tbody').find('td:eq(1)').width();
                var consWidth2 = $('.DoctorDisplay .tblAllPendingAbsent tbody').find('td:eq(2)').width();
                // var consWidth2 = $('.DoctorDisplay .tblAllPendingAbsent tbody').find('td:eq(2)').width();
                $('.DoctorDisplay .tblConsulting thead').find('td:eq(0)').width(consWidth0);
                $('.DoctorDisplay .tblConsulting thead').find('td:eq(1)').width(consWidth1);
                $('.DoctorDisplay .tblConsulting thead').find('td:eq(2)').width(consWidth2);
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}