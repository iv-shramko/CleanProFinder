using System;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class ProviderRequestInteractionInfo
    {
        public Guid ProviderId { get; set; }
        public string ProviderName { get; set; }
        public float Price { get; set; }
    }
}
