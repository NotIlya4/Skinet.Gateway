using Microsoft.AspNetCore.Http;

namespace Infrastructure.HttpMappers;

public class HttpForwarder
{
    private readonly HttpRequestMapper _httpRequestMapper;
    private readonly HttpResponseMapper _httpResponseMapper;
    private readonly HttpMessageInvoker _client;

    public HttpForwarder(HttpRequestMapper httpRequestMapper, HttpResponseMapper httpResponseMapper, HttpMessageInvoker client)
    {
        _httpRequestMapper = httpRequestMapper;
        _httpResponseMapper = httpResponseMapper;
        _client = client;
    }

    public async Task ForwardAndWriteToContext(HttpContext context, string baseUrl)
    {
        HttpRequestMessage request = await _httpRequestMapper.Map(context, baseUrl);
        HttpResponseMessage response = await _client.SendAsync(request, new CancellationToken());
        await _httpResponseMapper.CopyAll(response, context);
    }
}