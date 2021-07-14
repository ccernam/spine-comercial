using System;

namespace Spine.Entities.Seg
{
    public class Acceso
    {
        public Acceso() { }

        public int iAccesoId { set; get; }
        public int iUsuarioId { set; get; }
        public int iAplicacionId { set; get; }
        public string sAccToken { set; get; }
        public string sAccRefreshToken { set; get; }
        public DateTime dAccExpiracion { set; get; }
        public byte iAccEstado { set; get; }
        public DateTime dAccCreacion { set; get; }
    }
}
