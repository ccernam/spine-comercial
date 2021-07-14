using Spine.Repositories.Interfaces.Seg;
using Spine.Entities.Seg;
using Spine.Librerias.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Seg
{
    public class SesionRepository : RepositoryBase, ISesionRepository
    {
        public SesionRepository() : base() { }

        public async Task<Usuario> Autenticar(string psUsuUsuario, string psUsuClave)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return (await vobjConexion.EjecutarConsultaAsync<Usuario>(
                    "Seg.pa_Sesion_Autenticar",
                    new SqlParameter("@psUsuUsuario", psUsuUsuario),
                    new SqlParameter("@psUsuClave", psUsuClave)
                )).FirstOrDefault();
            }
        }

        public async Task<Sesion> Crear(Sesion pobjSesion)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piSesionId", pobjSesion.iSesionId){ Direction = ParameterDirection.Output },
                new SqlParameter("@piUsuarioId", pobjSesion.iUsuarioId),
                new SqlParameter("@piAplicacionId", pobjSesion.iAplicacionId),
                new SqlParameter("@psSesAccessToken", pobjSesion.sSesAccessToken),
                new SqlParameter("@psSesRefreshToken", pobjSesion.sSesRefreshToken),
                new SqlParameter("@pdSesExpiracion", pobjSesion.dSesExpiracion),
                new SqlParameter("@piSesEstado", pobjSesion.iSesEstado),
                new SqlParameter("@pdSesCreacion", pobjSesion.dSesCreacion),
                new SqlParameter("@pdSesEdicion", pobjSesion.dSesEdicion)
            };

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync("Seg.pa_Sesion_Crear", varrParametros);
            }

            pobjSesion.iSesionId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piSesionId").Value);
            return pobjSesion;
        }

        public async Task<Sesion> Editar(Sesion pobjSesion)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync(
                    "Seg.pa_Sesion_Editar",
                    new SqlParameter("@piSesionId", pobjSesion.iSesionId),
                    new SqlParameter("@psSesAccessToken", pobjSesion.sSesAccessToken),
                    new SqlParameter("@psSesRefreshToken", pobjSesion.sSesRefreshToken),
                    new SqlParameter("@pdSesExpiracion", pobjSesion.dSesExpiracion),
                    new SqlParameter("@pdSesEdicion", pobjSesion.dSesEdicion)
                );
            }
            return pobjSesion;
        }

        public Sesion EditarSync(Sesion pobjSesion)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.Ejecutar(
                    "Seg.pa_Sesion_Editar",
                    new SqlParameter("@piSesionId", pobjSesion.iSesionId),
                    new SqlParameter("@psSesAccessToken", pobjSesion.sSesAccessToken),
                    new SqlParameter("@psSesRefreshToken", pobjSesion.sSesRefreshToken),
                    new SqlParameter("@pdSesExpiracion", pobjSesion.dSesExpiracion),
                    new SqlParameter("@pdSesEdicion", pobjSesion.dSesEdicion)
                );
            }
            return pobjSesion;
        }

        public IEnumerable<Sesion> ConsultarSync(int piSesionId = -1, string psSesAccessToken = "", string psSesRefreshToken = "", short piSesEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<Sesion>(
                    "Seg.pa_Sesion_Consultar",
                    new SqlParameter("@piSesionId", piSesionId),
                    new SqlParameter("@psSesAccessToken", psSesAccessToken),
                    new SqlParameter("@psSesRefreshToken", psSesRefreshToken),
                    new SqlParameter("@piSesEstado", piSesEstado)
                );
            }
        }

        public bool ValidarSesionSync(string psSesAccessToken)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@psSesAccessToken", psSesAccessToken),
                new SqlParameter("@plRespuesta", false) { Direction = ParameterDirection.Output }
            };
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.Ejecutar("Seg.pa_Sesion_Validar", varrParametros);
            }
            return Convert.ToBoolean(varrParametros.First(x => x.ParameterName == "@plRespuesta").Value);
        }

        public bool ValidarAccionSync(string psSesAccessToken, string psAcnRuta, string psAcnMetodo)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@psSesAccessToken", psSesAccessToken),
                new SqlParameter("@psAcnRuta", psAcnRuta),
                new SqlParameter("@psAcnMetodo", psAcnMetodo),
                new SqlParameter("@plRespuesta", false) { Direction = ParameterDirection.Output }
            };
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.Ejecutar("Seg.pa_Sesion_ValidarAccion", varrParametros);
            }
            return Convert.ToBoolean(varrParametros.First(x => x.ParameterName == "@plRespuesta").Value);
        }

        public bool ValidarComponenteSync(string psSesAccessToken, string psCmpRuta)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@psSesAccessToken", psSesAccessToken),
                new SqlParameter("@psCmpRuta", psCmpRuta),
                new SqlParameter("@plRespuesta", false) { Direction = ParameterDirection.Output }
            };
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.Ejecutar("Seg.pa_Sesion_ValidarComponente", varrParametros);
            }
            return Convert.ToBoolean(varrParametros.First(x => x.ParameterName == "@plRespuesta").Value);
        }
    }
}