using AutoMapper;
using iThinking.Manager.Identity;
using iThinking.Mapper.Identity;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Microsoft.AspNet.Identity;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        public RoleController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;

            _applicationProjectManager = new ApplicationProjectManager(unitOfWorkAsync);
            _applicationRoleManager = new ApplicationRoleManager(unitOfWorkAsync);
        }

        #region Index

        [Authorize(Roles = "Admin, UserCenter.RoleView")]
        public ActionResult Index()
        {
            ApplicationRoleIndexViewModel _applicationRoleIndexViewModel = new ApplicationRoleIndexViewModel();
            _applicationRoleIndexViewModel.ApplicationRoles = _applicationRoleManager.GetAll(_applicationRoleIndexViewModel).ToList();
            
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll().ToList(), "Id", "Name");

            return View(_applicationRoleIndexViewModel);
        }

        [Authorize(Roles = "Admin, UserCenter.RoleView")]
        [HttpPost]
        public ActionResult Index(ApplicationRoleIndexViewModel applicationRoleIndexViewModel)
        {
            applicationRoleIndexViewModel.ApplicationRoles = _applicationRoleManager.GetAll(applicationRoleIndexViewModel).ToList();

            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll().ToList(), "Id", "Name");

            return View(applicationRoleIndexViewModel);
        }

        #endregion Index

        #region Details

        [Authorize(Roles = "Admin, UserCenter.RoleView")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _role = await RoleManager.FindByIdAsync(id);
            if (_role == null)
            {
                return HttpNotFound();
            }
            ApplicationRoleViewModel _appRoleViewModel = Mapper.Map<ApplicationRole, ApplicationRoleViewModel>(_role);

            return View(_appRoleViewModel);
        }

        #endregion Details

        #region Create

        [Authorize(Roles = "Admin, UserCenter.RoleCreate")]
        public ActionResult Create()
        {
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, UserCenter.RoleCreate")]
        public async Task<ActionResult> Create(ApplicationRoleCreateViewModel appRoleCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole();
                role.UpdateApplicationRole(appRoleCreateViewModel);

                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", appRoleCreateViewModel.ApplicationProjectId);
            return View();
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Admin, UserCenter.RoleEdit")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var _role = await RoleManager.FindByIdAsync(id);
            if (_role == null)
            {
                return HttpNotFound();
            }
            ApplicationRoleEditViewModel _appRoleViewModel = Mapper.Map<ApplicationRole, ApplicationRoleEditViewModel>(_role);
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", _appRoleViewModel.ApplicationProjectId);
            return View(_appRoleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.RoleEdit")]
        public async Task<ActionResult> Edit(ApplicationRoleEditViewModel appRoleEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await RoleManager.FindByIdAsync(appRoleEditViewModel.Id);
                role.UpdateApplicationRole(appRoleEditViewModel);

                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", appRoleEditViewModel.ApplicationProjectId);
            return View();
        }

        #endregion Edit

        #region Delete

        [Authorize(Roles = "Admin, UserCenter.RoleDelete")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = RoleManager.FindById(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            RoleManager.Delete(applicationRole);

            return RedirectToAction("Index");
        }

        #endregion Delete
    }
}