using Spine.Services.Cfg;
using Spine.Services.Interfaces.Aud;
using Spine.Web.Clases;
using System;
using System.Web.Mvc;

namespace Spine.Web.Areas.Cfg.Controllers
{
    public class MonedaController : WebBaseController
    {
        public MonedaController(
            IErrorService pobjErrorService
        ) : base(pobjErrorService)
        {

        }

        [HttpGet]
        // [Autoriza]
        public ContentResult Consultar(short piMonId = -1, short piMonEstado = -1)
        {
            try
            {
                return JsonExito(new MonedaNeg().Consultar(piMonId: piMonId, piMonEstado: piMonEstado));
            }
            catch (Exception vobjExcepcion)
            {
                return JsonError(vobjExcepcion);
            }
        }
    }
}