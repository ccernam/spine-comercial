using Spine.Entities.Aud;
using Spine.Entities.Seg;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Aud
{
    public interface IErrorService
    {
        Task<Error> Crear(Sesion pobjSesion, Error pobjError);
    }
}
