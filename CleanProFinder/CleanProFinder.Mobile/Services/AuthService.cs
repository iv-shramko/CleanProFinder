using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public class AuthService : IAuthService
{
    private const string SignUpEndpoint = "api/account/service-user/create";
    private const string SignInEndpoint = "api/account/sign-in";

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

        var response = await _httpService.SendAsync<SignUpResultDto>(HttpMethod.Post, SignUpEndpoint, signUpCommand);

        if (response.IsSuccess)
        {
            await SaveCurrentUserAsync(response.Result.Bearer);
        }

        return response;
    }

    public async Task<ServiceResponse<SignInResultDto>> SignIn(string email, string password)
    {
        var signInCommand = new SignInCommandDto
        {
            Email = email,
            Password = password
        };

        var response = await _httpService.SendAsync<SignInResultDto>(HttpMethod.Post, SignInEndpoint, signInCommand);

        if (response.IsSuccess)
        {
            await SaveCurrentUserAsync(response.Result.Bearer);
        }

        return response;
    }


    public async Task SaveCurrentUserAsync(string bearerToken)
    {
        await SecureStorage.SetAsync("BearerToken", bearerToken);
    }
}