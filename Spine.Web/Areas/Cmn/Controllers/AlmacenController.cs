using Spine.Web.Clases;
using System;
using System.Web.Mvc;
using Spine.Entities.Cmn;
using Spine.Services.Interfaces.Cmn;
using System.Threading.Tasks;
using Spine.Constants.Cmn;
using Spine.Constants.Cfg;
using Spine.Web.Areas.Cmn.Models;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class AlmacenController : WebBaseController
    {
        private readonly IAlmacenService _objAlmacenService;
        private readonly ISucursalService _objSucursalService;

        public AlmacenController(
            IErrorService pobjErrorService,
            IAlmacenService pobjAlmacenService,
            ISucursalService pobjSucursalService
        ) : base(pobjErrorService)
        {
            _objAlmacenService = pobjAlmacenService;
            _objSucursalService = pobjSucursalService;
        }

        [HttpGet]
        [Autoriza(lValidarComponente = true)]
        public async Task<ActionResult> Index()
        {
            ViewBag.lstSucursales = await _objSucursalService.Consultar(piSucEstado: EstadoConstant.Activado);
            ViewBag.objAlmacenAccess = await GetAccess<AlmacenAccess>(EntidadConstant.Almacen);
            return View();
        }

        [HttpGet]
        [Autoriza]
        public async Task<ActionResult> Consultar(int piAlmacenId = -1, int piSucursalId = -1, string psAlmNombre = "", short piAlmEstado = -1)
        {
            try
            {
                return JsonExito(await _objAlmacenService.Consultar(piAlmacenId: piAlmacenId, piSucursalId: piSucursalId, psAlmNombre: psAlmNombre, piAlmEstado: piAlmEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Crear(Almacen pobjAlmacen)
        {
            try
            {
                return JsonExito(await _objAlmacenService.Crear(objSesion, pobjAlmacen));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Editar(Almacen pobjAlmacen)
        {
            try
            {
                return JsonExito(await _objAlmacenService.Editar(objSesion, pobjAlmacen));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPatch]
        [Autoriza(lValidarAccion = true)]
        [Route("cmn/almacen/activar/{piAlmacenId}")]
        public async Task<ActionResult> Activar(int piAlmacenId)
        {
            try
            {
                return JsonExito(await _objAlmacenService.Activar(objSesion, piAlmacenId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPatch]
        [Autoriza(lValidarAccion = true)]
        [Route("cmn/almacen/desactivar/{piAlmacenId}")]
        public async Task<ActionResult> Desactivar(int piAlmacenId)
        {
            try
            {
                return JsonExito(await _objAlmacenService.Desactivar(objSesion, piAlmacenId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}