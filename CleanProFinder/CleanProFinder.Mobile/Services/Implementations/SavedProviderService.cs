using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Dto.SavedProviders;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class SavedProviderService : ISavedProviderService
{
    private const string GetProvidersEndpoint = "api/SavedProvider/my-saved-providers";
    private const string SaveProviderEndpoint = "api/SavedProvider/my-saved-providers/save";
    private const string DeleteProviderEndpoint = "api/SavedProvider/my-saved-providers/delete";

    private readonly IHttpService _httpService;

    public SavedProviderService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse<IEnumerable<ProviderPreviewDto>>> GetProvidersAsync()
    {
        return await _httpService.SendAsync<IEnumerable<ProviderPreviewDto>>(HttpMethod.Get, GetProvidersEndpoint);
    }

    public async Task<ServiceResponse> SaveProviderAsync(Guid providerId)
    {
        var payload = providerId.ToString();
        return await _httpService.SendAsync(HttpMethod.Get, SaveProviderEndpoint, payload);
    }

    public async Task<ServiceResponse> DeleteProviderAsync(Guid providerId)
    {
        var payload = providerId.ToString();
        return await _httpService.SendAsync(HttpMethod.Get, DeleteProviderEndpoint, payload);
    }
}
