using Spine.Repositories.Interfaces.Cfg;
using Spine.Entities.Cfg;
using Spine.Services.Interfaces.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Cfg
{
    public class CatalogoService : ServiceBase, ICatalogoService
    {
        private readonly ICatalogoRepository _objCatalogoRepository;

        public CatalogoService(ICatalogoRepository pobjCatalogoRepository) { _objCatalogoRepository = pobjCatalogoRepository; }

        public async Task<IEnumerable<Catalogo>> Consultar(int piCatalogoId = -1, short piCatEstado = -1)
        {
            return await _objCatalogoRepository.Consultar(piCatalogoId: piCatalogoId, piCatEstado: piCatEstado);
        }

        public async Task<IEnumerable<CatalogoItem>> ConsultarItems(int piCatalogoId, short piCatIteEstado = -1)
        {
            return await _objCatalogoRepository.ConsultarItems(piCatalogoId: piCatalogoId, piCatIteEstado: piCatIteEstado);
        }
    }
}
