
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class GuiaRemisionLinea
    {
        // COLUMNAS
        [XmlAttribute]
        public long nIdGuiaRemisionLinea { get; set; }

        [XmlAttribute]
        public long nIdGuiaRemisionLineaPadre { get; set; }

        [XmlAttribute]
        public long nIdGuiaRemision { get; set; }

        [XmlAttribute]
        public long nIdAlmacenMovLinea { get; set; }

        [XmlAttribute]
        public int nIdProducto { get; set; }

        [XmlAttribute]
        public int nGRLGarantia { get; set; }

        [XmlAttribute]
        public string sGRLSeries { get; set; }

        [XmlAttribute]
        public decimal nGRLCantidad { get; set; }

        [XmlAttribute]
        public string sGRLDetalle { get; set; }

        // OTROS
        [XmlAttribute]
        public string sProNombre { get; set; }
        [XmlAttribute]
        public string sProCodigo { get; set; }
        [XmlAttribute]
        public string sProCodigoExterno { get; set; }
        [XmlAttribute]
        public int nProArticuloTipo { get; set; }
        [XmlAttribute]
        public string sUMeNombre { get; set; }

        //RELACIONES
        public List<GuiaRemisionLinea> lstGuiaRemisionLinea { get; set; }
    }
}