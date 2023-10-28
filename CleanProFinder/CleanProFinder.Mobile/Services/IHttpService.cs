using CleanProFinder.Shared.ServiceResponseHandling;

namespace CleanProFinder.Mobile.Services;

public interface IHttpService
{
    Task ApplyAuthorizationAsync();
    Task<ServiceResponse<T>> SendAsync<T>(HttpMethod method, string endpoint, object payload = default);
    Task<ServiceResponse> SendAsync(HttpMethod method, string endpoint, object payload = default);
}