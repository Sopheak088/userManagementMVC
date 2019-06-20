using iThinking.UserCenter.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace iThinking.UserCenter.IdentityManager
{
    public class IdentityRoleManager : RoleManager<ApplicationRole>
    {
        public IdentityRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static IdentityRoleManager Create(IdentityFactoryOptions<IdentityRoleManager> options, IOwinContext context)
        {
            return new IdentityRoleManager(new ApplicationRoleStore(context.Get<UserCenterDbContext>()));
        }
    }
}