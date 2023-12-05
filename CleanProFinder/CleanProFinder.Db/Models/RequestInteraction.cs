using CleanProFinder.Shared.Enums;

namespace CleanProFinder.Db.Models
{
    public class RequestInteraction : Entity
    {
        public Guid RequestId { get; set; }
        public Request Request { get; set; }

        public Guid? ProviderId { get; set; }
        public CleaningServiceProvider? Provider { get; set; }

        public float? Price { get; set; }
        public RequestInteractionStatus InteractionStatus { get; set; }
    }
}
