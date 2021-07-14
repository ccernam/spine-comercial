using Spine.Entities.Cmn;
using System.Collections.Generic;
using Spine.Librerias.Database;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Spine.Repositories.Interfaces.Cmn;
using Spine.Librerias.General;

namespace Spine.Repositories.Implementations.Cmn
{
    public class SucursalRepository : RepositoryBase, ISucursalRepository
    {
        public SucursalRepository() : base() { }

        public async Task<IEnumerable<Sucursal>> Consultar(int piSucId = -1, string psSucNombre = "", short piSucEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Sucursal>(
                    "Cmn.pa_Sucursal_Consultar",
                    new SqlParameter("@piSucId", piSucId),
                    new SqlParameter("@psSucNombre", psSucNombre),
                    new SqlParameter("@piSucEstado", piSucEstado)
                );
            }
        }

        public async Task<Sucursal> Crear(Conexion pobjConexion, Sucursal pobjSucursal)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piSucId", pobjSucursal.iSucursalId) { Direction = ParameterDirection.Output },
                new SqlParameter("@psSucNombre", pobjSucursal.sSucNombre),
                new SqlParameter("@piUbiId", pobjSucursal.iUbiId),
                new SqlParameter("@psSucDireccion", pobjSucursal.sSucDireccion ),
                new SqlParameter("@piSucEstado", pobjSucursal.iSucEstado)
            };
            await pobjConexion.EjecutarAsync("Cmn.pa_Sucursal_Crear", varrParametros);
            pobjSucursal.iSucursalId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piSucId").Value);
            return pobjSucursal;
        }


        public async Task<Sucursal> Editar(Conexion pobjConexion, Sucursal pobjSucursal)
        {
            await pobjConexion.EjecutarAsync(
                "Cmn.pa_Sucursal_Editar",
                new SqlParameter("@piSucId", pobjSucursal.iSucursalId),
                new SqlParameter("@psSucNombre", pobjSucursal.sSucNombre),
                new SqlParameter("@piUbiId", pobjSucursal.iUbiId),
                new SqlParameter("@psSucDireccion", pobjSucursal.sSucDireccion),
                new SqlParameter("@piSucEstado", pobjSucursal.iSucEstado)
            );
            return pobjSucursal;
        }

        public async Task<bool> ValidarGuardar(Sucursal pobjSucursal)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piSucId", pobjSucursal.iSucursalId),
                new SqlParameter("@psSucNombre", pobjSucursal.sSucNombre),
                new SqlParameter("@psMensaje", string.Empty){ Direction = ParameterDirection.Output }
            };

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarEscalarAsync("Cmn.pa_Sucursal_ValidarGuardar", varrParametros);
            }

            string vsMensaje = Convert.ToString(varrParametros.First(x => x.ParameterName == "@psMensaje").Value);
            if (!string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);

            return true;
        }
    }
}