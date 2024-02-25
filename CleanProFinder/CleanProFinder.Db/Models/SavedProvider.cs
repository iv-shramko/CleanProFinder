using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProFinder.Db.Models
{
    public class SavedProvider : Entity
    {
        public Guid ServiceUserId { get; set; }
        public ServiceUser ServiceUser { get; set; }

        public Guid CleaningServiceProviderId { get; set; }
        public CleaningServiceProvider CleaningServiceProvider { get; set; }

        public DateTime SubscribedAt { get; set; }

    }
}