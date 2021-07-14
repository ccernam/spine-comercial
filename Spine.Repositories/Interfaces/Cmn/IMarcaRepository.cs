using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cmn
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> Consultar(int piMarId = -1, string psMarNombre = "", short piMarEstado = -1);

        Task<Marca> Crear(Conexion pobjConexion, Marca pobjMarca);

        Task<Marca> Editar(Conexion pobjConexion, Marca pobjMarca);

        Task<bool> ValidarGuardar(Marca pobjMarca);
    }
}
