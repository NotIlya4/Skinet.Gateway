using Infrastructure.Auther.Client;
using Infrastructure.Auther.Helpers;
using Infrastructure.Auther.Models;
using Moq;

namespace UnitTests.Auther;

public class AutherTests
{
    private readonly Mock<IJwtTokenProvider> _jwtTokenProvider;
    private readonly string _jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik1FR0EgRUFTVEVSIEVHRyEhISIsImlhdCI6MTUxNjIzOTAyMn0.WWNUQstZK-8ieMuwb1Hk3hHgcj_u4iAdEbGn9kyTThM";
    private readonly AccountServiceUrlProvider _url = new(baseUrl: "http://localhost:5000", "users/jwt/{jwt}");
    private readonly Mock<ISimpleHttpClient> _client = new();
    private readonly Infrastructure.Auther.Auther _auther;
    
    public AutherTests()
    {
        _jwtTokenProvider = new Mock<IJwtTokenProvider>();
        _jwtTokenProvider.Setup(p => p.Read()).Returns(_jwt);
        _auther = new Infrastructure.Auther.Auther(_jwtTokenProvider.Object, _client.Object, _url);
    }
    
    [Fact]
    public async Task GetUserInfo_StubbedJwtProvider_CallClientWithSpecificUrl()
    {
        await _auther.GetUserInfo();

        _client.Verify(c => c.Get<UserInfo>(new Uri($"http://localhost:5000/users/jwt/{_jwt}")), Times.Once());
    }
}