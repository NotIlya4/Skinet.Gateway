using Infrastructure.CorrelationIdSystem.Repository;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Auther.SimpleHttpClient;

public class SimpleHttpClient : ISimpleHttpClient
{
    private readonly HttpClient _client;
    private readonly ICorrelationIdProvider _provider;

    public SimpleHttpClient(HttpClient client, ICorrelationIdProvider provider)
    {
        _client = client;
        _provider = provider;
    }

    public async Task<T> Get<T>(Uri url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        Guid? id = _provider.GetCorrelationId();
        if (id.HasValue)
        {
            request.Headers.Add("x-request-id", id.Value.ToString());
        }

        HttpResponseMessage response = await _client.SendAsync(request);
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