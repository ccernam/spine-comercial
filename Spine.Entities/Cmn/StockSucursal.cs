

namespace Spine.Entities.Cmn
{
    public class StockSucursal
    {
        public int nIdStockSucursal { set; get; }
        public byte nIdEmpresa { set; get; }
        public short nIdSucursal { set; get; }
        public int nIdProducto { set; get; }
        public short nSSuCantidad { set; get; }

        //OTROS
        public string sSucNombre { set; get; }
        public string sProNombre { set; get; }
    }
}
