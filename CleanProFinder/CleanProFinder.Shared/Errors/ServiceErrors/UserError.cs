using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class UserError : ServiceError
    {
        public UserError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }

        public static UserError InvalidAuthorization =>
            new UserError("Invalid Authorization", "Invalid Authorization", 1);

        public static UserError UserNotFound =>
            new UserError("User not found", "User not found", 2);        
        public static UserError UserIsRestricted =>
            new UserError("User is restricted", "User is restricted", 3);
    }
}