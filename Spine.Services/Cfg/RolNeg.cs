using Spine.Entities.Cfg;

using System.Collections.Generic;

namespace Spine.Services.Cfg
{
    public class RolNeg : ServiceBase
    {
        public RolNeg() : base()
        {
        }

        public List<Rol> Consultar(short piRolId = -1, string psRolCodigo = "", short piRolEstado = -1)
        {
            return null;
        }

        public bool Activar(short piRolId)
        {
            return false;
        }

        public bool Desactivar(short piRolId)
        {
            return false;
        }

        public List<RolOpcion> ConsultarOpciones(short piRolId)
        {
            return null;
        }
    }
}
