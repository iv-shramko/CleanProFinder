using System;
using System.Collections.Generic;

namespace CleanProFinder.Shared.Dto.Request
{
    public class CreateRequestCommandDto
    {
        public Guid PremiseId { get; set; }
        public List<Guid> ServicesId { get; set; }
        public string Description { get; set; }
    }
}
