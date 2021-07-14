using Spine.Entities.Aud;
using Spine.Entities.Seg;
using Spine.Librerias.General;
using Spine.Services.Interfaces.Aud;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Spine.Web.Clases
{
    public class WebBaseController : Controller
    {
        protected readonly IErrorService _objErrorService;

        protected Sesion objSesion { get { return Request.Cookies["Sesion"] != null ? Utilitarios.GetSesion(Request.Cookies["Sesion"].Value, false) : null; } }

        public WebBaseController(IErrorService pobjErrorService)
        {
            _objErrorService = pobjErrorService;
        }

        protected ContentResult JsonExito<T>(T pobjObjeto)
        {
            return Content(Metodos.ToJson(new ApiRespuesta<T>() { iTipo = 1, objDatos = pobjObjeto }), "application/json", Encoding.UTF8);
        }
        protected ContentResult JsonError(Exception pobjExcepcion, params object[] parrParametros)
        {
            if (Utilitarios.EsValidacion(pobjExcepcion))
            {
                return Content(Metodos.ToJson(new ApiRespuesta<object>() { iTipo = 2, sMensaje = pobjExcepcion.Message }), "application/json", Encoding.UTF8);
            }
            else
            {
                int viErrorId = 0;
                try
                {
                    Error vobjError = new Error();
                    vobjError.sErrMensaje = pobjExcepcion.InnerException == null ? pobjExcepcion.Message : pobjExcepcion.InnerException.Message;
                    vobjError.sErrPila = pobjExcepcion.StackTrace.Trim();
                    vobjError.sErrParametros = Metodos.ToJson(parrParametros, false);
                    _objErrorService.Crear(objSesion, vobjError);
                }
                catch (Exception)
                {
                }

                string vsMensaje = "Lo sentimos, se ha producido un error al ejecutar operación" + (viErrorId == 0 ? "" : " (" + viErrorId + ")");
                var vobjRpta = new ApiRespuesta<object>() { iTipo = 0, sMensaje = vsMensaje, iErrId = viErrorId };
                return Content(Metodos.ToJson(vobjRpta), "application/json", Encoding.UTF8);
            }
        }


        protected async Task<T> GetAccess<T>(int piEntidadId)
        {
            return Activator.CreateInstance<T>();
        }

        //protected T GetDatos<T>(string psCadena)
        //{
        //    ApiRespuesta<T> vobjApiRpta = Metodos.FromJson<ApiRespuesta<T>>(psCadena);
        //    if (vobjApiRpta.nTipo == (byte)0)
        //    {
        //        Exception vobjExcepcion = new Exception(vobjApiRpta.sMensaje + (vobjApiRpta.nError > 0 ? " (" + vobjApiRpta.nError + ")." : ""));
        //        if (vobjApiRpta.nError > 0)
        //            vobjExcepcion.Data["nIdLogAplicacion"] = vobjApiRpta.nError;
        //        throw vobjExcepcion;
        //    }
        //    else if (vobjApiRpta.nTipo == (byte)2)
        //        Metodos.LanzarVal(vobjApiRpta.sMensaje);
        //    return vobjApiRpta.objDatos;
        //}

        //protected FileContentResult DescargarArchivo(byte[] parrArchivo, string psArchivoContenido = "", string psArchivoNombre = "")
        //{
        //    if (psArchivoContenido == "")
        //        psArchivoContenido = CnsWeb.CONTENT_OCTET;

        //    if (psArchivoNombre == "")
        //        return File(parrArchivo, psArchivoContenido);
        //    else
        //        return File(parrArchivo, psArchivoContenido, psArchivoNombre);
        //}
        //protected FileContentResult DescargarArchivo(string psArchivoRuta, string psArchivoContenido = "", string psArchivoNombre = "")
        //{
        //    return DescargarArchivo(Metodos.LeerArchivo(psArchivoRuta), psArchivoContenido, psArchivoNombre);
        //}

        //public static string GetUrlBase(string psUrlAbsoluta)
        //{
        //    // Variables :
        //    string vsHttp = string.Empty;
        //    string[] varrSegmentos = new string[] { };
        //    string vsUrlBase = string.Empty;

        //    // Procesamiento
        //    byte vnSegmentosBase = Convert.ToByte(ConfigGlobal.WEB_URL_SEGMENTOS);

        //    vsHttp = (psUrlAbsoluta.Contains("https://") ? "https://" : "http://");
        //    psUrlAbsoluta = psUrlAbsoluta.Replace(vsHttp, string.Empty); // Quitamos http
        //    varrSegmentos = psUrlAbsoluta.Split('/');

        //    for (byte i = 0; i < varrSegmentos.Length; i++)
        //    {
        //        if (i < vnSegmentosBase)
        //            vsUrlBase += varrSegmentos[i] + "/";
        //        else
        //            break;
        //    }

        //    vsUrlBase = vsHttp + vsUrlBase;
        //    return vsUrlBase;
        //}
    }
}
