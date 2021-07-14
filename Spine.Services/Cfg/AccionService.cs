using Spine.Repositories.Cfg;
using Spine.Entities.Cfg;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Cfg
{
    public class AccionService : ServiceBase
    {
        private readonly AccionRepository _objAccionRepository;

        public AccionService() : base()
        {
            _objAccionRepository = new AccionRepository();
        }

        public async Task<IEnumerable<Accion>> Consultar(short piAcnEstado = -1)
        {
            return await _objAccionRepository.Consultar(piAcnEstado: piAcnEstado);
        }
    }
}
