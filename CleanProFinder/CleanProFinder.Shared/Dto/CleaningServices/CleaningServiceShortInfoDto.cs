using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.CleaningServices
{
    public class CleaningServiceShortInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}