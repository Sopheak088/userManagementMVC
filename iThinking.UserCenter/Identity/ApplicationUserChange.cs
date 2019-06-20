using iThinking.UserCenter.Identity.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetUserChanges")]
    public class ApplicationUserChange : ApplicationUserBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public string ReasonChange { get; set; }

        public virtual IEnumerable<ApplicationUserGroupChange> ApplicationUserGroupChanges { set; get; }

        public ApplicationUserChange()
        {
            Id = Guid.NewGuid();
        }
    }
}