using System.Text;
using Infrastructure.Auther.SimpleHttpClient;

namespace Api.Middlewares;

public class ServiceBadResponseExceptionCatcherMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ServiceBadResponseException e)
        {
            context.Response.StatusCode = e.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(e.ResponseDto.ToString()));
        }
    }
}