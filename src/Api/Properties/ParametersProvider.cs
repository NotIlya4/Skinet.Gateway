using Infrastructure;

namespace Api.Properties;

public class ParametersProvider
{
    private readonly IConfiguration _config;

    public ParametersProvider(IConfiguration config)
    {
        _config = config;
    }

    public LinkProvider GetLinkProvider()
    {
        return new LinkProvider(GetRequiredValue<string>("Links:AccountService"),
            GetRequiredValue<string>("Links:ProductService"));
    }
    
    public T GetRequiredValue<T>(string key)
    {
        T? value = _config.GetValue<T>(key);
        if (value is null)
        {
            throw new InvalidOperationException(key);
        }
        return value;
    }
}