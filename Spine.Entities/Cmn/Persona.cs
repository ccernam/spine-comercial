using System.Collections.Generic;

namespace Spine.Entities.Cmn
{
    public class Persona
    {
        public Persona() { }

        // Columnas
        public int iPerId { set; get; }
        public byte iPerTipo { set; get; }
        public byte iPerTipoDocumento { set; get; }
        public string sPerDocumento { set; get; }
        public byte iPerTipoAtencion { set; get; }
        public string sPerNombre { set; get; }
        public string sPerApellidos { set; get; }
        public string sPerNombres { set; get; }
        public string sPerRazonSocial { set; get; }
        public string sPerRazonComercial { set; get; }
        public byte iPerEstado { set; get; }

        // Relaciones
        public List<PersonaInfo> lstPersonaInfos { set; get; }

        // Otros
        public string sPerTipo { set; get; }
        public string sPerTipoDocumento { set; get; }
        public string sPerTipoAtencion { set; get; }
        public string sPerEstado { set; get; }
    }
}
