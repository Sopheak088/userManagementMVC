using iThinking.UserCenter.Identity;
using iThinking.ViewModel.Identity;

namespace iThinking.Mapper.Identity
{
    public static class ApplicationErrorMappers
    {
        public static void UpdateApplicationError(this ApplicationError error, ApplicationErrorViewModel errorViewModel)
        {
            error.Id = errorViewModel.Id;
            error.Message = errorViewModel.Message;
            error.StackTrace = errorViewModel.StackTrace;

            error.CreatedDate = errorViewModel.CreatedDate;
            error.CreatedBy = errorViewModel.CreatedBy;
            error.UpdatedDate = errorViewModel.UpdatedDate;
            error.UpdatedBy = errorViewModel.UpdatedBy;
        }
    }
}