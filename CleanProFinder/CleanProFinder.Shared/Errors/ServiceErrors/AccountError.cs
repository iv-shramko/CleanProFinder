using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class AccountError : ServiceError
    {
        public AccountError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }

        public static AccountError IdentityCreateError =>
            new AccountError("Account creation error", "Error when creating account", 1);

        public static AccountError UserCreateError =>
            new AccountError("Account creation error", "Error when creating account", 2);

        public static AccountError LoginServiceError =>
            new AccountError("Login error", "Error when performing login", 4);

        public static AccountError LoginUserError =>
            new AccountError("Login error", "Email or password is not valid", 5);
    }
}
