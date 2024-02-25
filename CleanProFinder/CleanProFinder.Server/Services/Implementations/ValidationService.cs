using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Errors.Base;
using CleanProFinder.Shared.ServiceResponseHandling;
using FluentValidation;

namespace CleanProFinder.Server.Services.Implementations
{
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ServiceResponse> ValidateAsync<T>(T item)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            
            if(validator is null)
            {
                return ServiceResponseBuilder.Success();
            }

            var validationContext = new ValidationContext<T>(item);
            var validationResult = await validator.ValidateAsync(validationContext);

            var errors = validationResult.Errors.Select(error => 
                new ValidationError() 
                { 
                    FieldCode = error.PropertyName, 
                    ErrorMessage = error.ErrorMessage 
                })
            .ToList();            

            return errors.Count > 0 ? ServiceResponseBuilder.Failure(errors) : ServiceResponseBuilder.Success();
        }
    }
}
