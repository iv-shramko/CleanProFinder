using System.Collections.Generic;

namespace CleanProFinder.Shared.Errors.Base
{
    public class Error
    {
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
        public List<ServiceError> ServiceErrors { get; set; } = new List<ServiceError>();

        public Error(List<ValidationError>? validationErrors = null, List<ServiceError>? serviceErrors = null)
        {
            ValidationErrors = validationErrors ?? ValidationErrors;
            ServiceErrors = serviceErrors ?? ServiceErrors;
        }
    }
}
