
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class NotaDebitoCompraLinea
    {
        [XmlAttribute]
        public long nIdNotaDebitoCompraLinea { get; set; }

        [XmlAttribute]
        public long nIdNotaDebitoCompra { get; set; }

        [XmlAttribute]
        public int nIdProducto { get; set; }

        [XmlAttribute]
        public int nNDLGarantiaProducto { get; set; }

        [XmlAttribute]
        public decimal nNDLCantidad { get; set; }

        [XmlAttribute]
        public string sNDLSeries { get; set; }

        [XmlAttribute]
        public decimal nNDLUnitarioValor { get; set; }

        [XmlAttribute]
        public decimal nNDLUnitarioIgv { get; set; }

        [XmlAttribute]
        public decimal nNDLUnitarioIsc { get; set; }

        [XmlAttribute]
        public decimal nNDLUnitarioPrecio { get; set; }

        [XmlAttribute]
        public decimal nNDLMontoValor { get; set; }

        [XmlAttribute]
        public decimal nNDLMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nNDLMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nNDLMontoSubtotal { get; set; }

        [XmlAttribute]
        public byte nNDLTipoOpe { get; set; }

        [XmlAttribute]
        public string sNDLDescripcion { get; set; }

        [XmlAttribute]
        public string sNDLDetalle { get; set; }

        //OTROS
        [XmlAttribute]
        public string sCVLSeries { get; set; }

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
        public string sUMeSimboloSUNAT { get; set; }

        [XmlAttribute]
        public int nNroFila { get; set; }

        [XmlAttribute]
        public bool lProPadre { get; set; }

        [XmlAttribute]
        public bool lProHijo { get; set; }

        //RELACIONES        
        public List<NotaDebitoCompraLinea> lstNotaDebitoCompraLinea { set; get; }
    }
}
