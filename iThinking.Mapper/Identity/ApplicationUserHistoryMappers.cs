using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationUserHistoryMappers
    {
        public static void UpdateApplicationUserHistory(this ApplicationUserHistory applicationUserHistory, ApplicationUserHistoryViewModel applicationUserHistoryViewModel)
        {
            applicationUserHistory.Id = applicationUserHistoryViewModel.Id;
            applicationUserHistory.ApplicationUserId = applicationUserHistoryViewModel.ApplicationUserId;
            applicationUserHistory.ApplicationUserChangeId = applicationUserHistoryViewModel.ApplicationUserChangeId;

            applicationUserHistory.Email = applicationUserHistoryViewModel.Email;
            applicationUserHistory.PhoneNumber = applicationUserHistoryViewModel.PhoneNumber;
            applicationUserHistory.FirstName = applicationUserHistoryViewModel.FirstName;
            applicationUserHistory.LastName = applicationUserHistoryViewModel.LastName;
            applicationUserHistory.Address = applicationUserHistoryViewModel.Address;
            applicationUserHistory.Birthday = applicationUserHistoryViewModel.Birthday;
            applicationUserHistory.Gender = applicationUserHistoryViewModel.Gender;
            applicationUserHistory.AvatarPath = applicationUserHistoryViewModel.AvatarPath;
            applicationUserHistory.UploadFolder = applicationUserHistoryViewModel.UploadFolder;
            applicationUserHistory.Points = applicationUserHistoryViewModel.Points;
            applicationUserHistory.CountViews = applicationUserHistoryViewModel.CountViews;
            applicationUserHistory.About = applicationUserHistoryViewModel.About;
            applicationUserHistory.IsCanLogin = applicationUserHistoryViewModel.IsCanLogin;

            applicationUserHistory.CreatedDate = applicationUserHistoryViewModel.CreatedDate;
            applicationUserHistory.CreatedBy = applicationUserHistoryViewModel.CreatedBy;
            applicationUserHistory.UpdatedDate = applicationUserHistoryViewModel.UpdatedDate;
            applicationUserHistory.UpdatedBy = applicationUserHistoryViewModel.UpdatedBy;
        }
    }
}