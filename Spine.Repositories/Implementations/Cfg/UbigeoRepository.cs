using Spine.Repositories.Interfaces.Cfg;
using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cfg
{
    public class UbigeoRepository : RepositoryBase, IUbigeoRepository
    {
        public UbigeoRepository() : base() { }

        public async Task<IEnumerable<Ubigeo>> Consultar(int piUbiId = -1, short piUbiTipo = -1, string psUbiRama = "", short piUbiEstado = 0)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Ubigeo>(
                        "Cfg.pa_Ubigeo_Consultar",
                        new SqlParameter("@piUbiId", piUbiId),
                        new SqlParameter("@piUbiTipo", piUbiTipo),
                        new SqlParameter("@psUbiRama", psUbiRama),
                        new SqlParameter("@piUbiEstado", piUbiEstado)
                    );
            }
        }
    }
}
