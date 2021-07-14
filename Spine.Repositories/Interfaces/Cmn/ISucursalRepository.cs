using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spine.Repositories.Interfaces.Cmn
{
    public interface ISucursalRepository
    {
        Task<IEnumerable<Sucursal>> Consultar(int piSucId = -1, string psSucNombre = "", short piSucEstado = -1);

        Task<Sucursal> Crear(Conexion pobjConexion, Sucursal pobjSucursal);

        Task<Sucursal> Editar(Conexion pobjConexion, Sucursal pobjSucursal);

        Task<bool> ValidarGuardar(Sucursal pobjSucursal);
    }
}
