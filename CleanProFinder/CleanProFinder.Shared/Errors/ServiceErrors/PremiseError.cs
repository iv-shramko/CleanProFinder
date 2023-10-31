using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class PremiseError : ServiceError
    {
        public PremiseError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }
        
        public static PremiseError MatchPremiseError => new PremiseError("Edit Premise Error", "Can not find premise", 1);
    }
}
