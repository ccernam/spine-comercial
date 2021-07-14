using Spine.Constants.Cfg;
using Spine.Entities.Seg;
using Spine.Services.Interfaces.Aud;
using Spine.Services.Interfaces.Seg;
using Spine.Web.Areas.Seg.Models;
using Spine.Web.Clases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Spine.Web.Areas.Seg.Controllers
{
    public class RolController : WebBaseController
    {
        private readonly IRolService _objRolService;

        public RolController(
            IErrorService pobjErrorService, 
            IRolService pobjRolService
        ) : base(pobjErrorService)
        {
            _objRolService = pobjRolService;
        }

        [HttpGet]
        [Autoriza(lValidarComponente = true)]
        public async Task<ActionResult> Index()
        {
            ViewBag.objRolAccess = await GetAccess<RolAccess>(EntidadConstant.Rol);
            return View();
        }

        [HttpGet]
        [Autoriza]
        public async Task<ActionResult> Consultar(short piRolId = -1, short piRolEstado = -1)
        {
            try
            {
                return JsonExito(await _objRolService.Consultar(piRolId: piRolId, piRolEstado: piRolEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Crear(Rol pobjRol)
        {
            try
            {
                return JsonExito(await _objRolService.Crear(objSesion, pobjRol));
            }
            catch (Exception vobjExcepcion)
            {
                return JsonError(vobjExcepcion, pobjRol);
            }
        }

        [HttpPut]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Editar(Rol pobjRol)
        {
            try
            {
                return JsonExito(await _objRolService.Editar(objSesion, pobjRol));
            }
            catch (Exception vobjExcepcion)
            {
                return JsonError(vobjExcepcion, pobjRol);
            }
        }

        [HttpPatch]
        [Route("seg/rol/activar/{piRolId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Activar(int piRolId)
        {
            try
            {
                return JsonExito(await _objRolService.Activar(objSesion, piRolId));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, piRolId);
            }
        }

        [HttpPatch]
        [Route("seg/rol/desactivar/{piRolId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Desactivar(int piRolId)
        {
            try
            {
                return JsonExito(await _objRolService.Desactivar(objSesion, piRolId));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, piRolId);
            }
        }

        [HttpGet]
        [Route("seg/rol/consultarOpciones/{piRolId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> ConsultarOpciones(int piRolId)
        {
            try
            {
                return JsonExito(await _objRolService.ConsultarOpciones(piRolId));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, piRolId);
            }
        }

        [HttpPut]
        [Route("seg/rol/editarOpciones/{piRolId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> EditarOpciones(int piRolId, IEnumerable<RolOpcion> plstRolOpciones)
        {
            try
            {
                return JsonExito(await _objRolService.EditarOpciones(objSesion, piRolId, plstRolOpciones));
            }
            catch (Exception vobjException)
            {
                return JsonError(vobjException, piRolId, plstRolOpciones);
            }
        }
    }
}