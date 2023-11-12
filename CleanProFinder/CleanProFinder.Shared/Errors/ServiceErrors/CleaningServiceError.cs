using CleanProFinder.Shared.Errors.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Errors.ServiceErrors
{
    public class CleaningServiceError : ServiceError
    {
        public CleaningServiceError(string header, string message, int code)
        {
            Header = header;
            ErrorMessage = message;
            Code = code;
        }
        public static CleaningServiceError MatchCleaningServiceError =>
            new CleaningServiceError("Edit Cleaning Service Error", "Can not find cleaning service", 1);
    }
}