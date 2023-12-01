using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IRequestService
{
    Task<ServiceResponse> AddServiceUserRequestAsync(Guid premiseId, IList<CleaningServiceDto> services, string description, Guid? selectedProviderId = null);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetServiceUserRequestsAsync();
    Task<ServiceResponse<RequestFullInfoDto>> GetServiceUserRequestAsync(Guid requestId);
    Task<ServiceResponse> CancelServiceUserRequestAsync(Guid requestId);
}
