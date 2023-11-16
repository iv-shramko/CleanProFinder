using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.CleaningServices
{
    public class EditProviderServiceDto
    {   
        public Guid CleaningServiceId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}