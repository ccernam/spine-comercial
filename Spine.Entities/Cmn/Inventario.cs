

namespace Spine.Entities.Cmn
{
    public class Inventario
    {
        public int nIdInventario { set; get; }

        public byte nIdEmpresa { set; get; }

        public short nIdAlmacen { set; get; }

        public string sAlmNombre { set; get; }

        public short nIdSucursal { set; get; }

        public string sSucNombre { set; get; }

        public int nIdProducto { set; get; }

        public byte nProTipo { get; set; }

        public byte nProArticuloTipo { get; set; }

        public string sProCodigo { set; get; }

        public string sProCodigoExterno { get; set; }

        public bool lProVenta { get; set; }

        public bool lProCompra { get; set; }

        public string sProNombre { set; get; }

        public short nProTipoOperacion { set; get; }

        public short nProGarantia { set; get; }

        public decimal nProUtilidad { get; set; }

        public bool lProSerieObligatoria { get; set; }

        public string sUMeNombre { get; set; }

        public decimal nInvInicial { set; get; }

        public decimal nInvDisponible { set; get; }

        public decimal nInvCantidad { set; get; }

        // OTROS
        public bool lProAplicaIgv { get; set; }
    }
}
