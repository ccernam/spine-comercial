using Spine.Constants.Cfg;
using Spine.Entities.Cmn;
using Spine.Services.Interfaces.Aud;
using Spine.Services.Interfaces.Cmn;
using Spine.Web.Areas.Cmn.Models;
using Spine.Web.Clases;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class UnidadMedidaController : WebBaseController
    {
        private readonly IUnidadMedidaService _objUnidadMedidaService;

        public UnidadMedidaController(
            IErrorService pobjErrorService,
            IUnidadMedidaService pobjUnidadMedidaService
        ) : base(pobjErrorService)
        {
            _objUnidadMedidaService = pobjUnidadMedidaService;
        }


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.objUnidadMedidaAccess = await GetAccess<UnidadMedidaAccess>(EntidadConstant.UnidadMedida);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Consultar(int piUniMedId = -1, string psUniMedNombre = "", short piUniMedServicio = -1, short piUniMedEstado = -1)
        {
            try
            {
                return JsonExito(await _objUnidadMedidaService.Consultar(piUniMedId: piUniMedId, psUniMedNombre: psUniMedNombre, piUniMedServicio: piUniMedServicio, piUniMedEstado: piUniMedEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Crear(UnidadMedida pobjUnidadMedida)
        {
            try
            {
                return JsonExito(await _objUnidadMedidaService.Crear(objSesion, pobjUnidadMedida));
            }
            catch (Exception vobjExcepcion)
            {
                return JsonError(vobjExcepcion, pobjUnidadMedida);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Editar(UnidadMedida pobjUnidadMedida)
        {
            try
            {
                return JsonExito(await _objUnidadMedidaService.Editar(objSesion, pobjUnidadMedida));
            }
            catch (Exception ex)
            {
                return JsonError(ex, pobjUnidadMedida);
            }
        }

        [HttpPut]
        [Route("cmn/unidadmedida/activar/{piUniMedId}")]
        public async Task<ActionResult> Activar(int piUniMedId)
        {
            try
            {
                return JsonExito(await _objUnidadMedidaService.Activar(objSesion, piUniMedId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        [Route("cmn/unidadmedida/desactivar/{piUniMedId}")]
        public async Task<ActionResult> Desactivar(int piUniMedId)
        {
            try
            {
                return JsonExito(await _objUnidadMedidaService.Desactivar(objSesion, piUniMedId)); ;
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}