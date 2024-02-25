using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.SavedProviders
{
    public class SavedProviderDto
    {
        public Guid CleaningServiceProviderId { get; set; }
        public string Name { get; set; }
        public DateTime SubscribedAt { get; set; }
    }
}