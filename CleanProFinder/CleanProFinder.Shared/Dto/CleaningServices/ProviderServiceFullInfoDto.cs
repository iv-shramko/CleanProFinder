using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.CleaningServices
{
    public class ProviderServiceFullInfoDto : EditableCleaningServiceDto
    {
        public Guid CleaningServiceId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}