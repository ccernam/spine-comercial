//using Spine.Clientes.Cfg;
//using Spine.Constantes.Cfg;
//using Spine.Librerias.General;
//using Spine.Librerias.Log;
//using Spine.Web.Clases;
//using Spine.Web.Clases.Constantes;
//using Spine.Modelos.Cfg;
//using Spine.Modelos.Seg;
//using Spine.Seg.Clientes;
//using Spine.Web.Clases;
//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;

using Spine.Entities.Seg;
using Spine.Services.Interfaces.Aud;
using Spine.Services.Interfaces.Seg;
using Spine.Web.Clases;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Spine.Web.Controllers
{
    public class HomeController : WebBaseController
    {
        private readonly ISesionService _objSesionService;

        //[Route("autenticar")]
        //public ActionResult Login()
        //{
        //    //if (objSesion != null)
        //    //    return RedirectToAction("index", "home");
        //    return View();
        //}

        public HomeController(
            IErrorService pobjErrorService,
            ISesionService pobjSesionService
        ) : base(pobjErrorService)
        {
            _objSesionService = pobjSesionService;
        }

        public ActionResult Init()
        {
            return RedirectToAction("acceder", "home");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Acceder()
        {
            return View();
        }

        



        //public ActionResult Index()
        //{
        //    try
        //    {
        //        if (objSesion == null)
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }


        //        List<MParametro> alertaTipoCambio = GetDatos<List<MParametro>>(new CParametro(objSesion).Consultar("pnIdEmpresa=" + objSesionInfo.nIdEmpresa, "psParCodigo=" + CnsParametro.ROL_ALERTA_TIPO_CAMBIO, "pnParEstado=" + 1));
        //        List<MRol> usuarioRol = GetDatos<List<MRol>>(new CUsuario(objSesion).ConsultarRol(pnIdEmpresa: objSesionInfo.nIdEmpresa, pnIdUsuario: (short)objSesionInfo.nIdUsuario, pnUsuEstado: 1));
        //        string[] rolAutorizado = alertaTipoCambio[0].sParValor.Split(Convert.ToChar(","));
        //        List<string> lstRolAutorizado = new List<string>(rolAutorizado);
        //        var vlAlerta = false;
        //        foreach (MRol vlstUsuarioRol in usuarioRol)
        //        {
        //            if (lstRolAutorizado.Contains(vlstUsuarioRol.sRolCodigo))
        //            {
        //                vlAlerta = true;
        //            }
        //        }
        //        ViewData["vlAlerta"] = vlAlerta;
        //        return View();
        //    }
        //    catch (Exception vobjExcepcion)
        //    {

        //        Tracer.GuardarError(objSesionInfo, vobjExcepcion, "HomeController.Index()");
        //        return MostrarError(vobjExcepcion);
        //    }

        //}



        //public ActionResult Init()
        //{
        //    return RedirectToAction("Login", "Home");
        //}

        //[HttpPost]
        //public ContentResult Logout()
        //{
        //    try
        //    {
        //        // Eliminamos sesión
        //        if (Session[CnsWeb.SESION_MENU] != null)
        //            Session.Remove(CnsWeb.SESION_MENU);
        //        if (Session[CnsWeb.SESION] != null)
        //            Session.Remove(CnsWeb.SESION);

        //        // Eliminamos cookie
        //        if (Request.Cookies[CnsWeb.COOKIE] != null)
        //        {
        //            Response.Cookies[CnsWeb.COOKIE][CnsWeb.COOKIE_TOKEN] = string.Empty;
        //            Response.Cookies.Remove(CnsWeb.COOKIE);
        //            Request.Cookies.Remove(CnsWeb.COOKIE);
        //        }

        //        return JsonContentExito(Metodos.ToJson(ApiRespuestaHelper.Exito(true)));
        //    }
        //    catch (Exception vobjExcepcion)
        //    {
        //        return JsonContentError(vobjExcepcion);
        //    }
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}