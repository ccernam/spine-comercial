using Spine.Repositories.Interfaces.Cfg;
using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cfg
{
    public class CatalogoRepository : RepositoryBase, ICatalogoRepository
    {
        public CatalogoRepository() { }

        public async Task<IEnumerable<Catalogo>> Consultar(int piCatalogoId = -1, short piCatEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Catalogo>(
                    "Cfg.pa_Catalogo_Consultar",
                    new SqlParameter("@piCatalogoId", piCatalogoId),
                    new SqlParameter("@piCatEstado", piCatEstado)
                );
            }
        }

        public async Task<IEnumerable<CatalogoItem>> ConsultarItems(int piCatalogoId, short piCatIteEstado = -1)
        {
            using (var vobjConn = ConexionFactory.Instanciar())
            {
                return await vobjConn.EjecutarConsultaAsync<CatalogoItem>(
                        "Cfg.pa_Catalogo_ConsultarItems",
                        new SqlParameter("@piCatalogoId", piCatalogoId),
                        new SqlParameter("@piCatIteEstado", piCatIteEstado)
                    );
            }
        }
    }
}
