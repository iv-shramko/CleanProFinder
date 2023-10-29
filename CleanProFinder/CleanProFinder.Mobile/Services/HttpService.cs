using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using CleanProFinder.Shared.Errors.Base;
using CleanProFinder.Shared.ServiceResponseHandling;
using Microsoft.Extensions.Configuration;

namespace CleanProFinder.Mobile.Services;

public class HttpService : IHttpService
{
    private const int TimeoutSeconds = 5;

    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public HttpService(IConfiguration configuration)
    {
        _httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(TimeoutSeconds)
        };
        _baseUrl = configuration["BaseUrl"];
    }

    public async Task ApplyAuthorizationAsync()
    {
        var bearerToken = await SecureStorage.GetAsync("BearerToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
    }

    public async Task<ServiceResponse<T>> SendAsync<T>(HttpMethod method, string endpoint, object payload = default)
    {
        var url = ConstructUrl(method, endpoint, payload);
        var request = CreateRequest(method, url, payload);
        return await SendRequestAsync<T>(request);
    }

    public async Task<ServiceResponse> SendAsync(HttpMethod method, string endpoint, object payload = default)
    {
        var url = ConstructUrl(method, endpoint, payload);
        var request = CreateRequest(method, url, payload);
        return await SendRequestAsync(request);
    }

    private string ConstructUrl(HttpMethod method, string endpoint, object payload)
    {
        var url = $"{_baseUrl}{endpoint}";

        if (method == HttpMethod.Get && payload != null)
        {
            if (payload is not Dictionary<string, object> parameters)
            {
                throw new Exception("Parameters for GET method should be in Dictionary<string, object>");
            }

            url += CreateQueryString(parameters);
        }

        return url;
    }

    private HttpRequestMessage CreateRequest(HttpMethod method, string url, object payload)
    {
        var request = new HttpRequestMessage(method, url);

        if (method != HttpMethod.Get)
        {
            var json = JsonConvert.SerializeObject(payload);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return request;
    }

    private async Task<ServiceResponse<T>> SendRequestAsync<T>(HttpRequestMessage request)
    {
        try
        {
            var httpResponseMessage = await _httpClient.SendAsync(request);
            var json = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<T>(json);
                return ServiceResponseBuilder.Success(result);
            }

            var error = JsonConvert.DeserializeObject<Error>(json);
            return ServiceResponseBuilder.Failure<T>(error);
        }
        catch (TaskCanceledException)
        {
            var timeoutServiceError = new ServiceError
            {
                ErrorMessage = "Connection timed out."
            };
            return ServiceResponseBuilder.Failure<T>(timeoutServiceError);
        }
    }

    private async Task<ServiceResponse> SendRequestAsync(HttpRequestMessage request)
    {
        return await SendRequestAsync<object>(request);
    }

    private static string CreateQueryString(Dictionary<string, object> parameters)
    {
        var builder = new StringBuilder("?");

        foreach (var pair in parameters)
        {
            if (pair.Value is IList list)
            {
                foreach (var item in list)
                {
                    builder.Append($"{pair.Key}={item}&");
                }
            }
            else
            {
                builder.Append($"{pair.Key}={pair.Value}&");
            }
        }

        builder.Length--;
        return builder.ToString();
    }
}
