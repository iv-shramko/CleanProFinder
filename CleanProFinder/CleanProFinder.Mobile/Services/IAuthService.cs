using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IAuthService
{
    Task<ServiceResponse<SignUpResultDto>> SignUp(string email, string password);
    Task<ServiceResponse<SignInResultDto>> SignIn(string email, string password);
    Task SaveCurrentUserAsync(string bearerToken);
}