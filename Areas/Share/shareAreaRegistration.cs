using System.Web.Mvc;

namespace MediSoftTech_HIS.Areas.share
{
    public class shareAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "share";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "share_default",
                "share/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}