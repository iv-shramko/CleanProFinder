using System;

namespace CleanProFinder.Shared.Dto.Requests
{
    public class AssignForRequestCommandDto
    {
        public Guid RequestId { get; set; }
        public float Price { get; set; }
    }
}
