using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IAuthService
{
    public bool IsAuthenticated { get; }
    public bool IsCustomer { get; }

    void Initialize();
    Task<ServiceResponse<SignUpResultDto>> SignUpServiceUserAsync(string email, string password);
    Task<ServiceResponse<SignUpResultDto>> SignUpServiceProviderAsync(string email, string password);
    Task<ServiceResponse<SignInResultDto>> SignInAsync(string email, string password);
    Task SaveCurrentUserAsync(string bearerToken);
}