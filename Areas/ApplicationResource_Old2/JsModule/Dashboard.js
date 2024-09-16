$(document).ready(function () {

})
function Generate() {
    var input = $('#txtInput').val();
    debugger
    var output = "\nvar objBO={}";
    var data = input.split(',');
    for (var i = 0; i <= data.length; i++) {
        output += "\nobjBO." + data[i].split(' ','')[0] + '="";';
        }
    $('#txtOutPut').val(output);
}