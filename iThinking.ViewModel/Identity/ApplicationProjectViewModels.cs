using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationProjectViewModel : BaseObjectViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<ApplicationGroup> ApplicationGroups { set; get; }

        public virtual IEnumerable<ApplicationRole> ApplicationRoles { set; get; }

        public ApplicationProjectViewModel()
        {
        }
    }

    public class ApplicationProjectCreateViewModel
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }
        
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ApplicationProjectCreateViewModel()
        {
        }
    }

    public class ApplicationProjectEditViewModel
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }
        
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ApplicationProjectEditViewModel()
        {
        }
    }

    public class ApplicationProjectIndexViewModel
    {
        [Display(Name = "Keyword")]
        public string Keyword { get; set; }

        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<ApplicationProject> ApplicationProjects { get; set; }

        public ApplicationProjectIndexViewModel()
        {
            ApplicationProjects = new List<ApplicationProject>();
        }
    }
}