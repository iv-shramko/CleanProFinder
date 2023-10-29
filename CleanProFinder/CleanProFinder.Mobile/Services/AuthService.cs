using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.ServiceResponseHandling;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CleanProFinder.Shared.Helpers;

namespace CleanProFinder.Mobile.Services;

public class AuthService : IAuthService
{
    private const string SignUpEndpoint = "api/account/service-user/create";
    private const string SignInEndpoint = "api/account/sign-in";

    private readonly IHttpService _httpService;

    private string _userRole;

    public AuthService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public bool IsAuthenticated { get; private set; }
    public bool IsCustomer => _userRole == Roles.ServiceUser;

    public void Initialize()
    {
        var bearerToken = SecureStorage.GetAsync("BearerToken").Result;
        IsAuthenticated = !string.IsNullOrEmpty(bearerToken);
        if (IsAuthenticated)
        {
            _httpService.SetAuthorizationHeader(bearerToken);
            _userRole = GetUserRoleFromBearer(bearerToken);
        }
    }

    public async Task<ServiceResponse<SignUpResultDto>> SignUpAsync(string email, string password)
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

    public async Task<ServiceResponse<SignInResultDto>> SignInAsync(string email, string password)
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
        _httpService.SetAuthorizationHeader(bearerToken);
        IsAuthenticated = true;
        _userRole = GetUserRoleFromBearer(bearerToken);
    }

    private string GetUserRoleFromBearer(string bearerToken)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtHandler.ReadJwtToken(bearerToken);
        return jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    }
}