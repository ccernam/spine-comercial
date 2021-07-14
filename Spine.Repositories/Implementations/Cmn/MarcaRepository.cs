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
    public class MarcaRepository : RepositoryBase, IMarcaRepository
    {
        public MarcaRepository() : base() { }

        public async Task<IEnumerable<Marca>> Consultar(int piMarId = -1, string psMarNombre = "", short piMarEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Marca>(
                    "Cmn.pa_Marca_Consultar",
                    new SqlParameter("@piMarId", piMarId),
                    new SqlParameter("@psMarNombre", psMarNombre),
                    new SqlParameter("@piMarEstado", piMarEstado)
                );
            }
        }

        public async Task<Marca> Crear(Conexion pobjConexion, Marca pobjMarca)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piMarId", pobjMarca.iMarId) { Direction = ParameterDirection.Output },
                new SqlParameter("@psMarNombre", pobjMarca.sMarNombre),
                new SqlParameter("@piMarEstado", pobjMarca.iMarEstado)
            };
            await pobjConexion.EjecutarAsync("Cmn.pa_Marca_Crear", varrParametros);
            pobjMarca.iMarId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piMarId").Value);
            return pobjMarca;
        }

        public async Task<Marca> Editar(Conexion pobjConexion, Marca pobjMarca)
        {
            await pobjConexion.EjecutarAsync(
               "Cmn.pa_Marca_Editar",
               new SqlParameter("@piMarId", pobjMarca.iMarId),
               new SqlParameter("@psMarNombre", pobjMarca.sMarNombre),
               new SqlParameter("@piMarEstado", pobjMarca.iMarEstado)
           );
            return pobjMarca;
        }

        public async Task<bool> ValidarGuardar(Marca pobjMarca)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piMarId", pobjMarca.iMarId),
                new SqlParameter("@psMarNombre", pobjMarca.sMarNombre),
                new SqlParameter("@psMensaje", string.Empty){ Direction = ParameterDirection.Output, Size = 250 }
            };

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync("Cmn.pa_Marca_ValidarGuardar", varrParametros);
            }

            string vsMensaje = Convert.ToString(varrParametros.FirstOrDefault(x => x.ParameterName == "@psMensaje").Value);
            if (!string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);

            return true;
        }
    }
}
