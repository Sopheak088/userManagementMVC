using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationUserChangeViewModel : ApplicationUserBaseViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public string ReasonChange { get; set; }

        public virtual IEnumerable<ApplicationUserGroupChange> ApplicationUserGroupChanges { set; get; }

        public ApplicationUserChangeViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}