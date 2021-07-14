using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Cfg
{
    public interface ICatalogoService
    {
        Task<IEnumerable<Catalogo>> Consultar(int piCatalogoId = -1, short piCatEstado = -1);

        Task<IEnumerable<CatalogoItem>> ConsultarItems(int piCatalogoId, short piCatIteEstado = -1);
    }
}
