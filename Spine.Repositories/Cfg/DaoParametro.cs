using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Spine.Repositories.Cfg
{

    public class DaoParametro : RepositoryBase
    {
        public DaoParametro() : base()
        {
        }

        public List<Parametro> Consultar(short piParEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<Parametro>(
                        "Cfg.pa_Parametro_Consultar",
                        new SqlParameter("@piParEstado", piParEstado)
                    );
            }
        }
    }
}
