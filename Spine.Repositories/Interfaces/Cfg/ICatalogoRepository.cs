using Spine.Entities.Cfg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cfg
{
    public interface ICatalogoRepository
    {
        Task<IEnumerable<Catalogo>> Consultar(int piCatalogoId = -1, short piCatEstado = -1);

        Task<IEnumerable<CatalogoItem>> ConsultarItems(int piCatalogoId, short piCatIteEstado = -1);
    }
}
