using System.Security.Claims;

namespace CleanProFinder.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool TryGetUserId(this IHttpContextAccessor contextAccessor, out Guid userId)
        {
            return contextAccessor.HttpContext.TryGetUserId(out userId);
        }

        public static bool TryGetUserId(this HttpContext? context, out Guid userId)
        {
            var identityName = context?.User?.Claims?.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
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
