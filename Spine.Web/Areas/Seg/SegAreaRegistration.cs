using System.Web.Mvc;

namespace Spine.Web.Areas.Seg
{
    public class SegAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Seg";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Seg_default",
                "Seg/{controller}/{action}",
                new { action = "Index" }
            );
        }
    }
}