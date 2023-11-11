using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProFinder.Db.Models
{
    public class CleaningService : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CleaningServiceServiceProvider> CleaningServiceServiceProviders { get; set; }
    }
}