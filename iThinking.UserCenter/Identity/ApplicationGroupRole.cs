using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetGroupRoles")]
    public class ApplicationGroupRole : Entity
    {
        public string ApplicationGroupId { get; set; }

        public string ApplicationRoleId { get; set; }

        [ForeignKey("ApplicationGroupId")]
        public ApplicationGroup ApplicationGroup { get; set; }

        [ForeignKey("ApplicationRoleId")]
        public ApplicationRole ApplicationRole { get; set; }
    }
}