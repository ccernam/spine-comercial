using Spine.Repositories.Cfg;
using Spine.Entities.Cfg;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Cfg
{
    public class EntidadService : ServiceBase
    {
        private readonly EntidadRepository _objEntidadRepository;

        public EntidadService() : base()
        {
            _objEntidadRepository = new EntidadRepository();
        }

        public async Task<IEnumerable<Entidad>> Consultar(short piEntEstado = -1)
        {
            return await _objEntidadRepository.Consultar(piEntEstado: piEntEstado);
        }
    }
}
