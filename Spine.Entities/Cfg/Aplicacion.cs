using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spine.Entities.Cfg
{
    public class Aplicacion
    {
        public Aplicacion() { }

        // Columnas
        public int iAplicacionId { set; get; }
        public string sAppNombre { set; get; }
        public byte iAppEstado { set; get; }

        // Otros
        public string sAppEstado { set; get; }
    }
}
