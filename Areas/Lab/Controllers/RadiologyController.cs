using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Lab.Controllers
{
    public class RadiologyController : Controller
    {
        // GET: Lab/Radiology
        public ActionResult TaxReportEditing()
        {
            return View();
        }
    }
}