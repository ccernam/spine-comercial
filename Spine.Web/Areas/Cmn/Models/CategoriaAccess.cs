using Spine.Entities.Seg;
using System.Collections.Generic;

namespace Spine.Web.Areas.Cmn.Models
{
    public class CategoriaAccess
    {
        public CategoriaAccess() { }

        public CategoriaAccess(IEnumerable<UsuarioAccion> plstUsuarioAcciones) { }

        public bool lCrear { private set; get; } = true;
        public bool lEditar { private set; get; } = true;
        public bool lActivar { private set; get; } = true;
        public bool lDesactivar { private set; get; } = true;
    }
}