using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IRequestService
{
    Task<ServiceResponse> ServiceUserAddRequestAsync(Guid premiseId, IList<CleaningServiceDto> services,
        string description, IList<ProviderRequestInteractionInfo>? selectedProviders = null);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> ServiceUserGetRequestsAsync();
    Task<ServiceResponse<RequestFullInfoDto>> ServiceUserGetRequestAsync(Guid requestId);
    Task<ServiceResponse> ServiceUserCancelRequestAsync(Guid requestId);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> ServiceProviderGetActiveRequestsAsync();
    Task<ServiceResponse> ServiceProviderAssignForRequestAsync(Guid requestId, float price);
    Task<ServiceResponse<RequestFullInfoProviderViewDto>> ServiceProviderGetRequestAsync(Guid requestId);
}
