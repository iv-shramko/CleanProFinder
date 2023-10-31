using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class ServerError : ServiceError
    {
        public ServerError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }

        public static PremiseError EditPremiseError => new PremiseError("Server error", "Edit Premise Error", 1);
        public static PremiseError CreatePremiseError => new PremiseError("Server error", "Create Premise Error", 2);
    }
}
