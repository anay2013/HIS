var dataDoseAndInTake = [];
var dataVal = [];
var activeSeachbox;
var IsActiveSeachbox = false;
$(document).ready(function () {
    FreqAndIntake();
    $('.MedicineTemplate tbody').on('mouseover', 'tr', function () {
        $(this).find('td:first').find('remove').show()
    }).on('mouseleave', 'tr', function () {
        $(this).find('td:first').find('remove').hide()
    });

    $(document).on('click', function (e) {
        var btnMediTemp = $('#btnMediTemp');
        if ($(btnMediTemp).is(e.target)) {
            //if ($('.MedicineTemplate tbody tr').length>0)
            //$('.MedicineTemplate tbody').empty();
            return
        }            

        var imgRx = $('#imgRx');
        if ($(imgRx).is(e.target)) {
            if (confirm('Are you sure to delete all Medicine?')) {
                $('.MedicineTemplate tbody').empty();
            }
        }
        var container = $('.MedicineTemplate');
        if ($(container).has(e.target).length === 0) {
            if (IsActiveSeachbox)
                return

            $('.MedicineTemplate tbody td').each(function () {
                if ($(this).index() == 0)
                    $(this).find('.delRow').remove();

                $(this).removeAttr('style');
                var content = '';             
                if ($(this).find('select').length > 0)
                    content = $(this).find('label').text() + ' ' + $(this).find('select option:selected').text();
                else if ($(this).find('label').length > 0)
                    content = $(this).find('label').text();
                else
                    content = $(this).text();

                if ($(this).index() == 0)
                    $(this).html("<remove class='delRow'>X</remove>" + content);
                else
                    $(this).html(content);

            });
        }
    });
    $(document).find('.MedicineTemplate tbody').on('click', '.delRow', function () {
        $(this).closest('tr').remove();
        if ($('#PrescribedMedicine .MedicineTemplate tbody tr').length == 0)
            $('.OPDPrintPreview #PrescribedMedicine').hide();
    });
    $('.MedicineTemplate tbody').on('click', 'td', function (e) {             
        if ($(this).find('label.editable').length === 1)
            return
        //$('.MedicineTemplate tbody td').each(function () {
        //    var content = $(this).text(); $(this).empty().removeAttr('style');
        //    $(this).html(content);           
        //});

        //$(this).find('.delRow').remove();
        //var content = $(this).not('.delRow').text();
        //$(this).html('<remove class="delRow">X</remove><input type="text" value="' + content + '"/>');
        //$('input:text').select();
        $(this).closest('tr').find('td').each(function () {          
            var indx = $(this).index();
            $(this).find('.delRow').remove();
            var content = $(this).text();
            $(this).empty().css('padding', '2px');
            var data = "<label class='editable' contenteditable='true'>" + content + "</label>";

            if (indx == 1)
                var data = "<label class='editable' onkeyup=Dose(this) contenteditable='true'>" + content + "</label>";
            if (indx == 4)
                var data = "<label class='editable' onkeyup=InTake(this) contenteditable='true'>" + content + "</label>";          
            if (indx == 5)
                var data = "<label class='editable' onkeyup=Route(this) contenteditable='true'>" + content + "</label>";   

            if ($(this).index() == 0)
                $(this).html('<remove class="delRow">X</remove>' + data);
            else
                $(this).html(data);
        })
        $(this).find('.editable').focus();
    });
    $(document).find('.MedicineTemplate thead').on('click', '.addmedNewRow', function () {
        AddNewRow() 
    });
    $('.MedicineTemplate tbody').on('keydown', 'label', function (e) {
        if ($(this).closest('td').index() == 6) {
            if (e.keyCode == 13) {                
                e.preventDefault();
                AddNewRow();                 
            }
        }
        if ($('input[id=IsDB]').is(':checked') && $(this).closest('td').index() == 0) {
            var val = $(this).text();
            if (val.length > 2)
                SearchPresMedicine(val, 'ETHICAL', this);

            $(this).autocomplete({
                source: dataVal,
                focus: function (event, ui) {
                    $(this).text(ui.item.value.split('~')[0])                    
                    return false;
                },
                select: function (event, ui) {
                    $(this).text(ui.item.value.split('~')[0])
                    $(this).closest('td').next('td').find('label').focus();
                    return false;
                }
            });
        }

        //if (e.keyCode == 19)
        //    $(this).removeClass('dbSearch')

        //if ($(this).hasClass('dbSearch')) {
        //    var val = $(this).text();
        //    if (val.length > 1)
        //        SearchPresMedicine(val, 'ETHICAL', this);

        //    $(this).autocomplete({
        //        source: dataVal,
        //        focus: function (event, ui) {
        //            $(this).text(ui.item.value.split('~')[0])
        //            return false;
        //        },
        //        select: function (event, ui) {
        //            $(this).text(ui.item.value.split('~')[0])
        //            return false;
        //        }
        //    });
        //}
    });
    $('#TemplateMasterRight').on('keydown', '#txtRemark', function (e) {
        var keyCode = e.keyCode;
        switch (keyCode) {
            case (keyCode = 13):
                AddMedicineTemplatge();
                break;
            default:
                break;
        }
    });
    $('#txtSearchProduct').on('keydown', function (e) {

        var tbody = $('#tblnavigate').find('tbody');
        var selected = tbody.find('.selected');
        var KeyCode = e.keyCode;
        switch (KeyCode) {
            case (KeyCode = 40):
                tbody.find('.selected').removeClass('selected');
                if (selected.next().length == 0) {
                    tbody.find('tr:first').addClass('selected');
                }
                else {
                    tbody.find('.selected').removeClass('selected');
                    selected.next().addClass('selected');
                }
                break;
            case (KeyCode = 38):
                tbody.find('.selected').removeClass('selected');
                if (selected.prev().length == 0) {
                    tbody.find('tr:last').addClass('selected');
                }
                else {
                    selected.prev().addClass('selected');
                }
                break;
            case (KeyCode = 13):
                var itemName = $('#tblnavigate').find('tbody').find('.selected').text().split('~')[0];
                var itemid = $('#tblnavigate').find('tbody').find('.selected').data('itemid');
                var IsCash = $('#tblnavigate').find('tbody').find('.selected').data('iscash');
                var msg = $('#tblnavigate').find('tbody').find('.selected').data('alertmsg');
                if (msg != '-') {
                    $('.msg').html('<p>' + msg + '</p><span>X</span>').show();
                }
                $('#txtFreqMaster').focus();
                $('#txtSearchProduct').val(itemName)
                $('#txtItemID').val(itemid);
                $('#txtIsCash').val(IsCash);
                $('#ItemList').hide();
                break;
            default:
                var val = $('#txtSearchProduct').val();
                if (val == '') {
                    $('#ItemList').hide();
                }
                else {
                    $('#ItemList').show();
                    var key = $(this).val();
                    var type = $('#ddlSearchType option:selected').val();
                    if (ValidateSearch()) {
                        SearchMedicine(key, type);
                    };
                }
                break;
        }
    });
    $('#tblnavigate tbody').on('click', 'tr', function () {
        var itemName = $(this).text().split('~')[0];
        var itemid = $(this).data('itemid');
        var msg = $(this).data('alertmsg');
        var IsCash = $(this).data('iscash');
        if (msg != '-') {
            $('.msg').html('<p>' + msg + '</p><span>X</span>').show();
        }
        $('#txtSearchProduct').val(itemName).blur();
        $('#txtItemID').val(itemid);
        $('#txtIsCash').val(IsCash);
        $('#ItemList').hide();
    });
    $('.msg').on('click', 'span', function () {
        $('.msg').html('').hide();
    });

    $('#btnMediTemp').on('click', function () {
        if ($('.MedicineTemplate tbody tr').length >0) {
            $('.MedicineTemplate tbody').empty();
        }
            $('.OPDPrintPreview #PrescribedMedicine').toggle();

        AddNewRow();
        //$('#btnSaveMediTempInfo').hide();
        //$('#btnPresMediItem').show();
        //$('#TemplateMasterLeft').hide();
        //$('#MedicineTemplate').show();
        //$('#MedicineTemplateForDB').hide();
        //$('#TemplateMasterRight').switchClass('col-md-8', 'col-md-12');
        //$('#modalPresMediTemplate').modal('show');
    })
    $('#btnCPOEMediTemp').on('click', function () {
        $('#btnSaveMediTempInfo').show();
        $('#btnPresMediItem').hide();
        $('#TemplateMasterLeft').show();
        $('#MedicineTemplate').hide();
        $('#MedicineTemplateForDB').show();
        $('#TemplateMasterRight').switchClass('col-md-12', 'col-md-8');
        $('#modalPresMediTemplate').modal('show');
    });
    $('#accordion').find('.panel:eq(2)').find('a[href=#ChiefComplaint]').trigger('click');
});
function AddNewRow() {  
        var tbody = "";
        tbody += "<tr data-itemid='newId'>";
        tbody += "<td style='padding:2px;'><remove class='delRow'>X</remove><label class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='padding:2px;'><label id='med1' onkeyup=Dose(this) class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='padding:2px;'><label id='med2' class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='display: flex;'><label id='med3' class='editable' contenteditable='true'></label>";
        tbody += "<select class='editable'>";
        tbody += "<option>Day</option>";
        tbody += "<option>Week</option>";
        tbody += "<option>Month</option>";
        tbody += "</select>";
        tbody += "</td>";
        tbody += "<td style='padding:2px;'><label id='med4' onkeyup=InTake(this) class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='padding:2px;'><label id='med5' onkeyup=Route(this) class='editable' contenteditable='true'></label></td>";
        tbody += "<td style='padding:2px;'><label id='med6' class='editable' contenteditable='true'></label></td>";
        tbody += "</tr>";
    $('.MedicineTemplate tbody').append(tbody);
    $('#PrescribedMedicine .MedicineTemplate tbody').find('tr:last td:first label:first').focus();   
}
function expandMedicine() {
    $('.prescribedItem:eq(6)').toggleClass('expandMedicine');
    $('.MedicineTemplate').toggleClass('expandMedicine-divContainer');
}
function Dose(elem) {
    var data = [];
    $.each(dataDoseAndInTake[0], function (key, val) {
        data.push(val.Descriptions)
    })
    $(elem).autocomplete({
        source: data
    });
}
function InTake(elem) {
    var data = [];
    $.each(dataDoseAndInTake[1], function (key, val) {
        data.push(val.instruction)
    })
    $(elem).autocomplete({
        source: data
    });
}
function Route(elem) {
    var data = [
        "Oral",
        "Nose",
        "Topical"
    ];
    $(elem).autocomplete({
        source: data
    });
}
function NextFocus(elem) {
    $(document).on('keydown', 'input, select', function (e) {
        var keyCode = e.keyCode;
        switch (keyCode) {
            case (keyCode = 13):
                $(this).blur();
                $('#' + elem).focus().select();
                break;
            default:
                break;
        }
    });
}
function SearchPresMedicine(key, type, elem) {
    disableLoading();
    dataVal = [];
    var url = config.baseUrl + "/api/IPDNursing/SearchMedicine";
    var objBO = {};
    objBO.searchKey = key;
    objBO.searchType = type;
    objBO.PanelId = $('span[data-panelid]').text();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        dataVal.push(val.item_name)
                    });
                }
            }
        },
        complete: function (response) {

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function SearchMedicine(key, type) {
    debugger
    disableLoading();
    var url = config.baseUrl + "/api/IPDNursing/SearchMedicine";
    var objBO = {};
    objBO.searchKey = key;
    objBO.searchType = type;
    objBO.PanelId = $('span[data-panelid]').text();
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data)
            $('#tblnavigate tbody').empty();
            if (data != '') {
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#tblnavigate').show();
                    $('<tr class="searchitems" data-AlertMsg="' + val.AlertMsg + '" data-itemid=' + val.item_id + ' data-iscash=' + val.IsCash + '><td>' + val.item_name + "</td></tr>").appendTo($('#tblnavigate tbody'));
                });
            }
            else {
                alert("Error");
            };
        },
        error: function (response) {
            loading();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function FreqAndIntake() {
    $('#txtFreqMaster').val('');
    $('#txtIntake').val('');
    $('#ddlFreMaster').empty().append($('<option></option>').val(00).html(''));
    $('#ddlIntake').empty().append($('<option></option>').val(00).html(''));
    var url = config.baseUrl + "/api/Prescription/CPOE_PrescriptionAdviceQueries";
    var objBO = {};
    objBO.Logic = 'FreqAndIntake';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            dataDoseAndInTake.push(data.ResultSet.Table);
            dataDoseAndInTake.push(data.ResultSet.Table1);
            $.each(data.ResultSet.Table, function (key, val) {
                $('#ddlFreMaster').append($('<option></option>').val(val.qty).html(val.Descriptions)).select2();
            });

            $.each(data.ResultSet.Table1, function (key, val) {
                $('#ddlIntake').append($('<option></option>').val(val.instruction).html(val.instruction)).select2();
            });
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AddMedicineTemplatge() {
    var itemid = $('#txtItemID').val();
    var itemName = $('#txtSearchProduct').val();
    var dose = $('#txtFreqMaster').val();
    var duration = $('#txtDuration').val();
    var days = $('#txtDays option:selected').text();
    var qty = $('#txtQty').val();
    var whenTake = $('#txtIntake').val();
    var route = $('#txtRoute').val();
    var remark = $('#txtRemark').val();
    var len = parseInt($('.MedicineTemplate tbody tr').length);
    var tbody = "";
    tbody += "<tr data-itemid=" + itemid + ">";
    tbody += "<td><remove class='delRow'>X</remove>" + itemName + "</td>";
    tbody += "<td>" + dose + "</td>";
    tbody += "<td>" + duration + "</td>";
    tbody += "<td>" + duration + " Days</td>";
    tbody += "<td>" + whenTake + "</td>";
    tbody += "<td>" + route + "</td>";
    tbody += "<td>" + remark + "</td>";
    tbody += "</tr>";
    if (ValidateProduct()) {
        var ids = $('.MedicineTemplate tbody').find('tr').filter(function () {
            if ($(this).data('itemid') == itemid) return true;
        });
        if (ids.length <= 0) {
            $('.MedicineTemplate tbody').append(tbody);
            $('.OPDPrintPreview #PrescribedMedicine').show();
            $('.modal-body').find('input:text').val('');
            $('.modal-body').find('select').prop('selectedIndex', '0').change();
        }
        else {
            alert('Product Already Selected...! Try Aother One.')
            $('.modal-body').find('input:text').val('');
            $('.modal-body').find('select').prop('selectedIndex', '0').change();
        }
    }
}
function QtyCalculate() {
    var dose = parseInt($('#ddlFreMaster option:selected').val());
    var duration = parseInt($('#txtDuration').val());
    var qty = (dose) * (duration);
    $('#txtQty').val(qty);

}
function ValidateSearch() {
    var searchType = $('#ddlSearchType option:selected').text();

    if (searchType == 'Choose Type') {
        $('#ddlSearchType').css({ 'border-color': 'red' });
        alert('Please Select Search Type..');
        $('#txtSearchProduct').val('');
        return false;
    }
    else {
        $('#ddlSearchType').removeAttr('style').siblings('span').empty();
    }
    return true;
}
function ValidateProduct() {
    var itemid = $('#txtItemID').val();
    var itemName = $('#txtSearchProduct').val();
    var dose = $('#ddlFreMaster option:selected').text();
    var duration = $('#txtDuration').val();
    var days = $('#txtDays option:selected').text();
    var qty = $('#txtQty').val();
    var whenTake = $('#ddlIntake option:selected').text();
    var route = $('#ddlRoute option:selected').text();
    var remark = $('#txtRemark').val();

    if (itemid == '') {
        alert('Please Choose Any Product..!');
        return false;
    }
    if (itemName == '') {
        $('#txtSearchProduct').css('border-color', 'red').focus();
        alert('Please Choose Any Product..!');
        return false;
    }
    else {
        $('#txtSearchProduct').removeAttr('style');
    }
    if (dose == 'Select') {
        alert('Please Choose Dose..!');
        return false;
    }
    if (duration == '') {
        $('#txtDuration').css('border-color', 'red').focus();
        alert('Please Provide Duration..!');
        return false;
    }
    else {
        $('#txtDuration').removeAttr('style');
    }
    if (qty == '') {
        $('#txtQty').css('border-color', 'red').focus();
        alert('Please Provide Quantity..!');
        return false;
    }
    else {
        $('#txtQty').removeAttr('style');
    }
    if (whenTake == 'Select') {
        alert('Please Choose when Take..!');
        return false;
    }
    if (route == 'Select Route') {
        alert('Please Choose Route..!');
        return false;
    }
    return true;
}