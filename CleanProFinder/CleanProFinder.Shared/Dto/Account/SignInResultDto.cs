using System;

namespace CleanProFinder.Shared.Dto.Account
{
    public class SignInResultDto
    {
        public Guid UserId { get; set; }
        public string Bearer { get; set; }
    }
}
