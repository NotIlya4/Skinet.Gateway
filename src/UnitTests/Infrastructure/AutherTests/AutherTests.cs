using Infrastructure.Auther;
using Infrastructure.Auther.Helpers;
using Infrastructure.Auther.JwtTokenProvider;
using Infrastructure.Auther.Models;
using Infrastructure.Auther.SimpleHttpClient;
using Moq;

namespace UnitTests.Infrastructure.AutherTests;

public class AutherTests
{
    private readonly string _jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ik1FR0EgRUFTVEVSIEVHRyEhISIsImlhdCI6MTUxNjIzOTAyMn0.WWNUQstZK-8ieMuwb1Hk3hHgcj_u4iAdEbGn9kyTThM";
    private readonly AccountServiceUrlProvider _url = new(baseUrl: "http://localhost:5000", "users/jwt/{jwt}");
    private readonly Mock<ISimpleHttpClient> _client = new();
    private readonly Auther _auther;
    
    public AutherTests()
    {
        var jwtTokenProvider = new Mock<IJwtTokenProvider>();
        jwtTokenProvider.Setup(p => p.Read()).Returns(_jwt);
        _auther = new Auther(jwtTokenProvider.Object, _client.Object, _url);
    }
    
    [Fact]
    public async Task GetUserInfo_StubbedJwtProvider_CallClientWithSpecificUrl()
    {
        await _auther.GetUserInfo();

        _client.Verify(c => c.Get<UserInfo>(new Uri($"http://localhost:5000/users/jwt/{_jwt}")), Times.Once());
    }
}