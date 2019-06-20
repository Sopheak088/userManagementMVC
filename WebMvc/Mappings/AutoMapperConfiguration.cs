using AutoMapper;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace WebMvc.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //Identity
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationUser, UserCreateViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationUser, EditUserViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationUser, UserDetailViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationRole, ApplicationRoleCreateViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationRole, ApplicationRoleEditViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationGroup, ApplicationGroupCreateViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationGroup, ApplicationGroupEditViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationProject, ApplicationProjectViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationProject, ApplicationProjectCreateViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationProject, ApplicationProjectEditViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationError, ApplicationErrorViewModel>().MaxDepth(2);
            });
        }
    }
}