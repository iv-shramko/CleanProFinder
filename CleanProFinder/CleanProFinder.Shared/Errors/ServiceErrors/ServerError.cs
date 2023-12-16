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
        public static RequestError CreateRequestError => new RequestError("Server error", "Create request Error", 10);
        public static RequestError ActiveRequestsError => new RequestError("Server error", "Get Active Requests Error", 11);
        public static RequestError RequestByIdError => new RequestError("Server error", "Get Request By Id Error", 12);
        public static RequestError OwnRequestsError => new RequestError("Server error", "Get Own Requests Error", 13);
        public static RequestError CancelRequestError => new RequestError("Server error", "Cancel Request Error", 14);
        public static SavedProviderError SaveProviderError => new SavedProviderError("Server error", "Save Provider Error", 15);
        public static SavedProviderError DeleteSavedProviderError => new SavedProviderError("Server error", "Delete Saved Provider Error", 16);
        public static SavedProviderError GetOwnSavedProvidersError => new SavedProviderError("Server error", "Get Own Saved Providers Error", 17);        
        public static SavedProviderError EditUserRestrictions => new SavedProviderError("Server error", "Edit Service User Restrictions Error", 18);        
        public static SavedProviderError EditProviderRestrictions => new SavedProviderError("Server error", "Edit Service Provider Restrictions Error", 19);  
        public static RequestError AcceptProviderForRequestError => new RequestError("Server error", "Accept Provider For Request Error", 20);
        public static RequestError ChangeRequestStatusError => new RequestError("Server error", "Chanre Request Status Error", 21);

    }
}
