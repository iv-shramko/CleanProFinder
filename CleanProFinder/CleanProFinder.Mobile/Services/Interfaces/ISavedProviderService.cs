using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Dto.SavedProviders;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface ISavedProviderService
{
    Task<ServiceResponse<IEnumerable<ProviderPreviewDto>>> GetProvidersAsync();
    Task<ServiceResponse> SaveProviderAsync(Guid providerId);
    Task<ServiceResponse> DeleteProviderAsync(Guid providerId);
}
