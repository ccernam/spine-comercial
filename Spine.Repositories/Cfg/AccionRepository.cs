using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Cfg
{
    public class AccionRepository : RepositoryBase
    {
        public AccionRepository() : base()
        {
        }

        public async Task<IEnumerable<Accion>> Consultar(short piAcnEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Accion>(
                        "Cfg.pa_Accion_Consultar",
                        new SqlParameter("@piAcnEstado", piAcnEstado)
                    );
            }
        }
    }
}
