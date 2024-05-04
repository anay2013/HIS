using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.MIS.Controllers
{
    public class FinanceController : Controller
    {     
        public ActionResult UserwiseCollectionReport()
        {
            return View();
        }
        public ActionResult DailyCollectionReport()
        {
            return View();
        }
        public ActionResult ReceiptWiseCollectionReport()
        {
            return View();
        }
        public ActionResult DailyWorkReport()
        {
            return View();
        }
        public ActionResult BugAudit()
        {
            return View();
        }
        public ActionResult ReceiptInfo()
        {
            return View();
        }
    }
}