using Spine.Entities.Seg;
using System.Collections.Generic;

namespace Spine.Web.Areas.Cmn.Models
{
    public class AlmacenAccess
    {
        public AlmacenAccess() { }

        public AlmacenAccess(IEnumerable<UsuarioAccion> plstUsuarioAcciones) { }

        public bool lCrear { private set; get; } = true;
        public bool lEditar { private set; get; } = true;
        public bool lActivar { private set; get; } = true;
        public bool lDesactivar { private set; get; } = true;
    }
}