using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auther.JwtTokenProvider;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly IHttpContextAccessor _accessor;

    public JwtTokenProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Read()
    {
        HttpContext? context = _accessor.HttpContext;
        if (context is null)
        {
            throw new InvalidOperationException("You are trying to access jwt token in non request scope");
        }
        
        string jwtToken = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            throw new JwtTokenNotProvidedException();
        }
        
        return jwtToken.Replace("Bearer ", "");
    }
}