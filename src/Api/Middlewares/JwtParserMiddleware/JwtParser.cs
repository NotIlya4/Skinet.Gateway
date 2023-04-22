using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middlewares.JwtParserMiddleware;

public class JwtParser : IMiddleware
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly TokenValidationParameters _validationParameters = new() 
    {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        SignatureValidator = (token, _) =>
        {
            var jwt = new JwtSecurityToken(token);
            return jwt;
        },
    };
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (HasAttribute(context))
        {
            string jwtToken = context.Request.Headers["Authorization"].ToString();
            jwtToken = jwtToken.Replace("Bearer ", "");
            try
            {
                _tokenHandler.ValidateToken(jwtToken, _validationParameters, out _);
            }
            catch (ArgumentNullException)
            {
                throw new JwtTokenNotProvidedException();
            }

            JwtTokenRequestStorage storage = new(context);
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