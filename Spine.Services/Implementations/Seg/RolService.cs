using Spine.Constants.Cfg;
using Spine.Constants.Cmn;
using Spine.Repositories.Interfaces.Aud;
using Spine.Repositories.Interfaces.Seg;
using Spine.Entities.Aud;
using Spine.Entities.Seg;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using Spine.Services.Interfaces.Seg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Seg
{
    public class RolService : ServiceBase, IRolService
    {
        private readonly ICambioRepository _objCambioRepository;
        private readonly IRolRepository _objRolRepository;

        public RolService(
            ICambioRepository pobjCambioRepository,
            IRolRepository pobjRolRepository
        ) : base()
        {
            _objCambioRepository = pobjCambioRepository;
            _objRolRepository = pobjRolRepository;
        }

        public async Task<IEnumerable<Rol>> Consultar(int piRolId = -1, short piRolEstado = -1)
        {
            return await _objRolRepository.Consultar(piRolId: piRolId, piRolEstado: piRolEstado);
        }

        public async Task<Rol> Crear(Sesion pobjSesion, Rol pobjRol)
        {
            // Validamos campos
            if (ValidarCampos(pobjRol))
            {
                // Validamos datos
                if (await _objRolRepository.ValidarGuardar(pobjRol))
                {
                    // Preparamos categoría
                    pobjRol.iRolEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAccionId = AccionConstant.Rol_Creacion;

                    // Ejecutamos transacción
                    using (var vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objRolRepository.Crear(vobjConexion, pobjRol);
                        vobjCambio.iCamRegistro = pobjRol.iRolId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }
                    return pobjRol;
                }
            }
            return null;
        }

        public async Task<Rol> Editar(Sesion pobjSesion, Rol pobjRol)
        {
            // Validamos campos
            if (ValidarCampos(pobjRol))
            {
                // Validamos datos
                if (await _objRolRepository.ValidarGuardar(pobjRol))
                {
                    // Preparamos categoría
                    pobjRol.iRolEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iCamRegistro = pobjRol.iRolId;
                    vobjCambio.iAccionId = AccionConstant.Rol_Edicion;

                    // Ejecutamos transacción
                    using (var vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objRolRepository.Editar(vobjConexion, pobjRol);
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }
                    return pobjRol;
                }
            }
            return null;
        }

        public async Task<Rol> Activar(Sesion pobjSesion, int piRolId)
        {
            // Validación de campos
            if (piRolId <= 0)
                throw Utilitarios.GetValidacion("'iRolId' no puede ser menor o igual a cero.");

            // Validación de datos
            Rol vobjRol = (await _objRolRepository.Consultar(piRolId: piRolId)).FirstOrDefault();
            if (vobjRol == null)
                throw Utilitarios.GetValidacion("Rol no se encuentra registrado.");
            else if (vobjRol.iRolEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Rol ya se encuentra activado.");

            // Preparamos rol
            vobjRol.iRolEstado = EstadoConstant.Activado;

            // Preparamos cambio
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = vobjRol.iRolId;
            vobjCambio.iAccionId = AccionConstant.Rol_Activacion;

            // Ejecutamos transacción
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objRolRepository.Editar(vobjConexion, vobjRol);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            // Retornamos respuesta
            return (await _objRolRepository.Consultar(piRolId: vobjRol.iRolId)).FirstOrDefault();
        }

        public async Task<Rol> Desactivar(Sesion pobjSesion, int piRolId)
        {
            // Validación de campos
            if (piRolId <= 0)
                throw Utilitarios.GetValidacion("'iRolId' no puede ser menor o igual a cero.");

            // Validación de datos
            var vobjRol = (await _objRolRepository.Consultar(piRolId: piRolId)).FirstOrDefault();
            if (vobjRol == null)
                throw Utilitarios.GetValidacion("Rol no se encuentra registrado.");
            else if (vobjRol.iRolEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Rol ya se encuentra desactivado.");

            // Preparamos rol
            vobjRol.iRolEstado = EstadoConstant.Desactivado;

            // Preparamos cambio
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = vobjRol.iRolId;
            vobjCambio.iAccionId = AccionConstant.Rol_Desactivacion;

            // Ejecutamos transacción
            using (var vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objRolRepository.Editar(vobjConexion, vobjRol);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            // Retornamos respuesta
            return (await _objRolRepository.Consultar(piRolId: vobjRol.iRolId)).FirstOrDefault();
        }

        public async Task<IEnumerable<RolOpcion>> ConsultarOpciones(int piRolId)
        {
            return await _objRolRepository.ConsultarOpciones(piRolId);
        }

        public async Task<bool> EditarOpciones(Sesion pobjSesion, int piRolId, IEnumerable<RolOpcion> plstRolOpciones)
        {
            // Validación de datos
            Rol vobjRol = (await _objRolRepository.Consultar(piRolId: piRolId)).FirstOrDefault();
            if (vobjRol == null)
                throw Utilitarios.GetValidacion("Rol no se encuentra registrado.");
            else if (vobjRol.iRolEstado != EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Rol no se encuentra activado.");


            // Preparamos cambio
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piRolId;
            vobjCambio.iAccionId = AccionConstant.Rol_EdicionOpciones;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objRolRepository.EditarOpciones(vobjConexion, piRolId, plstRolOpciones);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }
            return true;
        }
    }
}
