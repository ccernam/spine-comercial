using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using Spine.Repositories.Interfaces.Cfg;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cfg
{
    public class MonedaRepository : IMonedaRepository
    {
        public MonedaRepository() { }

        public async Task<IEnumerable<Moneda>> Consultar(int piMonedaId = -1, string psMonNombre = "", short piMonEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Moneda>(
                    "Cfg.pa_Moneda_Consultar",
                    new SqlParameter("@piMonedaId", piMonedaId),
                    new SqlParameter("@psMonNombre", psMonNombre),
                    new SqlParameter("@piMonEstado", piMonEstado)
                );
            }
        }
    }
}
