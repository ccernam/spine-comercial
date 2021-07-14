namespace Spine.Entities.Seg
{
    public class Usuario
    {
        public Usuario() { }

        // Columnas
        public int iUsuarioId { set; get; }
        public string sUsuUsuario { set; get; }
        public int? iSucursalId { set; get; }
        public int? iPersonaId { set; get; }
        public string sUsuClave { set; get; }
        public string sUsuCorreo { set; get; }
        public byte iUsuEstado { set; get; }

        // Otros

        public string sSucNombre { set; get; }
        public string sPerNombre { set; get; }
        public string sUsuEstado { set; get; }
        public string sUsuNuevaclave { set; get; }
    }
}