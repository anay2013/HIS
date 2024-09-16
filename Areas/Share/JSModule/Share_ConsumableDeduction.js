var autoid = "";
var groupname = "";
var deductionAmt = 0;
var TotalAmt = 0
var extradeductionAmt = 0;
var doctorid = "";
$(document).ready(function () {
    disableLoading();
    GetPreviousMonth();
    $('#txtdoctorName').on('keyup', function () {
        var val = $(this).val().toLowerCase();
        $('#tblExtraDeduction tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });
    $('#txtdoctor').on('keyup', function () {
        var val = $(this).val().toLowerCase();
        $('#tblDoctor tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(val) > -1);
        });
    });
    $("#tblReoprt tbody").on('click', '#BtnAdd', function () {
        autoid = $(this).closest('tr').find('td:eq(0)').text();
        groupname = $(this).closest('tr').find('td:eq(1)').text();
        TotalAmt = parseFloat($(this).closest('tr').find('td:eq(5)').text());
        deductionAmt = parseFloat($(this).closest('tr').find('td:eq(6) input#txtdeduction').val());
        SaveDeduction($(this));
        calc_total();
    })

    $("#UploadShare").change(function () {
        readURL(this);
    });

    $("#tblExtraDeduction tbody").on('click', '.BtnExtra', function () {
        debugger
        doctorid = $(this).closest('tr').find('td:eq(0)').text();
        extradeductionAmt = parseFloat($(this).closest('tr').find('td:eq(4) input#txtExtradeduction').val());
        SaveExtraDeduction($(this));
    })
});
function readURL(input) {
    if (input.files && input.files[0]) {
        var ext = $('#UploadShare').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'pdf', 'xlsx']) == -1) {
            alert('invalid fileextension!');
            return false;
        }
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#base64').val('');
            $('#base64').val(e.target.result);
        }
        reader.readAsDataURL(input.files[0]); // convert to base64 string
        var formData = new FormData();
        var files = $('#UploadShare').get(0).files;
    }
}
function GetPreviousMonth() {
    var date = new Date();
    var month = date.getMonth();
    var year = date.getFullYear();
    month -= 0;
    if (month < 0) {
        month = 11;
        year -= 1;
    }
    if (month < 10) month = "0" + month;
    var previousMonth = year + "-" + month;
    $("#txtMonth").val(previousMonth);
}
function GetAllDataList() {
    $("#loadingdata3").show();
    $("#tblReoprt tbody").empty();
    var url = config.baseUrl + "/api/Share/Share_ProcessQeries";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = "-";
    objBO.SubCatId = "-";
    objBO.Prm1 = '-';
    objBO.Prm2 = "-";
    objBO.Logic = "DeductionInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            var tbody = ""; var temp = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (temp != val.DeductionSource) {
                            tbody += "<tr style='background:#ccdde7;'>";
                            tbody += "<td colspan='15' style='font-size:13px;'><b>" + val.DeductionSource + "</b></td>";
                            tbody += "</tr>";
                            temp = val.DeductionSource
                        }
                        else {
                            tbody += '<tr>';
                        }

                        tbody += '<td hidden>' + val.autoId + '</td>';
                        tbody += '<td hidden>' + val.DeductionSource + '</td>';
                        tbody += '<td style="width:15%">' + val.CatName + '</td>';
                        tbody += '<td style="width:20%">' + val.SubCatName + '</td>';
                        tbody += '<td style="width:20%" >' + val.Remarks + '</td>';
                        tbody += '<td style="width:7%;text-align:center;">' + val.TotalAmount + '</td>';
                        tbody += '<td style="width:7%;text-align:center;"><input  id="txtdeduction" style="width:75%;text-align:center;" value=' + val.deductionAmount + '></td>';
                        tbody += '<td style="width:7%;text-align:center;"><buton id="BtnAdd" class="btn btn-success bn" style="height:25px; width:75px;">Deduct</button></div></td>';
                        tbody += '<td style="width:10%;text-align:center;" id="msgamount">' + val.Deduction + '</td>';

                        tbody += '</tr>';
                    });
                    $("#tblReoprt tbody").append(tbody);
                    calc_total();
                    $("#loadingdata3").hide();
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SaveDeduction(elem) {
    var url = config.baseUrl + "/api/Share/Share_MakeShareableInsert";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.Prm1 = '-'
    objBO.Total = TotalAmt;
    objBO.DeductionAmount = deductionAmt;
    objBO.AutoId = $(elem).closest('tr').find('td:first').text();
    objBO.Logic = 'Update:' + groupname;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                $(elem).closest('tr').find('td:eq(8)').text(data);
            }
            else {
                $(elem).closest('tr').find('td:eq(8)').text(data).css('color', 'red');
                calc_total();

            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function calc_total() {
    let totalamount = 0;
    $("#tblReoprt tbody tr").each(function () {
        let amount = parseFloat($(this).find('td:eq(8)').text());
        if (!isNaN(amount)) {
            totalamount += amount;
        }
    });
    $("#txttotalAmount").text(totalamount.toFixed(0));
}
function DownloadDoctorShare(elem) {
    var url = config.baseUrl + "/api/Share/Share_ProcessQeries";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = "-";
    objBO.SubCatId = "-";
    objBO.Prm1 = '-';
    objBO.Prm2 = "-";
    objBO.Logic = "ShareCorrectionInfo";
    objBO.OutPutType = 'Excel';
    Global_DownloadExcel1(url, objBO, "DeductionReport" + ".xlsx", elem);
}
function Global_DownloadExcel1(Url, objBO, fileName, elem) {
    $(elem).addClass('button-loading');
    var ajax = new XMLHttpRequest();
    ajax.open("Post", Url, true);
    ajax.responseType = "blob";
    ajax.setRequestHeader("Content-type", "application/json")
    ajax.onreadystatechange = function () {
        if (this.readyState == 4) {
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            saveAs(blob, fileName); //refernce by ~/JsModule/FileSaver.min.js
            $(elem).removeClass('button-loading');
        }
    };
    ajax.send(JSON.stringify(objBO));
}

function UploadShareCorrection(elem) {
        $(elem).addClass('button-loading')
        var uploadFile = $("#UploadShare");
        var files = uploadFile.get(0).files;
        var formData = new FormData();
        if (files.length > 0) {
            formData.append("ExcelFile",files[0]);
            formData.append("LoginId", Active.userId);
            formData.append("HospId", Active.HospId);
            formData.append("Logic", "BulkUpdate");
            $.ajax({
                url: config.baseUrl + "/api/Share/UploadShareCorrection",
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert(response);
                    $(elem).removeClass('button-loading');
                },
                error: function (response) {
                    alert('Server Error...!');
                }
            });
        }
   }

function SaveImport(logic) {
    if ("AutoDeduct" == logic) {
        $("#loadingdata2").show();
    }
    else if ("ImportData" == logic) {
        $("#loadingdata1").show();
    }
    var isConfirmed = confirm('Are you sure you want to Process the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/Share/Share_MakeShareableInsert";
        var objBO = {};
        objBO.ShareMonth = $("#txtMonth").val();
        objBO.CatId = '-';
        objBO.SubCatId = '-';
        objBO.Prm1 = '-'
        objBO.Total = '-';
        objBO.DeductionAmount = '-';
        objBO.AutoId = '-';
        objBO.Logic = logic;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#loadingdata1").hide();
                    $("#loadingdata2").hide();
                }
                else {
                    alert(data);
                    $("#loadingdata1").hide();
                    $("#loadingdata2").hide();

                }

            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
    else {
        alert('Data Save canceled.');
        $("#loadingdata1").hide();
        $("#loadingdata2").hide();
    }


}
function ExcelDownload() {
    $("#loadata6").show();
    var isConfirmed = confirm('Are you sure you want to Process the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/Share/Share_ProcessQeries";
        var objBO = {};
        objBO.ShareMonth = $("#txtMonth").val();
        objBO.CatId = "-";
        objBO.SubCatId = "-";
        objBO.Prm1 = '-';
        objBO.Prm2 = "-";
        objBO.Logic = "DownloadFinalShare";
        objBO.OutPutType = 'Excel';
        Global_DownloadExcel(url, objBO, "DeductionReport" + ".xlsx");
        $("#loadata6").hide();
    }
    else {
        alert('Data Save canceled.');
        $("#loadata6").hide();
    }

}
function GetAllSummmary() {
    $('#GetSummary').modal('show');
    $("#tblDeductionDetails tbody").empty();
    var url = config.baseUrl + "/api/Share/Share_ProcessQeries";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = "-";
    objBO.SubCatId = "-";
    objBO.Prm1 = '-';
    objBO.Prm2 = "-";
    objBO.Logic = "GetAllDeduction";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td style="width:5%;text-align:center">' + val.TCount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.amount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.ItemAmount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.nst_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.amt_forshare.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.diag_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.med_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.blood_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.ext_cons_discount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.commission_amt.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.shareable_amt.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.ExTraDeduction.toFixed(0) + '</td>';
                        tbody += '<td style="width:5%;text-align:center">' + val.share_amt.toFixed(0) + '</td>';
                        tbody += '</tr>';
                    });
                    $("#tblDeductionDetails tbody").append(tbody);

                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SaveCalAndSummarize(logic) {
    if ("Calculate" == logic) {
        $("#loadingdata4").show();
    }
    else if ("Summarise" == logic) {
        $("#loadingdata5").show();
    }
    var isConfirmed = confirm('Are you sure you want to Process the data?');
    if (isConfirmed) {
        var url = config.baseUrl + "/api/Share/Share_CalculateShare";
        var objBO = {};
        objBO.ShareMonth = $("#txtMonth").val();
        objBO.CatId = '-';
        objBO.SubCatId = '-';
        objBO.Prm1 = '-'
        objBO.Total = '-';
        objBO.DeductionAmount = '-';
        objBO.AutoId = '-';
        objBO.Logic = logic;
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data);
                    $("#loadingdata4").hide();
                    $("#loadingdata5").hide();
                }
                else {
                    alert(data);
                    $("#loadingdata4").hide();
                    $("#loadingdata5").hide();

                }

            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
    else {
        alert('Data Save canceled.');
        $("#loadingdata4").hide();
        $("#loadingdata5").hide();
    }
}
function GetAllExtraDeduction() {
    $('#GetExtra').modal('show');
    $("#tblExtraDeduction tbody").empty();
    var url = config.baseUrl + "/api/Share/Share_ProcessQeries";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = "-";
    objBO.SubCatId = "-";
    objBO.Prm1 = '-';
    objBO.Prm2 = "-";
    objBO.Logic = "ExtraDeduction";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td style="width:20%;">' + val.DoctorId + '</td>';
                        tbody += '<td style="width:20%;">' + val.DoctorName + '</td>';
                        tbody += '<td style="width:13%;text-align:center">' + val.min_gurantee.toFixed(0) + '</td>';
                        tbody += '<td style="width:13%;text-align:center">' + val.final_Share.toFixed(0) + '</td>';
                        tbody += '<td style="width:9%;text-align:center;"><input  id="txtExtradeduction" style="width:90%;text-align:center;"></td>';
                        tbody += '<td style="width:9%;text-align:center;"><buton  class="btn btn-success bn BtnExtra" style="height:25px; width:55px;">Deduct</button></div></td>';
                        tbody += '<td style="width:16%;text-align:center"></td>';
                        tbody += '</tr>';
                    });
                    $("#tblExtraDeduction tbody").append(tbody);
                    Extracalc_total();
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SaveExtraDeduction(elem) {
    if (extradeductionAmt > 100) {
        alert('Please Enter a Value less than Deduction');
        $('#txtExtradeduction').focus();
        return
    }
    var url = config.baseUrl + "/api/Share/Share_MakeShareableInsert";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = '-';
    objBO.SubCatId = '-';
    objBO.Prm1 = $(elem).closest('tr').find('td:first').text();
    objBO.Total = '0';
    objBO.DeductionAmount = extradeductionAmt;
    objBO.AutoId = '0';
    objBO.Logic = 'ExtraDeduction';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.includes('Success')) {
                $(elem).closest('tr').find('td:eq(6)').text(data);
                Extracalc_total();
            }
            else {
                $(elem).closest('tr').find('td:eq(6)').text(data).css('color', 'red');
                Extracalc_total();
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Extracalc_total() {
    let totalamount1 = 0;
    $("#tblExtraDeduction tbody tr").each(function () {
        let amount = parseFloat($(this).find('td:eq(6)').text());
        if (!isNaN(amount)) {
            totalamount1 += amount;
        }
    });
    $("#ExtraTotalAmount").text(totalamount1.toFixed(0));
}
function GetAllDoctorWise() {
    $('#GetDoctor').modal('show');
    $("#tblDoctor tbody").empty();
    var url = config.baseUrl + "/api/Share/Share_ProcessQeries";
    var objBO = {};
    objBO.ShareMonth = $("#txtMonth").val();
    objBO.CatId = "-";
    objBO.SubCatId = "-";
    objBO.Prm1 = '-';
    objBO.Prm2 = "-";
    objBO.Logic = "DoctorWiseRawData";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        tbody += '<tr>';
                        tbody += '<td style="width:5%;">' + val.DoctorId + '</td>';
                        tbody += '<td style="width:5%;">' + val.DoctorName + '</td>';
                        tbody += '<td style="width:5%;text-align:center">' + val.TCount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.amount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.ItemAmount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.nst_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.amt_forshare.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.diag_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.med_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.blood_disc.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.ext_cons_discount.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.commission_amt.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.shareable_amt.toFixed(0) + '</td>';
                        tbody += '<td style="width:8%;text-align:center">' + val.ExTraDeduction.toFixed(0) + '</td>';
                        tbody += '<td style="width:5%;text-align:center">' + val.share_amt.toFixed(0) + '</td>';
                        tbody += '</tr>';
                    });
                    $("#tblDoctor tbody").append(tbody);

                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}