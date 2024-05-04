var _entryDate = '';
$(document).ready(function () {
    $('input:text').addClass('form-control')
    ApacheSofaScoreByIPDNo();
})
function PrintApacheSofaScore() {
    var formattedDate = _entryDate.split('-')[2] + '-' + _entryDate.split('-')[1] + '-' + _entryDate.split('-')[0];
    var url = "../Print/PrintApacheSofaScore?IPDNO=" + _IPDNo + "&EntryDate=" + formattedDate;
    window.open(url, '_blank');
}
function ApacheSofaScoreByIPDNo() {
    var url = config.baseUrl + "/api/IPDNursing/SOFA_ScoreQueries";
    var objBO = {};
    objBO.ObservationId = 0;
    objBO.Sofasystem = '';
    objBO.ObservationName = '';
    objBO.Value = _IPDNo;
    objBO.from = '1900/01/01';
    objBO.to = '1900/01/01';
    objBO.Prm1 = '';
    objBO.Prm2 = '';
    objBO.Logic = 'ApacheSofaScoreByIPDNo';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        _entryDate = val.entryDate;
                        //console.log("Entry Date:", entryDate);
                        $('#age').val(val.AGE);
                        $('#gla').val(val.GlasgowComaScore);
                        $('#tem').val(val.TEMP);
                        $('#pam').val(val.MAP);
                        $('#fc').val(val.HR);
                        $('#fr').val(val.RR);
                        $('input[id=ven]').prop('checked', val.isMV);
                        $('#oxy').val(val.FiO2);
                        $('#pha').val(val.ArtPH);
                        $('#nas').val(val.NA);
                        $('#kas').val(val.Potassium);
                        $('#uri').val(val.UrOut);
                        $('#cre').val(val.Cr);
                        $('#ure').val(val.Urea);
                        $('#bsl').val(val.BSL);
                        $('#alb').val(val.Alb);
                        $('#bil').val(val.Bil);
                        $('#hto').val(val.HT);
                        $('#wbc').val(val.WBC);
                        $('#sco').val(val.aps_Score);
                        $('#mor').val(val.EstMortRate);
                        $('input[id=arf2]').eq((val.IsRenalFailure == 'Y') ? 0 : 1).prop('checked', true).change();
                        $('input[id=mal2]').eq((val.IsChronic == 'Y') ? 0 : 1).prop('checked', true).change();
                        console.log(data);
                        $('#sit option').each(function () {
                            if ($(this).text() == val.ChronicDisease)
                                $('#sit').prop('selectedIndex', '' + $(this).index() + '').change()
                        });

                    });

                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function InsertApacheSofaScore() {
    // validation to check if required fields are not empty
    if ($('#age').val() === '' ||
        $('#gla').val() === '' ||
        $('#tem').val() === '' ||
        $('#pam').val() === '' ||
        $('#fc').val() === '' ||
        $('#fr').val() === '' ||
        $('#pco').val() === '' ||
        $('#pha').val() === '' ||
        $('#nas').val() === '' ||
        $('#kas').val() === '' ||
        $('#cre').val() === '' ||
        $('#hto').val() === '' ||
        $('#wbc').val() === '' ||
        $('#sco').val() === '' ||
        $('#mor').val() === '') {
        alert('Please fill in all required fields.');
        return;
    }

    if (confirm('Are you sure to Submit?')) {
        var url = config.baseUrl + "/api/IPDNursing/SOFA_InsertApacheSofaScore";
        var objBO = {};
        objBO.ipdNo = _IPDNo;
        objBO.entryDate = '1900-01-01';
        objBO.AGE = $('#age').val();
        objBO.GlasgowComaScore = $('#gla').val();
        objBO.TEMP = $('#tem').val();
        objBO.MAP = $('#pam').val();
        objBO.HR = $('#fc').val();
        objBO.RR = $('#fr').val();
        objBO.isMV = '-';
        objBO.FiO2 = $('#oxy').val();
        objBO.Po2 = '-';
        objBO.PCO2 = $('#pco').val();
        objBO.ArtPH = $('#pha').val();
        objBO.NA = $('#nas').val();
        objBO.Potassium = $('#kas').val();
        objBO.UrOut = '-';
        objBO.Cr = $('#cre').val();
        objBO.Urea = '-';
        objBO.BSL = '-';
        objBO.Alb = '-';
        objBO.Bil = '-';
        objBO.HT = $('#hto').val();
        objBO.WBC = $('#wbc').val();
        objBO.isGCS = '-';
        objBO.ChronicDisease = $('#sit option:selected').text();
        objBO.Verbal = '-';
        objBO.Motor = '-';
        objBO.chc_crf = '-';
        objBO.chc_lym = '-';
        objBO.chc_cir = '-';
        objBO.chc_leu = '-';
        objBO.chc_hep = '-';
        objBO.chc_imm = '-';
        objBO.chc_met = '-';
        objBO.chc_aids = '-';
        objBO.pre_icu_loss = '-';
        objBO.origin = '-';
        objBO.IsRenalFailure = $('input[name=arf]:checked').val();
        objBO.IsChronic = ($('input[name=mal]:checked').val() == 'o') ? 'Y' : 'N';
        objBO.isEmergency = '-';
        objBO.isOperative = '-';
        objBO.sys = '-';
        objBO.diag = '-';
        objBO.isThrombolysis = '-';
        objBO.IV_Score = '-';
        objBO.Eyes = '-';
        objBO.aps_Score = $('#sco').val();
        objBO.EstMortRate = $('#mor').val();
        objBO.EstLenOfStay = '-';
        objBO.login_id = Active.userId;
        objBO.Logic = 'Insert';
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data.includes('Success')) {
                    alert(data)
                } else {
                    alert(data)
                }
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}


var disp = 0;
function helpapp() {
    if (disp == 0) {
        document.getElementById('tl').style.display = "block";
        disp = 1;
        visi = "hidden";
    }

    else {
        document.getElementById('tl').style.display = "none";
        disp = 0;
        visi = "visible";
    }
    document.apa.fio.style.visibility = visi;
    document.apa.aci.style.visibility = visi;
    document.apa.sit.style.visibility = visi;
    document.apa.sys.style.visibility = visi;
    document.apa.defsys.style.visibility = visi;
    document.apa.typ.style.visibility = visi;
    document.apa.deftyp.style.visibility = visi;
}
var sho = 0;
function gradapp() {
    if (sho == 0) {
        document.getElementById('aagrad').style.display = "block";
        sho = 1;
        vis = "hidden";
    }

    else {
        document.getElementById('aagrad').style.display = "none";
        sho = 0;
        vis = "visible";
    }

    document.apa.fio.style.visibility = vis;
    document.apa.aci.style.visibility = vis;
}
function gradcalc() {
    var pao, paco, quo, patm, fi;
    var alv, grad;

    pao = 1 * document.apa.pao.value;
    paco = 1 * document.apa.paco.value;
    quo = 1 * document.apa.quo.value;
    patm = 1 * document.apa.patm.value;
    fi = 1 * document.apa.fi.value;

    alv = (patm - 47) * (fi / 100) - (paco / quo);
    grad = (fi / 100) * (patm - 47) - (paco / quo) - pao;

    document.apa.oxy.value = grad.toFixed(1);
    gradapp();
}
function defoxy() {
    fio = document.apa.fio.value;

    if (fio == "a") {
        document.apa.oxy.value = 150;
        document.getElementById("oxyvar").innerHTML = "AaDO2 :";
        document.getElementById('gradlink').style.visibility = "visible";
    }
    if (fio == "b") {
        document.apa.oxy.value = 100;
        document.getElementById("oxyvar").innerHTML = " PaO2 :";
        document.getElementById("gradlink").style.visibility = "hidden";
    }
}
function defacid() {
    aci = document.apa.aci.value;

    if (aci == "1") { document.apa.pha.value = 7.4; }
    if (aci == "2") { document.apa.pha.value = 23; }
}
function selmal() {
    lon = document.apa.mal.length;

    for (j = 0; j < lon; j++) {
        if (document.apa.mal[j].checked) { cx = document.apa.mal[j].value; }
    }

    if (cx == "n") {
        document.apa.sit.selectedIndex = 0;
        document.apa.sit.disabled = true;
    }
    else { document.apa.sit.disabled = false; }
}
function apachcalc() {
    var tem, pam, fc, fr, fio, oxy, aci, pha, nas, kas, cre, hto, wbc, gla, age, mal, sit;
    var ch, cx, sco, ap;

    tem = 1 * document.apa.tem.value;
    pam = 1 * document.apa.pam.value;
    fc = 1 * document.apa.fc.value;
    fr = 1 * document.apa.fr.value;
    fio = document.apa.fio.value;
    oxy = 1 * document.apa.oxy.value;
    aci = document.apa.aci.value;
    pha = 1 * document.apa.pha.value;
    nas = 1 * document.apa.nas.value;
    kas = 1 * document.apa.kas.value;
    cre = 1 * document.apa.cre.value;
    hto = 1 * document.apa.hto.value;
    wbc = 1 * document.apa.wbc.value;
    gla = 1 * document.apa.gla.value;
    age = 1 * document.apa.age.value;
    sit = 1 * document.apa.sit.value;

    sco = 0;

    ch = 1;
    len = document.apa.arf.length;

    for (i = 0; i < len; i++) {
        if (document.apa.arf[i].checked) {
            ch = 1 * document.apa.arf[i].value;
        }
    }

    cx = "";
    lon = document.apa.mal.length;

    for (j = 0; j < lon; j++) {
        if (document.apa.mal[j].checked) { cx = document.apa.mal[j].value; }
    }

    if (cx == "") { alert("Select Maladie Chronique"); }
    if (cx == "o" && document.apa.sit.selectedIndex == 0) { alert("Input Maladie Chronique"); }

    if (tem == "") { alert("Input Temperature"); }
    //else {
    //    if (tem >= 41) { sco += 4; }
    //    if (tem >= 39 && tem < 41) { sco += 3; }
    //    if (tem >= 38.5 && tem < 39) { sco += 1; }
    //    if (tem >= 36 && tem < 38.5) { sco += 0; }
    //    if (tem >= 34 && tem < 36) { sco += 1; }
    //    if (tem >= 32 && tem < 34) { sco += 2; }
    //    if (tem >= 30 && tem < 32) { sco += 3; }
    //    if (tem < 30) { sco += 4; }
    //}
    else {
        if (tem < 91.4) { sco += 20; }
        else if (tem < 92.3) { sco += 16; }
        else if (tem < 93.2) { sco += 13; }
        else if (tem < 95) { sco += 8; }
        else if (tem < 96.8) { sco += 2; }
        else if (tem < 104) { sco += 0; }
        else { sco += 4; }
    }

    if (pam == "") { alert("Input PAM"); }
    else {
        if (pam >= 160) { sco += 4; }
        if (pam >= 130 && pam < 160) { sco += 3; }
        if (pam >= 110 && pam < 130) { sco += 2; }
        if (pam >= 70 && pam < 110) { sco += 0; }
        if (pam >= 50 && pam < 70) { sco += 2; }
        if (pam < 50) { sco += 4; }
    }

    if (fc == "") { alert("Input FC"); }
    else {
        if (fc >= 180) { sco += 4; }
        if (fc >= 140 && fc < 180) { sco += 3; }
        if (fc >= 110 && fc < 140) { sco += 2; }
        if (fc >= 70 && fc < 110) { sco += 0; }
        if (fc >= 55 && fc < 70) { sco += 2; }
        if (fc >= 40 && fc < 55) { sco += 3; }
        if (fc < 40) { sco += 4; }
    }

    if (fr == "") { alert("Input FR"); }
    else {
        if (fr >= 50) { sco += 4; }
        if (fr >= 35 && fr < 50) { sco += 3; }
        if (fr >= 25 && fr < 35) { sco += 1; }
        if (fr >= 12 && fr < 25) { sco += 0; }
        if (fr >= 10 && fr < 12) { sco += 1; }
        if (fr >= 6 && fr < 10) { sco += 2; }
        if (fr < 6) { sco += 4; }
    }

    if (oxy == "") { alert("Input Oxygenation"); }
    else {
        if (fio == "a") {
            if (oxy >= 500) { sco += 4; }
            if (oxy >= 350 && oxy < 500) { sco += 3; }
            if (oxy >= 200 && oxy < 350) { sco += 2; }
            if (oxy < 200) { sco += 0; }
        }
        else {
            if (oxy > 70) { sco += 0; }
            if (oxy > 60 && oxy <= 70) { sco += 1; }
            if (oxy >= 55 && oxy <= 60) { sco += 3; }
            if (oxy < 55) { sco += 4; }
        }
    }

    if (pha == "") { alert("Input pH"); }
    else {
        if (aci == 1) {
            if (pha >= 7.7) { sco += 4; }
            if (pha >= 7.6 && pha < 7.7) { sco += 3; }
            if (pha >= 7.5 && pha < 7.6) { sco += 1; }
            if (pha >= 7.33 && pha < 7.5) { sco += 0; }
            if (pha >= 7.25 && pha < 7.33) { sco += 2; }
            if (pha >= 7.15 && pha < 7.25) { sco += 3; }
            if (pha < 7.15) { sco += 4; }
        }
        else {
            if (pha >= 52) { sco += 4; }
            if (pha >= 41 && pha < 52) { sco += 3; }
            if (pha >= 32 && pha < 41) { sco += 1; }
            if (pha >= 22 && pha < 32) { sco += 0; }
            if (pha >= 18 && pha < 22) { sco += 2; }
            if (pha >= 15 && pha < 18) { sco += 3; }
            if (pha < 15) { sco += 4; }
        }
    }

    if (nas == "") { alert("Input Na+"); }
    else {
        if (nas >= 180) { sco += 4; }
        if (nas >= 160 && nas < 180) { sco += 3; }
        if (nas >= 155 && nas < 160) { sco += 2; }
        if (nas >= 150 && nas < 155) { sco += 1; }
        if (nas >= 130 && nas < 150) { sco += 0; }
        if (nas >= 120 && nas < 130) { sco += 2; }
        if (nas >= 111 && nas < 120) { sco += 3; }
        if (nas < 111) { sco += 4; }
    }

    if (kas == "") { alert("Input K+"); }
    else {
        if (kas >= 7) { sco += 4; }
        if (kas >= 6 && kas < 7) { sco += 3; }
        if (kas >= 5.5 && kas < 6) { sco += 1; }
        if (kas >= 3.5 && kas < 5.5) { sco += 0; }
        if (kas >= 3 && kas < 3.5) { sco += 1; }
        if (kas >= 2.5 && kas < 3) { sco += 2; }
        if (kas < 2.5) { sco += 4; }
    }

    if (cre == "") { alert("Input Creatinine"); }
    else {
        if (cre >= 3.5) { sco += ch * 4; }
        if (cre >= 2 && cre < 3.5) { sco += ch * 3; }
        if (cre >= 1.5 && cre < 2) { sco += ch * 2; }
        if (cre >= 0.6 && cre < 1.5) { sco += 0; }
        if (cre < 0.6) { sco += ch * 2; }
    }

    if (hto == "") { alert("Input Ht"); }
    else {
        if (hto >= 60) { sco += 4; }
        if (hto >= 50 && hto < 60) { sco += 2; }
        if (hto >= 46 && hto < 50) { sco += 1; }
        if (hto >= 30 && hto < 46) { sco += 0; }
        if (hto >= 20 && hto < 30) { sco += 2; }
        if (hto < 20) { sco += 4; }
    }

    if (wbc == "") { alert("Input WBC"); }
    else {
        if (wbc >= 40) { sco += 4; }
        if (wbc >= 20 && wbc < 40) { sco += 2; }
        if (wbc >= 15 && wbc < 20) { sco += 1; }
        if (wbc >= 3 && wbc < 15) { sco += 0; }
        if (wbc >= 1 && wbc < 3) { sco += 2; }
        if (wbc < 1) { sco += 4; }
    }

    if (gla == "") { alert("Input Glasgow"); }
    else {
        sco += (15 - gla);
    }

    if (age == "") { alert("Input Age"); }
    else {
        if (age >= 75) { sco += 6; }
        if (age >= 65 && age < 75) { sco += 5; }
        if (age >= 55 && age < 65) { sco += 3; }
        if (age >= 45 && age < 55) { sco += 2; }
        if (age < 45) { sco += 0; }
    }

    sco += sit;

    ap = -3.517 + (sco * 0.146);
    mor = 100 * (Math.exp(ap)) / (1 + Math.exp(ap));

    document.apa.sco.value = sco;
    document.apa.mor.value = mor.toFixed(1);
}
function defadjust() {
    var le, k;

    le = document.apa.adj.length;

    for (k = 0; k < le; k++) {
        if (document.apa.adj[k].checked) {
            ck = document.apa.adj[k].value;
        }
    }

    if (ck == 1) {
        m = "1";
        n = "2";
        document.apa.typ.selectedIndex = 0;
        document.apa.typ.disabled = true;
        document.apa.sys.disabled = false;

        document.apa.deftyp.disabled = true;
        document.apa.defsys.disabled = false;
    }
    else {
        m = "2";
        n = "1";
        document.apa.sys.selectedIndex = 0;
        document.apa.sys.disabled = true;
        document.apa.typ.disabled = false;

        document.apa.defsys.disabled = true;
        document.apa.deftyp.disabled = false;
    }

    document.getElementById("tab" + n).style.backgroundColor = "";
    document.getElementById("tab" + m).style.backgroundColor = "#CCCCCC";
}
function defmed() {
    opts = document.apa.defsys;
    t = opts.length;

    for (u = 0; u < t; u++) {
        opts.remove(0);
    }

    v = document.apa.sys.selectedIndex;

    lissys = new Array();

    lissys[0] = new Array("");
    lissys[1] = new Array("Asthma/Allergy", "COPD", "Oedema non-cardiogenic", "Respiratoiry arrest", "Aspiration/Poisonning/Toxic", "Pulmonary embolus", "Infection", "Neoplasm", "Other");
    lissys[2] = new Array("Hypertension", "Arythmia", "Congestive failure", "Cardiogenic shock", "Coronary disease", "Sepsis", "Cardiac arrest", "Hypovolemic shock", "Dissecting aneurysm/AAA", "Other");
    lissys[3] = new Array("Seizure", "Hematoma/Haemorrhage", "Other");
    lissys[4] = new Array("Bleeding", "Other");
    lissys[5] = new Array("Ketoacidosis", "Other");
    lissys[6] = new Array("All");
    lissys[7] = new Array("Multi trauma", "Head injry");
    lissys[8] = new Array("Drug overdose");

    valsys = new Array();

    valsys[1] = new Array("-2.108", "-0.367", "-0.251", "-0.168", "-0.142", "-0.128", "0", "0.891", "-0.89");
    valsys[2] = new Array("-1.798", "-1.368", "-0.424", "-0.259", "-0.191", "0.113", "0.393", "0.493", "0.731", "0.470");
    valsys[3] = new Array("-0.584", "0.723", "-0.759");
    valsys[4] = new Array("0.334", "0.501");
    valsys[5] = new Array("-1.507", "-0.885");
    valsys[6] = new Array("-0.885");
    valsys[7] = new Array("-1.228", "-0.517");
    valsys[8] = new Array("-3.353");

    l = lissys[v].length;

    for (p = 0; p < l; p++) {
        var opt = new Option(lissys[v][p], valsys[v][p]);
        opts.add(opt, p);
    }
}
function defchir() {
    var lo, p;
    lo = document.apa.adj.length;

    for (p = 0; p < lo; p++) {
        if (document.apa.adj[p].checked) {
            c = document.apa.adj[p].value;
        }
    }

    optt = document.apa.deftyp;
    t = optt.length;

    for (u = 0; u < t; u++) {
        optt.remove(0);
    }

    w = document.apa.typ.selectedIndex;

    listyp = new Array();

    listyp[0] = "";
    listyp[1] = new Array("Intrathoracic neoplasm surgery", "Respiratory insufficiency", "Other");
    listyp[2] = new Array("Chronic cardiovascular disease", "Peripheral vascular surgery", "Heart valve surgery", "Hemorragic shock", "Other");
    listyp[3] = new Array("Intracranial neoplasm surgery", "Intracranial Hematoma/Haemorrhage surgery", "Spinal cord surgery", "Other");
    listyp[4] = new Array("Bleeding", "GI neoplasm surgery", "Perforation/Obstruction", "Other");
    listyp[5] = new Array("All");
    listyp[6] = new Array("Renal neoplasm surgery", "Renal transplant", "Other");
    listyp[7] = new Array("Multi trauma", "Head trauma");

    valtyp = new Array();

    if (c == 2) {
        valtyp[1] = new Array("-0.802", "-0.14", "-0.61");
        valtyp[2] = new Array("-1.376", "-1.315", "-1.261", "-0.682", "-0.797");
        valtyp[3] = new Array("-1.245", "-0.788", "-0.699", "-1.15");
        valtyp[4] = new Array("-0.617", "-0.248", "0.06", "-0.613");
        valtyp[5] = new Array("-0.196");
        valtyp[6] = new Array("-1.204", "-1.042", "-0.196");
        valtyp[7] = new Array("-1.684", "-0.955");
    }

    else {
        valtyp[1] = new Array("-0.199", "0.463", "-0.007");
        valtyp[2] = new Array("-0.773", "-0.712", "-0.658", "-0.079", "-0.194");
        valtyp[3] = new Array("-0.642", "-0.185", "-0.096", "-0.574");
        valtyp[4] = new Array("-0.014", "0.355", "0.663", "-0.01");
        valtyp[5] = new Array("0.407");
        valtyp[6] = new Array("-0.601", "-0.439", "0.407");
        valtyp[7] = new Array("-1.081", "-0.352");
    }

    z = listyp[w].length;

    for (q = 0; q < z; q++) {
        var opt = new Option(listyp[w][q], valtyp[w][q]);
        optt.add(opt, p);
    }
}
function adjucalc() {
    sco = document.apa.sco.value;

    var le, k;

    le = document.apa.adj.length;

    for (k = 0; k < le; k++) {
        if (document.apa.adj[k].checked) {
            ck = document.apa.adj[k].value;
        }
    }

    if (ck == "") { alert("Select Situation"); }

    if (ck == 1) { y = 1 * document.apa.defsys.value; }

    if (ck == 2 || ck == 3) { y = 1 * document.apa.deftyp.value; }

    x = -3.517 + (sco * 0.146) + y;
    aju = 100 * (Math.exp(x)) / (1 + Math.exp(x));

    document.apa.aju.value = aju.toFixed(1);
}
function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}
function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}
function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}
function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}