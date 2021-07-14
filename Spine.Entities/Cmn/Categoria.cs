namespace Spine.Entities.Cmn
{
    public class Categoria
    {
        public Categoria() { }

        // Columnas
        public short iCategoriaId { get; set; }
        public byte iCtgTipoProducto { get; set; }
        public string sCtgNombre { get; set; }
        public byte iCtgEstado { get; set; }

        // Otros
        public string sCtgTipoProducto { get; set; }
        public string sCtgEstado { get; set; }
    }
}
