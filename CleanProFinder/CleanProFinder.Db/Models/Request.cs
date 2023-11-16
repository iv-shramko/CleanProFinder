namespace CleanProFinder.Db.Models
{
    public class Request : Entity
    {
        public Premise Premise { get; set; }
        public Guid PremiseId { get; set; }

        public ICollection<CleaningService> Services { get; set; }

        public string Description { get; set; }
    }
}
