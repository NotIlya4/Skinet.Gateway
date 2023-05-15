using Api.Extensions;
using Infrastructure.Auther.Helpers;
using Infrastructure.Yarp.AccountService;
using Infrastructure.Yarp.BasketService;
using Infrastructure.Yarp.ProductService;

namespace Api.Properties;

public class ParametersProvider
{
    private readonly IConfiguration _config;

    public ParametersProvider(IConfiguration config)
    {
        _config = config;
    }

    public string Seq => GetRequiredValue<string>("SeqUrl");

    public AccountServiceUrlProvider AccountServiceUrlProvider =>
        _config.AccountServiceUrlProvider("AccountServiceUrls");

    public ProductServiceForwardInfoOptions ProductServiceForwardInfoOptions =>
        _config.ProductServiceForwarderOptions("ProductServiceForwardInfo");
    
    public BasketServiceForwardInfoOptions BasketServiceForwardInfoOptions =>
        _config.BasketServiceForwardInfoOptions("BasketServiceForwardInfo");
    
    public AccountServiceForwardInfoOptions AccountServiceForwardInfoOptions =>
        _config.AccountServiceForwardInfoOptions("AccountServiceForwardInfo");

    public IConfiguration Yarp => _config.GetSection("Yarp");
    
    public T GetRequiredValue<T>(string key)
    {
        return GetRequiredValue<T>(_config, key);
    }
    
    public T GetRequiredValue<T>(IConfiguration config, string key)
    {
        T? value = config.GetValue<T>(key);
        if (value is null)
        {
            throw new InvalidOperationException(key);
        }
        return value;
    }
}