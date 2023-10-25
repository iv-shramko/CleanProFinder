using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public class AuthService : IAuthService
{
    private const string SignUpEndpoint = "api/Account/service-user/account/create";

    private readonly IHttpService _httpService;

    public AuthService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse<SignUpResultDto>> SignUp(string email, string password)
    {
        var signUpCommand = new CreateServiceUserCommandDto
        {
            Email = email,
            Password = password
        };

        return await _httpService.SendAsync<SignUpResultDto>(HttpMethod.Post, SignUpEndpoint, signUpCommand);
    }

    public async Task SaveCurrentUserAsync(string bearerToken)
    {
        await SecureStorage.SetAsync("BearerToken", bearerToken);
    }
}