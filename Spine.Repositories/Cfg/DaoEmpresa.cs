using Spine.Entities.Cfg;
using Spine.Librerias.Database;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Spine.Repositories.Cfg
{

    public class DaoEmpresa : RepositoryBase
    {
        public DaoEmpresa() : base()
        {
        }

        public List<Empresa> Consultar(short piEmpId = -1, string psEmpRuc = "", short piEmpEstado = -1)
        {
            using (var vobjConn = ConexionFactory.Instanciar())
            {
                return vobjConn.EjecutarConsulta<Empresa>(
                        "Cfg.paEmpresaConsultar",
                        new SqlParameter("@piEmpId", piEmpId),
                        new SqlParameter("@psEmpRuc", psEmpRuc),
                        new SqlParameter("@piEmpEstado", piEmpEstado)                        
                    );
            }
        }
    }
}
