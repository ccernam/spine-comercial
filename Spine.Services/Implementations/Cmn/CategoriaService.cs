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
    public class CategoriaService : ServiceBase, ICategoriaService
    {
        private readonly ICambioRepository _objCambioRepository;
        private readonly ICategoriaRepository _objCategoriaRepository;

        public CategoriaService(
            ICambioRepository pobjCambioRepository,
            ICategoriaRepository pobjCategoriaRepository
        ) : base()
        {
            _objCambioRepository = pobjCambioRepository;
            _objCategoriaRepository = pobjCategoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> Consultar(int piCategoriaId = -1, short piCtgTipoProducto = -1, string psCtgNombre = "", short piCtgEstado = -1)
        {
            return await _objCategoriaRepository.Consultar(piCategoriaId: piCategoriaId, piCtgTipoProducto: piCtgTipoProducto, psCtgNombre: psCtgNombre, piCtgEstado: piCtgEstado);
        }

        public async Task<Categoria> Crear(Sesion pobjSesion, Categoria pobjCategoria)
        {
            // Validamos campos
            if (ValidarCampos(pobjCategoria))
            {
                // Validamos datos
                if (await _objCategoriaRepository.ValidarGuardar(pobjCategoria))
                {
                    // Preparamos categoría
                    pobjCategoria.iCtgEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.Categoria_Creacion;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objCategoriaRepository.Crear(vobjConexion, pobjCategoria);
                        vobjCambio.iCamRegistro = pobjCategoria.iCategoriaId;
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjCategoria;
                }
            }
            return null;
        }

        public async Task<Categoria> Editar(Sesion pobjSesion, Categoria pobjCategoria)
        {
            // Validamos campos
            if (ValidarCampos(pobjCategoria))
            {
                // Validamos datos
                if (await _objCategoriaRepository.ValidarGuardar(pobjCategoria))
                {
                    // Preparamos categoría
                    pobjCategoria.iCtgEstado = EstadoConstant.Activado;

                    // Preparamos cambio
                    Cambio vobjCambio = new Cambio();
                    vobjCambio.dCamFecha = Metodos.GetFechaSistema();
                    vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
                    vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
                    vobjCambio.iAccionId = AccionConstant.Categoria_Edicion;
                    vobjCambio.iCamRegistro = pobjCategoria.iCategoriaId;

                    // Ejecutamos transacción
                    using (Conexion vobjConexion = ConexionFactory.Instanciar())
                    {
                        vobjConexion.BeginTransaction();
                        await _objCategoriaRepository.Editar(vobjConexion, pobjCategoria);
                        await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                        vobjConexion.Commit();
                    }

                    // Retornamos respuesta
                    return pobjCategoria;
                }
            }
            return null;
        }

        public async Task<Categoria> Activar(Sesion pobjSesion, int piCategoriaId)
        {
            // Validación de campos
            if (piCategoriaId <= 0)
                throw Utilitarios.GetValidacion("'piCategoriaId' no puede ser menor o igual a cero.");

            // Validación de datos
            Categoria vobjCategoria = (await _objCategoriaRepository.Consultar(piCategoriaId: piCategoriaId)).FirstOrDefault();
            if (vobjCategoria == null)
                throw Utilitarios.GetValidacion("Categoría no existe.");
            else if (vobjCategoria.iCtgEstado == EstadoConstant.Activado)
                throw Utilitarios.GetValidacion("Categoría ya ha sido activado anteriormente.");

            // Preparamos categoría
            vobjCategoria.iCtgEstado = EstadoConstant.Activado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piCategoriaId;
            vobjCambio.iAccionId = AccionConstant.Categoria_Activacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objCategoriaRepository.Editar(vobjConexion, vobjCategoria);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objCategoriaRepository.Consultar(piCategoriaId: piCategoriaId)).FirstOrDefault();
        }

        public async Task<Categoria> Desactivar(Sesion pobjSesion, int piCategoriaId)
        {
            // Validación de campos
            if (piCategoriaId <= 0)
                throw Utilitarios.GetValidacion("'piCategoriaId' no puede ser menor o igual a cero.");

            // Validación de datos
            Categoria vobjCategoria = (await _objCategoriaRepository.Consultar(piCategoriaId: piCategoriaId)).FirstOrDefault();
            if (vobjCategoria == null)
                throw Utilitarios.GetValidacion("Categoría no existe.");
            else if (vobjCategoria.iCtgEstado == EstadoConstant.Desactivado)
                throw Utilitarios.GetValidacion("Categoría ya ha sido desactivado anteriormente.");

            // Preparamos categoría
            vobjCategoria.iCtgEstado = EstadoConstant.Desactivado;

            // Preparamos cambios
            Cambio vobjCambio = new Cambio();
            vobjCambio.dCamFecha = Metodos.GetFechaSistema();
            vobjCambio.iUsuarioId = pobjSesion.iUsuarioId;
            vobjCambio.iAplicacionId = pobjSesion.iAplicacionId;
            vobjCambio.iCamRegistro = piCategoriaId;
            vobjCambio.iAccionId = AccionConstant.Categoria_Desactivacion;

            // Ejecutamos transacción
            using (Conexion vobjConexion = ConexionFactory.Instanciar())
            {
                vobjConexion.BeginTransaction();
                await _objCategoriaRepository.Editar(vobjConexion, vobjCategoria);
                await _objCambioRepository.Crear(vobjConexion, vobjCambio);
                vobjConexion.Commit();
            }

            return (await _objCategoriaRepository.Consultar(piCategoriaId: piCategoriaId)).FirstOrDefault();
        }
    }
}
