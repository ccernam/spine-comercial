using Spine.Repositories.Implementations.Aud;
using Spine.Repositories.Implementations.Cfg;
using Spine.Repositories.Implementations.Cmn;
using Spine.Repositories.Implementations.Seg;
using Spine.Repositories.Interfaces.Aud;
using Spine.Repositories.Interfaces.Cfg;
using Spine.Repositories.Interfaces.Cmn;
using Spine.Repositories.Interfaces.Seg;
using Spine.Services.Implementations.Aud;
using Spine.Services.Implementations.Cfg;
using Spine.Services.Implementations.Cmn;
using Spine.Services.Implementations.Seg;
using Spine.Services.Interfaces.Aud;
using Spine.Services.Interfaces.Cfg;
using Spine.Services.Interfaces.Cmn;
using Spine.Services.Interfaces.Seg;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace Spine.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }

        public static void RegisterDependencies()
        {
            UnityContainer vobjContainer = new UnityContainer();

            // Services: Aud
            vobjContainer.RegisterSingleton<ICambioService, CambioService>();
            vobjContainer.RegisterSingleton<IErrorService, ErrorService>();
            // Services: Cfg
            vobjContainer.RegisterSingleton<IUbigeoService, UbigeoService>();
            vobjContainer.RegisterSingleton<ICatalogoService, CatalogoService>();
            // Services: Cmn
            vobjContainer.RegisterSingleton<ICategoriaService, CategoriaService>();
            vobjContainer.RegisterSingleton<ISucursalService, SucursalService>();
            vobjContainer.RegisterSingleton<IAlmacenService, AlmacenService>();
            vobjContainer.RegisterSingleton<IUnidadMedidaService, UnidadMedidaService>();
            // Services: Seg
            vobjContainer.RegisterSingleton<IUsuarioService, UsuarioService>();
            vobjContainer.RegisterSingleton<IRolService, RolService>();
            vobjContainer.RegisterSingleton<ISesionService, SesionService>();

            // Repositories: Aud
            vobjContainer.RegisterSingleton<ICambioRepository, CambioRepository>();
            vobjContainer.RegisterSingleton<IErrorRepository, ErrorRepository>();
            // Repositories: Cfg
            vobjContainer.RegisterSingleton<IUbigeoRepository, UbigeoRepository>();
            vobjContainer.RegisterSingleton<ICatalogoRepository, CatalogoRepository>();
            // Repositories: Cmn
            vobjContainer.RegisterSingleton<ICategoriaRepository, CategoriaRepository>();
            vobjContainer.RegisterSingleton<ISucursalRepository, SucursalRepository>();
            vobjContainer.RegisterSingleton<IAlmacenRepository, AlmacenRepository>();
            vobjContainer.RegisterSingleton<IUnidadMedidaRepository, UnidadMedidaRepository>();
            // Repositories: Cfg
            vobjContainer.RegisterSingleton<IAplicacionRepository, AplicacionRepository>();
            // Repositories: Seg
            vobjContainer.RegisterSingleton<ISesionRepository, SesionRepository>();
            vobjContainer.RegisterSingleton<IUsuarioRepository, UsuarioRepository>();
            vobjContainer.RegisterSingleton<IRolRepository, RolRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(vobjContainer));

        }
    }
}