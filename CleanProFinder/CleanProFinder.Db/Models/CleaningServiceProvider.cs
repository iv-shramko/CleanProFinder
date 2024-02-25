namespace CleanProFinder.Db.Models
{
    public class CleaningServiceProvider : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Site { get; set; }
        public string? LogoUrl { get; set; }

        public ICollection<CleaningServiceServiceProvider> CleaningServiceServiceProviders { get; set; }
        public ICollection<RequestInteraction> Interactions { get; set; }
        public ICollection<SavedProvider> SavedProviders { get; set; }
        public bool IsRestricted { get; set; }

    }
}
