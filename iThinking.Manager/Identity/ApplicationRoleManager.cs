using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationRoleManager : ObjectManager
    {
        public ApplicationRoleManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationRole>()
                .Query().Include(m => m.ApplicationProject)
                .Select().AsEnumerable();
        }

        public IEnumerable<ApplicationRole> GetAll(ApplicationRoleIndexViewModel applicationRoleIndexViewModel)
        {
            IEnumerable<ApplicationRole> _applicationRoles = GetAll(applicationRoleIndexViewModel.Keyword);

            if (!string.IsNullOrEmpty(applicationRoleIndexViewModel.ApplicationProjectId))
            {
                _applicationRoles = _applicationRoles.Where(m => m.ApplicationProjectId == applicationRoleIndexViewModel.ApplicationProjectId);
            }
            if (!string.IsNullOrEmpty(applicationRoleIndexViewModel.GroupName))
            {
                _applicationRoles = _applicationRoles.Where(m => m.GroupName == applicationRoleIndexViewModel.GroupName);
            }

            return _applicationRoles;
        }

        public void Insert(ApplicationRole applicationRole)
        {
            _unitOfWorkAsync.Repository<ApplicationRole>().Insert(applicationRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationRole> applicationRoles)
        {
            _unitOfWorkAsync.Repository<ApplicationRole>().InsertRange(applicationRoles);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationRole applicationRole)
        {
            _unitOfWorkAsync.Repository<ApplicationRole>().Update(applicationRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationRole applicationRole)
        {
            _unitOfWorkAsync.Repository<ApplicationRole>().Delete(applicationRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationRole> applicationRoles)
        {
            _unitOfWorkAsync.Repository<ApplicationRole>().DeleteRange(applicationRoles);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationRole> GetAll(string keyword)
        {
            var _applicationRoles = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationRoles.Where(m => m.Description.Contains(keyword));
            else
                return _applicationRoles;
        }

        public ApplicationRole Find(string id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<ApplicationRole> GetByGroupId(string groupId)
        {
            _applicationGroupRoleManager = new ApplicationGroupRoleManager(_unitOfWorkAsync);
            return _applicationGroupRoleManager.GetByGroupId(groupId).Select(m => m.ApplicationRole);
        }

        public IEnumerable<ApplicationRole> GetByProjectId(string projectId)
        {
            return GetAll().Where(m => m.ApplicationProjectId == projectId);
        }
    }
}