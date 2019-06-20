using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserChangeManager : ObjectManager
    {
        public ApplicationUserChangeManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUserChange> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationUserChange>().Query()
                .Select().AsEnumerable().OrderByDescending(t => t.CreatedDate);
        }

        public void Insert(ApplicationUserChange applicationUserChange)
        {
            _unitOfWorkAsync.Repository<ApplicationUserChange>().Insert(applicationUserChange);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUserChange> applicationUserChanges)
        {
            _unitOfWorkAsync.Repository<ApplicationUserChange>().InsertRange(applicationUserChanges);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUserChange applicationUserChange)
        {
            _unitOfWorkAsync.Repository<ApplicationUserChange>().Update(applicationUserChange);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUserChange applicationUserChange)
        {
            _unitOfWorkAsync.Repository<ApplicationUserChange>().Delete(applicationUserChange);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUserChange> applicationUserChanges)
        {
            _unitOfWorkAsync.Repository<ApplicationUserChange>().DeleteRange(applicationUserChanges);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationUserChange> GetAll(string keyword)
        {
            var _applicationUserChanges = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUserChanges.Where(m => m.ReasonChange.Contains(keyword)).AsEnumerable().OrderByDescending(t => t.CreatedDate);
            else
                return _applicationUserChanges;
        }

        public ApplicationUserChange Find(Guid id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
    }
}