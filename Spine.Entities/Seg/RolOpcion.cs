namespace Spine.Entities.Seg
{
    public class RolOpcion
    {
        public RolOpcion() { }

        // Columnas
        public int iRolOpcionId { set; get; }
        public int iRolId { set; get; }
        public int iOpcionId { set; get; }
        public bool lRolOpcActivado { set; get; }

        // Otros
        public int? iOpcionPadreId { set; get; }
        public string sOpcRama { set; get; }
        public string sOpcDescripcion { set; get; }
        public bool lOpcActivable { set; get; }
    }
}
