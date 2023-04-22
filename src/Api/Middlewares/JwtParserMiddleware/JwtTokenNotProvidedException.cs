namespace Api.Middlewares.JwtParserMiddleware;

public class JwtTokenNotProvidedException : Exception
{
    public JwtTokenNotProvidedException() : base("Jwt token must be provided for this endpoint")
    {
        
    }
}