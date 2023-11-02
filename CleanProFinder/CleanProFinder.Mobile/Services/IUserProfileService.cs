using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IUserProfileService
{
    Task<ServiceResponse<UserProfileViewInfoDto>> GetServiceUserProfileAsync();
    Task<ServiceResponse<ProviderProfileViewInfoDto>> GetServiceProviderProfileAsync();
    Task<ServiceResponse> EditServiceUserProfileAsync(string firstName, string lastName, string phoneNumber);
    Task<ServiceResponse> EditServiceProviderProfileAsync(string providerName, string description, string phoneNumber, string websiteUrl);
}