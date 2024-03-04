using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.IPD.Controllers
{
    public class CQIController : Controller
    {
        public ActionResult CQIEntry()
        {
            return View();
        }
        public ActionResult OpenView(string page)
        {
            return PartialView(page);
        }
    }
}