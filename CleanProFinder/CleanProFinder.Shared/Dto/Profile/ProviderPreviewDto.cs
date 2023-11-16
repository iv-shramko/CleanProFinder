using CleanProFinder.Shared.Dto.CleaningServices;
using System;
using System.Collections.Generic;

namespace CleanProFinder.Shared.Dto.Profile
{
    public class ProviderPreviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ProviderServiceFullInfoDto> Services { get; set; }
    }
}