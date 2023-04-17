using Microsoft.AspNetCore.Http;
using Yarp.ReverseProxy.Forwarder;

namespace Infrastructure.HttpHeaderEnricher;

public class CustomHttpHeaderEnricher : HttpTransformer, IHttpHeaderEnricher
{
    public async Task EnrichRequestHeaders(HttpContext from, HttpRequestMessage to)
    {
        await TransformRequestAsync(from, to, "", new CancellationToken());
    }

    public async Task EnrichResponseHeaders(HttpResponseMessage from, HttpContext to)
    {
        await TransformResponseAsync(to, from, new CancellationToken());
    }
}