using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Request;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class RequestService : IRequestService
{
    private const string CreateServiceUserPremiseEndpoint = "api/request/create";
    private const string GetServiceUserRequestsEndpoint = "api/request/my-requests";
    private const string CancelServiceUserRequestEndpoint = "api/request/my-requests/cancel";

    private readonly IHttpService _httpService;

    public RequestService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse> AddServiceUserRequestAsync(Guid premiseId, List<Guid> servicesId, string description, Guid? selectedProviderId)
    {
        var createRequestCommand = new CreateRequestCommandDto
        {
            PremiseId = premiseId,
            ServicesId = servicesId,
            Description = description,
            SelectedProviderId = selectedProviderId
        };

        return await _httpService.SendAsync(HttpMethod.Post, CreateServiceUserPremiseEndpoint, createRequestCommand);
    }

    public async Task<ServiceResponse<IEnumerable<RequestShortInfoDto>>> GetServiceUserRequestsAsync()
    {
        return await _httpService.SendAsync<IEnumerable<RequestShortInfoDto>>(HttpMethod.Get, GetServiceUserRequestsEndpoint);
    }

    public async Task<ServiceResponse<RequestFullInfoDto>> GetServiceUserRequestAsync(string payload)
    {
        return await _httpService.SendAsync<RequestFullInfoDto>(HttpMethod.Get, GetServiceUserRequestsEndpoint, payload);
    }

    public async Task<ServiceResponse> CancelServiceUserRequestAsync(string payload)
    {
        return await _httpService.SendAsync(HttpMethod.Get, CancelServiceUserRequestEndpoint, payload);
    }
}
