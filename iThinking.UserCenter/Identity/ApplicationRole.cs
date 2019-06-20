using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetRoles")]
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name) : this()
        {
            this.Name = name;
        }

        public ApplicationRole(string name, string title, string description, string groupName, string applicationProjectId) : this()
        {
            this.Name = name;
            this.Title = title;
            this.Description = description;
            this.GroupName = groupName;
            this.ApplicationProjectId = applicationProjectId;
        }

        // Add any custom Role properties/code here
        [StringLength(250)]
        [Required]
        public string Title { set; get; }

        public string GroupName { get; set; }

        public string ApplicationProjectId { get; set; }

        [ForeignKey("ApplicationProjectId")]
        public ApplicationProject ApplicationProject { get; set; }

        [StringLength(250)]
        public virtual string Description { set; get; }
    }
}