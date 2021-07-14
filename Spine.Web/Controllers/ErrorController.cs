using System.Web.Mvc;

namespace Spine.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Page401()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Page403()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Page404()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Page500()
        {
            return View();
        }
    }
}