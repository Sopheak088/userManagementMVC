using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<string>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}