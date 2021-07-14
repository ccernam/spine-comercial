using Spine.Repositories.Cfg;
using Spine.Entities.Cfg;

using System.Collections.Generic;

namespace Spine.Services.Cfg
{
    public class MonedaNeg : ServiceBase
    {
        public MonedaNeg() : base()
        {
        }

        public List<Moneda> Consultar(short piMonId = -1, short piMonEstado = -1)
        {
            return new DaoMoneda().Consultar(piMonId: piMonId, piMonEstado: piMonEstado);
        }

    }
}
