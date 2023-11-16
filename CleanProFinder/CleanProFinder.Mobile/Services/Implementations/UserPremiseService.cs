using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class UserPremiseService : IUserPremiseService
{
    private const string CreateServiceUserPremiseEndpoint = "api/premise/create";
    private const string EditServiceUserPremiseEndpoint = "api/premise/edit";
    private const string GetServiceUserPremisesEndpoint = "api/premise/my-premises";
    private const string GetServiceUserPremiseEndpoint = "api/premise/full-info";
    private const string DeleteServiceUserPremiseEndpoint = "api/premise";

    private readonly IHttpService _httpService;

    public UserPremiseService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse> AddServiceUserPremiseAsync(float square, string description, string address)
    {
        var createPremiseCommand = new CreatePremiseCommandDto
        {
            Square = square,
            Description = description,
            Address = address
        };

        return await _httpService.SendAsync(HttpMethod.Post, CreateServiceUserPremiseEndpoint, createPremiseCommand);
    }

    public async Task<ServiceResponse> EditServiceUserPremiseAsync(Guid id, float square, string description, string address)
    {
        var editPremiseCommand = new EditPremiseCommandDto
        {
            Id = id,
            Square = square,
            Description = description,
            Address = address
        };

        return await _httpService.SendAsync(HttpMethod.Post, EditServiceUserPremiseEndpoint, editPremiseCommand);
    }

    public async Task<ServiceResponse<IEnumerable<OwnPremiseShortInfoDto>>> GetServiceUserPremisesAsync()
    {
        return await _httpService.SendAsync<IEnumerable<OwnPremiseShortInfoDto>>(HttpMethod.Get, GetServiceUserPremisesEndpoint);
    }

    public async Task<ServiceResponse<OwnPremiseFullInfoDto>> GetServiceUserPremiseAsync(Dictionary<string, object> payload)
    {
        return await _httpService.SendAsync<OwnPremiseFullInfoDto>(HttpMethod.Get, GetServiceUserPremiseEndpoint, payload);
    }

    public async Task<ServiceResponse> DeleteServiceUserPremiseAsync(Dictionary<string, object> payload)
    {
        return await _httpService.SendAsync(HttpMethod.Delete, DeleteServiceUserPremiseEndpoint, payload);
    }
}
