using Infrastructure.HttpHeaderEnricher;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.HttpMappers;

public class HttpRequestMapper
{
    private readonly IHttpHeaderEnricher _httpHeaderEnricher;

    public HttpRequestMapper(IHttpHeaderEnricher httpHeaderEnricher)
    {
        _httpHeaderEnricher = httpHeaderEnricher;
    }

    public async Task<HttpRequestMessage> Map(HttpContext from, string baseUrl)
    {
        HttpRequestMessage message = new();
        await Enrich(from, message, baseUrl);
        return message;
    }

    public async Task Enrich(HttpContext from, HttpRequestMessage to, string baseUrl)
    {
        SetContent(to, from);
        SetVerb(to, from);
        SetUrl(to, from, baseUrl);
        await SetHeaders(to, from);
    }
    
    private void SetContent(HttpRequestMessage message, HttpContext context)
    {
        context.Request.Body.Seek(0, SeekOrigin.Begin);
        message.Content = new StreamContent(context.Request.Body);
    }

    private void SetVerb(HttpRequestMessage message, HttpContext context)
    {
        message.Method = new HttpMethod(context.Request.Method);
    }

    private void SetUrl(HttpRequestMessage message, HttpContext context, string baseUrl)
    {
        SetUrl(message, context, new Uri(baseUrl));
    }
    
    private void SetUrl(HttpRequestMessage message, HttpContext context, Uri baseUrl)
    {
        string path = context.Request.Path.Value ?? "";
        string queryString = context.Request.QueryString.Value ?? "";
        message.RequestUri = new Uri(baseUrl, $"{path}{queryString}");
    }

    private async Task SetHeaders(HttpRequestMessage message, HttpContext context)
    {
        await _httpHeaderEnricher.EnrichRequestHeaders(context, message);
    }
}