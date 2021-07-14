

namespace Spine.Entities.Cmn
{
    public class PrecioInventario
    {
        
        public int nIdInventario { get; set; }
        
        public short nIdAlmacen { get; set; }
        
        public string sAlmNombre { get; set; }
        
        public short nIdSucursal { get; set; }
        
        public string sSucNombre { get; set; }
        
        public int nIdProducto { get; set; }
        
        public short nProTipo { get; set; }
        
        public short nProArticuloTipo { get; set; }
        
        public string sProCodigo { get; set; }
        
        public string sProCodigoExterno { get; set; }
        
        public byte lProVenta { get; set; }
        
        public byte lProCompra { get; set; }
        
        public string sProNombre { get; set; }
        
        public int nProTipoOperacion { get; set; }
        
        public int nProGarantia { get; set; }
        
        public decimal nProUtilidad { get; set; }
        
        public byte lProSerieObligatoria { get; set; }
        
        public string sUMeNombre { get; set; }
        
        public int nInvInicial { get; set; }
        
        public int nInvDisponible { get; set; }
        
        public int nInvCantidad { get; set; }
        
        public decimal nPrePrecio { get; set; }
        
        public decimal nPrePrecioMin { get; set; }
        
        public decimal nPreDescuento { get; set; }
        
        public decimal nIdMoneda { get; set; }
    }
}
