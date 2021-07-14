

namespace Spine.Entities.Cmn
{
    public class Folio
    {
        public short nIdFolio { set; get; }
        public byte nIdEmpresa { set; get; }
        public short nIdSucursal { set; get; }
        public short nIdAlmacen { set; get; }
        public byte nFolTipo { set; get; }
        public byte nFolDocTipo { set; get; }
        public byte nFolDocRefTipo { set; get; }
        public string sFolSerie { set; get; }
        public long nFolCorrelativo { set; get; }
        public byte nFolEstado { set; get; }

        //OTROS
        public string sSucNombre { set; get; }

        public string sAlmNombre { set; get; }

        public string sFolTipo { set; get; }

        public string sFolDocTipo { set; get; }

        public string sFolDocRefTipo { set; get; }

        public string sFolCorrelativo { set; get; }

        public string sFolEstado { set; get; }
    }
}
