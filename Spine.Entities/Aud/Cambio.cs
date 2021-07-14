using System;

namespace Spine.Entities.Aud
{
    public class Cambio
    {
        public Cambio() { }

        // Columnas
        public int iCambioId { set; get; }
        public DateTime dCamFecha { set; get; }
        public int iUsuarioId { set; get; }
        public int iAplicacionId { set; get; }
        public int iCamRegistro { set; get; }
        public int iAccionId { set; get; }

        // Otros
        public string sUsuUsuario { set; get; }
        public string sAppNombre { set; get; }
        public string sAcnNombre { set; get; }
    }
}
