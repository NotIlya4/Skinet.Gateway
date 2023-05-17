using System.Text;
using Infrastructure.Auther;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Infrastructure.Yarp.BasketService;

public class GetBasketForwardInfo : IForwardInfo
{
    public RouteConfig Route { get; }
    public ClusterConfig Cluster { get; }

    public GetBasketForwardInfo(BasketServiceForwardInfoOptions options)
    {
        Route = new RouteConfig()
        {
            RouteId = $"get {options.ServiceName}",
            ClusterId = $"get {options.ServiceName}",
            Match = new RouteMatch
            {
                Methods = new[] { "GET" },
                Path = options.MatchPath,
            }
        };
        Cluster = new ClusterConfig()
        {
            ClusterId = $"get {options.ServiceName}",
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
        context.Path = new PathString($"/baskets/user/id/{userInfo.Id.ToString()}");
    }

    public async Task TransformResponse(ResponseTransformContext context)
    {
        if (context.ProxyResponse is null)
        {
            return;
        }

        if (context.ProxyResponse.IsSuccessStatusCode)
        {
            JToken items = JObject.Parse(await context.ProxyResponse.Content.ReadAsStringAsync())["items"]!;
            context.ProxyResponse.Content = new StringContent(items.ToString(), Encoding.UTF8, "application/json");
        }
    }
}