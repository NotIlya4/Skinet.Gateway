using System.Text;
using Infrastructure.Auther;
using Newtonsoft.Json.Linq;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp.BasketService;

public class PostBasketForwardInfo : IForwardInfo
{
    public RouteConfig Route { get; }
    public ClusterConfig Cluster { get; }

    public PostBasketForwardInfo(BasketServiceForwardInfoOptions options)
    {
        Route = new RouteConfig()
        {
            RouteId = $"post {options.ServiceName}",
            ClusterId = $"post {options.ServiceName}",
            Match = new RouteMatch
            {
                Methods = new [] { "POST" },
                Path = options.MatchPath,
            }
        };
        Cluster = new ClusterConfig()
        {
            ClusterId = $"post {options.ServiceName}",
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
        JArray items = JArray.Parse(await context.ProxyRequest.Content!.ReadAsStringAsync());
        var body = new JObject()
        {
            ["userId"] = userInfo.Id.ToString(),
            ["items"] = items
        };
        context.ProxyRequest.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
    }

    public Task TransformResponse(ResponseTransformContext context)
    {
        return Task.CompletedTask;
    }
}