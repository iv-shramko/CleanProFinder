using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
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

    public async Task<ServiceResponse> AddServiceUserRequestAsync(Guid premiseId, IList<CleaningServiceDto> services, string description, Guid? selectedProviderId)
    {
        var servicesId = services.Select(s => s.Id).ToList();

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

    public async Task<ServiceResponse<RequestFullInfoDto>> GetServiceUserRequestAsync(Guid requestId)
    {
        return await _httpService.SendAsync<RequestFullInfoDto>(HttpMethod.Get, GetServiceUserRequestsEndpoint, requestId.ToString());
    }

    public async Task<ServiceResponse> CancelServiceUserRequestAsync(Guid requestId)
    {
        return await _httpService.SendAsync(HttpMethod.Get, CancelServiceUserRequestEndpoint, requestId.ToString());
    }
}
