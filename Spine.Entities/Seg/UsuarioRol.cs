namespace Spine.Entities.Seg
{
    public class UsuarioRol
    {
        // Constructor
        public UsuarioRol() { }

        // Columnas
        public int iUsuarioRolId { set; get; }
        public int iUsuarioId { set; get; }
        public int iRolId { set; get; }
        public bool lUsuRolActivado { set; get; }

        // Otros
        public string sRolNombre { set; get; }
        public string sRolDescripcion { set; get; }
    }
}