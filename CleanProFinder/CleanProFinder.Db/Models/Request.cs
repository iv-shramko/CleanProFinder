using CleanProFinder.Shared.Enums;

namespace CleanProFinder.Db.Models
{
    public class Request : Entity
    {
        public Premise Premise { get; set; }
        public Guid PremiseId { get; set; }

        public ICollection<CleaningService> Services { get; set; }

        public string Description { get; set; }

        public Guid? ProviderId { get; set; }
        public CleaningServiceProvider? Provider { get; set; }

        public float? ProviderPrice { get; set; }

        public RequestStatus Status { get; set; }
    }
}
