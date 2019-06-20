using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationGroupManager : ObjectManager
    {
        public ApplicationGroupManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationGroup>().Query()
                .Include(m => m.ApplicationProject).Select().AsEnumerable();
        }

        public IEnumerable<ApplicationGroup> GetAll(ApplicationGroupIndexViewModel applicationGroupSeachViewModel)
        {
            IEnumerable<ApplicationGroup> _applicationGroups = GetAll();
            if (!string.IsNullOrEmpty(applicationGroupSeachViewModel.Keyword))
            {
                _applicationGroups = _applicationGroups.Where(m =>
                    (!string.IsNullOrEmpty(m.Name) && m.Name.Contains(applicationGroupSeachViewModel.Keyword)) ||
                    (!string.IsNullOrEmpty(m.Description) && m.Description.Contains(applicationGroupSeachViewModel.Keyword))
                    );
            }
            if (!string.IsNullOrEmpty(applicationGroupSeachViewModel.ApplicationProjectId))
            {
                _applicationGroups = _applicationGroups.Where(m => m.ApplicationProjectId == applicationGroupSeachViewModel.ApplicationProjectId).AsQueryable();
            }

            return _applicationGroups;
        }

        public void Insert(ApplicationGroup applicationGroup)
        {
            _unitOfWorkAsync.Repository<ApplicationGroup>().Insert(applicationGroup);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationGroup> applicationGroups)
        {
            _unitOfWorkAsync.Repository<ApplicationGroup>().InsertRange(applicationGroups);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationGroup applicationGroup)
        {
            _unitOfWorkAsync.Repository<ApplicationGroup>().Update(applicationGroup);
            _unitOfWorkAsync.SaveChanges();
        }

        public void SetGroupRoleIds(string groupId, params string[] roleIds)
        {
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(_unitOfWorkAsync);
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);

            //Delete old GroupRole
            _applicationGroupRoleManager.DeleteByGroupId(groupId);

            //add new GroupRole
            foreach (var roleId in roleIds)
            {
                _applicationGroupRoleManager.Insert(new ApplicationGroupRole { ApplicationGroupId = groupId, ApplicationRoleId = roleId });
            }

            //Refresh UserGroup and UserRole
            List<string> _userIds = _applicationUserGroupManager.GetByGroupId(groupId).Select(m => m.ApplicationUserId).ToList();
            foreach (var userId in _userIds)
            {
                RefreshUserGroupRoles(userId);
            }
        }

        public void SetGroupRoleNames(string groupId, params string[] roleNames)
        {
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(_unitOfWorkAsync);
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);
            _applicationRoleManager = new ApplicationRoleManager(_unitOfWorkAsync);

            // Clear all the roles associated with this group:
            var thisGroup = Find(groupId);
            //Delete old GroupRole
            _applicationGroupRoleManager.DeleteByGroupId(groupId);

            //add new GroupRole
            var newRoles = _applicationRoleManager.GetAll().Where(r => roleNames.Any(n => n == r.Name));
            foreach (var role in newRoles)
            {
                _applicationGroupRoleManager.Insert(new ApplicationGroupRole { ApplicationGroupId = groupId, ApplicationRoleId = role.Id });
            }

            //Refresh UserGroup and UserRole
            List<string> _userIds = _applicationUserGroupManager.GetByGroupId(groupId).Select(m => m.ApplicationUserId).ToList();
            foreach (var userId in _userIds)
            {
                RefreshUserGroupRoles(userId);
            }
        }

        public void Delete(string applicationGroupId)
        {
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(_unitOfWorkAsync);
            _applicationUserGroupChangeManager = new ApplicationUserGroupChangeManager(_unitOfWorkAsync);
            _applicationUserGroupHistoryManager = new ApplicationUserGroupHistoryManager(_unitOfWorkAsync);

            ApplicationGroup _applicationGroup = Find(applicationGroupId);
            List<string> _applicationUserGroupIds = _applicationUserGroupManager.GetByGroupId(_applicationGroup.Id).Select(m => m.ApplicationUserId).ToList();

            _applicationGroupRoleManager.DeleteByGroupId(_applicationGroup.Id);
            _applicationUserGroupManager.DeleteByGroupId(_applicationGroup.Id);
            _applicationUserGroupChangeManager.DeleteByGroupId(_applicationGroup.Id);
            _applicationUserGroupHistoryManager.DeleteByGroupId(_applicationGroup.Id);

            _unitOfWorkAsync.Repository<ApplicationGroup>().Delete(_applicationGroup);
            _unitOfWorkAsync.SaveChanges();

            foreach (var applicationUserGroupId in _applicationUserGroupIds)
            {
                RefreshUserGroupRoles(applicationUserGroupId);
            }
        }

        public void Delete(ApplicationGroup applicationGroup)
        {
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(_unitOfWorkAsync);
            _applicationUserGroupChangeManager = new ApplicationUserGroupChangeManager(_unitOfWorkAsync);
            _applicationUserGroupHistoryManager = new ApplicationUserGroupHistoryManager(_unitOfWorkAsync);

            List<string> _applicationUserGroupIds = _applicationUserGroupManager.GetByGroupId(applicationGroup.Id).Select(m => m.ApplicationUserId).ToList();

            _applicationGroupRoleManager.DeleteByGroupId(applicationGroup.Id);
            _applicationUserGroupManager.DeleteByGroupId(applicationGroup.Id);
            _applicationUserGroupChangeManager.DeleteByGroupId(applicationGroup.Id);
            _applicationUserGroupHistoryManager.DeleteByGroupId(applicationGroup.Id);

            _unitOfWorkAsync.Repository<ApplicationGroup>().Delete(applicationGroup);
            _unitOfWorkAsync.SaveChanges();

            foreach (var applicationUserGroupId in _applicationUserGroupIds)
            {
                RefreshUserGroupRoles(applicationUserGroupId);
            }
        }

        public void DeleteRange(IEnumerable<ApplicationGroup> applicationGroups)
        {
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);

            var _applicationUserGroups = new List<ApplicationUserGroup>();
            foreach (var applicationGroup in applicationGroups)
            {
                _applicationUserGroups.AddRange(_applicationUserGroupManager.GetByGroupId(applicationGroup.Id));
            }

            _unitOfWorkAsync.Repository<ApplicationGroup>().DeleteRange(applicationGroups);
            _unitOfWorkAsync.SaveChanges();

            foreach (var applicationUserGroup in _applicationUserGroups)
            {
                RefreshUserGroupRoles(applicationUserGroup.ApplicationUserId);
            }
        }

        public IEnumerable<ApplicationGroup> GetAll(string keyword)
        {
            var _applicationGroups = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationGroups.Where(m => m.Description.Contains(keyword));
            else
                return _applicationGroups;
        }

        public ApplicationGroup Find(string id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }

        public void AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);

            _applicationUserGroupManager.DeleteByUserId(userId);
            _applicationUserGroupManager.InsertRange(userGroups);
        }

        public void RefreshUserGroupRoles(string userId)
        {
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);
            _applicationUserManager = new ApplicationUserManager(_unitOfWorkAsync);
            _applicationUserRoleManager = new ApplicationUserRoleManager(_unitOfWorkAsync);
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(_unitOfWorkAsync);

            var user = _applicationUserManager.Find(userId);
            if (user == null)
            {
                throw new ArgumentNullException("User");
            }
            // Remove user from previous roles:
            var _oldUserRoles = _applicationUserRoleManager.GetByUserId(userId);
            _applicationUserRoleManager.DeleteRange(_oldUserRoles);

            // Find teh roles this user is entitled to from group membership:
            var _newGroups = _applicationUserGroupManager.GetByUserId(userId).Select(m => m.ApplicationGroupId).ToArray();

            // Get the damn role:
            var _groupRoles = _applicationGroupRoleManager.GetByGroupIds(_newGroups);

            // Add the user to the proper roles
            foreach (var groupRole in _groupRoles)
            {
                _applicationUserRoleManager.Insert(new ApplicationUserRole { RoleId = groupRole.ApplicationRoleId, UserId = userId });
            }
        }

        public void SetUserGroups(string userId, params string[] groupIds)
        {
            _applicationUserGroupManager = new ApplicationUserGroupManager(_unitOfWorkAsync);

            // Clear current group membership:
            var _currentUserGroups = _applicationUserGroupManager.GetByUserId(userId);
            _applicationUserGroupManager.DeleteRange(_currentUserGroups);

            // Add the user to the new groups:
            foreach (string groupId in groupIds)
            {
                _applicationUserGroupManager.Insert(new ApplicationUserGroup { ApplicationGroupId = groupId, ApplicationUserId = userId });
            }

            RefreshUserGroupRoles(userId);
        }
    }
}