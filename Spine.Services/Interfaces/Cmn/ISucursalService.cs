using Spine.Entities.Cmn;
using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cmn
{
    public interface ISucursalService
    {
        Task<IEnumerable<Sucursal>> Consultar(int piSucId = -1, string psSucNombre = "", short piSucEstado = -1);

        Task<Sucursal> Crear(Sesion pobjSesion, Sucursal pobjSucursal);

        Task<Sucursal> Editar(Sesion pobjSesion, Sucursal pobjSucursal);

        Task<Sucursal> Activar(Sesion pobjSesion, int piSucId);

        Task<Sucursal> Desactivar(Sesion pobjSesion, int piSucId);
    }
}
