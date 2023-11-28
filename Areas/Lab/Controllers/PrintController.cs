﻿using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HiQPdf;
using MediSoftTech_HIS.Areas.Lab.Repository;

namespace MediSoftTech_HIS.Areas.Lab.Controllers
{
    public class PrintController : Controller
    {
        //GET:Lab/Print
        public FileResult SampleTransferSheet(string DispatchNo)
        {
          
            PdfGenerator pdfConverter = new PdfGenerator();
            SampleDispatchInfo obj = new SampleDispatchInfo();
            obj.from = Convert.ToDateTime("1900/01/01");
            obj.to = Convert.ToDateTime("1900/01/01");
            obj.Prm1 = DispatchNo;
            obj.Logic = "GetDispatchInfoForPrint";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("Lab/SampleDispatchQueries", obj);

            DataSet ds = dsResult.ResultSet;
            StringBuilder b = new StringBuilder();
            string BranchName = string.Empty;
            string To = string.Empty;
            string DispatchDate = string.Empty;
            string DeliveryBoy = string.Empty;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BranchName = dr["DispatchFrom"].ToString();
                    To = dr["DispatchTo"].ToString();
                    DispatchDate = dr["sample_collect_date"].ToString();
                    DeliveryBoy = dr["DeliveryBoy"].ToString();
                }
                b.Append("<h2 style='text-align:center;font-weight:bold;margin-bottom:5px;font-family: Sans-serif; '>Sample Transfer Sheet</h2>");
                b.Append("<hr/>");
                //1st Info Table 1
                b.Append("<div style='background:#eee;padding:5px;'>");
                b.Append("<table style='font-size:12px;width:100%;font-family:Sans-serif;'>");
                b.Append("<tr>");
                b.Append("<td style='width:12%;'><b>Dispatch No</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + DispatchNo + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td style='width:13%;'><b>BarCode</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td><i" +
                    "mg src=" + BarcodeGenerator.GenerateBarCode(DispatchNo, 300, 70) + " style='width:250px'/></td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td style='width:12%;'><b>Branch Name</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + BranchName + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td style='width:13%;'><b>To</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + To + "</td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td style='width:13%;'><b>Dispatch Date </td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + DispatchDate + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td style='width:13%;'><b>Delivery Boy</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + DeliveryBoy + "</td>");
                b.Append("</tr>");
                b.Append("</table>");
                b.Append("</div>");
                //Second Info Table
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<table style='width:100%;font-size:11px; font-family: Sans-serif;text-align:left;margin-top:8px' border='0'  cellspacing='0'>");
                b.Append("<tr>");
                b.Append("<th style='white-space:none;width:15%;border:1px solid #ddd;padding-left:8px'>Visit No</th>");
                b.Append("<th style='white-space:none;width:12%;border:1px solid #ddd;padding-left:8px'>BarCode No</th>");
                b.Append("<th style='white-space:none;width:40%;border:1px solid #ddd;padding-left:8px'>Patient Name</th>");
                b.Append("<th style='white-space:none;width:16%;border:1px solid #ddd;padding-left:8px'>Reg Date</th>");
                b.Append("<th style='white-space:none;width:16%;border:1px solid #ddd;padding-left:8px'>Sample Date</th>");
                b.Append("</tr>");
                int count = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    count++;
                    b.Append("<tr>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px'>" + dr["VisitNo"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px'>" + dr["barcodeNo"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px'>" + dr["patient_name"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px; white-space:nowrap;'>" + dr["RegDate"].ToString() + "</td>");
                    b.Append("<td style='border:1px solid #ddd;padding-left:8px; white-space:nowrap;'>" + dr["sample_collect_date"].ToString() + "</td>");
                    b.Append("</tr>");
                    b.Append("<tr>");
                    b.Append("<td colspan='5' style='white-space:none;background:#eee; font-size:8px;border:1px solid #ddd;padding-left:8px'><b>Test Name</b> : " + dr["TestName"].ToString() + "</td>");
                    b.Append("</tr>");
                }
                b.Append("</table>");
                b.Append("</div>");
            }
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf("-", b.ToString(), "-", "Sample-Transfer-Sheet.pdf");
        }
        public FileResult PrintConsentForm(string visitNo, string consentId)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            LabTemplates obj = new LabTemplates();
            obj.hosp_id = "-";
            obj.subcatid = "-";
            obj.testcode = "-";
            obj.prm_1 = visitNo;
            obj.prm_2 = consentId;
            obj.login_id = "-";
            obj.Logic = "PrintConsentForm";
            dataSet dsResult = APIProxy.CallWebApiMethod("Lab/mLabTemplateQueries", obj);

            DataSet ds = dsResult.ResultSet;
            StringBuilder h = new StringBuilder();
            StringBuilder b = new StringBuilder();
            StringBuilder f = new StringBuilder();
            string content = string.Empty;
            string VisitNo = string.Empty;
            string PatientName = string.Empty;
            string Gender = string.Empty;
            string Age = string.Empty;
            string Address = string.Empty;
            string AgeInfo = string.Empty;
            string MobileNo = string.Empty;
            string Investigation = string.Empty;
            b.Append("<div style='font-size:20px;width:100%;height:1005px;font-family:calibri'>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<div style='width:100%;font-size:20px;float:left;margin-top:-12px;padding:8px;font-family:calibri'>");
                    string chandanLogo = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    b.Append("<div style='text-align:left;width:32%;float:left'>");
                    b.Append("<img src=" + chandanLogo + " style='width:180px;margin-top:5px;' />");
                    b.Append("</div>");
                    b.Append("<div style='text-align:left;width:auto;float:left;width:65%;'>");
                    b.Append("<h2 style='font-weight:bold;margin:0'>" + dr["Hospital_Name"].ToString() + "</h2>");
                    b.Append("<span style='text-align:left;'>" + dr["Full_Address"].ToString() + "</span><br/>");
                    b.Append("<span style='text-align:left;'><b>Landline No : </b>" + dr["LandlineNo"].ToString() + "</span><br/>");
                    //b.Append("<span style='text-align:left;'><b>Email ID : </b>" + dr["EmailID"].ToString() + "</span><br/>");
                    b.Append("<span style='text-align:left;'><b>CIN No: " + dr["cin_no"].ToString() + "</b></span><br/>");
                    b.Append("</div>");
                    b.Append("</div>");
                    b.Append("<hr style='margin:0;'/>");
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    VisitNo = dr["VisitNo"].ToString();
                    PatientName = dr["patient_name"].ToString();
                    Gender = dr["gender"].ToString();
                    Age = dr["age"].ToString();
                    Address = dr["Address"].ToString();
                    AgeInfo = dr["ageInfo"].ToString();
                    MobileNo = dr["mobile_no"].ToString();
                    Investigation = dr["ItemName"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(PatientName))
            {
                b.Append("<h1 style='font-weight:bold;margin:0px;text-align:center'>Consent Form</h1>");
                b.Append("<div style='width:100%;padding:10px;background:#ececec'>");
                b.Append("<table style='font-size:19px;font-family:calibri;text-align:left;background:#ececec;'>");
                b.Append("<tr>");
                b.Append("<td style='white-space:pre;'><b>Visit No</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td style='white-space:pre;'><b>" + VisitNo + "</b></td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td><b>Patient Name</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + PatientName + "</td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td><b>Age</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + AgeInfo + "</td>");
                b.Append("<td>&nbsp;</td>");
                b.Append("<td><b>Mobile No</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td>" + MobileNo + "</td>");
                b.Append("</tr>");
                b.Append("<tr>");
                b.Append("<td><b>Investigation</b></td>");
                b.Append("<td><b>:</b></td>");
                b.Append("<td colspan='4'>" + Investigation + "</td>");
                b.Append("</tr>");
                b.Append("</table>");
                b.Append("</div>");
                b.Append("<div style='margin-bottom:15px'></div>");
            }
            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    content = dr["t_content"].ToString();
                    foreach (var var1 in dr["var_list"].ToString().Split(','))
                    {
                        var oldString = "{<strong>" + var1 + "</strong>}";
                        var newString = "<strong>" + var1 + "</strong>";
                        if (var1 == "PatientName")
                            content = content.Replace(oldString, "<strong>" + PatientName + "</strong>");
                        if (var1 == "Gender")
                            content = content.Replace(oldString, "<strong>" + Gender + "</strong>");
                        if (var1 == "Age")
                            content = content.Replace(oldString, "<strong>" + Age + "</strong>");
                        if (var1 == "MobileNo")
                            content = content.Replace(oldString, "<strong>" + MobileNo + "</strong>");
                        if (var1 == "Address")
                            content = content.Replace(oldString, "<strong>" + Address + "</strong>");
                        if (var1 == "Investigation")
                            content = content.Replace(oldString, "<strong>" + Investigation + "</strong>");
                    }
                    b.Append(content);
                }
            }
            b.Append("<div style='width:100%;position:absolute;bottom:5px;right:10px'>");
            b.Append("<div style='width:50%;float:left;text-align:center'>");
            b.Append("<hr style='width:60%;' />Patient Signature");
            b.Append("</div>");
            b.Append("<div style='width:50%;float:right;text-align:center'>");
            b.Append("<hr style='width:60%;'/>Authorized Signature");
            b.Append("</div>");
            b.Append("</div>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 50;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 5;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A5";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf("-", b.ToString(), f.ToString(), "ConsentForm.pdf");
        }
        public FileResult PrintLabReport(string visitNo, string SubCat,string TestIds,string Logic)
        {
            GenReport rep = new GenReport();
            return rep.PrintLabReport(visitNo, SubCat, TestIds, Logic);
        }
        public FileResult PrintWorkSheet(string visitNo, string SubCat, string TestIds, string Logic)
        {
            WorkSheet rep = new WorkSheet();
            return rep.PrintWorkSheet(visitNo, SubCat, TestIds, Logic);

        }
    }
}