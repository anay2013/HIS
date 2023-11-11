﻿using HiQPdf;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.OPD.Repository
{
    public class DoctorPrescription
    {
        dataSet dsResult = new dataSet();
        string _Deptname = string.Empty;
        string _IsNABL = string.Empty;
        string _PrintWithHeader = "N";
        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        public FileResult PrintPriscription(string app_no)
        {
            AppointmentQueries obj = new AppointmentQueries();
            obj.AppointmentId = app_no;
            obj.Logic = "OPD:Prescription";
            dsResult = APIProxy.CallWebApiMethod("Appointment/Opd_AppointmentQueries", obj);
            DataSet ds = dsResult.ResultSet;
            PdfDocument repDocument=new PdfDocument();
            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";
            PdfPage page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(15, 10, 10,50), PdfPageOrientation.Portrait);

            string HtmlBody = GetBodyHTML(dsResult.ResultSet);
            PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
            htmlBody.BrowserWidth = 780;
            //htmlBody.BrowserHeight= ;
            htmlBody.FontEmbedding = false;
            htmlBody.PageCreatingEvent += new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
            htmlBody.ImagesCutAllowed = false;
            page1.Layout(htmlBody);


            byte[] pdfdata = repDocument.WriteToMemory();
            FileResult fileResult = new FileContentResult(pdfdata, "application/pdf");
            return fileResult;
        }
        public void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage page1 = eventParams.PdfPage;

            string HtmlBody = "<div style='float:left;height:1300px;margin-top:200px; width:100%;border:1px solid #000;'></div>";
            PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
            page1.Layout(htmlBody);

            //Set Header
            SetHeader(page1);
            SetFooter(page1,false);
        }
        private void SetHeader(PdfPage pdfPage)
        {
            if(pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(190);
                string StrHTMLlHeader = GetHeaderHTML(dsResult.ResultSet);
                PdfHtml headerHtml = new PdfHtml(0,0,StrHTMLlHeader,null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage,bool IsLastPage)
        {
            if (pdfPage != null)
            {
                string StrhtmlFooter = string.Empty;
                pdfPage.CreateFooterCanvas(150);

                StrhtmlFooter = GetFooterHTML(dsResult.ResultSet);
                if (pdfPage.Footer != null)
                {
                    PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                    footerHtml.FitDestHeight = true;
                    footerHtml.FontEmbedding = true;

                    pdfPage.Footer.Layout(footerHtml);
                    Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                    PdfText pageNumberText;
                    pageNumberText = new PdfText(500,150, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    //pdfPage.Footer.Layout(pageNumberText);
                }
            }
        }
        private string GetBodyHTML(DataSet ds)
        {
            string _result = string.Empty;
            string temp = "-";
            StringBuilder b = new StringBuilder();
           // b.Append("<div style='float:left;width:100%;margin-left:-1px;'>");
            //Left Block Start
            b.Append("<div style='float:left;width:26%;position:relative;'>");
            if (ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
            {
                //Vital Sign
                b.Append("<div style='width:95%;margin-top:0px;font-size:13px;border-right:1px solid #000'>");
                b.Append("<p style='text-align:left;margin:0'><b>Vital Sign :</b></p>");
                b.Append("<table style='width:99%;font-size:11px;line-height:8px;margin-left:3px;'>");
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    b.Append("<tr>");
                    b.Append("<td>Weight</td>");
                    b.Append("<td>:</td>");
                    b.Append("<td>" + dr["WT"].ToString() + " kg</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Temprature</td>");
                    b.Append("<td>:</td>");
                    b.Append("<td>" + dr["Temprarture"].ToString() + " °C</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Pulse</td>");
                    b.Append("<td>:</td>");
                    b.Append("<td>" + dr["Pulse"].ToString() + " °C</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>B/P</td>");
                    b.Append("<td>:</td>");
                    b.Append("<td style='white-space: nowrap;'>" + dr["BP_Sys"].ToString() + " Mm/Hg</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>SPO2</td>");
                    b.Append("<td>:</td>");
                    b.Append("<td>" + dr["SPO2"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td>Height</td>");
                    b.Append("<td>:</td>");
                    b.Append("<td>" + dr["HT"].ToString() + " cm</td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
                b.Append("</div>");
            }
            //Declaration For Template Items
            var T00001 = string.Empty;
            var T00002 = string.Empty;
            var T00003 = string.Empty;
            var T00005 = string.Empty;
            var T00006 = string.Empty;
            var T00007 = string.Empty;
            var T00008 = string.Empty;
            var T00009 = string.Empty;
            var T00010 = string.Empty;
            var T00015 = string.Empty;
            var tbody = string.Empty;
            //Left Block End			
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (dr["TemplateId"].ToString() == "T00001")
                    {
                        T00001 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00002")
                    {
                        T00002 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00003")
                    {
                        T00003 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00005")
                    {
                        T00005 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00006")
                    {
                        T00006 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00007")
                    {
                        T00007 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00008")
                    {
                        T00008 += dr["ItemName"].ToString() + ",";
                    }
                    if (dr["TemplateId"].ToString() == "T00009")
                    {
                        T00009 += "<tr><td>" + dr["ItemName"].ToString() + "</td></tr>";
                    }
                    if (dr["TemplateId"].ToString() == "T00010")
                    {
                        T00010 += "<tr><td>" + dr["ItemName"].ToString() + "</td></tr>";
                    }
                    if (dr["TemplateId"].ToString() == "T00015")
                    {
                        T00015 += dr["ItemName"].ToString() + ",";
                    }
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                var count = 0;
                b.Append("<table style='width:99%;font-size:11px;line-height:12px;margin-left:3px;border:1px solid #000'>");
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    count++;
                    tbody += "<tr>";                                  
                    tbody += "<td style='padding-left:3px;border:1px solid #000'>" + count + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:12px;border:1px solid #000;'>" + dr["Item_name"].ToString() + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:12px;border:1px solid #000'>" + dr["med_dose"].ToString() + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:10px;border:1px solid #000;text-align:center'>" + dr["med_times"].ToString() + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:10px;border:1px solid #000;text-align:center'>" + dr["med_duration"].ToString() + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:10px;border:1px solid #000'>" + dr["med_intake"].ToString() + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:10px;border:1px solid #000;text-align:center'>" + dr["med_route"].ToString() + "</td>";
                    tbody += "<td style='padding-left:3px;font-size:8px;border:1px solid #000'>" + dr["remark"].ToString() + "</td>";
                    tbody += "</tr>";
                }
                b.Append("</table>");
            }
            if (!string.IsNullOrEmpty(T00009))
            {
                //Investigation
                b.Append("<div style='width:95%;margin:5px;font-size:13px;border-right:1px solid #000'>");
                b.Append("<p style='text-align:left;margin:0'><b>Investigation :</b></p>");
                b.Append("<table style='width:99%;font-size:11px;line-height:12px;margin-left:3px;'>");
                b.Append(T00009);
                b.Append("</table>");
                b.Append("</div>");
            }
            if (!string.IsNullOrEmpty(T00010))
            {
                //Procedure
                b.Append("<div style='width:95%;margin:5px;font-size:13px;border-right:1px solid #000'>");
                b.Append("<p style='text-align:left;margin:0'><b>Procedure :</b></p>");
                b.Append("<table style='width:99%;font-size:11px;line-height:12px;margin-left:3px;'>");
                b.Append(T00010);
                b.Append("</table>");
                b.Append("</div>");
            }
            b.Append("<div style='width:95%;margin:0 5px;font-size:12px;position:relative;bottom:0;border-right:1px solid #000'>");
            b.Append("<p style='text-align:center;margin:0;'>Note<hr style='width:90%;text-align:center;'/></p>");
            if (ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    b.Append("<div style='width:100%;font-size:15px;line-height:8px;margin-top:-10px;'>");
                    b.Append(dr["content"].ToString());
                    b.Append("</div>");
                }
            }
            b.Append("</div>");
            b.Append("</div>");
            //Right Block Start
            b.Append("<div style='float:right;width:73%;padding-top:5px;position:relative;'>");
            if (!string.IsNullOrEmpty(T00001))
            {
                //Provisional Diagnosis Begin
                b.Append("<div style='width:95%;font-size:13px;'>");
                b.Append("<p style='text-align:left;margin:0'><b>Provisional Diagnosis :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00001.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Provisional Diagnosis End	
            }
            if (!string.IsNullOrEmpty(T00002))
            {
                //Chief Complaint Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Chief Complaint :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00002.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Chief Complaint End
            }
            if (!string.IsNullOrEmpty(T00003))
            {
                //Sign & Symptoms Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Sign & Symptoms :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00003.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Sign & Symptoms End
            }
            if (!string.IsNullOrEmpty(tbody))
            {
                //Medicine Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                string rx =HttpContext.Current.Server.MapPath("/Content/logo/rx.png");
                b.Append("<p style='text-align:left;margin:0'><img src=" + rx + " style='width:15px;margin-bottom:-7px;' /></p>");
                b.Append("<table style='width:100%;float:left;font-size:11px;margin:10px 0;text-align:left;border-collapse: collapse;border:1px solid #000;'  >" +
                    "<tr>" +
                    "<th style='padding-left:3px;'>Sr</th><th style='padding-left:3px;'>Name</th><th style='padding-left:3px;'>Dose</th><th style='padding-left:3px;'>Times</th><th style='padding-left:3px;'>Duration</th><th style='padding-left:3px;'>Meal</th><th style='padding-left:3px;'>Route</th><th style='padding-left:3px;'>Remarks</th>" +
                    "</tr>" +
                    tbody +
                    "</table>");
                b.Append("</div>");
                //Medicine End
            }
            if (!string.IsNullOrEmpty(T00005))
            {

                //Doctor Notes Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Doctor Notes :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00005.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Doctor Notes End
            }
            if (!string.IsNullOrEmpty(T00006))
            {
                //Allergies Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Allergies :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00006.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Allergies End
            }
            if (!string.IsNullOrEmpty(T00015))
            {
                //History Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>History :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00015.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //History End
            }
            if (!string.IsNullOrEmpty(T00007))
            {
                //History Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Past Medication :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00007.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //History End
            }
            if (!string.IsNullOrEmpty(T00008))
            {
                //History Begin
                b.Append("<div style='width:95%;font-size:13px;'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Doctor Advice :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00008.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //History End
            }
            if (ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    //Doctor Details Bottom Begin
                    b.Append("<div style='width:95%;position:absolute;bottom:0'>");
                    b.Append("<h3 style='text-align:right;margin:0;padding:0'><img style='float:right' src='" + dr["SignVirtualPath"].ToString() + "'/><br/><br/><br/>" + dr["DoctorName"].ToString() + "</h3>");
                    b.Append("<h4 style='text-align:right;margin:0 10px 10px'>" + dr["degree"].ToString() + "</h4>");
                    b.Append("</div>");
                    //Doctor Details Bottom End
                }
            }
            b.Append("</div>"); //Right Block End
            b.Append("</div>");
            //b.Append("</div>");
            return b.ToString();
        }
        private string GetHeaderHTML(DataSet ds)
        {
            _PrintWithHeader = "Y";
            StringBuilder b = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<table style='width:1200px;border-top:1px solid #000;border-bottom:1px solid #000; font-size:25px;float:left;margin-top:200px;padding:8px;font-family:calibri'>");
                    b.Append("<tr>");
                    b.Append("<td><b>Patient Name</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["patient_name"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td><b>UHID No.</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["UHID"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Age/Gender</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["Age"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td><b>Appointment Date</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["AppDate"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Doctor</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["DoctorName"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td><b>Appointment Type</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["appType"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Mobile No.</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["mobile_no"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td><b>Valid To</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["AppDate"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Panel</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["PanelName"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td><b>Barcode</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td><img src=" + BarcodeGenerator.GenerateBarCode(dr["UHID"].ToString(), 225, 25) + " style='width:150px;height:100%'/></td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                }
            }
            return b.ToString();
        }
        private string GetFooterHTML(DataSet ds)
        {
            _PrintWithHeader = "Y";
            StringBuilder b = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<table style='width:1500px;height:100px;font-size:25px;float:left;margin-top:200px;padding:8px;font-family:calibri'>");
                    b.Append("<tr>");
                    b.Append("<td style='width:50%'></td>");
                    b.Append("<td style='width:50%'></td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                }
            }
            return b.ToString();
        }
    }
    public class ipPageCounter
    {
        public string DeptName { get; set; }
        public int PageIndex { get; set; }
        public bool IsLastPage { get; set; }
    }

}