using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserGroupHistoryManager : ObjectManager
    {
        public ApplicationUserGroupHistoryManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUserGroupHistory> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationUserGroupHistory>().Query()
                .Include(m => m.ApplicationUserHistory)
                .Include(m => m.ApplicationGroup).Select().AsEnumerable();
        }

        public void Insert(ApplicationUserGroupHistory applicationUserGroupHistory)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupHistory>().Insert(applicationUserGroupHistory);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUserGroupHistory> applicationUserGroupHistories)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupHistory>().InsertRange(applicationUserGroupHistories);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUserGroupHistory applicationUserGroupHistory)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupHistory>().Update(applicationUserGroupHistory);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUserGroupHistory applicationUserGroupHistory)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupHistory>().Delete(applicationUserGroupHistory);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUserGroupHistory> applicationUserGroupHistories)
        {
            _unitOfWorkAsync.Repository<ApplicationUserGroupHistory>().DeleteRange(applicationUserGroupHistories);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteByGroupId(string groupId)
        {
            var _groupRoles = GetByGroupId(groupId);
            DeleteRange(_groupRoles);
        }

        public IEnumerable<ApplicationUserGroupHistory> GetAll(string keyword)
        {
            var _applicationUserGroupHistorys = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUserGroupHistorys.Where(m => m.ApplicationGroupId.Contains(keyword));
            else
                return _applicationUserGroupHistorys;
        }

        public IEnumerable<ApplicationUserGroupHistory> GetByGroupId(string groupId)
        {
            return GetAll().Where(m => m.ApplicationGroupId == groupId);
        }

        public IEnumerable<ApplicationUserGroupHistory> GetByUserHistoryId(Guid userHistoryId)
        {
            return GetAll().Where(m => m.ApplicationUserHistoryId == userHistoryId);
        }
    }
}