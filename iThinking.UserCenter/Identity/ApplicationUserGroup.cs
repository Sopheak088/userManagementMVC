using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetUserGroups")]
    public class ApplicationUserGroup : Entity
    {
        public string ApplicationUserId { get; set; }

        public string ApplicationGroupId { get; set; }

        [ForeignKey("ApplicationGroupId")]
        public ApplicationGroup ApplicationGroup { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}