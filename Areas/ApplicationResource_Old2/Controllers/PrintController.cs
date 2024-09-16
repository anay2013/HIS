
using HIS.Repository;
using HISWebApi.Models;
using MediSoftTech_HIS.App_Start;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.ApplicationResource.Controllers
{
    public class PrintController : Controller
    {
        public FileResult PrintDocInfo(string inputValue)
        {
            PdfGenerator pdfConverter = new PdfGenerator();
            FormDocInfo obj = new FormDocInfo();
            obj.AutoId = 0;
            obj.Prm1 = inputValue;
            obj.Prm2 = "-";
            obj.login_id = "-";
            obj.Logic = "PrintDocInfo";
            HISWebApi.Models.dataSet dsResult = APIProxy.CallWebApiMethod("ApplicationResource/IT_FormDocQueries", obj);

            DataSet ds = dsResult.ResultSet;
            StringBuilder b = new StringBuilder();
            string FormName = string.Empty;
            string FormDescription = string.Empty;
            string GroupName = string.Empty;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (FormName != dr["FormName"].ToString())
                    {
                        b.Append("<h2 style='text-align:center;font-weight:bold;margin-bottom:5px;font-family: Sans-serif; '>" + dr["FormName"].ToString() + "</h2>");
                        b.Append("<h3 style='border:1px solid #000;padding:10px;text-align:center;font-size:14px;margin-bottom:5px;font-family: Sans-serif; '>" + dr["FormDescription"].ToString() + "</h3>");
                        b.Append("<hr/>");
                        FormName = dr["FormName"].ToString();
                    }
                    if (GroupName != dr["GroupName"].ToString())
                    {
                        b.Append("<h3 style='background:#ddd;padding:6px;font-size:13px;font-family: Sans-serif; '>" + dr["GroupName"].ToString() + "</h3>");
                        GroupName = dr["GroupName"].ToString();
                    }
                    b.Append("<ul style='margin:0'>");
                    b.Append("<li style='margin:-1px 0;'>" + dr["textContent"].ToString() + "</li>");
                    b.Append("</ul>");

                }
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
            return pdfConverter.ConvertToPdf("-", b.ToString(), "-", "IT_FormDocInfo.pdf");
        }
    }
}