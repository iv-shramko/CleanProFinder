using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Enums;
using System;
using System.Collections.Generic;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class RequestShortInfoDto
    {
        public Guid Id { get; set; }
        public float Square { get; set; }
        public string Address { get; set; }
        public List<CleaningServiceDto> Services { get; set; }
        public RequestStatus Status { get; set; }
    }
}
