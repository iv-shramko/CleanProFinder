using CleanProFinder.Shared.Errors.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class EditProfileError : ServiceError
    {
        public EditProfileError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }
        public static EditProfileError ProfileUpdateError =>
            new EditProfileError("Profile edit error", "Error when updating the profile", 1);
    }
}