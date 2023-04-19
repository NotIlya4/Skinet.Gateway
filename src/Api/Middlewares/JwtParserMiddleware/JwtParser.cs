namespace Api.Middlewares.JwtParserMiddleware;

public class JwtParser : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (HasAttribute(context))
        {
            JwtTokenRequestStorage storage = new(context);

            string jwtToken = context.Request.Headers["Authorization"].ToString();
        
            storage.SaveJwtToken(jwtToken);
        }

        await next(context);
    }

    private bool HasAttribute(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        ParseJwtHeaderAttribute? attribute = endpoint?.Metadata.GetMetadata<ParseJwtHeaderAttribute>();
        return attribute is not null;
    }
}