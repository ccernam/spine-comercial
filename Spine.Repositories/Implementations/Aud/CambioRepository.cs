using Spine.Repositories.Interfaces.Aud;
using Spine.Entities.Aud;
using Spine.Librerias.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Aud
{
    public class CambioRepository : RepositoryBase, ICambioRepository
    {
        public CambioRepository() : base()
        {
        }

        public async Task<Cambio> Crear(Conexion pobjConexion, Cambio pobjCambio)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piCambioId", pobjCambio.iCambioId) { Direction = ParameterDirection.InputOutput },
                new SqlParameter("@pdCamFecha", pobjCambio.dCamFecha),
                new SqlParameter("@piUsuarioId", pobjCambio.iUsuarioId == 0 ? 1 : pobjCambio.iUsuarioId), // TODO:
                new SqlParameter("@piAplicacionId", pobjCambio.iAplicacionId == 0 ? 1 : pobjCambio.iAplicacionId), // TODO:
                new SqlParameter("@piCamRegistro", pobjCambio.iCamRegistro),
                new SqlParameter("@piAccionId", pobjCambio.iAccionId)
            };
            await pobjConexion.EjecutarAsync("Aud.pa_Cambio_Crear", varrParametros);
            pobjCambio.iCambioId = Convert.ToInt32(varrParametros.FirstOrDefault(x => x.ParameterName == "@piCambioId").Value);
            return pobjCambio;
        }

        public async Task<IEnumerable<Cambio>> Consultar(int piEntId, int piCamRegistro)
        {
            using (var vobjConn = ConexionFactory.Instanciar())
            {
                return await vobjConn.EjecutarConsultaAsync<Cambio>(
                        "Aud.pa_Cambio_Consultar",
                        new SqlParameter("@piEntId", piEntId),
                        new SqlParameter("@piCamRegistro", piCamRegistro)
                    );
            }
        }
    }
}
