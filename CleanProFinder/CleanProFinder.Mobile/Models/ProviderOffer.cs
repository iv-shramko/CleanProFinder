using CleanProFinder.Shared.Dto.CleaningServices;

namespace CleanProFinder.Mobile.Models;

public class ProviderOffer
{
    public Guid ProviderId { get; set; }
    public string ProviderName { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public IList<ProviderServiceFullInfoDto> Services { get; set; }
}