using Spine.Entities.Seg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Seg
{
    public interface ISesionRepository
    {
        Task<Usuario> Autenticar(string psUsuUsuario, string psUsuClave);

        Task<Sesion> Crear(Sesion pobjSesion);

        Task<Sesion> Editar(Sesion pobjSesion);

        Sesion EditarSync(Sesion pobjSesion);

        IEnumerable<Sesion> ConsultarSync(int piSesionId = -1, string psSesAccessToken = "", string psSesRefreshToken = "", short piSesEstado = -1);

        bool ValidarSesionSync(string psSesAccessToken);

        bool ValidarAccionSync(string psSesAccessToken, string psAcnRuta, string psAcnMetodo);

        bool ValidarComponenteSync(string psSesAccessToken, string psCmpRuta);
    }
}