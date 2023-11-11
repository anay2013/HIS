
$(document).ready(function () {
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
});

function ReceiptTypeWise() {
    $('#tblReport tbody').empty();
    var url = config.baseUrl + "/api/Finance/Financial_Queries";
    var objBO = {};
    objBO.hosp_id = Active.HospId;
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.prm_1 = '-';
    objBO.prm_2 = '-';
    objBO.loginId = 'ALL';
    objBO.Logic = $('#ddlType option:selected').val();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            var temp = "";
            var count = 0;
            var p_Cash = 0;
            var p_SwipeCard = 0;
            var p_Cheque = 0;
            var p_NEFT_RTGS = 0;
            var n_Cash = 0;
            var n_SwipeCard = 0;
            var n_Cheque = 0;
            var n_NEFT_RTGS = 0;
            var FinalTotal = 0;

            var p_Cash1 = 0;
            var p_SwipeCard1 = 0;
            var p_Cheque1 = 0;
            var p_NEFT_RTGS1 = 0;
            var n_Cash1 = 0;
            var n_SwipeCard1 = 0;
            var n_Cheque1 = 0;
            var n_NEFT_RTGS1 = 0;
            var FinalTotal1 = 0;

            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.typeName) {
                            if (count > 0) {
                                tbody += '<tr>';
                                tbody += '<td colspan="2" class="text-right" style="background:#ddd;font-size:12px;"><b>' + temp + ' Total</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_Cash + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_Cheque + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_SwipeCard + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_NEFT_RTGS + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_Cash + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_Cheque + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_SwipeCard + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_NEFT_RTGS + '</b></td>';
                                tbody += '<td class="text-right" style="background:#ddd;font-size:12px;"><b>' + FinalTotal + '</b></td>';
                                tbody += '</tr>';
                            }
                            tbody += '<tr style="background:#c5e5ff">';
                            tbody += '<td colspan="11">' + val.typeName + '</td>';
                            tbody += '</tr>';
                            temp = val.typeName;

                            p_Cash = 0;
                            p_SwipeCard = 0;
                            p_Cheque = 0;
                            p_NEFT_RTGS = 0;
                            n_Cash = 0;
                            n_SwipeCard = 0;
                            n_Cheque = 0;
                            n_NEFT_RTGS = 0;
                            FinalTotal = 0;
                            count++
                        }
                        p_Cash += val.p_Cash;
                        p_Cheque += val.p_Cheque;
                        p_SwipeCard += val.p_SwipeCard;
                        p_NEFT_RTGS += val.p_NEFT_RTGS;
                        n_Cash += val.n_Cash;
                        n_Cheque += val.n_Cheque;
                        n_SwipeCard += val.n_SwipeCard;
                        n_NEFT_RTGS += val.n_NEFT_RTGS;
                        FinalTotal += val.FinalTotal;

                        p_Cash1 += val.p_Cash;
                        p_Cheque1 += val.p_Cheque;
                        p_SwipeCard1 += val.p_SwipeCard;
                        p_NEFT_RTGS1 += val.p_NEFT_RTGS;
                        n_Cash1 += val.n_Cash;
                        n_Cheque1 += val.n_Cheque;
                        n_SwipeCard1 += val.n_SwipeCard;
                        n_NEFT_RTGS1 += val.n_NEFT_RTGS;
                        FinalTotal1 += val.FinalTotal;


                        tbody += '<tr>';
                        tbody += '<td>' + val.IPOPType + '</td>';
                        tbody += '<td>' + val.ReceiptType + '</td>';
                        tbody += '<td class="text-right">' + val.p_Cash + '</td>';
                        tbody += '<td class="text-right">' + val.p_Cheque + '</td>';
                        tbody += '<td class="text-right">' + val.p_SwipeCard + '</td>';
                        tbody += '<td class="text-right">' + val.p_NEFT_RTGS + '</td>';
                        tbody += '<td class="text-right">' + val.n_Cash + '</td>';
                        tbody += '<td class="text-right">' + val.n_Cheque + '</td>';
                        tbody += '<td class="text-right">' + val.n_SwipeCard + '</td>';
                        tbody += '<td class="text-right">' + val.n_NEFT_RTGS + '</td>';
                        tbody += '<td class="text-right">' + val.FinalTotal + '</td>';
                        tbody += '</tr>';
                    });
                    tbody += '<tr>';
                    tbody += '<td colspan="2" class="text-right" style="background:#ddd;font-size:12px;"><b>' + temp + ' Total</b></td>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_Cash + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_Cheque + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_SwipeCard + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + p_NEFT_RTGS + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_Cash + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_Cheque + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_SwipeCard + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + n_NEFT_RTGS + '</b></th>';
                    tbody += '<th class="text-right" style="background:#ddd;font-size:12px;"><b>' + FinalTotal + '</b></th>';
                    tbody += '</tr>';

                    tbody += '<tr style="background:#f8bfbf">';
                    tbody += '<td colspan="2" class="text-right" style="font-size:12px;"><b>Overall Total</b></td>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + p_Cash1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + p_Cheque1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + p_SwipeCard1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + p_NEFT_RTGS1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + n_Cash1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + n_Cheque1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + n_SwipeCard1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + n_NEFT_RTGS1 + '</b></th>';
                    tbody += '<th class="text-right" style="background:#f8bfbf;font-size:14px;"><b>' + FinalTotal1 + '</b></th>';
                    tbody += '</tr>';
                    $('#tblReport tbody').append(tbody);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}

function PrintReport() {
    var hosp_id = Active.HospId;
    var from = $('#txtFrom').val();
    var to = $('#txtTo').val();
    var loginId = 'ALL';
    var url = "../Print/CollectionSummaryReport?hosp_id=" + hosp_id + "&from=" + from + "&to=" + to + "&loginId=" + loginId;
    window.open(url, '_blank');
}