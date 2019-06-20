using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationUserManager : ObjectManager
    {
        public ApplicationUserManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var _applicationUsers = _unitOfWorkAsync.Repository<ApplicationUser>().Query()
                .Include(m => m.ApplicationUserChange)
                .Select().AsEnumerable().OrderByDescending(t => t.CreatedDate);
            return _applicationUsers;
        }

        public IEnumerable<ApplicationUser> GetAll(ApplicationUserIndexViewModel applicationUserSeachViewModel)
        {
            IEnumerable<ApplicationUser> _applicationUsers = GetAll().OrderBy(m => m.CreatedDate).ThenBy(m => m.UserName).ToList();
            if (!string.IsNullOrEmpty(applicationUserSeachViewModel.Keyword))
            {
                _applicationUsers = _applicationUsers.Where(m =>
                    (!string.IsNullOrEmpty(m.FirstName) && m.FirstName.Contains(applicationUserSeachViewModel.Keyword)) ||
                    (!string.IsNullOrEmpty(m.LastName) && m.LastName.Contains(applicationUserSeachViewModel.Keyword)) ||
                    (!string.IsNullOrEmpty(m.Address) && m.Address.Contains(applicationUserSeachViewModel.Keyword)) ||
                    (!string.IsNullOrEmpty(m.PhoneNumber) && m.PhoneNumber.Contains(applicationUserSeachViewModel.Keyword)) ||
                    (!string.IsNullOrEmpty(m.UserName) && m.UserName.Contains(applicationUserSeachViewModel.Keyword)) ||
                    (!string.IsNullOrEmpty(m.About) && m.About.Contains(applicationUserSeachViewModel.Keyword))
                    ).AsEnumerable();
            }
            if (applicationUserSeachViewModel.IsApproved.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.IsApproved == applicationUserSeachViewModel.IsApproved);
            }
            if (applicationUserSeachViewModel.Gender.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.Gender == applicationUserSeachViewModel.Gender);
            }
            if (applicationUserSeachViewModel.StartBirthday.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.Birthday >= applicationUserSeachViewModel.StartBirthday.Value);
            }
            if (applicationUserSeachViewModel.EndBirthday.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.Birthday <= applicationUserSeachViewModel.EndBirthday.Value);
            }
            if (applicationUserSeachViewModel.StartCreatedDate.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.CreatedDate >= applicationUserSeachViewModel.StartCreatedDate.Value);
            }
            if (applicationUserSeachViewModel.EndCreatedDate.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.CreatedDate <= applicationUserSeachViewModel.EndCreatedDate.Value);
            }
            if (applicationUserSeachViewModel.StartUpdatedDate.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.UpdatedDate >= applicationUserSeachViewModel.StartUpdatedDate.Value);
            }
            if (applicationUserSeachViewModel.EndUpdatedDate.HasValue)
            {
                _applicationUsers = _applicationUsers.Where(m => m.UpdatedDate <= applicationUserSeachViewModel.EndUpdatedDate.Value);
            }

            return _applicationUsers;
        }

        public void Insert(ApplicationUser applicationUser)
        {
            _unitOfWorkAsync.Repository<ApplicationUser>().Insert(applicationUser);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationUser> applicationUsers)
        {
            _unitOfWorkAsync.Repository<ApplicationUser>().InsertRange(applicationUsers);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationUser applicationUser)
        {
            _unitOfWorkAsync.Repository<ApplicationUser>().Update(applicationUser);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationUser applicationUser)
        {
            _unitOfWorkAsync.Repository<ApplicationUser>().Delete(applicationUser);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationUser> applicationUsers)
        {
            _unitOfWorkAsync.Repository<ApplicationUser>().DeleteRange(applicationUsers);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAll(string keyword)
        {
            var _applicationUsers = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationUsers.Where(m => m.About.Contains(keyword)).AsEnumerable().OrderByDescending(t => t.CreatedDate);
            else
                return _applicationUsers;
        }

        public ApplicationUser Find(string id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }

        public ApplicationUser FindByUserName(string username)
        {
            return GetAll().FirstOrDefault(m => m.UserName == username);
        }

        public bool IsCanLogin(string username)
        {
            var _user = GetAll().FirstOrDefault(m => m.UserName == username && m.IsApproved == true);

            if (_user != null)
                return true;
            else
                return false;
        }

        public int GetCount()
        {
            return _unitOfWorkAsync.Repository<ApplicationUser>().Query()
                .Select().Count();
        }
    }
}