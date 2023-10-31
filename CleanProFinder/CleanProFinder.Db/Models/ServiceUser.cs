namespace CleanProFinder.Db.Models
{
    public class ServiceUser : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public ICollection<Premise>? UserPremises { get; set; }
    }
}
