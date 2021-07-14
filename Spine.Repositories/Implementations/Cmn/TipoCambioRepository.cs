using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using Spine.Repositories.Interfaces.Cmn;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cmn
{
    public class TipoCambioRepository : RepositoryBase, ITipoCambioRepository
    {
        public TipoCambioRepository() : base() { }

        public async Task<IEnumerable<TipoCambio>> Consultar(int piTipoCambioId = -1, int piMonedaId = -1, string psTipCamFechaInicio = "", string psTipCamFechaFin = "", short piTipCamEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<TipoCambio>(
                    "Cmn.pa_TipoCambio_Consultar",
                    new SqlParameter("@piTipoCambioId", piTipoCambioId),
                    new SqlParameter("@piMonedaId", piMonedaId),
                    new SqlParameter("@psTipCamFechaInicio", psTipCamFechaInicio),
                    new SqlParameter("@psTipCamFechaFin", psTipCamFechaFin),
                    new SqlParameter("@piTipCamEstado", piTipCamEstado)
                );
            }
        }

        public Task<TipoCambio> Crear(Conexion vobjConexion, TipoCambio pobjTipoCambio)
        {
            throw new System.NotImplementedException();
        }

        public Task<TipoCambio> Editar(Conexion vobjConexion, TipoCambio pobjTipoCambio)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ValidarGuardar(TipoCambio pobjTipoCambio)
        {
            throw new System.NotImplementedException();
        }
    }
}
