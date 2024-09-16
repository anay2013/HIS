using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.share.Controllers
{
    public class ShareController : Controller
    {
        // GET: Share/Share
        public ActionResult Share_SubcatEmplink()
        {
            return View();
        }
        public ActionResult Share_DoctorShift()
        {
            return View();
        }
        public ActionResult Share_ConsumableDeduction()
        {
            return View();
        }
        public ActionResult Share_AdlAmount()
        {
            return View();
        }
    }
}