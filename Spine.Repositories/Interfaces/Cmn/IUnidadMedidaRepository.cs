using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cmn
{
    public interface IUnidadMedidaRepository
    {
        Task<IEnumerable<UnidadMedida>> Consultar(int piUniMedId = -1, string psUniMedNombre = "", short piUniMedServicio = -1, short piUniMedEstado = -1);
        Task<UnidadMedida> Crear(Conexion pobjConexion, UnidadMedida pobjUnidadMedida);
        Task<UnidadMedida> Editar(Conexion pobjConexion, UnidadMedida pobjUnidadMedida);
        Task<bool> ValidarGuardar(UnidadMedida pobjUnidadMedida);
    }
}
