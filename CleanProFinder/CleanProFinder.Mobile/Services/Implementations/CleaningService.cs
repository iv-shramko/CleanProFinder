using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class CleaningService : ICleaningService
{
    private const string GetCleaningServicesEndpoint = "api/cleaningservice/services";
    private const string EditOwnCleaningServicesEndpoint = "api/profile/service-provider/edit-services";

    private readonly IHttpService _httpService;

    public CleaningService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse<IEnumerable<CleaningServiceDto>>> GetServicesAsync()
    {
        return await _httpService.SendAsync<IEnumerable<CleaningServiceDto>>(HttpMethod.Get, GetCleaningServicesEndpoint);
    }

    public async Task<ServiceResponse> EditOwnServicesAsync(IEnumerable<ProviderServiceFullInfoDto> cleaningServices)
    {
        var editProviderServicesCommand = new EditProviderServicesDto
        {
            Services = cleaningServices.Select(cleaningService => new EditProviderServiceDto
            {
                CleaningServiceId = cleaningService.CleaningServiceId,
                Description = cleaningService.Description,
                Price = cleaningService.Price
            })
            .ToList()
        };
        
        return await _httpService.SendAsync(HttpMethod.Post, EditOwnCleaningServicesEndpoint, editProviderServicesCommand);
    }
}