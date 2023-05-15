using Newtonsoft.Json.Linq;
using UriQueryStringComposer;

namespace Infrastructure.Auther.Client;

public class SimpleHttpClient : ISimpleHttpClient
{
    private readonly HttpClient _client;

    public SimpleHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<T> Get<T>(Uri url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        await EnsureSuccess(response);
        return await ParseResponseBody<T>(response);
    }

    private async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw await ServiceBadResponseException.Create(response);
        }
    }
    
    private async Task<T> ParseResponseBody<T>(HttpResponseMessage response)
    {
        return JToken.Parse(await response.Content.ReadAsStringAsync()).ToObject<T>() ??
               throw new InvalidOperationException("Failed to parse response from account service");
    }
}