using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IPremiseService
{
    Task<ServiceResponse> AddPremiseAsync(float square, string description, string address);
    Task<ServiceResponse> EditPremiseAsync(Guid id, float square, string description, string address);
    Task<ServiceResponse<IEnumerable<OwnPremiseShortInfoDto>>> GetPremisesAsync();
    Task<ServiceResponse<OwnPremiseFullInfoDto>> GetPremiseAsync(Dictionary<string, object> payload);
    Task<ServiceResponse> DeletePremiseAsync(Dictionary<string, object> payload);
}
