using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Implementations;

public class PremiseService : IPremiseService
{
    private const string CreatePremiseEndpoint = "api/premise/create";
    private const string EditPremiseEndpoint = "api/premise/edit";
    private const string GetOwnPremisesEndpoint = "api/premise/my-premises";
    private const string GetPremiseEndpoint = "api/premise/full-info";
    private const string DeletePremiseEndpoint = "api/premise";

    private readonly IHttpService _httpService;

    public PremiseService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<ServiceResponse> AddPremiseAsync(float square, string description, string address)
    {
        var createPremiseCommand = new CreatePremiseCommandDto
        {
            Square = square,
            Description = description,
            Address = address
        };

        return await _httpService.SendAsync(HttpMethod.Post, CreatePremiseEndpoint, createPremiseCommand);
    }

    public async Task<ServiceResponse> EditPremiseAsync(Guid premiseId, float square, string description, string address)
    {
        var editPremiseCommand = new EditPremiseCommandDto
        {
            Id = premiseId,
            Square = square,
            Description = description,
            Address = address
        };

        return await _httpService.SendAsync(HttpMethod.Post, EditPremiseEndpoint, editPremiseCommand);
    }

    public async Task<ServiceResponse<IEnumerable<OwnPremiseShortInfoDto>>> GetOwnPremisesAsync()
    {
        return await _httpService.SendAsync<IEnumerable<OwnPremiseShortInfoDto>>(HttpMethod.Get, GetOwnPremisesEndpoint);
    }

    public async Task<ServiceResponse<OwnPremiseFullInfoDto>> GetPremiseAsync(Guid premiseId)
    {
        var payload = new Dictionary<string, object>
        {
            { "premiseId", premiseId }
        };

        return await _httpService.SendAsync<OwnPremiseFullInfoDto>(HttpMethod.Get, GetPremiseEndpoint, payload);
    }

    public async Task<ServiceResponse> DeletePremiseAsync(Guid premiseId)
    {
        var payload = new Dictionary<string, object>
        {
            { "premiseId", premiseId }
        };

        return await _httpService.SendAsync(HttpMethod.Delete, DeletePremiseEndpoint, payload);
    }
}
