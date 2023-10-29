using Microsoft.AspNetCore.Identity;

namespace CleanProFinder.Server.Services.Interfaces
{
    public interface ITokenGenerator
    {
        public Task<string> GenerateAsync(IdentityUser user);
    }
}
