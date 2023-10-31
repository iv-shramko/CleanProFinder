using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Dto.Profile
{
    public class ViewUserProfileInfoDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}