using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Errors.Base;
using CleanProFinder.Shared.ServiceResponseHandling;
using FluentValidation;
using System.Reflection;


namespace CleanProFinder.Server.Services.Implementations
{
    public class ValidationService : IValidationService
    {
        public async Task<ServiceResponse> ValidateAsync<T>(T item)
        {
            var validatorType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.BaseType != null && t.BaseType.IsGenericType &&
                                     t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>) &&
                                     t.BaseType.GetGenericArguments().Any(genericArg => genericArg == typeof(T)));


            var validator = (IValidator)Activator.CreateInstance(validatorType);
            var validationContext = new ValidationContext<T>(item);
            var validationResult = await validator.ValidateAsync(validationContext);

            var errors = validationResult.Errors.Select(error => new ValidationError() { FieldCode = error.PropertyName, ErrorMessage = error.ErrorMessage });

            return ServiceResponseBuilder.Failure(new List<ValidationError>());
        }
    }
}
