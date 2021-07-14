using Spine.Entities.Cmn;
using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cmn
{
    public interface IAlmacenService
    {
        Task<IEnumerable<Almacen>> Consultar(int piAlmacenId = -1, int piSucursalId = -1, string psAlmNombre = "", short piAlmEstado = -1);

        Task<Almacen> Crear(Sesion pobjSesion, Almacen pobjAlmacen);

        Task<Almacen> Editar(Sesion pobjSesion, Almacen pobjAlmacen);

        Task<Almacen> Activar(Sesion pobjSesion, int piAlmacenId);

        Task<Almacen> Desactivar(Sesion pobjSesion, int piAlmacenId);
    }
}
