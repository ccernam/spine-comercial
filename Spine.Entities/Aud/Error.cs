using System;

namespace Spine.Entities.Aud
{
    public class Error
    {
        public int iErrorId { set; get; }
        public int iUsuarioId { set; get; }
        public byte iAplicacionId { set; get; }
        public DateTime dErrFecha { set; get; }
        public string sErrMensaje { set; get; }
        public string sErrPila { set; get; }
        public string sErrParametros { set; get; }
    }
}
