using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationProjectMappers
    {
        public static void UpdateApplicationProject(this ApplicationProject applicationProject, ApplicationProjectViewModel applicationProjectViewModel)
        {
            applicationProject.Id = applicationProjectViewModel.Id;
            applicationProject.Name = applicationProjectViewModel.Name;
            applicationProject.Description = applicationProjectViewModel.Description;

            applicationProject.CreatedDate = applicationProjectViewModel.CreatedDate;
            applicationProject.CreatedBy = applicationProjectViewModel.CreatedBy;
            applicationProject.UpdatedDate = applicationProjectViewModel.UpdatedDate;
            applicationProject.UpdatedBy = applicationProjectViewModel.UpdatedBy;
        }

        public static void UpdateApplicationProject(this ApplicationProject applicationProject, ApplicationProjectCreateViewModel applicationProjectCreateViewModel)
        {
            applicationProject.Id = applicationProjectCreateViewModel.Id;
            applicationProject.Name = applicationProjectCreateViewModel.Name;
            applicationProject.Description = applicationProjectCreateViewModel.Description;
        }

        public static void UpdateApplicationProject(this ApplicationProject applicationProject, ApplicationProjectEditViewModel applicationProjectEditViewModel)
        {
            applicationProject.Id = applicationProjectEditViewModel.Id;
            applicationProject.Name = applicationProjectEditViewModel.Name;
            applicationProject.Description = applicationProjectEditViewModel.Description;
        }
    }
}