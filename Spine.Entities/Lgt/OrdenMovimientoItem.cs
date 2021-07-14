
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class OrdenMovimientoItem
    {
        //COLUMNAS
        [XmlAttribute]
        public long nIdAlmacenMovLinea { get; set; }

        [XmlAttribute]
        public long nIdAlmacenMovLineaPadre { set; get; }

        [XmlAttribute]
        public long nIdAlmacenMov { get; set; }

        [XmlAttribute]
        public int nIdProducto { get; set; }

        [XmlAttribute]
        public decimal nAMLCantidad { get; set; }

        [XmlAttribute]
        public int nAMLGarantia { get; set; }

        [XmlAttribute]
        public string sAMLSeries { get; set; }

        [XmlAttribute]
        public string sAMLDetalle { get; set; }

        [XmlAttribute]
        public long nIdComprobanteVentaLineaRef { set; get; }

        [XmlAttribute]
        public long nIdComprobanteCompraLineaRef { set; get; }

        //OTROS
        [XmlAttribute]
        public decimal nAMLCantidadMaxima { get; set; }

        [XmlAttribute]
        public string sAMLSeriesPorFacturar { get; set; }

        [XmlAttribute]
        public decimal nAMLFacturado { get; set; }

        [XmlAttribute]
        public string sProNombre { get; set; }

        [XmlAttribute]
        public string sProNombreComercial { get; set; }

        [XmlAttribute]
        public string sProCodigo { get; set; }

        [XmlAttribute]
        public string sProCodigoExterno { get; set; }

        [XmlAttribute]
        public short nProArticuloTipo { set; get; }

        [XmlAttribute]
        public bool lProSerieObligatoria { set; get; }

        [XmlAttribute]
        public string sUMeNombre { get; set; }

        [XmlAttribute]
        public byte nIdUnidadMedida { get; set; }

        [XmlAttribute]
        public short nProGarantia { get; set; }

        [XmlAttribute]
        public decimal nProUtilidad { get; set; }

        [XmlAttribute]
        public decimal nAMLCantidadParaGuia { get; set; }

        [XmlAttribute]
        public string sAMLSeriesParaGuia { get; set; }

        //RELACIONES
        public List<OrdenMovimientoItem> lstAlmacenMovLinea { get; set; }
    }
}
