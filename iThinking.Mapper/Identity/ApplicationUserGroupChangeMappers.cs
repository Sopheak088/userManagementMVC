using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationUserGroupChangeMappers
    {
        public static void UpdateApplicationUserGroupChange(this ApplicationUserGroupChange applicationUserGroupChange, ApplicationUserGroupChangeViewModel applicationUserGroupChangeViewModel)
        {
            applicationUserGroupChange.ApplicationUserChangeId = applicationUserGroupChangeViewModel.ApplicationUserChangeId;
            applicationUserGroupChange.ApplicationGroupId = applicationUserGroupChangeViewModel.ApplicationGroupId;
        }
    }
}