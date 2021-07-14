using Spine.Web.Clases;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Spine.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfig.RegisterDependencies();

            ConstanteConfig.RegistrarAcciones();
            ConstanteConfig.RegistrarEntidades();
            ConstanteConfig.RegistrarCatalogo();
            ConstanteConfig.RegistrarCatalogoItem();
            ConstanteConfig.RegistrarParametro();
        }

        //protected void Application_EndRequest()
        //{
        //    var vobjContext = new HttpContextWrapper(Context);
        //    if (vobjContext.Response.StatusCode == 401)
        //    {
        //        vobjContext.Response.Redirect("~/Error/Page401");
        //    }
        //    else if (vobjContext.Response.StatusCode == 403)
        //    {
        //        vobjContext.Response.Redirect("~/Error/Page403");
        //    }
        //    else if (vobjContext.Response.StatusCode == 404)
        //    {
        //        vobjContext.Response.Redirect("~/Error/Page404");
        //    }
        //    else if (vobjContext.Response.StatusCode == 500)
        //    {
        //        vobjContext.Response.Redirect("~/Error/Page500");
        //    }
        //}
    }
}
