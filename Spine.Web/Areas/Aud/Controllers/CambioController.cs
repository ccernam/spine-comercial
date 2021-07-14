using Spine.Web.Clases;
using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Aud.Controllers
{
    public class CambioController : WebBaseController
    {
        private readonly ICambioService _objCambioService;

        public CambioController(
            IErrorService pobjErrorService,
            ICambioService pobjCambioService
        ) : base(pobjErrorService)
        {
            _objCambioService = pobjCambioService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("aud/cambio/consultar/{piEntId}/{piCamRegistro}")]
        public async Task<ActionResult> Consultar(int piEntId, int piCamRegistro)
        {
            try
            {
                return JsonExito(await _objCambioService.Consultar(piEntId: piEntId, piCamRegistro: piCamRegistro));
            }
            catch (Exception vobjExcepcion)
            {
                return JsonError(vobjExcepcion, piEntId, piCamRegistro);
            }
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Ver(string psEtiqueta, int piEntId)
        {
            try
            {
                ViewBag.sEtiqueta = psEtiqueta;
                ViewBag.iEntId = piEntId;
                return View();
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}