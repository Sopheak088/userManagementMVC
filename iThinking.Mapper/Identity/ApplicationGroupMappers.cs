using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationGroupMappers
    {
        public static void UpdateApplicationGroup(this ApplicationGroup applicationGroup, ApplicationGroupViewModel applicationGroupViewModel)
        {
            applicationGroup.Name = applicationGroupViewModel.Name;
            applicationGroup.ApplicationProjectId = applicationGroupViewModel.ApplicationProjectId;
            applicationGroup.Description = applicationGroupViewModel.Description;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup applicationGroup, ApplicationGroupCreateViewModel applicationGroupCreateViewModel)
        {
            applicationGroup.Name = applicationGroupCreateViewModel.ApplicationProjectId + "." + applicationGroupCreateViewModel.Name;
            applicationGroup.ApplicationProjectId = applicationGroupCreateViewModel.ApplicationProjectId;
            applicationGroup.Description = applicationGroupCreateViewModel.Description;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup applicationGroup, ApplicationGroupEditViewModel applicationGroupEditViewModel)
        {
            applicationGroup.Name = applicationGroupEditViewModel.Name;
            applicationGroup.ApplicationProjectId = applicationGroupEditViewModel.ApplicationProjectId;
            applicationGroup.Description = applicationGroupEditViewModel.Description;
        }

        public static void UpdateApplicationGroupForRegisterViewModel(this ApplicationGroupForRegisterViewModel applicationGroupForRegisterViewModel, ApplicationGroup applicationGroup)
        {
            applicationGroupForRegisterViewModel.Id = applicationGroup.Id;
            applicationGroupForRegisterViewModel.Name = applicationGroup.Name;
            applicationGroupForRegisterViewModel.Description = applicationGroup.Description;
            applicationGroupForRegisterViewModel.GroupName = applicationGroup.ApplicationProject.Name;
        }
    }
}