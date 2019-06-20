using iThinking.UserCenter.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace iThinking.UserCenter.Identity.Bases
{
    public class ApplicationUserBase : BaseObject
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [MaxLength(256)]
        public string FirstName { set; get; }

        [MaxLength(256)]
        public string LastName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public DateTime? Birthday { set; get; }

        public Gender Gender { get; set; }

        public string AvatarPath { set; get; }

        public string UploadFolder { set; get; }

        public int? Points { set; get; }

        public int? CountViews { set; get; }

        public string About { set; get; }

        public bool IsCanLogin { get; set; }

        public ApplicationUserBase()
        {
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
}