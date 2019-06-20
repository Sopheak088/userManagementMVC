using iThinking.UserCenter.Identity.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetUserHistories")]
    public class ApplicationUserHistory : ApplicationUserBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public Guid? ApplicationUserChangeId { get; set; }

        [ForeignKey("ApplicationUserChangeId")]
        public ApplicationUserChange ApplicationUserChange { get; set; }

        public virtual IEnumerable<ApplicationUserGroupHistory> ApplicationUserGroupHistories { set; get; }

        public ApplicationUserHistory()
        {
            Id = Guid.NewGuid();
        }
    }
}