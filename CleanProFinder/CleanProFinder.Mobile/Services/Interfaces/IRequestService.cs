using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IRequestService
{
    Task<ServiceResponse> AddServiceUserRequestAsync(Guid premiseId, List<Guid> servicesId, string description, Guid? selectedProviderId);
    Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetServiceUserRequestsAsync();
    Task<ServiceResponse<RequestFullInfoDto>> GetServiceUserRequestAsync(string payload);
    Task<ServiceResponse> CancelServiceUserRequestAsync(string payload);
}
