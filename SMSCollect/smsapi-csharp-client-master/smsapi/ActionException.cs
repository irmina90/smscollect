
namespace SMSApi.Api
{
    public class ActionException : SMSApi.Api.Exception
    {
        private int Code;

        public ActionException(string message, int code)
            : base(message)
        {
            Code = code;
        }
    }
}
