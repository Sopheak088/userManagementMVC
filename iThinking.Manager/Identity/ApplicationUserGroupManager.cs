using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserGroupManager : ObjectManager
    {
        public ApplicationUserGroupManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUserGroup> GetAll()
        {
            var _applicationUserGroups = _unitOfWorkAsync.Repository<ApplicationUserGroup>().Query()
                .Include(m => m.ApplicationGroup)
                .Include(m => m.ApplicationUser)
                .Select().AsEnumerable();
            return _applicationUserGroups;
        }

        public void Insert(ApplicationUserGroup applicationUserGroup)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroup>().Insert(applicationUserGroup);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUserGroup> applicationUserGroups)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroup>().InsertRange(applicationUserGroups);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUserGroup applicationUserGroup)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroup>().Update(applicationUserGroup);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUserGroup applicationUserGroup)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroup>().Delete(applicationUserGroup);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUserGroup> applicationUserGroups)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroup>().DeleteRange(applicationUserGroups);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationUserGroup> GetByGroupId(string groupId)
        {
            return GetAll().Where(m => m.ApplicationGroupId == groupId);
        }

        public void DeleteByGroupId(string groupId)
        {
            var _userGroups = GetByGroupId(groupId);
            DeleteRange(_userGroups);
        }

        public IEnumerable<ApplicationUserGroup> GetByUserId(string userId)
        {
            return GetAll().Where(m => m.ApplicationUserId == userId);
        }

        public void DeleteByUserId(string userId)
        {
            var _userGroups = GetByUserId(userId);
            DeleteRange(_userGroups);
        }

        public IEnumerable<ApplicationUserGroup> GetAll(string keyword)
        {
            var _applicationUserGroups = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUserGroups.Where(m => m.ApplicationGroupId.Contains(keyword));
            else
                return _applicationUserGroups;
        }

        public int GetCount(string projectId)
        {
            return _unitOfWorkAsync.Repository<ApplicationUserGroup>().Query()
                .Include(m => m.ApplicationGroup)
                .Include(m => m.ApplicationUser)
                .Select().Where(m => m.ApplicationGroup.ApplicationProjectId == projectId).ToList().GroupBy(m => m.ApplicationUserId).Select(m => m.First()).Count();
        }
    }
}