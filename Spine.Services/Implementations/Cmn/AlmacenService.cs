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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spine.Services.Implementations.Cmn
{
    public class AlmacenService : ServiceBase, IAlmacenService
    {
        private readonly ICambioRepository _objCambioRepository;
        private readonly IAlmacenRepository _objAlmacenRepository;

        public AlmacenService(
            ICambioRepository pobjCambioRepository,
            IAlmacenRepository pobjAlmacenRepository
        ) : base()
        {
            _objCambioRepository = pobjCambioRepository;
            _objAlmacenRepository = pobjAlmacenRepository;
        }

        public async Task<IEnumerable<Almacen>> Consultar(int piAlmacenId = -1, int piSucursalId = -1, string psAlmNombre = "", short piAlmEstado = -1)
        {
            return await _objAlmacenRepository.Consultar(piAlmacenId: piAlmacenId, piSucursalId: piSucursalId, psAlmNombre: psAlmNombre, piAlmEstado: piAlmEstado);
        }

        public async Task<Almacen> Crear(Sesion pobjSesion, Almacen pobjAlmacen)
        {
            // Validamos campos
            if (ValidarCampos(pobjAlmacen))
            {
                // Validamos datos
                if (await _objAlmacenRepository.ValidarGuardar(pobjAlmacen))
                {
                    // Preparamos categoría
                    pobjAlmacen.sAlmCodigoSunat = Metodos.ValidarVacio(pobjAlmacen.sAlmCodigoSunat);
                    pobjAlmacen.iAlmEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.Almacen_Creacion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objAlmacenRepository.Crear(vobjConexion, pobjAlmacen);
                        vobjCambio.iCamRegistro = pobjAlmacen.iSucursalId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjAlmacen;
                }
            }
            return null;
        }

        public async Task<Almacen> Editar(Sesion pobjSesion, Almacen pobjAlmacen)
        {
            // Validamos campos
            if (ValidarCampos(pobjAlmacen))
            {
                // Validamos datos
                if (await _objAlmacenRepository.ValidarGuardar(pobjAlmacen))
                {
                    // Preparamos categoría
                    pobjAlmacen.sAlmCodigoSunat = Metodos.ValidarVacio(pobjAlmacen.sAlmCodigoSunat);
                    pobjAlmacen.iAlmEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iCamRegistro = pobjAlmacen.iAlmacenId;
                    vobjCambio.iAccionId = AccionConstant.Almacen_Edicion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objAlmacenRepository.Editar(vobjConexion, pobjAlmacen);
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjAlmacen;
                }
            }
            return null;
        }

        public async Task<Almacen> Activar(Sesion pobjSesion, int piAlmacenId)
        {
            // Validación de campos
            if (piAlmacenId <= 0)
                throw Utilitarios.GetValidacion("'piAlmacenId' no puede ser menor o igual a cero.");

            // Validación de datos
            Almacen vobjAlmacen = (await _objAlmacenRepository.Consultar(piAlmacenId: piAlmacenId)).FirstOrDefault();
            if (vobjAlmacen == null)
                throw Utilitarios.GetValidacion("Almacén no existe.");
            else if (vobjAlmacen.iAlmEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Almacén ya ha sido activado anteriormente.");

            // Preparamos sucursal
            vobjAlmacen.iAlmEstado = EstadoConstant.Activado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piAlmacenId;
            vobjCambio.iAccionId = AccionConstant.Almacen_Activacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objAlmacenRepository.Editar(vobjConexion, vobjAlmacen);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objAlmacenRepository.Consultar(piAlmacenId: piAlmacenId)).FirstOrDefault();
        }

        public async Task<Almacen> Desactivar(Sesion pobjSesion, int piAlmacenId)
        {
            // Validación de campos
            if (piAlmacenId <= 0)
                throw Utilitarios.GetValidacion("'piAlmacenId' no puede ser menor o igual a cero.");

            // Validación de datos
            Almacen vobjAlmacen = (await _objAlmacenRepository.Consultar(piAlmacenId: piAlmacenId)).FirstOrDefault();
            if (vobjAlmacen == null)
                throw Utilitarios.GetValidacion("Almacén no existe.");
            else if (vobjAlmacen.iAlmEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Almacén ya ha sido desactivado anteriormente.");

            // Preparamos sucursal
            vobjAlmacen.iAlmEstado = EstadoConstant.Desactivado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piAlmacenId;
            vobjCambio.iAccionId = AccionConstant.Almacen_Desactivacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objAlmacenRepository.Editar(vobjConexion, vobjAlmacen);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objAlmacenRepository.Consultar(piAlmacenId: piAlmacenId)).FirstOrDefault();
        }
    }
}
