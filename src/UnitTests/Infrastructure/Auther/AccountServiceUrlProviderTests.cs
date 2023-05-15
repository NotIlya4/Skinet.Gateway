using Infrastructure.Auther.Helpers;

namespace UnitTests;

public class AccountServiceUrlProviderTests
{
    public AccountServiceUrlProvider UrlProvider { get; }
    
    public AccountServiceUrlProviderTests()
    {
        UrlProvider = new AccountServiceUrlProvider(
            baseUrl: "http://account-service",
            getUserByJwtPath: "users/jwt/{jwt}");
    }

    [Fact]
    public void Login_AccountServiceUrlInfo_GenerateSpecificUrls()
    {
        Assert.Equal(new Uri("http://account-service/users/jwt/sampleJwt"), UrlProvider.MakeGetUserByJwtTokenUrl("sampleJwt"));
    }
}