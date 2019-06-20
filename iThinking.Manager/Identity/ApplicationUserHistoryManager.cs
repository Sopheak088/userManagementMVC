using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserHistoryManager : ObjectManager
    {
        public ApplicationUserHistoryManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUserHistory> GetAll()
        {
            var _applicationUserHistorys = _unitOfWorkAsync.Repository<ApplicationUserHistory>().Query()
                .Include(m => m.ApplicationUserChange)
                .Select().AsEnumerable().OrderByDescending(t => t.CreatedDate);
            return _applicationUserHistorys;
        }

        public void Insert(ApplicationUserHistory applicationUserHistory)
        {
            _unitOfWorkAsync.Repository<ApplicationUserHistory>().Insert(applicationUserHistory);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUserHistory> applicationUserHistories)
        {
            _unitOfWorkAsync.Repository<ApplicationUserHistory>().InsertRange(applicationUserHistories);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUserHistory applicationUserHistory)
        {
            _unitOfWorkAsync.Repository<ApplicationUserHistory>().Update(applicationUserHistory);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUserHistory applicationUserHistory)
        {
            _unitOfWorkAsync.Repository<ApplicationUserHistory>().Delete(applicationUserHistory);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUserHistory> applicationUserHistories)
        {
            _unitOfWorkAsync.Repository<ApplicationUserHistory>().DeleteRange(applicationUserHistories);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationUserHistory> GetAll(string keyword)
        {
            var _applicationUserHistorys = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUserHistorys.Where(m => m.About.Contains(keyword)).AsEnumerable().OrderByDescending(t => t.CreatedDate);
            else
                return _applicationUserHistorys;
        }

        public ApplicationUserHistory Find(Guid id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
    }
}