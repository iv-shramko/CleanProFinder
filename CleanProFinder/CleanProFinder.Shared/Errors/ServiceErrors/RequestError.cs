using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class RequestError : ServiceError
    {
        public RequestError(string header, string message, int code)
            :base(header, message, code) { }

        public static RequestError InvalidPremise => new RequestError("Request error", "Invalid premise", 1);
    }
}
