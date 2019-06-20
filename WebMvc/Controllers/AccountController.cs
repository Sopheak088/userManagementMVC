using iThinking.Manager.Identity;
using iThinking.ViewModel.Identity;
using Microsoft.AspNet.Identity.Owin;
using Repository.Pattern.UnitOfWork;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;

            _applicationUserManager = new ApplicationUserManager(unitOfWorkAsync);
        }

        #region Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_applicationUserManager.IsCanLogin(model.UserName))
            {
                // This doen't count login failures towards lockout only two factor authentication
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);

                    case SignInStatus.LockedOut:
                        return View("Lockout");

                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });

                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Wrong username or password.");
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Your account isn't ready yet.");
                return View(model);
            }
        }

        #endregion Login

        #region LogOut

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #endregion LogOut

        #region ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion ExternalLoginFailure
    }
}