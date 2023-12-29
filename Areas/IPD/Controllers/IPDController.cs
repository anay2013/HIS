using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.IPD.Controllers
{
    public class IPDController : Controller
    {
        public ActionResult IPD_OverDuePatientList()
        {
            return View();
        }
        public ActionResult IPDRegistration()
        {
            return View();
        }
        public ActionResult IPD_NursingPatientRegister()
        {
            return View();
        }
        public ActionResult GatewayNursing()
        {
            return View();
        }
        public ActionResult IPD_BillingPatientRegister()
        {
            return View();
        }
        public ActionResult GatewayBilling()
        {
            return View();
        }
        public ActionResult IPD_DoctorPatientRegister()
        {
            return View();
        }
        public ActionResult GatewayDoctor()
        {
            return View();
        }
        public ActionResult IPDBody(string page)
        {
            return PartialView(page);
        }
        public ActionResult ApprovalCorrection()
        {
            return View();
        }
        public ActionResult TPAApproval()
        {
            return View();
        }
        public ActionResult BloodRequisition()
        {
            return View();
        }
        public ActionResult IPDPatientRegister()
        {
            return View();
        }
        public ActionResult IPDAdvancePayment()
        {
            return View();
        }
        public ActionResult HospitalDocuments()
        {
            return View();
        }

        public ActionResult ClosingPending()
        {
            return View();
        }

        public ActionResult AllowBillCorrection()
        {
            return View();
        }
    }
}