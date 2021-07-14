using Spine.Librerias.General;
using System.Collections.Generic;
using System.Linq;
using Spine.Librerias.Database;
using System.Threading.Tasks;
using Spine.Constants.Cmn;
using Spine.Constants.Cfg;
using Spine.Entities.Aud;
using Spine.Entities.Seg;
using Spine.Repositories.Interfaces.Seg;
using Spine.Repositories.Interfaces.Aud;
using Spine.Services.Interfaces.Seg;

namespace Spine.Services.Implementations.Seg
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly ICambioRepository _objCambioRepository;
        private readonly IUsuarioRepository _objUsuarioRepository;

        public UsuarioService(
            ICambioRepository pobjCambioRepository,
            IUsuarioRepository pobjUsuarioRepository
        )
        {
            _objCambioRepository = pobjCambioRepository;
            _objUsuarioRepository = pobjUsuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> Consultar(
            int piUsuarioId = -1, string psUsuUsuario = "", string psUsuCorreo = "", int piSucursalId = -1,
            int piPersonaId = -1, short piUsuEstado = -1
        )
        {
            return await _objUsuarioRepository.Consultar(
                    piUsuarioId: piUsuarioId, psUsuUsuario: psUsuUsuario, psUsuCorreo: psUsuCorreo, piSucursalId: piSucursalId,
                    piPersonaId: piPersonaId, piUsuEstado: piUsuEstado
                );
        }

        public async Task<Usuario> Crear(Sesion pobjSesion, Usuario pobjUsuario)
        {
            // Validamos campos
            if (ValidarCampos(pobjUsuario))
            {
                // Validamos datos
                if (await _objUsuarioRepository.ValidarGuardar(pobjUsuario))
                {
                    // Preparamos usuario
                    pobjUsuario.sUsuClave = Metodos.Encriptar(pobjUsuario.sUsuUsuario);
                    pobjUsuario.iUsuEstado = EstadoConstant.Activado;

                    // Preparamos cambios
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iCamRegistro = pobjUsuario.iUsuarioId;
                    vobjCambio.iAccionId = AccionConstant.Usuario_Creacion;

                    // Ejecutamos transacción
                    using (var vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objUsuarioRepository.Crear(vobjConexion, pobjUsuario);
                        vobjCambio.iCamRegistro = pobjUsuario.iUsuarioId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return (await _objUsuarioRepository.Consultar(piUsuarioId: pobjUsuario.iUsuarioId)).FirstOrDefault();
                }
            }
            return null;
        }

        public async Task<Usuario> Editar(Sesion pobjSesion, Usuario pobjUsuario)
        {
            // Validación de campos
            if (ValidarCampos(pobjUsuario))
            {
                // Validación de datos
                if (await _objUsuarioRepository.ValidarGuardar(pobjUsuario))
                {
                    // Preparamos usuario
                    pobjUsuario.iUsuEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iCamRegistro = pobjUsuario.iUsuarioId;
                    vobjCambio.iAccionId = AccionConstant.Usuario_Edicion;

                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objUsuarioRepository.Editar(vobjConexion, pobjUsuario);
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return (await _objUsuarioRepository.Consultar(piUsuarioId: pobjUsuario.iUsuarioId)).FirstOrDefault();
                }
            }
            return null;
        }

        public async Task<Usuario> Activar(Sesion pobjSesion, int piUsuId)
        {
            // Validación de campos
            if (piUsuId <= 0)
                throw Utilitarios.GetValidacion("'piUsuId' no puede ser menor o igual a cero.");

            // Validación de datos
            Usuario vobjUsuario = (await _objUsuarioRepository.Consultar(piUsuarioId: piUsuId)).FirstOrDefault();
            if (vobjUsuario == null)
                throw Utilitarios.GetValidacion("Usuario no existe.");
            else if (vobjUsuario.iUsuEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Usuario ya ha sido activado anteriormente.");

            // Preparamos usuario
            vobjUsuario.iUsuEstado = EstadoConstant.Activado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piUsuId;
            vobjCambio.iAccionId = AccionConstant.Usuario_Activacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objUsuarioRepository.Editar(vobjConexion, vobjUsuario);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objUsuarioRepository.Consultar(piUsuarioId: piUsuId)).FirstOrDefault();
        }

        public async Task<Usuario> Desactivar(Sesion pobjSesion, int piUsuId)
        {
            // Validación de campos
            if (piUsuId <= 0)
                throw Utilitarios.GetValidacion("'piUsuId' no puede ser menor o igual a cero.");

            // Validación de datos
            Usuario vobjUsuario = (await _objUsuarioRepository.Consultar(piUsuarioId: piUsuId)).FirstOrDefault();
            if (vobjUsuario == null)
                throw Utilitarios.GetValidacion("Usuario no existe.");
            else if (vobjUsuario.iUsuEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Usuario ya ha sido desactivado anteriormente.");

            // Preparamos usuario
            vobjUsuario.iUsuEstado = EstadoConstant.Desactivado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piUsuId;
            vobjCambio.iAccionId = AccionConstant.Usuario_Desactivacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objUsuarioRepository.Editar(vobjConexion, vobjUsuario);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objUsuarioRepository.Consultar(piUsuarioId: piUsuId)).FirstOrDefault();
        }

        public async Task<bool> ReiniciarClave(Sesion pobjSesion, int piUsuId)
        {
            // Validamos campos
            if (piUsuId <= 0)
                throw Utilitarios.GetValidacion("'piUsuId' no puede ser menor o igual a cero.");

            // Validamos datos
            Usuario vobjUsuario = (await _objUsuarioRepository.Consultar(piUsuarioId: piUsuId)).FirstOrDefault();
            if (vobjUsuario == null)
                throw Utilitarios.GetValidacion("Usuario no existe.");
            else if (vobjUsuario.iUsuEstado != EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Usuario no se encuentra activado.");

            // Preparamos usuario
            vobjUsuario.sUsuClave = Metodos.Encriptar(vobjUsuario.sUsuUsuario);

            // Preparamos cambio
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piUsuId;
            vobjCambio.iAccionId = AccionConstant.Usuario_ReinicioClave;

            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objUsuarioRepository.ActualizarClave(vobjConexion, vobjUsuario.iUsuarioId, vobjUsuario.sUsuClave);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }
            return true;
        }

        public async Task<IEnumerable<UsuarioRol>> ConsultarRoles(int piUsuId)
        {
            return await _objUsuarioRepository.ConsultarRoles(piUsuId);
        }

        public async Task<bool> EditarRoles(Sesion pobjSesion, int piUsuId, IEnumerable<UsuarioRol> plstUsuarioRoles)
        {
            // Preparamos cambio
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piUsuId;
            vobjCambio.iAccionId = AccionConstant.Usuario_EdicionRoles;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objUsuarioRepository.EditarRoles(vobjConexion, piUsuId, plstUsuarioRoles);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }
            return true;
        }
    }
}