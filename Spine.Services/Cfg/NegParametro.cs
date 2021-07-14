using Spine.Repositories.Cfg;
using Spine.Entities.Cfg;

using System.Collections.Generic;

namespace Spine.Services.Cfg
{
    public class NegParametro : ServiceBase
    {
        public NegParametro() : base()
        {
        }

        public List<Parametro> Consultar(short piParEstado = -1)
        {
            return new DaoParametro().Consultar(piParEstado: piParEstado);
        }
    }
}
