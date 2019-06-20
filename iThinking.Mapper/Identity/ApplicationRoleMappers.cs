using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationRoleMappers
    {
        public static void UpdateApplicationRole(this ApplicationRole applicationRole, ApplicationRoleViewModel appRoleViewModel)
        {
            applicationRole.Name = appRoleViewModel.Name;
            applicationRole.Title = appRoleViewModel.Title;
            applicationRole.Description = appRoleViewModel.Description;
            applicationRole.ApplicationProjectId = appRoleViewModel.ApplicationProjectId;
            applicationRole.GroupName = appRoleViewModel.GroupName;
        }

        public static void UpdateApplicationRole(this ApplicationRole applicationRole, ApplicationRoleCreateViewModel appRoleCreateViewModel)
        {
            applicationRole.Name = appRoleCreateViewModel.ApplicationProjectId + "." + appRoleCreateViewModel.Name;
            applicationRole.Title = appRoleCreateViewModel.Title;
            applicationRole.Description = appRoleCreateViewModel.Description;
            applicationRole.ApplicationProjectId = appRoleCreateViewModel.ApplicationProjectId;
            applicationRole.GroupName = appRoleCreateViewModel.GroupName;
        }

        public static void UpdateApplicationRole(this ApplicationRole applicationRole, ApplicationRoleEditViewModel appRoleEditViewModel)
        {
            applicationRole.Name = appRoleEditViewModel.Name;
            applicationRole.Title = appRoleEditViewModel.Title;
            applicationRole.Description = appRoleEditViewModel.Description;
            applicationRole.ApplicationProjectId = appRoleEditViewModel.ApplicationProjectId;
            applicationRole.GroupName = appRoleEditViewModel.GroupName;
        }
    }
}