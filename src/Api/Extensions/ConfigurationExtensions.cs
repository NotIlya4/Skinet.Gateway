using Infrastructure.Auther.Helpers;
using Infrastructure.Yarp.AccountService;
using Infrastructure.Yarp.BasketService;
using Infrastructure.Yarp.ProductService;

namespace Api.Extensions;

public static class ConfigurationExtensions
{
    public static ProductServiceForwardInfoOptions ProductServiceForwarderOptions(this IConfiguration config, string key)
    {
        var section = config.GetSection(key);
        return new ProductServiceForwardInfoOptions(
            id: "product-service",
            matchPath: "products/{**catch-all}",
            destinationUrl: section.GetRequiredValue("DestinationUrl"));
    }
    
    public static BasketServiceForwardInfoOptions BasketServiceForwardInfoOptions(this IConfiguration config, string key)
    {
        var section = config.GetSection(key);
        return new BasketServiceForwardInfoOptions(
            serviceName: "basket-service",
            matchPath: "baskets/{**catch-all}",
            destinationUrl: section.GetRequiredValue("DestinationUrl"));
    }

    public static AccountServiceForwardInfoOptions AccountServiceForwardInfoOptions(this IConfiguration config, string key)
    {
        var section = config.GetSection(key);
        return new AccountServiceForwardInfoOptions(
            serviceName: "account-service",
            matchPath: "users/{**catch-all}",
            destinationUrl: section.GetRequiredValue("DestinationUrl"));
    }

    public static AccountServiceUrlProvider AccountServiceUrlProvider(this IConfiguration config, string key)
    {
        var section = config.GetSection(key);
        return new AccountServiceUrlProvider(
            baseUrl: GetRequiredValue<string>(section, "BaseUrl"),
            getUserByJwtPath: GetRequiredValue<string>(section, "GetUserByJwtPath")); 
    }
    
    public static string GetRequiredValue(this IConfiguration config, string key)
    {
        return config.GetRequiredValue<string>(key);
    }
    
    public static T GetRequiredValue<T>(this IConfiguration config, string key)
    {
        T? value = config.GetValue<T>(key);
        if (value is null)
        {
            throw new InvalidOperationException(key);
        }
        return value;
    }
}