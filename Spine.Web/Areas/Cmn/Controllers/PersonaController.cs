using Spine.Web.Clases;
using Spine.Services.Cmn;
using System;
using System.Web.Mvc;
using Spine.Entities.Cmn;
using System.Linq;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class PersonaController : WebBaseController
    {
        public PersonaController(
            IErrorService pobjErrorService
        ) : base(pobjErrorService)
        {

        }


        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.lCrear = true;
            ViewBag.lEditar = true;
            ViewBag.lActivar = true;
            ViewBag.lDesactivar = true;
            return View();
        }

        [HttpGet]
        public ActionResult Ver()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Consultar(int piPerId = -1, short piPerTipo = -1, short piPerTipoDocumento = -1, string psPerDocumento = "", string psPerNombre = "", string psPerComodin = "", short piPerEstado = -1)
        {
            try
            {
                return JsonExito(new PersonaService().Consultar(piPerId: piPerId, piPerTipo: piPerTipo, piPerTipoDocumento: piPerTipoDocumento, psPerDocumento: psPerDocumento, psPerNombre: psPerNombre, psPerComodin: psPerComodin, piPerEstado: piPerEstado));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Route("Cmn/Persona/Activar/{piPerId}")]
        public ActionResult Activar(int piPerId)
        {
            try
            {
                new PersonaService().Activar(piPerId);
                Persona vobjPersona = new PersonaService().Consultar(piPerId: piPerId).FirstOrDefault();
                return JsonExito(vobjPersona);
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Route("Cmn/Persona/Desactivar/{piPerId}")]
        public ActionResult Desactivar(int piPerId)
        {
            try
            {
                new PersonaService().Desactivar(piPerId);
                Persona vobjPersona = new PersonaService().Consultar(piPerId: piPerId).FirstOrDefault();
                return JsonExito(vobjPersona);
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}