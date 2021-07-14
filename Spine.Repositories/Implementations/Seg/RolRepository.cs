using Spine.Constants.Cmn;
using Spine.Repositories.Interfaces.Seg;
using Spine.Entities.Seg;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Seg
{
    public class RolRepository : RepositoryBase, IRolRepository
    {
        public RolRepository() : base()
        {
        }

        public async Task<IEnumerable<Rol>> Consultar(int piRolId = -1, short piRolEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Rol>(
                    "Seg.pa_Rol_Consultar",
                    new SqlParameter("@piRolId", piRolId),
                    new SqlParameter("@piRolEstado", piRolEstado)
                );
            }
        }

        public async Task<Rol> Crear(Conexion pobjConexion, Rol pobjRol)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piRolId", pobjRol.iRolId){ Direction = ParameterDirection.Output },
                new SqlParameter("@psRolNombre", pobjRol.sRolNombre),
                new SqlParameter("@psRolDescripcion", pobjRol.sRolDescripcion),
                new SqlParameter("@piRolEstado", pobjRol.iRolEstado)
            };
            await pobjConexion.EjecutarAsync("Seg.pa_Rol_Crear", varrParametros);
            pobjRol.iRolId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piRolId").Value);
            return pobjRol;
        }

        public async Task<Rol> Editar(Conexion pobjConexion, Rol pobjRol)
        {
            await pobjConexion.EjecutarAsync(
                "Seg.pa_Rol_Editar",
                new SqlParameter("@piRolId", pobjRol.iRolId),
                new SqlParameter("@psRolNombre", pobjRol.sRolNombre),
                new SqlParameter("@psRolDescripcion", pobjRol.sRolDescripcion),
                new SqlParameter("@piRolEstado", pobjRol.iRolEstado)
            );
            return pobjRol;
        }

        public async Task<bool> ValidarGuardar(Rol pobjRol)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piRolId", pobjRol.iRolId),
                new SqlParameter("@psRolNombre", pobjRol.sRolNombre),
                new SqlParameter("@psMensaje", string.Empty){ Size = 250, Direction = ParameterDirection.Output }
            };

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarEscalarAsync("Seg.pa_Rol_ValidarGuardar", varrParametros);
            }

            string vsMensaje = varrParametros.FirstOrDefault(x => x.ParameterName == "@psMensaje").Value.ToString();
            if (!string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);
            return true;
        }

        public async Task<IEnumerable<RolOpcion>> ConsultarOpciones(int piRolId)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<RolOpcion>(
                        "Seg.pa_Rol_ConsultarOpciones",
                        new SqlParameter("@piRolId", piRolId),
                        new SqlParameter("@piEstadoActivado", EstadoConstant.Activado)
                    );
            }
        }

        public async Task<bool> EditarOpciones(Conexion pobjConexion, int piRolId, IEnumerable<RolOpcion> plstRolOpciones)
        {
            return (await pobjConexion.EjecutarAsync(
                    "Seg.pa_Rol_EditarOpciones",
                    new SqlParameter("@piRolId", piRolId),
                    new SqlParameter("@psRolOpciones", Metodos.ToJson(plstRolOpciones))
                ) > 0);
        }
    }
}