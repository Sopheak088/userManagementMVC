using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationUserChangeMappers
    {
        public static void UpdateApplicationUserChange(this ApplicationUserChange applicationUserChange, ApplicationUserChangeViewModel applicationUserChangeViewModel)
        {
            applicationUserChange.Id = applicationUserChangeViewModel.Id;
            applicationUserChange.ApplicationUserId = applicationUserChangeViewModel.ApplicationUserId;
            applicationUserChange.ReasonChange = applicationUserChangeViewModel.ReasonChange;

            applicationUserChange.Email = applicationUserChangeViewModel.Email;
            applicationUserChange.PhoneNumber = applicationUserChangeViewModel.PhoneNumber;
            applicationUserChange.FirstName = applicationUserChangeViewModel.FirstName;
            applicationUserChange.LastName = applicationUserChangeViewModel.LastName;
            applicationUserChange.Address = applicationUserChangeViewModel.Address;
            applicationUserChange.Birthday = applicationUserChangeViewModel.Birthday;
            applicationUserChange.Gender = applicationUserChangeViewModel.Gender;
            applicationUserChange.AvatarPath = applicationUserChangeViewModel.AvatarPath;
            applicationUserChange.UploadFolder = applicationUserChangeViewModel.UploadFolder;
            applicationUserChange.Points = applicationUserChangeViewModel.Points;
            applicationUserChange.CountViews = applicationUserChangeViewModel.CountViews;
            applicationUserChange.About = applicationUserChangeViewModel.About;
            applicationUserChange.IsCanLogin = applicationUserChangeViewModel.IsCanLogin;

            applicationUserChange.CreatedDate = applicationUserChangeViewModel.CreatedDate;
            applicationUserChange.CreatedBy = applicationUserChangeViewModel.CreatedBy;
            applicationUserChange.UpdatedDate = applicationUserChangeViewModel.UpdatedDate;
            applicationUserChange.UpdatedBy = applicationUserChangeViewModel.UpdatedBy;
        }
    }
}