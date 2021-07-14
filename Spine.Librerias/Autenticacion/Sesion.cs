using System.Security.Principal;

namespace Spine.Librerias.Autenticacion
{
    public class Sesion : IIdentity
    {
        public int iAplicacionId { set; get; }
        public string sAppNombre { set; get; }

        public int iUsuarioId { set; get; }
        public string sUsuUsuario { set; get; }
        public string sUsuCorreo { set; get; }

        public int? iPersonaId { set; get; }
        public string sPerNombre { set; get; }

        public int? iSucursalId { set; get; }
        public string sSucNombre { set; get; }

        public bool lSesCambiarClave { set; get; }

        // Atributos heredados
        public string Name { set; get; }
        public string AuthenticationType { get { return "Token"; } }
        public bool IsAuthenticated { get { return true; } }
    }
}
