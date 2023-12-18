using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class RequestError : ServiceError
    {
        public RequestError(string header, string message, int code)
            :base(header, message, code) { }

        public static RequestError InvalidPremise => new RequestError("Request error", "Invalid premise", 1);
        public static RequestError InvalidId => new RequestError("Request error", "Invalid request id", 2);
        public static RequestError NotRequestOwner => new RequestError("Request error", "It's not your request", 3);
        public static RequestError InvalidInteraction => new RequestError("Request error", "Wrong interaction", 4);
        public static RequestError NoAcceptedInteractions => new RequestError("Request error", "No accepted interactions for you with this request", 5);

    }
}
