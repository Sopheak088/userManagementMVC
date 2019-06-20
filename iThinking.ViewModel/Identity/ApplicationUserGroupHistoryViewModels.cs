using iThinking.UserCenter.Identity;
using System;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationUserGroupHistoryViewModel
    {
        public Guid ApplicationUserHistoryId { get; set; }

        public virtual ApplicationUserHistory ApplicationUserHistory { get; set; }

        public string ApplicationGroupId { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; }

        public ApplicationUserGroupHistoryViewModel()
        {
        }
    }
}