using Spine.Constants.Cfg;
using Spine.Constants.Cmn;
using Spine.Repositories.Interfaces.Aud;
using Spine.Repositories.Interfaces.Cmn;
using Spine.Entities.Aud;
using Spine.Entities.Cmn;
using Spine.Entities.Seg;
using Spine.Librerias.Database;
using Spine.Librerias.General;
using Spine.Services.Interfaces.Cmn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Cmn
{
    public class SucursalService : ServiceBase, ISucursalService
    {
        // Aud
        private readonly ICambioRepository _objCambioRepository;
        // Cmn
        private readonly ISucursalRepository _objSucursalRepository;

        public SucursalService(
            // Aud
            ICambioRepository pobjCambioRepository,
            // Cmn
            ISucursalRepository pobjSucursalRepository
        ) : base()
        {
            // Aud
            _objCambioRepository = pobjCambioRepository;
            // Cmn
            _objSucursalRepository = pobjSucursalRepository;
        }

        public async Task<IEnumerable<Sucursal>> Consultar(int piSucId = -1, string psSucNombre = "", short piSucEstado = -1)
        {
            return await _objSucursalRepository.Consultar(piSucId: piSucId, psSucNombre: psSucNombre, piSucEstado: piSucEstado);
        }

        public async Task<Sucursal> Crear(Sesion pobjSesion, Sucursal pobjSucursal)
        {
            // Validamos campos
            if (ValidarCampos(pobjSucursal))
            {
                // Validamos datos
                if (await _objSucursalRepository.ValidarGuardar(pobjSucursal))
                {
                    // Preparamos categoría
                    pobjSucursal.iSucEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.Sucursal_Creacion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objSucursalRepository.Crear(vobjConexion, pobjSucursal);
                        vobjCambio.iCamRegistro = pobjSucursal.iSucursalId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjSucursal;
                }
            }
            return null;
        }

        public async Task<Sucursal> Editar(Sesion pobjSesion, Sucursal pobjSucursal)
        {
            // Validamos campos
            if (ValidarCampos(pobjSucursal))
            {
                // Validamos datos
                if (await _objSucursalRepository.ValidarGuardar(pobjSucursal))
                {
                    // Preparamos categoría
                    pobjSucursal.iSucEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iCamRegistro = pobjSucursal.iSucursalId;
                    vobjCambio.iAccionId = AccionConstant.Sucursal_Edicion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objSucursalRepository.Editar(vobjConexion, pobjSucursal);
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjSucursal;
                }
            }
            return null;
        }

        public async Task<Sucursal> Activar(Sesion pobjSesion, int piSucId)
        {
            // Validación de campos
            if (piSucId <= 0)
                throw Utilitarios.GetValidacion("'piSucId' no puede ser menor o igual a cero.");

            // Validación de datos
            Sucursal vobjSucursal = (await _objSucursalRepository.Consultar(piSucId: piSucId)).FirstOrDefault();
            if (vobjSucursal == null)
                throw Utilitarios.GetValidacion("Sucursal no existe.");
            else if (vobjSucursal.iSucEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Sucursal ya ha sido activada anteriormente.");

            // Preparamos sucursal
            vobjSucursal.iSucEstado = EstadoConstant.Activado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piSucId;
            vobjCambio.iAccionId = AccionConstant.Sucursal_Activacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objSucursalRepository.Editar(vobjConexion, vobjSucursal);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objSucursalRepository.Consultar(piSucId: piSucId)).FirstOrDefault();
        }

        public async Task<Sucursal> Desactivar(Sesion pobjSesion, int piSucId)
        {
            // Validación de campos
            if (piSucId <= 0)
                throw Utilitarios.GetValidacion("'piSucId' no puede ser menor o igual a cero.");

            // Validación de datos
            Sucursal vobjSucursal = (await _objSucursalRepository.Consultar(piSucId: piSucId)).FirstOrDefault();
            if (vobjSucursal == null)
                throw Utilitarios.GetValidacion("Sucursal no existe.");
            else if (vobjSucursal.iSucEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Sucursal ya ha sido desactivada anteriormente.");

            // Preparamos sucursal
            vobjSucursal.iSucEstado = EstadoConstant.Desactivado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piSucId;
            vobjCambio.iAccionId = AccionConstant.Sucursal_Desactivacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objSucursalRepository.Editar(vobjConexion, vobjSucursal);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objSucursalRepository.Consultar(piSucId: piSucId)).FirstOrDefault();
        }
    }
}

