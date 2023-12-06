using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Request;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class RequestService : IRequestService
{
    private const string CreateRequestEndpoint = "api/request/create";
    private const string GetOwnRequestsEndpoint = "api/request/my-requests";
    private const string CancelRequestEndpoint = "api/request/my-requests/cancel";
    private const string GetActiveRequestEndpoint = "api/request/active-requests";
    private const string AssignRequestsEndpoint = "api/request/assign-request";
    private const string ProviderGetRequestEndpoint = "api/request/request";
    private const string AcceptProviderForRequestEndpoint = "api/request/accept-provider";

    private readonly IHttpService _httpService;

    public RequestService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse> AddRequestAsync(Guid premiseId, IList<CleaningServiceDto> services,
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

        return await _httpService.SendAsync(HttpMethod.Post, CreateRequestEndpoint, createRequestCommand);
    }

    public async Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetOwnRequestsAsync()
    {
        return await _httpService.SendAsync<IEnumerable<RequestShortInfoDto>>(HttpMethod.Get, GetOwnRequestsEndpoint);
    }

    public async Task<ServiceResponse<RequestFullInfoDto>> GetOwnRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync<RequestFullInfoDto>(HttpMethod.Get, GetOwnRequestsEndpoint, payload);
    }

    public async Task<ServiceResponse> CancelRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync(HttpMethod.Get, CancelRequestEndpoint, payload);
    }

    public async Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetActiveRequestsAsync()
    {
        return await _httpService.SendAsync<IEnumerable<RequestShortInfoDto>>(HttpMethod.Get, GetActiveRequestEndpoint);
    }

    public async Task<ServiceResponse> ServiceProviderAssignForRequestAsync(Guid requestId, float price)
    {
        var assignForRequestCommand = new AssignForRequestCommandDto
        {
            RequestId = requestId,
            Price = price
        };

        return await _httpService.SendAsync(HttpMethod.Post, AssignRequestsEndpoint, assignForRequestCommand);
    }

    public async Task<ServiceResponse<RequestFullInfoProviderViewDto>> GetRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync<RequestFullInfoProviderViewDto>(HttpMethod.Get, ProviderGetRequestEndpoint, payload);
    }

    public async Task<ServiceResponse> AcceptProviderForRequestAsync(Guid providerId, Guid requestId)
    {
        var acceptProviderForRequestCommand = new AcceptProviderForRequestDto
        {
            ProviderId = providerId,
            RequestId = requestId
        };
        
        return await _httpService.SendAsync(HttpMethod.Post, AcceptProviderForRequestEndpoint, acceptProviderForRequestCommand);
    }
}
