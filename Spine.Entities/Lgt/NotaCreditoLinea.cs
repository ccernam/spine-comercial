
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class NotaCreditoLinea
    {
        //COLUMNAS
        [XmlAttribute]
        public long nIdNotaCreditoLinea { get; set; }

        [XmlAttribute]
        public long nIdNotaCredito { get; set; }

        [XmlAttribute]
        public long nIdComprobanteCompraLinea { get; set; }

        [XmlAttribute]
        public byte nNCLTipoOpe { get; set; }

        [XmlAttribute]
        public int nIdProducto { get; set; }

        [XmlAttribute]
        public int nNCLGarantia { get; set; }

        [XmlAttribute]
        public string sNCLSeries { get; set; }

        [XmlAttribute]
        public decimal nNCLCantidad { get; set; }

        [XmlAttribute]
        public decimal nNCLCantidadPendiente { get; set; }

        [XmlAttribute]
        public decimal nNCLPrecioCalculo { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioValor { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioIgv { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioIsc { get; set; }

        [XmlAttribute]
        public decimal nNCLUnitarioPrecio { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoValor { get; set; }

        [XmlAttribute]
        public decimal nNCLMontoIgv { get; set; }

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
        public string sNCLTipoOpe { get; set; }
        [XmlAttribute]
        public decimal nNCLIngresado { get; set; }
    }
}