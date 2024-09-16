var Billno = '';
$(document).ready(function () {
    Billno = query()['billno'];
});
function downloadExecl() {

}
function printDownload() {
    var url = 'PrintInvoice?InvoiceNo=' + InvoiceNo;
    window.open(url, '_blank');
}