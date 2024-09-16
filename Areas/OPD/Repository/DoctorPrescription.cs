using HiQPdf;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        string NextFollowUpDate = "-";
        List<ipPageCounter> pgCounterList = new List<ipPageCounter>();
        public FileResult PrintPriscription(string app_no)
        {
            AppointmentQueries obj = new AppointmentQueries();
            obj.AppointmentId = app_no;
            obj.Logic = "OPD:Prescription";
            dsResult = APIProxy.CallWebApiMethod("Appointment/Opd_AppointmentQueries", obj);
            DataSet ds = dsResult.ResultSet;
            PdfDocument repDocument = new PdfDocument();
            repDocument.SerialNumber = "PXVUbG1Z-W3FUX09c-T0QMCBMN-HQwdDh0M-HQ4MEwwP-EwQEBAQ=";
            PdfPage page1 = repDocument.AddPage(PdfPageSize.A4, new PdfDocumentMargins(40, 10, 10, 70), PdfPageOrientation.Portrait);

            string HtmlBody = GetBodyHTML(dsResult.ResultSet);
            PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
            htmlBody.FitDestWidth = true;
            htmlBody.BrowserWidth = 780;
            htmlBody.ForceFitDestWidth = true;
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

            string HtmlBody = "<div style='float:left;width:1%;border-left: 1px solid #000;height:1130;margin-left:25.2%;margin-top:393px;position:absolute'></div><div style='float:left;height:1330;margin-top:200px;width:100%;border-top:1px solid #000;border-left:1px solid #000;border-right:1px solid #000;'></div>";
            PdfHtml htmlBody = new PdfHtml(HtmlBody, null);
            page1.Layout(htmlBody);

            //Set Header
            SetHeader(page1);
            SetFooter(page1, false);
        }
        private void SetHeader(PdfPage pdfPage)
        {
            if (pdfPage != null)
            {
                pdfPage.CreateHeaderCanvas(190);
                string StrHTMLlHeader = GetHeaderHTML(dsResult.ResultSet);
                PdfHtml headerHtml = new PdfHtml(StrHTMLlHeader, null);
                pdfPage.Header.Layout(headerHtml);
            }
        }
        private void SetFooter(PdfPage pdfPage, bool IsLastPage)
        {
            if (pdfPage != null)
            {
                string StrhtmlFooter = string.Empty;
                pdfPage.CreateFooterCanvas(70);

                StrhtmlFooter = GetFooterHTML(dsResult.ResultSet);
                if (pdfPage.Footer != null)
                {
                    PdfHtml footerHtml = new PdfHtml(0, 0, StrhtmlFooter, null);
                    footerHtml.FitDestWidth = true;
                    //footerHtml.BrowserWidth = 780;
                    footerHtml.ForceFitDestWidth = true;
                    footerHtml.FontEmbedding = false;
                    //footerHtml.FitDestWidth = true;
                    //footerHtml.BrowserWidth = 780;                                      
                    //footerHtml.FontEmbedding = true;

                    pdfPage.Footer.Layout(footerHtml);
                    Font pageNumberFont = new Font(new FontFamily("Arial"), 8, GraphicsUnit.Point); // 1
                    PdfText pageNumberText;
                    pageNumberText = new PdfText(500, 150, "Page {CrtPage} of {PageCount}", pageNumberFont); // 2
                    pdfPage.Footer.Layout(pageNumberText);
                }
            }
        }
        private string GetBodyHTML(DataSet ds)
        {
            string _result = string.Empty;
            string temp = "-";
            StringBuilder b = new StringBuilder();

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
            // b.Append("<div style='float:left;width:100%;margin-left:-1px;'>");
            //Left Block Start 
            // b.Append("<div style='float:left;width:26%;height:67%;border-right:1px solid #000'>");
            b.Append("<div style='float:left;width:26%;height:600px;z-index:9999;font-family:Calibri'>");
            if (!string.IsNullOrEmpty(T00006))
            {
                //Allergies Begin
                b.Append("<div style='width:95%;font-size:13px;margin-left:3px;'>");
                b.Append("<p style='text-align:left;margin:0'><b>Allergies :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00006.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Allergies End
            }
            if (ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
            {
                int VitalCount = 0;
                //Vital Sign

                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    if (Convert.ToDecimal(dr["WT"]) > 0 || Convert.ToDecimal(dr["Temprarture"]) > 0 || Convert.ToDecimal(dr["Pulse"]) > 0 || Convert.ToDecimal(dr["BP_Sys"]) > 0 || Convert.ToDecimal(dr["SPO2"]) > 0 || Convert.ToDecimal(dr["HT"]) > 0)
                    {
                        b.Append("<div style='width:95%;margin-top:5px;font-size:13px;margin-left:3px;'>");
                        b.Append("<p style='text-align:left;margin:0'><b>Vital Sign :</b></p>");
                        b.Append("<table style='width:99%;font-size:11px;line-height:8px;margin-left:3px;font-family:Calibri'>");

                        if (Convert.ToDecimal(dr["WT"]) > 0)
                        {
                            b.Append("<tr>");
                            b.Append("<td>Weight</td>");
                            b.Append("<td>:</td>");
                            b.Append("<td>" + dr["WT"].ToString() + " Kg</td>");
                            b.Append("</tr>");
                        }
                        if (Convert.ToDecimal(dr["Temprarture"]) > 0)
                        {
                            b.Append("<tr>");
                            b.Append("<td>Temprature</td>");
                            b.Append("<td>:</td>");
                            b.Append("<td>" + dr["Temprarture"].ToString() + "</td>");
                            b.Append("</tr>");
                        }
                        if (Convert.ToDecimal(dr["Pulse"]) > 0)
                        {
                            b.Append("<tr>");
                            b.Append("<td>Pulse</td>");
                            b.Append("<td>:</td>");
                            b.Append("<td>" + dr["Pulse"].ToString() + "</td>");
                            b.Append("</tr>");
                        }
                        if (dr["BP_Sys"].ToString().Length > 0 || dr["BP_Dys"].ToString().Length > 0)
                        {
                            b.Append("<tr>");
                            b.Append("<td>B/P</td>");
                            b.Append("<td>:</td>");
                            b.Append("<td style='white-space: nowrap;'>" + dr["BP_Sys"].ToString() + "/" + dr["BP_Dys"].ToString() + " Mm/Hg</td>");
                            b.Append("</tr>");
                        }
                        if (Convert.ToDecimal(dr["SPO2"]) > 0)
                        {
                            b.Append("<tr>");
                            b.Append("<td>SPO2</td>");
                            b.Append("<td>:</td>");
                            b.Append("<td>" + dr["SPO2"].ToString() + "</td>");
                            b.Append("</tr>");
                        }
                        if (Convert.ToDecimal(dr["HT"]) > 0)
                        {
                            b.Append("<tr>");
                            b.Append("<td>Height</td>");
                            b.Append("<td>:</td>");
                            b.Append("<td>" + dr["HT"].ToString() + " cm</td>");
                            b.Append("</tr>");
                        }
                        b.Append("</table>");
                        b.Append("</div>");
                    }
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                var count = 0;
                // b.Append("<table style='width:99%;font-size:11px;line-height:12px;margin-left:3px;border:1px solid #000'>");
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
                    tbody += "<td style='padding-left:3px;font-size:10px;border:1px solid #000'>" + dr["remark"].ToString() + "</td>";
                    tbody += "</tr>";
                }
                //  b.Append("</table>");
            }
            if (!string.IsNullOrEmpty(T00010))
            {
                //Procedure
                b.Append("<div style='width:95%;margin:5px;font-size:13px;font-family:Calibri'>");
                b.Append("<p style='text-align:left;margin:0'><b>Procedure :</b></p>");
                b.Append("<table style='width:99%;font-size:11px;line-height:12px;margin-left:3px;font-family:Calibri'>");
                b.Append(T00010);
                b.Append("</table>");
                b.Append("</div>");
            }
            if (!string.IsNullOrEmpty(T00009))
            {
                //Investigation
                b.Append("<div style='width:95%;margin:5px;font-size:13px;font-family:Calibri'>");
                b.Append("<p style='text-align:left;margin:-2px'><b>Investigation :</b></p>");
                b.Append("<table style='width:99%;font-size:11px;line-height:12px;margin-left:3px;font-family:Calibri'>");
                b.Append(T00009);
                b.Append("</table>");
                b.Append("</div>");
            }

            b.Append("<div style='width:95%;margin:0 5px;font-size:12px;position:relative;bottom:0;font-family:Calibri'>");

            if (ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    if (dr["IsFollowUp"].ToString() != "-")
                        NextFollowUpDate = dr["NextFollowUpDate"].ToString();
                    else
                        NextFollowUpDate = "-";

                    b.Append("<p style='text-align:center;margin:0;'><b>Note</b><hr style='width:90%;text-align:center;'/></p>");
                    b.Append("<div style='width:100%;font-size:15px;line-height:8px;margin-top:-10px;font-family:Calibri'>");
                    b.Append(dr["content"].ToString());
                    b.Append("</div>");
                }
            }
            b.Append("</div>");
            b.Append("</div>");
            //Right Block Start
            // b.Append("<div style='float:left;width:1%;border-left: 1px solid #000;height:650px;margin:-8px'></div>");
            b.Append("<div style='float:right;width:72%;height:620px;font-family:Calibri'>");
            if (!string.IsNullOrEmpty(T00002))
            {
                //Chief Complaint Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'>");
                b.Append("<p style='text-align:left;margin:0'><b>Chief Complaint :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00002.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Chief Complaint End
            }
            if (!string.IsNullOrEmpty(T00007))
            {
                //History Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Past Medication :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00007.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //History End
            }
            if (!string.IsNullOrEmpty(T00001))
            {
                //Provisional Diagnosis Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Provisional Diagnosis :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00001.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Provisional Diagnosis End	
            }
            if (!string.IsNullOrEmpty(tbody))
            {
                //Medicine Begin
                b.Append("<div style='width:95%;'>");
                string rx = HttpContext.Current.Server.MapPath("~/Content/logo/rx.png");
                b.Append("<p style='text-align:left;margin:0'><img src=" + rx + " style='width:15px;margin-bottom:-7px;' /></p>");
                b.Append("<table style='width:100%;float:left;font-size:11px;margin:10px 0;text-align:left;border-collapse: collapse;border:1px solid #000;font-family:Calibri'>");
                b.Append("<tr>");
                b.Append("<th style='padding-left:3px;'>Sr</th>");
                b.Append("<th style='padding-left:3px;'>Name</th>");
                b.Append("<th style='padding-left:3px;'>Dose</th>");
                b.Append("<th style='padding-left:3px;'>Times</th>");
                b.Append("<th style='padding-left:3px;'>Duration</th>");
                b.Append("<th style='padding-left:3px;'>Meal</th>");
                b.Append("<th style='padding-left:3px;'>Route</th>");
                b.Append("<th style='padding-left:3px;'>Remark</th>");
                b.Append("</tr>");
                b.Append(tbody);
                b.Append("</table>");
                b.Append("</div>");

                //b.Append("<table style='width:100%;float:left;font-size:11px;margin:10px 0;text-align:left;border-collapse: collapse;border:1px solid #000;'>" +
                //    "<tr>" +
                //    "<th style='padding-left:3px;'>Sr</th><th style='padding-left:3px;'>Name</th><th style='padding-left:3px;'>Dose</th><th style='padding-left:3px;'>Times</th><th style='padding-left:3px;'>Duration</th><th style='padding-left:3px;'>Meal</th><th style='padding-left:3px;'>Route</th><th style='padding-left:3px;'>Remarks</th>" +
                //    "</tr>" +
                //    tbody +
                //    "</table>");
                //b.Append("</div>");
                //Medicine End
            }
            if (NextFollowUpDate != "-")
            {
                //NextFollowUpDate Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Next Follow Up Date And Remark :</b></p>");
                b.Append("<span style='font-size:15px;'>" + NextFollowUpDate + "</span>");
                b.Append("</div>");
                //NextFollowUpDate End

                //b.Append("<p style='text-align:center;margin:0;'><b>Next Follow Up : </b> " + NextFollowUpDate + "<hr style='width:90%;text-align:center;'/></p>");
            }
            if (!string.IsNullOrEmpty(T00005))
            {

                //Doctor Notes Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Doctor Notes :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00005.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Doctor Notes End
            }
            if (!string.IsNullOrEmpty(T00003))
            {
                //Sign & Symptoms Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Sign & Symptoms :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00003.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //Sign & Symptoms End
            }

            if (!string.IsNullOrEmpty(T00015))
            {
                //History Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>History :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00015.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //History End
            }

            if (!string.IsNullOrEmpty(T00008))
            {
                //History Begin
                b.Append("<div style='width:95%;font-size:13px;font-family:Calibri'><br>");
                b.Append("<p style='text-align:left;margin:0'><b>Doctor Advice :</b></p>");
                b.Append("<span style='font-size:15px;'>" + T00008.TrimEnd(',') + "</span>");
                b.Append("</div>");
                //History End
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
                    b.Append("<div style='height:163px;font-family:Calibri'>");
                    b.Append("<table style='padding:2px;background:#fff;width:100%;;font-size:18px;text-align:left;margin-top:30px;font-family:Calibri'>");

                    b.Append("<tr>");
                    b.Append("<td style='width:35%;text-align:left;'></td>");
                    b.Append("<td style='width:65%;text-align:left;font-size:35px'><b>"+ds.Tables[0].Rows[0]["Hospital_Name"].ToString()+"<b/></td>");
                    b.Append("</tr>");

                    b.Append("<tr>");
                    b.Append("<td style='width:35%;text-align:left;'></td>");
                    b.Append("<td style='width:65%;text-align:left;font-size:33px'>" + ds.Tables[0].Rows[0]["dept_name"].ToString() + "</td>");
                    b.Append("</tr>");

                    b.Append("<tr>");
                    b.Append("<td style='width:35%;text-align:left;'></td>");
                    b.Append("<td style='width:65%;text-align:left;font-size:22px'>" + ds.Tables[0].Rows[0]["Full_Address"].ToString() + "</td>");
                    b.Append("</tr>");

                    b.Append("<tr>");
                    b.Append("<td style='width:35%;text-align:left;'></td>");
                    b.Append("<td style='width:65%;text-align:left;font-size:22px'>" + ds.Tables[0].Rows[0]["Info"].ToString() + "</td>");
                    b.Append("</tr>");

                    b.Append("</table>");
                    b.Append("</div>");

                    b.Append("<table style='width:100%;border-bottom:1px solid #000; font-size:25px;float:left;padding:8px;font-family:calibri'>");
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
                    b.Append("<td><b>Valid Till</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["validTill"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td><b>Panel</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["PanelName"].ToString() + "</td>");
                    b.Append("<td>&nbsp;</td>");
                    b.Append("<td><b>Appointment No.</b></td>");
                    b.Append("<td><b>:</b></td>");
                    b.Append("<td>" + dr["app_no"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                }
            }
            return b.ToString();
        }
        private string GetFooterHTML(DataSet ds)
        {
            _PrintWithHeader = "Y";
            StringBuilder f = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    //Doctor Details Bottom Begin
                    ////  b.Append("<div style='width:100%;zoom:1;font-size:22px;margin-top:50px;margin-right:10px;text-align:right;float:right>");
                    //b.Append("<div style='width:100%;position:absolute;bottom:0;right:20px'>");
                    //b.Append("<img style='float:right;margin-top:20px;' src='" + UtilityClass.documentServerUrl + "/" + dr["SignVirtualPath"].ToString() + "'/><br/>");
                    //b.Append("<h4 style='text-align:right;width:100%;float:right'>" + dr["DoctorName"].ToString() + "</h4>");
                    //b.Append("<h4 style='text-align:right;width:100%;float:right'>" + dr["degree"].ToString() + "</h4>");
                    //b.Append("</div>");
                    ////Doctor Details Bottom End

                    //Doctor Details Bottom Begin
                    f.Append("<div style='width:100%;float:left;background:transparent;border-bottom:1px solid #000;border-right:1px solid #000;border-left:1px solid #000;font-family:Calibri'>");
                    f.Append("<div style='width:26%;float:left;'>");
                    f.Append("</div>");
                    f.Append("<div style='width:74%;float:right;border-left:1px solid #000;padding-right:6px;padding-bottom:6px'>");
                    f.Append("<h3 style='text-align:right;margin:0;font-size:18px'><img style='height:70px' src='" + UtilityClass.documentServerUrl + "/" + dr["SignVirtualPath"].ToString() + "'/></h3>");
                    f.Append("<h4 style='text-align:right !important;margin:0;font-size:18px'>" + dr["DoctorName"].ToString() + "<br>" + dr["degree"].ToString() + "</h4>");
                    f.Append("</div>");
                    f.Append("</div>");
                    //Doctor Details Bottom End
                }
            }
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