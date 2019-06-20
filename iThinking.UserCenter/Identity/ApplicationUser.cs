using iThinking.UserCenter.Common;
using iThinking.UserCenter.IdentityManager;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.Pattern.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iThinking.UserCenter.Identity
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>, IObjectState
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            // Add any custom User properties/code here
            IsCanLogin = true;

            Points = 0;
            CountViews = 0;
            Gender = Gender.NotSpecified;
        }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(IdentityUserManager manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        [MaxLength(256)]
        public string FirstName { set; get; }

        [MaxLength(256)]
        public string LastName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? Birthday { set; get; }

        public Gender Gender { get; set; }

        public string AvatarPath { set; get; }

        [Required]
        public string UploadFolder { set; get; }

        public int? Points { set; get; }

        public int? CountViews { set; get; }

        public string About { set; get; }

        public bool IsCanLogin { get; set; }

        public bool IsApproved { get; set; }

        public Guid? ApplicationUserChangeId { get; set; }

        [ForeignKey("ApplicationUserChangeId")]
        public ApplicationUserChange ApplicationUserChange { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public void New(string userName)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userName;
        }

        public void Update(string userName)
        {
            UpdatedDate = DateTime.Now;
            UpdatedBy = userName;
        }

        public string GetFullName()
        {
            string fullName = "";
            if (!string.IsNullOrEmpty(LastName))
                fullName += LastName;
            if (!string.IsNullOrEmpty(FirstName))
            {
                if (!string.IsNullOrEmpty(fullName))
                {
                    fullName += " ";
                }
                fullName += FirstName;
            }
            return fullName;
        }
    }
}