using System.Web;

namespace Spine.Web.Clases
{
    public static class WebMetodos
    {
        public static string GetUrlBase(HttpRequestBase pobjRequest)
        {
            var vdctDataTokens = pobjRequest.RequestContext.RouteData.DataTokens;
            string vsArea = vdctDataTokens["area"] == null ? string.Empty : vdctDataTokens["area"].ToString().ToLower();

            var vdctValues = pobjRequest.RequestContext.RouteData.Values;
            string vsControlador = vdctValues["controller"] == null ? string.Empty : vdctValues["controller"].ToString().ToLower();
            string vsAccion = vdctValues["action"] == null ? string.Empty : vdctValues["action"].ToString().ToLower();

            string vsReemplazar = (string.IsNullOrEmpty(vsArea) ? string.Empty : vsArea + "/") + vsControlador + "/" + vsAccion;

            string vsUrl = pobjRequest.Url.ToString().ToLower();
            if (!string.IsNullOrEmpty(pobjRequest.Url.Query))
                vsUrl = vsUrl.Replace(pobjRequest.Url.Query, string.Empty);
            vsUrl = vsUrl.Replace(vsReemplazar, string.Empty);

            return vsUrl;
        }
    }
}
