using Spine.Constants.Cfg;
using Spine.Constants.Cmn;
using Spine.Repositories.Interfaces.Aud;
using Spine.Repositories.Interfaces.Cfg;
using Spine.Repositories.Interfaces.Seg;
using Spine.Entities.Aud;
using Spine.Entities.Cfg;
using Spine.Entities.Seg;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using Spine.Services.Interfaces.Seg;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Seg
{
    public class SesionService : ServiceBase, ISesionService
    {
        private readonly ICambioRepository _objCambioRepository;
        private readonly IAplicacionRepository _objAplicacionRepository;
        private readonly IUsuarioRepository _objUsuarioRepository;
        private readonly ISesionRepository _objSesionRepository;

        public SesionService(
            ICambioRepository pobjCambioRepository,
            IAplicacionRepository pobjAplicacionRepository,
            IUsuarioRepository pobjUsuarioRepository,
            ISesionRepository pobjSesionRepository
        ) : base()
        {
            _objCambioRepository = pobjCambioRepository;
            _objAplicacionRepository = pobjAplicacionRepository;
            _objUsuarioRepository = pobjUsuarioRepository;
            _objSesionRepository = pobjSesionRepository;
        }

        public async Task<Sesion> Autenticar(Autenticar pobjAutenticar)
        {
            // Validación de campos
            if (ValidarCampos(pobjAutenticar))
            {
                // Validación de datos
                Aplicacion vobjAplicacion = (await _objAplicacionRepository.Consultar(piAplicacionId: pobjAutenticar.iAplicacionId)).FirstOrDefault();
                if (vobjAplicacion == null)
                    throw Utilitarios.GetValidacion("Aplicación no existe.");
                else if (vobjAplicacion.iAppEstado == EstadoConstant.Desactivado)
                    throw Utilitarios.GetValidacion("Aplicación se encuentra desactivada.");

                Usuario vobjUsuario = await _objSesionRepository.Autenticar(pobjAutenticar.sUsuUsuario, Metodos.Encriptar(pobjAutenticar.sUsuClave));
                if (vobjUsuario == null)
                    throw Utilitarios.GetValidacion("Usuario y/o clave incorrectos");
                else if (vobjUsuario.iUsuEstado == EstadoConstant.Desactivado)
                    throw Utilitarios.GetValidacion("Usuario se encuentra desactivado, contáctese con el administrador.");

                // Creamos sesión
                Sesion vobjSesion = new Sesion();
                vobjSesion.iUsuarioId = vobjUsuario.iUsuarioId;
                vobjSesion.sUsuUsuario = vobjUsuario.sUsuUsuario;
                vobjSesion.sUsuCorreo = vobjUsuario.sUsuCorreo;
                vobjSesion.iSucursalId = vobjUsuario.iSucursalId;
                vobjSesion.sSucNombre = vobjUsuario.sSucNombre;
                vobjSesion.iPersonaId = vobjUsuario.iPersonaId;
                vobjSesion.sPerNombre = vobjUsuario.sPerNombre;
                vobjSesion.iAplicacionId = vobjAplicacion.iAplicacionId;
                vobjSesion.sAppNombre = vobjAplicacion.sAppNombre;
                vobjSesion.sSesRefreshToken = string.Empty;
                vobjSesion.sSesAccessToken = string.Empty;
                vobjSesion.dSesExpiracion = Utilitarios.GetFechaSistema().AddMinutes(30);
                vobjSesion.iSesEstado = EstadoConstant.Activado;
                vobjSesion.dSesCreacion = Utilitarios.GetFechaSistema();
                vobjSesion.dSesEdicion = vobjSesion.dSesCreacion;
                await _objSesionRepository.Crear(vobjSesion);

                // Editamos sesión
                vobjSesion.sSesRefreshToken = Utilitarios.GetRefreshToken();
                vobjSesion.sSesAccessToken = Utilitarios.GetAccessToken(vobjSesion);
                _objSesionRepository.EditarSync(vobjSesion);

                // Consultamos menús

                // Retornamos respuesta
                return vobjSesion;
            }
            return null;
        }

        public async Task<bool> CambiarClave(Sesion pobjSesion, CambiarClave pobjCambiarClave)
        {
            if (ValidarCampos(pobjCambiarClave))
            {
                // Validamos datos
                Usuario vobjUsuario = await _objSesionRepository.Autenticar(pobjCambiarClave.sUsuUsuario, Metodos.Encriptar(pobjCambiarClave.sUsuClave));
                if (vobjUsuario == null)
                    throw Utilitarios.GetValidacion("Usuario y/o clave incorrectos");
                else if (vobjUsuario.iUsuEstado == EstadoConstant.Desactivado)
                    throw Utilitarios.GetValidacion("Usuario se encuentra desactivado");

                // Preparamos usuario
                vobjUsuario.sUsuClave = Metodos.Encriptar(pobjCambiarClave.sUsuNuevaClave);

                // Preparamos cambio
                Cambio vobjCambio = new Cambio();
                vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                vobjCambio.iCamRegistro = vobjUsuario.iUsuarioId;
                vobjCambio.iAccionId = AccionConstant.Usuario_CambioClave;

                // Ejecutamos transacción
                using (var vobjConexion = ConexionFactory.Instanciar())
                {
                    vobjConexion.BeginTransaction();
                    await _objUsuarioRepository.ActualizarClave(vobjConexion, vobjUsuario.iUsuarioId, vobjUsuario.sUsuClave);
                    await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                    vobjConexion.Commit();
                }

                // Retornamos respuesta
                return true;
            }
            return false;
        }

        public Sesion RefrescarTokenSync(string psSesRefreshToken)
        {
            if (string.IsNullOrEmpty(psSesRefreshToken))
                throw Utilitarios.GetValidacion("'psSesRefreshToken' no puede ser nulo o vacío.");

            Sesion vobjSesion = _objSesionRepository.ConsultarSync(psSesRefreshToken: psSesRefreshToken).FirstOrDefault();
            if (vobjSesion == null)
                throw Utilitarios.GetValidacion("Sesión no existe.");
            else if (vobjSesion.iSesEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Sesión se encuentra desactivada.");

            vobjSesion.dSesExpiracion = Utilitarios.GetFechaSistema().AddMinutes(30);
            vobjSesion.sSesRefreshToken = Utilitarios.GetRefreshToken();
            vobjSesion.sSesAccessToken = Utilitarios.GetAccessToken(vobjSesion);
            vobjSesion.dSesEdicion = Utilitarios.GetFechaSistema();
            _objSesionRepository.EditarSync(vobjSesion);

            return vobjSesion;
        }

        public bool ValidarSesionSync(string psSesAccessToken)
        {
            return _objSesionRepository.ValidarSesionSync(psSesAccessToken);
        }

        public bool ValidarAccionSync(string psSesAccessToken, string psAcnRuta, string psAcnMetodo)
        {
            return _objSesionRepository.ValidarAccionSync(psSesAccessToken, psAcnRuta, psAcnMetodo);
        }

        public bool ValidarComponenteSync(string psSesAccessToken, string psCmpRuta)
        {
            return _objSesionRepository.ValidarComponenteSync(psSesAccessToken, psCmpRuta);
        }
    }
}
