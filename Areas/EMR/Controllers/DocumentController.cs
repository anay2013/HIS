using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.EMR.Controllers
{
    public class DocumentController : Controller
    {      
        public ActionResult CaseSheet()
        {
            return View();
        }
    }
}