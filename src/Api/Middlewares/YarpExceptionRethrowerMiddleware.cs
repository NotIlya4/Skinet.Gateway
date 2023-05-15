using Yarp.ReverseProxy.Forwarder;

namespace Api.Middlewares;

public class YarpExceptionRethrowerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
        IForwarderErrorFeature? feature = context.GetForwarderErrorFeature();
        if (feature is not null)
        {
            Exception? exception = feature.Exception;
            if (exception is not null)
            {
                throw exception;
            }
        }
    }
}