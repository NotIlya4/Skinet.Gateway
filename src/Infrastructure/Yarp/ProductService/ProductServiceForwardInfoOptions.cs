namespace Infrastructure.Yarp.ProductService;

public class ProductServiceForwardInfoOptions
{
    public string Id { get; }
    public string MatchPath { get; }
    public string DestinationUrl { get; }

    public ProductServiceForwardInfoOptions(string id, string matchPath, string destinationUrl)
    {
        Id = id;
        MatchPath = matchPath;
        DestinationUrl = destinationUrl;
    }
}