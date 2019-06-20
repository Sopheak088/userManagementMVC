using iThinking.UserCenter.Identity;
using System;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationUserGroupChangeViewModel
    {
        public Guid ApplicationUserChangeId { get; set; }

        public virtual ApplicationUserChange ApplicationUserChange { get; set; }

        public string ApplicationGroupId { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; }

        public ApplicationUserGroupChangeViewModel()
        {
        }
    }
}