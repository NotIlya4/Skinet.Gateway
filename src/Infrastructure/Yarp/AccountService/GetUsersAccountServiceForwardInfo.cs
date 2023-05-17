using Infrastructure.Auther;
using Microsoft.AspNetCore.Http;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp.AccountService;

public class GetUsersAccountServiceForwardInfo : IForwardInfo
{
    public RouteConfig Route { get; }
    public ClusterConfig Cluster { get; }

    public GetUsersAccountServiceForwardInfo(AccountServiceForwardInfoOptions options)
    {
        Route = new RouteConfig()
        {
            RouteId = $"get user {options.ServiceName}",
            ClusterId = $"get user {options.ServiceName}",
            Match = new RouteMatch()
            {
                Path = "users"
            },
        };
        Cluster = new ClusterConfig()
        {
            ClusterId = $"get user {options.ServiceName}",
            Destinations = new Dictionary<string, DestinationConfig>()
            {
                [options.ServiceName] = new()
                {
                    Address = options.DestinationUrl
                }
            }
        };
    }
    
    public async Task TransformRequest(RequestTransformContext context, IAuther auther)
    {
        var userInfo = await auther.GetUserInfo();
        context.Path = new PathString($"/users/id/{userInfo.Id}");
    }

    public Task TransformResponse(ResponseTransformContext context)
    {
        return Task.CompletedTask;
    }
}