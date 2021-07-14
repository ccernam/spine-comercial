using System;


namespace Spine.Entities.Cmn
{
	public class Kardex
	{
		public DateTime dKarEmision { set; get; }

		public string sKarMovTipo { set; get; }

		public string sKarObservacion { set; get; }

		public string sKarAMoCodigo { set; get; }

		public string sKarDocTipo { set; get; }

		public string sKarSerieCorrelativo { set; get; }

		public decimal nKarCantidad { set; get; }

		public decimal nSaldo { set; get; }

        public decimal nSaldoAnterior { set; get; }

        public string sKarPersona { set; get; }

        public int sKarnIdDocumento { set; get; }
        public byte sKarnDocumentoTipo { set; get; }
        public byte sKarnTipo { set; get; }
        public decimal nKarValPreUnitario { set; get; }
        public string sMonNombre { set; get; }
        public byte nAMoTipo { set; get; }
    }
}
