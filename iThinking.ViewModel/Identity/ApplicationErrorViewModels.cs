using iThinking.ViewModel.Common;

namespace iThinking.ViewModel.Identity
{
    public class ApplicationErrorViewModel : BaseObjectViewModel
    {
        public int Id { set; get; }

        public string Message { set; get; }

        public string StackTrace { set; get; }

        public ApplicationErrorViewModel()
        {
        }
    }
}