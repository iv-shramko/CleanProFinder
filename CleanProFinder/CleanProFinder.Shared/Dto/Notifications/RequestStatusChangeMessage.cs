using System;

namespace CleanProFinder.Shared.Dto.Notifications
{
    public class RequestStatusChangeMessage
    {
        public Guid RequestId { get; set; }
        public string RequestPremiseAddress { get; set; }
        public string NewStatus { get; set; }
    }
}
