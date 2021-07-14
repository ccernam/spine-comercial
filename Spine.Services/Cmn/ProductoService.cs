using Spine.Constants.Cfg;
using Spine.Repositories.Cmn;
using Spine.Repositories.Implementations.Aud;
using Spine.Entities.Aud;
using Spine.Entities.Cmn;

using Spine.Librerias.Database;
using Spine.Librerias.General;
using System.Collections.Generic;

namespace Spine.Services.Cmn
{
    public class ProductoService : ServiceBase
    {
        public ProductoService() : base()
        {
        }

        public List<Producto> Consultar(int pnProId = -1, short pnCtgId = -1, short pnMarId = -1, short pnUniMedId = -1,
                                        short pnProTipo = -1, string psProCodigo = "", string psProNombre = "",
                                        string psProDesc = "", short pnProEstado = -1)
        {
            return new DaoProducto().Consultar(pnProId, pnCtgId, pnMarId, pnUniMedId,
                                                        pnProTipo, psProCodigo, psProNombre,
                                                        psProDesc, pnProEstado);
        }

        public int Crear(Producto pobjProducto)
        {
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();

                new DaoProducto().Crear(vobjConexion, pobjProducto);

                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iUsuarioId = 1; // objSesion.iUsuarioId;
                vobjCambio.iCamRegistro = pobjProducto.nProId;
                vobjCambio.iAccionId = AccionConstant.Producto_Creacion;
                new CambioRepository().Crear(vobjConexion, vobjCambio);

                vobjConexion.Commit();

                return pobjProducto.nProId;
            }
        }

        public int Editar(Producto pobjProducto)
        {
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();

                new DaoProducto().Editar(vobjConexion, pobjProducto);

                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iUsuarioId = 1; // objSesion.iUsuarioId;
                vobjCambio.iCamRegistro = pobjProducto.nProId;
                vobjCambio.iAccionId = AccionConstant.Producto_Edicion;
                new CambioRepository().Crear(vobjConexion, vobjCambio);

                vobjConexion.Commit();

                return pobjProducto.nProId;
            }
        }

        public bool Activar(int pnProId)
        {
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();

                new DaoProducto().CambiarEstado(vobjConexion, pnProId, 1);

                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iUsuarioId = 1; // objSesion.iUsuarioId;
                vobjCambio.iCamRegistro = pnProId;
                vobjCambio.iAccionId = AccionConstant.Producto_Activacion;
                new CambioRepository().Crear(vobjConexion, vobjCambio);

                vobjConexion.Commit();

                return true;
            }
        }

        public bool Desactivar(int pnProId)
        {
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();

                new DaoProducto().CambiarEstado(vobjConexion, pnProId, 0);

                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iUsuarioId = 1; // objSesion.iUsuarioId;
                vobjCambio.iCamRegistro = pnProId;
                vobjCambio.iAccionId = AccionConstant.Producto_Desactivacion;
                new CambioRepository().Crear(vobjConexion, vobjCambio);

                vobjConexion.Commit();

                return true;
            }
        }

    }
}
