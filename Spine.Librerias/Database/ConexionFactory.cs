using System.Configuration;

namespace Spine.Librerias.Database
{
    public static class ConexionFactory
    {
        public static Conexion Instanciar()
        {
            return new Conexion(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }
    }
}
