using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cfg
{
    public interface IUbigeoRepository
    {
        Task<IEnumerable<Ubigeo>> Consultar(int piUbiId = -1, short piUbiTipo = -1, string psUbiRama = "", short piUbiEstado = -1);
    }
}
