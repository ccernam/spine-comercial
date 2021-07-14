using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cmn
{
    public interface ITipoCambioRepository
    {
        Task<IEnumerable<TipoCambio>> Consultar(int piTipoCambioId = -1, int piMonedaId = -1, string psTipCamFechaInicio = "", string psTipCamFechaFin = "", short piTipCamEstado = -1);

        Task<TipoCambio> Crear(Conexion vobjConexion, TipoCambio pobjTipoCambio);

        Task<TipoCambio> Editar(Conexion vobjConexion, TipoCambio pobjTipoCambio);

        Task<bool> ValidarGuardar(TipoCambio pobjTipoCambio);
    }
}
