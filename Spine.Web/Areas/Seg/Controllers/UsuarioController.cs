using Spine.Entities.Seg;
using Spine.Web.Clases;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Threading.Tasks;
using Spine.Constants.Cmn;
using Spine.Services.Interfaces.Seg;
using Spine.Services.Interfaces.Cmn;
using Spine.Constants.Cfg;
using Spine.Web.Areas.Seg.Models;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Seg.Controllers
{
    public class UsuarioController : WebBaseController
    {
        private readonly IUsuarioService _objUsuarioService;
        private readonly ISucursalService _objSucursalService;

        public UsuarioController(
            IErrorService pobjErrorService,
            ISucursalService pobjSucursalService,
            IUsuarioService pobjUsuarioService
        ) : base(pobjErrorService)
        {
            _objSucursalService = pobjSucursalService;
            _objUsuarioService = pobjUsuarioService;
        }

        [HttpGet]
        [Autoriza(lValidarComponente = true)]
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.objUsuarioAccess = await GetAccess<UsuarioAccess>(EntidadConstant.Usuario);
                ViewBag.lstSucursales = await _objSucursalService.Consultar(piSucEstado: EstadoConstant.Activado);
                return View();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message); //  (HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Consultar(
            int piUsuId = -1, string psUsuUsuario = "", string psUsuCorreo = "", int piSucId = -1,
            int piPerId = -1, short piUsuEstado = -1)
        {
            try
            {
                return JsonExito(await _objUsuarioService.Consultar(
                    piUsuarioId: piUsuId, psUsuUsuario: psUsuUsuario, psUsuCorreo: psUsuCorreo, piSucursalId: piSucId,
                    piPersonaId: piPerId, piUsuEstado: piUsuEstado)
                );
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Crear(Usuario pobjUsuario)
        {
            try
            {
                return JsonExito(await _objUsuarioService.Crear(objSesion, pobjUsuario));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Editar(Usuario pobjUsuario)
        {
            try
            {
                return JsonExito(await _objUsuarioService.Editar(objSesion, pobjUsuario));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPatch]
        [Route("seg/usuario/activar/{piUsuarioId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Activar(int piUsuarioId)
        {
            try
            {
                return JsonExito(await _objUsuarioService.Activar(objSesion, piUsuarioId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPatch]
        [Route("seg/usuario/desactivar/{piUsuarioId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> Desactivar(int piUsuarioId)
        {
            try
            {
                return JsonExito(await _objUsuarioService.Desactivar(objSesion, piUsuarioId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        [Route("seg/usuario/reiniciarClave/{piUsuarioId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> ReiniciarClave(int piUsuarioId)
        {
            try
            {
                return JsonExito(await _objUsuarioService.ReiniciarClave(objSesion, piUsuarioId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpGet]
        [Route("seg/usuario/consultarRoles/{piUsuarioId}")]
        public async Task<ActionResult> ConsultarRoles(int piUsuarioId)
        {
            try
            {
                return JsonExito(await _objUsuarioService.ConsultarRoles(piUsuarioId));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        [Route("seg/usuario/editarRoles/{piUsuarioId}")]
        [Autoriza(lValidarAccion = true)]
        public async Task<ActionResult> EditarRoles(int piUsuarioId, IEnumerable<UsuarioRol> plstUsuarioRoles)
        {
            try
            {
                return JsonExito(await _objUsuarioService.EditarRoles(objSesion, piUsuarioId, plstUsuarioRoles));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}