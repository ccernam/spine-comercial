using Spine.Repositories.Interfaces.Cfg;
using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cfg
{
    public class AplicacionRepository : RepositoryBase, IAplicacionRepository
    {
        public AplicacionRepository() : base()
        {
        }

        public async Task<IEnumerable<Aplicacion>> Consultar(int piAplicacionId = -1, string psAppNombre = "", short piAppEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Aplicacion>(
                        "Cfg.pa_Aplicacion_Consultar",
                        new SqlParameter("@piAplicacionId", piAplicacionId),
                        new SqlParameter("@psAppNombre", psAppNombre),
                        new SqlParameter("@piAppEstado", piAppEstado)
                    );
            }
        }
    }
}
