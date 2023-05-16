using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp;

public interface IRequestTransformer
{
    public Task Transform(RequestTransformContext context);
}