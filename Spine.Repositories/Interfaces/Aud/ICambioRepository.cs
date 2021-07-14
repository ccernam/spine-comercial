using Spine.Entities.Aud;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Aud
{
    public interface ICambioRepository
    {
        Task<Cambio> Crear(Conexion pobjConexion, Cambio pobjCambio);

        Task<IEnumerable<Cambio>> Consultar(int piEntId, int piCamRegistro);
    }
}
