using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;

namespace CleanProFinder.Mobile.Services.Implementations;

public class RequestStorage : IRequestStorage
{
    public string PremiseId { get; set; }
    public List<CleaningServiceDto> Services { get; set; }
    public string Description { get; set; }
    public string ServiceProviderId { get; set; }

    public RequestStorage()
    {
        Services = new List<CleaningServiceDto>();
    }

    public void Reset()
    {
        PremiseId = null;
        Services = new List<CleaningServiceDto>();
        Description = null;
        ServiceProviderId = null;
    }
}
