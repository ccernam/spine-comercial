using Spine.Entities.Seg;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Seg
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> Consultar(int piUsuarioId = -1, string psUsuUsuario = "", string psUsuCorreo = "", int piSucursalId = -1, int piPersonaId = -1, short piUsuEstado = -1);

        Task<Usuario> Crear(Conexion pobjConexion, Usuario pobjUsuario);

        Task<Usuario> Editar(Conexion pobjConexion, Usuario pobjUsuario);

        Task<bool> ValidarGuardar(Usuario pobjUsuario);

        Task<bool> ActualizarClave(Conexion pobjConexion, int piUsuarioId, string psUsuClave);

        Task<IEnumerable<UsuarioRol>> ConsultarRoles(int piUsuarioId);

        Task<bool> EditarRoles(Conexion pobjConexion, int piUsuarioId, IEnumerable<UsuarioRol> plstUsuarioRoles);
    }
}
