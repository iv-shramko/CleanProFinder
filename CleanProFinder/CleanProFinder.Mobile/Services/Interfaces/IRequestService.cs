using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IRequestService
{
    Task<ServiceResponse> AddRequestAsync(Guid premiseId, IList<CleaningServiceDto> services,
        string description, IList<ProviderRequestInteractionInfo>? selectedProviders = null);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetOwnRequestsAsync();
    Task<ServiceResponse<RequestFullInfoDto>> GetOwnRequestAsync(Guid requestId);
    Task<ServiceResponse> CancelRequestAsync(Guid requestId);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetActiveRequestsAsync();
    Task<ServiceResponse> ServiceProviderAssignForRequestAsync(Guid requestId, float price);
    Task<ServiceResponse<RequestFullInfoProviderViewDto>> GetRequestAsync(Guid requestId);
    Task<ServiceResponse> AcceptProviderForRequestAsync(Guid providerId, Guid requestId);
}
