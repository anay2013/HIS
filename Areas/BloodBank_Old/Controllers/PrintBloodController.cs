using HIS.Repository;
using MediSoftTech_HIS.App_Start;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.Mvc;
using static HISWebApi.Models.BloodBank;
namespace MediSoftTech_HIS.Areas.BloodBank.Controllers
{
    public class PrintBloodController : Controller
    {
        // GET: BloodBank/PrintBlood
        public ActionResult Index()
        {
            return View();
        }

        public FileResult BloodBankPrint(string visit_id)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            SelectQueriesInfo obj = new SelectQueriesInfo();
            obj.VisitId = visit_id;
            obj.Logic = "Print:DonorForm";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("BloodBank/BB_SelectQueries", obj);
            DataSet ds = dsResult.ResultSet;
            string _result = string.Empty;
            StringBuilder b = new StringBuilder();
            StringBuilder h = new StringBuilder();
            //  one table varible
            string Donarid = "";
            string DonorName = "";
            string Age = "";
            string AadharNo = "";
            string DonateTo = "";
            string Gender = "";
            string Address = "";
            string ContactNo = "";
            string Email = "";
            // two table varible
            string Weight = "";
            string Pulse = "";
            string Bp = "";
            string Hb = "";
            string Temprature = "";
            string Fitvalue = "";

            string imagePath = string.Empty;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    imagePath = ConfigurationManager.AppSettings["documentServerUrl"].ToString() + dr["photo_path"].ToString();
                    Donarid = dr["donor_id"].ToString();
                    DonorName = dr["donorName"].ToString();
                    Age = dr["age"].ToString();
                    AadharNo = dr["aadharNo"].ToString();
                    DonateTo = dr["donate_to"].ToString();
                    Gender = dr["Gender"].ToString();
                    Address = dr["Address"].ToString();
                    ContactNo = dr["contactNo"].ToString();
                    Email = dr["Email"].ToString();
                }
                if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {

                        Weight = dr["Weight"].ToString();
                        Pulse = dr["Pulse"].ToString();
                        Bp = dr["Blood_Pressure"].ToString();
                        Hb = dr["Hb"].ToString();
                        Temprature = dr["Temprature"].ToString();
                        var IsFit = dr["isFit"].ToString();
                        if (IsFit == "Y")
                        {
                            Fitvalue = "Fit";
                        }
                        else
                        {

                        }


                    }
                }
            }
            b.Append("<div style='width:95%;padding:10px'>");

            b.Append("<fieldset style='height:2000px; width:98%; border:1px solid black;margin-top:2%'>");
            b.Append("<legend style='font-size:15px;text-align:Center;border-style:double;height:25px;width:300px; background-color:white'><b>Donor Registration</b></legend>");


            b.Append("<p style='font-size:16px; width:104%;display:flex;margin-left:-2%;'><span style='width:100%;border-bottom:1px solid;display:block;margin-top:3%'><b></b></span></p>");
            b.Append("<div style='font-size:15px;text-align:left;margin-top:-2%'><b>Demographic Details :-</b></div>");

            b.Append("<table style='width:80%;font-size:15px;text-align:left;'>");
            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:10%'>Donor ID </td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:17%'>" + Donarid.ToString() + "</td>");

            b.Append("<td style='text-align:left; width:8%'>Visit ID</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:15%'>" + visit_id.ToString() + "</td>");
            b.Append("</tr>");

            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:10%'>Donor</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:17%'>" + DonorName.ToString() + "</td>");
            b.Append("<td style='text-align:left; width:8%'>Age</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:15%'>" + Age.ToString() + "</td>");
            b.Append("</tr>");


            b.Append("<tr style='padding:3px;'colspan='3'>");
            b.Append("<td style='text-align:left; width:10%'>Gender</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:15%'>" + Gender.ToString() + "</td>");
            b.Append("<td style='text-align:left; width:8%'>Aadhar No.</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:15%'>" + AadharNo.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("<tr style='padding:3px;'colspan='3'>");
            b.Append("<td style='text-align:left; width:10%'>Donate To</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:15%'>" + DonateTo.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("</table>");
            b.Append("<img src='" + imagePath + "' style='width:18%; height:100px;margin-top:-10%; margin-left:80%;border:1px solid black' />");
            b.Append("<p style='font-size:16px; width:104%;display:flex;margin-left:-2%;'><span style='width:100%;border-bottom:1px solid;display:block;'><b></b></span></p>");
            b.Append("<div style='font-size:15px;text-align:left;margin-top:-2%'><b>Contact Details :-</b></div>");

            b.Append("<table style='width:80%;font-size:15px;text-align:left;'>");
            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:15%'>Address</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:85%'>" + Address.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("<tr style='padding:3px;' colspan='3'>");
            b.Append("<td style='text-align:left; width:15%'>Contact No</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:10%'>" + ContactNo.ToString() + "</td>");
            b.Append("<td style='text-align:left; width:10%'>Email</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:15%'>" + Email.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("</table>");

            b.Append("<p style='font-size:16px; width:104%;display:flex;margin-left:-2%;'><span style='width:100%;border-bottom:1px solid;display:block;'><b></b></span></p>");
            b.Append("<div style='font-size:15px;text-align:left; margin-top:-2%'><b>Donation Examination :-</b></div>");

            b.Append("<table style='width:80%;font-size:15px;text-align:left;'>");
            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:14%'>Weight</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:35%'>" + Weight.ToString() + "</td>");
            b.Append("</tr>");

            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:14%'>Pulse</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:35%'>" + Pulse.ToString() + "</td>");
            b.Append("</tr>");

            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:14%'>BP</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:35%'>" + Bp.ToString() + "</td>");
            b.Append("</tr>");

            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:14%'>HB</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:35%'>" + Hb.ToString() + "</td>");
            b.Append("</tr>");

            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:14%'>Temp</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:35%'>" + Temprature.ToString() + "</td>"); ;
            b.Append("</tr>");

            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left; width:14%'>Medical Examination</td>");
            b.Append("<td style='width:1%'><b>:</b></td>");
            b.Append("<td style='text-align:left; width:35%'>" + Fitvalue.ToString() + "</td>");
            b.Append("</tr>");
            b.Append("</table>");

            b.Append("<p style='font-size:16px; width:104%;display:flex;margin-left:-2%;'><span style='width:100%;border-bottom:1px solid;display:block;'><b></b></span></p>");
            b.Append("<div style='font-size:15px;text-align:left;margin-top:-2%'><b>Questionnair :-</b></div>");
            b.Append("<table style='width:95%;font-size:15px;text-align:left;'>");
            b.Append("<tr style='padding:3px;'>");
            b.Append("<td style='text-align:left;font-weight:bold; width:90%'><u>Question</u></td>");
            b.Append("<td style='text-align:left;font-weight:bold; width:10%'><u>Answer</u></td>");
            b.Append("</tr>");

            if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr3 in ds.Tables[2].Rows)
                {
                    b.Append("<tr style='padding:3px;'>");
                    b.Append("<td style='text-align:left; width:90%'>" + dr3["Question"].ToString() + "</td>");
                    b.Append("<td style='text-align:left; width:10%'>" + dr3["Answer"].ToString() + "</td>");
                    b.Append("</tr>");



                }
            }
            b.Append("</table>");
            b.Append("<p style='font-size:16px;float:left; width:100%;display:flex;margin-top:45%;margin-left:10px'><span style='width:30%;'><b>Donor Sign</b></span>&nbsp;<span style='margin-left:30px;width:30%;'><b>Technician Sign</b></span>&nbsp;<span style='margin-left:35px;width:30%;'><b>Blood Bank Incharge</b></span></p>");
            b.Append("</fieldset>");
            b.Append("</div>");



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
            return pdfConverter.ConvertToPdf(h.ToString(), b.ToString(), "-", "BloodBank.pdf");
        }
    }
}