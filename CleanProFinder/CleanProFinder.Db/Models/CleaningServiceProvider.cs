namespace CleanProFinder.Db.Models
{
    public class CleaningServiceProvider : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Site { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }
}
