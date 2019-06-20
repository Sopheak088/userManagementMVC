using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationUserGroupHistoryMappers
    {
        public static void UpdateApplicationUserGroupHistory(this ApplicationUserGroupHistory applicationUserGroupHistory, ApplicationUserGroupHistoryViewModel applicationUserGroupHistoryViewModel)
        {
            applicationUserGroupHistory.ApplicationUserHistoryId = applicationUserGroupHistoryViewModel.ApplicationUserHistoryId;
            applicationUserGroupHistory.ApplicationGroupId = applicationUserGroupHistoryViewModel.ApplicationGroupId;
        }
    }
}