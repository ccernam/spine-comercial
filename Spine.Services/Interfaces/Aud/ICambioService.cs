using Spine.Entities.Aud;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Services.Interfaces.Aud
{
    public interface ICambioService
    {
        Task<IEnumerable<Cambio>> Consultar(int piEntId, int piCamRegistro);
    }
}
