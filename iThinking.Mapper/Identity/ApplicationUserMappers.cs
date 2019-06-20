using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationUserMappers
    {
        public static void UpdateApplicationUser(this ApplicationUser applicationUser, ApplicationUserViewModel appUserViewModel)
        {
            applicationUser.UserName = appUserViewModel.UserName;
            applicationUser.FirstName = appUserViewModel.FirstName;
            applicationUser.LastName = appUserViewModel.LastName;
            applicationUser.Address = appUserViewModel.Address;
            applicationUser.Birthday = appUserViewModel.Birthday;
            applicationUser.Email = appUserViewModel.Email;
            applicationUser.PhoneNumber = appUserViewModel.PhoneNumber;
            applicationUser.Gender = appUserViewModel.Gender;
            applicationUser.IsCanLogin = appUserViewModel.IsCanLogin;
            applicationUser.IsApproved = appUserViewModel.IsApproved;
            applicationUser.ApplicationUserChangeId = appUserViewModel.ApplicationUserChangeId;

            applicationUser.CreatedDate = appUserViewModel.CreatedDate;
            applicationUser.CreatedBy = appUserViewModel.CreatedBy;
            applicationUser.UpdatedDate = appUserViewModel.UpdatedDate;
            applicationUser.UpdatedBy = appUserViewModel.UpdatedBy;
        }

        public static void UpdateApplicationUser(this ApplicationUser applicationUser, UserCreateViewModel userCreateViewModel)
        {
            applicationUser.UserName = userCreateViewModel.UserName;
            applicationUser.UploadFolder = userCreateViewModel.Email.Replace("@", "_");
            applicationUser.FirstName = userCreateViewModel.FirstName;
            applicationUser.LastName = userCreateViewModel.LastName;
            applicationUser.Address = userCreateViewModel.Address;
            applicationUser.Birthday = userCreateViewModel.Birthday;
            applicationUser.Email = userCreateViewModel.Email;
            applicationUser.PhoneNumber = userCreateViewModel.PhoneNumber;
            applicationUser.IsCanLogin = userCreateViewModel.IsCanLogin;
        }

        public static void UpdateApplicationUser(this ApplicationUser applicationUser, EditUserViewModel editUserViewModel)
        {
            applicationUser.UserName = editUserViewModel.UserName;
            applicationUser.FirstName = editUserViewModel.FirstName;
            applicationUser.LastName = editUserViewModel.LastName;
            applicationUser.Address = editUserViewModel.Address;
            applicationUser.Birthday = editUserViewModel.Birthday;
            applicationUser.Email = editUserViewModel.Email;
            applicationUser.PhoneNumber = editUserViewModel.PhoneNumber;
            applicationUser.Gender = editUserViewModel.Gender;
            applicationUser.IsCanLogin = editUserViewModel.IsCanLogin;
        }

        public static void UpdateApplicationUser(this ApplicationUser applicationUser, ApplicationUserDetailViewModel appUserDetailViewModel)
        {
            applicationUser.UserName = appUserDetailViewModel.UserName;
            applicationUser.FirstName = appUserDetailViewModel.FirstName;
            applicationUser.LastName = appUserDetailViewModel.LastName;
            applicationUser.Address = appUserDetailViewModel.Address;
            applicationUser.Birthday = appUserDetailViewModel.Birthday;
            applicationUser.Email = appUserDetailViewModel.Email;
            applicationUser.PhoneNumber = appUserDetailViewModel.PhoneNumber;
            applicationUser.Gender = appUserDetailViewModel.Gender;
            applicationUser.IsApproved = appUserDetailViewModel.IsApproved;
            applicationUser.ApplicationUserChangeId = appUserDetailViewModel.ApplicationUserChangeId;

            applicationUser.CreatedDate = appUserDetailViewModel.CreatedDate;
            applicationUser.CreatedBy = appUserDetailViewModel.CreatedBy;
            applicationUser.UpdatedDate = appUserDetailViewModel.UpdatedDate;
            applicationUser.UpdatedBy = appUserDetailViewModel.UpdatedBy;
        }

        public static void UpdateApplicationUser(this ApplicationUser applicationUser, ApplicationUserAddViewModel applicationUserAddViewModel)
        {
            applicationUser.UserName = applicationUserAddViewModel.UserName;
            applicationUser.FirstName = applicationUserAddViewModel.FirstName;
            applicationUser.LastName = applicationUserAddViewModel.LastName;
            applicationUser.Address = applicationUserAddViewModel.Address;
            applicationUser.Birthday = applicationUserAddViewModel.Birthday;
            applicationUser.Email = applicationUserAddViewModel.Email;
            applicationUser.PhoneNumber = applicationUserAddViewModel.PhoneNumber;
            applicationUser.Gender = applicationUserAddViewModel.Gender;
        }

        public static void UpdateApplicationUser(this ApplicationUser applicationUser, ApplicationUserEditViewModel applicationUserEditViewModel)
        {
            applicationUser.UserName = applicationUserEditViewModel.UserName;
            applicationUser.FirstName = applicationUserEditViewModel.FirstName;
            applicationUser.LastName = applicationUserEditViewModel.LastName;
            applicationUser.Address = applicationUserEditViewModel.Address;
            applicationUser.Birthday = applicationUserEditViewModel.Birthday;
            applicationUser.Email = applicationUserEditViewModel.Email;
            applicationUser.PhoneNumber = applicationUserEditViewModel.PhoneNumber;
            applicationUser.Gender = applicationUserEditViewModel.Gender;
        }

        public static void UpdateUserDetailViewModel(this UserDetailViewModel userDetailViewModel, ApplicationUser appUser)
        {
            userDetailViewModel.UserName = appUser.UserName;
            userDetailViewModel.FirstName = appUser.FirstName;
            userDetailViewModel.LastName = appUser.LastName;
            userDetailViewModel.Address = appUser.Address;
            userDetailViewModel.Birthday = appUser.Birthday;
            userDetailViewModel.Email = appUser.Email;
            userDetailViewModel.PhoneNumber = appUser.PhoneNumber;
            userDetailViewModel.Gender = appUser.Gender;
        }
    }
}