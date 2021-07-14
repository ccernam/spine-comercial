
using System;

namespace Spine.Entities.Cmn
{
    public class Precio
    {
        // COLUMNAS
        public int nIdPrecio { set; get; }
        public byte nIdEmpresa { set; get; }
        public short nIdUsuario { set; get; }
        public string sUsuUsuario { set; get; }

        public short nIdSucursal { set; get; }

        public int nIdProducto { set; get; }


        public byte nPreTipo { set; get; }


        public byte nIdMoneda { set; get; }


        public decimal nPrePrecio { set; get; }


        public decimal nPrePrecioMin { set; get; }


        public decimal nPreDescuento { set; get; }


        public decimal nInvDisponible { set; get; }


        public byte nPreEstado { set; get; }
        public short nProTipoOperacion { set; get; }


        public string sProNombre { set; get; }


        public byte nPreEditado { set; get; }

        // OTROS
        public string sSucNombre { set; get; }

        public string sMonNombre { set; get; }

        public string sPreTipo { set; get; }

        public string sPreEstado { set; get; }

        public string sProCodigoExterno { set; get; }

        public string sUMeNombre { set; get; }

        public string sPreNombre { set; get; }

        public DateTime dPreCreacion { set; get; }
    }
}