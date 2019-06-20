using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationGroupViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Project Id")]
        public string ApplicationProjectId { get; set; }

        public ApplicationProject ApplicationProject { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public IEnumerable<ApplicationRole> Roles { set; get; }

        public virtual ICollection<string> MyRoles { set; get; }

        public ApplicationGroupViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }

    public class ApplicationGroupCreateViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Display(Name = "Project Id")]
        [Required(AllowEmptyStrings = false)]
        public string ApplicationProjectId { get; set; }

        public ApplicationProject ApplicationProject { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public IEnumerable<ApplicationRole> Roles { set; get; }

        public virtual ICollection<string> MyRoles { set; get; }

        public ApplicationGroupCreateViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }

    public class ApplicationGroupEditViewModel
    {
        [Display(Name = "Id")]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Display(Name = "Project Id")]
        [Required(AllowEmptyStrings = false)]
        public string ApplicationProjectId { get; set; }

        public ApplicationProject ApplicationProject { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public IEnumerable<ApplicationRole> Roles { set; get; }

        public virtual ICollection<string> MyRoles { set; get; }
        
        public ApplicationGroupEditViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }

    public class ApplicationGroupForRegisterViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Project Id")]
        public string GroupName { get; set; }

        public ApplicationGroupForRegisterViewModel()
        {

        }
    }

    public class GroupViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Project Id")]
        public string ApplicationProjectId { get; set; }

        public ApplicationProject ApplicationProject { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public ICollection<SelectListItem> UsersList { get; set; }
        public ICollection<SelectListItem> RolesList { get; set; }

        public GroupViewModel()
        {
            this.UsersList = new List<SelectListItem>();
            this.RolesList = new List<SelectListItem>();
        }

    }

    public class ApplicationGroupIndexViewModel
    {
        [Display(Name = "Keyword")]
        public string Keyword { set; get; }

        [Display(Name = "Group name")]
        public string GroupName { get; set; }

        [Display(Name = "Project Id")]
        public string ApplicationProjectId { get; set; }

        public List<ApplicationGroup> ApplicationGroups { get; set; }

        public ApplicationGroupIndexViewModel()
        {
            ApplicationGroups = new List<ApplicationGroup>();
        }
    }
}