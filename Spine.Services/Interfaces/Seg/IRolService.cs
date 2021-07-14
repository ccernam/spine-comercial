using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Seg
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> Consultar(int piRolId = -1, short piRolEstado = -1);

        Task<Rol> Crear(Sesion pobjSesion, Rol pobjRol);

        Task<Rol> Editar(Sesion pobjSesion, Rol pobjRol);

        Task<Rol> Activar(Sesion pobjSesion, int piRolId);

        Task<Rol> Desactivar(Sesion pobjSesion, int piRolId);

        Task<IEnumerable<RolOpcion>> ConsultarOpciones(int piRolId);

        Task<bool> EditarOpciones(Sesion pobjSesion, int piRolId, IEnumerable<RolOpcion> plstRolOpciones);
    }
}