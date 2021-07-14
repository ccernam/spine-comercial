using Spine.Entities.Seg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Seg
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> Consultar(int piRolId = -1, short piRolEstado = -1);

        Task<Rol> Crear(Conexion pobjConexion, Rol pobjRol);

        Task<Rol> Editar(Conexion pobjConexion, Rol pobjRol);

        Task<bool> ValidarGuardar(Rol pobjRol);

        Task<IEnumerable<RolOpcion>> ConsultarOpciones(int piRolId);

        Task<bool> EditarOpciones(Conexion pobjConexion, int piRolId, IEnumerable<RolOpcion> plstRolOpciones);
    }
}
