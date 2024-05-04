using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using MediSoftTech_HIS.Areas.IPD.Repository;
using MediSoftTech_HIS.Areas.Lab.Repository;
using MediSoftTech_HIS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using static HISWebApi.Models.IPDBO;

namespace MediSoftTech_HIS.Areas.IPD.Controllers
{
    public class PrintController : Controller
    {
        public FileResult RequisitionSlip(string indent_no, string Logic = "IndentDetail")
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipIndentReport obj = new ipIndentReport();
            obj.indent_no = indent_no;
            obj.Logic = Logic;
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/IPOP_IndentQueries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string pName = "";
            string IndentBy = "";
            string dept_name = "";
            string IndentDate = "";
            string IPDNo = "";
            string Panel = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                pName = dr["pt_name"].ToString();
                IndentBy = dr["IndentBy"].ToString();
                dept_name = dr["dept_name"].ToString();
                IndentDate = dr["cr_date"].ToString();
                IPDNo = dr["ipop_no"].ToString();
                Panel = dr["panel_name"].ToString();
            }
            b.Append("<h1 style='text-align:center;font-weight:bold;text-decoration: underline;'>Requisition Slip</h1>");
            b.Append("<table style='background:#f5f5f5;width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'><b>Indent No.</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + indent_no + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:10%;'><b>Indent By</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:27%;'>" + IndentBy + "(" + dept_name + ")" + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:12%;'><b>Indent Date</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:22%;'>" + IndentDate + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:10%;'><b>IPD No.</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + IPDNo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:14%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:27%;'>" + pName + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:12%;'><b>Panel</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:20%;'>" + Panel + "</td>");

            b.Append("</tr>");
            b.Append("</table>");

            b.Append("<p>To,<br/>&nbsp;Chandan Hospital</p>");
            b.Append("<b>Subject : Please Issue Following Items.</b><br/>");
            b.Append("<span>Dear Sir,<br/>Kindly Arrange to Procure Following Items at the earliast for</span><br/>");
            b.Append("<br/>");
            b.Append("<table border='1' style='width:100%;font-size:11px;border-collapse: collapse'>");
            b.Append("<tr>");
            b.Append("<th style='width:40%;text-align:left;padding-left:4px;'>Item Name</th>");
            b.Append("<th style='white-space: nowrap;text-align:right;'>Qty</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>Issue Qty</th>");
            b.Append("<th style='white-space: nowrap;text-align:right'>Balance Qty</th>");
            b.Append("<th style='text-align:right;padding-right:4px;'>Type</th>");
            //b.Append("<th style='text-align:right;padding-right:4px;'>Mfd. Name</th>");
            b.Append("</tr>");
            //Body			
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    b.Append("<tr>");
                    b.Append("<td style='text-align:left;white-space: nowrap;padding-left:4px;'>" + dr["item_name"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["qty"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;'>" + dr["issue_qty"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;white-space: nowrap;padding-right:4px;'>" + dr["balance"].ToString() + "</td>");
                    b.Append("<td style='text-align:right;padding-right:4px;white-space: nowrap'>" + dr["ReqType"] + "</td>");
                    b.Append("</tr>");
                }
            }
            b.Append("</table>");
            //b.Append("<div style='float:right;'>");
            //b.Append("<b>From</b><br/>");
            //b.Append(IndentBy + "<br/>");
            //b.Append(dept_name);
            //b.Append("</div>");

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
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult BloodRequisitionForm(string IndentNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = IndentNo;
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Logic = "BloodRequisitionFormPrint";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursingService/IPD_PatientQueries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();

            string headerImageFile = HttpContext.Server.MapPath(@"~/Content/logo/logo.png");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                b.Append("<div style='width:99.50%;float:left;padding:8px'>");//Main Div Start
                b.Append("<div style='width:100%;float:left;padding:8px'>");
                b.Append("<div style='width:30%;float:left;'>");
                b.Append("<img src=" + headerImageFile + " style='width:130px;margin-top:-15px' />");
                b.Append("</div>");
                b.Append("<div style='width:70%;float:right;'>");
                b.Append("<h2 style='font-weight:bold;margin:0;text-align:left'>BLOOD REQUISITION FORM</h2>");
                b.Append("<h3 style='margin:0;text-align:left'>Indent No : "+ IndentNo + "</h3>");
                b.Append("</div>");
                b.Append("</div>");
                b.Append("<hr/>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;border:1px solid #000;'>");

                b.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>Hospital</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:60%'>CHANDAN HOSPITAL (Private) Reg. No. 0915700044</td>");
                b.Append("<td style='text-align:left;font-weight:bold; width:10%'><b>Other</b></td>");
                b.Append("<td style='width:6%;text-align:right;'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:8%'>" + dr["IPOPType"].ToString() + "</td>");

                b.Append("</tr>");
                b.Append("</table>");
                b.Append("<table style='width:100%;font-size:15px;text-align:left;margin-top:2%'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>Patient Name</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:29%'>" + dr["PatientName"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%'><b>Age</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:14%'>" + dr["ageInfo"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%'><b>Gender</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:14%'>" + dr["Gender"].ToString() + "</td>");
                b.Append("</tr>");

                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>UHID</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:30%'>" + dr["UHID"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%'><b>IPD No.</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:15%'>" + dr["IPOPNo"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%'><b>RoomWard</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:15%'>" + dr["RoomType"].ToString() + "</td>");
                b.Append("</tr>");

                b.Append("<tr style='padding:3px;' colspan='6'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>Address</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:30%'>" + dr["address"].ToString() + "</td>");

                b.Append("</tr>");
                b.Append("</table>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");

                b.Append("<table style='width:100%;font-size:15px;text-align:left;'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:25%'><b>Consultant</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:24%'>" + dr["Consultant"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold; width:25%'><b>Diagnosis</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:14%'>" + dr["Diagnosis"].ToString() + "</td>");

                b.Append("</tr>");

                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:25%'><b>Indication for Transfusion</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:24%'>" + dr["TransfusionIndicator"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold; width:25%'><b>Hb %</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left;width:14%'>" + dr["HbPerc"].ToString() + "</td>");
                b.Append("</tr>");

                b.Append("<tr style='padding:3px;' colspan='3'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>Previous Transfusion</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:30%'>" + dr["PreviousTransfusion"].ToString() + "</td>");
                b.Append("</tr>");
                b.Append("</table>");
                b.Append("<table style='width:100%;font-size:15px;text-align:left;'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:10%'><b>Donor No </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:10%'>" + dr["DonorNo"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold; width:10%'><b>ABO Rh </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:10%'>" + dr["AboRH"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>Date Transfused </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:13%'>" + dr["dateTransfuse"].ToString() + "</td>");

                b.Append("<td style='text-align:left;font-weight:bold; width:15%'><b>Reaction if any  </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='text-align:left; width:13%'>" + dr["ReactionAny"].ToString() + "</td>");
                b.Append("</tr>");
                b.Append("</table>");
                b.Append("</div>");
                if (dr["IsPregnant"].ToString() == "Y")
                {
                    b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                    b.Append("<div style='width:100%;float:left;'>");
                    b.Append("<label style='width:100%;float:left;padding:3px;'><b>Obstetrics History for Female Patient Only</b></label>");
                    b.Append("<div style='width:15%;padding:3px;float:left;'>");
                    b.Append("<label><b>Pregnancy : </b> " + dr["IsPregnant"].ToString() + "</label>");
                    b.Append("</div>");
                    b.Append("<div style='width:40%;padding:2px;float:left;'>");
                    b.Append("<label><b>H/O of haemolytic disease in new born : </b> " + dr["haemolytic_disease"].ToString() + "</label>");
                    b.Append("</div>");
                    b.Append("<div style='width:40%;padding:2px;float:left;'>");
                    b.Append("<label><b>H/O of still birth/miscarriage : </b> " + dr["miscarriage"].ToString() + "</label>");
                    b.Append("</div>");
                    b.Append("</div>");
                    b.Append("</div>");
                }
                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<label style='width:100%;float:left;padding:3px;'><b>Component and Quantity</b></label>");
                b.Append("<div style='width:95%;padding:3px;float:left;display:flex'>");
                foreach (DataRow dr1 in ds.Tables[1].Rows)
                {
                    b.Append("<label style='margin-left:6px;'><b>" + dr1["AliasName"].ToString() + " :</b> " + dr1["IndentQty"].ToString() + "</label>");
                }
                b.Append("</div>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<label style='width:100%;float:left;padding:3px;'><b>Requirement Time</b></label>");

                b.Append("<div style='width:20%;padding:3px;float:left;'>");
                b.Append("<label><b>" + dr["ReqType"].ToString() + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:36%;padding:2px;float:left;'>");
                b.Append("<label>" + dr["ReqType_Remark1"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("<div style='width:36%;padding:2px;float:left;'>");
                b.Append("<label><b>Supply Time : </b> " + dr["ReqType_Remark2"].ToString() + "</label>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:2px 0;line-height:18px;height:auto;margin-top:15px;border:1px solid #000;'>");
                b.Append("<div style='width:100%;float:left;'>");
                b.Append("<label style='width:100%;float:left;padding:3px;'>I certify that I have personally collected tha blood sample after identification of patients ID No. and name. I have expalined the necessity of blood transfusion and risks associated with it to patients/relatives and taken informed conscent. </label>");

                b.Append("<div style='width:17%;padding:3px;float:left;'>");
                b.Append("<label>Date : <b>" + dr["IndentDate"].ToString().Split(' ')[0] + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:15%;padding:2px;float:left;'>");
                b.Append("<label>Time : <b>" + dr["IndentDate"].ToString().Split(' ')[1] + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:35%;padding:2px;float:left;'>");
                b.Append("<label>Doctor Name : <b> " + dr["DoctorName"].ToString() + "</b></label>");
                b.Append("</div>");
                b.Append("<div style='width:27%;padding:2px;float:left;'>");
                b.Append("<label><b>Sign of Doctor: </b> <img src=" + dr["SignVirtualPath"].ToString() + " style='width:109px' /></label>");
                b.Append("</div>");
                b.Append("</div>");
                b.Append("</div>");


                b.Append("<label style='margin-top:15px;width:100%;float:left;padding:3px;text-align:center'><b>TO BE FILLED BY DEPARTMENT OF TRANSFUSION MEDICINE</b></label>");

                b.Append("<div style='width:100%;float:left;font-size:15px;padding:5px 0;line-height:18px;height:auto;margin-top:5px;border:1px solid #000;'>");

                b.Append("<table style='width:100%;font-size:15px;text-align:left;'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:18%'><b>Booking No</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");
                b.Append("<td style='text-align:left;font-weight:bold;width:15%;margin-left:5px'><b>Date</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");
                b.Append("<td style='text-align:left;font-weight:bold;width:15%;margin-left:5px'><b>Time</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");
                b.Append("</tr>");
                b.Append("</table>");

                b.Append("<table style='width:100%;font-size:15px;text-align:left;'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:25%'><b>Blood Group (ABO & Rh) </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:74%'></td>");
                b.Append("</tr>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:25%'><b>Mode of Adjustment </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:74%'></td>");
                b.Append("</tr>");

                b.Append("</table>");
                b.Append("<table style='width:100%;font-size:15px;text-align:left;'>");
                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:18%; margin-left:2px'><b>Issue on date</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%;'></td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%; margin-left:2px'><b>Booking No </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;2px;width:16%'></td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15% margin-left:2px'><b>Donor No</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");
                b.Append("</tr>");

                b.Append("<tr style='padding:3px;'>");
                b.Append("<td style='text-align:left;font-weight:bold; width:18%'><b>Amount Collected </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%'><b>Bill No </b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");

                b.Append("<td style='text-align:left;font-weight:bold;width:15%'><b>Bill Date</b></td>");
                b.Append("<td style='width:1%'><b>:</b></td>");
                b.Append("<td style='height:15px;border:1px solid #000;width:16%'></td>");
                b.Append("</tr>");
                b.Append("</table>");
                b.Append("</div>");
                b.Append("<label style='width:100%;font-size:13px;float:left;padding:3px;text-align:left;margin-top:10px'><b>NOTE : ALL FIELDS MUST BE FILLED. PLEASE WRITE N/A WHERE NOT APPLICABLE.</b></label>");

                b.Append("</div>");//Main Div Close
            }

            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 5;
            pdfConverter.PageMarginRight = 5;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult SalesBill(string InvoiceNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            SalesBillInfo obj = new SalesBillInfo();
            obj.unit_id = "MS-H0048";
            obj.Bill_No = InvoiceNo;
            obj.login_id = "-";
            obj.login_name = "-";
            dataSetPharmacy dsResult = APIProxy.CallPharmacyWebApiMethod("sales/billInformation", obj);
            DataSet ds = dsResult.result;

            string _result = string.Empty;
            StringBuilder h = new StringBuilder();
            StringBuilder b = new StringBuilder();
            StringBuilder f = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds.Tables[0].Rows)
                {
                    /*
                    h.Append("<div style='width:100%;padding:2px 4px;zoom:1.5;'>");//head Start		
                    h.Append("<table style='width:100%'>");
                    h.Append("<tr><td style='width:25%'><b>GSTIN :</b> " + dr1["gstno"].ToString() + "</td>");
                    h.Append("<td style='width:25%'><b>Drug Lic. No. : </b>" + dr1["Dl_No"].ToString() + "</td>");
                    h.Append("<td style='width:25%'><b>Food Lic. No. : </b>" + dr1["FoodLicenceNo"].ToString() + "</td>");
                    h.Append("<td style='width:25%'><b>C.I.N. : </b>" + dr1["cin_no"].ToString() + "</td></tr>");
                    h.Append("</table>");
                    h.Append("</div>");//head End
                    */

                    h.Append("<div style='width:100%;padding:2px 4px;zoom:1.5;'>");//head Start			
                    h.Append("<div style='width:25%;float:left;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>GSTIN :</b> <br/> " + dr1["gstno"].ToString() + "</span>");
                    h.Append("</div>");

                    h.Append("<div style='width:25%;float:right;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>Drug Lic. No. : </b> <br/> " + dr1["Dl_No"].ToString() + "</span>");
                    h.Append("</div>");

                    h.Append("<div style='width:25%;float:right;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>Food Lic. No. : </b> <br/> " + dr1["FoodLicenceNo"].ToString() + "</span>");
                    h.Append("</div>");

                    h.Append("<div style='width:25%;float:right;'>");
                    h.Append("<span style='text-align:center;font-size:13px;'><b>C.I.N. : </b> <br/> " + dr1["cin_no"].ToString() + "</span>");
                    h.Append("</div>");
                    h.Append("</div>");//head End

                    h.Append("<div style='width:100%;border:1px solid #000;border-radius:5px;zoom:1.5;'>");
                    //Header
                    h.Append("<div style='width:100%;margin-top:10px;'>");
                    h.Append("<div style='width:20%;float:left;font-size:11px;'>");
                    string headerImageFile = HttpContext.Server.MapPath(@"~/Content/logo/logo.png");
                    h.Append("<img src=" + headerImageFile + " style='width:90px;margin:20px 10px;' />");
                    h.Append("</div>");
                    h.Append("<div style='width:50%;float:left;font-size:13px;line-height:16px;'>");
                    h.Append("<h3 style='font-weight:bold;margin:0'>" + dr1["shop_name"].ToString() + "</h3>");
                    h.Append("<span style='text-align:left;'>" + dr1["comp_name"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'>" + dr1["address"].ToString() + "</span><br/>");
                    h.Append("<span style='text-align:left;'>Contact No. 05226666601, 8707542569, Email:care@chandanpharmacy.com</span><br/>");
                    h.Append("</div>");
                    h.Append("<div style='width:30%;float:right;font-size:12px;line-height:16px;zoom:1.2'>");
                    h.Append("<b style='float:right;padding-right:5px;'>Sale Date : " + Convert.ToDateTime(dr1["sale_date"]).ToString("dd-MMM-yyy") + "</b><br/>");
                    h.Append("<b style='float:right;padding-right:5px;'>Sale Inv. No. : " + dr1["sale_inv_no"].ToString() + "</b><br/>");
                    h.Append("<b style='float:right;padding-right:5px;'>Health Card No. : " + dr1["card_no"].ToString() + "</b>");
                    h.Append("</div>");
                    h.Append("</div>");
                    //Information
                    h.Append("<div style='width:99%;border-top:1px solid #000;height:60px;border-bottom:1px solid #000;margin:80px 0 0 0;padding:2px 4px;zoom:1.2'>");
                    h.Append("<div style='width:40%;float:left;font-size:12px;line-height:18px'>");
                    h.Append("<span style='text-align:left;'>Details of Receiver (Billed to)</span><br/>");
                    h.Append("<b style='text-align:left;'>Name : " + dr1["pt_name"].ToString() + "</b><br/>");
                    h.Append("<span style='text-align:left;'>Prescribed By : " + dr1["ref_name"].ToString() + "</span><br/>");
                    h.Append("</div>");
                    h.Append("<div style='width:50%;float:left;font-size:13px;'>");
                    h.Append("<span style='text-align:left;'>IPD/OPD PATIENT INFORMATION</span><br/>");
                    h.Append("<table style='width:100%;font-size:13px;text-align:left;margin:0'>");
                    h.Append("<td style='width:12%;'> " + dr1["IpdInfo"].ToString() + "</td>");
                    h.Append("</tr>");
                    h.Append("</table>");
                    h.Append("</div>");
                    h.Append("<div style='width:9%;float:right;font-size:10px;line-height:22px;border:2px solid #000;border-radius:2px;text-align:center'>");
                    h.Append("<b style='padding:2px;'>Token No.</b><br/>");
                    h.Append("<span style='padding:2px;font-size:18px;'>" + dr1["token_no"].ToString() + "</span>");
                    h.Append("</div>");
                    h.Append("</div>");
                    //Items 				
                    b.Append("<div style='width:100%;'>");
                    b.Append("<table style='width:100%;font-size:13px;text-align:left;margin:0; border-collapse: collapse;' border='1' >");
                    b.Append("<tr style='background:#ddd;'>");
                    b.Append("<td style='width:1%;padding-left:5px'><b>No.</b></td>");
                    b.Append("<td style='width:30%;padding-left:5px'><b>Medicine Name </b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>HSN</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>UOM</b></td>");
                    b.Append("<td style='width:8%;font-size:10px;text-align:right;padding-right:2px'><b>Unit MRP</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>Qty</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>Amount</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>Discount</b></td>");
                    b.Append("<td style='width:6%;font-size:10px;text-align:right;padding-right:2px'><b>Taxable Value</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>CGST Rate</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>CGST Amt.</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>SGST Rate</b></td>");
                    b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'><b>SGST Amt.</b></td>");
                    b.Append("</tr>");
                    //Body			
                    var count = 0;
                    if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            count++;
                            b.Append("<tr>");
                            b.Append("<td style='width:1%;padding-left:5px'>" + count + "</td>");
                            b.Append("<td style='width:30%;padding-left:5px'>" + dr["item_name"].ToString() + "<br> Mfd/Mkt: " + dr["mfd_name"].ToString() + ",Batch No. " + dr["batch_no"].ToString() + "<br>Expiry : " + dr["exp_date"].ToString() + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + dr["hsn"].ToString() + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + dr["uom"].ToString() + "</td>");
                            b.Append("<td style='width:8%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["mrp"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + dr["sale_qty"].ToString() + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["amount"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["discount"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:6%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["taxvalue"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["CGST_rate"]).ToString(".0") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["cgst"]).ToString(".00") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["SGST_rate"]).ToString(".0") + "</td>");
                            b.Append("<td style='width:5%;font-size:10px;text-align:right;padding-right:2px'>" + Convert.ToDecimal(dr["sgst"]).ToString(".00") + "</td>");
                            b.Append("</tr>");
                        }
                    }

                    b.Append("<tr>");
                    b.Append("<th style='text-align:right;padding-left:5px;' colspan='9'>Amount of Tax Subject to Reverse Charge</th>");
                    b.Append("<td style='font-size:12px;text-align:right;padding-right:2px' colspan='2'> " + Convert.ToDecimal(dr1["cgst"]).ToString(".00") + "</td>");
                    b.Append("<td style='font-size:12px;text-align:right;padding-right:2px' colspan='2'> " + Convert.ToDecimal(dr1["sgst"]).ToString(".00") + "</td>");
                    b.Append("</tr>");
                    b.Append("</table>");
                    b.Append("</div>");
                    b.Append("</div>");//Final End										
                                       //Bottom info			
                    f.Append("<div style='width:100%;border:none;zoom:1.5;'>");
                    f.Append("<div style='width:45%;float:left;font-size:12px;line-height:10px'>");
                    f.Append("<span style='text-align:left;'>Note :</span><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>1) Price Difference Under Drug Price Control order 1970 will be refunded if any</b><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>2) Medicine without Cash Memo, Batch No & Exp. will not be taken back.</b><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>3) Please get medicines verified from prescription before use.</b><br/>");
                    f.Append("<b style='text-align:left;font-size:8px'>4) All disputes subject to LUCKNOW Jurisdiction only.</ b><br/>");
                    f.Append("</div>");
                    f.Append("<div style='width:20%;float:left;font-size:12px;line-height:13px;margin-top:3px;'>");
                    string signature = HttpContext.Server.MapPath(@"/Content/logo/logo.png");
                    f.Append("<img src='data:image/png;base64," + dr1["sign_image"].ToString() + " style='width:80px;height:18px;' /><br/>");
                    f.Append("<b style='text-align:center;font-size:15px;'>Signature</b>");
                    f.Append("</div>");
                    f.Append("<div style='width:35%;float:right;font-size:13px;'>");
                    f.Append("<table style='width:100%;font-size:13px;text-align:left;margin:4px; border-collapse: collapse;' border='1' >");
                    f.Append("<tr>");
                    f.Append("<td style='width:12%;text-align:right;'><b>Total</b></td>");
                    f.Append("<td style='width:12%;text-align:right;'><b>Discount</b></td>");
                    f.Append("<td style='width:12%;text-align:right;'><b>Payable</b></td>");
                    f.Append("</tr>");
                    f.Append("<tr>");
                    f.Append("<td style='width:12%;text-align:right;'> " + Convert.ToDecimal(dr1["total"]).ToString(".00") + "</td>");
                    f.Append("<td style='width:12%;text-align:right;'> " + Convert.ToDecimal(dr1["discount"]).ToString(".00") + "</td>");
                    f.Append("<td style='width:12%;text-align:right;'> " + Convert.ToDecimal(dr1["net"]).ToString(".00") + "</td>");
                    f.Append("</tr>");
                    f.Append("</table>");
                    f.Append("<b style='text-align:center;font-size:15px;'>Received</b>&nbsp;&nbsp;");
                    f.Append("<b style='min-width:95px;text-align:right;border:1px solid #000;padding:1px 3px;float:right;font-size:15px'> " + Convert.ToDecimal(dr1["received"]).ToString(".00") + "</b>");
                    f.Append("</div>");
                    f.Append("<div style='width:45%;float:left;'>");
                    f.Append("<img src=" + BarcodeGenerator.GenerateBarCode(dr1["barcode"].ToString(), 400, 70) + " style='float:right' />");
                    //f.Append("<img src=" + BarcodeGenerator.GenerateBarCode(dr1["barcode"].ToString(),480,60) + " style='float:right' />");
                    f.Append("</div>");
                    f.Append("<div style='width:55%;float:right;padding:20px 0'>");
                    f.Append("<span style='float:right;font-size:13px;'>In Words :" + AmountConverter.ConvertToWords(Convert.ToString(dr1["net"].ToString()).ToString()) + "</span><br><br><br>");
                    f.Append("<span style='float:right;font-size:16px;'>You have saved Rs. " + Convert.ToDecimal(dr1["discount"]).ToString(".00") + "</span>");
                    f.Append("</div>");
                    f.Append("</div>");
                    string CurrentDateTime = DateTime.Now.ToString("dd-MMM-yyy hh:mm tt");
                    f.Append("<div style='width:100%;border-top:1px solid #000;zoom:1.5;margin-top:10px'>");//Final Start	
                    f.Append("<table style='width:100%;font-size:12px;text-align:left;margin:4px; border-collapse: collapse;' border='0' >");
                    f.Append("<tr>");
                    f.Append("<td style='width:20%;text-align:center;'><b>Counter No :</b> " + dr1["counter_id"].ToString() + "</td>");
                    f.Append("<td style='width:40%;text-align:center;'><b>Bill Creation Date & Time :</b> " + CurrentDateTime + "</td>");
                    f.Append("<td style='width:40%;text-align:center;'><b>Bill Printing Time :</b> " + CurrentDateTime + "</td>");
                    f.Append("</tr>");
                    f.Append("</table>");
                    f.Append("</div>");//Footer End						
                }

            }
            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 110;
            pdfConverter.Header_Hight = 100;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A5";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), f.ToString(), "Sales-Bill.pdf");
        }
        public FileResult ClinicalSafetyRoundReports(string date, string FloorName, string Logic = "ClinicalSafetyRoundReports")
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipHandoverInfo obj = new ipHandoverInfo();
            obj.Prm1 = date;
            obj.Prm2 = FloorName;
            obj.Logic = Logic;
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/IPD_ClinicalSafetyRoundQuesries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                b.Append("<div>");
                b.Append("<span style='width:30%;font-size:16px;'><b>DATE : " + Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "</span>");
                b.Append("<span style='width:70%;font-size:16px;margin-left:50px;'><b>Floor Name: " + FloorName + "</span>");
                b.Append("</div>");
                b.Append("<h2 style='background-color:#ccc;text-align:center;font-family:Verdana,sans-serif;font-weight:bold;margin-bottom:5px;'>Clinical Safety Round Report</h2>");
                b.Append("<hr/>");
                b.Append("<div>");
                b.Append("<table border='1' style='width:100%;white-space:nowrap;font-size:11px;border-collapse: collapse;text-align:center;'>");
                b.Append("<tr>");
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    b.Append("<th style='width:12%;background-color:#ddd;'>" + dc.ColumnName + "</td>");
                }
                b.Append("</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    b.Append("<tr>");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        b.Append("<th style='width:12%;'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>");
                    }
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
            pdfConverter.PageOrientation = "Landscape";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult PatientAllotmentReport(string date, string FloorName, string Logic = "PatientAllotmentReport")
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            ipHandoverInfo obj = new ipHandoverInfo();
            obj.Prm1 = date;
            obj.Prm2 = FloorName;
            obj.Logic = Logic;
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/IPD_ClinicalSafetyRoundQuesries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                b.Append("<div>");
                b.Append("<span style='width:30%;font-size:16px;'><b>DATE : " + Convert.ToDateTime(date).ToString("dd-MM-yyyy") + "</span>");
                b.Append("<span style='width:70%;font-size:16px;margin-left:50px;'><b>Floor Name: " + FloorName + "</span>");
                b.Append("</div>");
                b.Append("<h2 style='background-color:#ccc;text-align:center;font-family:Verdana,sans-serif;font-weight:bold;margin-bottom:5px;'>Staff Assignment Report </h2>");
                b.Append("<hr/>");
                b.Append("<div style='width:100%;'>");
                b.Append("<table border='1' style='width:100%;white-space:nowrap;font-size:11px;border-collapse: collapse;text-align:center;'>");
                b.Append("<tr>");
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    b.Append("<th style='width:12%;background-color:#ddd;text-align:left'>" + dc.ColumnName + "</td>");
                }
                b.Append("</tr>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    b.Append("<tr>");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (ds.Tables[0].Rows[i][j].ToString() == "0")
                            b.Append("<th style='width:12%;text-align:left'></td>");
                        else
                            b.Append("<th style='width:12%;text-align:left;font-size:10px'>" + ds.Tables[0].Rows[i][j].ToString() + "</td>");
                    }
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
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "StaffAlloted.pdf");
        }
        public FileResult IPDBillSummary(string _IPDNo, string _ReceiptList, string _BillPrintType, string ExcludeAdlItemDiscount)
        {
            IPDBillPrint obj = new IPDBillPrint();
            return obj.PrintBill(_IPDNo, _ReceiptList, _BillPrintType, ExcludeAdlItemDiscount);
        }
        public FileResult IPDBillSummary2(string _IPDNo, string _ReceiptList, string _BillPrintType)
        {
            _IPDNo = UtilityClass.decoding(_IPDNo);
            if (_ReceiptList == null)
                _ReceiptList = "";
            _ReceiptList = UtilityClass.decoding(_ReceiptList);

            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = _IPDNo;
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Prm1 = _ReceiptList;
            obj.Prm2 = _BillPrintType;
            obj.login_id = "-";
            obj.Logic = "BillPrinting";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_BillPrint", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
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
            h.Append("<div style='height:110px;'></div>");
            h.Append("<table style='width:2080px;padding:10px 0;background:#fff;font-size:42px;text-align:left;border:1px solid #000;margin:0 15px'>");
            //h.Append("<tr>");
            //h.Append("<td colspan='7' style='font-size:62px;text-align:center'>Final Bill Detail<hr/ style='margin:7px'></td>");
            //h.Append("</tr>");

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

            b.Append("<div style='height:110px;'></div>");

            if (_BillPrintType == "BillTypeCategorywiseOnly")
                b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + BillType + " Summary Bill Detail</h2>");

            if (_BillPrintType == "ItemWise")
                b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + BillType + " Item wise Bill Detail</h2>");

            if (_BillPrintType == "IncludingPackagedItem")
                b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + BillType + " Package Breakup Detail</h2>");

            b.Append("<table style='padding:10px 0;background:#fff;width:100%;font-size:14px;text-align:left;border:1px solid #000;margin-bottom:-15px;margin-top:0'>");
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
            if (_BillPrintType != "IncludingPackagedItem")
            {
                double GrossAmount = 0;
                double PanelDiscount = 0;
                double AdlDiscount = 0;
                double TotalDiscount = 0;
                double NetAmount = 0;
                double ReceivedAmount = 0;
                double PanelApprovedAmount = 0;
                double BalanceAmount = 0;
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
                //Bottom info			
                b.Append("<div style='width:100%;float:left;margin-top:5px'>");
                b.Append("<hr/>");
                b.Append("<div style='width:60%;float:left'>");

                int Count = 0;
                if (ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
                {
                    b.Append("<label style='padding:2px 5px;'><b>Advance/Return Details</b></label>");
                    b.Append("<table border='1' style='width:100%;font-size:12px;border-collapse: collapse;margin-top:10px;'>");

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
                b.Append("<table style='font-size:12px;float:right' border='0' cellspacing='0'>");

                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Net Amount (a-(b+c)) </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;border-bottom:1px solid #000'><b>" + NetAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");

                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Gross Amount(a)</b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + GrossAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='width:55%;text-align:left'><b>Panel Discount(b)</b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='width:25%;text-align:right;white-space: nowrap;'><b>" + PanelDiscount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
                b.Append("<tr style='font-size:13px;'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left;'><b>Adl. Discount(c)</b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center;border-bottom:1px solid #000'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;border-bottom:1px solid #000'><b>" + AdlDiscount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");
                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Total Discount(b+c) </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + TotalDiscount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");


                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Received Amount </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + ReceivedAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");

                if (PanelApprovedAmount > 0)
                {
                    b.Append("<tr style='font-size:13px'>");
                    b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Panel Approval Amount </b></td>");
                    b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                    b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                    b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + PanelApprovedAmount.ToString("0.00") + "</b></td>");
                    b.Append("</tr>");
                }

                b.Append("<tr style='font-size:13px'>");
                b.Append("<td colspan='2' style='padding:3px 0;width:55%;text-align:left'><b>Balance Amount </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> : </b></td>");
                b.Append("<td style='padding:3px 0;width:10%;text-align:center'><b> Rs. </b></td>");
                b.Append("<td style='padding:3px 0;width:25%;text-align:right;white-space: nowrap;'><b>" + BalanceAmount.ToString("0.00") + "</b></td>");
                b.Append("</tr>");


                b.Append("</table>");
                b.Append("</div>");
                b.Append("</div>");

                b.Append("<div style='width:100%;float:left;margin-top:60px;display:flex'>");
                b.Append("<p style='width:48%;float:left,display:inline-block'>");
                b.Append("<labe style='text-align:center;'>...............................................<br><b>Patient's/Attendent's Signature</b></label>");
                b.Append("</p>");

                b.Append("<p style='width:48%;float:right;text-align:right;display:inline-block'>");
                b.Append("<labe style='float:right;text-align:center;'>............................................<br><b>Authorised Signature</b><br/>" + DischargeByName + "</label>");
                b.Append("</p>");

                b.Append("</div>");
            }

            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 60;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 20;
            pdfConverter.PageMarginRight = 15;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.HeaderSource = "IPDBill";
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult IPDDischargeReport(string _IPDNo)
        {
            IPDDischargeSummaryPrint obj = new IPDDischargeSummaryPrint();
            return obj.PrintDischargeReport(_IPDNo,"N");
        }

        public FileResult IPDDischargeReportOld(string _IPDNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
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
            DataSet ds = dsResult.ResultSet;

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
            //h.Append("<tr>");
            //h.Append("<td colspan='7' style='font-size:62px;text-align:center'>Final Bill Detail<hr/ style='margin:7px'></td>");
            //h.Append("</tr>");
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

            b.Append("<div style='height:110px;'></div>");

            b.Append("<h2 style='text-align:center;font-weight:bold;text-decoration: underline;'>" + DischargeHeader + "</h2>");
            b.Append("<table style='padding:10px 0;background:#fff;width:100%;font-size:15px;text-align:left;border:1px solid #000;margin-bottom:-15px;margin-top:0'>");
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
            b.Append("</table></br/>");

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    b.Append("<div style='width:100%;margin-top:10px'>");
                    b.Append("<label style='padding:2px 5px;margin-top:20px'><b>" + dr["HeaderName"].ToString() + "</b><hr/ style='margin:0;'></label>");
                    b.Append(dr["template_content"].ToString());
                    b.Append("</div>");
                }
            }
            b.Append("</table>");

            pdfConverter.Header_Enabled = true;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 20;
            pdfConverter.PageMarginRight = 15;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.HeaderSource = "IPDDischargeReport";
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Print-Dispatch-Invoice.pdf");
        }
        public FileResult AdmissionAndDischargeReport(string _IPDNo)
        {
            //var decryptedString = AesOperation.DecryptString(Prms);

            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = _IPDNo;
            obj.UHID = "-";
            obj.DoctorId = "-";
            obj.from = "1900/01/01";
            obj.to = "1900/01/01";
            obj.Prm1 = "-";
            obj.Prm2 = "-";
            obj.login_id = "-";
            obj.Logic = "AdmissionAndDischargeReport";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_BillingQuerries", obj);
            DataSet ds = dsResult.ResultSet;

            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string patient_name = "";
            string ageInfo = "";
            string AdmitDate = "";
            string AdmitTime = "";
            string IPDNo = "";
            string roomFullName = "";
            string PanelName = "";
            string ContactNo = "";
            string Address = "";
            string RelativeName = "";
            string RelationShip = "";
            string MaritalStatus = "";
            string EmergencyContactNo = "";
            string FirstDoctor = "";
            string SecondDoctor = "";
            string AdmissionType = "";


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
                roomFullName = dr["roomFullName"].ToString();
                ContactNo = dr["mobile_no"].ToString();
                RelativeName = dr["relation_name"].ToString();
                RelationShip = dr["relation_of"].ToString();
                MaritalStatus = dr["marital_status"].ToString();
                EmergencyContactNo = dr["EmergencyNo"].ToString();
                Address = dr["address"].ToString();
                PanelName = dr["PanelName"].ToString();
                FirstDoctor = dr["DoctorName"].ToString();
                AdmitDate = dr["AdmitDate"].ToString();
                AdmitTime = dr["AdmitTime"].ToString();
                AdmissionType = dr["AdmissionType"].ToString();
            }
            b.Append("<h1 style='width:100%;padding:4px 0;text-align:center'>CHANDAN HOSPITAL</h1>");
            b.Append("<fieldset>");
            b.Append("<legend style='text-align:center;background:#f4f4f4;border:1px solid #000'><b>ADMISSION AND DISCHARGE RECORD</b></legend>");
            b.Append("<table style='width:97%;background:#fff;font-size:14px;text-align:left;margin:0'>");
            b.Append("<tr>");
            b.Append("<td style='width:16%;padding:3px;'><b>UHID</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;text-aligh:left'>" + UHID + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>IPD No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + IPDNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;'><b>Patient Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + patient_name + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>Ward/Bed</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + roomFullName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;'><b>Age/Gender</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + ageInfo + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>Contact No.</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + ContactNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td colspan='7' style='border-bottom:1px solid grey'></td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;padding:3px;'><b>Relative Name</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;text-aligh:left'>" + RelativeName + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>Relation Ship</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + RelationShip + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;'><b>Marital Status</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + MaritalStatus + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>Emergency No</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + EmergencyContactNo + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;'><b>Address</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + Address + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>Panel</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + PanelName + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td colspan='7' style='border-bottom:1px solid grey'></td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;padding:3px;'><b>First Doctor</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:83%;text-aligh:left' colspan='5'>" + FirstDoctor + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;padding:3px;'><b>Second Doctor</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:83%;text-aligh:left' colspan='5'>" + SecondDoctor + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td colspan='7' style='border-bottom:1px solid grey'></td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;'><b>Admit Date</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + AdmitDate + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:16%;'><b>Admit Time</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:33%;'>" + AdmitTime + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:16%;padding:3px;'><b>Admission Type</b></td>");
            b.Append("<td style='width:1%;'><b>:</b></td>");
            b.Append("<td style='width:83%;text-aligh:left' colspan='5'>" + AdmissionType + "</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td colspan='7' style='border-bottom:1px solid grey'></td>");
            b.Append("</tr>");

            b.Append("</table>");
            b.Append("<p style='font-size:14px;'>I have understood clearly all the policies and protocols of the hospital and received the In-Patient information booklet.I have been educated about patient rights and responsibilities.</p><br/>");
            b.Append("<p style='font-size:12px;'>मैंने अस्पताल की सभी नीतियों और प्रोटोकॉल को स्पष्ट रूप से पढ़ा और समझा है  और मुझे इन-पेशेंट पुस्तिका प्राप्त हुई है </p>");
            b.Append("<p style='font-size:12px;'>मुझे रोगी अधिकारों और जिम्मेदारियों के बारे में शिक्षित किया गया है</p>");

            b.Append("<p style='width:40%;font-size:13px;float:left;'>Signature of Patient/Attendant<br>रोगी / परिचारक का हस्ताक्षर <br><br><br><br></p>");
            b.Append("<p style='width:40%;font-size:13px;float:right;text-align:right;'>Signature of Admitting Ofiicer<br>प्रवेश करने वाले अधिकारी का हस्ताक्षर <br><br><br><br> </p>");

            b.Append("<p style='font-size:15px;'><br><br><br></p>");
            b.Append("<p style='font-size:15px;'><b>DISCHARGE/DEATH DATE/TIME: ----------------------------------------------------------------------------------------------</b></p>");
            b.Append("<p style='font-size:15px;'><b>TYPE OF DISCHARGE: -------------------------------------------------------------(NORMAL/LAMA/ABSCONDED)</b></p>");
            b.Append("<p style='font-size:15px;'><b>RECEVIED IN MEDICAL RECORDS DATE/TIME: ------------------------------------------------------------------------</b></p>");
            b.Append("</fieldset>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 70;
            pdfConverter.PageMarginLeft = 20;
            pdfConverter.PageMarginRight = 15;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.HeaderSource = "AdmissionAndDischargeReport";
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "AdmissionAndDischargeReport.pdf");
        }

        public string PrintFormsList(List<ipFormPrintingRequest> FormList)
        {
            if (FormList != null)
            {
                Session["FormList"] = null;
            }
            Session["FormList"] = FormList;
            PrintForms();
            return "Success";
        }
        public FileResult PrintForms()
        {
            var FormList = Session["FormList"] as List<ipFormPrintingRequest>;
            PrintIPDForms obj = new PrintIPDForms();
            return obj.PrintForms(FormList);
        }


        public FileResult DeathNotification(string IPDNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            IPDInfo obj = new IPDInfo();
            obj.IPDNo = IPDNo;
            obj.Logic = "PrintDeathCertificate";
            obj.login_id = "-";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursingService/IPD_PatientQueries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string Certificateid = "";
            string PatientName = "";
            string ageInfo = "";
            string UHID = "";
            string IPDNoo = "";
            string fathername = "";
            string DoctorName = "";
            string SufferingFrom = "";
            string ImmediateCauseOfDeath = "";
            string address = "";
            string date = "";
            string time = "";

            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            b.Append("<div class='row' style='padding:10px'>");
            b.Append("<h1 style='text-align:center; color:black; text-transform:uppercase;margin-top:-2%'>Chandan Hospital</h1>");
            b.Append("<h1 style='text-align:center; font-size:13px;color:black;margin-top:-2%'> Faizabad Road, Near Chinhat Flyover, Vijayant Khand,Gomti Nagar Lucknow,Uttar Pradesh 226010</h1>");
            b.Append("<h1 style='text-align:center; font-size:13px;color:black;'> phone No : 0522-6666666 &nbsp; &nbsp;Email: care@chandanhospital.in</h1>");
            b.Append("<h1 style='text-align:center; font-size:16px; color:black;margin-top:2%'><u> DEATH  NOTIFICATION</u></h1>");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    PatientName = dr["patient_name"].ToString();
                    ageInfo = dr["ageInfo"].ToString();
                    UHID = dr["UHID"].ToString();
                    IPDNoo = dr["IPDNo"].ToString();
                }
                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {

                        Certificateid = dr["Certificateid"].ToString();
                        PatientName = dr["patient_name"].ToString();
                        fathername = dr["FatherName"].ToString();
                        ageInfo = dr["ageInfo"].ToString();
                        DoctorName = dr["DoctorName"].ToString();
                        address = dr["address"].ToString();
                        SufferingFrom = dr["SufferingFrom"].ToString();
                        ImmediateCauseOfDeath = dr["ImmediateCauseOfDeath"].ToString();
                        date = dr["DeathDate"].ToString();
                        time = dr["DeathTime"].ToString();
                    }
                }
            }


            b.Append("<table style='width:100%;font-size:15px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px'>");
            b.Append("<tr>");
            b.Append("<td style='text-align:left'><b>DCFNo:</b></td>");
            b.Append("<td style='text-align:left;margin-left:25px'>" + Certificateid + "</td>");
            b.Append("<td>&nbsp;</td>");
            b.Append("<td style='text-align:left'><b>Date:</b></td>");
            b.Append("<td style='text-align:left;margin-left:25px'>" + date + "</td>");
            b.Append("<td>&nbsp;</td>");
            b.Append("<td style='text-align:left'><b>UHID :</b></td>");
            b.Append("<td style='text-align:left;margin-left:25px'>" + UHID + "</td>");

            b.Append("</tr>");
            b.Append("</table>");
            b.Append("<hr style='border: 1px solid black;margin-top:3%'>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex'><span style='white-space:pre'>This is notify that : &nbsp;</span><span style='margin-left:20px;width:100%;border-bottom:1px dashed;display:block'><b>" + PatientName + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre'>&nbsp;</span><span style='width:80%;border-bottom:1px dashed;display:block'><b>" + fathername + "</b></span>&nbsp;<span style='white-space:pre'>Age:&nbsp;</span><span style='margin-left:20px;width:20%;border-bottom:1px dashed;display:block'><b>" + ageInfo + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>R/O: &nbsp;</span><span style='margin-left:10px;width:100%;border-bottom:1px dashed;display:block'><b>" + address + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>was Suffering from </span>&nbsp;<span style='margin-left:35px;width:100%;border-bottom:1px dashed;display:block'><b>" + SufferingFrom + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'> and was under my treatrment of </span>&nbsp;&nbsp;<span style='margin-left:80px;width:100%;border-bottom:1px dashed;display:block'><b>" + DoctorName + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>at Chandan Hospital  Faizabad Road,Hear High Count,Lucknow</span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>Immediate Cause of Death Was</span>&nbsp;<span style='margin-left:55px;width:100%;border-bottom:1px dashed;display:block'><b>" + ImmediateCauseOfDeath + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>At </span>&nbsp;<span style='margin-left:5px;width:20%;border-bottom:1px dashed;display:block'><b>" + time + "</b></span> &nbsp;<span style='white-space:pre'>on Dated : &nbsp;</span><span style='margin-left:20px;width:80%;border-bottom:1px dashed;display:block'><b>" + date + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:10%'><span style='width:60%;'><b> Signature of Patient Attendant</b></span><span style='width:40%;'><b>Signature of Death Declaring Doctor</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='width:60%;'><b>Patient Attendant Name</b></span><span style='width:40%;'><b>" + DoctorName + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:6%'><span style='width:60%;'><b></b></span><span style='width:40%'><b> Counter Signature <br>Name..........................</b></span></p>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "DeathNotification.pdf");
        }

        public FileResult BirthCertificate(string IPDNo)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            InsertBirthCertificate obj = new InsertBirthCertificate();
            obj.IPDNO = IPDNo;
            obj.Logic = "PrintBirthCertificate";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursingService/IPD_PatientQueries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string BabyName = "";
            string GuardianName = "";
            string Gender = "";
            string Address = "";
            string DeliveryTime = "";
            string Height = "";
            string Weight = "";
            string BirthCertificateid = "";
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            b.Append("<div class='row' style='padding:10px'>");
            b.Append("<h2 style='text-align:center; color:black; text-transform:uppercase;'>Chandan Hospital</h2>");
            b.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            b.Append("<h1 style='text-align:center; font-size:13px;color:black;margin-top:-9%;margin-left:150px'> Faizabad Road, Near Chinhat Flyover, Vijayant Khand,Gomti Nagar Lucknow,Uttar Pradesh 226010</h1>");
            b.Append("<h1 style='text-align:left; font-size:13px;color:black;margin-left:175px;'> phone No : 0522-6666666 </h1>");
            b.Append("<h1 style='text-align:left; font-size:13px;color:black;margin-left:175px;'>care@chandanhospital.in &nbsp; &nbsp;&nbsp;&nbsp;Website:-www.chandanhospital.in</h1>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    BirthCertificateid = dr["BirthCertificateid"].ToString();
                    BabyName = dr["BabyName"].ToString();
                    GuardianName = dr["GuardianName"].ToString();
                    Gender = dr["Gender"].ToString();
                    Address = dr["Address"].ToString();
                    //DeliveryTime = dr["DeliveryTime"].ToString();
                    DateTime BirthDateTime = Convert.ToDateTime(dr["DeliveryTime"]);
                    DeliveryTime = BirthDateTime.ToString("dd/MM/yyyy HH:mm");

                    Height = dr["Height"].ToString();
                    Weight = dr["Weight"].ToString();
                }

            }
            b.Append("<hr style='border: 1px solid black;margin-top:3%'>");
            b.Append("<h1 style='text-align:center; font-size:17px; color:black;margin-top:1%'><u> BIRTH CERTIFICATE</u></h1>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>BIRTH NO:</span><span style='margin-left:20px;width:100%;'><b>" + BirthCertificateid + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>Name:&nbsp;</span>&nbsp;&nbsp;<span style='margin-left:25px;width:100%;border-bottom:1px dashed;display:block'><b>" + BabyName + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre'>S/O&nbsp;/&nbsp;D/O</span><span style='margin-left:20px;width:60%;border-bottom:1px dashed;display:block'><b>" + GuardianName + "</b></span>&nbsp;<span style='white-space:pre'>Gender:&nbsp;</span><span style='margin-left:20px;width:40%;border-bottom:1px dashed;display:block'><b>" + Gender + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>Address &nbsp;</span><span style='margin-left:20px;width:100%;border-bottom:1px dashed;display:block'><b>" + Address + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>Date/Time</span>&nbsp;<span style='margin-left:25px;width:40%;border-bottom:1px dashed;display:block'><b>" + DeliveryTime + "</b></span>&nbsp;<span style='white-space:pre;text-align:center;'>&nbsp;Weight</span>&nbsp;<span style='margin-left:30px;width:30%;border-bottom:1px dashed;display:block'><b>" + Weight + "</b></span><span style='white-space:pre;text-align:center;'>&nbsp;Height</span><span style='margin-left:30px;width:30%;border-bottom:1px dashed;display:block'><b>" + Height + "</b></span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-1%'><span style='white-space:pre;'>at Chandan Hospital ,Lucknow</span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:85%'><span style='width:30%;'>___________________________</span>&nbsp;<span style='margin-left:30px;width:30%;'>___________________________</span>&nbsp;<span style='margin-left:30px;width:30%;'>___________________________</span></p>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:-2%'><span style='width:30%;'><b>Neonatologist</b></span>&nbsp;<span style='margin-left:30px;width:30%;'><b>Obstetrician</b></span>&nbsp;<span style='margin-left:35px;width:30%;'><b>Medical Director</b></span></p>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "BirthCertificate.pdf");
        }
        public FileResult SofaScorePrint(string IPDNO, string EntryDate)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            InSofaScoreQueries obj = new InSofaScoreQueries();
            obj.ObservationId = "-";
            obj.Sofasystem = "-";
            obj.ObservationName = "-";
            obj.Value = "-";
            obj.from = "1900-01-01";
            obj.to = "1900-01-01";
            obj.Prm1 = IPDNO;
            obj.Prm2 = EntryDate;
            obj.Logic = "Get:SofaScoreSheet";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/SOFA_ScoreQueries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string MortalityPerc = "";
            string Score = "";
            string UHID = "";
            string IPDNo = "";
            string patient_name = "";
            string emp_name = "";
            string ageInfo = "";
            string entrydate = "";
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                MortalityPerc = dr["MortalityPerc"].ToString();
                Score = dr["Score"].ToString();
                entrydate = dr["EntryDate"].ToString();
                UHID = dr["UHID"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                emp_name = dr["emp_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
            }
            b.Append("<div class='row' style='padding:10px'>");
            b.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            b.Append("<h5 style = 'border:1px solid;margin-left:300px;margin-right:130px;text-align: justify ; margin-top:-70px; width:700px;height:60px; padding:10px;'>");
            b.Append("<table style='width:100%;font-size:16px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;'>");
            b.Append("<tr>");
            b.Append("<td><b>Patient Name</b> </td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + patient_name + "</td>");
            b.Append("<td><b>Age/Gender</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + ageInfo + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>UHID No</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + UHID.ToString() + "</td>");
            b.Append("<td><b>IPD No</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + IPDNo.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</h5>");
            b.Append("<h1 style='text-align:center; font-size:25px; color:black;margin-top:2%'><u> SOFA SCORE</u></h1>");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                b.Append("<table border='1' style='width:98%;text-align:left;border-collapse:collapse;margin-top:-5px;white-space:nowrap;height:300px;background-color:#fff;'>");
                b.Append("<tr style='background-color:#fff;'>");
                b.Append("<th colspan='8' style='text-align:center; padding:10px;font-size:20px'><u> SOFA SCORE</u></th>");
                b.Append("</tr>");
                // Display dynamic header row outside the loop
                b.Append("<tr style='color:#000;background-color:#ddd;'>");

                // Display specific columns (Sofa Score, 0, 1, 2, 3, 4)
                b.Append("<th style='text-align:left; padding:5px;'>Sofa Score</th>");
                for (int i = 0; i <= 4; i++)
                {
                    b.Append("<th style='text-align:center; padding:5px;'>" + i.ToString() + "</th>");
                }
                b.Append("<th style='text-align:center; padding:5px;'>Input Value</th>");

                b.Append("<th style='text-align:center; padding:5px;'> Score</th>");
                b.Append("</tr>");

                //Display data rows
                foreach (DataRow drr in ds.Tables[0].Rows)
                {
                    b.Append("<tr>");

                    // Display specific columns (Sofa Score, 0, 1, 2, 3, 4)
                    b.Append("<td style='text-align:left; padding:10px;'><strong>" + (drr[3] != null ? drr[2].ToString() : "") + "</strong></td>");
                    for (int i = 3; i <= 9; i++)
                    {
                        b.Append("<td style='text-align:center; padding:10px; " + ((i == 8 || i == 9) ? "font-weight:bold;" : "") + "border: 1px solid #000;'>" + (drr[i] != null ? drr[i].ToString() : "") + "</td>");
                    }
                    b.Append("</tr>");
                }
            }
            else
            {
                b.Append("<div style='color:red; font-size:18px;'>No data available.</div>");
            }

            b.Append("<table border='1'style='width:40%;text-align:center;border-collapse:collapse;margin-top:35px;height:300px;background-color:#fff;margin-left:100px'>");
            b.Append("<thead>");
            b.Append("<tr style='color:#000;background-color:#ddd;padding:15px'>");
            b.Append("<th style='padding:8px'> Mortality</th>");
            b.Append("<th style='padding:8px'>Sofa Score</th>");
            b.Append("</tr>");
            b.Append("</thead>");
            b.Append("<tbody>");
            b.Append("<tr style='background-color: #fff;'>");
            b.Append("<td class='button-column'>< 10%</td>");
            b.Append("<td class='button-column'>0-6</td>");
            b.Append("</tr>");
            b.Append("<tr style='background-color: #fff;'>");
            b.Append("<td class=\"button-column\">15-20%</td>");
            b.Append("<td class=\"button-column\">7-9</td>");
            b.Append("</tr>");
            b.Append("<tr style='background-color: #fff;'>");
            b.Append("<td class=\"button-column\">40-50%</td>");
            b.Append("<td class=\"button-column\">10--12</td>");
            b.Append("</tr>");
            b.Append("<tr style='background-color: #fff;'>");
            b.Append("<td class=\"button-column\">50-60%</td>");
            b.Append("<td class=\"button-column\">13-14</td>");
            b.Append("</tr>");
            b.Append("<tr style='background-color: #fff;'>");
            b.Append("<td class=\"button-column\">>80%</td>");
            b.Append("<td class=\"button-column\">15</td>");
            b.Append("</tr>");
            b.Append("<tr style='background-color: #fff;'>");
            b.Append("<td class=\"button-column\">>90%</td>");
            b.Append("<td class=\"button-column\">15-26</td>");
            b.Append("</tr>");
            b.Append("</tbody>");
            b.Append("</table>");
            b.Append("<table style='background:#f0f0f0;width:40%;font-size:18px;border:0px solid #dcdcdc;float:right;margin-top:-250px;margin-left:-200px'>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'><b>Total Score :</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + Score + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<br>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'><b>Mortality :</b></td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + MortalityPerc + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("<br>");
            b.Append("<br>");

            b.Append("<table  style='font-size:18px;text-align:left'>");
            b.Append("<tr>");
            b.Append("<td><b>Name Of Staff </b></td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td>:</td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td style='text-aligh:left'>" + emp_name + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>Signature </b></td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td>:</td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>Date </b></td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td>:</td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td style='text-aligh:left'>" + entrydate + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "SofaScore.pdf");
        }

        public FileResult PrintApacheSofaScore(string IPDNO, string EntryDate)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            InSofaScoreQueries obj = new InSofaScoreQueries();
            obj.ObservationId = "-";
            obj.Sofasystem = "-";
            obj.ObservationName = "-";
            obj.Value = IPDNO;
            obj.from = "1900-01-01";
            obj.to = "1900-01-01";
            obj.Prm1 = "-";
            obj.Prm2 = EntryDate;
            obj.Logic = "PrintApacheSofaScore";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDNursing/SOFA_ScoreQueries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string UHID = "";
            string IPDNo = "";
            string patient_name = "";
            string emp_name = "";
            string ageInfo = "";
            string AGE = "";
            string GlasgowComaScore = "";
            string Potassium = "";
            string IsRenalFailure = "";
            string Hematocrit = "";
            string IsChronic = "";
            string ChronicDisease = "";
            string TEMP = "";
            string MAP = "";
            string HR = "";
            string RR = "";
            string FiO2 = "";
            string Po2 = "";
            string PCO2 = "";
            string ArtPH = "";
            string NA = "";
            string Cr = "";
            string HT = "";
            string WBC = "";
            string aps_Score = "";
            string EstMortRate = "";
            string entryDate = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UHID = dr["UHID"].ToString();
                IPDNo = dr["IPDNo"].ToString();
                patient_name = dr["patient_name"].ToString();
                emp_name = dr["emp_name"].ToString();
                ageInfo = dr["ageInfo"].ToString();
            }
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                AGE = dr["AGE"].ToString();
                GlasgowComaScore = dr["GlasgowComaScore"].ToString();
                Potassium = dr["Potassium"].ToString();
                IsRenalFailure = dr["IsRenalFailure"].ToString();
                Hematocrit = dr["Hematocrit"].ToString();
                IsChronic = dr["IsChronic"].ToString();
                ChronicDisease = dr["ChronicDisease"].ToString();
                TEMP = dr["TEMP"].ToString();
                MAP = dr["MAP"].ToString();
                HR = dr["HR"].ToString();
                RR = dr["RR"].ToString();
                FiO2 = dr["FiO2"].ToString();
                Po2 = dr["Po2"].ToString();
                PCO2 = dr["PCO2"].ToString();
                ArtPH = dr["ArtPH"].ToString();
                NA = dr["NA"].ToString();
                Cr = dr["Cr"].ToString();
                HT = dr["HT"].ToString();
                WBC = dr["WBC"].ToString();
                EstMortRate = dr["EstMortRate"].ToString();
                aps_Score = dr["aps_Score"].ToString();
                entryDate = dr["entryDate"].ToString();
            }
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            b.Append("<div class='row' style='padding:10px'>");
            b.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            b.Append("<h5 style = 'border:1px solid;margin-left:200px;text-align: justify ; margin-top:-80px; width:500px;height:60px; padding:10px;'>");
            b.Append("<table style='width:100%;font-size:14px;text-align:left;border:0px solid #dcdcdc;margin-bottom:-15px;'>");
            b.Append("<tr>");
            b.Append("<td><b>Patient Name</b> </td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + patient_name + "</td>");
            b.Append("<td><b>Age/Gender</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + ageInfo + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>UHID No</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + UHID.ToString() + "</td>");
            b.Append("<td><b>IPD No</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + IPDNo.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</h5>");
            b.Append("</div>");

            b.Append("<h1 style='text-align:center; font-size:16px; color:black;margin-top:-10px'><u> Apache Sofa Score</u></h1>");
            b.Append("<div class='row' style='padding:20px'>");

            b.Append("<div style = 'border: 1px solid #b1aaaa;text-align: justify ; width:53%;height:400px;font-size:18px;margin-top:-10px'>");
            b.Append("<table width='100%' border='0' style='padding:5px'>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Temperature (°F)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + TEMP + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>MAP (mmHg)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + MAP + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>HR (/min)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + HR + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>RR (/min)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + RR + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>FiO2 &gt;=50%  <b>/</b> FiO2&lt;50%</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + FiO2 + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Arterial pH  <b>/</b> HCO3-</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + ArtPH + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Na+ (mEq/L)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + NA + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");


            b.Append("<tr>");
            b.Append("<td style='width:12%;'>K+ (mEq/L)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + Potassium + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Creatinine (mg/dL) </td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + Cr + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>ARF </td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + IsRenalFailure + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>WBC (x1000/mm3) </td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + HT + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");

            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Ht (%) </td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + WBC + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Glasgow (/15)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + GlasgowComaScore + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Age (ans)</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + AGE + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Chronic Disease</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + IsChronic + " <b>/</b> &nbsp;" + ChronicDisease + "</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");

            b.Append("<div style = 'border: 1px solid #b1aaaa;text-align: justify ; width:43%;height:80px;font-size:18px;float:right;margin-top:-352px;background-color:#f3f3f3;'>");
            b.Append("<table width='100%' border='0' style='padding:5px'>");
            b.Append("<tr>");
            b.Append("<td></td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>APACHE II Score</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + aps_Score + " &nbsp; /71</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td style='width:12%;'>Mortality Rate</td>");
            b.Append("<td style='width:2%;'><b>:</b></td>");
            b.Append("<td style='width:8%;text-aligh:left'>" + EstMortRate + " &nbsp; %</td>");
            b.Append("<td style='width:1%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");

            b.Append("<table  style='font-size:16px;text-align:left;margin-top:50px'>");
            b.Append("<tr>");
            b.Append("<td><b>Name Of Staff </b></td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td>:</td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td style='text-aligh:left'>" + emp_name + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>Signature </b></td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td>:</td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>Date </b></td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td>:</td>");
            b.Append("<td style='width:5%;'>&nbsp;</td>");
            b.Append("<td style='text-aligh:left'>" + entryDate + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = false;
            pdfConverter.Header_Hight = 150;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";

            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "ApacheSofaScore.pdf");
        }
        public FileResult PrintEstimateForm(string UHIDNO, string estDate)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            IPD_Estimates objBO = new IPD_Estimates();
            objBO.hospId = "-";
            objBO.estDate = estDate;
            objBO.estimateNo = "-";
            objBO.uhid = UHIDNO;
            objBO.patientName = "-";
            objBO.isActive = '-';
            objBO.createdBy = "-";
            objBO.toWhom = "-";
            objBO.amount = "0";
            objBO.estContent = "-";
            objBO.var_list = "-";
            objBO.result = "-";
            objBO.Logic = "PrintEstimate";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("IPDBilling/IPD_EstimateQueries", objBO);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            string ipopno = "";
            string Patientname = "";
            string age = "";
            string content = string.Empty;
            string Gender = string.Empty;
            string EstimateNo = string.Empty;
            string AccountName = string.Empty;
            string AccountNo = string.Empty;
            string BankName = string.Empty;
            string BankBranch = string.Empty;
            string IFSCcode = string.Empty;
            string Company = string.Empty;
            string MobileNo = string.Empty;
            string Amount = string.Empty;
            string toWhom = string.Empty;
            string estimateNo = string.Empty;
            string var_list = string.Empty;
            string Dated = string.Empty;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    AccountName = dr["ac_name"].ToString();
                    IFSCcode = dr["ac_ifsc"].ToString();
                    AccountNo = dr["ac_no"].ToString();
                    BankName = dr["ac_bankName"].ToString();
                    BankBranch = dr["ac_BranchName"].ToString();

                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    Patientname = dr["patient_name"].ToString();
                    Gender = dr["gender"].ToString();
                    age = dr["ageInfo"].ToString();
                    ipopno = dr["ipop_no"].ToString();
                }
            }
            string imagePath = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/logo/ChandanLogo.jpg");
            b.Append("<div class='row' style='padding:30px'>");
            b.Append("<img src='" + imagePath + "' style='width:150px; height:70px;' />");
            b.Append("<h5 style = 'border:1px solid;margin-left:200px;text-align: justify ; margin-top:-90px; width:500px;height:60px; padding:10px;'>");
            b.Append("<table style='width:100%;font-size:14px;border:0px solid #dcdcdc;margin-bottom:-15px;'>");
            b.Append("<tr>");
            b.Append("<td><b>Patient Name</b> </td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + Patientname + "</td>");
            b.Append("<td><b>Age/Gender</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + age + "</td>");
            b.Append("</tr>");
            b.Append("<tr>");
            b.Append("<td><b>UHID No</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + UHIDNO.ToString() + "</td>");
            b.Append("<td><b>IPD No</b></td>");
            b.Append("<td><b>:</b></td>");
            b.Append("<td>" + ipopno.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("</h5>");

            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    Dated = dr["estDate"].ToString();
                    EstimateNo = dr["estimateNo"].ToString();
                    toWhom = dr["toWhom"].ToString();
                    Amount = dr["amount"].ToString();
                    content = dr["estContent"].ToString();
                    foreach (var var1 in dr["var_list"].ToString().Split(','))
                    {
                        var oldString = "{<strong>" + var1 + "</strong>}";
                        var newString = "<strong>" + var1 + "</strong>";
                        if (var1 == "PatientName")
                            content = content.Replace(oldString, "<strong>" + Patientname + "</strong>");
                        if (var1 == "Age")
                            content = content.Replace(oldString, "<strong>" + age + "</strong>");
                        if (var1 == "Gender")
                            content = content.Replace(oldString, "<strong>" + Gender + "</strong>");
                        if (var1 == "UHIDNO")
                            content = content.Replace(oldString, "<strong>" + UHIDNO + "</strong>");
                        if (var1 == "Dated")
                            content = content.Replace(oldString, "<strong>" + Dated + "</strong>");
                        if (var1 == "EstimateNo")
                            content = content.Replace(oldString, "<strong>" + EstimateNo + "</strong>");
                        if (var1 == "approx(amount)")
                            content = content.Replace(oldString, "<strong>" + Amount + "</strong>");
                        if (var1 == "AccountName")
                            content = content.Replace(oldString, "<strong>" + AccountName + "</strong>");
                        if (var1 == "AccountNo")
                            content = content.Replace(oldString, "<strong>" + AccountNo + "</strong>");
                        if (var1 == "BankName")
                            content = content.Replace(oldString, "<strong>" + BankName + "</strong>");
                        if (var1 == "BankBranch")
                            content = content.Replace(oldString, "<strong>" + BankBranch + "</strong>");
                        if (var1 == "IFSCcode")
                            content = content.Replace(oldString, "<strong>" + IFSCcode + "</strong>");
                    }
                    b.Append(content);
                }
            }
            b.Append("</div>");
            pdfConverter.Header_Enabled = false;
            pdfConverter.Footer_Enabled = true;
            pdfConverter.Footer_Hight = 17;
            pdfConverter.Header_Hight = 35;
            pdfConverter.PageMarginLeft = 10;
            pdfConverter.PageMarginRight = 10;
            pdfConverter.PageMarginBottom = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageMarginTop = 10;
            pdfConverter.PageName = "A4";
            pdfConverter.PageOrientation = "Portrait";
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "Estimate.pdf");
        }
    }
}