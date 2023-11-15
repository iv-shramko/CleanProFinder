using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IProviderService
{
    Task<ServiceResponse<IEnumerable<ProviderPreviewDto>>> GetServiceProvidersAsync();
}