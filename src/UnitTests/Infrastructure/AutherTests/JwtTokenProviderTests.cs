using HttpContextMoq;
using HttpContextMoq.Extensions;
using Infrastructure.Auther.Helpers;
using Infrastructure.Auther.JwtTokenProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;

namespace UnitTests.Infrastructure.AutherTests;

public class JwtTokenProviderTests
{
    private readonly string _jwt = "There must be jwt";
    private readonly HttpContextMock _httpContextMock;
    private readonly JwtTokenProvider _provider;
    
    public JwtTokenProviderTests()
    {
        _httpContextMock = new HttpContextMock();
        var accessor = new Mock<IHttpContextAccessor>();
        accessor.Setup(a => a.HttpContext).Returns(_httpContextMock);
        _provider = new JwtTokenProvider(accessor.Object);
    }
    
    [Fact]
    public void Read_AuthorizationHeaderWithBearerPrefix_JwtTokenWithoutBearerPrefix()
    {
        _httpContextMock.SetupRequestHeaders(new Dictionary<string, StringValues>() { ["Authorization"] = $"Bearer {_jwt}" });

        var result = _provider.Read();
        
        Assert.Equal(_jwt, result);
    }

    [Fact]
    public void Read_AuthorizationHeaderWithoutBearerPrefix_JwtTokenWithoutBearerPrefix()
    {
        _httpContextMock.SetupRequestHeaders(new Dictionary<string, StringValues>() { ["Authorization"] = _jwt });

        var result = _provider.Read();
        
        Assert.Equal(_jwt, result);
    }

    [Fact]
    public void Read_NoAuthorizationHeader_Throw()
    {
        Assert.Throws<JwtTokenNotProvidedException>(() => _provider.Read());
    }
}