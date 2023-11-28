using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.Lab.Controllers
{
    public class ReportController : Controller
    {        
        public ActionResult InvestigationReport()
        {
            return View();
        }
        public ActionResult LabWorkBook()
        {
            return View();
        }
    }
}