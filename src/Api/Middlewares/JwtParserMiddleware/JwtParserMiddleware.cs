using System.IdentityModel.Tokens.Jwt;
using Infrastructure.AccountService;
using Infrastructure.AccountService.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middlewares.JwtParserMiddleware;

public class JwtParserMiddleware : IMiddleware
{
    private readonly IAccountService _accountService;
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

    public JwtParserMiddleware(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (HasAttribute(context))
        {
            await Handle(context);
        }

        await next(context);
    }

    private async Task Handle(HttpContext context)
    {
        string jwtToken = context.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(jwtToken))
        {
            throw new JwtTokenNotProvidedException();
        }
        
        jwtToken = jwtToken.Replace("Bearer ", "");
        UserInfo userInfo = await _accountService.GetUser(jwtToken);

        UserInfoProvider storage = new(context);
        storage.SaveUserInfo(userInfo);
    }

    private bool HasAttribute(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        ParseJwtHeaderAttribute? attribute = endpoint?.Metadata.GetMetadata<ParseJwtHeaderAttribute>();
        return attribute is not null;
    }
}