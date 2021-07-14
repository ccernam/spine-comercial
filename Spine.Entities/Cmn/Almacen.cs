using System;

namespace Spine.Entities.Cmn
{
    public class Almacen
    {
        public Almacen() { }

        // Columnas
        public int iAlmacenId { set; get; }
        public string sAlmNombre { set; get; }
        public int iSucursalId { set; get; }
        public int iUbigeoId { set; get; }
        public string sAlmDireccion { set; get; }
        public string sAlmCodigoSunat { set; get; }
        public DateTime dAlmFechaCierre { set; get; }
        public byte iAlmEstado { set; get; }

        // Otros
        public string sSucNombre { get; set; }
        public string sUbiRama { get; set; }
        public string sAlmEstado { get; set; }
    }
}
