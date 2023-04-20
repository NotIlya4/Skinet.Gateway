namespace Api.Middlewares.JwtParserMiddleware;

public class JwtTokenRequestStorage
{
    private readonly HttpContext _httpContext;

    public JwtTokenRequestStorage(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    private const string Key = "jwtToken";

    public string ReadJwtToken()
    {
        object? rawToken = _httpContext.Items[Key];
        if (rawToken is null)
        {
            throw new InvalidOperationException("There is no token in http context");
        }

        string token = (string)rawToken;
        return token;
    }

    public void SaveJwtToken(string jwt)
    {
        jwt = jwt.Replace("Bearer ", "");
        _httpContext.Items[Key] = jwt;
    }
}