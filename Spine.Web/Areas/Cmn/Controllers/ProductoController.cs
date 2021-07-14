using Spine.Services.Cmn;
using Spine.Web.Clases;
using System;
using System.Web.Mvc;
using Spine.Entities.Cmn;
using System.Linq;
using Spine.Services.Interfaces.Aud;

namespace Spine.Web.Areas.Cmn.Controllers
{
    public class ProductoController : WebBaseController
    {
        public ProductoController(
            IErrorService pobjErrorService
        ) : base(pobjErrorService)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                //List<Opcion> vlstOpciones = new NegUsuario(objSesion).ConsultarOpciones(piUsuId: objSesion.iUsuId, piAppId: objSesion.iAppId, psEntCodigo: CodEntidad.CMN_Producto);
                ViewBag.lCrear = true;  //vlstOpciones.FirstOrDefault(x => x.iOpcAccion == ValCatalogoItem.AUD_ACCION_CREACION) != null;
                ViewBag.lEditar = true;  //vlstOpciones.FirstOrDefault(x => x.iOpcAccion == ValCatalogoItem.AUD_ACCION_EDICION) != null;
                ViewBag.lActivar = true;  //vlstOpciones.FirstOrDefault(x => x.iOpcAccion == ValCatalogoItem.AUD_ACCION_ACTIVACION) != null;
                ViewBag.lDesactivar = true;  //vlstOpciones.FirstOrDefault(x => x.iOpcAccion == ValCatalogoItem.AUD_ACCION_DESACTIVACION) != null;

                //ViewBag.lstSucursales = new NegSucursal(objSesion).Consultar(piSucEstado: ValCatalogoItem.AUD_ESTADO_ACTIVADO);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Consultar(int pnProId = -1, short pnCtgId = -1, short pnMarId = -1, short pnUniMedId = -1,
                                      short pnProTipo = -1, string psProCodigo = "", string psProNombre = "",
                                      string psProDesc = "", short pnProEstado = -1)
        {
            try
            {
                return JsonExito(new ProductoService().Consultar(pnProId: pnProId, pnCtgId: pnCtgId, pnMarId: pnMarId, pnUniMedId: pnUniMedId,
                                                                      pnProTipo: pnProTipo, psProCodigo: psProCodigo, psProNombre: psProNombre,
                                                                      psProDesc: psProDesc, pnProEstado: pnProEstado)
                                );
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        public ActionResult Crear(Producto pobjProducto)
        {
            try
            {
                return JsonExito(new ProductoService().Crear(pobjProducto: pobjProducto));
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPut]
        public ActionResult Editar(Producto pobjProducto)
        {
            try
            {
                return JsonExito(new ProductoService().Editar(pobjProducto: pobjProducto));

                //var vobjNegProducto = new NegProducto(objSesion);
                //int vnProId = vobjNegProducto.Editar(pobjProducto: pobjProducto);
                //pobjProducto = vobjNegProducto.Consultar(pnProId: vnProId).FirstOrDefault();
                //return JsonExito(pobjProducto);
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Route("Cmn/Producto/Activar/{pnProId}")]
        public ActionResult Activar(int pnProId)
        {
            try
            {
                var vobjNegProducto = new ProductoService();
                var vobjProducto = new Producto();
                if (vobjNegProducto.Activar(pnProId))
                    vobjProducto = vobjNegProducto.Consultar(pnProId: pnProId).FirstOrDefault();
                return JsonExito(vobjProducto);
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }

        [HttpPost]
        [Route("Cmn/Producto/Desactivar/{pnProId}")]
        public ActionResult Desactivar(int pnProId)
        {
            try
            {
                var vobjNegProducto = new ProductoService();
                var vobjProducto = new Producto();
                if (vobjNegProducto.Desactivar(pnProId))
                    vobjProducto = vobjNegProducto.Consultar(pnProId: pnProId).FirstOrDefault();
                return JsonExito(vobjProducto);
            }
            catch (Exception ex)
            {
                return JsonError(ex);
            }
        }
    }
}