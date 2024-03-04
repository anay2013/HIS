using HiQPdf;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using System.Web.Mvc;
using static HISWebApi.Models.IPDBO;

namespace MediSoftTech_HIS.Areas.Lab.Repository
{
    public class IPDBillPrint
    {
        dataSet dsResult = new dataSet();
        string _Deptname = string.Empty;
        string _IsNABL = string.Empty;
        string _BillPrintType = string.Empty;
        DataSet ds = new DataSet();

        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        PdfDocument repDocument = new PdfDocument();
        public FileResult PrintBill(string _IPDNo, string _ReceiptList, string BillPrintType, string ExcludeAdlItemDiscount)
        {
            _BillPrintType = BillPrintType;
            _IPDNo = UtilityClass.decoding(_IPDNo);
            if (_ReceiptList == null)
                _ReceiptList = "";
            _ReceiptList = UtilityClass.decoding(_ReceiptList);
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = _IPDNo;
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.ExcludeAdlItemDiscount = ExcludeAdlItemDiscount;
            obj.Prm1 = _ReceiptList;
            obj.Prm2 = _BillPrintType;
            obj.login_id = "-";
            obj.Logic = "BillPrinting";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_BillPrint", obj);
            ds = dsResult.ResultSet;
            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";

            PdfPage page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(15, 10, 10, 10), PdfPageOrientation.Portrait);
            string HtmlBody = string.Empty;
            HtmlBody = GetBodyHTML();

            PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
            htmlBody.BrowserWidth = 780;
            // htmlBody.RenderWebFonts = true;
            htmlBody.FontEmbedding = false;
            htmlBody.PageCreatingEvent += new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
            htmlBody.PageLayoutingEndedEvent += new PdfPageLayoutingEndedDelegate(htmlToPdfConverter_PdfPageLayoutingEndedDelegate);
            htmlBody.ImagesCutAllowed = false;
            page1.Layout(htmlBody);

            byte[] pdfdata = repDocument.WriteToMemory();
            FileResult fileResult = new FileContentResult(pdfdata, "application/pdf");
            return fileResult;
        }

        private void htmlToPdfConverter_PdfPageLayoutingEndedDelegate(PdfPageLayoutingEndedParams eventParams)
        {
            SetFooter(repDocument.Pages[repDocument.Pages.Count - 1], "FixAtLastPage", "N/R", false);
        }

        int count = 1;

        public void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage page1 = eventParams.PdfPage;
            ipPageCounter ipc = new ipPageCounter();
            ipc.DeptName = "-";
            ipc.PageIndex = page1.Index;
            pgCounterList.Add(ipc);
            //Set Header
            if (count == 1)
                SetHeader1(page1);
            else
                SetHeader2(page1);

            count++;

            SetFooter(page1, "", "N/R", false);

        }
        private void SetHeader1(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(240);
                string StrhtmlHeader = GetHeaderHTML1();
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetHeader2(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(140);
                string StrhtmlHeader = GetHeaderHTML2();
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage, string FooterType, string DepartmentName, bool IsLastPage)
        {
            if (pdfPage != null)
            {
                string StrhtmlFooter = "";
                if (FooterType == "FixAtLastPage")
                {
                    if (pdfPage.Footer != null)
                    {
                        pdfPage.CreateFooterCanvas(178);
                        StrhtmlFooter = GetFooterHTML();

                        PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                        footerHtml.FitDestHeight = true;
                        footerHtml.FitDestWidth = true;
                        footerHtml.FontEmbedding = true;
                        pdfPage.Footer.Layout(footerHtml);

                        //Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                        //PdfText pageNumberText;
                        //pageNumberText = new PdfText(500, 50, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                        //pageNumberText.HorizontalAlign = PdfTextHAlign.Center;
                        //pageNumberText.EmbedSystemFont = true;
                        //pdfPage.Footer.Layout(pageNumberText);
                    }

                }
                else
                {

                    pdfPage.CreateFooterCanvas(80);

                    PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                    footerHtml.FitDestHeight = true;
                    footerHtml.FontEmbedding = true;
                    pdfPage.Footer.Layout(footerHtml);

                    Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                    PdfText pageNumberText;
                    pageNumberText = new PdfText(500, 50, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    pageNumberText.HorizontalAlign = PdfTextHAlign.Center;
                    pageNumberText.EmbedSystemFont = true;
                    pdfPage.Footer.Layout(pageNumberText);

                }


            }

        }
        private string GetBodyHTML()
        {
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            if (_BillPrintType != "IncludingPackagedItem")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Category Wise Details</b></label>");
                b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:40%;text-align:left;padding-left:4px;'>Cat Name</th>");
                b.Append("<th style='white-space: nowrap;text-align:right;'>Item Count</th>");
                b.Append("<th style='text-align:right;padding-right:4px;'>Gross Amount</th>");
                b.Append("<th style='white-space: nowrap;text-align:right'>Discount</th>");
                b.Append("<th style='white-space: nowrap;text-align:right'>Net Amount</th>");
                b.Append("</tr>");
                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["CatName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["ItemCount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["GrossAmount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["Discount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["NetAmount"].ToString() + "</td>");
                        b.Append("</tr>");
                    }
                }
                b.Append("</table>");
            }
            if (_BillPrintType == "ItemWise")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Item Wise Details</b></label>");
                b.Append("<table border='0' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:5%;text-align:left;padding-left:4px;'>S.N</th>");
                b.Append("<th style='width:12%;text-align:left;padding-left:4px;'>Tnx Date</th>");
                b.Append("<th style='width:46%;text-align:left;padding-left:4px;'>Item Name</th>");
                b.Append("<th style='width:3%;white-space: nowrap;text-align:right;'>Qty</th>");
                b.Append("<th style='width:12%;text-align:right;padding-right:4px;'>Rate</th>");
                b.Append("<th style='width:11%;white-space: nowrap;text-align:right'>Discount</th>");
                b.Append("<th style='width:16%;white-space: nowrap;text-align:right'>Amount</th>");
                b.Append("</tr>");
                string temp = string.Empty;
                string temp2 = string.Empty;
                string temp3 = string.Empty;
                int CounItem = 1;
                int CountSurgeryItem = 1;
                decimal tAmount = 0;
                decimal tSurgeryAmt = 0;
                if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        if (temp != dr["SubCatNameDisplayName"].ToString())
                        {
                            if (CounItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:13px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SubCatNameDisplayName"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            temp = dr["SubCatNameDisplayName"].ToString();
                        }
                        if (dr["SurgeryName"].ToString().Length > 2 && temp3 != dr["SurgeryName"].ToString())
                        {
                            if (CountSurgeryItem != 0)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tSurgeryAmt = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:11px;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SurgeryName"].ToString() + "<b></td>");
                            b.Append("</tr>");
                            temp3 = dr["SurgeryName"].ToString();
                            tSurgeryAmt = 0;
                            CountSurgeryItem++;
                        }
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:5%'>" + (CounItem++).ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["TnxDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["ItemName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["Qty"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["panel_rate"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["discount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["amount"].ToString() + "</td>");
                        b.Append("</tr>");
                        tAmount = tAmount + Convert.ToDecimal(dr["amount"]);
                        tSurgeryAmt = tSurgeryAmt + Convert.ToDecimal(dr["amount"]);

                    }
                }
                if (CountSurgeryItem > 1)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }
                if (tAmount > 0)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }

                b.Append("</table>");
            }
            if (_BillPrintType == "DateWise")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Item Wise Details</b></label>");
                b.Append("<table border='0' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:5%;text-align:left;padding-left:4px;'>S.N</th>");
                b.Append("<th style='width:12%;text-align:left;padding-left:4px;'>Tnx Date</th>");
                b.Append("<th style='width:46%;text-align:left;padding-left:4px;'>Item Name</th>");
                b.Append("<th style='width:3%;white-space: nowrap;text-align:right;'>Qty</th>");
                b.Append("<th style='width:12%;text-align:right;padding-right:4px;'>Rate</th>");
                b.Append("<th style='width:11%;white-space: nowrap;text-align:right'>Discount</th>");
                b.Append("<th style='width:16%;white-space: nowrap;text-align:right'>Amount</th>");
                b.Append("</tr>");
                string temp = string.Empty;
                string tempDateWise = string.Empty;
                string temp3 = string.Empty;
                int CounItem = 1;
                int CountSurgeryItem = 1;
                int CountDateWise = 1;
                decimal tAmount = 0;
                decimal tSurgeryAmt = 0;
                decimal tDateWiseAmount = 0;
                if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        if (tempDateWise != dr["TnxDate"].ToString())
                        {
                            if (CountDateWise != 0)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Date Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tDateWiseAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tDateWiseAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:13px;text-decoration:underline;background-color:lightgray;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["TnxDate"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            tempDateWise = dr["TnxDate"].ToString();
                        }
                        if (temp != dr["SubCatNameDisplayName"].ToString())
                        {
                            if (CounItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:11px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SubCatNameDisplayName"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            temp = dr["SubCatNameDisplayName"].ToString();
                        }
                        if (dr["SurgeryName"].ToString().Length > 2 && temp3 != dr["SurgeryName"].ToString())
                        {
                            if (CountSurgeryItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                                b.Append("</tr>");
                                tSurgeryAmt = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='7' style='font-size:11px;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["SurgeryName"].ToString() + "<b></td>");
                            b.Append("</tr>");
                            temp3 = dr["SurgeryName"].ToString();
                            tSurgeryAmt = 0;
                            CountSurgeryItem++;
                        }
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:5%'>" + (CounItem++).ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["TnxDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["ItemName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["Qty"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["panel_rate"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["discount"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["amount"].ToString() + "</td>");
                        b.Append("</tr>");
                        tAmount = tAmount + Convert.ToDecimal(dr["amount"]);
                        tSurgeryAmt = tSurgeryAmt + Convert.ToDecimal(dr["amount"]);
                        tDateWiseAmount = tDateWiseAmount + Convert.ToDecimal(dr["amount"]);
                        CountDateWise++;
                    }
                }
                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Date Wise Total<b></td>");
                b.Append("<td  style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tDateWiseAmount).ToString(".00") + "<b></td>");
                b.Append("</tr>");

                if (CountSurgeryItem > 1)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Surgery Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tSurgeryAmt).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }
                if (tAmount > 0)
                {
                    b.Append("<tr style='background:#fff'>");
                    b.Append("<td colspan='6' style='font-size:11px;text-align:right;white-space: nowrap;padding:5px 4px;'><b>Category Wise Total<b></td>");
                    b.Append("<td  style='font-size:11px;text-decoration:underline;text-align:right;white-space: nowrap;padding:5px 4px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                    b.Append("</tr>");
                }

                b.Append("</table>");
            }
            if (_BillPrintType == "IncludingPackagedItem")
            {
                b.Append("<br/><label style='padding:2px 5px;margin-top:20px'><b>Item Wise Details</b></label>");
                b.Append("<table border='0' style='width:100%;font-size:12px;border-collapse: collapse'>");
                b.Append("<tr style='background:#ddd'>");
                b.Append("<th style='width:5%;text-align:left;padding-left:4px;'>S.N.</th>");
                b.Append("<th style='width:10%;text-align:left;padding-left:4px;'>Tnx Date</th>");
                b.Append("<th style='width:20%;text-align:left;padding-left:4px;'>Sub Category Name</th>");
                b.Append("<th style='width:45%;text-align:left;padding-left:4px;'>Item Name</th>");
                b.Append("<th style='width:5%;white-space: nowrap;text-align:right;'>Qty</th>");
                b.Append("<th style='width:15%;white-space: nowrap;text-align:right'>Amount</th>");
                b.Append("</tr>");
                string temp = string.Empty;
                string temp2 = string.Empty;
                string temp3 = string.Empty;
                int CounItem = 1;
                decimal tAmount = 0;
                decimal PackageAmount = 0;
                if (ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        if (temp != dr["PackageName"].ToString())
                        {
                            if (CounItem != 1)
                            {
                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='6' style='text-align:right;white-space: nowrap;padding-right:4px;hight:15px'></td>");
                                b.Append("</tr>");

                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-right:4px;width:5%;font-size:13px;'><b>Item wise Total<b></td>");
                                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");

                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Package Discount Amount<b></td>");
                                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount - PackageAmount).ToString(".00") + "<b></td>");
                                b.Append("</tr>");

                                b.Append("<tr style='background:#fff'>");
                                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Net Package Amount<b></td>");
                                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + PackageAmount.ToString(".00") + "<b></td>");
                                b.Append("</tr>");

                                tAmount = 0;
                            }
                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='6' style='text-align:right;white-space: nowrap;padding-right:4px;hight:15px'><hr/></td>");
                            b.Append("</tr>");

                            b.Append("<tr style='background:#fff'>");
                            b.Append("<td colspan='6' style='font-size:13px;text-decoration:underline;text-align:left;white-space: nowrap;padding:5px 4px;'><b>" + dr["PackageName"].ToString().ToUpper() + "<b></td>");
                            b.Append("</tr>");
                            temp = dr["PackageName"].ToString();
                            PackageAmount = Convert.ToDecimal(dr["PackageAmount"]);
                        }
                        b.Append("<tr>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:5%'>" + (CounItem++).ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:10%'>" + dr["TnxDate"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:20%'>" + dr["SubCatNameDisplayName"].ToString() + "</td>");
                        b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;width:45%'>" + dr["ItemName"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:5%'>" + dr["Qty"].ToString() + "</td>");
                        b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%'>" + Convert.ToDecimal(dr["amount"]).ToString(".00") + "</td>");
                        b.Append("</tr>");
                        tAmount = tAmount + Convert.ToDecimal(dr["amount"]);
                    }
                }
                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='6' style='text-align:right;white-space: nowrap;padding-right:4px;hight:15px'><hr/></td>");
                b.Append("</tr>");

                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-right:4px;width:5%;font-size:13px;'><b>Item wise Total<b></td>");
                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount).ToString(".00") + "<b></td>");
                b.Append("</tr>");

                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Package Discount Amount<b></td>");
                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + Convert.ToDecimal(tAmount - PackageAmount).ToString(".00") + "<b></td>");
                b.Append("</tr>");

                b.Append("<tr style='background:#fff'>");
                b.Append("<td colspan='4' style='text-align:right;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'><b>Net Package Amount<b></td>");
                b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;width:5%;font-size:13px;'>:<b></td>");
                b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;width:15%;font-size:13px;'><b>" + PackageAmount.ToString(".00") + "<b></td>");
                b.Append("</tr>");


                b.Append("</table>");
            }
            return b.ToString();
        }
        private string GetFooterHTML()
        {
            StringBuilder b = new StringBuilder();
            double GrossAmount = 0;
            double PanelDiscount = 0;
            double AdlDiscount = 0;
            double TotalDiscount = 0;
            double NetAmount = 0;
            double ReceivedAmount = 0;
            double PanelApprovedAmount = 0;
            double BalanceAmount = 0;
            string DischargeByName = string.Empty;
            foreach (DataRow dr in ds.Tables[4].Rows)
            {
                GrossAmount = Convert.ToDouble(dr["TotalAmount"].ToString());
                PanelDiscount = Convert.ToDouble(dr["PanelDiscount"].ToString());
                AdlDiscount = Convert.ToDouble(dr["AdlDiscount"].ToString());
                TotalDiscount = Convert.ToDouble(dr["TotalDiscount"].ToString());
                NetAmount = Convert.ToDouble(dr["NetAmount"].ToString());
                ReceivedAmount = Convert.ToDouble(dr["Received"].ToString());
                PanelApprovedAmount = Convert.ToDouble(dr["PanelApprovedAmount"].ToString());
                BalanceAmount = Convert.ToDouble(dr["BalanceAmount"].ToString());

            }
            DischargeByName = ds.Tables[0].Rows[0]["DischargeByName"].ToString();
            //Bottom info			
            b.Append("<div style='width:100%;float:left;margin-top:10px;border-top:1px solid #000;padding-top:5px'>");
            b.Append("<div style='width:60%;float:left'>");
            int Count = 0;
            if (ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
            {
                b.Append("<label style='padding:2px 5px;'><b>Advance/Return Details</b></label>");
                b.Append("<table border='1' style='width:100%;font-size:16px;border-collapse: collapse;margin-top:10px;'>");

                b.Append("<tr>");
                //b.Append("<th style='width:1%;text-align:left;padding-left:4px;'>S.No.</th>");
                b.Append("<th style='text-align:center;padding-left:4px;'>Receipt No</th>");
                b.Append("<th style='text-align:center;padding-left:4px;'>Receipt Date</th>");
                b.Append("<th style='text-align:right;padding-right:4px;'>Amount</th>");
                b.Append("<th style='text-align:center;padding-right:4px;'>Pay Mode</th>");
                b.Append("</tr>");
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    Count++;
                    b.Append("<tr>");
                    //  b.Append("<td style='padding-left:4px;'>" + Count + "</td>");
                    b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;'>" + dr["ReceiptNo"].ToString() + "</td>");
                    b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;'>" + dr["receiptDate"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;'>" + Convert.ToDecimal(dr["Amount"]).ToString("0.00") + "</td>");
                    b.Append("<td style='text-align:center;white-space: nowrap;padding-left:4px;'>" + dr["PayMode"].ToString() + "</td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
            }

            b.Append("</div>");
            b.Append("<div style='width:40%;float:right'>");
            b.Append("<table style='font-size:40px;float:right' border='0' cellspacing='0'>");

            b.Append("<tr style='font-size:20px'>");
            b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Net Amount (a-(b+c)) </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> : </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> Rs. </b></td>");
            b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;border-bottom:1px solid #000'><b>" + NetAmount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");

            b.Append("<tr style='font-size:20px'>");
            b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Gross Amount(a)</b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + GrossAmount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");
            b.Append("<tr style='font-size:20px'>");
            b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Panel Discount(b)</b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + PanelDiscount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");
            b.Append("<tr style='font-size:20px;'>");
            b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left;'><b>Adl. Discount(c)</b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> : </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> Rs. </b></td>");
            b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;border-bottom:1px solid #000'><b>" + AdlDiscount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");
            b.Append("<tr style='font-size:20px'>");
            b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Total Discount(b+c) </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + TotalDiscount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");


            b.Append("<tr style='font-size:20px'>");
            b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Received Amount </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + ReceivedAmount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");

            if (PanelApprovedAmount > 0)
            {
                b.Append("<tr style='font-size:20px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Panel Approval Amount </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + PanelApprovedAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
            }

            b.Append("<tr style='font-size:20px'>");
            b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Balance Amount </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
            b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
            b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + BalanceAmount.ToString("0.00") + "</b></td>");
            b.Append("</tr>");


            b.Append("</table>");
            b.Append("</div>");
            b.Append("</div>");

            b.Append("<div style='width:100%;float:left;margin-top:40px;display:flex;font-size:20px'>");
            b.Append("<p style='width:48%;float:left,display:inline-block'>");
            b.Append("<labe style='text-align:center;'>...............................................<br><b>Patient's/Attendent's Signature</b></label>");
            b.Append("</p>");

            b.Append("<p style='width:48%;float:right;text-align:right;display:inline-block'>");
            b.Append("<labe style='float:right;text-align:center;'>............................................<br><b>Authorised Signature</b><br/>" + DischargeByName + "</label>");
            b.Append("</p>");

            b.Append("</div>");
            return b.ToString();
        }
        private string GetHeaderHTML1()
        {
            StringBuilder b = new StringBuilder();
            string UHID = "";
            string patient_name = "";
            string BillType = "";
            string ageInfo = "";
            string AdmitDate = "";
            string IPDNo = "";
            string DoctorName = "";
            string roomFullName = "";
            string PanelName = "";
            string DischargeDate = "";
            string Department = "";
            string BillNo = "";
            string BillDate = "";
            string ContactNo = "";
            string DischargeType = "";
            string DischargeByName = "";
            string Address = "";
             foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
                BillType = dr["BillType"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
                AdmitDate = dr["AdmitDate"].ToString();
                DoctorName = dr["DoctorName"].ToString();
                roomFullName = dr["roomFullName"].ToString();
                PanelName = dr["PanelName"].ToString();
                DischargeDate = dr["DischargeDate"].ToString();
                Department = dr["Department"].ToString();
                BillNo = dr["BillNo"].ToString();
                BillDate = dr["BillDate"].ToString();
                DischargeType = dr["DischargeType"].ToString();
                DischargeByName = dr["DischargeByName"].ToString();
                ContactNo = dr["ContactNo"].ToString();
                Address = dr["Address"].ToString();
                BillType = dr["BillType"].ToString();
            }
            b.Append("<div style='height:140px;'></div>");

            b.Append("<table style='padding:5px;background:#fff;width:100%;font-size:18px;text-align:left;border:1px solid #000;'>");
            b.Append("<tr>");
            b.Append("<td style='width:25%;text-align:left;'><b>GST NO : 09AABCC9314K1ZH</b></td>");
            if (_BillPrintType == "ItemWise")
                b.Append("<td style='width:50%;text-align:center;font-size:22px;'><b>" + BillType + " Item wise Bill Detail</b></td>");
            if (_BillPrintType == "DateWise")
                b.Append("<td style='width:50%;text-align:center;font-size:22px;'><b>" + BillType + " Date wise Bill Detail</b></td>");
            if (_BillPrintType == "CategorywiseOnly")
                b.Append("<td style='width:50%;text-align:center;font-size:22px;'><b>" + BillType + " Summary Bill Detail</b></td>");
            if (_BillPrintType == "IncludingPackagedItem")
                b.Append("<td style='width:50%;text-align:center;font-size:22px;'><b>" + BillType + " Package Breakup Detail</b></td>");
            b.Append("<td style='width:25%;text-align:right'><b>PAN NO : AABCC9314K</b></td>");
            b.Append("</tr>");
            b.Append("</table>");



            b.Append("<table style='padding:10px 0;background:#fff;width:100%;font-size:22px;text-align:left;border:1px solid #000;margin-bottom:-15px;margin-top:0'>");
            b.Append("<tr>");
            b.Append("<td style='width:17%;padding:3px;'><b>UHID</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;text-aligh:left'>" + UHID + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>IPD No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + IPDNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillDate + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + patient_name + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Panel</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + PanelName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;padding:3px;'><b>Age</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;text-aligh:left'>" + ageInfo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Doctor Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DoctorName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Contact No </b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + ContactNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Type</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeType + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b>Address</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:32%;'>" + Address + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Admission Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + AdmitDate + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:17%;'><b></b></td>");
            b.Append("<td style='width:1%;'><b></b></td>");
            b.Append("<td style='width:32%;'></td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeDate + "</td>");
            b.Append("</tr>");

            b.Append("</table>");

            return b.ToString();
        }
        private string GetHeaderHTML2()
        {
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string patient_name = "";
            string BillType = "";
            string ageInfo = "";
            string AdmitDate = "";
            string IPDNo = "";
            string DoctorName = "";
            string roomFullName = "";
            string PanelName = "";
            string DischargeDate = "";
            string Department = "";
            string BillNo = "";
            string BillDate = "";
            string ContactNo = "";
            string DischargeType = "";
            string DischargeByName = "";
            string Address = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
                BillType = dr["BillType"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
                AdmitDate = dr["AdmitDate"].ToString();
                DoctorName = dr["DoctorName"].ToString();
                roomFullName = dr["roomFullName"].ToString();
                PanelName = dr["PanelName"].ToString();
                DischargeDate = dr["DischargeDate"].ToString();
                Department = dr["Department"].ToString();
                BillNo = dr["BillNo"].ToString();
                BillDate = dr["BillDate"].ToString();
                DischargeType = dr["DischargeType"].ToString();
                DischargeByName = dr["DischargeByName"].ToString();
                ContactNo = dr["ContactNo"].ToString();
                Address = dr["Address"].ToString();
            }
            h.Append("<div style='height:225px;'></div>");
            h.Append("<table style='width:2080px;padding:10px 0;background:#fff;font-size:42px;text-align:left;border:1px solid #000;margin:0 15px'>");

            h.Append("<tr>");
            h.Append("<td style='width:17%;padding:3px;'><b>UHID</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;text-aligh:left'>" + UHID + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Bill No</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + BillNo + "</td>");
            h.Append("</tr>");

            h.Append("<tr>");
            h.Append("<td style='width:17%;'><b>IPD No</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;'>" + IPDNo + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + BillDate + "</td>");
            h.Append("</tr>");
            h.Append("<tr>");
            h.Append("<td style='width:17%;'><b>Patient Name</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:32%;'>" + patient_name + "</td>");
            h.Append("<td style='width:1%;'>&nbsp;</td>");
            h.Append("<td style='width:20%;'><b>Panel</b></td>");
            h.Append("<td style='width:1%;'><b>:</b></td>");
            h.Append("<td style='width:29%;'>" + PanelName + "</td>");
            h.Append("</tr>");

            return h.ToString();
        }
    }


}