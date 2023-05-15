using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp;

public interface IRequestTransformer
{
    public ValueTask Transform(RequestTransformContext context);
}