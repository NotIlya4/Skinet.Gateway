using Infrastructure.Auther;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp.AccountService;

public class AccountServiceForwardInfo : IForwardInfo
{
    public RouteConfig Route { get; }
    public ClusterConfig Cluster { get; }

    public AccountServiceForwardInfo(AccountServiceForwardInfoOptions options)
    {
        Route = new RouteConfig()
        {
            RouteId = $"general {options.ServiceName}",
            ClusterId = $"general {options.ServiceName}",
            Match = new RouteMatch()
            {
                Path = options.MatchPath
            },
        };
        Cluster = new ClusterConfig()
        {
            ClusterId = $"general {options.ServiceName}",
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                [options.ServiceName] = new()
                {
                    Address = options.DestinationUrl
                }
            }
        };
    }
    
    public Task TransformRequest(RequestTransformContext context, IAuther auther)
    {
        return Task.CompletedTask;
    }
}