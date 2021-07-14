using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Spine.Librerias.General
{
    public static class Metodos
    {
        public static string ToJson<T>(T poObjeto, bool plFormatear = true)
        {
            if (plFormatear)
                return JsonConvert.SerializeObject(poObjeto, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            else
                return JsonConvert.SerializeObject(poObjeto, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static DateTime GetFechaSistema()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "SA Pacific Standard Time");
        }

        public static string Encriptar(string psCadena)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(psCadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string ValidarVacio(string psCadena)
        {
            return string.IsNullOrEmpty(psCadena) ? string.Empty : psCadena.Trim();
        }
    }
}
