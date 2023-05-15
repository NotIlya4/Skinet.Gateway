namespace Infrastructure.Yarp.BasketService;

public class BasketServiceForwardInfoOptions
{
    public string ServiceName { get; }
    public string MatchPath { get; }
    public string DestinationUrl { get; }

    public BasketServiceForwardInfoOptions(string serviceName, string matchPath, string destinationUrl)
    {
        ServiceName = serviceName;
        MatchPath = matchPath;
        DestinationUrl = destinationUrl;
    }
}