using Spine.Repositories.Interfaces.Aud;
using Spine.Entities.Aud;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Aud
{
    public class ErrorRepository : RepositoryBase, IErrorRepository
    {
        public ErrorRepository() { }

        public async Task<Error> Crear(Error pobjError)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                SqlParameter[] varrParametros = new SqlParameter[] {
                    new SqlParameter("@piErrorId", pobjError.iErrorId) { Direction = ParameterDirection.Output },
                    new SqlParameter("@piUsuarioId", pobjError.iUsuarioId),
                    new SqlParameter("@piAplicacionId", pobjError.iAplicacionId),
                    new SqlParameter("@pdErrFecha", Utilitarios.GetFechaSistema()),
                    new SqlParameter("@psErrMensaje", pobjError.sErrMensaje),
                    new SqlParameter("@psErrPila", pobjError.sErrPila),
                    new SqlParameter("@psErrParametros", pobjError.sErrParametros)
                };
                await vobjConexion.EjecutarAsync("Aud.pa_Error_Crear", varrParametros);
                pobjError.iErrorId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piErrorId").Value);
                return pobjError;
            }
        }
    }
}