using iThinking.Manager.Identity;
using Repository.Pattern.UnitOfWork;

namespace iThinking.Manager.Common
{
    public class ObjectManager
    {
        protected IUnitOfWorkAsync _unitOfWorkAsync;

        #region Identity

        protected ApplicationUserGroupManager _applicationUserGroupManager;
        protected ApplicationUserManager _applicationUserManager;
        protected ApplicationUserRoleManager _applicationUserRoleManager;
        protected ApplicationRoleManager _applicationRoleManager;
        protected ApplicationGroupRoleManager _applicationGroupRoleManager;
        protected ApplicationUserGroupChangeManager _applicationUserGroupChangeManager;
        protected ApplicationUserGroupHistoryManager _applicationUserGroupHistoryManager;
        protected ApplicationGroupManager _applicationGroupManager;
        protected ApplicationErrorManager _applicationErrorManager;

        #endregion Identity

        public ObjectManager(IUnitOfWorkAsync unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }
    }
}