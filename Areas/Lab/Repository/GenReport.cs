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
    public class GenReport
    {
        dataSet dsResult = new dataSet();
        string _Deptname = string.Empty;
        string _IsNABL = string.Empty;
        string _PrintWithHeader = "N";
        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        public FileResult PrintLabReport(string visitNo, string SubCat,string TestIds,string Logic)
        {
            if (Logic =="ByReportEditing")
            {
                LabReporting obj = new LabReporting();
                obj.LabCode = "-";
                obj.IpOpType = "-";
                obj.ReportStatus = "-";
                obj.SubCat = SubCat;
                obj.VisitNo = visitNo;
                obj.BarccodeNo = "-";
                obj.TestCategory = "-";
                obj.AutoTestId = 0;
                obj.TestCode = "-";
                obj.from = Convert.ToDateTime("1900/01/01");
                obj.to = Convert.ToDateTime("1900/01/01");
                obj.Logic = "PrintLabReport";
                dsResult = APIProxy.CallWebApiMethod("sample/LabReporting_Queries", obj);
            }
            if (Logic == "ByFinalPrint")
            {
                ReportPrintingInfo obj = new ReportPrintingInfo();
                obj.LabCode = "-";
                obj.PanelId = "-";
                obj.DoctorId = "-";
                obj.VisitNo = visitNo;
                obj.TestCategory = "-";
                obj.TestIds = TestIds;
                obj.from = "1900/01/01";
                obj.to = "1900/01/01";
                obj.Prm1 = "-";
                obj.Prm2 = "-";
                obj.Logic = "PrintLabReport";
                dsResult = APIProxy.CallWebApiMethod("Lab/Lab_ReportPrintingQueries", obj);
            }
            PdfDocument repDocument=new PdfDocument();
            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";
            //Geting Distinct department list with NABL Flag to separate the report body for NABLE Logo at header
            var DeptList = dsResult.ResultSet.Tables[2].AsEnumerable().Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName"),
                IsNABL = y.Field<string>("IsNABL"),
                SeqNo = y.Field<Int64>("SeqNo"),
                RowNo = y.Field<Int64>("RowNo"),
            }).ToList().OrderBy(y =>y.SeqNo).GroupBy(x => new { x.SubCatName, x.IsNABL});
            foreach (var dept in DeptList)
            {
                _Deptname = dept.First().SubCatName;
                _IsNABL= dept.First().IsNABL;
                PdfPage page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(15, 10, 10, 30), PdfPageOrientation.Portrait);
                string HtmlBody = string.Empty;
                HtmlBody = GetBodyHTML(_Deptname, dsResult.ResultSet,_IsNABL);
                Int64 LastRow = DeptList.Last().First().RowNo;
                //  Manging below Line /**** End Of Report ***/ at the last line in whole report  
                if(LastRow == dept.First().RowNo)
                {
                    StringBuilder seo = new StringBuilder();
                    seo.Append("<table style='width:100%;text-align:right;font-family:calibri;text-align:center;border: 1px solid black;'>");
                    seo.Append("<tr>");
                    seo.Append("<td colspan='6' style='text-align:center;padding-left:4px;font-size:18px'><b>*** End Of Report ***</b></td>");
                    seo.Append("</tr>");
                    seo.Append("</table>");

                    string QrCodeImgBase64 = ZxIngQRCode.GenerateMyQCCode("https://chandanhospital.in/");
                    string srcstr = "data:image/jpeg;base64," + QrCodeImgBase64 + "";

                    seo.Append("<table style='width:100%;text-align:right;font-family:calibri;text-align:center;'>");
                    seo.Append("<tr>");
                    seo.Append("<td colspan='6' style='text-align:center;'><img src='" + srcstr + "' /></td>");
                    seo.Append("</tr>");
                    seo.Append("</table>");

                    HtmlBody = HtmlBody + "" + seo.ToString();
                }

                PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
                htmlBody.BrowserWidth = 780;
                // htmlBody.RenderWebFonts = true;
                // htmlBody.FitDestHeight = true;
                htmlBody.FontEmbedding = false;
                htmlBody.PageCreatingEvent += new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
                htmlBody.ImagesCutAllowed = false;
                page1.Layout(htmlBody);
            }
            //Seting Footer at the last page of every Department 
            StringBuilder seor = new StringBuilder();
            var lastPageList = from s in pgCounterList
                               group s by s.DeptName into stugrp
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
        public void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage page1 = eventParams.PdfPage;
            //Set Header
            SetHeader(page1);
            ipPageCounter ipc = new ipPageCounter();
            ipc.DeptName = _Deptname;
            ipc.PageIndex = page1.Index;
            pgCounterList.Add(ipc);

            SetFooter(page1, "Blank", "N/R", false);
        }
        private void SetHeader(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(200);
                string StrhtmlHeader = GetHeaderHTML(_Deptname, dsResult.ResultSet,_IsNABL);
                PdfHtml headerHtml = new PdfHtml(0, 0, StrhtmlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage, string FooterType, string DepartmentName, bool IsLastPage)
        {
            if (pdfPage != null)
            {
                string StrhtmlFooter = string.Empty;
                if (FooterType == "Blank")
                {
                    pdfPage.CreateFooterCanvas(120);
                    StrhtmlFooter = "";
                }
                else
                {
                    if (!IsLastPage)
                        pdfPage.CreateFooterCanvas(120);
                    else
                        pdfPage.CreateFooterCanvas(140);

                    StrhtmlFooter = GetFooterHTML(DepartmentName, dsResult.ResultSet, IsLastPage);
                }
                if (pdfPage.Footer != null)
                {
                    PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                    footerHtml.FitDestHeight = true;
                    footerHtml.FontEmbedding = true;

                    pdfPage.Footer.Layout(footerHtml);
                    Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                    PdfText pageNumberText;
                    if (!IsLastPage)
                        pageNumberText = new PdfText(500, 80, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    else
                        pageNumberText = new PdfText(500, 105, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    pdfPage.Footer.Layout(pageNumberText);
                }
            }
        }
        private string GetBodyHTML(string DepartmentName, DataSet ds,string IsNABL)
        {
            string _result = string.Empty;
            string temp = "-";
            StringBuilder b = new StringBuilder();
            var ObsTest = ds.Tables[3].AsEnumerable().Where(z => z.Field<string>("SubCatName") == DepartmentName && z.Field<string>("IsNABL") == IsNABL).Select(y => new
            {
                AutoTestId = y.Field<Int64>("AutoTestId"),
                ObsCount = y.Field<Int64>("ObsCount"),
                TestSeqNo = y.Field<Int64>("TestSeqNo"),
                TestName = y.Field<string>("TestName"),
                report_content = y.Field<string>("report_content"),
                r_type = y.Field<string>("r_type"),
                samp_type = y.Field<string>("samp_type"),
                Interpretation = y.Field<string>("Interpretation"),
                AddedReportPath= y.Field<string>("AddedReportPath")
            }).OrderBy(y => y.TestSeqNo).ToList();
            if (ObsTest.Count > 0)
            {
                var tempCatName = "";
                foreach (var dr in ObsTest)
                {
                    if (tempCatName != dr.r_type)
                    {
                        if (dr.r_type != "Text")
                        {
                            b.Append("<table border='0' style='width:100%;font-size:15px; border-collapse: collapse;margin-top:-10px;'>");
                            b.Append("<tr>");
                            b.Append("<th style='width:35%;text-align:left;padding-left:4px;'>Test Name</th>");
                            b.Append("<th style='width:18%;text-align:left;padding-right:4px;'>Result</th>");
                            b.Append("<th style='width:2%;text-align:left;padding-right:4px;'></th>");
                            b.Append("<th style='width:10%;text-align:left;padding-right:4px;'>Unit</th>");
                            b.Append("<th style='width:25%;text-align:left;padding-right:4px;'>Bio. Ref. Interval</th>");
                            b.Append("<th style='width:10%;text-align:left;padding-right:4px;'>Method</th>");
                            b.Append("</tr>");
                        }
                        tempCatName = dr.r_type;
                    }
                    Int64 AutoTestId = dr.AutoTestId;
                    Int64 ObsCount = dr.ObsCount;
                    var ObsDetail = ds.Tables[4].AsEnumerable().Where(x => x.Field<Int64>("AutoTestId") == AutoTestId).Select(y => new
                    {
                        ObservationName = y.Field<string>("ObservationName"),
                        reading = y.Field<string>("reading"),
                        ab_flag = y.Field<string>("ab_flag"),
                        result_unit = y.Field<string>("result_unit"),
                        RefRange = y.Field<string>("RefRange"),
                        ObsSeqNo = y.Field<Int64>("ObsSeqNo"),
                        sample_type = y.Field<string>("sample_type"),
                        HeaderName = y.Field<string>("HeaderName"),
                        method_name = y.Field<string>("method_name"),
                        test_comment = y.Field<string>("test_comment")

                    }).ToList();
                    if(ObsCount==1)
                    {
                        foreach (var obj1 in ObsDetail)
                        {
                            b.Append("<tr>");
                            b.Append("<td style='width:35%;text-align:left;padding-left:4px;'><b>" + obj1.ObservationName + "</b>, <span style='font-size:12px'>" + dr.samp_type + "</span>" + "</td>");
                            if (obj1.ab_flag == "L" || obj1.ab_flag == "H")
                                b.Append("<td style='width:18%;text-align:left;padding-left:4px;'><b>" + obj1.reading + "</b></td>");
                            else
                                b.Append("<td style='width:18%;text-align:left;padding-left:4px;'>" + obj1.reading + "</td>");
                            b.Append("<td style='width:2%;text-align:left;padding-left:4px;font-size:12px;margin-right:5px'>" + obj1.ab_flag + "</td>");
                            b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:12px'>" + obj1.result_unit + "</td>");
                            b.Append("<td style='width:25%;text-align:left;padding-left:4px;font-size:12px'>" + obj1.RefRange + "</td>");
                            b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:11px'>" + obj1.method_name + "</td>");
                            b.Append("</tr>");
                            if (!string.IsNullOrEmpty(obj1.test_comment))
                            {
                                b.Append("<tr>");
                                b.Append("<td colspan='6'>" + obj1.test_comment + "</td>");
                                b.Append("</tr>");
                            }
                            //Code To Add Pdf as Image after conversion
                            if (dr.AddedReportPath.Length > 5)
                            {
                                PDFUtility pdfUtility = new PDFUtility();
                                string ImagePageList = pdfUtility.ConvertPdfToImageTags(dr.AddedReportPath);
                                b.Append("<tr>");
                                b.Append("<td colspan='6'><br/>" + ImagePageList + "</td>");
                                b.Append("</tr>");
                            }
                            if (!string.IsNullOrEmpty(dr.Interpretation))
                            {
                                b.Append("<tr>");
                                b.Append("<td colspan='6'><b style='font-size:12px'>Test Interpretation : </b></td>");
                                b.Append("</tr>");
                                b.Append("<tr>");
                                b.Append("<td style='font-size:12px !important' colspan='6'>" + dr.Interpretation + "</td>");
                                b.Append("</tr>");
                            }
                        }
                    }
                    else
                    {
                        b.Append("<tr>");
                        b.Append("<td colspan='6' style='padding-left:10px;line-height:10px'></td>");
                        b.Append("</tr>");
                        b.Append("<tr>");
                        b.Append("<td colspan='6' style='background:#ddd;text-align:left;padding-left:4px;'><b>" + dr.TestName + " </b>, <span style='font-size:11px'>" + dr.samp_type + "</span>" + "</td>");
                        b.Append("</tr>");
                        foreach (var obj1 in ObsDetail)
                        {
                            if (temp != obj1.HeaderName)
                            {
                                b.Append("<tr>");
                                b.Append("<td colspan='6' style='font-size:14px;text-decoration:underline;padding-left:4px;'><b>" + obj1.HeaderName + "</b></td>");
                                b.Append("</tr>");
                                temp = obj1.HeaderName;
                            }
                            b.Append("<tr>");
                            b.Append("<td style='width:35%;text-align:left;padding-left:4px;'>" + obj1.ObservationName + "</td>");
                            if (obj1.ab_flag == "L" || obj1.ab_flag == "H")
                                b.Append("<td style='width:18%;text-align:left;padding-left:4px;'><b>" + obj1.reading + "</b></td>");
                            else
                                b.Append("<td style='width:18%;text-align:left;padding-left:4px;'>" + obj1.reading + "</td>");

                            b.Append("<td style='width:2%;text-align:left;padding-left:4px;font-size:12px;margin-right:5px'>" + obj1.ab_flag + "</td>");
                            b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:12px'>" + obj1.result_unit + "</td>");
                            b.Append("<td style='width:25%;text-align:left;padding-left:4px;font-size:12px'>" + obj1.RefRange + "</td>");
                            b.Append("<td style='width:10%;text-align:left;padding-left:4px;font-size:11px'>" + obj1.method_name + "</td>");
                            b.Append("</tr>");
                            if (!string.IsNullOrEmpty(obj1.test_comment))
                            {
                                b.Append("<tr>");
                                b.Append("<td colspan='6'>" + obj1.test_comment + "</td>");
                                b.Append("</tr>");
                            }
                        }
                        
                        //Code To Add Pdf as Image after conversion
                        if (dr.AddedReportPath.Length > 5)
                        {
                            PDFUtility pdfUtility = new PDFUtility();
                            string ImagePageList = pdfUtility.ConvertPdfToImageTags(dr.AddedReportPath);
                            b.Append("<tr>");
                            b.Append("<td colspan='6'><br/>" + ImagePageList + "</td>");
                            b.Append("</tr>");
                        }

                        if (ObsDetail.Count > 1)
                        {
                            if (!string.IsNullOrEmpty(dr.Interpretation))
                            {
                                b.Append("<tr>");
                                b.Append("<td colspan='6'><b style='font-size:12px'>Test Interpretation : </b></td>");
                                b.Append("</tr>");

                                b.Append("<tr>");
                                b.Append("<td style='font-size:12px !important' colspan='6'>" + dr.Interpretation + "</td>");
                                b.Append("</tr>");
                            }
                            b.Append("<tr>");
                            b.Append("<td colspan='6'><hr/></td>");
                            b.Append("</tr>");
                        }
                    }
                    if (dr.r_type == "Text")
                    {
                        b.Append("<tr>");
                        b.Append("<td colspan='6' style='text-align:left;padding-left:4px;'>" + dr.TestName + "</td>");
                        b.Append("</tr>");

                        b.Append("<tr>");
                        b.Append("<td colspan='6' style='text-align:left;padding-left:4px;'>" + dr.report_content + "</td>");
                        b.Append("</tr>");
                    }
                }
            }
            b.Append("</table>");
            string t = b.ToString();
            return b.ToString();
        }
        private string GetHeaderHTML(string DepartmentName, DataSet ds,string IsNABL)
        {
            _PrintWithHeader = "Y";
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    h.Append("<table style='width:1200px;height:190px; font-size:25px;float:left;margin-top:-12px;padding:8px;font-family:calibri'>");
                    h.Append("<tr>");

                    h.Append("<td style='width:20%'>");
                    if(_PrintWithHeader == "Y")
                    {
                        string ChandanLogo = System.Web.HttpContext.Current.Server.MapPath(@"/images/ChandanLogo.jpg");
                        h.Append("<img src=" + ChandanLogo + " style='width:180px;height:100px' />");
                    }
                    h.Append("</td>");

                    h.Append("<td style='width:55%'>");
                    h.Append("<div style='text-align:left;width:auto;float:left;width:100%;'>");
                    h.Append("<h2 style='font-weight:bold;margin:0'>" + dr["Hospital_Name"].ToString() + "</h2>");
                    h.Append("<span style='text-align:left;'>" + dr["Full_Address"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'><b>Landline No : </b>" + dr["LandlineNo"].ToString() + "</span><br/>");
                    //b.Append("<span style='text-align:left;'><b>Email ID : </b>" + dr["EmailID"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'><b>CIN No: " + dr["cin_no"].ToString() + "</b></span><br/>");
                    h.Append("</div>");
                    h.Append("</td>");
                    h.Append("<td style='width:10%;text-align:left'>");
                    if (_IsNABL == "Y")
                    {
                        string NABLLogo = System.Web.HttpContext.Current.Server.MapPath(@"/images/NABL.jpg");
                        h.Append("<img src=" + NABLLogo + " style='width:120px;height:120px' />");
                    }
                    h.Append("</td>");

                    h.Append("<td style='width:15%'>");
                    if (_PrintWithHeader == "Y")
                    {
                        string YearCompleted = System.Web.HttpContext.Current.Server.MapPath(@"/images/sinceYear.jpg");
                        h.Append("<img src=" + YearCompleted + " style='width:120px;height:120px' />");
                    }
                    h.Append("</td>");
          
                    h.Append("</table>");
                    h.Append("<hr/>");

                    /*

                    h.Append("<div style='width:1200px;height:160px; font-size:25px;float:left;margin-top:-12px;padding:8px;font-family:calibri'>");
                    string chandanLogo = "~/images/NABL.jpg"; // HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    h.Append("<div style='text-align:left;width:32%;float:left'>");
                    h.Append("<img src=" + chandanLogo + " style='width:180px;margin-top:5px;' />");
                    h.Append("</div>");
                    h.Append("<div style='text-align:left;width:auto;float:left;width:43%;'>");
                    h.Append("<h2 style='font-weight:bold;margin:0'>" + dr["Hospital_Name"].ToString() + "</h2>");
                    h.Append("<span style='text-align:left;'>" + dr["Full_Address"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'><b>Landline No : </b>" + dr["LandlineNo"].ToString() + "</span><br/>");
                    //b.Append("<span style='text-align:left;'><b>Email ID : </b>" + dr["EmailID"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'><b>CIN No: " + dr["cin_no"].ToString() + "</b></span><br/>");
                    h.Append("</div>");
                    h.Append("<div style='text-align:left;width:25%;float:left'>");
                    if(_IsNABL == "Y")
                    {
                        string NABLLogo = System.Web.HttpContext.Current.Server.MapPath(@"/images/NABL.jpg");
                        h.Append("<img src=" + NABLLogo + " style='width:120px;height:120px' />");
                    }
                    h.Append("</div>");
                    h.Append("</div>");
                    h.Append("<hr/>");
                    */
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                //DataRow[] drList = ds.Tables[1].Select("SubCatName = " + DepartmentName);
                DataRow[] drList = ds.Tables[1].Select(string.Format("SubCatName ='{0}' ", DepartmentName));
                foreach (DataRow dr in drList)
                {
                    h.Append("<table style='width:1200px;font-size:22px;font-family:calibri;text-align:left;background:#ececec;'>");
                    h.Append("<tr>");
                    h.Append("<td><b>Patient Name</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td><b>" + dr["patient_name"].ToString() + "</b></td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Registered On</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["RegDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Age/Gender</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ageInfo"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Collected</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["sample_collect_date"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>UHID/MR NO</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["UHID"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Received</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["LabReceivedDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Visit ID</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Reported</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ApprovedDate"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("<tr>");
                    h.Append("<td><b>Ref Doctor</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["ref_name"].ToString() + "</td>");
                    h.Append("<td colspan='4'>&nbsp;</td>");
                    h.Append("<td><b>Status</b></td>");
                    h.Append("<td><b>:</b></td>");
                    h.Append("<td>" + dr["VisitNo"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("</table>");
                }

            }

            h.Append("<hr/>");
            h.Append("<div style='text-align:center;margin-top:5px;font-size:30px;font-weight:bold;width:100%;'>" + DepartmentName + " REPORT</div>");
            return h.ToString();
        }
        private string GetFooterHTML(string DepartmentName, DataSet ds, bool IsLastPage)
        {
            string _result = string.Empty;
            string temp = "-";
            StringBuilder f = new StringBuilder();
            var SignInfo = ds.Tables[2].AsEnumerable().Where(z => z.Field<string>("SubCatName") == DepartmentName).Select(y => new
            {
                SubCatName = y.Field<string>("SubCatName"),
                ReportApprDoctor = y.Field<string>("ReportApprDoctor"),
                degree = y.Field<string>("degree"),
                SignVirtualPath = y.Field<string>("SignVirtualPath"),
            }).ToList().GroupBy(x => new { x.SubCatName, x.ReportApprDoctor, x.degree, x.SignVirtualPath });
            f.Append("<div style='width:100%;float:right;text-align:right;margin-bottom:5px'>");
            f.Append("<table style='width:100%;text-align:right;font-size:22px;font-family:calibri;text-align:center;'>");
            f.Append("<tr>");
            foreach (var obj1 in SignInfo)
            {
                if(SignInfo.ToList().Count == 1)
                {
                    f.Append("<td style='width:400px;'></td>");
                    f.Append("<td style='width:400px;'></td>");
                    f.Append("<td style='width:400px;'>");
                    f.Append("<img src='http://192.168.0.11/HospDoc/" + obj1.Key.SignVirtualPath + "' style='height:60px'><br>");
                    f.Append("Dr. " + obj1.Key.ReportApprDoctor + "<br>" + obj1.Key.degree + "");
                    f.Append("</td>");
                }
                else
                {
                    f.Append("<td style='width:400px;'>");
                    f.Append("<img src='http://192.168.0.11/HospDoc/" + obj1.Key.SignVirtualPath + "' style='height:60px'><br>");
                    f.Append("Dr. " + obj1.Key.ReportApprDoctor + "<br>" + obj1.Key.degree + "");
                    f.Append("</td>");
                }

            }
            if (IsLastPage)
            {
                f.Append("<table style='width:100%;text-align:right;font-family:calibri;text-align:center;border: 1px solid black;'>");
                f.Append("<tr>");
                string Note = @"Facilities: Pathology, Bedside Sample Collection, Health Check-ups, Digital X-Ray, ECG (Bedside also), Allergy Testing, Test And Health Check-ups, Ultrasonography, Sonomammography,
Bone Mineral Density(BMD), Doppler Studies, 2D Echo, CT Scan, MRI, Blood Bank, TMT, EEG, PFT, OPG, Endoscopy, Digital Mammography, Electromyography (EMG), Nerve Condition
Velocity(NCV), Audiometry, Brainstem Evoked Response Audiometry(BERA), Colonoscopy, Ambulance Services, Online Booking Facilities for Diagnostics, Online Report Viewing * ";

                f.Append("<tr>");
                f.Append("<td colspan='3' style='font-size:16px'>" + Note + " </td>");
                f.Append("</tr>");
                f.Append("</table>");
            }

            f.Append("</tr>");
            f.Append("</table>");
            return f.ToString();
        }
    }
    public class ipPageCounter
    {
        public string DeptName { get; set; }
        public int PageIndex { get; set; }
        public bool IsLastPage { get; set; }
    }

}