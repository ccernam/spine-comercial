using Spine.Repositories.Interfaces.Cmn;
using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cmn
{
    public class UnidadMedidaRepository : IUnidadMedidaRepository
    {
        public UnidadMedidaRepository() { }

        public async Task<IEnumerable<UnidadMedida>> Consultar(int piUniMedId = -1, string psUniMedNombre = "", short piUniMedServicio = -1, short piUniMedEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<UnidadMedida>(
                    "Cmn.pa_UnidadMedida_Consultar",
                    new SqlParameter("@piUniMedId", piUniMedId),
                    new SqlParameter("@psUniMedNombre", psUniMedNombre),
                    new SqlParameter("@piUniMedServicio", piUniMedServicio),
                    new SqlParameter("@piUniMedEstado", piUniMedEstado)
                );
            }
        }
        public async Task<UnidadMedida> Crear(Conexion pobjConexion, UnidadMedida pobjUnidadMedida)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piUniMedId", pobjUnidadMedida.iUnidadMedidaId) { Direction = ParameterDirection.Output },
                new SqlParameter("@psUniMedNombre", pobjUnidadMedida.sUniMedNombre),
                new SqlParameter("@psUniMedSimbolo", pobjUnidadMedida.sUniMedSimbolo),
                new SqlParameter("@psUniMedCodigoIso",pobjUnidadMedida.sUniMedCodigoIso),
                new SqlParameter("@plUniMedDecimales", pobjUnidadMedida.lUniMedDecimales ? 1 : 0),
                new SqlParameter("@plUniMedServicio", pobjUnidadMedida.lUniMedServicio),
                new SqlParameter("@piUniMedEstado", pobjUnidadMedida.iUniMedEstado)
            };
            await pobjConexion.EjecutarAsync("Cmn.pa_UnidadMedida_Crear", varrParametros);
            pobjUnidadMedida.iUnidadMedidaId = Convert.ToInt16(varrParametros.First(x => x.ParameterName == "@piUniMedId").Value);
            return pobjUnidadMedida;
        }
        public async Task<UnidadMedida> Editar(Conexion pobjConexion, UnidadMedida pobjUnidadMedida)
        {
            await pobjConexion.EjecutarAsync(
                "Cmn.pa_UnidadMedida_Editar",
                new SqlParameter("@piUniMedId", pobjUnidadMedida.iUnidadMedidaId),
                new SqlParameter("@psUniMedNombre", pobjUnidadMedida.sUniMedNombre),
                new SqlParameter("@psUniMedSimbolo", pobjUnidadMedida.sUniMedSimbolo),
                new SqlParameter("@psUniMedCodigoIso", pobjUnidadMedida.sUniMedCodigoIso),
                new SqlParameter("@plUniMedDecimales", pobjUnidadMedida.lUniMedDecimales ? 1 : 0),
                new SqlParameter("@plUniMedServicio", pobjUnidadMedida.lUniMedServicio ? 1 : 0),
                new SqlParameter("@piUniMedEstado", pobjUnidadMedida.iUniMedEstado)
            );
            return pobjUnidadMedida;
        }
        public async Task<bool> ValidarGuardar(UnidadMedida pobjUnidadMedida)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piUniMedId", pobjUnidadMedida.iUnidadMedidaId),
                new SqlParameter("@psUniMedNombre", pobjUnidadMedida.sUniMedNombre),
                new SqlParameter("@psUniMedSimbolo", pobjUnidadMedida.sUniMedSimbolo),
                new SqlParameter("@psUniMedCodigoIso",pobjUnidadMedida.sUniMedCodigoIso),
                new SqlParameter("@psMensaje", string.Empty) { Direction = ParameterDirection.Output }
            };
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync("Cmn.pa_UnidadMedida_ValidarGuardar", varrParametros);
            }

            string vsMensaje = Convert.ToString(varrParametros.First(x => x.ParameterName == "@piUniMedId").Value);
            if (string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);

            return true;
        }
    }
}
