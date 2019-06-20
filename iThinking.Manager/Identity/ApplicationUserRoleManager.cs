using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserRoleManager : ObjectManager
    {
        public ApplicationUserRoleManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUserRole> GetAll()
        {
            var _applicationUserRoles = _unitOfWorkAsync.Repository<ApplicationUserRole>().Query()
            .Include(m => m.ApplicationRole)
            .Include(m => m.ApplicationUser).Select().AsEnumerable();
            return _applicationUserRoles;
        }

        public void Insert(ApplicationUserRole applicationUserRole)
        {
            _unitOfWorkAsync.Repository<ApplicationUserRole>().Insert(applicationUserRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUserRole> applicationUserRoles)
        {
            _unitOfWorkAsync.Repository<ApplicationUserRole>().InsertRange(applicationUserRoles);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUserRole applicationUserRole)
        {
            _unitOfWorkAsync.Repository<ApplicationUserRole>().Update(applicationUserRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUserRole applicationUserRole)
        {
            _unitOfWorkAsync.Repository<ApplicationUserRole>().Delete(applicationUserRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUserRole> applicationUserRoles)
        {
            _unitOfWorkAsync.Repository<ApplicationUserRole>().DeleteRange(applicationUserRoles);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationUserRole> GetAll(string keyword)
        {
            var _applicationUserRoles = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUserRoles.Where(m => m.UserId.Contains(keyword)).AsEnumerable();
            else
                return _applicationUserRoles;
        }

        public IEnumerable<ApplicationUserRole> GetByUserId(string userId)
        {
            return GetAll().Where(m => m.UserId == userId);
        }

        public IEnumerable<ApplicationUserRole> GetByRoleId(string roleId)
        {
            return GetAll().Where(m => m.RoleId == roleId);
        }
    }
}