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
    public class CategoriaRepository : RepositoryBase, ICategoriaRepository
    {
        public CategoriaRepository() : base() { }

        public async Task<IEnumerable<Categoria>> Consultar(int piCategoriaId = -1, short piCtgTipoProducto = -1, string psCtgNombre = "", short piCtgEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return await vobjConexion.EjecutarConsultaAsync<Categoria>(
                    "Cmn.pa_Categoria_Consultar",
                    new SqlParameter("@piCategoriaId", piCategoriaId),
                    new SqlParameter("@piCtgTipoProducto", piCtgTipoProducto),
                    new SqlParameter("@psCtgNombre", psCtgNombre),
                    new SqlParameter("@piCtgEstado", piCtgEstado)
                );
            }
        }

        public async Task<Categoria> Crear(Conexion pobjConexion, Categoria pobjCategoria)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piCategoriaId", pobjCategoria.iCategoriaId) { Direction = ParameterDirection.Output },
                new SqlParameter("@piCtgTipoProducto",  pobjCategoria.iCtgTipoProducto),
                new SqlParameter("@psCtgNombre", pobjCategoria.sCtgNombre),
                new SqlParameter("@piCtgEstado", pobjCategoria.iCtgEstado)
            };
            await pobjConexion.EjecutarAsync("Cmn.pa_Categoria_Crear", varrParametros);
            pobjCategoria.iCategoriaId = Convert.ToInt16(varrParametros.First(x => x.ParameterName == "@piCategoriaId").Value);
            return pobjCategoria;
        }

        public async Task<Categoria> Editar(Conexion pobjConexion, Categoria pobjCategoria)
        {
            await pobjConexion.EjecutarAsync(
                "Cmn.pa_Categoria_Editar",
                new SqlParameter("@piCategoriaId", pobjCategoria.iCategoriaId),
                new SqlParameter("@piCtgTipoProducto", pobjCategoria.iCtgTipoProducto),
                new SqlParameter("@psCtgNombre", pobjCategoria.sCtgNombre),
                new SqlParameter("@piCtgEstado", pobjCategoria.iCtgEstado)
            );
            return pobjCategoria;
        }

        public async Task<bool> ValidarGuardar(Categoria pobjCategoria)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@piCategoriaId", pobjCategoria.iCategoriaId),
                new SqlParameter("@piCtgTipoProducto",  pobjCategoria.iCtgTipoProducto),
                new SqlParameter("@psCtgNombre", pobjCategoria.sCtgNombre),
                new SqlParameter("@psMensaje", string.Empty){ Direction = ParameterDirection.Output, Size = 250 }
            };

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                await vobjConexion.EjecutarAsync("Cmn.pa_Categoria_ValidarGuardar", varrParametros);
            }

            string vsMensaje = Convert.ToString(varrParametros.FirstOrDefault(x => x.ParameterName == "@psMensaje").Value);
            if (!string.IsNullOrEmpty(vsMensaje))
                throw Utilitarios.GetValidacion(vsMensaje);

            return true;
        }
    }
}
