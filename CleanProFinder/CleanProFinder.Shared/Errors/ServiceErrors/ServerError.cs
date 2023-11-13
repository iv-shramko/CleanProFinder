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
        public static PremiseError GetOwnPremisesError => new PremiseError("Server error", "Get Own Premises Error", 3);
        public static PremiseError GetOwnPremiseError => new PremiseError("Server error", "Get Own Premise Error", 4);
        public static CleaningServiceError EditCleaningServiceError => new CleaningServiceError("Server error", "Edit Cleaning Service Error", 5);
        public static CleaningServiceError CreateCleaningServiceError => new CleaningServiceError("Server error", "Create Cleaning Service Error", 6);
        public static CleaningServiceError GetOwnCleaningServicesError => new CleaningServiceError("Server error", "Get Cleaning Services Error", 7);
        public static CleaningServiceError GetOwnCleaningServiceError => new CleaningServiceError("Server error", "Get Cleaning Service Error", 8);
        public static CleaningServiceError DeleteCleaningServiceError => new CleaningServiceError("Server error", "Delete Cleaning Service Error", 9);
        public static ServerError CreateRequestError => new ServerError("Server error", "Create request Error", 10);
        public static ServerError ActiveRequestsError => new ServerError("Server error", "Get Active Requests Error", 11);
    }
}
