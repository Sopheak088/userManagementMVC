using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationUserHistoryViewModel : ApplicationUserBaseViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public Guid? ApplicationUserChangeId { get; set; }

        public ApplicationUserChange ApplicationUserChange { get; set; }

        public virtual IEnumerable<ApplicationUserGroupHistory> ApplicationUserGroupHistories { set; get; }

        public ApplicationUserHistoryViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}