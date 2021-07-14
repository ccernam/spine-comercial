using System.Web.Mvc;

namespace Spine.Web.Areas.Aud
{
    public class AudAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Aud";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //    "Aud_default",
            //    "Aud/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
                "Aud_default",
                "Aud/{controller}/{action}",
                new { action = "Index" }
            );
        }
    }
}