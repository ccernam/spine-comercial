
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class NotaDebitoCompra
    {
        [XmlAttribute]
        public long nIdNotaDebitoCompra { set; get; }
        [XmlAttribute]
        public string sNDCSerie { set; get; }
        [XmlAttribute]
        public long nNDCCorrelativo { set; get; }
        [XmlAttribute]
        public DateTime dNDCEmision { set; get; }
        [XmlAttribute]
        public decimal nNDCTipoCambio { get; set; }
        [XmlAttribute]
        public byte nNDCTipoOpe { set; get; }
        [XmlAttribute]
        public string sNDCTipoOpe { set; get; }
        [XmlAttribute]
        public string sNDCMotivo { set; get; }
        [XmlAttribute]
        public byte nNDCEstado { set; get; }
        [XmlAttribute]
        public string sNDCEstado { set; get; }
        [XmlAttribute]
        public byte nNDCEstadoPago { set; get; }
        [XmlAttribute]
        public string sNDCEstadoPago { set; get; }
        [XmlAttribute]
        public decimal nNDCMontoValor { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoGravado { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoInafecto { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoExonerado { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoGratuito { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoIgv { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoIsc { get; set; }
        [XmlAttribute]
        public decimal nNDCMontoTotal { get; set; }
        [XmlAttribute]
        public decimal nNDCSaldo { set; get; }
        [XmlAttribute]
        public byte nNDCFormaPago { get; set; }
        [XmlAttribute]
        public byte nNDCMedioPago { get; set; }
        [XmlAttribute]
        public int nNDCPlazo { get; set; }
        [XmlAttribute]
        public DateTime dNDCVencimiento { get; set; }

        // Fk: Empresa
        [XmlAttribute]
        public byte nIdEmpresa { set; get; }

        // Fk: Sucursal
        [XmlAttribute]
        public short nIdSucursal { set; get; }
        [XmlAttribute]
        public string sSucNombre { set; get; }

        // Fk: Usuario
        [XmlAttribute]
        public short nIdUsuario { set; get; }
        [XmlAttribute]
        public string sUsuUsuario { set; get; }

        // Fk: Moneda
        [XmlAttribute]
        public byte nIdMoneda { set; get; }
        [XmlAttribute]
        public string sMonNombre { set; get; }

        // Fk: Proveedor
        [XmlAttribute]
        public short nIdProveedor { get; set; }
        [XmlAttribute]
        public string sPerDoc { set; get; }
        [XmlAttribute]
        public string sPerNombre { set; get; }

        // Fk: ComprobanteCompra
        [XmlAttribute]
        public long nIdComprobanteCompra { set; get; }
        [XmlAttribute]
        public string sCCoSerie { set; get; }
        [XmlAttribute]
        public long nCCoCorrelativo { set; get; }
        [XmlAttribute]
        public DateTime dCCoEmision { set; get; }
        [XmlAttribute]
        public string sCCoTipo { set; get; }
        [XmlAttribute]
        public string sCCoFormaPago { set; get; }
        [XmlAttribute]
        public decimal nCCoMontoTotal { get; set; }

        // OTROS
        //[XmlAttribute]
        //public string sProDoc { set; get; }
        [XmlAttribute]
        public byte nNDCDocRefTipo { set; get; }
        [XmlAttribute]
        public string sCCoSerieCorrelativo { set; get; }
        public List<NotaDebitoCompraLinea> lstNotaDebitoCompraLinea { set; get; }
    }
}
