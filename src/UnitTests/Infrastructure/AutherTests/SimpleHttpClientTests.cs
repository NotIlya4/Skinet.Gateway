using System.Net;
using Infrastructure.Auther.Models;
using Infrastructure.Auther.SimpleHttpClient;
using Infrastructure.CorrelationIdSystem.Repository;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Contrib.HttpClient;

namespace UnitTests.Infrastructure.AutherTests;

public class SimpleHttpClientTests
{
    private readonly SimpleHttpClient _simpleHttpClient;
    private readonly Mock<HttpMessageHandler> _httpMessageHandler;
    private readonly CorrelationIdRepository _correlationIdRepository;
    private readonly Uri _uri = new Uri("http://localhost:5000");
    
    public SimpleHttpClientTests()
    {
        _httpMessageHandler = new Mock<HttpMessageHandler>();
        var context = new DefaultHttpContext();
        var accessor = new Mock<IHttpContextAccessor>();
        accessor.Setup(a => a.HttpContext).Returns(context);
        _correlationIdRepository = new CorrelationIdRepository(accessor.Object);
        _simpleHttpClient = new SimpleHttpClient(new HttpClient(_httpMessageHandler.Object), _correlationIdRepository);
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

    [Fact]
    public async Task Get_CallGenerateCorrelationId_ClientSendRequestWithXRequestIdHeader()
    {
        _correlationIdRepository.GenerateAndSave();
        _httpMessageHandler.SetupAnyRequest().ReturnsJsonResponse("true");

        await _simpleHttpClient.Get<bool>(_uri);
        
        _httpMessageHandler.VerifyRequest(m =>
        {
            string header = m.Headers.First(h => h.Key == "X-Request-ID").Value.Single();
            return _correlationIdRepository.GetCorrelationId()!.Value.ToString() == header;
        }, Times.Once());
    }
}