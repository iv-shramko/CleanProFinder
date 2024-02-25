using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProFinder.Db.Models
{
    public class CleaningServiceServiceProvider : Entity
    {
        public Guid CleaningServiceId { get; set; }
        public CleaningService CleaningService { get; set; }

        public Guid CleaningServiceProviderId { get; set; }
        public CleaningServiceProvider CleaningServiceProvider { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}