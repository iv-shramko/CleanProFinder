using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CleanProFinder.Mobile.Messages;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Account;
using CleanProFinder.Shared.Helpers;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.Messaging;

namespace CleanProFinder.Mobile.Services.Implementations;

public class AuthService : IAuthService
{
    private const string SignUpServiceUserEndpoint = "api/account/service-user/create";
    private const string SignUpServiceProviderEndpoint = "api/account/service-provider/create";
    private const string SignInEndpoint = "api/account/sign-in";

    private readonly IHttpService _httpService;

    private string _userRole;

    public AuthService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    private string UserRole
    {
        get => _userRole;
        set
        {
            _userRole = value;
            WeakReferenceMessenger.Default.Send(new UserRoleAssignedMessage(IsServiceUser));
        }
    }

    public bool IsAuthenticated { get; private set; }
    public bool IsServiceUser => UserRole == Roles.ServiceUser;

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

    public async Task<ServiceResponse<SignUpResultDto>> SignUpServiceUserAsync(string email, string password)
    {
        var signUpServiceUserCommand = new CreateServiceUserCommandDto
        {
            Email = email,
            Password = password
        };

        var response = await _httpService.SendAsync<SignUpResultDto>(HttpMethod.Post, SignUpServiceUserEndpoint, signUpServiceUserCommand);

        if (response.IsSuccess)
        {
            await SaveCurrentUserAsync(response.Result.Bearer);
        }

        return response;
    }

    public async Task<ServiceResponse<SignUpResultDto>> SignUpServiceProviderAsync(string email, string password)
    {
        var signUpServiceProviderCommand = new CreateServiceProviderCommandDto
        {
            Email = email,
            Password = password
        };

        var response = await _httpService.SendAsync<SignUpResultDto>(HttpMethod.Post, SignUpServiceProviderEndpoint, signUpServiceProviderCommand);

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
        UserRole = GetUserRoleFromBearer(bearerToken);
    }

    public void Logout()
    {
        SecureStorage.RemoveAll();
        IsAuthenticated = false;
    }

    private string GetUserRoleFromBearer(string bearerToken)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = jwtHandler.ReadJwtToken(bearerToken);
        return jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    }
}