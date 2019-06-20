using iThinking.Manager.Identity;
using iThinking.UserCenter.IdentityManager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Repository.Pattern.UnitOfWork;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Infrastructure.Core
{
    public class BaseController : Controller
    {
        protected IUnitOfWorkAsync _unitOfWorkAsync;

        #region Identity

        protected ApplicationUserManager _applicationUserManager;
        protected ApplicationGroupManager _applicationGroupManager;
        protected ApplicationRoleManager _applicationRoleManager;
        protected ApplicationProjectManager _applicationProjectManager;
        protected ApplicationErrorManager _applicationErrorManager;
        protected ApplicationGroupRoleManager _applicationGroupRoleManager;
        protected ApplicationUserGroupManager _applicationUserGroupManager;

        #endregion Identity

        public BaseController(IUnitOfWorkAsync unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region Initialize

        protected IdentitySignInManager SignInManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IdentitySignInManager>();
            }
        }

        protected IdentityUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IdentityUserManager>();
            }
        }

        protected IdentityRoleManager RoleManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IdentityRoleManager>();
            }
        }

        protected IdentityGroupManager GroupManager
        {
            get
            {
                return new IdentityGroupManager();
            }
        }

        #endregion Initialize

        #region Helpers

        // Used for XSRF protection when adding external logins
        public const string XsrfKey = "XsrfId";

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helpers
    }
}