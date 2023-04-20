using System.Text;
using Newtonsoft.Json.Linq;
using UriQueryStringComposer;

namespace Infrastructure.Client;

public class ServiceHttpClient : IServiceHttpClient
{
    private readonly HttpClient _client;

    public ServiceHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<T> Get<T>(Uri url, object? queryParams = null)
    {
        HttpResponseMessage response = await _client.GetAsync(QueryStringComposer.Compose(url, queryParams));
        await EnsureSuccess(response);
        return await ParseResponseBody<T>(response);
    }

    public async Task Get(Uri url, object? queryParams = null)
    {
        HttpResponseMessage response = await _client.GetAsync(QueryStringComposer.Compose(url, queryParams));
        await EnsureSuccess(response);
    }

    public async Task<string> GetRaw(Uri url, object? queryParams = null)
    {
        HttpResponseMessage response = await _client.GetAsync(QueryStringComposer.Compose(url, queryParams));
        await EnsureSuccess(response);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<T> Post<T>(Uri url, object? body = null)
    {
        HttpContent? requestBody = MakeHttpContent(body);
        HttpResponseMessage response = await _client.PostAsync(url, requestBody);
        await EnsureSuccess(response);
        return await ParseResponseBody<T>(response);
    }

    public async Task Post(Uri url, object? body = null)
    {
        HttpContent? requestBody = MakeHttpContent(body);
        HttpResponseMessage response = await _client.PostAsync(url, requestBody);
        await EnsureSuccess(response);
    }

    public async Task<string> PostRaw(Uri url, object? body = null)
    {
        HttpContent? requestBody = MakeHttpContent(body);
        HttpResponseMessage response = await _client.PostAsync(url, requestBody);
        await EnsureSuccess(response);
        return await response.Content.ReadAsStringAsync();
    }

    private async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw await ServiceBadResponseException.Create(response);
        }
    }
    
    private StringContent? MakeHttpContent(object? body)
    {
        if (body is null)
        {
            return null;
        }
        JObject bodyJObject = JObject.FromObject(body);
        return new StringContent(bodyJObject.ToString(), Encoding.UTF8, "application/json");
    }
    
    private async Task<T> ParseResponseBody<T>(HttpResponseMessage response)
    {
        return JObject.Parse(await response.Content.ReadAsStringAsync()).ToObject<T>() ??
               throw new InvalidOperationException("Failed to parse response from account service");
    }
}