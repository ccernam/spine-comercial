using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cmn
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> Consultar(int piCategoriaId = -1, short piCtgTipoProducto = -1, string psCtgNombre = "", short piCtgEstado = -1);

        Task<Categoria> Crear(Conexion pobjConexion, Categoria pobjCategoria);

        Task<Categoria> Editar(Conexion pobjConexion, Categoria pobjCategoria);

        Task<bool> ValidarGuardar(Categoria pobjCategoria);
    }
}
