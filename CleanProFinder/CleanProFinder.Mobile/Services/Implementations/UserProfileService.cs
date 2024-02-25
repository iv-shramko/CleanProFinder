using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class UserProfileService : IUserProfileService
{
    private const string GetServiceUserProfileInfoEndpoint = "api/profile/service-user/info";
    private const string GetServiceProviderProfileInfoEndpoint = "api/profile/service-provider/info";
    private const string EditServiceUserProfileEndpoint = "api/profile/service-user/edit";
    private const string EditServiceProviderProfileEndpoint = "api/profile/service-provider/edit";

    private readonly IHttpService _httpService;

    public UserProfileService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse<UserProfileViewInfoDto>> GetServiceUserProfileAsync()
    {
        return await _httpService.SendAsync<UserProfileViewInfoDto>(HttpMethod.Get, GetServiceUserProfileInfoEndpoint);
    }

    public async Task<ServiceResponse<ProviderProfileViewInfoDto>> GetServiceProviderProfileAsync()
    {
        return await _httpService.SendAsync<ProviderProfileViewInfoDto>(HttpMethod.Get, GetServiceProviderProfileInfoEndpoint);
    }

    public async Task<ServiceResponse> EditServiceUserProfileAsync(string firstName, string lastName, string phoneNumber)
    {
        var userProfileDto = new UserProfileDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber
        };

        return await _httpService.SendAsync(HttpMethod.Post, EditServiceUserProfileEndpoint, userProfileDto);
    }

    public async Task<ServiceResponse> EditServiceProviderProfileAsync(string providerName, string description, string phoneNumber, string websiteUrl)
    {
        var providerProfileDto = new ProviderProfileDto
        {
            Name = providerName,
            Description = description,
            PhoneNumber = phoneNumber,
            Site = websiteUrl
        };

        return await _httpService.SendAsync(HttpMethod.Post, EditServiceProviderProfileEndpoint, providerProfileDto);
    }
}