using Spine.Entities.Cmn;
using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cmn
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> Consultar(int piCategoriaId = -1, short piCtgTipoProducto = -1, string psCtgNombre = "", short piCtgEstado = -1);

        Task<Categoria> Crear(Sesion pobjSesion, Categoria pobjCategoria);

        Task<Categoria> Editar(Sesion pobjSesion, Categoria pobjCategoria);

        Task<Categoria> Activar(Sesion pobjSesion, int piCategoriaId);

        Task<Categoria> Desactivar(Sesion pobjSesion, int piCategoriaId);
    }
}
