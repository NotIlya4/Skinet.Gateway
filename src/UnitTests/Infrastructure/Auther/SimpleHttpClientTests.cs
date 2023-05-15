using System.Net;
using Infrastructure.Auther.Client;
using Infrastructure.Auther.Models;
using Moq;
using Moq.Contrib.HttpClient;
using Newtonsoft.Json.Linq;

namespace UnitTests.Auther;

public class SimpleHttpClientTests
{
    private readonly SimpleHttpClient _simpleHttpClient;
    private readonly Mock<HttpMessageHandler> _httpMessageHandler;
    private readonly Uri _uri = new Uri("http://localhost:5000");
    
    public SimpleHttpClientTests()
    {
        _httpMessageHandler = new Mock<HttpMessageHandler>();
        _simpleHttpClient = new SimpleHttpClient(new HttpClient(_httpMessageHandler.Object));
    }

    [Fact]
    public async Task Get_BadResponse_Throw()
    {
        _httpMessageHandler.SetupAnyRequest().ReturnsJsonResponse(HttpStatusCode.BadRequest,
            new { Title = "Bad request", Detail = "BadRequest" });

        await Assert.ThrowsAsync<ServiceBadResponseException>(async () => await _simpleHttpClient.Get<UserInfo>(_uri));
    }

    [Fact]
    public async Task Get_ReceiveComplexObject_ParseIt()
    {
        var expect = new UserInfo(new Guid("9bc6a763-ab2e-400d-b892-d6ffb09ce6a1"), "Biba", "biba@email.com");
        _httpMessageHandler.SetupAnyRequest().ReturnsJsonResponse(expect);

        UserInfo result = await _simpleHttpClient.Get<UserInfo>(_uri);
        
        Assert.Equal(expect, result);
    }

    [Fact]
    public async Task Get_ReceivePrimitive_ParseItToo()
    {
        var expect = 10;
        _httpMessageHandler.SetupAnyRequest().ReturnsJsonResponse("10");

        var result = await _simpleHttpClient.Get<int>(_uri);
        
        Assert.Equal(expect, result);
    }
    
    [Fact]
    public async Task Get_ReceiveAnotherPrimitive_ParseItToo()
    {
        var expect = true;
        _httpMessageHandler.SetupAnyRequest().ReturnsJsonResponse("true");

        var result = await _simpleHttpClient.Get<bool>(_uri);
        
        Assert.Equal(expect, result);
    }
}