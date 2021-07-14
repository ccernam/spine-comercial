using Spine.Web.Clases;
using System;
using System.Web.Mvc;
using Spine.Entities.Cmn;
using System.Threading.Tasks;
using Spine.Services.Interfaces.Cmn;
using Spine.Web.Areas.Cmn.Models;
using Spine.Constants.Cfg;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class SucursalController : WebBaseController
    {
        private readonly ISucursalService _objSucursalService;

        public SucursalController(
            IErrorService pobjErrorService,
            ISucursalService pobjSucursalService
        ) : base(pobjErrorService)
        {
            _objSucursalService = pobjSucursalService;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.objSucursalAccess = await GetAccess<SucursalAccess>(EntidadConstant.Sucursal);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Consultar(short piSucId = -1, string psSucNombre = "", short piSucEstado = -1)
        {
            try
            {
                return JsonExito(await _objSucursalService.Consultar(piSucId: piSucId, psSucNombre: psSucNombre, piSucEstado: piSucEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Crear(Sucursal pobjSucursal)
        {
            try
            {
                return JsonExito(await _objSucursalService.Crear(objSesion, pobjSucursal));
            }
            catch (Exception ex)
            {
                return JsonError(ex, pobjSucursal);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Editar(Sucursal pobjSucursal)
        {
            try
            {
                return JsonExito(await _objSucursalService.Editar(objSesion, pobjSucursal));
            }
            catch (Exception ex)
            {
                return JsonError(ex, pobjSucursal);
            }
        }

        [HttpPut]
        [Route("cmn/sucursal/activar/{piSucId}")]
        public async Task<ActionResult> Activar(int piSucId)
        {
            try
            {
                return JsonExito(await _objSucursalService.Activar(objSesion, piSucId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        [Route("cmn/sucursal/desactivar/{piSucId}")]
        public async Task<ActionResult> Desactivar(int piSucId)
        {
            try
            {
                return JsonExito(await _objSucursalService.Desactivar(objSesion, piSucId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}