using iThinking.UserCenter;
using iThinking.UserCenter.Identity;
using iThinking.UserCenter.IdentityManager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using System;

namespace WebMvc.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion Unity Container

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container
                //UnitOfWork, DbContext, Repository
                .RegisterType<IDataContext, DataContext>()
                .RegisterType<IDataContextAsync, DataContext>()
                .RegisterType<IUnitOfWork, UnitOfWork>()
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>()
                .RegisterType<IDataContext, UserCenterDbContext>()
                .RegisterType<IDataContextAsync, UserCenterDbContext>()

                //Identity
                .RegisterType<IUserStore<IdentityUser>, UserStore<IdentityUser>>()
                .RegisterType<UserManager<ApplicationUser, string>>()
                .RegisterType<RoleManager<ApplicationRole>>()
                .RegisterType<IdentityUserManager>()
                .RegisterType<IdentityRoleManager>()
                .RegisterType<IdentitySignInManager>()
                .RegisterType<IdentityGroupManager>();
        }
    }
}