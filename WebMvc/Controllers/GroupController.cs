using AutoMapper;
using iThinking.Manager.Identity;
using iThinking.Mapper.Identity;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class GroupController : BaseController
    {
        public GroupController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;

            _applicationProjectManager = new ApplicationProjectManager(unitOfWorkAsync);
            _applicationGroupManager = new ApplicationGroupManager(unitOfWorkAsync);
            _applicationRoleManager = new ApplicationRoleManager(unitOfWorkAsync);
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(unitOfWorkAsync);
        }

        #region Index

        [Authorize(Roles = "Admin, UserCenter.GroupView")]
        public ActionResult Index()
        {
            ApplicationGroupIndexViewModel _applicationGroupIndexViewModel = new ApplicationGroupIndexViewModel();
            _applicationGroupIndexViewModel.ApplicationGroups = _applicationGroupManager.GetAll(_applicationGroupIndexViewModel).ToList();

            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll().ToList(), "Id", "Name");

            return View(_applicationGroupIndexViewModel);
        }

        [Authorize(Roles = "Admin, UserCenter.GroupView")]
        [HttpPost]
        public ActionResult Index(ApplicationGroupIndexViewModel applicationGroupIndexViewModel)
        {
            applicationGroupIndexViewModel.ApplicationGroups = _applicationGroupManager.GetAll(applicationGroupIndexViewModel).ToList();

            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll().ToList(), "Id", "Name");

            return View(applicationGroupIndexViewModel);
        }

        #endregion Index

        #region Details

        [Authorize(Roles = "Admin, UserCenter.GroupView")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationGroup applicationgroup = await this.GroupManager.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (applicationgroup == null)
            {
                return HttpNotFound();
            }
            var groupRoles = this.GroupManager.GetGroupRoles(applicationgroup.Id);
            string[] RoleNames = groupRoles.Select(p => p.Name).ToArray();
            ViewBag.RolesList = RoleNames;
            ViewBag.RolesCount = RoleNames.Count();

            ApplicationGroupViewModel _applicationGroupViewModel = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(applicationgroup);

            var _rolesAll = new List<ApplicationRole>();
            if (!string.IsNullOrEmpty(applicationgroup.ApplicationProjectId))
            {
                _rolesAll = _applicationRoleManager.GetByProjectId(applicationgroup.ApplicationProjectId).OrderBy(m => m.ApplicationProjectId).ToList();
            }
            ViewBag.RolesList = new SelectList(_rolesAll.ToList(), "Id", "Name");
            ViewBag.Roles = _rolesAll.ToList();
            ViewBag.SelectedRoles = _applicationGroupRoleManager.GetByGroupId(id).Select(m => m.ApplicationRoleId).ToList();
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", applicationgroup.ApplicationProjectId);

            return View(_applicationGroupViewModel);
        }

        #endregion Details

        #region Create

        [Authorize(Roles = "Admin, UserCenter.GroupCreate")]
        public ActionResult Create(string projectId)
        {
            ApplicationGroupCreateViewModel _applicationGroupCreateViewModel = new ApplicationGroupCreateViewModel();
            var _rolesAll = new List<ApplicationRole>();

            if (!string.IsNullOrEmpty(projectId))
            {
                _rolesAll = _applicationRoleManager.GetByProjectId(projectId).OrderBy(m => m.ApplicationProjectId).ToList();

                _applicationGroupCreateViewModel.ApplicationProjectId = projectId;
            }

            ViewBag.RolesList = new SelectList(_rolesAll, "Id", "Name");
            ViewBag.Roles = _rolesAll;
            ViewBag.SelectedRoles = null;
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", projectId);

            return View(_applicationGroupCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.GroupCreate")]
        public async Task<ActionResult> Create(ApplicationGroupCreateViewModel applicationGroupCreateViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var group = new ApplicationGroup();
                group.UpdateApplicationGroup(applicationGroupCreateViewModel);

                try
                {
                    // Create the new Group:
                    _applicationGroupManager.Insert(group);

                    selectedRoles = selectedRoles ?? new string[] { };

                    // Add the roles selected:
                    await this.GroupManager.SetGroupRoleIdsAsync(group.Id, selectedRoles);
                    //_applicationGroupManager.SetGroupRoleIds(group.Id, selectedRoles);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Create group error: " + ex.Message);

                    _applicationGroupManager.Delete(group.Id);
                }
            }

            // Otherwise, start over:
            var _rolesAll = new List<ApplicationRole>();
            if (!string.IsNullOrEmpty(applicationGroupCreateViewModel.ApplicationProjectId))
            {
                _rolesAll = _applicationRoleManager.GetByProjectId(applicationGroupCreateViewModel.ApplicationProjectId).OrderBy(m => m.ApplicationProjectId).ToList();
            }
            ViewBag.RolesList = new SelectList(_rolesAll.ToList(), "Id", "Name");
            ViewBag.Roles = _rolesAll.ToList();
            ViewBag.SelectedRoles = selectedRoles != null ? selectedRoles.ToList() : null;
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", applicationGroupCreateViewModel.ApplicationProjectId);

            return View(applicationGroupCreateViewModel);
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Admin, UserCenter.GroupEdit")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationGroup applicationgroup = await this.GroupManager.FindByIdAsync(id);
            if (applicationgroup == null)
            {
                return HttpNotFound();
            }

            // Get a list, not a DbSet or queryable:
            var allRoles = await this.RoleManager.Roles.ToListAsync();
            var groupRoles = this.GroupManager.GetGroupRoles(id);

            ApplicationGroupEditViewModel _applicationGroupEditViewModel = Mapper.Map<ApplicationGroup, ApplicationGroupEditViewModel>(applicationgroup);
            _applicationGroupEditViewModel.ApplicationProject = _applicationProjectManager.Find(applicationgroup.ApplicationProjectId);

            // load the roles/Roles for selection in the form:
            var _rolesAll = new List<ApplicationRole>();
            if (!string.IsNullOrEmpty(applicationgroup.ApplicationProjectId))
            {
                _rolesAll = _applicationRoleManager.GetByProjectId(applicationgroup.ApplicationProjectId).OrderBy(m => m.ApplicationProjectId).ToList();
            }
            ViewBag.RolesList = new SelectList(_rolesAll.ToList(), "Id", "Name");
            ViewBag.Roles = _rolesAll.ToList();
            ViewBag.SelectedRoles = _applicationGroupRoleManager.GetByGroupId(id).Select(m => m.ApplicationRoleId).ToList();
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", applicationgroup.ApplicationProjectId);

            return View(_applicationGroupEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.GroupEdit")]
        public async Task<ActionResult> Edit(ApplicationGroupEditViewModel applicationGroupEditViewModel, params string[] selectedRoles)
        {
            var group = await this.GroupManager.FindByIdAsync(applicationGroupEditViewModel.Id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                group.Name = applicationGroupEditViewModel.Name;
                group.Description = applicationGroupEditViewModel.Description;
                await this.GroupManager.UpdateGroupAsync(group);

                selectedRoles = selectedRoles ?? new string[] { };
                //await this.GroupManager.SetGroupRoleIdsAsync(group.Id, selectedRoles);
                _applicationGroupManager.SetGroupRoleIds(group.Id, selectedRoles);

                return RedirectToAction("Index");
            }

            var _rolesAll = new List<ApplicationRole>();
            if (!string.IsNullOrEmpty(group.ApplicationProjectId))
            {
                _rolesAll = _applicationRoleManager.GetByProjectId(group.ApplicationProjectId).OrderBy(m => m.ApplicationProjectId).ToList();
            }
            ViewBag.RolesList = new SelectList(_rolesAll.ToList(), "Id", "Name");
            ViewBag.Roles = _rolesAll.ToList();
            ViewBag.SelectedRoles = selectedRoles.ToList();
            ViewBag.ApplicationProjectId = new SelectList(_applicationProjectManager.GetAll(), "Id", "Name", group.ApplicationProjectId);

            return View(applicationGroupEditViewModel);
        }

        #endregion Edit

        #region Delete

        [Authorize(Roles = "Admin, UserCenter.GroupDelete")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationGroup applicationgroup = await this.GroupManager.FindByIdAsync(id);
            if (applicationgroup == null)
            {
                return HttpNotFound();
            }
            GroupManager.DeleteGroup(id);

            return RedirectToAction("Index");
        }

        #endregion Delete
    }
}