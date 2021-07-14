
using System;
using System.Xml.Serialization;

namespace Spine.Entities.Cmn
{
    public class IntervaloDet
    {
        public int nIdIntervaloDet { set; get; }

        public short nIdIntervalo { set; get; }

        public decimal nIDeInicio { set; get; }

        public decimal nIDeFin { set; get; }

        public decimal nIDeValor { set; get; }

        public byte nIDeEstado { set; get; }

        public DateTime dIDeCreacion { set; get; }

        public int nIdSesionCreacion { set; get; }

        public DateTime dIDeEdicion { set; get; }

        public int nIdSesionEdicion { set; get; }

        public byte lIDeActivo { set; get; }
    }
}
