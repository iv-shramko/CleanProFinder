namespace CleanProFinder.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool TryGetUserId(this HttpContext? context, out Guid userId)
        {
            var identityName = context?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(identityName))
            {
                var isValid = Guid.TryParse(identityName, out Guid parsedUserId);

                if (isValid && parsedUserId == Guid.Empty)
                {
                    isValid = !isValid;
                }

                userId = parsedUserId;

                return isValid;
            }

            userId = Guid.Empty;

            return false;
        }
    }
}
