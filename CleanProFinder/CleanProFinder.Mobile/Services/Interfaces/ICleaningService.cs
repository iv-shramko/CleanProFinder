using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface ICleaningService
{
    Task<ServiceResponse<IEnumerable<CleaningServiceDto>>> GetServicesAsync();
    Task<ServiceResponse> EditOwnServicesAsync(IEnumerable<ProviderServiceFullInfoDto> cleaningServices);
}