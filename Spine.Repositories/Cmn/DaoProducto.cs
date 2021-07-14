using Spine.Entities.Cmn;
using System.Collections.Generic;
using Spine.Librerias.Database;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;


namespace Spine.Repositories.Cmn
{
    public class DaoProducto : RepositoryBase
    {
        public DaoProducto() : base()
        {
        }


        public List<Producto> Consultar(int pnProId = -1, short pnCtgId = -1, short pnMarId = -1, short pnUniMedId = -1,
                                        short pnProTipo = -1, string psProCodigo = "", string psProNombre = "",
                                       string psProDesc = "", short pnProEstado = -1)
        {

            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<Producto>("Cmn.pa_Producto_Consultar",
                new SqlParameter("@pnProId", pnProId),
                new SqlParameter("@pnCtgId", pnCtgId),
                new SqlParameter("@pnMarId", pnMarId),
                new SqlParameter("@pnUniMedId", pnUniMedId),
                new SqlParameter("@pnProTipo", pnProTipo),
                new SqlParameter("@psProCodigo", psProCodigo),
                new SqlParameter("@psProNombre", psProNombre),
                new SqlParameter("@psProDesc", psProDesc),
                new SqlParameter("@pnProEstado", pnProEstado)
                );
            }

        }

        public int Crear(Conexion pobjConexion, Producto pobjProducto)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter("@pnProId", pobjProducto.nProId){ Direction = ParameterDirection.Output },
                new SqlParameter("@pnCtgId", pobjProducto.nCtgId),
                new SqlParameter("@pnMarId", pobjProducto.nMarId),
                new SqlParameter("@pnUniMedId", pobjProducto.nUniMedId),
                new SqlParameter("@pnProTipo", pobjProducto.nProTipo),
                new SqlParameter("@psProCodigo", pobjProducto.sProCodigo),
                new SqlParameter("@psProNombre", pobjProducto.sProNombre),
                new SqlParameter("@psProDesc", pobjProducto.sProDesc),
                new SqlParameter("@pnProEstado", pobjProducto.nProEstado)
            };

            pobjConexion.Ejecutar("Cmn.pa_Producto_Crear", varrParametros);
            pobjProducto.nProId = Convert.ToInt16(varrParametros.First(x => x.ParameterName == "@pnProId").Value);

            return pobjProducto.nProId;
        }

        public int Editar(Conexion pobjConexion, Producto pobjProducto)
        {
            pobjConexion.Ejecutar("Cmn.pa_Producto_Editar",
                new SqlParameter("@pnProId", pobjProducto.nProId),
                new SqlParameter("@pnCtgId", pobjProducto.nCtgId),
                    new SqlParameter("@pnMarId", pobjProducto.nMarId),
                    new SqlParameter("@pnUniMedId", pobjProducto.nUniMedId),
                    new SqlParameter("@pnProTipo", pobjProducto.nProTipo),
                    new SqlParameter("@psProCodigo", pobjProducto.sProCodigo),
                    new SqlParameter("@psProNombre", pobjProducto.sProNombre),
                    new SqlParameter("@psProDesc", pobjProducto.sProDesc),
                    new SqlParameter("@pnProEstado", pobjProducto.nProEstado)
                );

            return pobjProducto.nProId;
        }

        public bool CambiarEstado(Conexion pobjConexion, int pnProId, byte pnProEstado)
        {
            return pobjConexion.Ejecutar("Cmn.pa_Producto_CambiarEstado",
                new SqlParameter("@pnProId", pnProId),
                new SqlParameter("@pnProEstado", pnProEstado)
                ) == 1;
        }
    }
}

