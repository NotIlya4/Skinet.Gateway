using Infrastructure.AccountService.Helpers;
using Infrastructure.BasketService;
using Infrastructure.ProductService.Helpers;

namespace Api.Properties;

public class ParametersProvider
{
    private readonly IConfiguration _config;

    public ParametersProvider(IConfiguration config)
    {
        _config = config;
    }

    public AccountServiceUrlProvider GetAccountServiceUrlProvider()
    {
        var section = _config.GetSection("AccountServiceUrls");
        return new AccountServiceUrlProvider(
            baseUrl: GetRequiredValue<string>(section, "BaseUrl"),
            loginPath: GetRequiredValue<string>(section, "LoginPath"),
            logoutPath: GetRequiredValue<string>(section, "LogoutPath"),
            registerPath: GetRequiredValue<string>(section, "RegisterPath"),
            updateJwtPairPath: GetRequiredValue<string>(section, "UpdateJwtPairPath"),
            getUserByIdPath: GetRequiredValue<string>(section, "GetUserByIdPath"),
            getUserByJwtPath: GetRequiredValue<string>(section, "GetUserByJwtPath"),
            isEmailBusyPath: GetRequiredValue<string>(section, "IsEmailBusyPath"),
            isUsernameBusyPath: GetRequiredValue<string>(section, "IsUsernameBusyPath"));
    }
    
    public ProductServiceUrlProvider GetProductServiceUrlProvider()
    {
        var section = _config.GetSection("ProductServiceUrls");
        return new ProductServiceUrlProvider(
            baseUrl: GetRequiredValue<string>(section, "BaseUrl"),
            brandsPath: GetRequiredValue<string>(section, "BrandsPath"),
            productTypesPath: GetRequiredValue<string>(section, "ProductTypesPath"),
            productsPath: GetRequiredValue<string>(section, "ProductsPath"),
            getProductByIdPath: GetRequiredValue<string>(section, "GetProductByIdPath"));
    }

    public BasketServiceUrlProvider GetBasketServiceUrlProvider()
    {
        var section = _config.GetSection("BasketServiceUrls");
        return new BasketServiceUrlProvider(
            baseUrl: GetRequiredValue<string>(section, "BaseUrl"),
            postBasketPath: GetRequiredValue<string>(section, "PostBasketPath"),
            getBasketUrlPath: GetRequiredValue<string>(section, "GetBasketUrlPath"));
    }
    
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