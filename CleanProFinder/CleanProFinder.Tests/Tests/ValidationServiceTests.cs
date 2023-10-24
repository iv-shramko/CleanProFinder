using CleanProFinder.Server.Services.Implementations;
using CleanProFinder.Shared.Errors.Base;
using CleanProFinder.Shared.ServiceResponseHandling;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace CleanProFinder.Tests.Tests
{
    public class ValidationServiceTests
    {
        [Fact]
        public async Task ValidationService_Validate_Success()
        {
            var testClass = new TestClass()
            {
                Name = "Name",
                Age = 10
            };

            var validationService = ConstructorValidationService();
            var expectedResult = ServiceResponseBuilder.Success();

            var validationResult = await validationService.ValidateAsync(testClass);

            validationResult.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task ValidationService_Validate_Failure()
        {
            var testClass = new TestClass()
            {
                Name = null,
                Age = -1
            };

            var validationService = ConstructorValidationService();
            var expectedResult = ServiceResponseBuilder.Failure(
                new List<ValidationError> 
                { 
                    new ValidationError() 
                    {                        
                        FieldCode = nameof(TestClass.Name), 
                        ErrorMessage = TestValidator.NameError 
                    }, 
                    new ValidationError() 
                    { 
                        FieldCode = nameof(TestClass.Age), 
                        ErrorMessage = TestValidator.AgeError 
                    } 
                });

            var validationResult = await validationService.ValidateAsync(testClass);

            validationResult.ShouldBeEquivalentTo(expectedResult);
        }

        private static ValidationService ConstructorValidationService()
        {
            var services = new ServiceCollection();
            services.AddTransient<IValidator<TestClass>, TestValidator>();
            return new ValidationService(services.BuildServiceProvider());
        }

        private class TestClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class TestValidator : AbstractValidator<TestClass> 
        {
            public static string NameError = "Must not be empty";
            public static string AgeError = "Must be greater than 0";
            public TestValidator() 
            {
                RuleFor(t => t.Name)
                    .NotEmpty()
                    .WithMessage(NameError);

                RuleFor(t => t.Age)
                    .GreaterThan(0)
                    .WithMessage(AgeError);
            }
        }

    }
}