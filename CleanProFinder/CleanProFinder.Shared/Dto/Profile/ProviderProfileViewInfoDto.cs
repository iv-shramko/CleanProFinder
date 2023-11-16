using CleanProFinder.Shared.Dto.CleaningServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.Profile
{
    public class ProviderProfileViewInfoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public ICollection<ProviderServiceFullInfoDto> Services { get; set; }

    }
}