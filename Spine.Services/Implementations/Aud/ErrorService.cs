using Spine.Repositories.Interfaces.Aud;
using Spine.Entities.Aud;
using Spine.Entities.Seg;
using Spine.Services.Interfaces.Aud;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Aud
{
    public class ErrorService : ServiceBase, IErrorService
    {
        private readonly IErrorRepository _objErrorRepository;

        public ErrorService(IErrorRepository pobjErrorRepository) { _objErrorRepository = pobjErrorRepository; }

        public Task<Error> Crear(Sesion pobjSesion, Error pobjError)
        {
            return _objErrorRepository.Crear(pobjError);
        }
    }
}
