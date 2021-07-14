namespace Spine.Entities.Cmn
{
    public class Sucursal
    {
        public Sucursal() { }

        // Columnas
        public int iSucursalId { set; get; }
        public string sSucNombre { set; get; }
        public short iUbiId { set; get; }
        public string sSucDireccion { set; get; }
        public byte iSucEstado { set; get; }

        // Otros
        public string sUbiRama { set; get; }
        public string sSucEstado { set; get; }
    }
}