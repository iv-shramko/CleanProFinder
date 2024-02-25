using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class AcceptProviderForRequestDto
    {
        public Guid ProviderId { get; set; }
        public Guid RequestId { get; set; }
    }
}