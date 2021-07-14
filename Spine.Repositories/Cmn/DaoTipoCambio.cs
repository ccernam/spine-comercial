using Spine.Entities.Cmn;
using System.Collections.Generic;
using Spine.Librerias.Database;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System;

namespace Spine.Repositories.Cmn
{
    public class DaoTipoCambio : RepositoryBase
    {
        public DaoTipoCambio() : base()
        {
        }

        public List<TipoCambio> Consultar(int piTipCamId = -1, string psTipCamFechaIni = "", string psTipCamFechaFin = "", short piTipCamEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<TipoCambio>(
                    "Cmn.paTipoCambioConsultar",
                    new SqlParameter("@piTipCamId", piTipCamId),
                    new SqlParameter("@psTipCamFechaIni", psTipCamFechaIni),
                    new SqlParameter("@psTipCamFechaFin", psTipCamFechaFin),
                    new SqlParameter("@piTipCamEstado", piTipCamEstado)/*,
                    new SqlParameter("@psCatCodigoEstado", CodCatalogo.AUD_ESTADO)*/
                );
            }
        }

        public int Crear(Conexion pobjConexion, TipoCambio pobjTipoCambio)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter(){ ParameterName = "@piTipCamId", Direction = ParameterDirection.Output },
                new SqlParameter(){ ParameterName = "@pdTipCamFecha", Value = pobjTipoCambio.dTipCamFecha },
                new SqlParameter(){ ParameterName = "@pnTipCamVenta", Value = pobjTipoCambio.nTipCamVenta },
                new SqlParameter(){ ParameterName = "@pnTipCamCompra", Value = pobjTipoCambio.nTipCamCompra },
                new SqlParameter(){ ParameterName = "@piTipCamEstado", Value = pobjTipoCambio.iTipCamEstado }
            };
            pobjConexion.Ejecutar("Cmn.paTipoCambioCrear", varrParametros);
            pobjTipoCambio.iTipCamId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piTipCamId").Value);
            return pobjTipoCambio.iTipCamId;
        }

        public int Editar(Conexion pobjConexion, TipoCambio pobjTipoCambio)
        {
            pobjConexion.Ejecutar(
                "Cmn.paTipoCambioEditar",
                new SqlParameter() { ParameterName = "@piTipCamId", Value = pobjTipoCambio.iTipCamId },
                new SqlParameter() { ParameterName = "@pdTipCamFecha", Value = pobjTipoCambio.dTipCamFecha },
                new SqlParameter() { ParameterName = "@pnTipCamVenta", Value = pobjTipoCambio.nTipCamVenta },
                new SqlParameter() { ParameterName = "@pnTipCamCompra", Value = pobjTipoCambio.nTipCamCompra }
            );
            return pobjTipoCambio.iTipCamId;
        }

        public bool CambiarEstado(Conexion pobjConexion, short piTipCamId, byte piTipCamEstado)
        {
            return pobjConexion.Ejecutar(
                "Cmn.paTipoCambioCambiarEstado",
                new SqlParameter() { ParameterName = "@piTipCamId", Value = piTipCamId },
                new SqlParameter() { ParameterName = "@piTipCamEstado", Value = piTipCamEstado }
            ) == 1;
        }
    }
}
