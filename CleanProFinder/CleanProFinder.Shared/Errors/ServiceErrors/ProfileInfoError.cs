using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class ProfileInfoError : ServiceError
    {
        public ProfileInfoError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }

        public static ProfileInfoError ViewProfileInfoError =>
            new ProfileInfoError("View Profile Info Error", "Error when getting the profile information", 1);
        public static ProfileInfoError ViewProviderProfilesError =>
           new ProfileInfoError("View Provider Profiles Error", "Error when getting providers' profiles", 2);
    }
}