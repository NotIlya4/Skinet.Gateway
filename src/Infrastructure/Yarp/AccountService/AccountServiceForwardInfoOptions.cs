namespace Infrastructure.Yarp.AccountService;

public class AccountServiceForwardInfoOptions
{
    public string ServiceName { get; }
    public string MatchPath { get; }
    public string DestinationUrl { get; }

    public AccountServiceForwardInfoOptions(string serviceName, string matchPath, string destinationUrl)
    {
        ServiceName = serviceName;
        MatchPath = matchPath;
        DestinationUrl = destinationUrl;
    }
}