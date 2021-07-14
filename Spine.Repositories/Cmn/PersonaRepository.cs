using Spine.Entities.Cmn;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Spine.Repositories.Cmn
{
    public class PersonaRepository : RepositoryBase
    {
        public PersonaRepository() : base()
        {
        }

        public List<Persona> Consultar(int piPerId = -1, short piPerTipo = -1, short piPerTipoDocumento = -1, string psPerDocumento = "", string psPerNombre = "", string psPerComodin = "", short piPerEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<Persona>(
                    "Cmn.pa_Persona_Consultar",
                    new SqlParameter("@piPerId", piPerId),
                    new SqlParameter("@piPerTipo", piPerTipo),
                    new SqlParameter("@piPerTipoDocumento", piPerTipoDocumento),
                    new SqlParameter("@psPerDocumento", psPerDocumento),
                    new SqlParameter("@psPerNombre", psPerNombre),
                    new SqlParameter("@psPerComodin", psPerComodin),
                    new SqlParameter("@piPerEstado", piPerEstado)/*,
                    new SqlParameter("@psCatCodigoPerTipo", CodCatalogo.CMN_PERSONA_TIPO),
                    new SqlParameter("@psCatCodigoPerTipoDocumento", CodCatalogo.CMN_PERSONA_TIPO_DOCUMENTO),
                    new SqlParameter("@psCatCodigoPerTipoAtencion", CodCatalogo.CMN_PERSONA_TIPO_ATENCION),
                    new SqlParameter("@psCatCodigoPerEstado", CodCatalogo.AUD_ESTADO)*/
                );
            }
        }


        public List<PersonaInfo> ConsultarInfos(int piPerInfId = -1, int piPerId = -1, short piPerInfTipo = -1, short piPerInfPrincipal = -1, short piPerInfEstado = -1)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<PersonaInfo>(
                    "Cmn.pa_PersonaInfo_Consultar",
                    new SqlParameter("@piPerInfId", piPerInfId),
                    new SqlParameter("@piPerId", piPerId),
                    new SqlParameter("@piPerInfTipo", piPerInfTipo),
                    new SqlParameter("@piPerInfPrincipal", piPerInfPrincipal),
                    new SqlParameter("@piPerInfEstado", piPerInfEstado)/*,
                    new SqlParameter("@psCatCodigoTipoInfo", CodCatalogo.CMN_PERSONA_INFO_TIPO),
                    new SqlParameter("@psCatCodigoEstado", CodCatalogo.AUD_ESTADO)*/
                );
            }
        }

        public Persona Obtener(int piPerId)
        {
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                return vobjConexion.EjecutarConsulta<Persona>(
                    "Cmn.pa_Persona_Consultar",
                    new SqlParameter("@piPerId", piPerId)/*,
                    new SqlParameter("@psCatCodigoTipo", CodCatalogo.CMN_PERSONA_TIPO),
                    new SqlParameter("@psCatCodigoTipoDoc", CodCatalogo.CMN_PERSONA_TIPO_ATENCION),
                    new SqlParameter("@psCatCodigoEstado", CodCatalogo.AUD_ESTADO)*/
                ).FirstOrDefault();
            }
        }

        public int Crear(Conexion pobjConexion, Persona pobjPersona)
        {
            SqlParameter[] varrParametros = new SqlParameter[] {
                new SqlParameter(){ ParameterName = "@piPerId", Direction = ParameterDirection.Output },
                new SqlParameter(){ ParameterName = "@pxPersona", Value = Metodos.ToJson(pobjPersona) }
            };
            pobjConexion.Ejecutar("Cmn.pa_PersonaCrear_", varrParametros);
            pobjPersona.iPerId = Convert.ToInt32(varrParametros.First(x => x.ParameterName == "@piPerId").Value);
            return pobjPersona.iPerId;
        }

        public int Editar(Conexion pobjConexion, Persona pobjPersona)
        {
            pobjConexion.Ejecutar(
                "Cmn.pa_Persona_Editar",
                new SqlParameter() { ParameterName = "@pxPersona", Value = Metodos.ToJson(pobjPersona) }
            );
            return pobjPersona.iPerId;
        }

        public bool CambiarEstado(Conexion pobjConexion, int piPerId, byte piPerEstado)
        {
            return pobjConexion.Ejecutar(
                "Cmn.pa_Persona_CambiarEstado",
                new SqlParameter() { ParameterName = "@piPerId", Value = piPerId },
                new SqlParameter() { ParameterName = "@piPerEstado", Value = piPerEstado }
            ) == 1;
        }
    }
}
