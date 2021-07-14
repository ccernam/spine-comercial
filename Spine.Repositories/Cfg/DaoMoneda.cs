using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Data.SqlClient;
using Spine.Librerias.Database;

namespace Spine.Repositories.Cfg
{

    public class DaoMoneda : RepositoryBase
    {
        public DaoMoneda() : base()
        {
        }

        public List<Moneda> Consultar(short piMonId = -1, short piMonEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<Moneda>(
                        "Cfg.paMonedaConsultar",
                        new SqlParameter("@piMonId", piMonId),
                        new SqlParameter("@piMonEstado", piMonEstado)
                    );
            }
        }
    }
}
