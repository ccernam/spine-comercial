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
using System.Text;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Cmn
{
    public class UnidadMedidaService : ServiceBase, IUnidadMedidaService
    {
        // Aud
        private readonly ICambioRepository _objCambioRepository;
        // Cmn
        private readonly IUnidadMedidaRepository _objUnidadMedidaRepository;

        public UnidadMedidaService(
            // Aud
            ICambioRepository pobjCambioRepository,
            // Cmn
            IUnidadMedidaRepository pobjUnidadMedidaRepository
        )
        {
            _objCambioRepository = pobjCambioRepository;
            _objUnidadMedidaRepository = pobjUnidadMedidaRepository;
        }
        public async Task<IEnumerable<UnidadMedida>> Consultar(int piUniMedId = -1, string psUniMedNombre = "", short piUniMedServicio = -1, short piUniMedEstado = -1)
        {
            return await _objUnidadMedidaRepository.Consultar(piUniMedId: piUniMedId, psUniMedNombre: psUniMedNombre, piUniMedServicio: piUniMedServicio, piUniMedEstado: piUniMedEstado);
        }
        public async Task<UnidadMedida> Crear(Sesion pobjSesion, UnidadMedida pobjUnidadMedida)
        {
            // Validamos campos
            if (ValidarCampos(pobjUnidadMedida))
            {
                // Validamos datos
                if (await _objUnidadMedidaRepository.ValidarGuardar(pobjUnidadMedida))
                {
                    // Preparamos categoría
                    pobjUnidadMedida.iUniMedEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.UnidadMedida_Creacion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objUnidadMedidaRepository.Crear(vobjConexion, pobjUnidadMedida);
                        vobjCambio.iCamRegistro = pobjUnidadMedida.iUnidadMedidaId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjUnidadMedida;
                }
            }
            return null;
        }
        public async Task<UnidadMedida> Editar(Sesion pobjSesion, UnidadMedida pobjUnidadMedida)
        {
            // Validamos campos
            if (ValidarCampos(pobjUnidadMedida))
            {
                // Validamos datos
                if (await _objUnidadMedidaRepository.ValidarGuardar(pobjUnidadMedida))
                {
                    // Preparamos categoría
                    pobjUnidadMedida.iUniMedEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iCamRegistro = pobjUnidadMedida.iUnidadMedidaId;
                    vobjCambio.iAccionId = AccionConstant.UnidadMedida_Edicion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objUnidadMedidaRepository.Editar(vobjConexion, pobjUnidadMedida);
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjUnidadMedida;
                }
            }
            return null;
        }
        public async Task<UnidadMedida> Activar(Sesion pobjSesion, int piUniMedId)
        {
            // Validación de campos
            if (piUniMedId <= 0)
                throw Utilitarios.GetValidacion("'piUniMedId' no puede ser menor o igual a cero.");

            // Validación de datos
            UnidadMedida vobjUnidadMedida = (await _objUnidadMedidaRepository.Consultar(piUniMedId: piUniMedId)).FirstOrDefault();
            if (vobjUnidadMedida == null)
                throw Utilitarios.GetValidacion("Unidad de medida no existe.");
            else if (vobjUnidadMedida.iUniMedEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Unidad de medida ya ha sido activada anteriormente.");

            // Preparamos sucursal
            vobjUnidadMedida.iUniMedEstado = EstadoConstant.Activado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piUniMedId;
            vobjCambio.iAccionId = AccionConstant.UnidadMedida_Activacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objUnidadMedidaRepository.Editar(vobjConexion, vobjUnidadMedida);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objUnidadMedidaRepository.Consultar(piUniMedId: piUniMedId)).FirstOrDefault();
        }
        public async Task<UnidadMedida> Desactivar(Sesion pobjSesion, int piUniMedId)
        {
            // Validación de campos
            if (piUniMedId <= 0)
                throw Utilitarios.GetValidacion("'piUniMedId' no puede ser menor o igual a cero.");

            // Validación de datos
            UnidadMedida vobjUnidadMedida = (await _objUnidadMedidaRepository.Consultar(piUniMedId: piUniMedId)).FirstOrDefault();
            if (vobjUnidadMedida == null)
                throw Utilitarios.GetValidacion("Unidad de medida no existe.");
            else if (vobjUnidadMedida.iUniMedEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Unidad de medida ya ha sido desactivada anteriormente.");

            // Preparamos sucursal
            vobjUnidadMedida.iUniMedEstado = EstadoConstant.Desactivado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piUniMedId;
            vobjCambio.iAccionId = AccionConstant.UnidadMedida_Desactivacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objUnidadMedidaRepository.Editar(vobjConexion, vobjUnidadMedida);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objUnidadMedidaRepository.Consultar(piUniMedId: piUniMedId)).FirstOrDefault();
        }
    }
}
