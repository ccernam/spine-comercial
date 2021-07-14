
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class NotaCredito
    {
        // COLUMNAS
        [XmlAttribute]
        public long nIdNotaCredito { set; get; }

        [XmlAttribute]
        public byte nIdEmpresa { set; get; }

        [XmlAttribute]
        public short nIdSucursal { set; get; }

        [XmlAttribute]
        public short nIdAlmacen { set; get; }

        [XmlAttribute]
        public short nIdUsuario { set; get; }

        [XmlAttribute]
        public short nIdProveedor { get; set; }

        [XmlAttribute]
        public short nIdFolio { set; get; }

        [XmlAttribute]
        public byte nNCrTipo { set; get; }

        [XmlAttribute]
        public byte nIdMoneda { set; get; }

        [XmlAttribute]
        public decimal nNCrTipoCambio { get; set; }

        [XmlAttribute]
        public byte nNCrFormaPago { get; set; }

        [XmlAttribute]
        public byte nNCrMedioPago { get; set; }

        [XmlAttribute]
        public decimal nNCrIgv { get; set; }

        [XmlAttribute]
        public long nIdComprobanteCompra { set; get; }

        [XmlAttribute]
        public string sNCrSerie { set; get; }

        [XmlAttribute]
        public long nNCrCorrelativo { set; get; }

        [XmlAttribute]
        public DateTime dNCrEmision { set; get; }

        [XmlAttribute]
        public decimal nNCrMontoValor { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoGravado { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoInafecto { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoExonerado { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoGratuito { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nNCrMontoTotal { get; set; }

        [XmlAttribute]
        public decimal nNCrSaldo { get; set; }

        [XmlAttribute]
        public string sNCrObservacion { set; get; }

        [XmlAttribute]
        public byte nNCrTipoOpe { set; get; }

        [XmlAttribute]
        public string sNCrMotivo { set; get; }

        [XmlAttribute]
        public bool lNCrGenerarMov { get; set; }

        [XmlAttribute]
        public byte nNCrEstado { set; get; }

        // OTROS
        [XmlAttribute]
        public string sSucNombre { set; get; }
        [XmlAttribute]
        public string sUsuUsuario { set; get; }
        [XmlAttribute]
        public string sPerDocNumero { set; get; }
        [XmlAttribute]
        public string sPerNombre { set; get; }
        [XmlAttribute]
        public string sNCrSerieCorrelativo { set; get; }
        [XmlAttribute]
        public string sMonNombre { set; get; }
        [XmlAttribute]
        public string sNCrTipoOpe { set; get; }
        [XmlAttribute]
        public string sNCrEstado { set; get; }
        [XmlAttribute]
        public byte nCCoTipo { set; get; }
        [XmlAttribute]
        public string sCCoTipo { set; get; }
        [XmlAttribute]
        public byte nCCoFormaPago { set; get; }
        [XmlAttribute]
        public string sCCoFormaPago { set; get; }
        [XmlAttribute]
        public string sCCoSerie { set; get; }
        [XmlAttribute]
        public long nCCoCorrelativo { set; get; }
        [XmlAttribute]
        public string sCCoSerieCorrelativo { set; get; }
        [XmlAttribute]
        public string sNCrComprobanteRef { get; set; }
        [XmlAttribute]
        public DateTime dCCoEmision { set; get; }
        [XmlAttribute]
        public DateTime dCCoVencimiento { set; get; }
        [XmlAttribute]
        public int nCCoPlazo { set; get; }

        //RELACIONES        
        public List<NotaCreditoLinea> lstNotaCreditoLinea { set; get; }
    }
}