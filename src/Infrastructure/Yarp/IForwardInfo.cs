using Infrastructure.Auther;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp;

public interface IForwardInfo
{
    public RouteConfig Route { get; }
    public ClusterConfig Cluster { get; }
    public Task TransformRequest(RequestTransformContext context, IAuther auther);
    public Task TransformResponse(ResponseTransformContext context);
}