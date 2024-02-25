using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CleanProFinder.Db.Models
{
    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Entity ent && obj.GetType().Equals(GetType()))
            {
                return ent.Id == Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
