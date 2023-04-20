using Api.Controllers.AccountController.Helpers;
using Api.Controllers.ProductController;
using Api.ExceptionMappers;
using Api.Middlewares.JwtParserMiddleware;
using ExceptionCatcherMiddleware.Extensions;
using Infrastructure.AccountService;
using Infrastructure.AccountService.Helpers;
using Infrastructure.Client;
using Infrastructure.HttpHeaderEnricher;
using Infrastructure.HttpMappers;
using Infrastructure.ProductService;

namespace Api.Extensions;

public static class DiExtensions
{
    public static void AddConfiguredExceptionCatcherMiddleware(this IServiceCollection services)
    {
        services.AddExceptionCatcherMiddlewareServices(builder =>
        {
            builder.RegisterExceptionMapper<ServiceBadResponseException, ServiceBadResponseExceptionMapper>();
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

    public static void AddConfiguredCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyHeader();
            });
        });
    }

    public static void AddJwtParserMiddleware(this IServiceCollection services)
    {
        services.AddScoped<JwtParser>();
    }

    public static void AddServiceHttpClient(this IServiceCollection services)
    {
        services.AddScoped<HttpClient>();
        services.AddScoped<IServiceHttpClient, ServiceHttpClient>();
    }

    public static void AddAccountService(this IServiceCollection services, AccountServiceUrlProvider urlProvider)
    {
        services.AddSingleton(urlProvider);
        services.AddScoped<AccountControllerViewMapper>();
        services.AddScoped<IAccountService, AccountService>();
    }

    public static void AddProductService(this IServiceCollection services, ProductServiceUrlProvider urlProvider)
    {
        services.AddSingleton(urlProvider);
        services.AddScoped<ProductControllerViewMapper>();
        services.AddScoped<IProductService, ProductService>();
    }
}