using System.Web.Mvc;

namespace Spine.Web.Areas.Cmn
{
    public class CmnAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Cmn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Cmn_default",
            //    "Cmn/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Cmn_default",
                "Cmn/{controller}/{action}",
                new { action = "Index" }
            );


        }
    }
}