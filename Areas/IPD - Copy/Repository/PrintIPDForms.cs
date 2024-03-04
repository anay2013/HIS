using HiQPdf;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using MediSoftTech_HIS.Areas.IPD.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.IPD.Repository
{
    public class PrintIPDForms
    {
        dataSet dsResult = new dataSet();
        string _TemplateName = string.Empty;
        string _PageName = string.Empty;
        string _PageIndex = string.Empty;
    
        string _IsNABL = string.Empty;
        string _PrintWithHeader = "N";
        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        public FileResult PrintForms(List<ipFormPrintingRequest> FormList)
        {
            PdfDocument repDocument=new PdfDocument();
            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";
            FormTemplateRepo repo = new FormTemplateRepo();
            foreach (var form in FormList)
            {
                _TemplateName = form.FormHeader;
                _PageName = form.PageName;
                _PageIndex = form.PageIndex;
           
                PdfPage page1;
                if (form.PageOrientation == "Portrait")
                {
                    page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(15, 10, 10,10), PdfPageOrientation.Portrait);
                }
                else
                {
                    page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(15, 10, 10, 10), PdfPageOrientation.Landscape);
                }
                
                string HtmlBody= repo.GetFormTemplate(form.TemplateName,form.pname,form.uhidno,form.gender,form.admitdate,form.ipdno,form.doctor,form.Diagnosis,form.PageName, form.PageIndex,form.PageOrientation,form.FormHeader);
                
                PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
                htmlBody.BrowserWidth = 780;
                htmlBody.FontEmbedding = false;
                htmlBody.PageCreatingEvent += new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
                htmlBody.ImagesCutAllowed = false;
                page1.Layout(htmlBody);
                
            }
            byte[] pdfdata = repDocument.WriteToMemory();
            FileResult fileResult = new FileContentResult(pdfdata, "application/pdf");
            return fileResult;
        }
        public void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage page1 = eventParams.PdfPage;
            SetHeader(page1);
            SetFooter(page1, _PageName, _PageIndex);
        }
        private void SetHeader(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(24);
                string StrhtmlHeader = GetHeaderHTML(_TemplateName);
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }

        }
        private void SetFooter(PdfPage pdfPage, string PageName, string PageIndex)
        {
            if (pdfPage != null)
            {
                
                pdfPage.CreateFooterCanvas(18);
                string StrhtmlFooter = GetFooterHTML(PageName, PageIndex);
                PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                footerHtml.FitDestHeight = false;
                footerHtml.FontEmbedding = false;
                pdfPage.Footer.Layout(footerHtml);
                //Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                //PdfText pageNumberText;
                //pageNumberText = new PdfText(500, 105, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                //pdfPage.Footer.Layout(pageNumberText);
            }
            
        }
        private string GetHeaderHTML(string TemplateName)
        {
            //return "<div style='text-align:center;margin-top:1px;font-size:24px;font-weight:bold;width:100%;'>" + TemplateName + "</div>";
            return "<div style='text-align:center;margin-top:1px;font-size:24px;font-weight:bold;width:100%;'></div>";
        }
        private string GetFooterHTML(string PageName,string PageIndex)
        {
            StringBuilder f = new StringBuilder();
            f.Append("<table style='width:100%; margin-top:-12px;'>");
            f.Append("<tr>");
            f.Append("<td style='width:20%'></td>");
            f.Append("<td style='width:60%;text-align:center;font-size:25px;'>" + PageName + "</td>");
            f.Append("<td style='width:20%;text-align:right;font-size:30px;font-weight:bold;'>" + PageIndex + "</td>");
            f.Append("</tr>");
            f.Append("</table>");
            return f.ToString();
        }
    }
    public class ipPageCounter
    {
        public string FormName { get; set;}
        public int  PageIndex { get; set; }
        public bool IsLastPage { get; set; }
    }
    public class ipFormPrintingRequest
    {
        public string UsedIn { get; set; }
        public string TemplateName { get; set; }
        public string DocName { get; set; }
        public string PageName { get; set; }
        public string PageIndex { get; set; }
        public string pname { get; set; }
        public string uhidno { get; set; }
        public string gender { get; set; }
        public string admitdate { get; set; }
        public string ipdno { get; set; }
        public string doctor { get; set; }
        public string Diagnosis { get; set; }
        public string PageOrientation { get; set; }

        public string FormHeader { get; set; }
    }

}