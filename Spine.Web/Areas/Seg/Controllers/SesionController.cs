using Spine.Entities.Seg;
using Spine.Services.Interfaces.Aud;
using Spine.Services.Interfaces.Seg;
using Spine.Web.Clases;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Spine.Web.Areas.Seg.Controllers
{
    public class SesionController : WebBaseController
    {
        private readonly ISesionService _objSesionService;

        public SesionController(
            IErrorService pobjErrorService, 
            ISesionService pobjSesionService
        ) : base(pobjErrorService)
        {
            _objSesionService = pobjSesionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Autenticar(Autenticar pobjAutenticar)
        {
            try
            {
                Sesion vobjSesion = await _objSesionService.Autenticar(pobjAutenticar);

                Response.Cookies.Remove("Sesion");
                HttpCookie vobjCookie = new HttpCookie("Sesion") { Value = vobjSesion.sSesAccessToken };
                vobjCookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(vobjCookie);

                return JsonExito(true);
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPatch]
        [Autoriza]
        public async Task<ActionResult> CambiarClave(CambiarClave pobjCambiarClave)
        {
            try
            {
                return JsonExito(await _objSesionService.CambiarClave(objSesion, pobjCambiarClave));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}