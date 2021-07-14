
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Spine.Entities.Com
{
    public class NotaCreditoVenta
    {
		// COLUMNAS
		[XmlAttribute]
		public long nIdNotaCredito { set; get; }

		[XmlAttribute]
		public byte nIdEmpresa { set; get; }

		[XmlAttribute]
		public short nIdSucursal { set; get; }
        
        //[ValidaNumero(lEsMayorIgualCero = true)]
        //[XmlAttribute]
        //public short nIdAlmacen { set; get; }

		[XmlAttribute]
		public short nIdUsuario { set; get; }

        [XmlAttribute]
        public short nIdVendedor { get; set; }

		[XmlAttribute]
		public int nIdCliente { set; get; }

		[XmlAttribute]
		public short nIdFolio { set; get; }

		[XmlAttribute]
		public string sNCrSerie { set; get; }

		[XmlAttribute]
		public long nNCrCorrelativo { set; get; }

		[XmlAttribute]
		public DateTime dNCrEmision { set; get; }

		[XmlAttribute]
		public byte nIdMoneda { set; get; }

        [XmlAttribute]
        public decimal nNCrTipoCambio { get; set; }

        [XmlAttribute]
        public decimal nNCrIgv { get; set; }

        [XmlAttribute]
        public byte nNCrFormaPago { get; set; }

        [XmlAttribute]
        public byte nNCrMedioPago { get; set; }

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
		public decimal nNCrMontoIgvBase { get; set; }

		[XmlAttribute]
		public decimal nNCrMontoIgv { get; set; }

		[XmlAttribute]
		public decimal nNCrMontoIscBase { get; set; }

		[XmlAttribute]
		public decimal nNCrMontoIsc { get; set; }

		[XmlAttribute]
		public decimal nNCrMontoDesct { get; set; }

		[XmlAttribute]
		public decimal nNCrMontoDesctGlobal { get; set; }

		[XmlAttribute]
		public decimal nNCrMontoTotal { get; set; }

        [XmlAttribute]
        public decimal nNCrSaldo { get; set; }

		[XmlAttribute]
		public decimal nNCrPercepcion { get; set; }

		[XmlAttribute]
		public decimal nNCrRetencion { get; set; }

		[XmlAttribute]
		public decimal nNCrDetraccion { get; set; }

		[XmlAttribute]
		public long nIdComprobanteVenta { set; get; }

		[XmlAttribute]
		public byte nNCrTipoOpe { set; get; }

        [XmlAttribute]
        public string sNCrObservacion { set; get; }

		[XmlAttribute]
		public string sNCrMotivo { set; get; }

        [XmlAttribute]
        public bool lNCrImprimirSeries { get; set; }

        [XmlAttribute]
        public bool lNCrImprimirGarantia { get; set; }

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
        public string sUsuVendedor { set; get; }
        [XmlAttribute]
		public string sCliDocNumero { set; get; }
		[XmlAttribute]
		public string sCliNombre { set; get; }
		[XmlAttribute]
		public string sNCrSerieCorrelativo { set; get; }
		[XmlAttribute]
		public string sMonNombre { set; get; }
		[XmlAttribute]
		public string sNCrTipoOpe { set; get; }
		[XmlAttribute]
		public string sNCrEstado { set; get; }
		[XmlAttribute]
		public byte nCVeTipo { set; get; }
		[XmlAttribute]
		public string sCVeTipo { set; get; }
		[XmlAttribute]
		public byte nCVeFormaPago { set; get; }
		[XmlAttribute]
		public string sCVeFormaPago { set; get; }
        [XmlAttribute]
		public string sCVeSerie { set; get; }
		[XmlAttribute]
		public long nCVeCorrelativo { set; get; }
		[XmlAttribute]
		public string sCVeSerieCorrelativo { set; get; }
        [XmlAttribute]
        public string sNCrComprobanteRef { get; set; }
        [XmlAttribute]
		public DateTime dCVeEmision { set; get; }
        [XmlAttribute]
        public DateTime dCVeVencimiento { set; get; }
        [XmlAttribute]
        public int nCVePlazo { set; get; }

        //RELACIONES        
        public List<NotaCreditoVentaItem> lstNotaCreditoLinea { set; get; }
	}
}
