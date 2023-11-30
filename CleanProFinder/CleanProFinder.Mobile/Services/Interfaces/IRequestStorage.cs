using CleanProFinder.Shared.Dto.CleaningServices;

namespace CleanProFinder.Mobile.Services.Interfaces;

public interface IRequestStorage
{
    string PremiseId { get; set; }
    List<CleaningServiceDto> Services { get; set; }
    string Description { get; set; }
    string ServiceProviderId { get; set; }

    public void Reset();
}
