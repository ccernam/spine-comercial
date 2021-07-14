using Spine.Repositories.Lgt;
using Spine.Entities.Lgt;

using Spine.Librerias.Database;
using System.Collections.Generic;

namespace Spine.Services.Lgt
{
    public class NegFacturaCompra : ServiceBase
    {
        public NegFacturaCompra() : base()
        {
        }

        public List<FacturaCompra> Consultar()
        {




            return null;
        }

        public int Crear(FacturaCompra pobjFactura)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                var vdaoFacturaCompra = new DaoFacturaCompra();

                pobjFactura.iFacComId = vdaoFacturaCompra.Crear(vobjConexion, pobjFactura);

                if (pobjFactura.iFacComEstado == 2 && pobjFactura.lFacComGenerarOrdenMov) // Emitido
                {
                    // Generar movimiento de entrada
                
                }



                vobjConexion.Commit();
            }








            return 0;
        }

        public int Editar(FacturaCompra pobjFactura)
        {
            return 0;
        }
    }
}
