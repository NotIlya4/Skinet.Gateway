using Infrastructure.CorrelationIdSystem.Repository;

namespace Api.Middlewares;

public class CorrelationIdGeneratorMiddleware : IMiddleware
{
    private readonly ICorrelationIdSaver _saver;

    public CorrelationIdGeneratorMiddleware(ICorrelationIdSaver saver)
    {
        _saver = saver;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _saver.GenerateAndSave();
        await next(context);
    }
}