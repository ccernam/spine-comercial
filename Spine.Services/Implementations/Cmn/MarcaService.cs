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
    public class MarcaService : ServiceBase, IMarcaService
    {
        // Aud
        private readonly ICambioRepository _objCambioRepository;
        // Cmn
        private readonly IMarcaRepository _objMarcaRepository;

        public MarcaService(
            // Aud
            ICambioRepository pobjCambioRepository,
            // Cmn
            IMarcaRepository pobjMarcaRepository
        )
        {
            // Aud
            _objCambioRepository = pobjCambioRepository;
            // Cmn
            _objMarcaRepository = pobjMarcaRepository;
        }

        public async Task<IEnumerable<Marca>> Consultar(int piMarId = -1, string psMarNombre = "", short piMarEstado = -1)
        {
            return await _objMarcaRepository.Consultar(piMarId: piMarId, psMarNombre: psMarNombre, piMarEstado: piMarEstado);
        }

        public async Task<Marca> Crear(Sesion pobjSesion, Marca pobjMarca)
        {
            // Validamos campos
            if (ValidarCampos(pobjMarca))
            {
                // Validamos datos
                if (await _objMarcaRepository.ValidarGuardar(pobjMarca))
                {
                    // Preparamos marca
                    pobjMarca.iMarEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.Marca_Creacion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objMarcaRepository.Crear(vobjConexion, pobjMarca);
                        vobjCambio.iCamRegistro = pobjMarca.iMarId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjMarca;
                }
            }
            return null;
        }

        public async Task<Marca> Editar(Sesion pobjSesion, Marca pobjMarca)
        {
            // Validamos campos
            if (ValidarCampos(pobjMarca))
            {
                // Validamos datos
                if (await _objMarcaRepository.ValidarGuardar(pobjMarca))
                {
                    // Preparamos marca
                    pobjMarca.iMarEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.Marca_Edicion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objMarcaRepository.Editar(vobjConexion, pobjMarca);
                        vobjCambio.iCamRegistro = pobjMarca.iMarId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjMarca;
                }
            }
            return null;
        }

        public async Task<Marca> Activar(Sesion pobjSesion, int piMarId)
        {
            // Validación de campos
            if (piMarId <= 0)
                throw Utilitarios.GetValidacion("'piMarId' no puede ser menor o igual a cero.");

            // Validación de datos
            Marca vobjMarca = (await _objMarcaRepository.Consultar(piMarId: piMarId)).FirstOrDefault();
            if (vobjMarca == null)
                throw Utilitarios.GetValidacion("Marca no existe.");
            else if (vobjMarca.iMarEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Marca ya ha sido activado anteriormente.");

            // Preparamos marca
            vobjMarca.iMarEstado = EstadoConstant.Activado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piMarId;
            vobjCambio.iAccionId = AccionConstant.Marca_Activacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objMarcaRepository.Editar(vobjConexion, vobjMarca);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objMarcaRepository.Consultar(piMarId: piMarId)).FirstOrDefault();
        }

        public async Task<Marca> Desactivar(Sesion pobjSesion, int piMarId)
        {
            // Validación de campos
            if (piMarId <= 0)
                throw Utilitarios.GetValidacion("'piMarId' no puede ser menor o igual a cero.");

            // Validación de datos
            Marca vobjMarca = (await _objMarcaRepository.Consultar(piMarId: piMarId)).FirstOrDefault();
            if (vobjMarca == null)
                throw Utilitarios.GetValidacion("Marca no existe.");
            else if (vobjMarca.iMarEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Marca ya ha sido desactivado anteriormente.");

            // Preparamos marca
            vobjMarca.iMarEstado = EstadoConstant.Desactivado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piMarId;
            vobjCambio.iAccionId = AccionConstant.Marca_Desactivacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objMarcaRepository.Editar(vobjConexion, vobjMarca);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objMarcaRepository.Consultar(piMarId: piMarId)).FirstOrDefault();
        }
    }
}
