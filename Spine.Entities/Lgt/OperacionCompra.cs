
using System;

namespace Spine.Entities.Lgt
{
    public class OperacionCompra
    {
        public long nIdPagoCompra { get; set; }


        public long nIdComprobanteCompra { get; set; }


        public byte nIdEmpresa { get; set; }


        public DateTime dPCoOperacion { get; set; }


        public byte nPCoMedioPago { get; set; }

        public string sPCoMedioPago { get; set; }


        public DateTime dPCoVencimiento { get; set; }


        public decimal nPCoMontoRef { get; set; }


        public byte nIdMonedaRef { get; set; }

        public string sMonNombreRef { get; set; }


        public decimal nPCoTipoCambioRef { get; set; }


        public decimal nPCoMonto { get; set; }


        public byte nIdMoneda { get; set; }

        public string sMonNombre { get; set; }


        public decimal nPCoTipoCambio { get; set; }


        public string sPCoObservacion { get; set; }


        public byte nPCoEstado { get; set; }

        public string sPCoEstado { get; set; }
    }
}
