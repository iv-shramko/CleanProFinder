using CleanProFinder.Shared.Dto.Premises;
using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IPremiseService
{
    Task<ServiceResponse> AddPremiseAsync(float square, string description, string address);
    Task<ServiceResponse> EditPremiseAsync(Guid premiseId, float square, string description, string address);
    Task<ServiceResponse<IEnumerable<OwnPremiseShortInfoDto>>> GetOwnPremisesAsync();
    Task<ServiceResponse<OwnPremiseFullInfoDto>> GetPremiseAsync(Guid premiseId);
    Task<ServiceResponse> DeletePremiseAsync(Guid premiseId);
}
