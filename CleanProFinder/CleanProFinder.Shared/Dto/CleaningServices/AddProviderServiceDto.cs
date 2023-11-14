using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.CleaningServices
{
    public class AddProviderServiceDto : EditableCleaningServiceDto
    {   
        public Guid CleaningServiceProviderId { get; set; }
        public Guid CleaningServiceId { get; set; }
        public decimal Price { get; set; }
    }
}