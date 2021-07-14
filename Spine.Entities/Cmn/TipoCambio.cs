using System;

namespace Spine.Entities.Cmn
{
    public class TipoCambio
    {
        TipoCambio() { }

        // Columnas
        public int iTipCamId { set; get; }
        public DateTime dTipCamFecha { set; get; }
        public decimal nTipCamCompra { set; get; }
        public decimal nTipCamVenta { set; get; }
        public byte iTipCamEstado { set; get; }

        // Otros
        public string sTipCamEstado { set; get; }
    }
}
