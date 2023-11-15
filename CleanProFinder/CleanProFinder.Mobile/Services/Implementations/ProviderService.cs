using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class ProviderService : IProviderService
{
    private const string GetServiceProvidersEndpoint = "api/profile/service-user/providers";

    private readonly IHttpService _httpService;

    public ProviderService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse<IEnumerable<ProviderPreviewDto>>> GetServiceProvidersAsync()
    {
        return await _httpService.SendAsync<IEnumerable<ProviderPreviewDto>>(HttpMethod.Get, GetServiceProvidersEndpoint);
    }
}