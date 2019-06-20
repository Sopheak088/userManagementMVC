using AutoMapper;
using iThinking.Manager.Identity;
using iThinking.Mapper.Identity;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMvc.Infrastructure.Core;

namespace WebMvc.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        public UserController(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;

            _applicationUserManager = new ApplicationUserManager(unitOfWorkAsync);
            _applicationGroupManager = new ApplicationGroupManager(unitOfWorkAsync);
            _applicationUserGroupManager = new ApplicationUserGroupManager(unitOfWorkAsync);
        }

        #region Index

        [Authorize(Roles = "Admin, UserCenter.UserView")]
        public ActionResult Index()
        {
            ApplicationUserIndexViewModel _applicationUserIndexViewModel = new ApplicationUserIndexViewModel();
            _applicationUserIndexViewModel.ApplicationUsers = _applicationUserManager.GetAll(_applicationUserIndexViewModel).ToList();

            return View(_applicationUserIndexViewModel);
        }

        [Authorize(Roles = "Admin, UserCenter.UserView")]
        [HttpPost]
        public ActionResult Index(ApplicationUserIndexViewModel applicationUserIndexViewModel)
        {
            applicationUserIndexViewModel.ApplicationUsers = _applicationUserManager.GetAll(applicationUserIndexViewModel).ToList();

            return View(applicationUserIndexViewModel);
        }

        #endregion Index

        #region DoesUserNameExist

        public bool IsUserNameExist(string UserName)
        {
            var _user = _applicationUserManager.FindByUserName(UserName);

            if (_user != null)
                return true;
            else
                return false;
        }

        #endregion DoesUserNameExist

        #region Create

        [Authorize(Roles = "Admin, UserCenter.UserCreate")]
        public ActionResult Create()
        {
            UserCreateViewModel _userCreateViewModel = new UserCreateViewModel();
            _userCreateViewModel.Password = "Bidc@123";
            _userCreateViewModel.ConfirmPassword = "Bidc@123";

            // Show a list of available groups:
            var _groupsAll = _applicationGroupManager.GetAll();
            ViewBag.GroupsList = new SelectList(_groupsAll.ToList(), "Id", "Name");
            ViewBag.Groups = _groupsAll.ToList();
            ViewBag.SelectedGroups = null;

            return View(_userCreateViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, UserCenter.UserCreate")]
        public async Task<ActionResult> Create(UserCreateViewModel userCreateViewModel, params string[] selectedGroups)
        {
            if (ModelState.IsValid)
            {
                if (IsUserNameExist(userCreateViewModel.UserName))
                {
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                }
                else
                {
                    var user = new ApplicationUser();
                    user.UpdateApplicationUser(userCreateViewModel);
                    user.New(User.Identity.Name);

                    var adminresult = await UserManager.CreateAsync(user, userCreateViewModel.Password);

                    //Add User to the selected Groups
                    if (adminresult.Succeeded)
                    {
                        if (selectedGroups != null)
                        {
                            selectedGroups = selectedGroups ?? new string[] { };
                            await this.GroupManager.SetUserGroupsAsync(user.Id, selectedGroups);
                        }
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", adminresult.Errors.First().ToString());
                }
            }

            var _groupsAll = _applicationGroupManager.GetAll();
            ViewBag.GroupsList = new SelectList(_groupsAll.ToList(), "Id", "Name");
            ViewBag.Groups = _groupsAll.ToList();
            ViewBag.SelectedGroups = selectedGroups != null ? selectedGroups.ToList() : null;

            return View(userCreateViewModel);
        }

        #endregion Create

        #region Edit

        [Authorize(Roles = "Admin, UserCenter.UserEdit")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Display a list of available Groups:
            var allGroups = this.GroupManager.Groups;
            var userGroups = await this.GroupManager.GetUserGroupsAsync(id);
            EditUserViewModel model = Mapper.Map<ApplicationUser, EditUserViewModel>(user);

            var _groupsAll = _applicationGroupManager.GetAll();
            ViewBag.GroupsList = new SelectList(_groupsAll.ToList(), "Id", "Name");
            ViewBag.Groups = _groupsAll.ToList();
            ViewBag.SelectedGroups = _applicationUserGroupManager.GetByUserId(id).Select(m => m.ApplicationGroupId).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.UserEdit")]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, params string[] selectedGroups)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                // Update the User:
                user.UpdateApplicationUser(editUser);
                user.Update(User.Identity.Name);

                await this.UserManager.UpdateAsync(user);

                // Update the Groups:
                selectedGroups = selectedGroups ?? new string[] { };
                //await this.GroupManager.SetUserGroupsAsync(user.Id, selectedGroups);
                _applicationGroupManager.SetUserGroups(user.Id, selectedGroups);

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");

            var _groupsAll = _applicationGroupManager.GetAll();
            ViewBag.GroupsList = new SelectList(_groupsAll.ToList(), "Id", "Name");
            ViewBag.Groups = _groupsAll.ToList();
            ViewBag.SelectedGroups = selectedGroups.ToList();

            return View(editUser);
        }

        #endregion Edit

        #region Details

        [Authorize(Roles = "Admin, UserCenter.UserView")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Display a list of available Groups:
            var allGroups = this.GroupManager.Groups;
            var userGroups = await this.GroupManager.GetUserGroupsAsync(id);

            UserDetailViewModel model = Mapper.Map<ApplicationUser, UserDetailViewModel>(user);

            foreach (var group in allGroups)
            {
                var listItem = new SelectListItem()
                {
                    Text = group.Name,
                    Value = group.Id,
                    Selected = userGroups.Any(g => g.Id == group.Id)
                };
                model.GroupsList.Add(listItem);
            }
            return View(model);
        }

        #endregion Details

        #region Delete

        [Authorize(Roles = "Admin, UserCenter.UserDelete")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            await UserManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }

        #endregion Delete

        #region ChangeStatus

        [Authorize(Roles = "Admin, UserCenter.UserChangeStatus")]
        public async Task<ActionResult> ChangeStatus(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.IsApproved = !user.IsApproved;
            await UserManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        #endregion ChangeStatus

        #region ResetPassword

        [Authorize(Roles = "Admin, UserCenter.UserResetPassword")]
        public async Task<ActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Id = user.Id.ToString();
            model.UserName = user.UserName;

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, UserCenter.UserResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(model.Id);

            await UserManager.RemovePasswordAsync(user.Id);
            var result = await UserManager.AddPasswordAsync(user.Id, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            AddErrors(result);

            return View();
        }

        #endregion ResetPassword
    }
}