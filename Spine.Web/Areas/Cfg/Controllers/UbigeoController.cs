using Spine.Services.Interfaces.Aud;
using Spine.Services.Interfaces.Cfg;
using Spine.Web.Clases;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Spine.Web.Areas.Cfg.Controllers
{
    public class UbigeoController : WebBaseController
    {
        private readonly IUbigeoService _objUbigeoService;

        public UbigeoController(
            IErrorService pobjErrorService,
            IUbigeoService pobjUbigeoService
        ) : base(pobjErrorService)
        {
            _objUbigeoService = pobjUbigeoService;
        }

        [HttpGet]
        public async Task<ActionResult> Consultar(short piUbiId = -1, short piUbiTipo = -1, string psUbiRama = "", short piUbiEstado = -1)
        {
            try
            {
                return JsonExito(await _objUbigeoService.Consultar(piUbiId: piUbiId, piUbiTipo: piUbiTipo, psUbiRama: psUbiRama, piUbiEstado: piUbiEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}