
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Com
{
    public class NotaCreditoVentaItem
    {
        //COLUMNAS
        [XmlAttribute]
        public long nIdNotaCreditoLinea { get; set; }

        [XmlAttribute]
        public long nIdNotaCreditoLineaPadre { set; get; }

        [XmlAttribute]
        public long nIdNotaCredito { get; set; }

        [XmlAttribute]
        public long nIdComprobanteVentaLinea { get; set; }

        [XmlAttribute]
        public byte nNCLTipoOpe { get; set; }

        [XmlAttribute]
        public int nIdProducto { get; set; }

        [XmlAttribute]
        public int nNCLGarantiaProducto { get; set; }

        [XmlAttribute]
        public string sNCLSeries { get; set; }

        [XmlAttribute]
        public decimal nNCLCantidad { get; set; }

        [XmlAttribute]
        public decimal nNCLPrecioCalculo { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioValor { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioDesct { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioIgvBase { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioIgv { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioIscBase { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioIsc { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioPrecio { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoValor { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoDesct { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoIgvBase { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoIscBase { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoSubtotal { get; set; }

        [XmlAttribute]
        public string sNCLDescripcion { get; set; }

        [XmlAttribute]
        public string sNCLDetalle { get; set; }

        //OTROS
        [XmlAttribute]
        public string sProNombre { get; set; }
        [XmlAttribute]
        public string sProNombreComercial { get; set; }
        [XmlAttribute]
        public string sProCodigo { get; set; }
        [XmlAttribute]
        public string sProCodigoExterno { get; set; }
        [XmlAttribute]
        public string sProDenominacion { get; set; }
        [XmlAttribute]
        public string sUMeNombre { get; set; }
        [XmlAttribute]
        public string sUMeSimbolo { get; set; }
        [XmlAttribute]
        public string sUMeSimboloSUNAT { get; set; }
        [XmlAttribute]
        public decimal nNCLCantTotal { set; get; }
        [XmlAttribute]
        public string sNCLTipoOpe { get; set; }
        [XmlAttribute]
        public decimal nNCLEntregado { get; set; }

        //RELACIONES        
        public List<NotaCreditoVentaItem> lstNotaCreditoLinea { set; get; }
    }
}
