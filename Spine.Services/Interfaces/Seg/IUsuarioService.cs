using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Seg
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> Consultar(int piUsuarioId = -1, string psUsuUsuario = "", string psUsuCorreo = "", int piSucursalId = -1, int piPersonaId = -1, short piUsuEstado = -1);

        Task<Usuario> Crear(Sesion pobjSesion, Usuario pobjUsuario);

        Task<Usuario> Editar(Sesion pobjSesion, Usuario pobjUsuario);

        Task<Usuario> Activar(Sesion pobjSesion, int piUsuarioId);

        Task<Usuario> Desactivar(Sesion pobjSesion, int piUsuarioId);

        Task<bool> ReiniciarClave(Sesion pobjSesion, int piUsuarioId);

        Task<IEnumerable<UsuarioRol>> ConsultarRoles(int piUsuarioId);

        Task<bool> EditarRoles(Sesion pobjSesion, int piUsuarioId, IEnumerable<UsuarioRol> plstUsuarioRoles);
    }
}
