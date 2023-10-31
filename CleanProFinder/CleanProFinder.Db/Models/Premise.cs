namespace CleanProFinder.Db.Models
{
    public class Premise : Entity
    {
        public Guid UserId { get; set; }
        public ServiceUser User { get; set; }

        public float Square { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
