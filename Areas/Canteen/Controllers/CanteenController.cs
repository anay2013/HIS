using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Canteen.Controllers
{
    public class CanteenController : Controller
    {
        // GET: Canteen/Canteen
        public ActionResult ReceivablePayment()
        {
            return View();
        }
    }
}