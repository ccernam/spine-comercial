namespace Spine.Web.Clases
{
    public class ApiRespuesta<T>
    {
        public ApiRespuesta()
        {
        }

        public byte iTipo { set; get; }
        public string sMensaje { set; get; }
        public T objDatos { set; get; }
        public int iErrId { set; get; }
    }
}
