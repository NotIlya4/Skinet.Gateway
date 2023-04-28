using Infrastructure.BasketService.Models;
using Infrastructure.Client;

namespace Infrastructure.BasketService;

public class BasketService : IBasketService
{
    private readonly IServiceHttpClient _client;
    private readonly BasketServiceUrlProvider _urlProvider;

    public BasketService(IServiceHttpClient client, BasketServiceUrlProvider urlProvider)
    {
        _client = client;
        _urlProvider = urlProvider;
    }
    
    public async Task Insert(Basket basket)
    {
        await _client.Post(_urlProvider.PostBasket, basket);
    }

    public Task<Basket> Get(Guid userId)
    {
        return _client.Get<Basket>(_urlProvider.MakeGetBasketUrl(userId));
    }
}