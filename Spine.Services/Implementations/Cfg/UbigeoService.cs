using Spine.Repositories.Interfaces.Cfg;
using Spine.Entities.Cfg;
using Spine.Services.Interfaces.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Cfg
{
    public class UbigeoService : ServiceBase, IUbigeoService
    {
        private readonly IUbigeoRepository _objUbigeoRepository;

        public UbigeoService(IUbigeoRepository pobjUbigeoRepository) : base()
        {
            _objUbigeoRepository = pobjUbigeoRepository;
        }

        public async Task<IEnumerable<Ubigeo>> Consultar(int piUbiId = -1, short piUbiTipo = -1, string psUbiRama = "", short piUbiEstado = -1)
        {
            return await _objUbigeoRepository.Consultar(piUbiId: piUbiId, piUbiTipo: piUbiTipo, psUbiRama: psUbiRama, piUbiEstado: piUbiEstado);
        }
    }
}
