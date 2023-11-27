using CleanProFinder.Shared.Errors.Base;


namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class SavedProviderError : ServiceError
    {
        public SavedProviderError(string header, string message, int code)
        : base(header, message, code) { }

        public static SavedProviderError InvalidSavedProvider => new SavedProviderError("Saved Provider error", "No such saved provider in the list", 1);
        public static SavedProviderError AlreadySavedProvider => new SavedProviderError("Saved Provider error", "Already saved provider", 2);

    }
}