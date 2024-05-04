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

namespace MediSoftTech_HIS.Areas.IPD.Repository
{
    public class IPDDischargeSummaryPrint
    {
        dataSet dsResult = new dataSet();
        string _Deptname = string.Empty;
        string _IsNABL = string.Empty;
        string _BillPrintType = string.Empty;
        DataSet ds = new DataSet();

        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        PdfDocument repDocument = new PdfDocument();
        public FileResult PrintDischargeReport(string _IPDNo,string IsHeader )
        {
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = _IPDNo;
            obj.UHID = "-";
            obj.DoctorId = "-";
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Prm1 = "-";
            obj.Prm2 = "-";
            obj.login_id = "-";
            obj.Logic = "DischargeReport";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_BillingQuerries", obj);
            ds = dsResult.ResultSet;

            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";

            PdfPage page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(15, 10, 10,50), PdfPageOrientation.Portrait);
            string HtmlBody = string.Empty;
            HtmlBody = GetBodyHTML();

            PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
            htmlBody.BrowserWidth = 780;
            // htmlBody.RenderWebFonts = true;
       
            htmlBody.FontEmbedding = false;
            htmlBody.PageCreatingEvent += new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
            //htmlBody.PageLayoutingEndedEvent += new PdfPageLayoutingEndedDelegate(htmlToPdfConverter_PdfPageLayoutingEndedDelegate);
            htmlBody.ImagesCutAllowed = false;
            page1.Layout(htmlBody);

            var lastPageList = from s in pgCounterList
                               group s by s.FormName into stugrp
                               let topp = stugrp.Max(x => x.PageIndex)
                               select new { DeptName = stugrp.Key, LastPageIndex = topp };
            foreach (var t in lastPageList)
            {
                if ((t.LastPageIndex - 1) == repDocument.Pages.Count - 1)
                {
                    PdfPage lastpdfPage = repDocument.Pages[t.LastPageIndex - 1];
                    SetFooter(lastpdfPage, "FixAtLastPage", t.DeptName, true);
                }
                else
                {
                    PdfPage lastpdfPage = repDocument.Pages[t.LastPageIndex - 1];
                    SetFooter(lastpdfPage, "FixAtLastPage", t.DeptName, false);
                }
            }

            byte[] pdfdata = repDocument.WriteToMemory();
            FileResult fileResult = new FileContentResult(pdfdata, "application/pdf");
            return fileResult;
        }
        int counter = 0;
        private void htmlToPdfConverter_PdfPageLayoutingEndedDelegate(PdfPageLayoutingEndedParams eventParams)
        {
            if(repDocument.Pages.Count == 1 || (counter-2)== eventParams.PdfPage.Index)
            {
               SetFooter(eventParams.PdfPage, "FixAtLastPage", "N/R", false);
            }
            else
            {
               SetFooter(eventParams.PdfPage, "", "N/R", false);
            }
        }
        public void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage page1 = eventParams.PdfPage;
            int pdfPageNumber = eventParams.PdfPageNumber;
            //Set Header
            if (pdfPageNumber == 1)
                SetHeader1(page1);
            else
                SetHeader2(page1);
            counter++;
            ipPageCounter ipc = new ipPageCounter();
            ipc.FormName = "DischargeReport";
            ipc.PageIndex = page1.Index;
            pgCounterList.Add(ipc);

            SetFooter(page1, "Blank", "N/R", false);

        }
        private void SetHeader1(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(220);
                string StrhtmlHeader = GetHeaderHTML1();
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetHeader2(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(160);
                string StrhtmlHeader = GetHeaderHTML2();
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage, string FooterType, string DepartmentName, bool IsLastPage)
        {
            if(pdfPage != null)
            {
                //string StrhtmlFooter = string.Empty;
                //if (FooterType == "Blank")
                //{
                //    pdfPage.CreateFooterCanvas(120);
                //    StrhtmlFooter = "";
                //}
                //else
                //{
                //    if (!IsLastPage)
                //        pdfPage.CreateFooterCanvas(120);
                //    else
                //        pdfPage.CreateFooterCanvas(185);

                //    StrhtmlFooter = GetFooterHTML();
                //}
                string StrhtmlFooter = "";
                if (pdfPage.Footer != null)
                {
                    PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                    pdfPage.Footer.Layout(footerHtml);
                    Font pageNumberFont = new Font(new FontFamily("Arial"), 7, GraphicsUnit.Point); // 1
                    PdfText pageNumberText;
                    if (!IsLastPage)
                        pageNumberText = new PdfText(250, 50, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    else
                        pageNumberText = new PdfText(250,170, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    pdfPage.Footer.Layout(pageNumberText);
                }
            }
        }
        private string GetBodyHTML()
        {
            StringBuilder b = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    b.Append("<div style='width:100%;margin-top:10px'>");
                    b.Append("<label style='padding:2px;margin-top:20px'><b>" + dr["HeaderName"].ToString() + "</b><hr/ style='margin:0 0 3px 0;'></label>");
                    b.Append(dr["template_content"].ToString());
                    b.Append("</div>");
                }
            }
            return b.ToString();
        }
        private string GetFooterHTML()
        {
            return "";
        }
        private string GetHeaderHTML1()
        {
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string patient_name = "";
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
            string DischargeHeader = "";
            string Address = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
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
                ContactNo = dr["ContactNo"].ToString();
                Address = dr["Address"].ToString();
                DischargeHeader = dr["DischargeReportHeader"].ToString();
            }

            b.Append("<div style='height:110px;'></div>");
            b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + DischargeHeader + "</h2>");
            b.Append("<table style='padding:10px 0;background:#fff;width:100%;font-size:22px;text-align:left;border:1px solid #000;margin-bottom:-15px;margin-top:0'>");
            b.Append("<tr>");
            b.Append("<td style='width:20%;padding:3px;'><b>UHID</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;text-aligh:left'>" + UHID + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:20%;'><b>IPD No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + IPDNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Bill Date</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + BillDate + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:20%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + patient_name + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Panel</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + PanelName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:20%;padding:3px;'><b>Age</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;text-aligh:left'>" + ageInfo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Doctor Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DoctorName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:20%;'><b>Contact No </b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + ContactNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Type</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeType + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:20%;'><b>Admission Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + AdmitDate + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:20%;'><b>Discharge Date Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:29%;'>" + DischargeDate + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:20%;'><b>Address</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:79%;font-size:16px;' colspan='5'>" + Address + "</td>");
            b.Append("</tr>");

            b.Append("</table></br/>");
            return b.ToString();
        }
        private string GetHeaderHTML2()
        {
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string patient_name = "";
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
            string DischargeHeader = "";
            string Address = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
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
                ContactNo = dr["ContactNo"].ToString();
                Address = dr["Address"].ToString();
                DischargeHeader = dr["DischargeReportHeader"].ToString();
            }
            h.Append("<div style='height:320px;border-bottom:1px solid #000'></div>");
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