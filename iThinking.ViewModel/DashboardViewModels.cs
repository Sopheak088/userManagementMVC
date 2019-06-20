using iThinking.UserCenter.Identity;
using System.Collections.Generic;

namespace iThinking.ViewModel
{
    public class DashboardIndexViewModel
    {
        public int UserCount { get; set; }

        public int GroupCount { get; set; }

        public int RoleCount { get; set; }

        public int ErrorCount { get; set; }

        public DashboardIndexViewModel()
        {
            UserCount = 0;
            GroupCount = 0;
            RoleCount = 0;
            ErrorCount = 0;
        }
    }

    public class DashboardTopTitleViewModel
    {
        public int TotalProject { get; set; }

        public int TotalProjectInMonth { get; set; }

        public int TotalError { get; set; }

        public int TotalErrorInMonth { get; set; }

        public int TotalRole { get; set; }

        public int TotalGroup { get; set; }

        public int TotalUser { get; set; }

        public int TotalUserInMonth { get; set; }

        public int TotalConnection { get; set; }

        public int TotalConnectionInMonth { get; set; }

        public DashboardTopTitleViewModel()
        {
            TotalProject = 0;
            TotalProjectInMonth = 0;
            TotalError = 0;
            TotalErrorInMonth = 0;
            TotalRole = 0;
            TotalGroup = 0;
            TotalUser = 0;
            TotalUserInMonth = 0;
            TotalConnection = 0;
            TotalConnectionInMonth = 0;
        }
    }

    public class DashboardListNewViewModel
    {
        public virtual IEnumerable<ApplicationProject> ApplicationProjects { set; get; }

        public virtual IEnumerable<ApplicationError> Errors { set; get; }

        public virtual IEnumerable<ApplicationUser> ApplicationUsers { set; get; }

        public DashboardListNewViewModel()
        {
        }
    }
}