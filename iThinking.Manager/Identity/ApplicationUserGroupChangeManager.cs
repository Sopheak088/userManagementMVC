using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserGroupChangeManager : ObjectManager
    {
        public ApplicationUserGroupChangeManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUserGroupChange> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationUserGroupChange>().Query()
                .Include(m => m.ApplicationGroup)
                .Include(m => m.ApplicationUserChange).Select().AsEnumerable();
        }

        public void Insert(ApplicationUserGroupChange applicationUserGroupChange)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupChange>().Insert(applicationUserGroupChange);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUserGroupChange> applicationUserGroupChanges)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupChange>().InsertRange(applicationUserGroupChanges);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUserGroupChange applicationUserGroupChange)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupChange>().Update(applicationUserGroupChange);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUserGroupChange applicationUserGroupChange)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupChange>().Delete(applicationUserGroupChange);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUserGroupChange> applicationUserGroupChanges)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupChange>().DeleteRange(applicationUserGroupChanges);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteByGroupId(string groupId)
        {
            var _groupRoles = GetByGroupId(groupId);
            DeleteRange(_groupRoles);
        }

        public IEnumerable<ApplicationUserGroupChange> GetAll(string keyword)
        {
            var _applicationUserGroupChanges = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUserGroupChanges.Where(m => m.ApplicationGroupId.Contains(keyword));
            else
                return _applicationUserGroupChanges;
        }

        public IEnumerable<ApplicationUserGroupChange> GetByGroupId(string groupId)
        {
            return GetAll().Where(m => m.ApplicationGroupId == groupId);
        }

        public IEnumerable<ApplicationUserGroupChange> GetByUserChangeId(Guid userChangeId)
        {
            return GetAll().Where(m => m.ApplicationUserChangeId == userChangeId);
        }
    }
}