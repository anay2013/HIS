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

namespace MediSoftTech_HIS.Areas.Lab.Repository
{
    public class WorkSheet
    {
        dataSet dsResult = new dataSet();
        string _Deptname = string.Empty;
        string _BarCodeNo = string.Empty;
        string _IsNABL = string.Empty;
        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        public FileResult PrintWorkSheet(string visitNo, string SubCat,string TestIds,string Logic)
        {
            LabReporting obj = new LabReporting();
            obj.LabCode = "-";
            obj.IpOpType = "-";
            obj.ReportStatus = "-";
            obj.SubCat = TestIds;
            obj.VisitNo = visitNo;
            obj.BarccodeNo = "-";
            obj.TestCategory = "-";
            obj.AutoTestId = 0;
            obj.TestCode = "-";
            obj.from = Convert.ToDateTime("1900/01/01");
            obj.to = Convert.ToDateTime("1900/01/01");
            obj.Logic = "WorkSheetPrint";
            dsResult = APIProxy.CallWebApiMethod("sample/LabReporting_Queries", obj);
            PdfDocument repDocument=new PdfDocument();
            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";
            //Geting Distinct department list with NABL Flag to separate the report body for NABLE Logo at header

            var DeptList = dsResult.ResultSet.Tables[1].AsEnumerable().Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName"),
                barcodeNo = y.Field<string>("barcodeNo"),
                SeqNo = y.Field<Int64>("SeqNo")
            }).ToList().OrderBy(y =>y.SeqNo).GroupBy(x => new { x.SubCatName, x.barcodeNo });
            foreach (var dept in DeptList)
            {
                _Deptname = dept.First().SubCatName;
                _BarCodeNo= dept.First().barcodeNo;
                PdfPage page1 = repDocument.AddPage(PdfPageSize.A5,new PdfDocumentMargins(15, 10, 10, 10), PdfPageOrientation.Portrait);
                string HtmlBody = string.Empty;
                HtmlBody = GetBodyHTML(_Deptname, dsResult.ResultSet,_IsNABL, _BarCodeNo);
                PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
                htmlBody.BrowserWidth = 780;
                // htmlBody.RenderWebFonts = true;
                // htmlBody.FitDestHeight = true;
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
            //Set Header
            SetHeader(page1);
            SetFooter(page1, "Blank", "N/R", false);
        }
        private void SetHeader(PdfPage pdfPage)
        {
            if(pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(100);
                string StrhtmlHeader = GetHeaderHTML(_Deptname, dsResult.ResultSet,_IsNABL);
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage, string FooterType, string DepartmentName, bool IsLastPage)
        {
            if(pdfPage != null)
            {
                pdfPage.CreateFooterCanvas(40);
                string StrhtmlFooter = GetFooterHTML(DepartmentName, dsResult.ResultSet, IsLastPage);
                if (pdfPage.Footer != null)
                {
                    PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                    footerHtml.FitDestHeight = true;
                    footerHtml.FontEmbedding = true;
                    pdfPage.Footer.Layout(footerHtml);

                    Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                    PdfText pageNumberText;
                    pageNumberText = new PdfText(200,13, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    pdfPage.Footer.Layout(pageNumberText);
                }
            }
        }
        private string GetBodyHTML(string DepartmentName, DataSet ds,string IsNABL,string BarCodeNo)
        {
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            var ObsTest = ds.Tables[1].AsEnumerable().Where(z => z.Field<string>("SubCatName") == DepartmentName && z.Field<string>("barcodeNo") == BarCodeNo).Select(y => new
            {
                TestName = y.Field<string>("TestName"),
                ObservationName = y.Field<string>("ObservationName"),
                r_type = y.Field<string>("r_type"),
                ObsCount = y.Field<Int64>("ObsCount"),
                TestSeqNo = y.Field<Int64>("seqNo"),
            }).OrderBy(y => y.TestName).ToList();
            b.Append("<table border='0' style='width:100%;font-size:15px; border-collapse: collapse;margin-top:-10px;'>");
            if (ObsTest.Count > 0)
            {
                string temp = string.Empty;
                foreach (var dr in ObsTest)
                {
                    if (dr.ObsCount==1)
                    {
                        if (dr.r_type == "Text")
                        {
                            b.Append("<tr>");
                            b.Append("<td style='page-break-after;width:100%;text-align' colspan='2'>" + dr.TestName + "</td>");
                            b.Append("<tr>");
                            temp = dr.TestName;
                        }
                        else
                        {
                            b.Append("<tr style='height:35px;'><br/>");
                            b.Append("<td style='width:70%;font-size:20px;'>" + dr.ObservationName + "</td>");
                            b.Append("<td style='width:30%'>..........................................</td>");
                            b.Append("<tr>");
                        }
                    }
                    else
                    {
                        if (temp != dr.TestName)
                        {
                            b.Append("<tr>");
                            b.Append("<td colspan='2' style='width:100%;background-color:lightgray'>" + dr.TestName + "</td>");
                            b.Append("<tr>");
                           // b.Append("<hr style='border:1px solid black;'/>");
                            temp = dr.TestName;
                        }
                        if (dr.r_type=="Text")
                        {
                            b.Append("<tr style='height:200px;'>");
                            b.Append("<td style='width:70%;font-size:20px;vertical-align:bottom'>" + dr.ObservationName + "</td>");
                            b.Append("<td style='width:30%;vertical-align:bottom'>..........................................</td>");
                            b.Append("<tr>");
                        }
                        else
                        {
                            b.Append("<tr style='height:35px;'>");
                            b.Append("<td style='width:70%;font-size:20px;'>" + dr.ObservationName + "</td>");
                            b.Append("<td style='width:30%'>..........................................</td>");
                            b.Append("<tr>");
                        }

                    }
                }
            }
            b.Append("</table>");
            string t = b.ToString();
            return b.ToString();
        }
        private string GetHeaderHTML(string DepartmentName, DataSet ds,string IsNABL)
        {
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    h.Append("<table style='width:1200px;font-size:30px;font-family:calibri;text-align:left;background:#ececec;'>");
                    h.Append("<tr>");
                    h.Append("<td colspan='6' style='font-weight:bold;font-size:40px;text-align:center;width:100%;height:30px'>Patient Work Sheet (" + dr["IPOPType"].ToString() + ") </td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td colspan='6' style='font-weight:bold;font-size:40px;text-align:center;width:100%;height:10px'></td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Patient Name</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td><b>" + dr["patient_name"].ToString() + "</b></td>");
                    h.Append("<td><b>Age/Gender</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ageInfo"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>IPDNo</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ipop_no"].ToString() + "</td>");
                    h.Append("<td><b>Visit ID</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                    h.Append("<tr style='border:1px solid black;'>");
                    h.Append("<td><b>Ref Doctor</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ref_name"].ToString() + "</td>");
                    h.Append("<td><b>RegDate</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                    h.Append("</tr>");

                    h.Append("<tr style='border:1px solid black;'>");
                    h.Append("<td><b>BarCode</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td><b>" + _BarCodeNo + "</b></td>");
                    h.Append("<td><b>UHID</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["UHID"].ToString() + "</td>");
                    h.Append("</tr>");


                    h.Append("</table>");
                }

            }

            h.Append("<hr style='border:3px solid black;'/>");
            h.Append("<div style='text-align:center;margin-top:5px;font-size:30px;font-weight:bold;width:100%;'>" + DepartmentName + "</div>");
            return h.ToString();
        }
        private string GetFooterHTML(string DepartmentName, DataSet ds, bool IsLastPage)
        {
            string _result = string.Empty;
            StringBuilder f = new StringBuilder();
            f.Append("<table style='width:100%;text-align:right;font-size:22px;font-family:calibri;text-align:center;'>");
            f.Append("<tr>");
            f.Append("<td style='width:40%;text-align:left'>"+System.DateTime.Now.ToString("dd-MM-yyyy hh:mm tt")+"</td>");
            f.Append("<td style='width:30%;text-align:left'>Tech..........................</td>");
            f.Append("<td style='width:30%;text-align:left'>Sign..........................</td>");
            f.Append("</tr>");
            f.Append("</table>");
            return f.ToString();
        }
    }
  

}