using System.Text;
using Infrastructure.HttpHeaderEnricher;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.HttpMappers;

public class HttpResponseMapper
{
    private readonly IHttpHeaderEnricher _httpHeaderEnricher;

    public HttpResponseMapper(IHttpHeaderEnricher httpHeaderEnricher)
    {
        _httpHeaderEnricher = httpHeaderEnricher;
    }

    public async Task Enrich(HttpResponseMessage from, HttpContext to)
    {
        await SetHeaders(from, to);
        SetStatus(from, to);
        await SetBody(from, to);
    }

    public async Task SetHeaders(HttpResponseMessage from, HttpContext to)
    {
        await _httpHeaderEnricher.EnrichResponseHeaders(from, to);
    }

    public void SetStatus(HttpResponseMessage from, HttpContext to)
    {
        to.Response.StatusCode = (int)from.StatusCode;
    }

    public async Task SetBody(HttpResponseMessage from, HttpContext to)
    {
        await to.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(await from.Content.ReadAsStringAsync()));
    }
}