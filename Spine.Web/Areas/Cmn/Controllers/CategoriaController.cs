using Spine.Web.Clases;
using System;
using System.Web.Mvc;
using Spine.Entities.Cmn;
using System.Threading.Tasks;
using Spine.Services.Interfaces.Cmn;
using Spine.Constants.Cfg;
using Spine.Constants.Cmn;
using Spine.Web.Areas.Cmn.Models;
using Spine.Services.Interfaces.Cfg;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class CategoriaController : WebBaseController
    {
        private readonly ICatalogoService _objCatalogoService;
        private readonly ICategoriaService _objCategoriaService;

        public CategoriaController(
            IErrorService pobjErrorService,
            ICatalogoService pobjCatalogoService,
            ICategoriaService pobjCategoriaService
        ) : base(pobjErrorService)
        {
            _objCategoriaService = pobjCategoriaService;
            _objCatalogoService = pobjCatalogoService;
        }

        [HttpGet]
        [Autoriza(lValidarComponente = true)]
        public async Task<ActionResult> Index()
        {
            ViewBag.objCategoriaAccess = await GetAccess<CategoriaAccess>(EntidadConstant.Categoria);
            ViewBag.lstCtgTipo = await _objCatalogoService.ConsultarItems(piCatalogoId: CatalogoConstant.TipoProducto, piCatIteEstado: EstadoConstant.Activado);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Consultar(int piCategoriaId = -1, short piCtgTipoProducto = -1, string psCtgNombre = "", short piCtgEstado = -1)
        {
            try
            {
                return JsonExito(await _objCategoriaService.Consultar(piCategoriaId: piCategoriaId, piCtgTipoProducto: piCtgTipoProducto, psCtgNombre: psCtgNombre, piCtgEstado: piCtgEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Crear(Categoria pobjCategoria)
        {
            try
            {
                return JsonExito(await _objCategoriaService.Crear(objSesion, pobjCategoria));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, pobjCategoria);
            }
        }

        [HttpPut]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Editar(Categoria pobjCategoria)
        {
            try
            {
                return JsonExito(await _objCategoriaService.Editar(objSesion, pobjCategoria));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, pobjCategoria);
            }
        }

        [HttpPatch]
        [Autoriza(lValidarAccion = true)]
        [Route("cmn/categoria/activar/{piCategoriaId}")]
        public async Task<ActionResult> Activar(int piCategoriaId)
        {
            try
            {
                return JsonExito(await _objCategoriaService.Activar(objSesion, piCategoriaId));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, piCategoriaId);
            }
        }

        [HttpPatch]
        [Autoriza(lValidarAccion = true)]
        [Route("cmn/categoria/desactivar/{piCategoriaId}")]
        public async Task<ActionResult> Desactivar(int piCategoriaId)
        {
            try
            {
                return JsonExito(await _objCategoriaService.Desactivar(objSesion, piCategoriaId));
            }
            catch (Exception ex)
            {
                return JsonError(ex, piCategoriaId);
            }
        }
    }
}