using Spine.Repositories.Interfaces.Aud;
using Spine.Entities.Aud;
using Spine.Services.Interfaces.Aud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Aud
{
    public class CambioService : ServiceBase, ICambioService
    {
        private readonly ICambioRepository _objCambioRepository;

        public CambioService(ICambioRepository pobjCambioRepository) : base()
        {
            _objCambioRepository = pobjCambioRepository;
        }

        public async Task<IEnumerable<Cambio>> Consultar(int piEntId, int piCamRegistro)
        {
            return await _objCambioRepository.Consultar(piEntId: piEntId, piCamRegistro: piCamRegistro);
        }
    }
}
