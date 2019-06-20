using iThinking.UserCenter.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetProjects")]
    public class ApplicationProject : BaseObject
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<ApplicationGroup> ApplicationGroups { set; get; }

        public virtual IEnumerable<ApplicationRole> ApplicationRoles { set; get; }

        public ApplicationProject()
        {
        }
    }
}