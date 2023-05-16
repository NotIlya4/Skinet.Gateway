using Microsoft.AspNetCore.Http;

namespace Infrastructure.Auther.JwtTokenProvider;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly HttpContext _context;

    public JwtTokenProvider(HttpContext context)
    {
        _context = context;
    }

    public string Read()
    {
        string jwtToken = _context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            throw new JwtTokenNotProvidedException();
        }
        
        return jwtToken.Replace("Bearer ", "");
    }
}