using Spine.Constants.Cmn;
using Spine.Entities.Seg;
using Spine.Librerias.General;
using Spine.Services.Implementations.Seg;
using Spine.Services.Interfaces.Seg;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Spine.Web.Clases
{
    public class AutorizaAttribute : AuthorizeAttribute
    {
        // Variables
        private bool _lSesionValida = false;
        private bool _lComponenteValido = false;
        private bool _lAccionValida = false;

        private string _sErrorAutorizacion = string.Empty;
        private readonly ISesionService _objSesionService;
        private readonly string _sNombreCookie = "Sesion";
        private readonly string _sNombreParametro = "Sesion_Minutos_Expiracion";

        // Propiedades
        public bool lValidarComponente { set; get; } = false;
        public bool lValidarAccion { set; get; } = false;

        // Constructor
        public AutorizaAttribute()
        {
            _objSesionService = DependencyResolver.Current.GetService<ISesionService>();
        }

        // Métodos
        protected override bool AuthorizeCore(HttpContextBase pobjHttpContext)
        {
            bool vlRespuesta = false;
            try
            {
                HttpCookie vobjCookieToken = pobjHttpContext.Request.Cookies[_sNombreCookie];
                if (vobjCookieToken != null)
                {
                    // Validamos estado de sesión
                    if (_objSesionService.ValidarSesionSync(vobjCookieToken.Value))
                    {
                        _lSesionValida = true;

                        // Refrescamos token
                        Sesion vobjSesion = Utilitarios.GetSesion(vobjCookieToken.Value, false);
                        TimeSpan vtspDiferencia = vobjSesion.dSesExpiracion - Utilitarios.GetFechaSistema();
                        if (vtspDiferencia.TotalMinutes <= Convert.ToDouble(ConfigurationManager.AppSettings[_sNombreParametro]))
                        {
                            vobjSesion = _objSesionService.RefrescarTokenSync(vobjSesion.sSesRefreshToken);

                            pobjHttpContext.Response.Cookies.Remove(_sNombreCookie);
                            HttpCookie vobjCookie = new HttpCookie(_sNombreCookie) { Value = vobjSesion.sSesAccessToken };
                            vobjCookie.Expires = DateTime.MaxValue;
                            pobjHttpContext.Response.Cookies.Add(vobjCookie);
                        }

                        if (lValidarComponente) // Validamos acceso a componente
                        {
                            _lComponenteValido = _objSesionService.ValidarComponenteSync(vobjCookieToken.Value, pobjHttpContext.Request.RawUrl);
                            vlRespuesta = _lSesionValida && _lComponenteValido;
                        }
                        else if (lValidarAccion) // Validamos acceso a acción
                        {
                            _lAccionValida = _objSesionService.ValidarAccionSync(vobjCookieToken.Value, pobjHttpContext.Request.RawUrl, pobjHttpContext.Request.HttpMethod);
                            vlRespuesta = _lSesionValida && _lAccionValida;
                        }
                        else
                        {
                            vlRespuesta = true;
                        }
                    }
                }
                else
                {
                    _lSesionValida = false;
                }
            }
            catch (Exception ex)
            {
                _sErrorAutorizacion = ex.Message;
            }
            return vlRespuesta;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext pobjFilterContext)
        {
            if (!string.IsNullOrEmpty(_sErrorAutorizacion))
                pobjFilterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError, _sErrorAutorizacion);
            else if (!_lSesionValida)
                pobjFilterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            else if (lValidarComponente && !_lComponenteValido)
                pobjFilterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            else if (lValidarAccion && !_lAccionValida)
                pobjFilterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            else
                pobjFilterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Test");
        }
    }
}
