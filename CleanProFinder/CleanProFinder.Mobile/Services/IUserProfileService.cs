using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IUserProfileService
{
    Task<ServiceResponse> EditServiceUserProfileAsync(string firstName, string lastName, string phoneNumber);
    Task<ServiceResponse> EditServiceProviderProfileAsync(string providerName, Image logoImage, string phoneNumber, string websiteUrl);
}