namespace Api.Middlewares;

public class RequestBodyEnableBuffering : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.EnableBuffering();
        await next(context);
    }
}