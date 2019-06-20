using iThinking.UserCenter.Common;
using System;

namespace iThinking.Mapper.Common
{
    public static class ResultMappers
    {
        public static void UpdateResult(this Result result, Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            result.StackTrace = ex.StackTrace;
        }

        public static void UpdateResult(this Result result, Exception ex, string accessToken)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            result.StackTrace = ex.StackTrace;
            result.AccessToken = accessToken;
        }

        public static void UpdateResult(this Result result, string error)
        {
            result.Success = false;
            result.ErrorMessage = error;
        }

        public static void UpdateResult(this Result result, string error, string accessToken)
        {
            result.Success = false;
            result.ErrorMessage = error;
            result.AccessToken = accessToken;
        }
    }
}