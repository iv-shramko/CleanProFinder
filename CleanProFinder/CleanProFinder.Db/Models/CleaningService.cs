namespace CleanProFinder.Db.Models
{
    public class CleaningService : Entity
    {
        public string Name { get; set; }
        public ICollection<CleaningServiceServiceProvider> CleaningServiceServiceProviders { get; set; }
    }
}