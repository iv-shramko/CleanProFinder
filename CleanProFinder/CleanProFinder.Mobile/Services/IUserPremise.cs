using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IUserPremiseService
{
    Task<ServiceResponse> AddServiceUserPremiseAsync(float square, string description, string address);
    Task<ServiceResponse> EditServiceUserPremiseAsync(Guid id, float square, string description, string address);
    Task<ServiceResponse<IEnumerable<OwnPremiseShortInfoDto>>> GetServiceUserPremisesAsync();
    Task<ServiceResponse<OwnPremiseFullInfoDto>> GetServiceUserPremiseAsync(Dictionary<string, object> payload);
    Task<ServiceResponse> DeleteServiceUserPremiseAsync(Dictionary<string, object> payload);
}
