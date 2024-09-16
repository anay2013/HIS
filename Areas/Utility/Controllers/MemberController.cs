using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Utility.Controllers
{
    public class MemberController : Controller
    {
        // GET: Utility/Member
        public ActionResult NewMember()
        {
            return View();
        }
		public ActionResult AvailCoupon()
		{
			return View();
		}
        public ActionResult SalarySlip()
        {
            return View();
        }
        public ActionResult ABDMCallBack()
        {
            string rawBody = GetDocumentContents(Request);
            dynamic eventObj = JsonConvert.DeserializeObject(rawBody);
            return Content("Success!");
        }
        private string GetDocumentContents(HttpRequestBase Request)
        {
            string documentContents;
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }
    }
}