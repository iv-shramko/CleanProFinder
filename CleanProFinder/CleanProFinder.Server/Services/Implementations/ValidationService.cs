using CleanProFinder.Server.Services.Interfaces;
using CleanProFinder.Shared.Errors.Base;
using CleanProFinder.Shared.ServiceResponseHandling;
using FluentValidation;
using System.Reflection;


namespace CleanProFinder.Server.Services.Implementations
{
    public class ValidationService : IValidationService
    {
        private readonly Assembly[] _assemblies;

        public ValidationService(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public async Task<ServiceResponse> ValidateAsync<T>(T item)
        {
            Type validatorType = null; 

           _assemblies.ToList().ForEach(assembly =>
            {
                if (validatorType == null)
                {
                    var types = assembly.GetTypes();
                    validatorType = types.FirstOrDefault(t => typeof(AbstractValidator<>).IsAssignableFrom(t));
                }
            });

            var validator = (IValidator)Activator.CreateInstance(validatorType);
            var validationContext = new ValidationContext<T>(item);
            var validationResult = await validator.ValidateAsync(validationContext);

            var errors = validationResult.Errors.Select(error => new ValidationError() { FieldCode = error.PropertyName, ErrorMessage = error.ErrorMessage }).ToList();

            

            return errors.Count > 0 ? ServiceResponseBuilder.Failure(errors) : ServiceResponseBuilder.Success();
        }
    }
}
