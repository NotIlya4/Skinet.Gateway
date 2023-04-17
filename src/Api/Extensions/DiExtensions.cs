using ExceptionCatcherMiddleware.Extensions;
using Infrastructure.HttpHeaderEnricher;
using Infrastructure.HttpMappers;

namespace Api.Extensions;

public static class DiExtensions
{
    public static void AddConfiguredExceptionCatcherMiddlewareServices(this IServiceCollection services)
    {
        services.AddExceptionCatcherMiddlewareServices(_ =>
        {
            
        });
    }

    public static void AddForwarder(this IServiceCollection services)
    {
        services.AddScoped<HttpMessageInvoker, HttpClient>();
        services.AddScoped<HttpForwarder>();
        services.AddScoped<IHttpHeaderEnricher, CustomHttpHeaderEnricher>();
        services.AddScoped<HttpRequestMapper>();
        services.AddScoped<HttpResponseMapper>();
    }
}