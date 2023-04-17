using ExceptionCatcherMiddleware.Extensions;

namespace Api.Extensions;

public static class DiExtensions
{
    public static void AddConfiguredExceptionCatcherMiddlewareServices(this IServiceCollection services)
    {
        services.AddExceptionCatcherMiddlewareServices(_ =>
        {
            
        });
    }
}