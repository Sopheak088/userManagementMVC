using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace iThinking.UserCenter.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<string>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}