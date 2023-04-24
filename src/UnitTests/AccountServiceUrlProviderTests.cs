using Infrastructure.AccountService.Helpers;

namespace UnitTests;

public class AccountServiceUrlProviderTests
{
    public AccountServiceUrlProvider UrlProvider { get; }
    
    public AccountServiceUrlProviderTests()
    {
        UrlProvider = new AccountServiceUrlProvider(
            baseUrl: "http://account-service",
            loginPath: "users/login",
            logoutPath: "users/logout",
            registerPath: "users/register",
            updateJwtPairPath: "users/updateJwtPair",
            getUserByIdPath: "users/id/{id}",
            getUserByJwtPath: "users/jwt/{jwt}",
            isEmailBusyPath: "users/email/{email}/busy",
            isUsernameBusyPath: "users/username/{username}/busy");
    }

    [Fact]
    public void Login_AccountServiceUrlInfo_GenerateSpecificUrls()
    {
        Assert.Equal(new Uri("http://account-service/users/login"), UrlProvider.Login);
        Assert.Equal(new Uri("http://account-service/users/logout"), UrlProvider.Logout);
        Assert.Equal(new Uri("http://account-service/users/register"), UrlProvider.Register);
        Assert.Equal(new Uri("http://account-service/users/updateJwtPair"), UrlProvider.UpdateJwtPair);
        Assert.Equal(new Uri("http://account-service/users/id/sampleId"), UrlProvider.MakeGetUserByIdUrl("sampleId"));
        Assert.Equal(new Uri("http://account-service/users/jwt/sampleJwt"), UrlProvider.MakeGetUserByJwtTokenUrl("sampleJwt"));
    }
}