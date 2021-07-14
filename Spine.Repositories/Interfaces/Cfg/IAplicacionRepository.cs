using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cfg
{
    public interface IAplicacionRepository
    {
        Task<IEnumerable<Aplicacion>> Consultar(int piAplicacionId = -1, string psAppNombre = "", short piAppEstado = -1);
    }
}
