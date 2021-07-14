using Spine.Entities.Seg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spine.Web.Areas.Cmn.Models
{
    public class UnidadMedidaAccess
    {
        public UnidadMedidaAccess() { }

        public UnidadMedidaAccess(IEnumerable<UsuarioAccion> plstUsuarioAcciones) { }

        public bool lCrear { private set; get; } = true;
        public bool lEditar { private set; get; } = true;
        public bool lActivar { private set; get; } = true;
        public bool lDesactivar { private set; get; } = true;
    }
}