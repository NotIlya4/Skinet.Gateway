using Infrastructure.CorrelationIdSystem.Repository;
using Infrastructure.CorrelationIdSystem.Yarp;
using Microsoft.AspNetCore.Http;
using Moq;
using Yarp.ReverseProxy.Transforms;

namespace UnitTests.Infrastructure.CorrelationIdSystem;

public class CorrelationIdRequestTransformerTests
{
    private readonly CorrelationIdRepository _correlationIdRepository;
    private readonly CorrelationIdRequestTransformer _transformer;

    public CorrelationIdRequestTransformerTests()
    {
        var accessor = new Mock<IHttpContextAccessor>();
        var context = new DefaultHttpContext();
        accessor.Setup(a => a.HttpContext).Returns(context);
        _correlationIdRepository = new CorrelationIdRepository(accessor.Object);
        _transformer = new CorrelationIdRequestTransformer(_correlationIdRepository);
    }

    [Fact]
    public async Task Transform_HttpContextContainsCorrelationId_RequestContainHeaderAfterTransformRequest()
    {
        _correlationIdRepository.GenerateAndSave();
        var request = new HttpRequestMessage();
        await _transformer.Transform(new RequestTransformContext() { ProxyRequest = request });

        string result = request.Headers.First(h => h.Key == "X-Request-ID").Value.Single();
        
        Assert.Equal(_correlationIdRepository.GetCorrelationId()!.Value.ToString(), result);
    }
}