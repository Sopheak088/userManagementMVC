using Repository.Pattern.Ef6;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetUserGroupChanges")]
    public class ApplicationUserGroupChange : Entity
    {
        [Key, Column(Order = 0)]
        public Guid ApplicationUserChangeId { get; set; }

        [Key, Column(Order = 1)]
        public string ApplicationGroupId { get; set; }

        [ForeignKey("ApplicationUserChangeId")]
        public virtual ApplicationUserChange ApplicationUserChange { get; set; }

        [ForeignKey("ApplicationGroupId")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }

        public ApplicationUserGroupChange()
        {
        }
    }
}