using Microsoft.AspNetCore.Http;

namespace Infrastructure.HttpHeaderEnricher;

public interface IHttpHeaderEnricher
{
    public Task EnrichRequestHeaders(HttpContext from, HttpRequestMessage to);
    public Task EnrichResponseHeaders(HttpResponseMessage from, HttpContext to);
}