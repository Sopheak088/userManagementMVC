using iThinking.Manager.Identity;
using iThinking.ViewModel;
using Repository.Pattern.UnitOfWork;
using System.Linq;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _applicationErrorManager = new ApplicationErrorManager(unitOfWorkAsync);
        }

        #region Index

        [Authorize]
        public ActionResult Index()
        {
            DashboardIndexViewModel _dashboardIndexViewModel = new DashboardIndexViewModel();
            _dashboardIndexViewModel.UserCount = UserManager.Users.Count();
            _dashboardIndexViewModel.GroupCount = 0;//GroupManager.Groups.Count();
            _dashboardIndexViewModel.RoleCount = RoleManager.Roles.Count();
            _dashboardIndexViewModel.ErrorCount = _applicationErrorManager.GetAll().Count();

            return View(_dashboardIndexViewModel);
        }

        #endregion Index
    }
}