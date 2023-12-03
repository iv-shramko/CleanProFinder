using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Request;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class RequestService : IRequestService
{
    private const string CreateRequestEndpoint = "api/request/create";
    private const string GetRequestsEndpoint = "api/request/my-requests";
    private const string CancelRequestEndpoint = "api/request/my-requests/cancel";

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

    public async Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetRequestsAsync()
    {
        return await _httpService.SendAsync<IEnumerable<RequestShortInfoDto>>(HttpMethod.Get, GetRequestsEndpoint);
    }

    public async Task<ServiceResponse<RequestFullInfoDto>> GetRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync<RequestFullInfoDto>(HttpMethod.Get, GetRequestsEndpoint, payload);
    }

    public async Task<ServiceResponse> CancelRequestAsync(Guid requestId)
    {
        var payload = requestId.ToString();
        return await _httpService.SendAsync(HttpMethod.Get, CancelRequestEndpoint, payload);
    }
}
