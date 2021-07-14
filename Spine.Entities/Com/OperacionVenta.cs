
using System;

namespace Spine.Entities.Com
{
    public class OperacionVenta
    {
        public long nIdPagoVenta { get; set; }


        public long nIdComprobanteVenta { get; set; }


        public byte nIdEmpresa { get; set; }


        public DateTime dPVeOperacion { get; set; }


        public byte nPVeMedioPago { get; set; }

        public string sPVeMedioPago { get; set; }


        public DateTime dPVeVencimiento { get; set; }


        public decimal nPVeMontoRef { get; set; }


        public byte nIdMonedaRef { get; set; }

        public string sMonNombreRef { get; set; }


        public decimal nPVeTipoCambioRef { get; set; }


        public decimal nPVeMonto { get; set; }


        public byte nIdMoneda { get; set; }

        public string sMonNombre { get; set; }


        public decimal nPVeTipoCambio { get; set; }


        public string sPVeObservacion { get; set; }


        public byte nPVeEstado { get; set; }

        public string sPVeEstado { get; set; }
    }
}
