using Spine.Constants.Cfg;
using Spine.Repositories.Cmn;
using Spine.Repositories.Implementations.Aud;
using Spine.Entities.Aud;
using Spine.Entities.Cmn;

using Spine.Librerias.Database;
using Spine.Librerias.General;
using System.Collections.Generic;
using System.Linq;

namespace Spine.Services.Cmn
{
    public class PersonaService : ServiceBase
    {
        public PersonaService() : base()
        {
        }

        public IEnumerable<Persona> Consultar(int piPerId = -1, short piPerTipo = -1, short piPerTipoDocumento = -1, string psPerDocumento = "", string psPerNombre = "", string psPerComodin = "", short piPerEstado = -1)
        {
            return new PersonaRepository().Consultar(piPerId: piPerId, piPerTipo: piPerTipo, piPerTipoDocumento: piPerTipoDocumento, psPerDocumento: psPerDocumento, psPerNombre: psPerNombre, psPerComodin: psPerComodin, piPerEstado: piPerEstado);
        }

        public Persona Obtener(int piPerId)
        {
            PersonaRepository vdaoPersona = new PersonaRepository();
            Persona vobjPersona = vdaoPersona.Consultar(piPerId: piPerId).First();
            vobjPersona.lstPersonaInfos = vdaoPersona.ConsultarInfos(piPerId: vobjPersona.iPerId);
            return vobjPersona;
        }

        public bool Activar(int piPerId)
        {
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();

                new PersonaRepository().CambiarEstado(vobjConexion, piPerId, 1);

                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iUsuarioId = 1; // objSesion.iUsuarioId;
                vobjCambio.iCamRegistro = piPerId;
                vobjCambio.iAccionId = AccionConstant.Persona_Activacion;
                new CambioRepository().Crear(vobjConexion, vobjCambio);

                vobjConexion.Commit();

                return true;
            }
        }

        public bool Desactivar(int piPerId)
        {
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();

                new PersonaRepository().CambiarEstado(vobjConexion, piPerId, 0);

                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iUsuarioId = 1; // objSesion.iUsuarioId;
                vobjCambio.iCamRegistro = piPerId;
                vobjCambio.iAccionId = AccionConstant.Persona_Desactivacion;
                new CambioRepository().Crear(vobjConexion, vobjCambio);

                vobjConexion.Commit();

                return true;
            }
        }


    }
}
