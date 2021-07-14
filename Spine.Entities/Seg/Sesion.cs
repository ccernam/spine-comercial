using System;

namespace Spine.Entities.Seg
{
    public class Sesion
    {
        public Sesion() { }

        // Columnas
        public int iSesionId { set; get; }
        public int iUsuarioId { set; get; }
        public int iAplicacionId { set; get; }
        public string sSesAccessToken { set; get; }
        public string sSesRefreshToken { set; get; }
        public DateTime dSesExpiracion { set; get; }
        public byte iSesEstado { set; get; }
        public DateTime dSesCreacion { set; get; }
        public DateTime dSesEdicion { set; get; }

        // Relaciones
        public string sAppNombre { set; get; }
        public string sUsuUsuario { set; get; }
        public string sUsuCorreo { set; get; }
        public int? iPersonaId { set; get; }
        public string sPerNombre { set; get; }
        public int? iSucursalId { set; get; }
        public string sSucNombre { set; get; }
        public bool lUsuCambiarClave { set; get; }
    }
}
