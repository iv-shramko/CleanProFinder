using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Request;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class RequestService : IRequestService
{
    private const string ServiceUserCreateRequestEndpoint = "api/request/create";
    private const string ServiceUserGetRequestsEndpoint = "api/request/my-requests";
    private const string ServiceUserCancelRequestEndpoint = "api/request/my-requests/cancel";
    private const string ServiceProviderGetActiveRequestEndpoint = "api/request/active-requests";
    private const string ServiceProviderAssignRequestEndpoint = "api/request/assign-request";
    private const string ServiceProviderGetRequestEndpoint = "api/request/request";

    private readonly IHttpService _httpService;

    public RequestService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse> ServiceUserAddRequestAsync(Guid premiseId, IList<CleaningServiceDto> services,
        string description, IList<ProviderRequestInteractionInfo> selectedProviders)
    {
        var servicesId = services.Select(s => s.Id).ToList();
        var selectedProvidersIds = selectedProviders.Select(p => p.ProviderId).ToList();

        var createRequestCommand = new CreateRequestCommandDto
        {
            PremiseId = premiseId,
            ServicesId = servicesId,
            Description = description,
            SelectedProvidersIds = selectedProvidersIds
        };

        return await _httpService.SendAsync(HttpMethod.Post, ServiceUserCreateRequestEndpoint, createRequestCommand);
    }

    public async Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> ServiceUserGetRequestsAsync()
    {
        return await _httpService.SendAsync<IEnumerable<RequestShortInfoDto>>(HttpMethod.Get, ServiceUserGetRequestsEndpoint);
    }

    public async Task<ServiceResponse<RequestFullInfoDto>> ServiceUserGetRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync<RequestFullInfoDto>(HttpMethod.Get, ServiceUserGetRequestsEndpoint, payload);
    }

    public async Task<ServiceResponse> ServiceUserCancelRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync(HttpMethod.Get, ServiceUserCancelRequestEndpoint, payload);
    }

    public async Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> ServiceProviderGetActiveRequestsAsync()
    {
        return await _httpService.SendAsync<IEnumerable<RequestShortInfoDto>>(HttpMethod.Get, ServiceProviderGetActiveRequestEndpoint);
    }
    public async Task<ServiceResponse> ServiceProviderAssignForRequestAsync(Guid requestId, float price)
    {
        var assignForRequestCommand = new AssignForRequestCommandDto
        {
            RequestId = requestId,
            Price = price
        };

        return await _httpService.SendAsync(HttpMethod.Post, ServiceProviderAssignRequestEndpoint, assignForRequestCommand);
    }
    public async Task<ServiceResponse<RequestFullInfoProviderViewDto>> ServiceProviderGetRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync<RequestFullInfoProviderViewDto>(HttpMethod.Get, ServiceProviderGetRequestEndpoint, payload);
    }
}
