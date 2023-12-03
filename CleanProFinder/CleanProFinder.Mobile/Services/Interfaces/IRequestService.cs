using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IRequestService
{
    Task<ServiceResponse> AddRequestAsync(Guid premiseId, IList<CleaningServiceDto> services,
        string description, IList<ProviderRequestInteractionInfo>? selectedProviders = null);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetRequestsAsync();
    Task<ServiceResponse<RequestFullInfoDto>> GetRequestAsync(Guid requestId);
    Task<ServiceResponse> CancelRequestAsync(Guid requestId);
}
