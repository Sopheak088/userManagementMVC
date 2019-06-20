namespace iThinking.UserCenter.Common
{
    public class Result
    {
        public bool Success { get; set; }

        public string AccessToken { get; set; }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }

        public Result()
        {
            Success = true;
        }
    }
}