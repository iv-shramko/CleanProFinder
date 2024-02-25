using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Server.Services.Interfaces
{
    public interface IValidationService
    {
        public Task<ServiceResponse> ValidateAsync<T>(T item);
    }
}
