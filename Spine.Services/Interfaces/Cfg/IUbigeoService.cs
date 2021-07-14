using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cfg
{
    public interface IUbigeoService
    {
        Task<IEnumerable<Ubigeo>> Consultar(int piUbiId = -1, short piUbiTipo = -1, string psUbiRama = "", short piUbiEstado = -1);
    }
}
