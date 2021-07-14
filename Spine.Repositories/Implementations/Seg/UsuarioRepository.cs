using Spine.Entities.Seg;
using Spine.Librerias.Database;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System;
using Spine.Librerias.General;
using System.Threading.Tasks;
using Spine.Constants.Cmn;
using Spine.Repositories.Interfaces.Seg;

namespace Spine.Repositories.Implementations.Seg
{
    public class UsuarioRepository : RepositoryBase, IUsuarioRepository
    {
        public UsuarioRepository() { }

        public async Task<IEnumerable<Usuario>> Consultar(
            int piUsuarioId = -1, string psUsuUsuario = "", string psUsuCorreo = "", int piSucursalId = -1,
            int piPersonaId = -1, short piUsuEstado = -1
        )
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Usuario>(
                    "Seg.pa_Usuario_Consultar",
                    new SqlParameter("@piUsuarioId", piUsuarioId),
                    new SqlParameter("@psUsuUsuario", psUsuUsuario),
                    new SqlParameter("@psUsuCorreo", psUsuCorreo),
                    new SqlParameter("@piSucursalId", piSucursalId),
                    new SqlParameter("@piPersonaId", piPersonaId),
                    new SqlParameter("@piUsuEstado", piUsuEstado)
                );
            }
        }

        public async Task<Usuario> Crear(Conexion pobjConexion, Usuario pobjUsuario)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piUsuarioId", pobjUsuario.iUsuarioId){ Direction = ParameterDirection.Output },
                new SqlParameter("@psUsuUsuario",  pobjUsuario.sUsuUsuario),
                new SqlParameter("@psUsuCorreo", pobjUsuario.sUsuCorreo),
                new SqlParameter("@psUsuClave", pobjUsuario.sUsuClave),
                new SqlParameter("@piSucursalId", pobjUsuario.iSucursalId),
                new SqlParameter("@piPersonaId", pobjUsuario.iPersonaId),
                new SqlParameter("@piUsuEstado", pobjUsuario.iUsuEstado)
            };
            await pobjConexion.EjecutarAsync("Seg.pa_Usuario_Crear", varrParametros);
            pobjUsuario.iUsuarioId = Convert.ToInt16(varrParametros.First(x => x.ParameterName == "@piUsuarioId").Value);
            return pobjUsuario;
        }

        public async Task<Usuario> Editar(Conexion pobjConexion, Usuario pobjUsuario)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piUsuarioId", pobjUsuario.iUsuarioId),
                new SqlParameter("@psUsuUsuario",  pobjUsuario.sUsuUsuario),
                new SqlParameter("@psUsuCorreo", pobjUsuario.sUsuCorreo),
                new SqlParameter("@piSucursalId", pobjUsuario.iSucursalId),
                new SqlParameter("@piPersonaId", pobjUsuario.iPersonaId),
                new SqlParameter("@piUsuEstado", pobjUsuario.iUsuEstado)
            };
            await pobjConexion.EjecutarAsync("Seg.pa_Usuario_Editar", varrParametros);
            return pobjUsuario;
        }

        public async Task<bool> ValidarGuardar(Usuario pobjUsuario)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piUsuarioId", pobjUsuario.iUsuarioId),
                new SqlParameter("@psUsuUsuario",  pobjUsuario.sUsuUsuario),
                new SqlParameter("@psUsuCorreo", pobjUsuario.sUsuCorreo),
                new SqlParameter("@piPersonaId", pobjUsuario.iPersonaId),
                new SqlParameter("@psMensaje", string.Empty){ Direction = ParameterDirection.Output, Size = 250 }
            };
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync("Seg.pa_Usuario_ValidarGuardar", varrParametros);
            }

            string vsMensaje = varrParametros.First(x => x.ParameterName == "@psMensaje").Value.ToString();
            if (!string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);

            return true;
        }

        public async Task<bool> ActualizarClave(Conexion vobjConexion, int piUsuarioId, string psUsuClave)
        {
            return await vobjConexion.EjecutarAsync(
                    "Seg.pa_Usuario_ActualizarClave",
                    new SqlParameter("@piUsuarioId", piUsuarioId),
                    new SqlParameter("@psUsuClave", psUsuClave)
                ) == 1;
        }

        public async Task<IEnumerable<UsuarioRol>> ConsultarRoles(int piUsuarioId)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<UsuarioRol>(
                    "Seg.pa_Usuario_ConsultarRoles",
                    new SqlParameter("@piUsuarioId", piUsuarioId),
                    new SqlParameter("@piEstadoActivado", EstadoConstant.Activado)
                );
            }
        }

        public async Task<bool> EditarRoles(Conexion pobjConexion, int piUsuarioId, IEnumerable<UsuarioRol> plstUsuarioRoles)
        {



            return (await pobjConexion.EjecutarAsync(
                "Seg.pa_Usuario_EditarRoles",
                new SqlParameter("@piUsuarioId", piUsuarioId),
                new SqlParameter("@psUsuarioRol", Metodos.ToJson(plstUsuarioRoles))
            )) != 0;
        }
    }
}