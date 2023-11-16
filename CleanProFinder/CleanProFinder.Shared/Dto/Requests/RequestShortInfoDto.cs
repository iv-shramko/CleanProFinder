using CleanProFinder.Shared.Dto.CleaningServices;
using System.Collections.Generic;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class RequestShortInfoDto
    {
        public float Square { get; set; }
        public string Address { get; set; }
        public List<CleaningServiceDto> Services { get; set; }
    }
}
