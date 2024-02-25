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
        public static CleaningServiceError EditProviderServices =>
            new CleaningServiceError("Edit Provider Services Error", "Can not edit provider's services list", 2);        
        public static CleaningServiceError InvaidServiceId =>
            new CleaningServiceError("Invalid Cleaning Service Id Error", "Ivalid service Id", 3);   
        public static CleaningServiceError ServiceAlreadyAdded =>
            new CleaningServiceError("Service Already Added Error", "This service is already added to your services list", 4);
    }
}