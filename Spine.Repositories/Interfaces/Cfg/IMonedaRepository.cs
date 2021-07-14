using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cfg
{
    public interface IMonedaRepository
    {
        Task<IEnumerable<Moneda>> Consultar(int piMonedaId = -1, string psMonNombre = "", short piMonEstado = -1);
    }
}
