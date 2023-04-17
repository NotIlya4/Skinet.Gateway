namespace Infrastructure;

public class LinkProvider
{
    public string AccountService { get; }
    public string ProductService { get; }

    public LinkProvider(string accountService, string productService)
    {
        AccountService = accountService;
        ProductService = productService;
    }
}