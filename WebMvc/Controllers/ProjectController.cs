using AutoMapper;
using iThinking.Manager.Identity;
using iThinking.Mapper.Identity;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        public ProjectController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;

            _applicationProjectManager = new ApplicationProjectManager(unitOfWorkAsync);
        }

        #region Index

        [Authorize(Roles = "Admin, UserCenter.ProjectView")]
        public ActionResult Index()
        {
            ApplicationProjectIndexViewModel _applicationProjectIndexViewModel = new ApplicationProjectIndexViewModel();
            _applicationProjectIndexViewModel.ApplicationProjects = _applicationProjectManager.GetAll(_applicationProjectIndexViewModel).ToList();

            return View(_applicationProjectIndexViewModel);
        }

        [Authorize(Roles = "Admin, UserCenter.ProjectView")]
        [HttpPost]
        public ActionResult Index(ApplicationProjectIndexViewModel applicationProjectIndexViewModel)
        {
            applicationProjectIndexViewModel.ApplicationProjects = _applicationProjectManager.GetAll(applicationProjectIndexViewModel).ToList();

            return View(applicationProjectIndexViewModel);
        }

        #endregion Index

        #region Create

        [Authorize(Roles = "Admin, UserCenter.ProjectCreate")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.ProjectCreate")]
        public ActionResult Create(ApplicationProjectCreateViewModel applicationProjectCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationProject _project = new ApplicationProject();
                _project.UpdateApplicationProject(applicationProjectCreateViewModel);
                _project.New(User.Identity.Name);

                _applicationProjectManager.Insert(_project);

                return RedirectToAction("Index");
            }

            return View(applicationProjectCreateViewModel);
        }

        #endregion Create

        #region Details

        [Authorize(Roles = "Admin, UserCenter.ProjectView")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _project = _applicationProjectManager.Find(id);

            if (_project == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationProjectViewModel _appProjectViewModel = Mapper.Map<ApplicationProject, ApplicationProjectViewModel>(_project);

            return View(_appProjectViewModel);
        }

        #endregion Details

        #region Edit

        [Authorize(Roles = "Admin, UserCenter.ProjectEdit")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _project = _applicationProjectManager.Find(id);
            if (_project == null)
            {
                return HttpNotFound();
            }

            ApplicationProjectEditViewModel appProjectModel = Mapper.Map<ApplicationProject, ApplicationProjectEditViewModel>(_project);

            return View(appProjectModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.ProjectEdit")]
        public ActionResult Edit(ApplicationProjectEditViewModel appProjectModel)
        {
            if (ModelState.IsValid)
            {
                var _project = _applicationProjectManager.Find(appProjectModel.Id);
                _project.UpdateApplicationProject(appProjectModel);
                _project.Update(User.Identity.Name);

                _applicationProjectManager.Update(_project);

                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion Edit

        #region Delete

        [Authorize(Roles = "Admin, UserCenter.ProjectDelete")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationProject applicationProject = _applicationProjectManager.Find(id);
            if (applicationProject == null)
            {
                return HttpNotFound();
            }
            _applicationProjectManager.Delete(applicationProject);

            return RedirectToAction("Index");
        }

        #endregion Delete
    }
}