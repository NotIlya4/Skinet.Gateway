using Infrastructure.Auther;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp.ProductService;

public class ProductServiceForwardInfo : IForwardInfo
{
    public RouteConfig Route { get; }
    public ClusterConfig Cluster { get; }

    public ProductServiceForwardInfo(ProductServiceForwardInfoOptions infoOptions)
    {
        Route = new RouteConfig()
        {
            RouteId = infoOptions.Id,
            ClusterId = infoOptions.Id,
            Match = new RouteMatch
            {
                Path = infoOptions.MatchPath
            }
        };
        Cluster = new ClusterConfig()
        {
            ClusterId = infoOptions.Id,
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                [infoOptions.Id] = new()
                {
                    Address = infoOptions.DestinationUrl
                }
            }
        };
    }

    public Task TransformRequest(RequestTransformContext context, IAuther auther)
    {
        return Task.CompletedTask; 
    }

    public Task TransformResponse(ResponseTransformContext context)
    {
        return Task.CompletedTask;
    }
}