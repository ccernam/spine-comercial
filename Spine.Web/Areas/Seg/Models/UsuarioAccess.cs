using Spine.Entities.Seg;
using System.Collections.Generic;

namespace Spine.Web.Areas.Seg.Models
{
    public class UsuarioAccess
    {
        public UsuarioAccess() { }

        public UsuarioAccess(IEnumerable<UsuarioAccion> plstUsuarioAcciones)
        {
        }

        public bool lCrear { private set; get; } = true;
        public bool lEditar { private set; get; } = true;
        public bool lActivar { private set; get; } = true;
        public bool lDesactivar { private set; get; } = true;
        public bool lReiniciarClave { private set; get; } = true;
        public bool lEditarRoles { private set; get; } = true;
    }
}