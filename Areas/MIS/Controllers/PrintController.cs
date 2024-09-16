using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.MIS.Controllers
{
    public class PrintController : Controller
    {
        public FileResult CollectionSummaryReport(string hosp_id, string from, string to, string prm_1, string loginId)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipFinance obj = new ipFinance();
            obj.hosp_id = hosp_id;
            obj.from = from.ToString();
            obj.to = to.ToString();
            obj.prm_1 = prm_1.ToString();
            obj.loginId = loginId;
            obj.Logic = "CollectionSummaryReportByUser";
            dataSet dsResult = APIProxy.CallWebApiMethod("Finance/Financial_Queries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            StringBuilder f = new StringBuilder();
            int Count = 0;
            double p_Cash = 0;
            double p_SwipeCard = 0;
            double p_Cheque = 0;
            double p_NEFT_RTGS = 0;
            double n_Cash = 0;
            double n_SwipeCard = 0;
            double n_Cheque = 0;
            double n_NEFT_RTGS = 0;
            double f_cash = 0;
            double f_SwipeCard = 0;
            double f_Cheque = 0;
            double f_NEFT_RTGS = 0;
            double FinalTotal = 0;

            //b.Append("<div style='width:100%;float:left;margin-top:-12px;padding:8px'>");          
            b.Append("<div style='text-align:center;float:left;width:100%;'>");
            b.Append("<h2 style='font-weight:bold;margin:0'>Chandan Hospital</h2>");
            b.Append("<h3 style='font-weight:bold;margin:0'>Collection Summary Report</h3>");
            b.Append("<h4 style='font-weight:bold;margin:0'>From : " + Convert.ToDateTime(from).ToString("MM/dd/yyyy") + ", To : " + Convert.ToDateTime(to).ToString("MM/dd/yyyy") + "</h4>");
            b.Append("</div>");
            //b.Append("</div>");
            b.Append("<hr/>");
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    invoiceNo = ds.Tables[2].Rows[0]["InvoiceNo"].ToString();
            //    InvoiceType = ds.Tables[2].Rows[0]["InvoiceType"].ToString();
            //    InvoiceAmount = Convert.ToDouble(ds.Tables[2].Rows[0]["InvoiceAmount"].ToString());
            //    AmountTobePaid = Convert.ToDouble(ds.Tables[2].Rows[0]["AmountTobePaid"].ToString());
            //    EQAS_Deduction = Convert.ToDouble(ds.Tables[2].Rows[0]["EQAS_Deduction"].ToString());
            //}           
            b.Append("<div style='text-align:center;float:left;width:1200px;'>");
            b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse;margin-top:10px;'>");
            b.Append("<tr>");
            b.Append("<th></th>");
            b.Append("<th colspan='4' class='text-center'>Collection</th>");
            b.Append("<th colspan='4' class='text-center'>Cancel/Refunds</th>");
            b.Append("<th colspan='4' class='text-center'>Net Collection</th>");
            b.Append("<th></th>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<th style='padding-left:4px;text-align:left'>User Name</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cash</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Swipe Card</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cheque</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>NEFT</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cash</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Swipe Card</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cheque</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>NEFT</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cash</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Swipe Card</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cheque</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>NEFT</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Total</th>");
            b.Append("</tr>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    p_Cash += Convert.ToDouble(dr["p_Cash"].ToString());
                    p_SwipeCard += Convert.ToDouble(dr["p_SwipeCard"].ToString());
                    p_Cheque += Convert.ToDouble(dr["p_Cheque"].ToString());
                    p_NEFT_RTGS += Convert.ToDouble(dr["p_NEFT_RTGS"].ToString());
                    n_Cash += Convert.ToDouble(dr["n_Cash"].ToString());
                    n_SwipeCard += Convert.ToDouble(dr["n_SwipeCard"].ToString());
                    n_Cheque += Convert.ToDouble(dr["n_Cheque"].ToString());
                    n_NEFT_RTGS += Convert.ToDouble(dr["n_NEFT_RTGS"].ToString());
                    f_cash += Convert.ToDouble(dr["f_cash"].ToString());
                    f_SwipeCard += Convert.ToDouble(dr["f_SwipeCard"].ToString());
                    f_Cheque += Convert.ToDouble(dr["f_Cheque"].ToString());
                    f_NEFT_RTGS += Convert.ToDouble(dr["f_NEFT_RTGS"].ToString());
                    FinalTotal += Convert.ToDouble(dr["FinalTotal"].ToString());

                    Count++;
                    b.Append("<tr>");
                    b.Append("<td>" + dr["StaffName"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["p_Cash"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["p_SwipeCard"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["p_Cheque"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["p_NEFT_RTGS"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["n_Cash"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["n_SwipeCard"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["n_Cheque"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["n_NEFT_RTGS"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["f_cash"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["f_SwipeCard"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["f_Cheque"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["f_NEFT_RTGS"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["FinalTotal"].ToString() + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("<tr>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>Total</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + p_Cash + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + p_SwipeCard + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + p_Cheque + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + p_NEFT_RTGS + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + n_Cash + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + n_SwipeCard + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + n_Cheque + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + n_NEFT_RTGS + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + f_cash + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + f_SwipeCard + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + f_Cheque + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + f_NEFT_RTGS + "</th>");
            b.Append("<th style='padding-right:4px;text-align:right;background:#ddd;font-size:15px;'>" + FinalTotal + "</th>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Footer_Hight = 135;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Landscape";
            //pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "CollectionSummaryReportByUser.pdf");
        }
        public FileResult DailyCollectionReport(string hosp_id, string from, string to, string prm_1, string loginId)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipFinance obj = new ipFinance();
            obj.hosp_id = hosp_id;
            obj.from = from.ToString();
            obj.to = to.ToString();
            obj.prm_1 = prm_1.ToString();
            obj.loginId = loginId;
            obj.Logic = "DailyCollectionReport";
            dataSet dsResult = APIProxy.CallWebApiMethod("Finance/Financial_Queries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            StringBuilder f = new StringBuilder();
            int Count = 0;
            b.Append("<div style='text-align:center;float:left;width:100%;'>");
            b.Append("<h2 style='font-weight:bold;margin:0'>Chandan Hospital</h2>");
            b.Append("<h3 style='font-weight:bold;margin:0'>Daily Collection Report</h3>");
            b.Append("<h4 style='font-weight:bold;margin:0'>From : " + Convert.ToDateTime(from).ToString("MM/dd/yyyy") + ", To : " + Convert.ToDateTime(to).ToString("MM/dd/yyyy") + "</h4>");
            b.Append("</div>");
            b.Append("<hr/>");
            b.Append("<div style='text-align:center;float:left;width:1200px;'>");
            b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse;margin-top:10px;'>");
            b.Append("<tr>");
            b.Append("<th style='padding-left:4px;text-align:left'>S.No</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Date</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Receipt No.</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>UHID</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>IPOP No.</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Patient Name</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Panel Name</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Tnx Type</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Ref No</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Bill Total</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cash</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Cheque</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Swipe Card</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>NEFT</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Received</th>");
            b.Append("<th style='padding-right:4px;text-align:right'>Opd Credit</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>User</th>");
            b.Append("<th style='padding-left:4px;text-align:left'>Bill No</th>");
            b.Append("</tr>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Count++;
                    b.Append("<tr>");
                    b.Append("<td>" + dr["SrNo"].ToString() + "</td>");
                    b.Append("<td>" + dr["RcptDate"].ToString() + "</td>");
                    b.Append("<td>" + dr["ReceiptNo"].ToString() + "</td>");
                    b.Append("<td>" + dr["uhid"].ToString() + "</td>");
                    b.Append("<td>" + dr["ipop_no"].ToString() + "</td>");
                    b.Append("<td>" + dr["patient_name"].ToString() + "</td>");
                    b.Append("<td>" + dr["PanelName"].ToString() + "</td>");
                    b.Append("<td>" + dr["tnxType"].ToString() + "</td>");
                    b.Append("<td>" + dr["RefNo"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["BillTotal"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["Cash"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["Cheque"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["SwipeCard"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["NEFT"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["Received"].ToString() + "</td>");
                    b.Append("<td style='padding-right:4px;text-align:right'>" + dr["OPCredit"].ToString() + "</td>");
                    b.Append("<td>" + dr["ByStaff"].ToString() + "</td>");
                    b.Append("<td>" + dr["bill_no"].ToString() + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("</table>");
            b.Append("</div>");

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Footer_Hight = 135;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Landscape";
            //pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "DailyCollectionReport.pdf");
        }

        public FileResult ReceiptInfo(string fromdate, string todate, string UHIDNO, string Username, string Logicexcel)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipFinance obj = new ipFinance();
            obj.hosp_id = "-";
            obj.prm_1 = Username;
            obj.from = fromdate;
            obj.to = todate;
            obj.prm_2 = UHIDNO;
            obj.prm_4 = "-";
            obj.prm_3 = "-";
            obj.loginId = "-";
            obj.Logic = Logicexcel.Trim().ToString();
            dataSet dsResult = APIProxy.CallWebApiMethod("Finance/Financial_Queries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();

            b.Append("<div style='width:100%;float:left;'>");
            b.Append("<div style='text-align:center;width:auto;'>");
            b.Append("<h2 style='font-weight:bold;text-align:center;text-decoration:underline'>Advance/Adjusted Report</h2>");
            b.Append("<h3 style='font-weight:bold;text-align:center;text-decoration:underline;margin-top:-10px'></h3>");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("<table style='width:100%;font-size:10px;text-align:left;margin-top:5px;border-collapse:collapse;' border='1' >");
            b.Append("<tr>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;'>Sr.No</th>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;width:7%'>IPOPType</th>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;width:10%'>IPD NO</th>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;width:10%'>UHID</th>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;width:10%'>ReceiptType</th>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;width:15%'>ReceiptNo</th>");
            b.Append("<th style='white-space:nowrap;padding-left:3px;padding-right:3px;font-size:15px;width:15%'>ReceiptDate</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px; font-size:15px;width:20%'>Patient Name</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px;font-size:15px;width:10%'>AgeInfo</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px;font-size:15px;width:10%'>PayMode</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px;font-size:15px;width:10%'>Amount</th>");
            b.Append("<th style='padding-left:3px;padding-right:3px;font-size:15px;width:13%'>EntryBy</th>");
            b.Append("</tr>");

            //Body			
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var count = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    count++;
                    b.Append("<tr>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px'>" + count.ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:7%'>" + dr["IPOPType"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["ipop_no"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["UHID"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["ReceiptType"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["ReceiptNo"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:15%'>" + dr["receiptDate"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:20%'>" + dr["patient_name"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["ageInfo"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["PayMode"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:10%'>" + dr["Amount"].ToString() + "</td>");
                    b.Append("<td style='padding-left:3px;padding-right:3px;font-size:13px;width:13%'>" + dr["EntryBy"].ToString() + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("</table>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.Browser_Width = 1150;
            pdfConverter.PageOrientation = "Landscap";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "ReceiptInfo.pdf");
        }
    }
}