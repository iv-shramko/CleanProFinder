using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public class UserProfileService : IUserProfileService
{
    private const string EditServiceUserProfileEndpoint = "api/profile/service-user/edit";
    private const string EditServiceProviderProfileEndpoint = "api/profile/service-provider/edit";

    private readonly IHttpService _httpService;

    public UserProfileService(IHttpService httpService)
    {
        _httpService = httpService;
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

    public async Task<ServiceResponse> EditServiceProviderProfileAsync(string providerName, Image logoImage, string phoneNumber, string websiteUrl)
    {
        var providerProfileDto = new ProviderProfileDto
        {
            Name = providerName,
            PhoneNumber = phoneNumber,
            Site = websiteUrl
        };

        return await _httpService.SendAsync(HttpMethod.Post, EditServiceProviderProfileEndpoint, providerProfileDto);
    }
}