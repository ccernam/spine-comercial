
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Com
{
    public class NotaDebitoVenta
    {
        [XmlAttribute]
        public long nIdNotaDebito { set; get; }

        [XmlAttribute]
        public byte nNDeDocRefTipo { set; get; }

        [XmlAttribute]
        public string sNDeSerie { set; get; }

        [XmlAttribute]
        public long nNDeCorrelativo { set; get; }

        [XmlAttribute]
        public DateTime dNDeEmision { set; get; }

        [XmlAttribute]
        public decimal nNDeTipoCambio { get; set; }

        [XmlAttribute]
        public decimal nNDeIgv { get; set; }

        [XmlAttribute]
        public byte nNDeTipoOpe { set; get; }

        [XmlAttribute]
        public string sNDeTipoOpe { set; get; }

        [XmlAttribute]
        public string sNDeMotivo { set; get; }

        [XmlAttribute]
        public byte nNDeEstado { set; get; }

        [XmlAttribute]
        public string sNDeEstado { set; get; }

        [XmlAttribute]
        public byte nNDeEstadoPago { set; get; }

        [XmlAttribute]
        public string sNDeEstadoPago { set; get; }

        [XmlAttribute]
        public decimal nNDeMontoValor { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoGravado { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoInafecto { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoExonerado { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoGratuito { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoIscBase { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoDesct { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoDesctGlobal { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoTotal { get; set; }

        [XmlAttribute]
        public decimal nNDeSaldo { set; get; }

        [XmlAttribute]
        public decimal nNDePercepcion { get; set; }

        [XmlAttribute]
        public decimal nNDeRetencion { get; set; }

        [XmlAttribute]
        public decimal nNDeDetraccion { get; set; }

        [XmlAttribute]
        public byte nNDeFormaPago { get; set; }

        [XmlAttribute]
        public byte nNDeMedioPago { get; set; }

        [XmlAttribute]
        public int nNDePlazo { get; set; }

        [XmlAttribute]
        public DateTime dNDeVencimiento { get; set; }

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

        // Fk: Cliente
        [XmlAttribute]
        public short nIdCliente { get; set; }

        [XmlAttribute]
        public string sPerDoc { set; get; }

        [XmlAttribute]
        public string sCliNombre { set; get; }

        [XmlAttribute]
        public string sCliDocNumero { set; get; }

        [XmlAttribute]
        public string sPerNombre { set; get; }

        // Fk: ComprobanteVenta
        [XmlAttribute]
        public long nIdComprobanteVenta { set; get; }

        [XmlAttribute]
        public string sCVeSerie { set; get; }

        [XmlAttribute]
        public byte nCVeTipo { set; get; }

        [XmlAttribute]
        public string sCVeTipo { set; get; }

        [XmlAttribute]
        public long nCVeCorrelativo { set; get; }

        [XmlAttribute]
        public DateTime dCVeEmision { set; get; }

        [XmlAttribute]
        public bool lNDeImprimirSeries { get; set; }

        [XmlAttribute]
        public bool lNDeImprimirGarantia { get; set; }

        [XmlAttribute]
        public decimal nNDeMontoIgvBase { get; set; }

        [XmlAttribute]
        public short nIdVendedor { get; set; }

        //Otros
        [XmlAttribute]
        public byte nFolTipo { set; get; }
        [XmlAttribute]
        public byte nCVeFormaPago { set; get; }
        [XmlAttribute]
        public string sCVeFormaPago { set; get; }
        [XmlAttribute]
        public decimal nCVeMontoTotal { get; set; }
        [XmlAttribute]
        public string sUsuVendedor { set; get; }
        [XmlAttribute]
        public string sNDeFormaPago { get; set; }

        public List<NotaDebitoVentaItem> lstNotaDebitoLinea { get; set; }
    }
}
