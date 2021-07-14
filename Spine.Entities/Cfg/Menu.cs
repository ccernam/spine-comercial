
using System.Collections.Generic;

namespace Spine.Entities.Cfg
{
    public class Menu
    {
        public short nIdMenu { set; get; }
        public short nIdMenuPadre { set; get; }
        public short nIdMenuAntesDe { set; get; }
        public byte nIdAplicacion { set; get; }
        public short nIdFormulario { set; get; }
        public string sMenNombre { set; get; }
        public string sMenRuta { set; get; }
        public string sMenIcono { set; get; }
        public byte nMenOrden { set; get; }
        public byte nMenEstado { set; get; }

        // OTROS
        public string sMenEstado { set; get; }
        public string sAplNombre { set; get; }
        public string sForNombre { set; get; }
        public string sForRuta { set; get; }
        public List<Menu> lstMenus { set; get; }
    }
}