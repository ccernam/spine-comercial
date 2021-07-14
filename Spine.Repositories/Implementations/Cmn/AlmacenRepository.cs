using Spine.Repositories.Interfaces.Cmn;
using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Repositories.Implementations.Cmn
{
    public class AlmacenRepository : RepositoryBase, IAlmacenRepository
    {
        public AlmacenRepository() : base() { }

        public async Task<IEnumerable<Almacen>> Consultar(int piAlmacenId = -1, int piSucursalId = -1, string psAlmNombre = "", short piAlmEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Almacen>(
                    "Cmn.pa_Almacen_Consultar",
                    new SqlParameter("@piAlmacenId", piAlmacenId),
                    new SqlParameter("@piSucursalId", piSucursalId),
                    new SqlParameter("@psAlmNombre", psAlmNombre),
                    new SqlParameter("@piAlmEstado", piAlmEstado)
                );
            }
        }

        public async Task<Almacen> Crear(Conexion pobjConexion, Almacen pobjAlmacen)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piAlmacenId", pobjAlmacen.iAlmacenId){ Direction = ParameterDirection.Output },
                new SqlParameter("@piSucursalId",  pobjAlmacen.iSucursalId),
                new SqlParameter("@psAlmNombre", pobjAlmacen.sAlmNombre),
                new SqlParameter("@psAlmCodigoSunat", pobjAlmacen.sAlmCodigoSunat),
                new SqlParameter("@piUbigeoId", pobjAlmacen.iUbigeoId),
                new SqlParameter("@psAlmDireccion", pobjAlmacen.sAlmDireccion),
                new SqlParameter("@piAlmEstado", pobjAlmacen.iAlmEstado)
            };
            await pobjConexion.EjecutarAsync("Cmn.pa_Almacen_Crear", varrParametros);
            pobjAlmacen.iAlmacenId = Convert.ToInt16(varrParametros.First(x => x.ParameterName == "@piAlmId").Value);
            return pobjAlmacen;
        }

        public async Task<Almacen> Editar(Conexion pobjConexion, Almacen pobjAlmacen)
        {
            await pobjConexion.EjecutarAsync(
                "Cmn.pa_Almacen_Editar",
                new SqlParameter("@piAlmacenId", pobjAlmacen.iAlmacenId),
                new SqlParameter("@piSucursalId", pobjAlmacen.iSucursalId),
                new SqlParameter("@psAlmNombre", pobjAlmacen.sAlmNombre),
                new SqlParameter("@psAlmCodigoSunat", pobjAlmacen.sAlmCodigoSunat),
                new SqlParameter("@piUbigeoId", pobjAlmacen.iUbigeoId),
                new SqlParameter("@psAlmDireccion", pobjAlmacen.sAlmDireccion),
                new SqlParameter("@piAlmEstado", pobjAlmacen.iAlmEstado)
            );
            return pobjAlmacen;
        }

        public async Task<bool> ValidarGuardar(Almacen pobjAlmacen)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piAlmacenId", pobjAlmacen.iAlmacenId),
                new SqlParameter("@piSucursalId",  pobjAlmacen.iSucursalId),
                new SqlParameter("@psAlmNombre", pobjAlmacen.sAlmNombre),
                new SqlParameter("@psMensaje", string.Empty){ Direction = ParameterDirection.Output },
            };

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync("Cmn.pa_Almacen_ValidarGuardar", varrParametros);
            }

            string vsMensaje = Convert.ToString(varrParametros.First(x => x.ParameterName == "@psMensaje").Value);
            if (!string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);

            return true;
        }
    }
}
