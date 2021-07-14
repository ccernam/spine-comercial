
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class FacturaItemCompra
    {
        [XmlAttribute]
        public long nIdComprobanteCompraLinea { get; set; }

        [XmlAttribute]
        public long nIdComprobanteCompra { get; set; }

        [XmlAttribute]
        public byte nCCLTipoOpe { get; set; }

        [XmlAttribute]
        public int nIdProducto { get; set; }

        [XmlAttribute]
        public int nCCLGarantia { get; set; }

        [XmlAttribute]
        public string sCCLSeries { get; set; }

        [XmlAttribute]
        public decimal nCCLCantidad { get; set; }

        [XmlAttribute]
        public decimal nCCLIngresado { get; set; }

        [XmlAttribute]
        public decimal nCCLCantidadMaxima { get; set; }

        [XmlAttribute]
        public decimal nCCLPrecioCalculo { get; set; }

        [XmlAttribute]
        public decimal nCCLUnitarioValor { get; set; }

        [XmlAttribute]
        public decimal nCCLUnitarioIgv { get; set; }

        [XmlAttribute]
        public decimal nCCLUnitarioIsc { get; set; }

        [XmlAttribute]
        public decimal nCCLUnitarioPrecio { get; set; }

        [XmlAttribute]
        public decimal nCCLMontoValor { get; set; }

        [XmlAttribute]
        public decimal nCCLMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nCCLMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nCCLMontoSubtotal { get; set; }

        [XmlAttribute]
        public string sCCLDetalle { get; set; }

        [XmlAttribute]
        public byte lCCLActivo { get; set; }

        // Otros
        [XmlAttribute]
        public string sProNombre { get; set; }
        [XmlAttribute]
        public string sProNombreComercial { get; set; }
        [XmlAttribute]
        public string sProCodigo { get; set; }
        [XmlAttribute]
        public string sProCodigoExterno { get; set; }
        [XmlAttribute]
        public bool lProSerieObligatoria { get; set; }
        [XmlAttribute]
        public string sUMeNombre { get; set; }
        [XmlAttribute]
        public string sCCLDescripcion { get; set; }
        [XmlAttribute]
        public string sCCLTipoOperacion { get; set; }
        [XmlAttribute]
        public string sCCLSeriesDisponibles { get; set; }
        [XmlAttribute]
        public string sCCLSeriesIngresadas { get; set; }
        [XmlAttribute]
        public decimal nCCLCantidadPendiente { get; set; }
        [XmlAttribute]
        public string sCCLSeriesPendientes { get; set; }
    }
}
