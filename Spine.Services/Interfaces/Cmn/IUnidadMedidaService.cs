using Spine.Entities.Cmn;
using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cmn
{
    public interface IUnidadMedidaService
    {
        Task<IEnumerable<UnidadMedida>> Consultar(int piUniMedId = -1, string psUniMedNombre = "", short piUniMedServicio = -1, short piUniMedEstado = -1);
        Task<UnidadMedida> Crear(Sesion pobjSesion, UnidadMedida pobjUnidadMedida);
        Task<UnidadMedida> Editar(Sesion pobjSesion, UnidadMedida pobjUnidadMedida);
        Task<UnidadMedida> Activar(Sesion pobjSesion, int piUniMedId);
        Task<UnidadMedida> Desactivar(Sesion pobjSesion, int piUniMedId);
    }
}