using iThinking.Manager.Identity;
using Repository.Pattern.UnitOfWork;
using System.Linq;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class ErrorController : BaseController
    {
        public ErrorController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;

            _applicationErrorManager = new ApplicationErrorManager(unitOfWorkAsync);
        }

        #region Index

        [Authorize(Roles = "Admin, UserCenter.ErrorView")]
        public ActionResult Index()
        {
            return View(_applicationErrorManager.GetAll().ToList());
        }

        #endregion Index
    }
}