using iThinking.UserCenter.Common;
using System.ComponentModel.DataAnnotations;

namespace iThinking.UserCenter.Identity
{
    public class ApplicationError : BaseObject
    {
        [Key]
        public int Id { set; get; }

        public string Message { set; get; }

        public string StackTrace { set; get; }

        public ApplicationError()
        {
        }
    }
}