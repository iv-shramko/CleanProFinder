using System;

namespace CleanProFinder.Shared.Dto.Notifications
{
    public class ProviderAssignedToRequestMessage
    {
        public Guid RequestId { get; set; }
        public string RequestPremiseAddress { get; set; }
        public Guid ProviderId { get; set; }
        public string ProviderName { get; set; }
    }
}
