using CleanProFinder.Shared.Errors.Base;
using System.Collections.Generic;

namespace CleanProFinder.Shared.ServiceResponseHandling
{
    public class ServiceResponseBuilder
    {
        public static ServiceResponse Success() => new ServiceResponse { IsSuccess = true };

        public static ServiceResponse Failure(List<ValidationError> validationErrors)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                Error = new Error(validationErrors)
            };
        }

        public static ServiceResponse Failure(List<ServiceError> serviceErrors)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                Error = new Error(serviceErrors: serviceErrors)
            };
        }

        public static ServiceResponse Failure(ServiceError serviceErrors)
        {
            return new ServiceResponse
            {
                IsSuccess = false,
                Error = new Error(serviceErrors: new List<ServiceError>() { serviceErrors })
            };
        }

        public static ServiceResponse<TResult> Success<TResult>(TResult result) 
        {
            return new ServiceResponse<TResult> 
            { 
                IsSuccess = true,
                Result = result
            };
        } 

        public static ServiceResponse<TResult> Failure<TResult>(List<ValidationError> validationErrors)
        {
            return new ServiceResponse<TResult>
            {
                IsSuccess = false,
                Error = new Error(validationErrors)
            };
        }

        public static ServiceResponse<TResult> Failure<TResult>(List<ServiceError> serviceErrors)
        {
            return new ServiceResponse<TResult>
            {
                IsSuccess = false,
                Error = new Error(serviceErrors: serviceErrors)
            };
        }

        public static ServiceResponse<TResult> Failure<TResult>(ServiceError serviceErrors)
        {
            return new ServiceResponse<TResult>
            {
                IsSuccess = false,
                Error = new Error(serviceErrors: new List<ServiceError>() { serviceErrors })
            };
        }
    }
}
