using Infrastructure.AccountService;
using Infrastructure.AccountService.Models;

namespace Api.Middlewares.JwtParserMiddleware;

public class UserInfoProvider
{
    private readonly HttpContext _httpContext;

    public UserInfoProvider(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    private const string Key = "userInfo";

    public UserInfo ReadUserInfo()
    {
        object? rawUserInfo = _httpContext.Items[Key];
        if (rawUserInfo is null)
        {
            throw new InvalidOperationException("There is no user info in http context");
        }

        UserInfo userInfo = (UserInfo)rawUserInfo;
        return userInfo;
    }

    public void SaveUserInfo(UserInfo userInfo)
    {
        _httpContext.Items[Key] = userInfo;
    }
}