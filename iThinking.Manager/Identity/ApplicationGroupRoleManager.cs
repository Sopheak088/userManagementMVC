using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationGroupRoleManager : ObjectManager
    {
        public ApplicationGroupRoleManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationGroupRole> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationGroupRole>().Query()
                .Include(m => m.ApplicationGroup)
                .Include(m => m.ApplicationRole).Select().AsEnumerable();
        }

        public void Insert(ApplicationGroupRole applicationGroupRole)
        {
            _unitOfWorkAsync.Repository<ApplicationGroupRole>().Insert(applicationGroupRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationGroupRole> applicationGroupRoles)
        {
            _unitOfWorkAsync.Repository<ApplicationGroupRole>().InsertRange(applicationGroupRoles);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationGroupRole applicationGroupRole)
        {
            _unitOfWorkAsync.Repository<ApplicationGroupRole>().Update(applicationGroupRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationGroupRole applicationGroupRole)
        {
            _unitOfWorkAsync.Repository<ApplicationGroupRole>().Delete(applicationGroupRole);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationGroupRole> GetByGroupId(string groupId)
        {
            return GetAll().Where(m => m.ApplicationGroupId == groupId);
        }

        public IEnumerable<ApplicationGroupRole> GetByGroupIds(IEnumerable<string> groupIds)
        {
            List<ApplicationGroupRole> _applicationGroupRoles = new List<ApplicationGroupRole>();
            foreach (var groupId in groupIds)
            {
                var _results = GetAll().Where(m => m.ApplicationGroupId == groupId);

                _applicationGroupRoles.AddRange(_results);
            }
            return _applicationGroupRoles;
        }

        public void DeleteByGroupId(string groupId)
        {
            var _groupRoles = GetByGroupId(groupId);
            foreach (var item in _groupRoles)
            {
                _unitOfWorkAsync.Repository<ApplicationGroupRole>().Delete(item);
            }
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationGroupRole> GetAll(string keyword)
        {
            var _applicationGroupRoles = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationGroupRoles.Where(m => m.ApplicationGroupId.Contains(keyword)).AsEnumerable();
            else
                return _applicationGroupRoles;
        }
    }
}