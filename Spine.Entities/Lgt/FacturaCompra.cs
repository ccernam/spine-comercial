using System;
using System.Collections.Generic;

using System.Xml.Serialization;

namespace Spine.Entities.Lgt
{
    public class FacturaCompra
    {
        [XmlAttribute]
        public int iFacComId { get; set; }

        [XmlAttribute]
        public byte nIdEmpresa { get; set; }

        [XmlAttribute]
        public short nIdSucursal { get; set; }

        [XmlAttribute]
        public short nIdAlmacen { get; set; }

        [XmlAttribute]
        public short nIdUsuario { get; set; }

        [XmlAttribute]
        public int nIdProveedor { get; set; }

        [XmlAttribute]
        public byte nCCoTipo { get; set; }

        [XmlAttribute]
        public string sCCoSerie { get; set; }

        [XmlAttribute]
        public long nCCoCorrelativo { get; set; }

        [XmlAttribute]
        public DateTime dCCoEmision { get; set; }

        [XmlAttribute]
        public DateTime dCCoVencimiento { get; set; }

        [XmlAttribute]
        public byte nCCoFormaPago { get; set; }

        [XmlAttribute]
        public short nCCoPlazo { get; set; }

        [XmlAttribute]
        public string sCCoObservacion { get; set; }

        [XmlAttribute]
        public byte nIdMoneda { get; set; }

        [XmlAttribute]
        public decimal nCCoTipoCambio { get; set; }

        [XmlAttribute]
        public decimal nCCoIgv { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoGravado { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoInafecto { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoExonerado { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoGratuito { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoValor { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoIgv { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoIsc { get; set; }

        [XmlAttribute]
        public decimal nCCoMontoTotal { get; set; }

        [XmlAttribute]
        public decimal nCCoSaldo { get; set; }

        [XmlAttribute]
        public byte iFacComEstado { get; set; }

        [XmlAttribute]
        public byte nCCoMedioPago { get; set; }

        [XmlAttribute]
        public bool lCCoIngresado { get; set; }
        [XmlAttribute]
        public bool lFacComGenerarOrdenMov { get; set; }

        // Otros
        [XmlAttribute]
        public string sCCoSerieCorrelativo { get; set; }
        [XmlAttribute]
        public string sSucNombre { get; set; }
        [XmlAttribute]
        public string sUsuUsuario { get; set; }
        [XmlAttribute]
        public string sMonNombre { get; set; }
        [XmlAttribute]
        public string sProNombre { get; set; }
        [XmlAttribute]
        public string sProDoc { get; set; }
        [XmlAttribute]
        public string sCCoTipo { get; set; }
        [XmlAttribute]
        public string sCCoPagoForma { get; set; }
        [XmlAttribute]
        public string sCCoEstadoPago { get; set; }
        [XmlAttribute]
        public string sCCoEstado { get; set; }
        [XmlAttribute]
        public bool lCCoPagosActivos { get; set; }
        [XmlAttribute]
        public string sCCoMedioPago { get; set; }
        [XmlAttribute]
        public short nCCoGenerarMov { get; set; }

        // Relaciones
        public List<FacturaItemCompra> lstComprobanteCompraLinea { get; set; }
    }
}
