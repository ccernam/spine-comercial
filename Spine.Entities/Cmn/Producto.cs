using System;

namespace Spine.Entities.Cmn
{
    public class Producto
    {
        public Producto() { }

        public int nProId { set; get; }
        public short nCtgId { set; get; }
        public short nMarId { set; get; }
        public short nUniMedId { set; get; } 
        public short nProTipo { set; get; } 
        public short nProTipoOpe { set; get; }
        public string sProCodigo { set; get; }
        public string sProCodExt { set; get; }
        public string sProNombre { set; get; }
        public string sProDesc { set; get; } 
        public short nProProcedencia { set; get; } 
        public short nProTipoConversion { set; get; } 
        public decimal nProFactorConversion { set; get; }
        public byte nProEstado { set; get; }

        // ***** Descripciones
        public string sProTipo { set; get; }
        public string sProProcedencia { set; get; }
        public string sProEstado { set; get; }
        public string sProTipoConversion { set; get; }
        public string sProCategoria { set; get; }
        public string sProMarca { set; get; }
        public string sProUnidadMedida { set; get; }



        //public byte nIdEmpresa { set; get; }
        //public int nIdInventario { set; get; }
        //public short nProGarantia { set; get; }
        //public decimal nProUtilidad { get; set; }
        //public bool lProVenta { set; get; }
        //public bool lProCompra { set; get; }
        //public bool lProSerieObligatoria { set; get; }
        //public bool lProUtilidad { set; get; }

        ////OTROS
        //public string sProDenominacion { set; get; }
        //public decimal nInvDisponible { set; get; }
        //public decimal nPrePrecio { set; get; }
        //public int nIdMoneda { set; get; }
        //public decimal nTCaVenta { set; get; }
        //public decimal nInvCantidad { set; get; }
        //public string sPrePrecioSol { set; get; }
        //public string sPrePrecioDolar { set; get; }

    }
}