using Spine.Entities.Seg;
using System.Collections.Generic;

namespace Spine.Web.Areas.Seg.Models
{
    public class RolAccess
    {
        public RolAccess() { }

        public RolAccess(IEnumerable<UsuarioAccion> plstUsuarioAcciones) { }

        public bool lCrear { private set; get; } = true;
        public bool lEditar { private set; get; } = true;
        public bool lActivar { private set; get; } = true;
        public bool lDesactivar { private set; get; } = true;
        public bool lEditarOpciones { private set; get; } = true;
    }
}