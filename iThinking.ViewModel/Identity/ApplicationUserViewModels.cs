using iThinking.UserCenter.Common;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Common;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Web.Mvc;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationUserViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { set; get; }

        [MaxLength(256)]
        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [MaxLength(256)]
        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [MaxLength(256)]
        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Avatar path")]
        public string AvatarPath { set; get; }

        [Required]
        [Display(Name = "Upload folder")]
        public string UploadFolder { set; get; }

        [Display(Name = "Points")]
        public int? Points { set; get; }

        [Display(Name = "Count views")]
        public int? CountViews { set; get; }

        [Display(Name = "About")]
        public string About { set; get; }

        [Display(Name = "Is can login")]
        public bool IsCanLogin { get; set; }

        [Display(Name = "Is approved")]
        public bool IsApproved { get; set; }

        [Display(Name = "Change Id")]
        public Guid? ApplicationUserChangeId { get; set; }

        public ApplicationUserChange ApplicationUserChange { get; set; }

        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Updated date")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        public IEnumerable<ApplicationGroup> Groups { set; get; }

        public virtual ICollection<string> MyGroups { set; get; }

        public ApplicationUserViewModel()
        {
            this.Id = Guid.NewGuid().ToString();
            Points = 0;
            CountViews = 0;

            IsCanLogin = true;
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

    public class ApplicationUserIndexViewModel
    {
        [Display(Name = "Keyword")]
        public string Keyword { set; get; }

        [Display(Name = "Is approved")]
        public bool? IsApproved { get; set; }

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Display(Name = "Start birthday")]
        public DateTime? StartBirthday { set; get; }

        [Display(Name = "End birthday")]
        public DateTime? EndBirthday { set; get; }

        [Display(Name = "Start created date")]
        public DateTime? StartCreatedDate { get; set; }

        [Display(Name = "End created date")]
        public DateTime? EndCreatedDate { get; set; }

        [Display(Name = "Start updated date")]
        public DateTime? StartUpdatedDate { get; set; }

        [Display(Name = "End updated date")]
        public DateTime? EndUpdatedDate { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationUserIndexViewModel()
        {
            ApplicationUsers = new List<ApplicationUser>();
        }
    }

    public class ApplicationUserDetailViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Access failed count")]
        public int AccessFailedCount { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Email confirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Lockout enabled")]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Lockout end date utc")]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "Phone number confirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Security stamp")]
        public string SecurityStamp { get; set; }

        [Display(Name = "Two factor enabled")]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { set; get; }

        [MaxLength(256)]
        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [MaxLength(256)]
        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [MaxLength(256)]
        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Avatar path")]
        public string AvatarPath { set; get; }

        [Required]
        [Display(Name = "Upload folder")]
        public string UploadFolder { set; get; }

        [Display(Name = "Points")]
        public int? Points { set; get; }

        [Display(Name = "Count views")]
        public int? CountViews { set; get; }

        [Display(Name = "About")]
        public string About { set; get; }

        [Display(Name = "Is can login")]
        public bool IsCanLogin { get; set; }

        [Display(Name = "Is approved")]
        public bool IsApproved { get; set; }

        [Display(Name = "Change Id")]
        public Guid? ApplicationUserChangeId { get; set; }

        public ApplicationUserChange ApplicationUserChange { get; set; }

        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Display(Name = "Updated date")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        public IEnumerable<ApplicationGroup> Groups { set; get; }

        public IEnumerable<ApplicationGroupForRegisterViewModel> MyGroups { set; get; }

        public ApplicationUserDetailViewModel()
        {
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

    public class ApplicationUserAddViewModel
    {
        [Display(Name = "Id")]
        public string Id { set; get; }

        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { set; get; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Gender")]
        public Gender Gender { set; get; }

        [Display(Name = "Is can login")]
        public bool IsCanLogin { get; set; }

        public IEnumerable<ApplicationGroup> Groups { set; get; }

        public virtual ICollection<string> MyGroups { set; get; }

        public ApplicationUserAddViewModel()
        {
            Gender = Gender.NotSpecified;
            IsCanLogin = true;
        }
    }

    public class ApplicationUserEditViewModel
    {
        [Display(Name = "Id")]
        public string Id { set; get; }

        [Display(Name = "Username")]
        public string UserName { set; get; }

        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Gender")]
        public Gender Gender { set; get; }

        [Display(Name = "Email")]
        public string Email { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Is can login")]
        public bool IsCanLogin { get; set; }

        public IEnumerable<ApplicationGroup> Groups { set; get; }

        public virtual ICollection<string> MyGroups { set; get; }

        public ApplicationUserEditViewModel()
        {
        }
    }

    public class UserInfoViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Has registered")]
        public bool HasRegistered { get; set; }

        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class ManageInfoViewModel
    {
        [Display(Name = "Local login provider")]
        public string LocalLoginProvider { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class ExternalLoginViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }
    }

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        [Display(Name = "Return url")]
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        [Display(Name = "Selected provider")]
        public string SelectedProvider { get; set; }

        [Display(Name = "Return url")]
        public string ReturnUrl { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        [Display(Name = "Provider")]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Return url")]
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class EditUserViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { set; get; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Gender")]
        public Gender Gender { set; get; }

        [Display(Name = "Is can login")]
        public bool IsCanLogin { get; set; }

        // We will still use this, so leave it here:
        public ICollection<SelectListItem> RolesList { get; set; }

        // Add a GroupsList Property:
        public ICollection<SelectListItem> GroupsList { get; set; }

        public EditUserViewModel()
        {
            IsCanLogin = true;

            this.RolesList = new List<SelectListItem>();
            this.GroupsList = new List<SelectListItem>();
        }

    }

    public class UserDetailViewModel
    {
        [Display(Name = "Id")]
        public string Id { set; get; }

        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { set; get; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Gender")]
        public Gender Gender { set; get; }

        // We will still use this, so leave it here:
        public ICollection<SelectListItem> RolesList { get; set; }

        // Add a GroupsList Property:
        public ICollection<SelectListItem> GroupsList { get; set; }

        public UserDetailViewModel()
        {
            this.RolesList = new List<SelectListItem>();
            this.GroupsList = new List<SelectListItem>();
        }
    }

    public class UserCreateViewModel
    {
        [Display(Name = "First name")]
        public string FirstName { set; get; }

        [Display(Name = "Last name")]
        public string LastName { set; get; }

        [Required]
        [Display(Name = "User name")]
        [RegularExpression(@"^[a-zA-Z0-9-.]+$", ErrorMessage = "User name must be combination of letters and numbers only.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        public string Address { set; get; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { set; get; }

        [Display(Name = "Is can login")]
        public bool IsCanLogin { get; set; }

        // We will still use this, so leave it here:
        public ICollection<SelectListItem> RolesList { get; set; }

        // Add a GroupsList Property:
        public ICollection<SelectListItem> GroupsList { get; set; }

        public UserCreateViewModel()
        {
            this.RolesList = new List<SelectListItem>();
            this.GroupsList = new List<SelectListItem>();

            IsCanLogin = true;
        }
    }

    public class IndexViewModel
    {
        [Display(Name = "Has password")]
        public bool HasPassword { get; set; }
        
        public IList<UserLoginInfo> Logins { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Two factor")]
        public bool TwoFactor { get; set; }

        [Display(Name = "Browser remembered")]
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        [Display(Name = "Birthday")]
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        [Display(Name = "Selected provider")]
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }

    public class ExternalLoginData
    {
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public IList<Claim> GetClaims()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

            if (UserName != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
            }

            return claims;
        }
    }
}