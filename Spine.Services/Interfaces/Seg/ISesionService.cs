using Spine.Entities.Seg;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Seg
{
    public interface ISesionService
    {
        Task<Sesion> Autenticar(Autenticar pobjAutenticar);
        
        Task<bool> CambiarClave(Sesion pobjSesion, CambiarClave pobjCambiarClave);

        Sesion RefrescarTokenSync(string psSesRefreshToken);
        
        bool ValidarSesionSync(string psSesAccessToken);
        
        bool ValidarAccionSync(string psSesAccessToken, string psAcnRuta, string psAcnMetodo);
        
        bool ValidarComponenteSync(string psSesAccessToken, string psCmpRuta);
    }
}
