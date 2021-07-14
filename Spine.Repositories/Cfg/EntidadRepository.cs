using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Cfg
{
    public class EntidadRepository : RepositoryBase
    {
        public EntidadRepository() : base()
        {
        }

        public async Task<List<Entidad>> Consultar(short piEntEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Entidad>(
                    "Cfg.pa_Entidad_Consultar",
                    new SqlParameter("@piEntEstado", piEntEstado)
                );
            }
        }
    }
}
