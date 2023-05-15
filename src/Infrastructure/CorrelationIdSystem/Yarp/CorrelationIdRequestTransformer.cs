using Infrastructure.CorrelationIdSystem.Repository;
using Infrastructure.Yarp;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.CorrelationIdSystem.Yarp;

public class CorrelationIdRequestTransformer : IRequestTransformer
{
    private readonly ICorrelationIdProvider _provider;

    public CorrelationIdRequestTransformer(ICorrelationIdProvider provider)
    {
        _provider = provider;
    }

    ValueTask IRequestTransformer.Transform(RequestTransformContext context)
    {
        Guid? id = _provider.GetCorrelationId();
        if (id.HasValue)
        {
            context.ProxyRequest.Headers.Add("x-request-id", id.Value.ToString());
        }
        return ValueTask.CompletedTask;
    }
}