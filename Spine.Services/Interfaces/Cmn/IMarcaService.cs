using Spine.Entities.Cmn;
using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cmn
{
    public interface IMarcaService
    {
        Task<IEnumerable<Marca>> Consultar(int piMarId = -1, string psMarNombre = "", short piMarEstado = -1);

        Task<Marca> Crear(Sesion pobjSesion, Marca pobjMarca);

        Task<Marca> Editar(Sesion pobjSesion, Marca pobjMarca);

        Task<Marca> Activar(Sesion pobjSesion, int piMarId);

        Task<Marca> Desactivar(Sesion pobjSesion, int piMarId);
    }
}