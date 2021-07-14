using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cmn
{
    public interface IAlmacenRepository
    {
        Task<IEnumerable<Almacen>> Consultar(int piAlmacenId = -1, int piSucursalId = -1, string psAlmNombre = "", short piAlmEstado = -1);

        Task<Almacen> Crear(Conexion pobjConexion, Almacen pobjAlmacen);

        Task<Almacen> Editar(Conexion pobjConexion, Almacen pobjAlmacen);

        Task<bool> ValidarGuardar(Almacen pobjAlmacen);
    }
}
