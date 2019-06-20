using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationRoleViewModel
    {
        public string Id { set; get; }

        [Required(AllowEmptyStrings = false)]
        public string Name { set; get; }

        public string Title { set; get; }

        [Display(Name = "Project Id")]
        public string ApplicationProjectId { get; set; }

        public ApplicationProject ApplicationProject { get; set; }

        public string Description { set; get; }

        [Display(Name = "Group name")]
        public string GroupName { get; set; }
    }

    public class ApplicationRoleCreateViewModel
    {
        public string Id { set; get; }

        [Required(AllowEmptyStrings = false)]
        public string Name { set; get; }

        public string Title { set; get; }

        [Display(Name = "Project Id")]
        [Required(AllowEmptyStrings = false)]
        public string ApplicationProjectId { get; set; }

        public string Description { set; get; }

        [Display(Name = "Group name")]
        [Required(AllowEmptyStrings = false)]
        public string GroupName { get; set; }
    }

    public class ApplicationRoleEditViewModel
    {
        public string Id { set; get; }

        [Required(AllowEmptyStrings = false)]
        public string Name { set; get; }

        public string Title { set; get; }

        [Display(Name = "Project Id")]
        [Required(AllowEmptyStrings = false)]
        public string ApplicationProjectId { get; set; }

        public string Description { set; get; }

        [Display(Name = "Group name")]
        [Required(AllowEmptyStrings = false)]
        public string GroupName { get; set; }
    }

    public class ApplicationRoleIndexViewModel
    {
        [Display(Name = "Keyword")]
        public string Keyword { set; get; }

        [Display(Name = "Project")]
        public string ApplicationProjectId { get; set; }

        [Display(Name = "Group name")]
        public string GroupName { get; set; }

        public List<ApplicationRole> ApplicationRoles { get; set; }

        public ApplicationRoleIndexViewModel()
        {
            ApplicationRoles = new List<ApplicationRole>();
        }
    }
}