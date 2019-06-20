using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetGroups")]
    public class ApplicationGroup : Entity
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ApplicationProjectId { get; set; }

        [ForeignKey("ApplicationProjectId")]
        public ApplicationProject ApplicationProject { get; set; }

        public virtual ICollection<ApplicationGroupRole> ApplicationRoles { get; set; }
        public virtual ICollection<ApplicationUserGroup> ApplicationUsers { get; set; }
        public virtual ICollection<ApplicationUserGroupChange> ApplicationUserGroupChanges { get; set; }
        public virtual ICollection<ApplicationUserGroupHistory> ApplicationUserGroupHistories { get; set; }

        public ApplicationGroup()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationRoles = new List<ApplicationGroupRole>();
            this.ApplicationUsers = new List<ApplicationUserGroup>();
        }

        public ApplicationGroup(string name) : this()
        {
            this.Name = name;
        }

        public ApplicationGroup(string name, string description) : this(name)
        {
            this.Description = description;
        }
    }
}