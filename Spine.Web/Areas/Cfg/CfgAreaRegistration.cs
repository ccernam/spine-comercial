using System.Web.Mvc;

namespace Spine.Web.Areas.Cfg
{
    public class CfgAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cfg";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cfg_default",
                "Cfg/{controller}/{action}",
                new { action = "Index" }
            );
        }
    }
}