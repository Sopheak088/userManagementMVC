using Repository.Pattern.Ef6;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetUserGroupHistories")]
    public class ApplicationUserGroupHistory : Entity
    {
        [Key, Column(Order = 0)]
        public Guid ApplicationUserHistoryId { get; set; }

        [Key, Column(Order = 1)]
        public string ApplicationGroupId { get; set; }

        [ForeignKey("ApplicationUserHistoryId")]
        public virtual ApplicationUserHistory ApplicationUserHistory { get; set; }

        [ForeignKey("ApplicationGroupId")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }

        public ApplicationUserGroupHistory()
        {
        }
    }
}