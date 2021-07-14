using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Com
{
    public class FacturaVenta
    {
        public int iFVeId { get; set; }
        public short iSucId { get; set; }
        public int iPerId { get; set; }

        public byte iFVeTipo { get; set; }

        public decimal nCVeIgv { get; set; }

        public string sFVeSerie { get; set; }
        public int sFVeNumero { get; set; }

        public DateTime dFVeEmision { get; set; }

        public byte iMonId { get; set; }
        public decimal nFVeTipoCambio { get; set; }



        [XmlAttribute]
        public long nIdComprobanteVentaRef { get; set; }

        [XmlAttribute]
        public long nIdOrdenServicio { get; set; }

        [XmlAttribute]
        public short nIdFolio { get; set; }

        [XmlAttribute]
        public string sCVeSerie { get; set; }

        [XmlAttribute]
        public long nCVeCorrelativo { get; set; }

        [XmlAttribute]
        public DateTime dCVeEmision { get; set; }

        [XmlAttribute]
        public byte nIdMoneda { get; set; }
        [XmlAttribute]
        public int nCVePlazo { get; set; }

        [XmlAttribute]
        public decimal nCVeTipoCambio { get; set; }
        [XmlAttribute]
        public decimal nCVeMontoValor { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoGravado { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoInafecto { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoExonerado { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoGratuito { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoIgvBase { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoIscBase { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoDesct { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoDesctGlobal { get; set; }

        [XmlAttribute]
        public decimal nCVeMontoTotal { get; set; }

        [XmlAttribute]
        public decimal nCVeSaldo { get; set; }

        [XmlAttribute]
        public decimal nCVePercepcion { get; set; }

        [XmlAttribute]
        public decimal nCVeRetencion { get; set; }

        [XmlAttribute]
        public decimal nCVeDetraccion { get; set; }

        [XmlAttribute]
        public byte nCVeFormaPago { get; set; }

        [XmlAttribute]
        public DateTime dCVeVencimiento { get; set; }

        [XmlAttribute]
        public byte nCVeTipoOpe { get; set; }

        [XmlAttribute]
        public string sCVeMotivo { get; set; }

        [XmlAttribute]
        public string sCVeObservacion { get; set; }

        [XmlAttribute]
        public bool lCVeImprimirSeries { get; set; }

        [XmlAttribute]
        public bool lCVeImprimirGarantia { get; set; }

        [XmlAttribute]
        public byte nCVeEstado { get; set; }

        [XmlAttribute]
        public byte nCVeEstadoSunat { get; set; }

        [XmlAttribute]
        public bool lCVeGenerarMov { get; set; }

        [XmlAttribute]
        public bool lCVeEntregado { set; get; }

        [XmlAttribute]
        public byte nCVeMedioPago { get; set; }

        [XmlAttribute]
        public long nIdAlmacenMov { get; set; }

        [XmlAttribute]
        public bool lCVeDesdeAlmacenMov { get; set; }

        //OTROS
        [XmlAttribute]
        public string sSucNombre { get; set; }
        [XmlAttribute]
        public string sUsuUsuario { get; set; }
        [XmlAttribute]
        public string sUsuVendedor { get; set; }
        [XmlAttribute]
        public string sCVeAlmacen { get; set; }
        [XmlAttribute]
        public string sCVeTipo { get; set; }
        [XmlAttribute]
        public string sCVeSerieCorrelativo { get; set; }
        [XmlAttribute]
        public string sOSeSerieCorrelativo { get; set; }
        [XmlAttribute]
        public string sCVePagoForma { get; set; }
        [XmlAttribute]
        public string sCVePagoMedio { get; set; }
        [XmlAttribute]
        public string sCliNombre { get; set; }
        [XmlAttribute]
        public byte nCliDocTipo { set; get; }
        [XmlAttribute]
        public string sCliDocTipo { get; set; }
        [XmlAttribute]
        public string sCliDocNumero { get; set; }
        [XmlAttribute]
        public string sCVeEntregado { get; set; }
        [XmlAttribute]
        public string sCVeEstadoPago { get; set; }
        [XmlAttribute]
        public string sCVeEstado { get; set; }
        [XmlAttribute]
        public string sCVeEstadoSunat { get; set; }
        [XmlAttribute]
        public string sMonNombre { get; set; }
        [XmlAttribute]
        public byte nFolTipo { get; set; }
        [XmlAttribute]
        public string sPedCodigo { get; set; }
        [XmlAttribute]
        public string sCVeMoneda { get; set; }
        [XmlAttribute]
        public string sMonSimbolo { get; set; }
        [XmlAttribute]
        public decimal nCVeMontoPagos { get; set; }
        public string psParCodigo { get; set; }
        public short pnParEstado { get; set; }
        [XmlAttribute]
        public string sCliCorreo { get; set; }
        [XmlAttribute]
        public bool lCVeAnulacionNCr { set; get; }

        public List<FacturaVentaItem> lstItems { get; set; }
    }
}
