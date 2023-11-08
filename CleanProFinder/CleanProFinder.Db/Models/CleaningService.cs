using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProFinder.Db.Models
{
    public class CleaningService : Entity
    {
        public Guid ServiceProviderId { get; set; }
        public CleaningServiceProvider ServiceProvider { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}