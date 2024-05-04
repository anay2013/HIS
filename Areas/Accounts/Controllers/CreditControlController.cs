using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Accounts.Controllers
{
    public class CreditControlController : Controller
    {    
        public ActionResult OverALLReceivable()
        {
            return View();
        }
        public ActionResult OverALLTDSReceivable()
        {
            return View();
        }
        public ActionResult AgingReportPanelWise()
        {
            return View();
        }
    }
}