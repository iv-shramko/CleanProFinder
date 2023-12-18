using CleanProFinder.Shared.Dto.CleaningServices;
using System.Collections.Generic;
using System;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class RequestFullInfoProviderViewDto
    {
        public Guid Id { get; set; }

        public Guid PremiseId { get; set; }
        public string Description { get; set; }
        public float Square { get; set; }
        public string Address { get; set; }

        public List<CleaningServiceDto> Services { get; set; }

        public string Status { get; set; }
    }
}
