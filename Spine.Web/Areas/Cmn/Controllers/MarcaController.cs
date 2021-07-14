using Spine.Web.Clases;
using System;
using System.Web.Mvc;
using Spine.Entities.Cmn;
using Spine.Services.Interfaces.Cmn;
using System.Threading.Tasks;
using Spine.Web.Areas.Cmn.Models;
using Spine.Constants.Cfg;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class MarcaController : WebBaseController
    {
        private readonly IMarcaService _objMarcaService;

        public MarcaController(
            IErrorService pobjErrorService,
            IMarcaService pobjMarcaService
        ) : base(pobjErrorService)
        {
            _objMarcaService = pobjMarcaService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.objMarcaAccess = await GetAccess<MarcaAccess>(EntidadConstant.Marca);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Consultar(short piMarId = -1, string psMarNombre = "", short piMarEstado = -1)
        {
            try
            {
                return JsonExito(await _objMarcaService.Consultar(piMarId: piMarId, psMarNombre: psMarNombre, piMarEstado: piMarEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Crear(Marca pobjMarca)
        {
            try
            {
                return JsonExito(await _objMarcaService.Crear(objSesion, pobjMarca));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Editar(Marca pobjMarca)
        {
            try
            {
                return JsonExito(await _objMarcaService.Editar(objSesion, pobjMarca));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
        [HttpPost]
        [Route("cmn/marca/activar/{piMarId}")]
        public async Task<ActionResult> Activar(short piMarId)
        {
            try
            {
                return JsonExito(await _objMarcaService.Activar(objSesion, piMarId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
        [HttpPost]
        [Route("cmn/marca/desactivar/{piMarId}")]
        public async Task<ActionResult> Desactivar(short piMarId)
        {
            try
            {
                return JsonExito(await _objMarcaService.Desactivar(objSesion, piMarId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}